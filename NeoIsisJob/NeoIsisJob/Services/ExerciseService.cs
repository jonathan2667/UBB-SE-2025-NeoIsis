using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories;
using NeoIsisJob.Repositories.Interfaces;
using NeoIsisJob.Services.Interfaces;

namespace NeoIsisJob.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository exerciseRepository;

        public ExerciseService()
        {
            this.exerciseRepository = new ExerciseRepo();
        }

        public ExerciseService(IExerciseRepository exerciseRepository)
        {
            this.exerciseRepository = exerciseRepository; // ?? throw new ArgumentNullException(nameof(exerciseRepository));
            // Do not need that throw(dependency injection assures that we either have an instance or it throws if not)
        }

        public ExercisesModel GetExerciseById(int exerciseId)
        {
            return this.exerciseRepository.GetExerciseById(exerciseId);
        }

        public IList<ExercisesModel> GetAllExercises()
        {
            return this.exerciseRepository.GetAllExercises();
        }
    }
}
