namespace NeoIsisJob.Models
{
    public class UserModel
    {
        public int Id { get; private set; }

        private UserModel() { }

        public UserModel(int id)
        {
            Id = id;
        }
    }
}
