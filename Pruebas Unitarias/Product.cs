using System;


namespace Pruebas_Unitarias
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }


        public Product(int id, string name, decimal price, string category)
        {
            if (price < 0)
                throw new ArgumentException("El precio base de un producto no puede ser negativo.");

            if (category != "Electrónica" && category != "Alimentos")
                throw new ArgumentException("La categoría debe ser 'Electrónica' o 'Alimentos'.");

            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }
    }
}
