using Domain.Domain_Models;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IShoppingCartsService
    {
        ShoppingCart AddBookToShoppingCart(string userId, AddToShoppingCartDto model);
        AddToShoppingCartDto getBookInfo(Guid Id);
        ShoppingCartDTO getShoppingCartDetails(string userId);
        Boolean deleteFromShoppingCart(string userId, Guid? Id);
        Boolean orderBooks(string userId);


    }
}
