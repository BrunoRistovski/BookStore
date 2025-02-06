using Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class AddBookToBookStore
    {
        public List<Book>? Books { get; set; }
        public Guid? BookId { get; set; }
        public Book? Book { get; set; }
        public Guid? BookStoreId { get; set; }
        public BookStore? BookStore { get; set; }
        public int? Quantity { get; set; }
    }
}
