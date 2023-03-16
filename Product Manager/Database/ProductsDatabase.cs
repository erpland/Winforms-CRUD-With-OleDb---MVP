using Product_Manager.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Product_Manager.Database
{
    public sealed class ProductsDatabase : BaseDatabase, IProductDB
    {
        private static ProductsDatabase instance;

        public static ProductsDatabase Instance()
        {
            if (instance == null)
            {
                instance = new ProductsDatabase();
            }
            return instance;
        }
        public IEnumerable<ProductModel> GetAll()
        {
            List<ProductModel> productsList = new List<ProductModel>();

            using (var connection = new OleDbConnection(this.connectionString))
            {
                OleDbCommand command = new OleDbCommand("SELECT * FROM Products", connection);
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProductModel product = new ProductModel();
                    product.Id = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Price = reader.GetInt32(2);
                    productsList.Add(product);
                }
            }

            return productsList;
        }
        public IEnumerable<ProductModel> GetBySearchValue(string searchValue)
        {
            int id = int.TryParse(searchValue, out id) ? id : 0;
            string name = searchValue;

            List<ProductModel> productsList = new List<ProductModel>();
            using (var connection = new OleDbConnection(this.connectionString))
            {
                OleDbCommand command = new OleDbCommand("SELECT * FROM Products WHERE Id=@id or Name like '%'+ @name +'%'", connection);
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("name", name);

                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProductModel product = new ProductModel();
                    product.Id = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Price = reader.GetInt32(2);
                    productsList.Add(product);
                }
            }

            return productsList;
        }
        public void Create(ProductModel product)
        {
            using (var connection = new OleDbConnection(this.connectionString))
            {
                OleDbCommand command = new OleDbCommand("INSERT INTO Products (Name,Price) VALUES(@name,@price)", connection);
                command.Parameters.AddWithValue("name", product.Name);
                command.Parameters.AddWithValue("price", product.Price);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OleDbConnection(this.connectionString))
            {
                OleDbCommand command = new OleDbCommand("DELETE FROM Products WHERE Id=@id", connection);
                command.Parameters.AddWithValue("id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Update(ProductModel product)
        {
            using (var connection = new OleDbConnection(this.connectionString))
            {
                OleDbCommand command = new OleDbCommand("UPDATE Products SET Name = @name, Price = @price WHERE Id=@id", connection);
                command.Parameters.AddWithValue("name", product.Name);
                command.Parameters.AddWithValue("price", product.Price);
                command.Parameters.AddWithValue("id", product.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
