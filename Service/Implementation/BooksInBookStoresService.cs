using Domain.Domain_Models;
using Domain.DTO;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class BooksInBookStoresService : IBooksInBookStoresService
    {
        private readonly IRepository<BooksInBookStores> booksInBookStoresRepository;

        public BooksInBookStoresService(IRepository<BooksInBookStores> booksInBookStoresRepository)
        {
            this.booksInBookStoresRepository = booksInBookStoresRepository;
        }

        public void Confirm(AddBookToBookStore dto)
        {
            BooksInBookStores bibs = new BooksInBookStores();
            bibs.BookStoreId = dto.BookStoreId;
            bibs.BookStore = dto.BookStore;
            bibs.BookId = dto.BookId;
            bibs.Book = dto.Book;
            bibs.Quantity = dto.Quantity;
            booksInBookStoresRepository.Insert(bibs);
        }
    }
}
