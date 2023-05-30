using OnlineCinemaTickets.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaTickets.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public OnlineCinemaUser Owner { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
    }
}
