using Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ShoppingCartDTO
    {

        public List<BooksInShoppingCart> AllBooks { get; set; } // List of books in the shopping cart
        public double TotalPrice { get; set; } // Total price of all books in the cart

        public ShoppingCartDTO()
        {
            AllBooks = new List<BooksInShoppingCart>();
            TotalPrice = 0.0;
        }
    }
}

