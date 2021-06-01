using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Service.Data.Models
{
    public class UserRole : IdentityUserRole<Guid>
    {
        [Key, Column(Order = 0)]
        public override Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Key, Column(Order = 1)]
        public override Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

    }
}