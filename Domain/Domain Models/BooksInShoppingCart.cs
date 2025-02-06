using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain_Models
{
    public class BooksInShoppingCart : BaseEntity
    {
        public Guid? BookID { get; set; }
        public Book? Book { get; set; }
        public Guid? ShoppingCartID { get; set; }
        public ShoppingCart? ShoppingCart { get; set; } 
        public int? Quantity { get; set; }
    }
}
