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

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Event> events = new List<Event>(EventData.GetAll()); 
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
                    RegisterRequired = addEventViewModel.RegisterRequired
                };
                EventData.Add(newEvent);
                return Redirect("/Events");
            }
            return View(addEventViewModel);

        }
        public IActionResult Delete()
        {
            //ViewBag.events = EventData.GetAll();
            List<Event> events = new List<Event>(EventData.GetAll());

            return View(events);
        }
        [HttpPost]
        public IActionResult Delete(int [] eventIds)
        {
            foreach(int eventId in eventIds)
            {
                EventData.Remove(eventId);
            }

            return Redirect("/Events");
        }

        [HttpGet]
        [Route("/Events/Edit/{eventId?}")]
        public IActionResult Edit(int eventId)
        {
            ViewBag.eventToEdit = EventData.GetById(eventId);
            ViewBag.Title = $"Edit Event \"{ViewBag.eventToEdit.Name}\" (id = \"{ViewBag.eventToEdit.Id}\")";
            return View();
        }
        [HttpPost("/Events/Edit")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string description, string Location, int NumOfAttendees, string ContactEmail, bool RegisterRequired)
        {
            Event eventBeingEditited = EventData.GetById(eventId);
            eventBeingEditited.Name = name;
            eventBeingEditited.Description = description;
            eventBeingEditited.Location = Location;
            eventBeingEditited.NumOfAttendees = NumOfAttendees;
            eventBeingEditited.ContactEmail = ContactEmail;
            eventBeingEditited.RegisterRequired = RegisterRequired;

            return Redirect("/Events");
        }
    }
}
