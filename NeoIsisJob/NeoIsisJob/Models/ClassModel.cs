namespace NeoIsisJob.Models
{
    public class ClassModel
    {
        private int _id;
        private string _name;
        private string _description;
        private int _classTypeId;
        private int _personalTrainerId;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int ClassTypeId { get => _classTypeId; set => _classTypeId = value; }
        public int PersonalTrainerId { get => _personalTrainerId; set => _personalTrainerId = value; }

        public ClassModel() { }

        public ClassModel(int id, string name, string description, int classTypeId, int personalTrainerId)
        {
            Id = id;
            Name = name;
            Description = description;
            ClassTypeId = classTypeId;
            PersonalTrainerId = personalTrainerId;
        }
    }
}
