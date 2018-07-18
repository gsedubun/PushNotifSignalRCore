using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        

    }
}