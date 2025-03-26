namespace NeoIsisJob.Models
{
    public class WorkoutTypeModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        private WorkoutTypeModel() { }

        public WorkoutTypeModel(string name)
        {
            Name = name;
        }
    }
}
