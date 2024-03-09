using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoPlatform.WebApp.Data.Entities
{
    public class Channel : IdentityUser<Guid>
    {
        [StringLength(200, ErrorMessage = "Description must be up to 200 characters long!")]
        public string? Description { get; set; }

        public DateOnly DateCreated { get; set; }

        [ForeignKey(nameof(Role))]
        public Guid RoleId { get; set; }

        public Role Role { get; set; }
        public string? Username { get; internal set; }
        public object Password { get; internal set; }

        public int ChannelId { get; set; }
    }
}
