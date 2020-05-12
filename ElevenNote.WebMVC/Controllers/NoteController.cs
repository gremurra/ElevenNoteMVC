using ElevenNote.Models;
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
        public ActionResult Index()
        {
            var model = new NoteListItem[0];    //instantiating a new instance of the NoteListItem as an IEnumerable with the [0] syntax
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
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}