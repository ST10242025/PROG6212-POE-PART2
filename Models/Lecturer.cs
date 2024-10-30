using Microsoft.AspNetCore.Identity;

namespace CMCS.Models
{
    public class Lecturer : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? ContractNumber { get; set; }
    }
}
