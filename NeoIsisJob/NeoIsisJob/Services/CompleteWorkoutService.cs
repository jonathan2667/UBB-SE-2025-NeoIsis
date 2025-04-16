using NeoIsisJob.Models;
using NeoIsisJob.Repositories;
using NeoIsisJob.Repositories.Interfaces;
using NeoIsisJob.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Services
{
    public class CompleteWorkoutService : ICompleteWorkoutService
    {
        private readonly ICompleteWorkoutRepository _completeWorkoutRepository;

        public CompleteWorkoutService()
        {
            this._completeWorkoutRepository = new CompleteWorkoutRepo();
        }

        public CompleteWorkoutService(ICompleteWorkoutRepository completeWorkoutRepository)
        {
            this._completeWorkoutRepository = completeWorkoutRepository; // ?? throw new ArgumentNullException(nameof(completeWorkoutRepository));
            // Do not need that throw(dependency injection assures that we either have an instance or it throws if not)
        }

        public IList<CompleteWorkoutModel> GetAllCompleteWorkouts()
        {
            return this._completeWorkoutRepository.GetAllCompleteWorkouts();
        }

        public IList<CompleteWorkoutModel> GetCompleteWorkoutsByWorkoutId(int workoutId)
        {
            //like filter in java
            //return (IList<CompleteWorkoutModel>)this._completeWorkoutRepo.GetAllCompleteWorkouts().Where(completeWorkout => completeWorkout.WorkoutId == wid);

            IList<CompleteWorkoutModel> completeWorkouts = new List<CompleteWorkoutModel>();
            foreach (CompleteWorkoutModel completeWorkout in this._completeWorkoutRepository.GetAllCompleteWorkouts().Where(completeWorkout => completeWorkout.WorkoutId == workoutId))
            {
                completeWorkouts.Add(completeWorkout);
            }

            return completeWorkouts;
        }

        public void DeleteCompleteWorkoutsByWorkoutId(int workoutId)
        {
            this._completeWorkoutRepository.DeleteCompleteWorkoutsByWorkoutId(workoutId);
        }

        public void InsertCompleteWorkout(int workoutId, int exerciseId, int sets, int repetitionsPerSet)
        {
            this._completeWorkoutRepository.InsertCompleteWorkout(workoutId, exerciseId, sets, repetitionsPerSet);
        }
    }
}
