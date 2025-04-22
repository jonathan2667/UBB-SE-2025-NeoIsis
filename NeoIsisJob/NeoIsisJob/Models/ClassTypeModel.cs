namespace NeoIsisJob.Models
{
    public class ClassTypeModel
    {
        private int id;
        private string name;
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        public ClassTypeModel()
        {
        }

        public ClassTypeModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
