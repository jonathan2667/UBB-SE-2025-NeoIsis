namespace NeoIsisJob.Models
{
    public class ClassTypeModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        private ClassTypeModel() { }

        public ClassTypeModel(string name)
        {
            Name = name;
        }
    }
}
