using core.Models;
using core.Repositories;
using core.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace signal_core.Controllers
{

    public class BaseController : Controller
    {
        protected readonly Unitofwork unitofwork;
        public BaseController(Unitofwork unitofwork)
        {
            this.unitofwork = unitofwork;

        }
    }
    public class AccountController : BaseController
    {
        public AccountController(Unitofwork unitofwork) : base(unitofwork)
        {
        }

        [Route("token")]
        [HttpPost]
        public async Task<IActionResult> TokenAsync([FromBody] AkunLoginViewModel inputModel)
        {

            if (ModelState.IsValid)
            {
                var valid = unitofwork.AkunUser.ValidateUserLogin(inputModel);
                if (!valid)
                    return Unauthorized();


                var claims = new List<Claim>(){
                    new Claim(ClaimTypes.NameIdentifier, inputModel.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, inputModel.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                var cls = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(cls);
                await HttpContext.SignInAsync(claimsPrincipal);


                var credentials = new SigningCredentials(JwtSecurityKey.Create(),
                 SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(JwtSecurityKey.Issuer,
                 JwtSecurityKey.Audience,
                  claimsPrincipal.Claims, DateTime.Now, DateTime.Now.AddDays(1),
                  credentials);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return BadRequest(inputModel);
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
                    PhoneNumber = akunUser.PhoneNumber
                });
                unitofwork.Save();
                //return Ok();
                return RedirectToAction("Index", "Home");
            }
            return View(akunUser);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }


        [HttpGet]
        public IActionResult Login()
        {
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