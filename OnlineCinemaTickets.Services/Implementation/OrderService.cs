using OnlineCinemaTickets.Domain.DomainModels;
using OnlineCinemaTickets.Repository.Interface;
using OnlineCinemaTickets.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaTickets.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> getAllOrders()
        {
            return _orderRepository.getAllOrders();
        }
        public Order getOrderDetails(Guid id)
        {
            return this._orderRepository.getOrderDetails(id);
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return _orderRepository.getOrderDetails(model);
        }
    }
}
