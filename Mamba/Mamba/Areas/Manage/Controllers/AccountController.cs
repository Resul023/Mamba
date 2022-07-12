using Mamba.Areas.Manage.ViewModel;
using Mamba.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mamba.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }
        public async Task<IActionResult> CreateRole()
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            return Ok();
        }
      /*  public async Task<IActionResult> Index()
        {
            AppUser admin = new AppUser
            {
                FullName = "Hikmet",
                UserName = "Hikmet123",
                IsAdmin = true,


            };
            var result = await _userManager.CreateAsync(admin, "Alfa123");

            await _userManager.AddToRoleAsync(admin, "Admin");
            return View();
        }*/
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","Password or Username is not true");
                return View();
            }
            var admin = _userManager.Users.FirstOrDefault(x => x.IsAdmin && x.UserName == user.UserName);
            if (admin == null)
            {
                ModelState.AddModelError("", "Password or Username is not true");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(admin, user.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Password or Username is not true");
                return View();
            }
            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> SignOut( )
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }

    }
}
