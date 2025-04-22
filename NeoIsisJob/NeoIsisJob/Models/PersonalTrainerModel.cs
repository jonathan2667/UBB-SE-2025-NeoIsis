﻿using System;

namespace NeoIsisJob.Models
{
    public class PersonalTrainerModel
    {
        private int id;
        private string firstName;
        private string lastName;
        private DateTime workStartDateTime;

        public int Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime WorkStartDateTime { get => workStartDateTime; set => workStartDateTime = value; }

        public PersonalTrainerModel()
        {
        }

        public PersonalTrainerModel(int id, string firstName, string lastName, DateTime workStartDateTime)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            WorkStartDateTime = workStartDateTime;
        }
    }
}
