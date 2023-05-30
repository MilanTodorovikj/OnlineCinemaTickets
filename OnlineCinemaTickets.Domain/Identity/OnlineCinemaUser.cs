using Microsoft.AspNetCore.Identity;
using OnlineCinemaTickets.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaTickets.Domain.Identity
{
    public class OnlineCinemaUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public virtual ShoppingCart UserCart { get; set; }
    }
}
