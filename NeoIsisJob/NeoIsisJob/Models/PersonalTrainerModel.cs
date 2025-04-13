using System;

namespace NeoIsisJob.Models
{
    public class PersonalTrainerModel
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private DateTime _workStartDateTime;

        public int Id { get => _id; set => _id = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public DateTime WorkStartDateTime { get => _workStartDateTime; set => _workStartDateTime = value; }

        public PersonalTrainerModel() { }

        public PersonalTrainerModel(int id, string firstName, string lastName, DateTime workStartDateTime)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            WorkStartDateTime = workStartDateTime;
        }
    }
}
