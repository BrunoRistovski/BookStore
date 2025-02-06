using Domain.Domain_Models;
using Domain.Identity_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Repository
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookStore> BookStores { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<BooksInBookStores> BooksInBookStores { get; set; }
        public virtual DbSet<BooksInShoppingCart> BooksInShoppingCart { get; set; }
        public virtual DbSet<BooksInOrders> BooksInOrders { get; set; }


    }
}
