using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain_Models
{
    public class Book : BaseEntity
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }
        public int? Isbn { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? Price { get; set; }
        public virtual ICollection<BooksInBookStores>? BooksInBookStores { get; set; }
        public virtual ICollection<BooksInShoppingCart>? BooksInShoppingCart { get; set; }
        public virtual ICollection<BooksInOrders>? BooksInOrders { get; set; }
    }
}
