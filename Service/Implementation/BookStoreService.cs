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
    public class BookStoreService : IBookStoreService
    {
        private readonly IRepository<BookStore> bookStoreRepository;

        public BookStoreService(IRepository<BookStore> bookStoreRepository)
        {
            this.bookStoreRepository = bookStoreRepository;
        }

        public BookStore CreateNewBookStore(BookStore bookStore)
        {
            return bookStoreRepository.Insert(bookStore);
        }

        public BookStore DeleteBookStore(Guid id)
        {
            var bookStore = bookStoreRepository.Get(id);
            return bookStoreRepository.Delete(bookStore);
        }

        public List<BookStore> GetBookStores()
        {
            return bookStoreRepository.GetAll().ToList();
        }

        public BookStore GetBookStoreById(Guid? id)
        {
            return bookStoreRepository.Get(id);
        }

        public BookStore UpdateBookStore(BookStore bookStore)
        {
            return bookStoreRepository.Update(bookStore);
        }
    }
}
