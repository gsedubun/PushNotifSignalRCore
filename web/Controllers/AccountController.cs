using core.Models;
using core.Repositories;
using core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace signal_core.Controllers{

    [Route("api/[controller]")]
    public class AccountController : Controller{
        private readonly Unitofwork unitofwork;

        public AccountController(Unitofwork unitofwork)
        {
            this.unitofwork = unitofwork;
        }

        [Route("getuser")]
        public IActionResult GetUser()
        {
            return Ok(unitofwork.AkunUser.All());
        }
        [Route("adduser")]
        [HttpPost]
        public IActionResult AddUser( AkunUserViewModel akunUser)
        {
            if (ModelState.IsValid)
            {
                unitofwork.AkunUser.Add(new AkunUser
                {
                    FullName = akunUser.FullName,
                    Email = akunUser.Email,
                    Password = akunUser.Password
                });
                unitofwork.Save();
                return Ok();
            }
            return BadRequest(ModelState);
        }

    }
}