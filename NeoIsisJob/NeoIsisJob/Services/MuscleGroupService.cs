using NeoIsisJob.Models;
using NeoIsisJob.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Services
{
    public class MuscleGroupService
    {
        private readonly MuscleGroupRepo _muscleGroupRepository;

        public MuscleGroupService()
        {
            this._muscleGroupRepository = new MuscleGroupRepo();
        }

        public MuscleGroupModel GetMuscleGroupById(int muscleGroupId)
        {
            return this._muscleGroupRepository.GetMuscleGroupById(muscleGroupId);
        }


        //TODO -> the rest of CRUD if it is needed
    }
}
