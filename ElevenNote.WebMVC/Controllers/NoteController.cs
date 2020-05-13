using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index()     //Displays all the notes for the current user (based on the methods and services that it calls upon)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            var model = service.GetNotes();

            return View(model);
        }

        //GET: CREATE
        public ActionResult Create()        //This method: making a request to get the Create View.
        {
            return View();
        }

        //POST: CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid)        //makes sure that the model is valid
            {
                return View(model);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());     //grabs the UserId
            var service = new NoteService(userId);

            service.CreateNote(model);      //calls on CreateNote (in service layer)

            return RedirectToAction("Index");       //returns the user back to the index view
        }
    }
}