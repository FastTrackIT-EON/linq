using System;

namespace LinqExercises
{
    public class Category
    {
        public Category(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id
        {
            get;
        }

        public string Name
        {
            get;
        }

        public bool IsVirtual { get; set; }

        public bool IsPhysical { get; set; }

        public void Print()
        {
            Console.WriteLine($"#{Id}) Category '{Name}'");
        }
    }
}
