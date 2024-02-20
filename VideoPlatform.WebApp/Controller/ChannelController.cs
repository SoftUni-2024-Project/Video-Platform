using Microsoft.AspNetCore.Mvc;
using static VideoPlatform.WebApp.Model.User.ChannelRequestModel;
using VideoPlatform.WebApp.Model.User;
using VideoPlatform.WebApp.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace VideoPlatform.WebApp.Controler
{
    public class ChannelController : ControllerBase
    {
        private readonly IChannelService _channelService;
        public ChannelController(IChannelService channelService)
        {
            _channelService = channelService;
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

        [HttpPost()]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Create(CreateChannelRequestModel request)
        {
            if (ModelState.IsValid)
            {
                ChannelResponseModel createdChannel = _channelService.CreateChannel(request);
            }
            return Ok(request);
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
            return Ok(request);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            ChannelResponseModel channel = _channelService.GetChannelById(id);
            if (channel == null)
            {
                return HttpNotFound();
            }
            return Ok(channel);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("editChannel")]
        public ActionResult Edit(int id)
        {
            ChannelResponseModel channel = _channelService.GetChannelById(id);
            if (channel == null)
            {
                return HttpNotFound();
            }
            return Ok(channel);
        }

        public IActionResult Index()
        {
            return Ok();
        }
    }
}
