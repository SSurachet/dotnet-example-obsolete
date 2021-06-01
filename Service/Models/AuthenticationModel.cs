using System;
using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

    public class LoginTokenModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}