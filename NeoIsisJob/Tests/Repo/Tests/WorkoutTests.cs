using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Repositories;
using NeoIsisJob.Models;
using Tests.Repo.Mocks;

namespace Tests.Repo.Tests
{
    [TestClass]
    public class WorkoutTests
    {
        private WorkoutMock _workoutRepository = new WorkoutMock();
        [TestMethod]
        public void TestGetAllWorkouts()
        {
            IList<WorkoutModel> res = _workoutRepository.GetAllWorkouts();
            Assert.IsNotNull(res);
            Assert.AreEqual(res.Count, 3);
        }

        [TestMethod]
        public void TestInsertWorkout()
        {
            WorkoutModel newWorkout = new WorkoutModel(4, "Workout D", 4);
            _workoutRepository.InsertWorkout(newWorkout.Name, newWorkout.WorkoutTypeId);

            WorkoutModel insertedWorkout = _workoutRepository.GetWorkoutById(newWorkout.Id);
            Assert.IsNotNull(insertedWorkout);
        }

        [TestMethod]
        public void TestUpdateWorkout()
        {
            WorkoutModel existingWorkout = _workoutRepository.GetWorkoutById(1);
            existingWorkout.Name = "Updated Workout A";
            _workoutRepository.UpdateWorkout(existingWorkout);
            WorkoutModel updatedWorkout = _workoutRepository.GetWorkoutById(1);
            Assert.IsNotNull(updatedWorkout);
            Assert.AreEqual(updatedWorkout.Name, "Updated Workout A");

            WorkoutModel nonExistingWorkout = new WorkoutModel(999, "Non-existing Workout", 1);
            try
            {
                _workoutRepository.UpdateWorkout(nonExistingWorkout);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Workout not found.", ex.Message);
            }
        }

        [TestMethod]
        public void TestDeleteWorkout()
        {
            _workoutRepository.DeleteWorkout(1);
            WorkoutModel deletedWorkout = _workoutRepository.GetWorkoutById(1);
            Assert.IsNull(deletedWorkout);

            try
            {
                _workoutRepository.DeleteWorkout(999);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (KeyNotFoundException ex)
            {
                Assert.AreEqual("Workout not found.", ex.Message);
            }
        }
    }
}
