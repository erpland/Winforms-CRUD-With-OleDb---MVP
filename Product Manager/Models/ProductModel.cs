using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Product_Manager.Models
{
    public class ProductModel
    {
        [DisplayName("Product ID")]
        public int Id { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Product name must be between 3-50 charcters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Price is required")]
        [DisplayName("Product Price")]
        public double Price { get; set; }

        public ProductModel()
        {

        }

        public ProductModel(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public ProductModel(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
