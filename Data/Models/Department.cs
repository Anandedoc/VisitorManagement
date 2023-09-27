namespace Data.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        public string? Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
