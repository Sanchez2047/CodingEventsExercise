﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.ViewModels
{
    public class AddEventCategoryViewModel
    {
        [Required(ErrorMessage = ">> Category Name Required <<")]
        [StringLength(20, MinimumLength = 3, ErrorMessage =">> Category Name must be between 3 and 20 characters long <<")]
        public string Name { get; set; }
    }
}
