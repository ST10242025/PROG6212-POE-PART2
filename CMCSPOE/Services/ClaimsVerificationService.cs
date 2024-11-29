using CMCSPOE.Models;

namespace CMCSPOE.Services
{
    public class ClaimsVerificationService
    {
        public (bool isValid, string? flagReason) VerifyClaim(Claims claim)
        {
            if (claim.HoursWorked < 1 || claim.HoursWorked > 40)
                return (false, "Hours worked must be between 1 and 40.");

            if (claim.HourlyRate < 10 || claim.HourlyRate > 200)
                return (false, "Hourly rate must be between R10 and R200.");

            if (claim.ClaimDate < DateTime.Now.AddMonths(-1) || claim.ClaimDate > DateTime.Now)
                return (false, "Claim date must be within the current month.");

            if (string.IsNullOrEmpty(claim.Documentation))
                return (false, "Documentation is required.");

            return (true, null); // Claim passes all checks
        }
    }
}
