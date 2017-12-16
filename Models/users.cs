using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace belt_exam.Models
{
    public class users{
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string psw { get; set; }
        public List<attendees> attendees { get; set; }
        public users()
        {
            attendees = new List<attendees>();
        }
    }
}