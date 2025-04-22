using NeoIsisJob.Models;

namespace NeoIsisJob.Repositories.Interfaces
{
    public interface IMuscleGroupRepo
    {
        MuscleGroupModel GetMuscleGroupById(int muscleGroupId);
    }
}