using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace frontend.Models
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public int? OfferId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        // Changed to int
        public int Amount { get; set; }

        // Changed to int
        public int DiscountAmount { get; set; }

        // Changed to int
        public int FinalAmount { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? CancelledDate { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; }

        [StringLength(100)]
        public string PaymentReference { get; set; }
    }
}
