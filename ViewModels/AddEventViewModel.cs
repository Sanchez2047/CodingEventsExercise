using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.ViewModels
{
    public class AddEventViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description for your event.")]
        [StringLength(500, ErrorMessage = "Description is too long!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a location for your event.")]
        public string Location { get; set; }

        [Range(0, 100000, ErrorMessage = "Please enter a number between 0 and 100,000")]
        public int NumOfAttendees { get; set; }

        [EmailAddress(ErrorMessage = "...Do you know what an email is?")]
        public string ContactEmail { get; set; }

        public static bool IsTrue { get { return true; } }
        [Compare("IsTrue", ErrorMessage = "Registration required")]
        public bool RegisterRequired { get; set; }
    }
}
