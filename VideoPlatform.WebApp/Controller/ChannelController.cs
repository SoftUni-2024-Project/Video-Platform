using Microsoft.AspNetCore.Mvc;
using static VideoPlatform.WebApp.Model.User.ChannelRequestModel;
using VideoPlatform.WebApp.Model.User;
using VideoPlatform.WebApp.Repos;
using Microsoft.AspNetCore.Identity;

namespace VideoPlatform.WebApp.Controler
{
    public class ChannelController : Controller
    {
        private readonly IChannelService _channelService;
        public ChannelController(IChannelService channelService)
        {
            _channelService = channelService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

       /* [HttpPost]
        public async Task<IActionResult> Login(LogInRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // Redirect to dashboard or channel page upon successful login
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }*/


        [HttpPost]
        [Route("register")]
        public ActionResult Create(CreateChannelRequestModel request)
        {
            if (ModelState.IsValid)
            {
                ChannelResponseModel createdChannel = _channelService.CreateChannel(request);
            }
            return View(request);
        }

        [HttpPost]
        [Route("editProfile")]
        public ActionResult Edit(EditChannelRequestModel request)
        {
            if (ModelState.IsValid)
            {
                ChannelResponseModel updatedChannel = _channelService.EditChannel(request);
                return RedirectToAction("Details", new { id = updatedChannel.ChannelId });
            }
            return View(request);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            ChannelResponseModel channel = _channelService.GetChannelById(id);
            if (channel == null)
            {
                return HttpNotFound();
            }
            return View(channel);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ChannelResponseModel channel = _channelService.GetChannelById(id);
            if (channel == null)
            {
                return HttpNotFound();
            }
            return View(channel);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
