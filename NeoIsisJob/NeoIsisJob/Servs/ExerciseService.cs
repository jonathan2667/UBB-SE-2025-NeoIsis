using NeoIsisJob.Models;
using NeoIsisJob.Repos;
using NeoIsisJob.Repos.Interfaces;
using NeoIsisJob.Servs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Servs
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseService()
        {
            this._exerciseRepository = new ExerciseRepo();
        }

        public ExerciseService(IExerciseRepository exerciseRepository)
        {
            this._exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
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
