using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinemaTickets.Services.Interface
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}
