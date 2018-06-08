using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.Models
{
    public class AkunUser : BaseEntity
    {
        [MaxLength(350)]
        public string FullName { get; set; }
        [MaxLength(150)]
        public string Password { get; set; }

        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }

        [Phone]
        [MaxLength(150)]
        public string PhoneNumber { get; set; }
    }

}
