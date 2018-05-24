using System;
using System.Collections.Generic;
using System.Text;

namespace core.ViewModels
{
    public class AkunUserViewModel
    {
        public string Email { get; set; }
    }

    public class AkunRegisterViewModel : AkunUserViewModel
    {
        public string Password { get; set; }
        public string FullName { get; set; }
    }
    public class AkunLoginViewModel : AkunUserViewModel
    {
        public string Password { get; set; }
    }
}
