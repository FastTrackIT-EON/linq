using System;
using System.Globalization;
using System.Runtime.Remoting;

namespace LinqExercises
{
    public class Person : IComparable<Person>, IEquatable<Person>
    {
        public Person(
            string firstName,
            string lastName,
            DateTime dateOfBirth,
            Gender gender)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Gender = gender;
            DateOfBirth = dateOfBirth;
        }

        public string FirstName
        {
            get;
        }

        public string LastName
        {
            get;
        }

        public string FullName
            => $"{LastName} {FirstName}";

        public Gender Gender
        {
            get;
        }

        public DateTime DateOfBirth
        {
            get;
        }

        public int Age
            => DateTime.Today.Year - DateOfBirth.Year;

        public void Print()
        {
            Console.WriteLine($"{FullName} date of birth: {DateOfBirth:yyyy-MM-dd}, age: {Age}");
        }

        internal static bool TryCreate(
            string firstName,
            string lastName,
            string genderString,
            string dateOfBirthString,
            out Person result)
        {
            result = null;

            if (string.IsNullOrEmpty(firstName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(genderString) ||
                !Enum.TryParse(genderString, out Gender parsedGender))
            {
                return false;
            }

            if (string.IsNullOrEmpty(dateOfBirthString) ||
                !DateTime.TryParseExact(
                    dateOfBirthString,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime parsedDateOfBirth))
            {
                return false;
            }

            result = new Person(firstName, lastName, parsedDateOfBirth, parsedGender);
            return true;
        }

        public int CompareTo(Person other)
        {
            // Less than zero: This instance precedes other in the sort order.
            // Zero: This instance occurs in the same position in the sort order as other.
            // Greater than zero: This instance follows other in the sort order.
            
            if (other is null)
            {
                // null is less than something
                return -1;
            }

            // DateTimes are compared by number of ticks that passed since Year 1
            int orderByTicks = this.DateOfBirth.CompareTo(other.DateOfBirth);
            return -orderByTicks;

            /*
            if (this.Age < other.Age)
            {
                // this person is younger than the other person
                return -1;
            }
            else if (this.Age == other.Age)
            {
                return this.DateOfBirth.Month.CompareTo(other.DateOfBirth.Month);
            }
            else
            {
                // this person is older than the other person
                return 1;
            }
            */
        }


        public bool Equals(Person other)
        {
            if (other is null)
            {
                return false;
            }

            return string.Equals(this.FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(this.LastName, other.LastName, StringComparison.OrdinalIgnoreCase) &&
                    this.DateOfBirth == other.DateOfBirth &&
                    this.Gender == other.Gender;

        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Person);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, DateOfBirth, Gender);
        }
        
    }
}
