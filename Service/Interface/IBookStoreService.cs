using Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBookStoreService
    {
        public List<BookStore> GetBookStores();
        public BookStore GetBookStoreById(Guid? id);
        public BookStore CreateNewBookStore(BookStore bookStore);
        public BookStore UpdateBookStore(BookStore bookStore);
        public BookStore DeleteBookStore(Guid id);
    }
}
