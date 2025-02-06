using Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class AddToShoppingCartDto
    {
        public Guid? BookID { get; set; }
        public Book? Book { get; set; }
        public int? Quantity { get; set; }
    }
}
