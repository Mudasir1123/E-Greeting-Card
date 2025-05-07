using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }

}
