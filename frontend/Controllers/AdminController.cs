using frontend.Areas.Identity.Data;
using frontend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace frontend.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<frontendUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<frontendUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Dashboard Pages
        public IActionResult Dashboard() => View();
        public IActionResult CardManagement() => View();
        public IActionResult Categories() => View();
        public IActionResult Templates() => View();
        public IActionResult Subscriptions() => View();
        public IActionResult Analytics() => View();
        public IActionResult Settings() => View();

        // READ: User List
        public async Task<IActionResult> UserManagement()
        {
            var users = await _userManager.Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    // Removed CreatedAt and IsActive
                }).ToListAsync();

            foreach (var user in users)
            {
                var identityUser = await _userManager.FindByIdAsync(user.Id);
                var roles = await _userManager.GetRolesAsync(identityUser);
                user.Role = roles.FirstOrDefault() ?? "None";
            }

            return View(users);
        }

        // CREATE: Add new user (GET)
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        // CREATE: Add new user (POST)
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new frontendUser
            {
                UserName = model.UserName,
                Email = model.Email
                // Removed CreatedAt and IsActive
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(model.Role))
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));

                await _userManager.AddToRoleAsync(user, model.Role);

                return RedirectToAction(nameof(UserManagement));
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // UPDATE: Edit user (GET)
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            var model = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = role
                // Removed IsActive
            };

            return View(model);
        }

        // UPDATE: Edit user (POST)
        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            user.UserName = model.UserName;
            user.Email = model.Email;
            // Removed IsActive update

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

                if (!await _roleManager.RoleExistsAsync(model.Role))
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));

                await _userManager.AddToRoleAsync(user, model.Role);

                return RedirectToAction(nameof(UserManagement));
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // DELETE: Confirm Delete (GET)
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            var model = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = role
            };

            return View(model);
        }

        // DELETE: Final Delete (POST)
        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return RedirectToAction(nameof(UserManagement));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View("DeleteUser", new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            });
        }
    }
}
