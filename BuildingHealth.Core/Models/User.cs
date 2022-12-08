using System;
using System.Collections.Generic;

namespace BuildingHealth.Core.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }

        public virtual Admin? Admin { get; set; }
        public virtual Architect? Architect { get; set; }
        public virtual Builder? Builder { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
