using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Service.Data.Models
{
    public class Role : IdentityRole<Guid>
    {
        [Key]
        public override Guid Id { get; set; }

    }
}