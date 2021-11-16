using Bookmark.Data;
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
    [Authorize]
    public class NoteController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private readonly INoteServices _service;

        public NoteController(INoteServices service)
        {
            _service = service;
        }

        // GET: Note
        public ActionResult Index()
        {
            var model = _service.GetNotes(User.Identity.GetUserId());

            return View(model);
        }

        // GET: Create Note
        public ActionResult Create()
        {
            ViewData["Books"] = _db.Books.Select(book => new SelectListItem
            {
                Text = book.Title,
                Value = book.BookId.ToString()
            });

            return View();
        }

        // POST: Create Book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            model.UserId = User.Identity.GetUserId();

            if (_service.CreateNote(model))
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
            var model = _service.GetNoteById(id, User.Identity.GetUserId());

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var detail = _service.GetNoteById(id, User.Identity.GetUserId());
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

            model.UserId = User.Identity.GetUserId();

            if (_service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your note was updated!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated!");
            return View();
        }

        public ActionResult Delete(int id)
        {
            var model = _service.GetNoteById(id, User.Identity.GetUserId());

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {

            _service.DeleteNote(id, User.Identity.GetUserId());

            TempData["SaveResult"] = "Your note was deleted!";

            return RedirectToAction("Index");
        }
    }
}