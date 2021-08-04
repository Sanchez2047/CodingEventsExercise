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
                };

                _context.Events.Add(newEvent);

                _context.SaveChanges();

                return Redirect("/Events");
            }
            return View(addEventViewModel);

        }
        public IActionResult Delete()
        {


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
            Event eventToEdit = _context.Events.Find(eventId);
            AddEventViewModel addEventViewModel = new AddEventViewModel
            {
                Name = eventToEdit.Name,
                Description = eventToEdit.Description,
                Location = eventToEdit.Location,
                NumOfAttendees = eventToEdit.NumOfAttendees,
                ContactEmail = eventToEdit.ContactEmail,
                Type = eventToEdit.Type,
                Id = eventToEdit.Id
            };

            ViewBag.Title = $"Edit Event \"{addEventViewModel.Name}\" (id = \"{addEventViewModel.Id}\")";
            return View(addEventViewModel);
        }
        [HttpPost]
        public IActionResult Edit(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                Event eventBeingEditited = _context.Events.Find(addEventViewModel.Id);
                eventBeingEditited.Name = addEventViewModel.Name;
                eventBeingEditited.Description = addEventViewModel.Description;
                eventBeingEditited.Location = addEventViewModel.Location;
                eventBeingEditited.NumOfAttendees = addEventViewModel.NumOfAttendees;
                eventBeingEditited.ContactEmail = addEventViewModel.ContactEmail;
                eventBeingEditited.Type = addEventViewModel.Type;
                _context.SaveChanges();
                return Redirect("/Events");
            }
            return View(addEventViewModel);


        }
    }
}
