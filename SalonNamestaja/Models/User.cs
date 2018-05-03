using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalonNamestaja.Models
{
    public class User
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string role { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
    }
}