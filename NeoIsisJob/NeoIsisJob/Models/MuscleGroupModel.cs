namespace NeoIsisJob.Models
{
    public class MuscleGroupModel
    {
        private int id;
        private string name;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        public MuscleGroupModel()
        {
        }

        public MuscleGroupModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
