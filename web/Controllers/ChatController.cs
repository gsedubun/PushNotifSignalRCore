using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Models;
using core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace signal_core.Controllers
{
    [Authorize]
    public class ChatController : BaseController
    {
        public ChatController(Unitofwork unitofwork) : base(unitofwork)
        {
        }

        public IActionResult Index()
        {
            return View();
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

    }
}