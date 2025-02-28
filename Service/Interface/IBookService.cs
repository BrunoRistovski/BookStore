﻿using Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBookService
    {
        public List<Book> GetBook();
        public Book GetBookById(Guid? id);
        public Book CreateNewBook(Book book);
        public Book UpdateBook(Book book);
        public Book DeleteBook(Guid id);

    }
}
