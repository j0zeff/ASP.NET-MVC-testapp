using ASP.NET_MVC_testapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (_event == null)
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
                    Organizer_name = viewModel.Organizer_name
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
            if (!string.IsNullOrEmpty(currentUserId) && EventId != null)
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
        [HttpGet]
        public async Task<IActionResult> EditEvent(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }
            var _event = _context.Events.Find(id);

            if (_event == null)
            {
                return NotFound();
            }
            EventViewModel eventViewModel = new EventViewModel
            {
                Id = _event.Id,
                Title = _event.Title,
                ShortDescription = _event.ShortDescription,
                LongDescription = _event.LongDescription,
                DateAndTime = _event.DateAndTime,
                City = _event.City,
                Address = _event.Address,
                Organizer_email = _event.Organizer_email,
                Organizer_name = _event.Organizer_name
            };

            return View(eventViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEvent(int id, EventViewModel eventViewModel)
        {
            if (id != eventViewModel.Id)
            {
                return NotFound();
            }
            var _event = _context.Events.Find(id);
            if (_event == null) { 
                return NotFound();
            }
            _event.ShortDescription = eventViewModel.ShortDescription;
            _event.LongDescription = eventViewModel.LongDescription;
            _event.Title = eventViewModel.Title;
            _event.DateAndTime = eventViewModel.DateAndTime;
            _event.Address = eventViewModel.Address;
            _event.City = eventViewModel.City;
            _event.Organizer_email = eventViewModel.Organizer_email;
            _event.Organizer_name = eventViewModel.Organizer_name;
            if (eventViewModel.image_data != null && eventViewModel.image_data.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await eventViewModel.image_data.CopyToAsync(memoryStream);
                    _event.image_data = memoryStream.ToArray();
                }
            }
            try
            {
                //_context.Update(_event);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(_event.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Home");
        }
        private bool EventExists(int id)
        {
            return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
