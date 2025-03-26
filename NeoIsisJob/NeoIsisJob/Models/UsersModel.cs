namespace NeoIsisJob.Models
{
    public class UsersModel
    {
        private int _id;

        public int Id { get => _id; set => _id = value; }

        private UsersModel() { }

        public UsersModel(int id) { Id = id; }
    }
}
