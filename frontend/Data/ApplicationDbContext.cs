using Microsoft.EntityFrameworkCore;
using frontend.Models;


namespace frontend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ECardTemplate> ECardTemplates { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Category and ECardTemplate
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Templates)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId);

            // Configure the relationship between Offer and Subscription
            modelBuilder.Entity<Offer>()
                .HasMany(o => o.Subscriptions)
                .WithOne(s => s.Offer)
                .HasForeignKey(s => s.OfferId);

            // Note: Removed all relationships that involved the User entity
            // The Subscription, Feedback, and Transaction entities will need to be updated
            // to remove their User navigation properties and UserId foreign keys
        }
    }
}