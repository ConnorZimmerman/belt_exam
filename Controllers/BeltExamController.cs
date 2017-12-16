using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using belt_exam.Models;

namespace belt_exam.Controllers
{
    public class BeltExamController : Controller
    {
        private belt_examContext _context;

        public BeltExamController(belt_examContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Home")]
        public IActionResult Dashboard()
        {
            int? userId = HttpContext.Session.GetInt32("userId"); //checks if user is in session
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            users user = GrabUser();
            List<activities> allActivities = _context.activities.Include(a => a.user).OrderBy(a => a.date).Include(a => a.attendees).ToList();
            List<activities> allCurrentActivities = new List<activities>();
            for (int i = 0; i < allActivities.Count; i++) //checks if activity has passed by creating a new list and adding all future activiites
            {
                if(allActivities[i].date > DateTime.Now){
                    allCurrentActivities.Add(allActivities[i]);
                }
            }
            ViewBag.activities = allCurrentActivities;
            ViewBag.user = user;
            return View("Dash");
        }

        [HttpGet]
        [Route("New")]
        public IActionResult CreateActivityPage()
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            users user = GrabUser();
            ViewBag.userId = user.id;
            return View("Create");
        }

        [HttpPost]
        [Route("CreateActivity")]
        public IActionResult CreateActivity(ActivityViewModels newActivity)
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                activities activity = new activities
                {
                    name = newActivity.name,
                    date = newActivity.date,
                    timeOfEvent = newActivity.timeOfEvent,
                    timeDurInt = newActivity.timeDurInt,
                    timeType = newActivity.timeType,
                    description = newActivity.description,
                    userId = newActivity.userId
                };
                _context.activities.Add(activity);
                _context.SaveChanges();
                int activityId = activity.id;
                return Redirect($"activity/{activityId}");
            }
            return View("Create");
        }

        [HttpGet]
        [Route("activity/{activityId}")]
        public IActionResult ActivityProfile(int activityId)
        {
            int? userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            activities activity = _context.activities.Where(a => a.id == activityId).Include(a => a.user).Include(a => a.attendees).ThenInclude(at => at.user).Single();
            ViewBag.activity = activity;
            users user = GrabUser();
            ViewBag.userId = user.id;
            return View("ActivityProfile");
        }

        [HttpGet]
        [Route("add_attendee/{activityId}")]
        public IActionResult AddAttendee(int activityId)
        {
            int? userId = HttpContext.Session.GetInt32("userId"); //checks if user is in session
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            users user = GrabUser();
            attendees newAttendee = new attendees
            {
                userId = user.id,
                activityId = activityId
            };
            _context.attendees.Add(newAttendee);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("remove_attendee/{attendeeId}")]
        public IActionResult RemoveAttendee(int attendeeId)
        {
            int? num = HttpContext.Session.GetInt32("userId");
            if (num == null)
            {
                return RedirectToAction("Index", "Home");
            }
            users user = GrabUser();
            attendees attendeeInst = _context.attendees.Where(a => a.id == attendeeId).Single();
            _context.attendees.Remove(attendeeInst);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("delete_activity/{activityId}")]
        public IActionResult DeleteActivity(int activityId)
        {
            int? num = HttpContext.Session.GetInt32("userId");
            if (num == null)
            {
                return RedirectToAction("Index", "Home");
            }
            users user = GrabUser();
            activities activity = _context.activities.Where(a => a.id == activityId).Single();
            if (user.id != activity.userId) //Makes sure the creator is the only who can delete the event
            {
                return RedirectToAction("Index", "Home");
            }
            _context.activities.Remove(activity);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        public users GrabUser() //grabs user from session
        {
            int? num = HttpContext.Session.GetInt32("userId");
            int userId = (int)num;
            users user = _context.users.Where(u => u.id == userId).SingleOrDefault();
            return user;
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}