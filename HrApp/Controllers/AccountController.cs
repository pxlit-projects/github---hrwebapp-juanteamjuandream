using HrApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
// ReSharper disable All

namespace HrApp.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signinManager;
        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
        }
        
        public IActionResult Login()
        {
            return View();
        }
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerData)
        {
            return View();
        }
        #endregion
        public IActionResult Logout()
        {
            return View();
        }
        
        
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerData)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser { UserName = registerData.UserName, Email = registerData.Email };
                var result = await _userManager.CreateAsync(identityUser, registerData.Password);
                if (result.Succeeded)
                {
                    return View("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(registerData);
                }
            }
            return View(registerData);
        }

        
        [HttpGet]
        public IActionResult LoginWithUserName()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginWithUserNameAsync(LoginUsernameViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(login.UserName);
                if (user != null)
                {
                    var result = await _signinManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
            }
            return View(login);
        }
        [HttpGet]
        public IActionResult LoginWithEmail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginWithEmailAsync(LoginEmailViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user != null)
                {
                    var result = await _signinManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
            }
            return View(login);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
