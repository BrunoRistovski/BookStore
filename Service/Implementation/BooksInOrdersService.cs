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
    public class BooksInOrdersService : IBooksInOrdersService
    {
        private readonly IRepository<BooksInOrders> booksInOrdersRepository;
        private readonly IRepository<Book> bookRepository;

        public BooksInOrdersService(IRepository<BooksInOrders> booksInOrdersRepository, IRepository<Book> bookRepository)
        {
            this.booksInOrdersRepository = booksInOrdersRepository;
            this.bookRepository = bookRepository;
        }

        public bool deleteFromBooksInOrders(Guid bookId)
        {
            if (bookId != Guid.Empty)
            {
                var allBooksInOrders = booksInOrdersRepository.GetAll();
                foreach (BooksInOrders item in allBooksInOrders){

                    if (item.BookID.Equals(bookId))
                    {
                        booksInOrdersRepository.Delete(item);
                    }
                }
                return true;
            }
            return false;
        }
    }
}
