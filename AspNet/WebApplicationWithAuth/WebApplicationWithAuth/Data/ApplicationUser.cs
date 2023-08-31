using Microsoft.AspNetCore.Identity;

namespace WebApplicationWithAuth
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; }

        public DateTime LastVisitedAt { get; set; }
    }
}