namespace NeoIsisJob.Models
{
    public class ExercisesModel
    {
        private int _id;
        private string _name;
        private string _description;
        private int _difficulty;
        private int _muscleGroupId;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Difficulty { get => _difficulty; set => _difficulty = value; }
        public int MuscleGroupId { get => _muscleGroupId; set => _muscleGroupId = value; }

        private ExercisesModel() { }

        public ExercisesModel(string name, string description, int difficulty, int muscleGroupId)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty;
            MuscleGroupId = muscleGroupId;
        }
    }
}
