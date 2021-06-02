using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Service.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        [Key]
        public override Guid Id { get; set; }

    }
}