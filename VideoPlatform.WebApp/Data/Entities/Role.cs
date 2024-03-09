using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VideoPlatform.WebApp.Data.Entities
{
    public class Role : IdentityRole<Guid>
    {
        [StringLength(255)]
        public string Color { get; set; }
    }
}
