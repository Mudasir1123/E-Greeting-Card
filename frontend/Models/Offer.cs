using System;
using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Offer name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Discount percentage is required")]
        [Range(1, 100, ErrorMessage = "Discount must be between 1 and 100")]
        public int DiscountPercentage { get; set; }

        [Required(ErrorMessage = "Offer code is required")]
        [StringLength(20, ErrorMessage = "Offer code cannot be longer than 20 characters")]
        public string OfferCode { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
