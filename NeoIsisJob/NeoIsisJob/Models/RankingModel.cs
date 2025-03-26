namespace NeoIsisJob.Models
{
    public class RankingModel
    {
        public int UserId { get; private set; }
        public int MuscleGroupId { get; private set; }
        public int Rank { get; private set; }

        private RankingModel() { }

        public RankingModel(int userId, int muscleGroupId, int rank)
        {
            UserId = userId;
            MuscleGroupId = muscleGroupId;
            Rank = rank;
        }
    }
}
