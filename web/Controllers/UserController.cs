using System.Linq;
using core.Models;
using core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace signal_core.Controllers

{
    public class UserController : BaseController
    {
        public UserController(Unitofwork unitofwork) : base(unitofwork)
        {
        }

        [Route("getuser")]
        public IActionResult GetUser()
        {
            return Ok(unitofwork.AkunUser.All().Select(d => new AkunUser
            {
                FullName = d.FullName,
                Email = d.Email
            }));
        }

        [Route("searchuser")]
        [HttpPost]
        public IActionResult SearchUser([FromBody] string email)
        {
            return Ok(unitofwork.AkunUser.Search(email));
        }
    }
}