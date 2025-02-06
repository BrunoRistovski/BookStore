using Domain.Domain_Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class BooksInShoppingCartsService : IBooksInShoppingCartService
    {
        private readonly IRepository<BooksInShoppingCart> booksInShoppingCartRepository;
        private readonly IRepository<Book> bookRepository;

        public BooksInShoppingCartsService(IRepository<BooksInShoppingCart> booksInShoppingCartRepository)
        {
            this.booksInShoppingCartRepository = booksInShoppingCartRepository;
        }

        public bool deleteFromShoppingCart(Guid? bookId)
        {
            if (bookId != Guid.Empty)
            {
                var allBooksInOrders = booksInShoppingCartRepository.GetAll();
                foreach (BooksInShoppingCart item in allBooksInOrders)
                {

                    if (item.BookID.Equals(bookId))
                    {
                        booksInShoppingCartRepository.Delete(item);
                    }
                }
                return true;
            }
            return false;
        }
    }
}
