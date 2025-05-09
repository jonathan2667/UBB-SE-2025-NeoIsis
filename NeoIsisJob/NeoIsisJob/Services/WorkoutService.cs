﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories;
using NeoIsisJob.Repositories.Interfaces;
using NeoIsisJob.Services.Interfaces;

namespace NeoIsisJob.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository workoutRepository;

        public WorkoutService()
        {
            this.workoutRepository = new WorkoutRepo();
        }

        public WorkoutService(IWorkoutRepository workoutRepository)
        {
            this.workoutRepository = workoutRepository; // ?? throw new ArgumentNullException(nameof(workoutRepository));
        }

        public WorkoutModel GetWorkout(int workoutId)
        {
            return this.workoutRepository.GetWorkoutById(workoutId);
        }

        public WorkoutModel GetWorkoutByName(string workoutName)
        {
            return this.workoutRepository.GetWorkoutByName(workoutName);
        }

        public void InsertWorkout(string workoutName, int workoutTypeId)
        {
            // NAME HAS TO BE UNIQUE
            if (string.IsNullOrWhiteSpace(workoutName))
            {
                throw new ArgumentException("Workout name cannot be empty or null.");
            }

            try
            {
                this.workoutRepository.InsertWorkout(workoutName, workoutTypeId);
            }
            catch (SqlException ex) when (ex.Number == 2627) // SQL Server unique constraint violation
            {
                throw new Exception("A workout with this name already exists.");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting workout.", ex);
            }
        }

        public void DeleteWorkout(int workoutId)
        {
            this.workoutRepository.DeleteWorkout(workoutId);
        }

        public void UpdateWorkout(WorkoutModel workout)
        {
            if (workout == null)
            {
                throw new ArgumentNullException(nameof(workout), "Workout cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(workout.Name))
            {
                throw new ArgumentException("Workout name cannot be empty or null.", nameof(workout.Name));
            }

            try
            {
                this.workoutRepository.UpdateWorkout(workout);
            }
            catch (Exception ex) when (ex.Message.Contains("A workout with this name already exists"))
            {
                throw new Exception("A workout with this name already exists. Please choose a different name.");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the workout: {ex.Message}", ex);
            }
        }

        public IList<WorkoutModel> GetAllWorkouts()
        {
            return this.workoutRepository.GetAllWorkouts();
        }
    }
}
