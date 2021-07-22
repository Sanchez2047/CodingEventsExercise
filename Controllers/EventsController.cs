﻿using CodingEvents.Data;
using CodingEvents.Models;
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
            ViewBag.events = EventData.GetAll(); ;
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("/Events/Add")]
        public IActionResult NewEvent(Event newEvent)
        {
            EventData.Add(newEvent);
            return Redirect("/Events");
        }
        public IActionResult Delete()
        {
            ViewBag.events = EventData.GetAll();
            return View();
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
            ViewBag.Title = "Edit Event NAME (id=ID)";
            return View();
        }
        [HttpPost("/Events/Edit")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string description)
        {
            Event eventBeingEditited = EventData.GetById(eventId);
            eventBeingEditited.Name = name;
            eventBeingEditited.Description = description;
            return Redirect("/Events");
        }
    }
}
