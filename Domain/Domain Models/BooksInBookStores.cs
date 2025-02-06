using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain_Models
{
    public class BooksInBookStores : BaseEntity
    {
        public Guid? BookId { get; set; }
        public Book? Book { get; set; }
        public Guid? BookStoreId { get; set; }
        public BookStore? BookStore { get; set; }
        public int? Quantity { get; set; }
    }
}
