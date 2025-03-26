using Windows.ApplicationModel.Activation;

namespace NeoIsisJob.Models
{
    public class ClassModel
    {
        private int _id;
        private string _name;
        private string _description;
        private int _classTypeId;
        private int _personalTrainerId;

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
        public int ClassTypeId
        {
            get { return _classTypeId; }
            set { _classTypeId = value; }
        }
        public int PersonalTrainerId
        {
            get { return _personalTrainerId; }
            set { _personalTrainerId = value; }
        }

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
