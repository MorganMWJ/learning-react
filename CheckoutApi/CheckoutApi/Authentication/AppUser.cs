using Microsoft.AspNetCore.Identity;

namespace CheckoutApi.Authentication
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // Other custom properties...

        // Custom methods, if needed...
    }
}
