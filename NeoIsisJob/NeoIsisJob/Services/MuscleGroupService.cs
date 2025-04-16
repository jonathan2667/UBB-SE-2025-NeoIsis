using NeoIsisJob.Models;
using NeoIsisJob.Repositories;
using NeoIsisJob.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Services
{
    public class MuscleGroupService
    {
        private readonly IMuscleGroupRepo _muscleGroupRepository;

        public MuscleGroupService()
        {
            this._muscleGroupRepository = new MuscleGroupRepo();
        }

        public MuscleGroupService(IMuscleGroupRepo muscleGroupRepository)
        {
            this._muscleGroupRepository = muscleGroupRepository;
        }

        public MuscleGroupModel GetMuscleGroupById(int muscleGroupId)
        {
            return this._muscleGroupRepository.GetMuscleGroupById(muscleGroupId);
        }


        //TODO -> the rest of CRUD if it is needed
    }
}
