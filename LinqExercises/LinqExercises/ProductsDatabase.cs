using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LinqExercises
{
    public static class ProductsDatabase
    {
        public static List<Category> Categories { get; private set; } = new List<Category>();

        public static List<Product> Products { get; private set; } = new List<Product>();

        public static void ReadFromXml(string filePath)
        {
            // XElement database = XElement.Load(filePath);
            XDocument database = XDocument.Load(filePath);

            List<Category> xmlCategories = new List<Category>();

            // foreach (XElement categoryElement in database.XPathSelectElements("./Categories/Category"))
            foreach (XElement categoryElement in database.Document.XPathSelectElements("/Database/Categories/Category"))
            {
                string categoryId = categoryElement.Attribute("id")?.Value;
                if (string.IsNullOrEmpty(categoryId))
                {
                    continue;
                }

                if (!int.TryParse(categoryId, out int categoryIdNumeric))
                {
                    continue;
                }

                string categoryName = categoryElement.Attribute("name")?.Value;
                if (string.IsNullOrEmpty(categoryName))
                {
                    continue;
                }

                Category category = new Category(categoryIdNumeric, categoryName);
                xmlCategories.Add(category);
            }

            List<Product> xmlProducts = new List<Product>();
            // foreach (XElement productElement in database.XPathSelectElements("./Products/Product"))
            foreach (XElement productElement in database.Document.XPathSelectElements("/Database/Products/Product"))
            {
                string productId = productElement.Attribute("id")?.Value;
                if (string.IsNullOrEmpty(productId))
                {
                    continue;
                }

                if (!int.TryParse(productId, out int productIdNumeric))
                {
                    continue;
                }

                string productName = productElement.Attribute("name")?.Value;
                if (string.IsNullOrEmpty(productName))
                {
                    continue;
                }

                string categoryId = productElement.Attribute("categoryId")?.Value;
                if (string.IsNullOrEmpty(categoryId))
                {
                    continue;
                }

                if (!int.TryParse(categoryId, out int categoryIdNumeric))
                {
                    continue;
                }

                Product product = new Product(productIdNumeric, productName, categoryIdNumeric);
                xmlProducts.Add(product);
            }

            ProductsDatabase.Categories = new List<Category>(xmlCategories);
            ProductsDatabase.Products = new List<Product>(xmlProducts);
        }
    }
}
