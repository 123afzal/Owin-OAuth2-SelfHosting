using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testingOwin
{
    public class UserService
    {
        public ClientMaster GetUserByCredentials(string email, string password)
        {
            ClientMaster user = new ClientMaster() { Id = "1", Email = "email@domain.com", Password = "password", Name = "custom" };
            if (user != null)
            {
                user.Password = string.Empty;
            }
            return user;
        }
    }
}
