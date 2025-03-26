namespace NeoIsisJob.Models
{
    public class UserModel
    {
        private int _id;

        public int Id { get => _id; set => _id = value; }

        public UserModel() { }

        public UserModel(int id) { Id = id; }
    }
}
