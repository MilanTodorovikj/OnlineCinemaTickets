using OnlineCinemaTickets.Domain.DomainModels;
using OnlineCinemaTickets.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaTickets.Services.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        List<Ticket> GetAllTicketsWithGenre(string Genre);
        List<Ticket> GetAllTicketsByValidTo(DateTime year);
        Ticket GetDetailsForTicket(Guid? id);
        void CreateNewTicket(Ticket t);
        void UpdateExistingTicket(Ticket t);
        AddToShoppingCartDto GetShoppingCartInfo(Guid? id);
        void DeleteTicket(Guid id);
        bool AddToShoppingCart(AddToShoppingCartDto item, string userID);
    }
}
