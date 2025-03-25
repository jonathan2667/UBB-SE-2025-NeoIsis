namespace NeoIsisJob.Models
{
    public class MuscleGroupModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        private MuscleGroupModel() { }

        public MuscleGroupModel(string name)
        {
            Name = name;
        }
    }
}
