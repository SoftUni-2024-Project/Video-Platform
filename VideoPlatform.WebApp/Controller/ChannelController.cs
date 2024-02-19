using Microsoft.AspNetCore.Mvc;
using static VideoPlatform.WebApp.Model.User.ChannelRequestModel;
using VideoPlatform.WebApp.Model.User;
using VideoPlatform.WebApp.Repos;

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
        public ActionResult Create()
        {
            return View();
        }

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

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("editChannel")]
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
