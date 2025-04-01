using NeoIsisJob.Models;
using NeoIsisJob.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Servs
{
    class ExerciseService
    {
        private readonly ExerciseRepo _exerciseRepo;

        public ExerciseService()
        {
            this._exerciseRepo = new ExerciseRepo();
        }

        public ExercisesModel GetExerciseById(int eid)
        {
            return this._exerciseRepo.GetExerciseById(eid);
        }
    }
}
