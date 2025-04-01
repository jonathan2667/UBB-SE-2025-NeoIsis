namespace NeoIsisJob.Models
{
    public class ClassTypeModel
    {
        private int _id;
        private string _name;
        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }

        public ClassTypeModel() { }

        public ClassTypeModel(string name)
        {
            Name = name;
        }
    }
}
