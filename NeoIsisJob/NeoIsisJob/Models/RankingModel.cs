namespace NeoIsisJob.Models
{
    public class RankingModel
    {
        private int _userId;
        private int _muscleGroupId;
        private int _rank;
        
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public int MuscleGroupId
        {
            get { return _muscleGroupId; }
            set { _muscleGroupId = value; }
        }
        public int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        private RankingModel() { }

        public RankingModel(int userId, int muscleGroupId, int rank)
        {
            UserId = userId;
            MuscleGroupId = muscleGroupId;
            Rank = rank;
        }
    }
}
