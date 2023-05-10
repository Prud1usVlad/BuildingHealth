using Microsoft.AspNetCore.Identity;

namespace BuildingHealth.Core.Models
{
    public partial class User : IdentityUser
    {
        public User()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Phone { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
        public virtual Admin? Admin { get; set; }
        public virtual Architect? Architect { get; set; }
        public virtual Builder? Builder { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
