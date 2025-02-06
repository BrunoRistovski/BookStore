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
    public class BookService : IBookService
    {
        private readonly IRepository<Book> bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public Book CreateNewBook(Book book)
        {
            return bookRepository.Insert(book);
        }

        public Book DeleteBook(Guid id)
        {
            var book = bookRepository.Get(id);
            return bookRepository.Delete(book);
        }

        public List<Book> GetBook()
        {
            return bookRepository.GetAll().ToList();
        }

        public Book GetBookById(Guid? id)
        {
            return bookRepository.Get(id);
        }

        public Book UpdateBook(Book book)
        {
            return bookRepository.Update(book);
        }
    }
}
