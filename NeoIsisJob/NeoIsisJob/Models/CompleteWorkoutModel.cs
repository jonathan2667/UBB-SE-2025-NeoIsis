namespace NeoIsisJob.Models
{
    public class CompleteWorkoutModel
    {
        private int _workoutId;
        private int _exerciseId;
        private int _sets;
        private int _repsPerSet;

        public int WorkoutId { get => _workoutId; set => _workoutId = value; }
        public int ExerciseId { get => _exerciseId; set => _exerciseId = value; }
        public int Sets { get => _sets; set => _sets = value; }
        public int RepsPerSet { get => _repsPerSet; set => _repsPerSet = value; }

        private CompleteWorkoutModel() { }

        public CompleteWorkoutModel(int workoutId, int exerciseId, int sets, int repsPerSet)
        {
            WorkoutId = workoutId;
            ExerciseId = exerciseId;
            Sets = sets;
            RepsPerSet = repsPerSet;
        }
    }
}
