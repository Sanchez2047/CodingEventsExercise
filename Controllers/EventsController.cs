using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
        private EventDbContext _context;

        public EventsController(EventDbContext dbContext)
        {
            this._context = dbContext;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Event> events = _context.Events.ToList();
            return View(events);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddEventViewModel addEventViewModel = new AddEventViewModel();
            return View(addEventViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    Location = addEventViewModel.Location,
                    NumOfAttendees = addEventViewModel.NumOfAttendees,
                    ContactEmail = addEventViewModel.ContactEmail,
                    Type = addEventViewModel.Type,
                    RegisterRequired = addEventViewModel.RegisterRequired
                };

                _context.Events.Add(newEvent);

                _context.SaveChanges();

                return Redirect("/Events");
            }
            return View(addEventViewModel);

        }
        public IActionResult Delete()
        {
            //ViewBag.events = EventData.GetAll();
            //List<Event> events = new List<Event>(EventData.GetAll());

            List<Event> events = _context.Events.ToList();

            return View(events);
        }
        [HttpPost]
        public IActionResult Delete(int [] eventIds)
        {
            foreach(int eventId in eventIds)
            {

                Event theEvent = _context.Events.Find(eventId);

                _context.Events.Remove(theEvent);
            }

            _context.SaveChanges();

            return Redirect("/Events");
        }

        [HttpGet]
        [Route("/Events/Edit/{eventId?}")]
        public IActionResult Edit(int eventId)
        {
            //ViewBag.eventToEdit = EventData.GetById(eventId);
            ViewBag.eventToEdit = _context.Events.Find(eventId);
            ViewBag.Title = $"Edit Event \"{ViewBag.eventToEdit.Name}\" (id = \"{ViewBag.eventToEdit.Id}\")";
            return View();
        }
        [HttpPost("/Events/Edit")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string description, string Location, int NumOfAttendees, string ContactEmail, bool RegisterRequired)
        {
            Event eventBeingEditited = _context.Events.Find(eventId);
            eventBeingEditited.Name = name;
            eventBeingEditited.Description = description;
            eventBeingEditited.Location = Location;
            eventBeingEditited.NumOfAttendees = NumOfAttendees;
            eventBeingEditited.ContactEmail = ContactEmail;
            eventBeingEditited.RegisterRequired = RegisterRequired;

            _context.SaveChanges();


            return Redirect("/Events");
        }
    }
}
