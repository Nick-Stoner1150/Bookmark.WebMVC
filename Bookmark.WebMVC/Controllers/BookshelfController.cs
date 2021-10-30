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
        private BookshelfServices CreateBookshelfService()
        {
            var userId = User.Identity.GetUserId();
            var service = new BookshelfServices(userId);

            return service;
        }
        // GET: Bookshelves
        public ActionResult Index()
        {
            var service = CreateBookshelfService();
            var model = service.GetBookshelves();

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

            var service = CreateBookshelfService();

            if (service.CreateBookshelf(model))
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
            var svc = CreateBookshelfService();
            var model = svc.Get(id);

            return View(model);
        }

        //GET: Edit
        public ActionResult Edit(int id)
        {
            var service = CreateBookshelfService();
            var detail = service.Get(id);
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
                ModelState.AddModelError("", "ID Mismatch!");
                return View(model);
            }

            var service = CreateBookshelfService();

            if(service.UpdateBookshelf(model))
            {
                TempData["SaveResult"] = "Your bookshelf was updated!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your bookshelf could not be updated!");
            return View();
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateBookshelfService();
            var model = svc.Get(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateBookshelfService();

            service.DeleteBookshelf(id);

            TempData["SaveResult"] = "Your bookshelf was deleted";

            return RedirectToAction("Index");
        }


    }
}