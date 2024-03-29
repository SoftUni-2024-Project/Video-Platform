﻿using Microsoft.AspNetCore.Mvc;
using VideoPlatform.WebApp.Model.User;
using VideoPlatform.WebApp.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using VideoPlatform.WebApp.Data.Entities;
using VideoPlatform.WebApp.Data;
using VideoPlatform.WebApp.Services;


namespace VideoPlatform.WebApp.Controllers
{
    public class ChannelController : Controller
    {
        private readonly UserManager<Channel> userManager;
        private readonly SignInManager<Channel> signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IChannelService _channelService;


        public ChannelController(
            UserManager<Channel> _userManager,
            SignInManager<Channel> _signInManager,
            ApplicationDbContext context,
            IEmailService emailService, IChannelService channelService)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            _context = context;
            _emailService = emailService;
            _channelService = channelService;
        }

        [HttpGet("Register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new Channel()
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
                _emailService.SendEmail(message);

                ViewData["Email"] = user.Email;
                return View("EmailVerification");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }


            return View(model);
        }

        [HttpPost()]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LogInRequestModel model)
        {
            var user = _channelService.GetChannelByUsername(model.Username);
            if (user == null)
            {
                return Unauthorized();
            }
            if (model.Password != user.Password)
            {
                return Unauthorized();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, "login");
            var principal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(principal);

            return Ok(new { message = "Login successful" });
        }

        [HttpPost]
        [Route("editProfile")]
        public ActionResult Edit(EditChannelRequestModel request)
        {
            if (ModelState.IsValid)
            {
                EditChannelResponceModel updatedChannel = _channelService.EditChannel(request);
                return RedirectToAction("Details", new { id = updatedChannel.ChannelId });
            }
            return View(request);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
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

