namespace NeoIsisJob.Models
{
    public class UserModel
    {
        private int id;

        public int Id { get => id; set => id = value; }

        public UserModel()
        {
        }

        public UserModel(int id)
        {
            Id = id;
        }
    }
}
