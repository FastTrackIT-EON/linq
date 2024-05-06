using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace LinqExercises
{
    public static class PersonsDatabase
    {
        public static List<Person> Persons { get; private set; } = new List<Person>();

        public static void ReadFromXml(string filePath)
        {
            XElement persons = XElement.Load(filePath);

            List<Person> xmlPersons = new List<Person>();
            foreach (XElement personElement in persons.Descendants("Person"))
            {
                string firstName = personElement.Attribute("firstName")?.Value;
                if (string.IsNullOrEmpty(firstName))
                {
                    continue;
                }

                string lastName = personElement.Attribute("lastName")?.Value;
                if (string.IsNullOrEmpty(lastName))
                {
                    continue;
                }

                string genderString = personElement.Attribute("gender")?.Value;
                if (string.IsNullOrEmpty(genderString) ||
                    !Enum.TryParse(genderString, out Gender parsedGender))
                {
                    continue;
                }

                string dateOfBirthString = personElement.Attribute("dateOfBirth")?.Value;
                if (string.IsNullOrEmpty(dateOfBirthString) ||
                    !DateTime.TryParseExact(
                        dateOfBirthString,
                        "yyyy-MM-dd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime parsedDateOfBirth))
                {
                    continue;
                }

                Person p = new Person(firstName, lastName, parsedDateOfBirth, parsedGender);
                xmlPersons.Add(p);
            }

            PersonsDatabase.Persons = new List<Person>(xmlPersons);
        }
    }
}
