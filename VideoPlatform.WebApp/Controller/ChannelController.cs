using Microsoft.AspNetCore.Mvc;
using static VideoPlatform.WebApp.Model.User.ChannelRequestModel;
using VideoPlatform.WebApp.Model.User;
using VideoPlatform.WebApp.Service;

namespace VideoPlatform.WebApp.Controler
{
    public class ChannelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IChannelService _channelService;

        public ChannelController(IChannelService channelService)
        {
            _channelService = channelService;
        }

        // GET: Channel/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Channel/Create
        [HttpPost]
        public ActionResult Create(CreateChannelRequestModel request)
        {
            if (ModelState.IsValid)
            {
                ChannelResponseModel createdChannel = _channelService.CreateChannel(request);
            }
            return View(request);
        }

        // GET: Channel/Edit/{id}
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

        // POST: Channel/Edit/{id}
        [HttpPost]
        public ActionResult Edit(EditChannelRequestModel request)
        {
            if (ModelState.IsValid)
            {
                ChannelResponseModel updatedChannel = _channelService.EditChannel(request);
                return RedirectToAction("Details", new { id = updatedChannel.ChannelId });
            }
            return View(request);
        }

        // GET: Channel/Details/{id}
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
    }
}
