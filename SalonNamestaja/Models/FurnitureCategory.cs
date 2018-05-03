using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalonNamestaja.Models
{
    public class FurnitureCategory
    {
        public int id;
        [Display(Name = "Name")]
        private string _categoryName;
        public string CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                if (value != null)
                {
                    _categoryName = value;
                }
            }
        }

        private string _categoryDescription;
        public string CategoryDescription
        {
            get
            {
                return _categoryDescription;
            }
            set
            {
                if (value.Length > 10)
                {
                    _categoryDescription = value;
                }
            }
        }

        //private static int counter = 1;

        //public FurnitureCategory()
        //{
        //    id = counter;
        //    counter++;
        //}
    }
}