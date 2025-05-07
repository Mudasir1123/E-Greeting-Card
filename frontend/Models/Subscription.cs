using System;
using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }

        [Required]
        public string EmailList { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;

        public bool PaymentVerified { get; set; } = false;

        // Foreign key to Offer (optional)
        public int? OfferId { get; set; }
        public Offer Offer { get; set; }
    }

}
