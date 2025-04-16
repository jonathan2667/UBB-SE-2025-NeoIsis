using NeoIsisJob.Models;
using NeoIsisJob.Repositories;
using NeoIsisJob.Repositories.Interfaces;
using NeoIsisJob.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Services
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
            this._exerciseRepository = exerciseRepository; // ?? throw new ArgumentNullException(nameof(exerciseRepository));
            // Do not need that throw(dependency injection assures that we either have an instance or it throws if not)
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
