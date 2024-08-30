using System;
using System.Collections.Generic;
using System.Linq;

namespace Pruebas_Unitarias
{
    public class ProductManager
    {
        public List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {
            var producto_temp = FindProductByName(product.Name);
            if (null != producto_temp) return;
            products.Add(product);
        }

        public void EditProduct(int productId, string newName, string newCategory, decimal newPrice)
        {
            Product product_temp = FindProductByName(newName);
            if (product_temp == null) throw new Exception("Producto no encontrado");
            product_temp.Id = productId;
            product_temp.Category = newCategory;
            product_temp.Price = newPrice;
        }

        public decimal CalculateTotalPrice(int productId)
        {
            var product = products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                throw new ArgumentException("Producto no encontrado.");

            if (product.Category == "Electrónica")
                return product.Price * 1.10m;
            else if (product.Category == "Alimentos")
                return product.Price * 1.05m;

            throw new ArgumentException("Categoría de producto desconocida.");
        }

        public Product FindProductByName(string name)
        {
            return products.FirstOrDefault(p => p.Name == name);
        }

        public void DeleteProduct(string productName)
        {
            var product = FindProductByName(productName);
            if (product == null)
                throw new ArgumentException("Producto no encontrado.");
            products.Remove(product);
        }

    }
}
