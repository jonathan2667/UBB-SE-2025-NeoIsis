using System;

namespace NeoIsisJob.Models
{
    public class WorkoutModel
    {
        private int _id;
        private string _name;
        private int _workoutTypeId;
        private string _description;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int WorkoutTypeId { get => _workoutTypeId; set => _workoutTypeId = value; }
        public string Description { get => _description; set => _description = value; }

        public WorkoutModel() { }

        public WorkoutModel(int id, string name, int workoutTypeId, string description = null)
        {
            Id = id;
            Name = name;
            WorkoutTypeId = workoutTypeId;
            Description = description;
        }
    }
}
