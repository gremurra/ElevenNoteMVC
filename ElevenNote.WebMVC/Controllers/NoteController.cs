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
            if (!ModelState.IsValid) return View(model);       //makes sure that the model is valid

            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");       //returns the user back to the index view
            };

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

        //GET: DETAILS
        public ActionResult Details(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        //GET: EDIT
        //Note/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    CategoryId = detail.CategoryId,
                    NoteId = detail.NoteId,
                    Title = detail.Title,
                    Content = detail.Content
                };

            return View(model);
        }

        //POST: EDIT
        //Note/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)        //this is an overloaded Edit ActionResult
        {
            if (!ModelState.IsValid) return View(model);        //if ModelState is not valid, just return the view

            if (model.NoteId != id)                             //if the NoteId doesn't match up
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateNoteService();

            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }

        //GET: DELETE
        //Note/Delete/{id}
        public ActionResult Delete(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        //POST: DELETE
        //Note/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateNoteService();

            service.DeleteNote(id);

            TempData["SaveResult"] = "Your note was deleted.";

            return RedirectToAction("Index");
        }

        private NoteService CreateNoteService()     //Helper Method
        {
            var userId = Guid.Parse(User.Identity.GetUserId());     //grabs the UserId
            var service = new NoteService(userId);
            return service;
        }
    }
}