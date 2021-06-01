using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Service.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            Roles = new HashSet<UserRole>();
        }

        [Key]
        public override Guid Id { get; set; }



        [InverseProperty(nameof(UserRole.User))]
        public virtual ICollection<UserRole> Roles { get; set; }
    }
}