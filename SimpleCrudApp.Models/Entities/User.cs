using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrudApp.Models.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        public virtual ICollection<Project>? Projects { get; set; }

    }
}
