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
        private readonly ExerciseRepo _exerciseRepository;

        public ExerciseService()
        {
            this._exerciseRepository = new ExerciseRepo();
        }

        public ExercisesModel GetExerciseById(int exerciseId)
        {
            return this._exerciseRepository.GetExerciseById(exerciseId);
        }

        public IList<ExercisesModel> GetAllExercises()
        {
            return this._exerciseRepository.GetAllExercises();
        }
    }
}
