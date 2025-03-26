using System;

namespace NeoIsisJob.Models
{
    public class PersonalTrainerModel
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime WorksSince { get; private set; }

        private PersonalTrainerModel() { }

        public PersonalTrainerModel(string firstName, string lastName, DateTime worksSince)
        {
            FirstName = firstName;
            LastName = lastName;
            WorksSince = worksSince;
        }
    }
}
