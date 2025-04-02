using NeoIsisJob.Models;
using NeoIsisJob.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Servs
{
    public class MuscleGroupService
    {
        private readonly MuscleGroupRepo _muscleGroupRepo;

        public MuscleGroupService()
        {
            this._muscleGroupRepo = new MuscleGroupRepo();
        }

        public MuscleGroupModel GetMuscleGroupById(int mgid)
        {
            return this._muscleGroupRepo.GetMuscleGroupById(mgid);
        }


        //TODO -> the rest of CRUD if it is needed
    }
}
