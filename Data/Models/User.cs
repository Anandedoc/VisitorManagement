namespace Data.Models
{
    public class User : IdentityUser
    {
        [DisplayName("Employee Id")]
        public long? UserId { get; set; }

        [Required(ErrorMessage = "Name field is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        [DisplayName("To Meet")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email field is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        [EmailAddress]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Phone field is required.")]
        [StringLength(maximumLength: 15, MinimumLength = 10)]

        public string? UserRole { get; set; }

        public string? Address { get; set; }
        [ForeignKey("DepartmentId")]
        [DisplayName("Department")]
        [ValidateNever]
        public long? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public string? CreatedById { get; set; }

        public DateTimeOffset? CreatedTime { get; set; }

        public string? UpdatedById { get; set; }

        public DateTimeOffset? UpdatedTime { get; set; }

        public void UpdateAuditFields(EntityState state, string userId, DateTimeOffset dateTimeOffset)
        {
            switch (state)
            {
                case EntityState.Added:
                    CreatedById = userId;
                    CreatedTime = dateTimeOffset;
                    break;

                case EntityState.Modified:
                    UpdatedById = userId;
                    UpdatedTime = dateTimeOffset;
                    break;
            }
        }

    }
}