namespace NeoIsisJob.Models
{
    public class WorkoutModel
    {
        private int _id;
        private string _name;
        private int _workoutTypeId;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int WorkoutTypeId { get => _workoutTypeId; set => _workoutTypeId = value; }

        public WorkoutModel() { }

        public WorkoutModel(int id, string name, int workoutTypeId)
        {
            Id = id;
            Name = name;
            WorkoutTypeId = workoutTypeId;
        }
    }
}
