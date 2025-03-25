namespace NeoIsisJob.Models
{
    public class ExercisesModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Difficulty { get; private set; }

        private ExercisesModel() { }

        public ExercisesModel(string name, string description, string difficulty)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty;
        }
    }
}
