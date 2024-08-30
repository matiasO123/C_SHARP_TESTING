using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Pruebas_Unitarias;

namespace Pruebas
{
    [TestClass]
    public class UnitTest
    {
        private ProductManager pm;

        [TestInitialize]
        public void SetUp()
        {
            pm = new ProductManager();
        }

        [TestMethod]
        public void TestAddProduct_empty()
        {
            pm.products.Clear();
            var product = new Product(1, "Laptop", 1000, "Electrónica");
            pm.AddProduct(product);
            Assert.AreEqual(1, pm.products.Count);
        }

        [TestMethod]
        public void TestAddProduct_x_2()
        {
            pm.products.Clear();
            var product = new Product(1, "Laptop", 1000, "Electrónica");
            pm.AddProduct(product);
            var product2 = new Product(2, "Laptop2", 1000, "Electrónica");
            pm.AddProduct(product2);
            Assert.AreEqual(2, pm.products.Count);
        }

        [TestMethod]
        public void AddProduct_existing_name()
        {
            pm.products.Clear();
            var product = new Product(1, "Laptop", 1000, "Electrónica");
            pm.AddProduct(product);
            var product2 = new Product(2, "Laptop", 1000, "Electrónica");
            pm.AddProduct(product2);
            Assert.AreEqual(1, pm.products.Count);
        }

        [TestMethod]
        public void AddProduct_check_attributes()
        {
            pm.products.Clear();
            int id = 1;
            string name = "Laptop";
            decimal price = 1000;
            string category = "Electrónica";
            var product = new Product(id, name, price, category);
            pm.AddProduct(product);

            Product producto_resultado = pm.products.Find(x => x.Id == id);

            Assert.AreEqual(id, producto_resultado.Id);
            Assert.AreEqual(name, producto_resultado.Name);
            Assert.AreEqual(price, producto_resultado.Price);
            Assert.AreEqual(category, producto_resultado.Category);
        }

        [TestMethod]
        public void TestCalculateTotalPriceElectronica()
        {
            var product = new Product(1, "Laptop", 1000, "Electrónica");
            pm.AddProduct(product);
            var totalPrice = pm.CalculateTotalPrice(1);
            Assert.AreEqual(1100, totalPrice);
        }

        [TestMethod]
        public void TestCalculateTotalPriceAlimentos()
        {
            var product = new Product(2, "Manzana", 2, "Alimentos");
            pm.AddProduct(product);
            var totalPrice = pm.CalculateTotalPrice(2);
            Assert.AreEqual(2.1m, totalPrice);
        }

        [TestMethod]
        public void TestFindProductByName()
        {
            var product = new Product(3, "Televisor", 1500, "Electrónica");
            pm.AddProduct(product);
            var foundProduct = pm.FindProductByName("Televisor");
            Assert.IsNotNull(foundProduct);
            Assert.AreEqual(3, foundProduct.Id);
        }

        [TestMethod]
        public void TestNegativePrice()
        {
            Assert.ThrowsException<ArgumentException>(() => new Product(4, "Televisor", -10, "Electrónica"));
        }

        [TestMethod]
        public void TestInvalidCategory()
        {
            Assert.ThrowsException<ArgumentException>(() => new Product(5, "Televisor", 100, "Muebles"));
        }

        [TestMethod]
        public void TestEditProduct()
        {
            var productManager = new ProductManager();
            var product = new Product (1, "Laptop", 1000m, "Electrónica");


            //new values
            string category = "Alimentos";
            int id = 33;
            decimal price = 1500m;
            productManager.AddProduct(product);
            productManager.EditProduct(id, "Laptop", category, price);

            var editedProduct = productManager.FindProductByName("Laptop");
            Assert.AreEqual(category, editedProduct.Category);
            Assert.AreEqual(id, editedProduct.Id);
            Assert.AreEqual(price, editedProduct.Price);         
        }



        [TestMethod]
        //Integración
        public void TestAddAndFindProduct()
        {
            var productManager = new ProductManager();
            var product = new Product (1,"Laptop",1000m,"Electrónica");

            productManager.AddProduct(product);
            var foundProduct = productManager.FindProductByName("Laptop");

            Assert.AreEqual(product, foundProduct);          
        }

        //Integración
        [TestMethod]
        public void TestAddEditAndDeleteProduct()
        {
            string name = "Laptop";
            var productManager = new ProductManager();
            var product = new Product(1, name, 1000m, "Alimentos");

            // Add product
            productManager.AddProduct(product);
            var addedProduct = productManager.FindProductByName(name);
            Assert.IsNotNull(addedProduct);

            // Edit product
            productManager.EditProduct(1, name, "Electrónica", 1500m);
            var editedProduct = productManager.FindProductByName(name);
            Assert.IsNotNull(editedProduct);
            Assert.AreEqual(1500m, editedProduct.Price);

            // Delete product
            productManager.DeleteProduct(name);
            var deletedProduct = productManager.FindProductByName(name);
            Assert.IsNull(deletedProduct);
        }


        //Performance test
        [TestMethod]
        public void TestPerformanceWithManyProducts()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var productManager = new ProductManager();
            for (int i = 0; i < 15000; i++)
            {
                var product = new Product (i, "Product" + i, 100m,"Electrónica");
                productManager.AddProduct(product);
            }          
            //var totalPrice = productManager.CalculateTotalPrice(99999);
            watch.Stop();
            Assert.IsTrue(watch.ElapsedMilliseconds < 10000,$"Se tardó este tiempo: {watch.ElapsedMilliseconds}");
        }

        [TestMethod]
        public void TestDeleteProduct()
        {
            var productManager = new ProductManager();
            string nombre = "Laptop";
            var product = new Product(1, nombre,1000m,  "Alimentos");

            productManager.AddProduct(product);
            var addedProduct = productManager.FindProductByName(nombre);
            Assert.AreEqual(product, addedProduct);
            productManager.DeleteProduct(product.Name);

            var deletedProduct = productManager.FindProductByName(nombre);

            Assert.IsTrue(deletedProduct == null);
        }
    }

}
