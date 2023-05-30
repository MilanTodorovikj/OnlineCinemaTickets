using OnlineCinemaTickets.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaTickets.Services.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteTicketFromSoppingCart(string userId, Guid productId);
        bool order(string userId);
    }
}
