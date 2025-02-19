﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Mission12.Models;

namespace Mission12.Controllers
{
    public class HomeController : Controller
    {

        private IGroupRepository repo;
        private IAppointmentRepository apprepo;
       

        public HomeController(IGroupRepository temp, IAppointmentRepository temp2)
        {
            repo = temp;
            apprepo = temp2;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Form(Group g)
        {
            repo.CreateGroup(g);

            return RedirectToAction("SignUp");
        }
        [HttpGet]
        public IActionResult ViewAppointments(Appointment a)
        {
            var appointments = apprepo.Appointments;
            
            return View(appointments);
        }

        [HttpGet]
        public IActionResult Edit(int appointmentId)
        {
            ViewBag.Cat = apprepo.Appointments.ToList();
            var appt = apprepo.Appointments.Single(x => x.AppointmentId == appointmentId);
            return View("AddAppt", appt);
        }

        [HttpPost]
        public IActionResult Edit(Appointment a)
        {
        
            apprepo.SaveAppointment(a);
            return RedirectToAction("ViewAppointments");
        }

        [HttpGet]
        public IActionResult SignUp(Appointment a)
        {
            var appointments = apprepo.Appointments;

            {
                appointments = apprepo.Appointments
                .Where(a => a.Booked == false);

            };
            return View(appointments);
        }
        [HttpGet]
        public IActionResult Delete(int appointmentId)
        {
            Appointment appt = apprepo.Appointments.Single(x => x.AppointmentId == appointmentId);
                apprepo.DeleteAppointment(appt);
            return RedirectToAction("ViewAppointment");
        }

    }
}
