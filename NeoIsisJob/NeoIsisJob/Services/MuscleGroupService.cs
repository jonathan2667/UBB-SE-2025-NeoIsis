using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories;
using NeoIsisJob.Repositories.Interfaces;

namespace NeoIsisJob.Services
{
    public class MuscleGroupService
    {
        private readonly IMuscleGroupRepo muscleGroupRepository;

        public MuscleGroupService()
        {
            this.muscleGroupRepository = new MuscleGroupRepo();
        }

        public MuscleGroupService(IMuscleGroupRepo muscleGroupRepository)
        {
            this.muscleGroupRepository = muscleGroupRepository;
        }

        public MuscleGroupModel GetMuscleGroupById(int muscleGroupId)
        {
            return this.muscleGroupRepository.GetMuscleGroupById(muscleGroupId);
        }

        // TODO -> the rest of CRUD if it is needed
    }
}
