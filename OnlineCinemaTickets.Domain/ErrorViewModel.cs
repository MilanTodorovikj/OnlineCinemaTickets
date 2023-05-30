using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaTickets.Domain
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
