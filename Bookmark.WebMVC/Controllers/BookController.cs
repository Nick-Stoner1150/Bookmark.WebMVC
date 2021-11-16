using Bookmark.Data;
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
        private ApplicationDbContext _db = new ApplicationDbContext();
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        // GET: Books
        public ActionResult Index()
        {
            var model = _service.GetBooks(User.Identity.GetUserId());
            return View(model);
        }

        // GET: Create Book
        public ActionResult Create()
        {
            ViewData["Bookshelves"] = _db.Bookshelves.Select(bookshelf => new SelectListItem
            {
                Text = bookshelf.Name,
                Value = bookshelf.BookshelfId.ToString()
            });

            return View();
        }

        // POST: Create Book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            model.UserId = User.Identity.GetUserId();


            if (_service.CreateBook(model))
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
            var model = _service.GetBookById(id, User.Identity.GetUserId());

            return View(model);
        }

        // GET: Edit 
        public ActionResult Edit (int id)
        {
            var detail = _service.GetBookById(id, User.Identity.GetUserId());

            ViewData["Bookshelves"] = _db.Bookshelves.Select(bookshelf => new SelectListItem
            {
                Text = bookshelf.Name,
                Value = bookshelf.BookshelfId.ToString()
            });

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

            model.UserId = User.Identity.GetUserId();

            if (model.BookId != id)
            {
                ModelState.AddModelError("", "ID Mismatch!");
                return View(model);
            }


            if(_service.UpdateBook(model))
            {
                TempData["SaveResult"] = "Your book was updated!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your book could not be updated!");
            return View();
        }

        public ActionResult Delete(int id)
        {
            var model = _service.GetBookById(id, User.Identity.GetUserId());

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            _service.DeleteBook(id, User.Identity.GetUserId());

            TempData["SaveResult"] = "Your book was deleted!";

            return RedirectToAction("Index");
        }

        
    }
}