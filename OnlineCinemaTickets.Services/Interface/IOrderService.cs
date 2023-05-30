using OnlineCinemaTickets.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaTickets.Services.Interface
{
    public interface IOrderService
    {
        public List<Order> getAllOrders();
        public Order getOrderDetails(Guid id);
        public Order getOrderDetails(BaseEntity model);
    }
}
