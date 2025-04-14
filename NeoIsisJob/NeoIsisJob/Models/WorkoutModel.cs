using System;

namespace NeoIsisJob.Models
{
    public class WorkoutModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public TimeSpan Duration { get; set; }
        public int MuscleGroupId { get; set; }
        public int WorkoutTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public WorkoutModel() { }

        public WorkoutModel(int id, string name, int workoutTypeId)
        {
            Id = id;
            Name = name;
            WorkoutTypeId = workoutTypeId;
        }
    }
}
