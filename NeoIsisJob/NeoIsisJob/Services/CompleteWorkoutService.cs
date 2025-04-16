using NeoIsisJob.Data;
using NeoIsisJob.Data.Interfaces; // Import the IDatabaseHelper interface
using NeoIsisJob.Models;
using NeoIsisJob.Repositories;
using NeoIsisJob.Repositories.Interfaces;
using NeoIsisJob.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeoIsisJob.Services
{
    public class CompleteWorkoutService : ICompleteWorkoutService
    {
        private readonly ICompleteWorkoutRepository _completeWorkoutRepository;

        // Constructor now takes IDatabaseHelper as a parameter for CompleteWorkoutRepo
        public CompleteWorkoutService(IDatabaseHelper databaseHelper)
        {
            // Injecting IDatabaseHelper into CompleteWorkoutRepo
            this._completeWorkoutRepository = new CompleteWorkoutRepo(databaseHelper);
        }

        // Second constructor allows dependency injection of ICompleteWorkoutRepository
        public CompleteWorkoutService(ICompleteWorkoutRepository completeWorkoutRepository)
        {
            this._completeWorkoutRepository = completeWorkoutRepository ?? throw new ArgumentNullException(nameof(completeWorkoutRepository));
        }

        // Default constructor to create CompleteWorkoutRepo with IDatabaseHelper (fallback)
        public CompleteWorkoutService()
        {
            var databaseHelper = new DatabaseHelper(); // Fallback to DatabaseHelper if no DI is used
            this._completeWorkoutRepository = new CompleteWorkoutRepo(databaseHelper);
        }

        public IList<CompleteWorkoutModel> GetAllCompleteWorkouts()
        {
            return this._completeWorkoutRepository.GetAllCompleteWorkouts();
        }

        public IList<CompleteWorkoutModel> GetCompleteWorkoutsByWorkoutId(int workoutId)
        {
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
