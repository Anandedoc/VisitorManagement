namespace Data.Models
{
    public class VisitorDetails : AbstractEntity
    {

        [Required(ErrorMessage = "Name field is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email field is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        [EmailAddress]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Phone field is required.")]
        //[StringLength(maximumLength: 15, MinimumLength = 10)]
        public string? Phone { get; set; }
        public string? ByVechicle { get; set; }

        public string? Address { get; set; }

        public string Description { get; set; }
        [DisplayName("To Meet")]
        public string? ToUserId { get; set; }

        [ForeignKey("ToUserId")]
        [ValidateNever]

        public virtual User? User { get; set; }

        public string? Date { get; set; }
        [DisplayName("Timing")]
        public DateTime? AppointMentTime { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }

        [DisplayName("Status")]
        public VisitingStatus VisitingStatus { get; set; } = VisitingStatus.Pending;

    }
}
