namespace NeoIsisJob.Models
{
    public class ClassModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int ClassTypeId { get; private set; }
        public int PersonalTrainerId { get; private set; }

        private ClassModel() { }

        public ClassModel(string name, string description, int classTypeId, int personalTrainerId)
        {
            Name = name;
            Description = description;
            ClassTypeId = classTypeId;
            PersonalTrainerId = personalTrainerId;
        }
    }
}
