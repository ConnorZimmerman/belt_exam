using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace belt_exam.Models
{
    public class activities { 
        public int id {get; set;}
        public string name {get; set;}
        public DateTime date {get; set;}
        public string timeOfEvent {get; set;}
        public int timeDurInt {get; set;}
        public string timeType {get; set;}
        public string description {get; set;}
        public int userId {get; set;}
        public users user {get; set;}
        public List<attendees> attendees { get; set; }
        public activities()
        {
            attendees = new List<attendees>();
        }
    }
}