namespace NeoIsisJob.Models
{
    public class UserModel
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private UserModel() { }

        public UserModel(int id)
        {
            Id = id;
        }
    }
}
