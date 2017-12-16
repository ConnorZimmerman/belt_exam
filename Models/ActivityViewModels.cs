using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace belt_exam.Models
{
    public class ActivityViewModels
    {
        [Required]
        [Display(Name = "Title: ")]
        [MinLength(2)]
        public string name { get; set; }

        [Required]
        [Display(Name = "Date ")]
        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Date must be a future time!")]
        public DateTime date {get; set;}

        [Required]
        [Display(Name = "Time: ")]
        public string timeOfEvent { get; set; }

        [Required]
        [Display(Name = "Duration: ")]
        public int timeDurInt { get; set; }

        [Required]
        public string timeType { get; set; }
        
        [Required]
        [Display(Name = "Description: ")]
        [MinLength(10)]
        public string description { get; set; }

        public int userId {get; set;}
    }
}