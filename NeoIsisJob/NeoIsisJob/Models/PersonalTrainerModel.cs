using System;

namespace NeoIsisJob.Models
{
    public class PersonalTrainerModel
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private DateTime _worksSince;

        public int Id { get => _id; set => _id = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public DateTime WorksSince { get => _worksSince; set => _worksSince = value; }

        public PersonalTrainerModel() { }

        public PersonalTrainerModel(string firstName, string lastName, DateTime worksSince)
        {
            FirstName = firstName;
            LastName = lastName;
            WorksSince = worksSince;
        }
    }
}
