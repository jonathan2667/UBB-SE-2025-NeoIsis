namespace NeoIsisJob.Models
{
    public class WorkoutModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int WorkoutTypeId { get; private set; }

        private WorkoutModel() { }

        public WorkoutModel(string name, int workoutTypeId)
        {
            Name = name;
            WorkoutTypeId = workoutTypeId;
        }
    }
}
