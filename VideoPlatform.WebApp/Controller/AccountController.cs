using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.WebApp.Data;
using VideoPlatform.WebApp.Model;
using VideoPlatform.WebApp.Model.AccountModel;
using VideoPlatform.WebApp.Data.Entities;
using VideoPlatform.WebApp.Model.User;
using VideoPlatform.WebApp.Services;

namespace VideoPlatform.WebApp.Controller
{
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ApplicationDbContext context;
        private readonly IEmailService emailService;
        public AccountController(
            UserManager<IdentityUser> _userManager,
            SignInManager<IdentityUser> _signInManager,
            ApplicationDbContext context,
            IEmailService emailService)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            this.context = context;
            this.emailService = emailService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser()
            {
                Email = model.Email,
                EmailConfirmed = true,
                UserName = model.Email,
            };
            var result = await userManager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);


                
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(EmailVerification), "Account", new { token, email = user.Email }, Request.Scheme);
                var message = new Message((new string[] { user.Email! }).ToList(), "Confirmation email link", confirmationLink!);
                emailService.SendEmail(message);
             
                ViewData["Email"] = user.Email;
                return View("EmailVerification");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }


            return View(model);
        }

       
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult TestEmail()
        {
            this.emailService.SendEmail(
                new Message((new string[] { "kal04an@gmail.com" }).ToList(),
                "Testing Email Service",
                "Test Email"
                 ));

            return Redirect("/");
        }

        [HttpGet("EmailVerification")]
        public async Task<IActionResult> EmailVerification(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {

                    
                    return RedirectToAction("Index", "Home");
                }
            }
           
            return RedirectToAction("Index", "Home");
        }




    }
}
