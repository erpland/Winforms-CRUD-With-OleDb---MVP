using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Manager.Models
{
    public interface IProductDB
    {
        void Create(ProductModel product);
        void Update(ProductModel product);
        void Delete(int id);
        IEnumerable<ProductModel> GetAll();
        IEnumerable<ProductModel> GetBySearchValue(string searchValue);
    }
}
