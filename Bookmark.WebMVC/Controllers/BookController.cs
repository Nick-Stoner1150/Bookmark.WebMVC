using Bookmark.Models.BookModels;
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
    public class BookController : Controller
    {
        private BookService CreateBookService()
        {
            var userId = User.Identity.GetUserId();
            var service = new BookService(userId);
            return service;
        }
        // GET: Books
        public ActionResult Index()
        {
            var service = CreateBookService();
            var model = service.GetBooks();

            return View(model);
        }

        // GET: Create Book
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create Book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateBookService();

            if (service.CreateBook(model))
            {
                TempData["SaveResult"] = "Your book was created!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Book could not be created");

            return View(model);
        }

        // GET: Book By Id
        [HttpGet]
        public ActionResult Details(int id)
        {
            var svc = CreateBookService();
            var model = svc.GetBookById(id);

            return View(model);
        }

        // GET: Edit 
        public ActionResult Edit (int id)
        {
            var service = CreateBookService();
            var detail = service.GetBookById(id);
            var model =
                new BookEdit
                {
                    BookId = detail.BookId,
                    CurrentPage = detail.CurrentPage,
                    BookshelfId = detail.BookshelfId
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.BookId != id)
            {
                ModelState.AddModelError("", "ID Mismatch!");
                return View(model);
            }

            var service = CreateBookService();

            if(service.UpdateBook(model))
            {
                TempData["SaveResult"] = "Your book was updated!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your book could not be updated!");
            return View();
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateBookService();
            var model = svc.GetBookById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateBookService();

            service.DeleteBook(id);

            TempData["SaveResult"] = "Your book was deleted!";

            return RedirectToAction("Index");
        }

        
    }
}