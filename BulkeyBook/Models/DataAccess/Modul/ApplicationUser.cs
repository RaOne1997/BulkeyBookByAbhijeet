using Microsoft.AspNetCore.Identity;

namespace BulkeyBook.Models.DataAccess.Modul
{
    public class UserINtoUser : IdentityUser
    {
        public string? Name { get; set; }    
    }
}
