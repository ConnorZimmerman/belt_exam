using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace belt_exam.Models
{
    public class attendees { 
        public int id {get; set;}
        public int userId {get; set;}
        public users user {get; set;}
        public int activityId {get; set;}
        public activities activity {get; set;}
    }
}