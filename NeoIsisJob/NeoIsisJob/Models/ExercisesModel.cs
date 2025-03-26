namespace NeoIsisJob.Models
{
    public class ExercisesModel
    {
        private int _id;
        private string _name;
        private string _description;
        private string _difficulty;

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
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Difficulty
        {
            get { return _difficulty; }
            set { _difficulty = value; }
        }

        private ExercisesModel() { }

        public ExercisesModel(string name, string description, string difficulty)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty;
        }
    }
}
