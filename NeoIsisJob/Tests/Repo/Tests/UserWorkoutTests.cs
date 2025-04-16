using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Repo.Mocks;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories;

namespace Tests.Repo.Tests
{
    [TestClass]
    public class UserWorkoutTests
    {

        UserWorkoutMock userWorkoutRepository = new UserWorkoutMock();

        [TestMethod]
        public void TestGetByUserID()
        {
            UserWorkoutModel res = userWorkoutRepository.GetUserWorkoutModel(1, 1, DateTime.Now);
            Assert.IsNotNull(res);
            Assert.AreEqual(res.UserId, 1);
        }
        [TestMethod]
        public void TestAddUserWorkout()
        {
            UserWorkoutModel newUserWorkout = new UserWorkoutModel(1, 4, DateTime.Now, false);
            userWorkoutRepository.AddUserWorkout(newUserWorkout);
            UserWorkoutModel res = userWorkoutRepository.GetUserWorkoutModel(1, 4, DateTime.Now);
            Assert.IsNotNull(res);
            Assert.AreEqual(res.UserId, 1);
            Assert.AreEqual(res.WorkoutId, 4);

            UserWorkoutModel nullUserWorkout = null;
            try
            {
                userWorkoutRepository.AddUserWorkout(nullUserWorkout);
                Assert.Fail("Expected ArgumentNullException was not thrown.");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(ex.ParamName, "userWorkout");
            }
        }

        [TestMethod]
        public void TestUpdateUserWorkout()
        {
            UserWorkoutModel existingUserWorkout = userWorkoutRepository.GetUserWorkoutModel(1, 1, DateTime.Now);
            userWorkoutRepository.UpdateUserWorkout(existingUserWorkout);
            UserWorkoutModel res = userWorkoutRepository.GetUserWorkoutModel(1, 1, DateTime.Now);
            Assert.IsNotNull(res);
            Assert.AreEqual(res.Completed, existingUserWorkout.Completed);
            UserWorkoutModel nullUserWorkout = null;
            try
            {
                userWorkoutRepository.UpdateUserWorkout(nullUserWorkout);
                Assert.Fail("Expected ArgumentNullException was not thrown.");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(ex.ParamName, "userWorkout");
            }
        }

        [TestMethod]
        public void TestDeleteUserWorkout()
        {
            UserWorkoutModel existingUserWorkout = userWorkoutRepository.GetUserWorkoutModel(1, 1, DateTime.Now);
            userWorkoutRepository.DeleteUserWorkout(existingUserWorkout.UserId, existingUserWorkout.WorkoutId, existingUserWorkout.Date);
            UserWorkoutModel res = userWorkoutRepository.GetUserWorkoutModel(1, 1, DateTime.Now);
            Assert.IsNull(res);
            try
            {
                userWorkoutRepository.DeleteUserWorkout(1, 1, DateTime.Now);
                Assert.Fail("Expected KeyNotFoundException was not thrown.");
            }
            catch (KeyNotFoundException ex)
            {
                Assert.AreEqual(ex.Message, "User workout not found.");
            }
        }
    }
}
