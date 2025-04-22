﻿namespace NeoIsisJob.Models
{
    public class CompleteWorkoutModel
    {
        private int _workoutId;
        private int _exerciseId;
        private int _sets;
        private int _repetitionsPerSet;

        private ExercisesModel _exercise;

        public int WorkoutId { get => _workoutId; set => _workoutId = value; }
        public int ExerciseId { get => _exerciseId; set => _exerciseId = value; }
        public int Sets { get => _sets; set => _sets = value; }
        public int RepetitionsPerSet { get => _repetitionsPerSet; set => _repetitionsPerSet = value; }

        //property for tracking the complete excercise
        public ExercisesModel Exercise { get => _exercise; set => _exercise = value; }

        private CompleteWorkoutModel() { }

        public CompleteWorkoutModel(int workoutId, int exerciseId, int sets, int repetitionsPerSet)
        {
            WorkoutId = workoutId;
            ExerciseId = exerciseId;
            Sets = sets;
            RepetitionsPerSet = repetitionsPerSet;
        }
    }
}
