using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinemaTickets.Domain.Identity
{
    public class AddRoleModel
    {
        public string Username { get; set; }
        public List<string> usernames { get; set; }
        public List<string> roles { get; set; }
        public string SelectedRole { get; set; }
    }
}
