using Product_Manager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Manager.Presenters.Common
{
    public static class ProductDataValidation
    {
        public static void ValidateProduct(ProductModel product)
        {
            string errorMsg = "";
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext ctx = new ValidationContext(product);
            bool isValid = Validator.TryValidateObject(product, ctx, results, true);
            if (!isValid)
            {
                foreach (ValidationResult result in results)
                {
                    errorMsg += result.ErrorMessage + "\n";
                }
                throw new Exception(errorMsg);
            }
        }
    }
}
