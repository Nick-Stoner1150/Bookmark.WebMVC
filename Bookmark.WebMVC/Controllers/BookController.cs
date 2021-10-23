using Bookmark.Models.BookModels;
using Bookmark.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookmark.WebMVC.Controllers
{
    public class BookController : Controller
    {
        private BookService CreateBookService()
        {
            var service = new BookService();
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

            ModelState.AddModelError("", "Note could not be created");

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
    }
}