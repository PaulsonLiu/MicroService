using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Models
{
    public class User
    {
        public User()
        {

        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
