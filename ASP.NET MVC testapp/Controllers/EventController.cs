using ASP.NET_MVC_testapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ASP.NET_MVC_testapp.Controllers
{
    public class EventController : Controller
    {
        private readonly MyDbContext _context;
        private readonly ILogger _logger;

        public EventController(MyDbContext context, ILogger<EventController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult EventPage(int? id)
        {
            var _event = _context.Events.Find(id);
            if( _event == null )
                return NotFound();
            return View(_event);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (viewModel.image_data != null && viewModel.image_data.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await viewModel.image_data.CopyToAsync(memoryStream);
                        imageData = memoryStream.ToArray();
                    }
                }

                var _event = new Event
                {
                    image_data = imageData,
                    ShortDescription = viewModel.ShortDescription,
                    LongDescription = viewModel.LongDescription,
                    Title = viewModel.Title,
                    DateAndTime = viewModel.DateAndTime,
                    Address = viewModel.Address,
                    City = viewModel.City,
                    Organizer_email = viewModel.Organizer_email,
                    Organizer_name  = viewModel.Organizer_name
                };

                _context.Events.Add(_event);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View("~/Views/Home/Index.cshtml", viewModel);
        }

        public IActionResult CreateEvent()
        {
            return View();
        }
        public IActionResult AddToEventVisitors(int EventId)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(currentUserId) && EventId!=null)
            {
                var temp = _context.eventVisitors.Where(b => b.User_id == currentUserId && b.Event_id == EventId).FirstOrDefault();
                if (temp == null)
                {
                    EventVisitor visitor = new EventVisitor()
                    {
                        User_id = currentUserId,
                        Event_id = EventId
                    };
                    _context.eventVisitors.Add(visitor);
                    _context.SaveChanges();
                    return RedirectToRoute(new { action = "EventPage", controller = "Event", id = EventId });
                }
                return RedirectToRoute(new { action = "EventPage", controller = "Event", id = EventId });
            }
            else 
            return NotFound();
        }
    }
}
