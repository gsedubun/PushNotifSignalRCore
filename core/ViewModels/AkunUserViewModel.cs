using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.ViewModels
{
    public class AkunUserViewModel
    {
        [MaxLength(200)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }

    public class AkunRegisterViewModel : AkunUserViewModel
    {
        [MaxLength(150)]
        [Required]
        public string Password { get; set; }

        [MaxLength(150)]
        [Required]
        public string RePassword { get; set; }


        [MaxLength(350)]
        [Required]
        public string FullName { get; set; }

        [Phone]
        [MaxLength(150)]
        [Required]
        public string PhoneNumber { get; set; }
    }
    public class AkunLoginViewModel : AkunUserViewModel
    {
        public string Password { get; set; }
    }
}
