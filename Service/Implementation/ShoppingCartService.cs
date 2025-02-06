using Domain.Domain_Models;
using Domain.DTO;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ShoppingCartService : IShoppingCartsService
    {
        private readonly IRepository<ShoppingCart> shoppingCartRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<BooksInOrders> booksInOrdersRepository;
        private readonly IRepository<BooksInShoppingCart> booksInShoppingCartRepository;
        private readonly IBooksInShoppingCartService booksInShoppingCartService;


        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, ICustomerRepository customerRepository, IRepository<Book> bookRepository,
            IRepository<Order> orderRepository, IRepository<BooksInOrders> booksInOrdersRepository, IRepository<BooksInShoppingCart> booksInShoppingCartRepository, IBooksInShoppingCartService booksInShoppingCartService)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.customerRepository = customerRepository;
            this.bookRepository = bookRepository;
            this.orderRepository = orderRepository;
            this.booksInOrdersRepository = booksInOrdersRepository;
            this.booksInShoppingCartRepository = booksInShoppingCartRepository;
            this.booksInShoppingCartService = booksInShoppingCartService;
        }

        public ShoppingCart AddBookToShoppingCart(string userId, AddToShoppingCartDto model)
        {
            if (userId != null)
            {
                var loggedInUser = customerRepository.Get(userId);
                //var shoppingCart = shoppingCartRepository.
                var shoppingCart = loggedInUser?.ShoppingCart;

                var book = bookRepository.Get(model.BookID);

                if (book != null && shoppingCart != null)
                {
                    shoppingCart?.BooksInShoppingCart?.Add(new BooksInShoppingCart
                    {
                        Book = book,
                        BookID = book.Id,
                        ShoppingCart = shoppingCart,
                        ShoppingCartID = shoppingCart.Id,
                        Quantity = model.Quantity
                    });

                    return shoppingCartRepository.Update(shoppingCart);
                }
            }
            return null;

        }

        public bool deleteFromShoppingCart(string userId, Guid? Id)
        {
            if (userId != null)
                {
                    var loggedInUser = customerRepository.Get(userId);
                    var result = booksInShoppingCartService.deleteFromShoppingCart(Id);
                    if (result)
                    {
                        shoppingCartRepository.Update(loggedInUser.ShoppingCart);
                        return true;
                    }
                }
                return false;
        }


        public AddToShoppingCartDto getBookInfo(Guid Id)
        {
            var selectedBook = bookRepository.Get(Id);
            if (selectedBook != null)
            {
                var model = new AddToShoppingCartDto
                {

                    BookID = selectedBook.Id,
                    Book = selectedBook,
                    Quantity = 1
                };
                return model;
            }
            return null;
        }

        public ShoppingCartDTO getShoppingCartDetails(string userId)
        {
            if (userId != null && !userId.IsNullOrEmpty())
            {
                var loggedInUser = customerRepository.Get(userId);

                var allBooks = loggedInUser?.ShoppingCart?.BooksInShoppingCart?.ToList();

                var totalPrice = 0.0;

                
                foreach (var item in allBooks)
                {
                    totalPrice += Double.Round((double)(item.Quantity * item.Book.Price), 2);
                }
                

                var model = new ShoppingCartDTO
                {
                    AllBooks = allBooks,
                    TotalPrice = totalPrice
                };

                return model;

            }

            return new ShoppingCartDTO
            {
                AllBooks = new List<BooksInShoppingCart>(),
                TotalPrice = 0.0
            };
        }

        public bool orderBooks(string userId)
        {
            if (userId != null && !userId.IsNullOrEmpty())
            {
                var loggedInUser = customerRepository.Get(userId);

                var userCart = loggedInUser?.ShoppingCart;

                var userOrder = new Order
                {
                    Id = Guid.NewGuid(),
                    CustomerId = userId,
                    Customer = loggedInUser
                };

                orderRepository.Insert(userOrder);

                var bookInOrders = userCart?.BooksInShoppingCart?.Select(z => new BooksInOrders
                {
                    Order = userOrder,
                    OrderId = userOrder.Id,
                    BookID = z.BookID,
                    Book = z.Book,
                    Quantity = z.Quantity,
                }).ToList();

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order conatins: ");

                for (int i = 1; i <= bookInOrders?.Count(); i++)
                {
                    var currentItem = bookInOrders[i - 1];
                    totalPrice += (double)(currentItem.Quantity * currentItem.Book.Price);
                    sb.AppendLine(i.ToString() + ". " + currentItem.Book.Title + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Book.Price);
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());

                //booksInOrdersRepository.InsertMany(bookInOrders);
                for (var i = 0; i < bookInOrders.Count; i++)
                {
                    var item = bookInOrders[i];
                    booksInOrdersRepository.Insert(item);
                }

                userCart?.BooksInShoppingCart.Clear();

                shoppingCartRepository.Update(userCart);

                return true;
            }
            return false;
        }
    }
}
