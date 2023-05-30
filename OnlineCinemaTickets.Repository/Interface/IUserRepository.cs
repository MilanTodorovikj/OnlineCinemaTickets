using OnlineCinemaTickets.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaTickets.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<OnlineCinemaUser> GetAll();
        OnlineCinemaUser Get(string id);
        void Insert(OnlineCinemaUser entity);
        void Update(OnlineCinemaUser entity);
        void Delete(OnlineCinemaUser entity);
    }
}
