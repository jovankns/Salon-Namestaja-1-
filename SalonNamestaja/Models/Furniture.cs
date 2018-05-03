using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalonNamestaja.Models
{
    public class Furniture
    {
        public int id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public string country { get; set; }
        public string nameOfTheManufacturer { get; set; }
        public string price { get; set; }
        public string quantity { get; set; }
        //public string CategoryName { get; set; }
        public string year { get; set; }
        public string salon { get; set; }
        public string ImageSource { get; set; }

        private string _category;
        public string CategoryName
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }
    }
}