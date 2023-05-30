﻿using OnlineCinemaTickets.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaTickets.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public OnlineCinemaUser User { get; set; }

        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }
    }
}
