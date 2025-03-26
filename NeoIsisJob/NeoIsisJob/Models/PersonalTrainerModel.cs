using System;

namespace NeoIsisJob.Models
{
    public class PersonalTrainerModel
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private DateTime _worksSince;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public DateTime WorksSince
        {
            get { return _worksSince; }
            set { _worksSince = value; }
        }

        private PersonalTrainerModel() { }

        public PersonalTrainerModel(string firstName, string lastName, DateTime worksSince)
        {
            FirstName = firstName;
            LastName = lastName;
            WorksSince = worksSince;
        }
    }
}
