using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaTickets.Domain
{
    public class StripeSettings
    {
        public string PublishableKey { get; set; }
        public string SecretKey { get; set; }
    }
}
