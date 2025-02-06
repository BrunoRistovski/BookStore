using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain_Models
{
    public class BooksInOrders : BaseEntity
    {
        public Guid? BookID { get; set; }
        public Book? Book { get; set; }
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        public int? Quantity { get; set; }
    }
}
