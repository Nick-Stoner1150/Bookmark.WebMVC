using Bookmark.Models.NoteModels;
using Bookmark.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookmark.WebMVC.Controllers
{
    public class NoteController : Controller
    {
        private NoteServices CreateNoteSerivce()
        {
            var userId = User.Identity.GetUserId();
            var service = new NoteServices(userId);
            return service;
        }

        // GET: Note
        public ActionResult Index()
        {
            var service = CreateNoteSerivce();
            var model = service.GetNotes();

            return View(model);
        }

        // GET: Create Note
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create Book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateNoteSerivce();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);

        }

        // GET: Note by ID
        [HttpGet]
        public ActionResult Details(int id)
        {
            var svc = CreateNoteSerivce();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateNoteSerivce();
            var detail = service.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    NoteId = detail.NoteId,
                    NoteTitle = detail.NoteTitle,
                    Text = detail.Text
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.NoteId != id)
            {
                ModelState.AddModelError("", "ID Mismatch!");
                return View(model);
            }

            var service = CreateNoteSerivce();

            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your note was updated!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated!");
            return View();
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateNoteSerivce();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateNoteSerivce();

            service.DeleteNote(id);

            TempData["SaveResult"] = "Your note was deleted!";

            return RedirectToAction("Index");
        }
    }
}