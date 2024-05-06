using System;

namespace LinqExercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PersonsDatabase.ReadFromXml("Persons.xml");

            foreach (Person p in PersonsDatabase.Persons)
            {
                p.Print();
            }

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}
