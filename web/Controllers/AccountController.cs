using core.Models;
using core.Repositories;
using core.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace signal_core.Controllers
{

    public class AccountController : Controller
    {
        private readonly Unitofwork unitofwork;

        public AccountController(Unitofwork unitofwork)
        {
            this.unitofwork = unitofwork;
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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Register(AkunRegisterViewModel akunUser)
        {
            if (ModelState.IsValid)
            {
                unitofwork.AkunUser.Add(new AkunUser
                {
                    FullName = akunUser.FullName,
                    Email = akunUser.Email,
                    Password = akunUser.Password,
                    PhoneNumber=akunUser.PhoneNumber
                });
                unitofwork.Save();
                //return Ok();
               return RedirectToAction("Index", "Home");
            }
            return View(akunUser);
        }


        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AkunLoginViewModel akunUser, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var isvalid = unitofwork.AkunUser.ValidateUserLogin(akunUser);
                if (isvalid)
                {
                    var claims = new List<Claim>() {
                        new Claim("user", akunUser.Email),
                        new Claim("role","user")
                    };
                    var claimIds = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "user", "role");
                    var claimsprincipal = new ClaimsPrincipal(claimIds);
                    await HttpContext.SignInAsync(claimsprincipal);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return Redirect("/");
                    }
                }
                else
                {
                    return View();
                }
            }
            return BadRequest(ModelState);
        }
    }
}