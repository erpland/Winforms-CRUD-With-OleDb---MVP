using Product_Manager.Database;
using Product_Manager.Views;
using Product_Manager.Models;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using Product_Manager.Presenters.Common;

namespace Product_Manager.Presenters
{
    public sealed class ProductsPresenter
    {
        private static ProductsPresenter instance;
        BindingSource productBindingSource;
        ProductsView view;
        ProductsDatabase database;
        IEnumerable<ProductModel> productsList;
        private ProductsPresenter(ProductsView view)
        {
            this.view = view;
            this.database = ProductsDatabase.Instance();

            this.productBindingSource = new BindingSource();
            this.view.SetBindingSource(productBindingSource);

        }
        public static ProductsPresenter Instance(ProductsView view)
        {
            if (instance == null)
            {
                instance = new ProductsPresenter(view);
            }
            return instance;
        }

        public void LoadAllProducts()
        {
            try
            {
                productsList = database.GetAll();
                productBindingSource.DataSource = productsList;
                ClearFields();
            }
            catch (Exception ex)
            {
                view.IsSuccssfull = false;
                view.Message = ex.Message;
            }
        }
        public void CreateNewProduct()
        {
            try
            {
                ProductModel product = new ProductModel(view.ProductName, Convert.ToDouble(view.ProductPrice));
                ProductDataValidation.ValidateProduct(product);
                database.Create(product);
                LoadAllProducts();
                ClearFields();
                view.IsSuccssfull = true;
                view.Message = "Succsesfully created new record";
            }
            catch (Exception ex)
            {
                view.IsSuccssfull = false;
                view.Message = ex.Message;
            }
        }

        public void UpdateProduct()
        {
            try
            {
                ProductModel product = new ProductModel(Convert.ToInt32(view.ProductId), view.ProductName, Convert.ToDouble(view.ProductPrice));
                ProductDataValidation.ValidateProduct(product);
                database.Update(product);
                LoadAllProducts();
                ClearFields();
                view.IsSuccssfull = true;
                view.Message = "Succsesfully updated record with id " + product.Id;
            }
            catch (Exception ex)
            {
                view.IsSuccssfull = false;
                view.Message = ex.Message;
            }
        }
        public void DeleteProduct()
        {
            try
            {
                int productId = Convert.ToInt32(view.ProductId);
                database.Delete(productId);
                LoadAllProducts();
                ClearFields();
                view.IsSuccssfull = true;
                view.Message = "Succsesfully deleted record with id " + productId;
            }
            catch (Exception ex)
            {
                view.IsSuccssfull = false;
                view.Message = ex.Message;
            }
        }
        public void SearchList()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(view.SearchValue))
                {
                    LoadAllProducts();
                }
                else
                {
                    productsList = database.GetBySearchValue(view.SearchValue);
                    productBindingSource.DataSource = productsList;
                }
            }
            catch (Exception ex)
            {
                view.IsSuccssfull = false;
                view.Message = ex.Message;
            }
        }
        public void ClearFields()
        {
            view.ProductId = "";
            view.ProductName = "";
            view.ProductPrice = "";
        }


    }
}
