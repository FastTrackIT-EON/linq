using System;

namespace LinqExercises
{
    public class Student : Person
    {
        public Student(
            string firstName,
            string lastName,
            DateTime dateOfBirth,
            Gender gender,
            string university)
            : base (firstName, lastName, dateOfBirth, gender)
        { 
            University = university;
        }

        public string University { get; }
    }
}
