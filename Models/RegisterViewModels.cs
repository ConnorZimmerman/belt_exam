using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace belt_exam.Models
{
    public class RegisterViewModels
    {
        [Required]
        [Display(Name = "First Name: ")]
        [StringLength(2)]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name: ")]
        [StringLength(2)]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Email: ")]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [PasswordValidation(ErrorMessage = "The Password must have at least one numeric and one special character and be at least 8 characters long.")]
        public string psw { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("psw")]
        public string conPassword { get; set; }
    }
}