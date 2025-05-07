using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<ECardTemplate> Templates { get; set; }
    }

}
