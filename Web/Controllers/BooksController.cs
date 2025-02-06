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
using Service.Implementation;
using System.Security.Claims;
using Domain.DTO;
using Repository.Implementation;
using Repository.Interface;

namespace Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly IBookService bookService;
        private readonly ICustomerRepository customerRepository;
        private readonly IRepository<ShoppingCart> repository;
        private readonly IBooksInOrdersService booksInOrdersService;

        public BooksController(ApplicationDbContext context, IBookService bookService, IShoppingCartsService shoppingCartsService, IRepository<ShoppingCart> repository, ICustomerRepository customerRepository, IBooksInOrdersService booksInOrdersService)
        {
            _context = context;
            this.bookService = bookService;
            this.shoppingCartsService = shoppingCartsService;
            this.repository = repository;
            this.customerRepository = customerRepository;
            this.booksInOrdersService = booksInOrdersService;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(bookService.GetBook());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Author,Genre,Description,Isbn,CreatedAt,Price,Id")] Book book)
        {
            if (ModelState.IsValid)
            {
                bookService.CreateNewBook(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Author,Genre,Description,Isbn,CreatedAt,Price,Id")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bookService.UpdateBook(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;
            var result2 = booksInOrdersService.deleteFromBooksInOrders(id);
            var result = shoppingCartsService.deleteFromShoppingCart(userId, id);
            if (result == true && result2==true)
                bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddBookToShoppingCart(Guid Id)
        {
            var result = shoppingCartsService.getBookInfo(Id);
            if (result != null)
            {
                return View(result);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddBookToShoppingCart(AddToShoppingCartDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = shoppingCartsService.AddBookToShoppingCart(userId, model);

            if (result != null)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
            else { return View(model); }
        }

        private bool BookExists(Guid id)
        {
            return bookService.GetBookById(id) != null ;
        }


    }
}
