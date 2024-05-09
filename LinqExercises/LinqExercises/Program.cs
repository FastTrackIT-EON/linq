using System;

namespace LinqExercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PersonsDatabase.ReadFromXmlUsingXmlDeserialization("Persons.xml");

            foreach (Person p in PersonsDatabase.Persons)
            {
                p.Print();
            }

            Console.WriteLine();
            Console.WriteLine("-------------");
            Console.WriteLine();

            ProductsDatabase.ReadFromXml("CategoriesWithProducts.xml");

            foreach (Category category in ProductsDatabase.Categories)
            {
                category.Print();
            }

            foreach (Product product in ProductsDatabase.Products)
            {
                product.Print();
            }

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}
