using Bookmark.Models.BookshelfModels;
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
    public class BookshelfController : Controller
    {
        private readonly IBookshelfServices _service;

        public BookshelfController(IBookshelfServices service)
        {
            _service = service;
        }
        // GET: Bookshelves
        public ActionResult Index()
        {
            var model = _service.GetBookshelves(User.Identity.GetUserId());
            return View(model);
        }

        // GET: Create Bookshelf
        public ActionResult Create()
        {
            return View();
        }

        // Post: Create Book
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookshelfCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            model.UserId = User.Identity.GetUserId();

            if (_service.CreateBookshelf(model))
            {
                TempData["SaveResult"] = "Your bookshelf was created!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Bookshelf could not be created! ");

            return View(model);
        }

        // GET: Bookshelf By Id
        public ActionResult Details(int id)
        {
            var model = _service.Get(id, User.Identity.GetUserId());

            return View(model);
        }

        //GET: Edit
        public ActionResult Edit(int id)
        {
            var detail = _service.Get(id, User.Identity.GetUserId());
            var model =
                new BookshelfEdit
                {
                    BookshelfId = detail.BookshelfId,
                    Name = detail.Name,
                    Description = detail.Description
                };

            return View(model);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookshelfEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.BookshelfId != id)
            {
                ModelState.AddModelError("", "The ID Must remain the same!");
                return View(model);
            }

            model.UserId = User.Identity.GetUserId();

            if(_service.UpdateBookshelf(model))
            {
                TempData["SaveResult"] = "Your bookshelf was updated!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your bookshelf could not be updated!");
            return View();
        }

        public ActionResult Delete(int id)
        {
            var model = _service.Get(id, User.Identity.GetUserId());

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {

            _service.DeleteBookshelf(id, User.Identity.GetUserId());

            TempData["SaveResult"] = "Your bookshelf was deleted";

            return RedirectToAction("Index");
        }


    }
}