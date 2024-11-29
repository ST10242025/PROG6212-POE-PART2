using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;



namespace CMCSPOE.Models
{
    public class Claims
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Lecturer surname is required")]
        [Display(Name = "Surname")]
        public string? Surname { get; set; }

        public string? Status { get; set; } = "Pending"; // Default status

        [Required(ErrorMessage = "Lecturer email is required")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Hours worked is required")]
        [Range(1, 100, ErrorMessage = "Hours worked must be between 1 and 100")]
        public decimal? HoursWorked { get; set; }

        [Required(ErrorMessage = "Hourly rate is required")]
        [Range(10, 200, ErrorMessage = "Hourly rate must be between R10 and R200")]
        public decimal? HourlyRate { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Date")]
        public DateTime ClaimDate { get; set; }
        
        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Documentation is required")]
        [Display(Name = "Documentation")]
        public string? Documentation { get; set; }

        [Display(Name = "Total")]
        public decimal Total => (HoursWorked ?? 0) * (HourlyRate ?? 0);
        public DateTime CreatedAt { get; set; }
         
        public bool IsAutomaticallyApproved { get; set; } // Indicates system-approved claims

        public string? FlagReason { get; set; }

        // Grouping Logic (for combining the ViewModel)
        public static ClaimsGroupedByStatus GroupByStatus(IEnumerable<Claims> claims)
        {
            return new ClaimsGroupedByStatus
            {
                PendingClaims = claims.Where(c => c.Status == "Pending").ToList(),
                ApprovedClaims = claims.Where(c => c.Status == "Approved").ToList(),
                RejectedClaims = claims.Where(c => c.Status == "Rejected").ToList()
            };
        }
    }

    // Helper class for grouping claims
    public class ClaimsGroupedByStatus
    {
        public List<Claims> PendingClaims { get; set; } = new();
        public List<Claims> ApprovedClaims { get; set; } = new();
        public List<Claims> RejectedClaims { get; set; } = new();
    }

}


