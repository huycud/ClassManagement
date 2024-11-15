using Microsoft.AspNetCore.Identity;

namespace ClassManagement.Api.Data.Entities
{
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}
