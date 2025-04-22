﻿namespace NeoIsisJob.Models
{
    public class RankingModel
    {
        private int userId;
        private int muscleGroupId;
        private int rank;

        public int UserId { get => userId; set => userId = value; }
        public int MuscleGroupId { get => muscleGroupId; set => muscleGroupId = value; }
        public int Rank { get => rank; set => rank = value; }

        private RankingModel()
        {
        }

        public RankingModel(int userId, int muscleGroupId, int rank)
        {
            UserId = userId;
            MuscleGroupId = muscleGroupId;
            Rank = rank;
        }
    }
}
