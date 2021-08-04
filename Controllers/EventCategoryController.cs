using CodingEvents.Data;
using CodingEvents.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.ViewModels;



namespace CodingEvents.Controllers
{
    public class EventCategoryController : Controller
    {
        private EventDbContext _context;

        public EventCategoryController(EventDbContext dbContext)
        {
            this._context = dbContext;
        }

        public IActionResult Index()
        {
            List<EventCategory> eventCategories = _context.EventCategories.ToList();
            return View(eventCategories);
        }

        public IActionResult Create()
        {
            AddEventCategoryViewModel addEventCategoryViewModel = new AddEventCategoryViewModel();
            return View(addEventCategoryViewModel);
        }
        [HttpPost("Create")]
        public IActionResult ProcessCreateEventCategoryForm(AddEventCategoryViewModel addEventCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                EventCategory eventCategory = new EventCategory(addEventCategoryViewModel.Name);
                _context.EventCategories.Add(eventCategory);
                _context.SaveChanges();
                return Redirect("EventCategory/Index");
            }
            return View("Create",addEventCategoryViewModel);
        }
    }
}
