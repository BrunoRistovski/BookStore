using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Domain_Models;
using Repository;
using Service.Interface;
using Domain.DTO;

namespace Web.Controllers
{
    public class BookStoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookStoreService bookStoreService;
        private readonly IBookService bookService;
        private readonly IBooksInBookStoresService booksInBookStoresService;

        public BookStoresController(ApplicationDbContext context, IBookStoreService bookStoreService, IBookService bookService, IBooksInBookStoresService booksInBookStoresService)
        {
            _context = context;
            this.bookStoreService = bookStoreService;
            this.bookService = bookService;
            this.booksInBookStoresService = booksInBookStoresService;

        }

        public IActionResult AddBook(Guid? id)
        {
            AddBookToBookStore dto = new AddBookToBookStore();
            dto.BookStoreId = id;
            dto.Books = bookService.GetBook();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Confirm(AddBookToBookStore dto)
        {
            booksInBookStoresService.Confirm(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: BookStores
        public IActionResult Index()
        {
            return View(bookStoreService.GetBookStores());
        }

        // GET: BookStores/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookStore = bookStoreService.GetBookStoreById(id);
            if (bookStore == null)
            {
                return NotFound();
            }

            return View(bookStore);
        }

        // GET: BookStores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookStores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Address,City,Email,PhoneNumber,WebsiteUrl,Id")] BookStore bookStore)
        {
            if (ModelState.IsValid)
            {
                bookStoreService.CreateNewBookStore(bookStore);
                return RedirectToAction(nameof(Index));
            }
            return View(bookStore);
        }

        // GET: BookStores/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookStore = bookStoreService.GetBookStoreById(id);
            if (bookStore == null)
            {
                return NotFound();
            }
            return View(bookStore);
        }

        // POST: BookStores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Address,City,Email,PhoneNumber,WebsiteUrl,Id")] BookStore bookStore)
        {
            if (id != bookStore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                bookStoreService.UpdateBookStore(bookStore);
                return RedirectToAction(nameof(Index));
            }
            return View(bookStore);
        }

        // GET: BookStores/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookStore = bookStoreService.GetBookStoreById(id);
            if (bookStore == null)
            {
                return NotFound();
            }

            return View(bookStore);
        }

        // POST: BookStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            bookStoreService.DeleteBookStore(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BookStoreExists(Guid id)
        {
            return  bookStoreService.GetBookStoreById(id) != null;
        }
    }
}
