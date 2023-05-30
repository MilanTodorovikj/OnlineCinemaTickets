using OnlineCinemaTickets.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaTickets.Repository.Interface
{
    public interface IOrderRepository
    {
        public List<Order> getAllOrders();
        public Order getOrderDetails(Guid id);
        public Order getOrderDetails(BaseEntity model);
        

    }
}
