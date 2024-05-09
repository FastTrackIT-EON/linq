using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LinqExercises
{
    public static class PersonsDatabase
    {
        public static List<Person> Persons { get; private set; } = new List<Person>();

        public static void ReadFromXmlUsingLinq(string filePath)
        {
            XElement persons = XElement.Load(filePath);

            List<Person> xmlPersons = new List<Person>();
            foreach (XElement personElement in persons.Descendants("Person"))
            {
                string firstName = personElement.Attribute("firstName")?.Value;
                string lastName = personElement.Attribute("lastName")?.Value;
                string genderString = personElement.Attribute("gender")?.Value;
                string dateOfBirthString = personElement.Attribute("dateOfBirth")?.Value;

                if (!Person.TryCreate(
                    firstName: firstName,
                    lastName: lastName,
                    genderString: genderString,
                    dateOfBirthString: dateOfBirthString,
                    result: out Person p))
                {
                    continue;
                }

                xmlPersons.Add(p);
            }

            PersonsDatabase.Persons = new List<Person>(xmlPersons);
        }

        public static void ReadFromXmlUsingXmlReader(string filePath)
        {
            List<Person> xmlPersons = new List<Person>();

            using (XmlReader reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Person")
                    {
                        string firstName = reader.GetAttribute("firstName");
                        string lastName = reader.GetAttribute("lastName");
                        string genderString = reader.GetAttribute("gender");
                        string dateOfBirthString = reader.GetAttribute("dateOfBirth");

                        if (!Person.TryCreate(
                            firstName: firstName,
                            lastName: lastName,
                            genderString: genderString,
                            dateOfBirthString: dateOfBirthString,
                            result: out Person p))
                        {
                            continue;
                        }

                        xmlPersons.Add(p);
                    }
                }
            }

            PersonsDatabase.Persons = new List<Person>(xmlPersons);
        }

        public static void ReadFromXmlUsingXmlDeserialization(string filePath)
        {
            List<Person> xmlPersons = new List<Person>();

            XmlSerializer serializer = new XmlSerializer(typeof(SerializablePersonCollection));
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                SerializablePersonCollection persons = serializer.Deserialize(fs) as SerializablePersonCollection;
                if (persons == null)
                {
                    throw new InvalidOperationException($"Unable to deserialize file '{filePath}'");
                }

                foreach (SerializablePerson person in persons.Persons)
                {
                    if (!Person.TryCreate(
                            firstName: person.FirstName,
                            lastName: person.LastName,
                            genderString: person.Gender,
                            dateOfBirthString: person.DateOfBirth,
                            result: out Person p))
                    {
                        continue;
                    }

                    xmlPersons.Add(p);
                }
            }

            PersonsDatabase.Persons = new List<Person>(xmlPersons);
        }

        [Serializable, XmlRoot(ElementName = "Persons")]
        public class SerializablePersonCollection
        {
            [XmlElement(ElementName = "Person")]
            public List<SerializablePerson> Persons { get; set; } = new List<SerializablePerson>();
        }

        [Serializable]
        public class SerializablePerson
        {
            [XmlAttribute("firstName")]
            public string FirstName { get; set; }

            [XmlAttribute("lastName")]
            public string LastName { get; set; }

            [XmlAttribute("gender")]
            public string Gender { get; set; }

            [XmlAttribute("dateOfBirth")]
            public string DateOfBirth { get; set; }
        }
    }
}
