namespace NeoIsisJob.Models
{
    public class WorkoutTypeModel
    {
        private int _id;
        private string _name;
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

        private WorkoutTypeModel() { }

        public WorkoutTypeModel(string name)
        {
            Name = name;
        }
    }
}
