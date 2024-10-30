using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;



namespace CMCS.Models
{
    public class Claims
    {
        [Key]
        public int id { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Lecturer surname is required")]
        [Display(Name = "Surname")]
        public string? Surname { get; set; }

        public string? Status { get; set; }

        [Required(ErrorMessage = "Lecturer email is required")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Hours worked is required")]
        [Display(Name = "Hours worked")]
        public decimal? HoursWorked { get; set; }

        [Required(ErrorMessage = "Hourly rate required")]
        [Display(Name = "Hourly rate")]
        public decimal? HourlyRate { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Date")]
        public DateTime ClaimDate { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Documentation is required")]
        [Display(Name = "Documentation")]
        public string? Documentation { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }    
}
