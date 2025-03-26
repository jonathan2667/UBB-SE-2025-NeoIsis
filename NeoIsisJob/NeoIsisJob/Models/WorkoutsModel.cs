namespace NeoIsisJob.Models
{
    public class WorkoutModel
    {
        private int _id;
        private string _name;
        private int _workoutTypeId;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int WorkoutTypeId
        {
            get { return _workoutTypeId; }
            set { _workoutTypeId = value; }
        }

        private WorkoutModel() { }

        public WorkoutModel(string name, int workoutTypeId)
        {
            Name = name;
            WorkoutTypeId = workoutTypeId;
        }
    }
}
