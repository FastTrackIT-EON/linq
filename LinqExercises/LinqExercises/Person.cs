using System;
using System.Globalization;

namespace LinqExercises
{
    public class Person
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
            => $"{FirstName} {LastName}";

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
    }
}
