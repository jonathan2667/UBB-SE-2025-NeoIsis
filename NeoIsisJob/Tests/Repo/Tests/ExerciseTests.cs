using Moq;
using NeoIsisJob.Data.Interfaces;
using NeoIsisJob.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Data.SqlClient;

namespace Tests.Repo.Tests
{
    public class ExerciseRepoTests
    {
        private readonly Mock<IDatabaseHelper> _mockDatabaseHelper;
        private readonly ExerciseRepo _repo;

        public ExerciseRepoTests()
        {
            _mockDatabaseHelper = new Mock<IDatabaseHelper>();
            _repo = new ExerciseRepo(_mockDatabaseHelper.Object); // You’ll need to update your repo to accept IDatabaseHelper
        }

        [Fact]
        public void GetAllExercises_ShouldReturnListOfExercises()
        {
            // Arrange
            var dataTable = new DataTable();
            dataTable.Columns.Add("EID", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("Difficulty", typeof(int));
            dataTable.Columns.Add("MGID", typeof(int));

            dataTable.Rows.Add(1, "Push-up", "Chest exercise", 2, 1);
            dataTable.Rows.Add(2, "Squat", "Leg exercise", 3, 2);

            _mockDatabaseHelper.Setup(db => db.ExecuteReader(It.IsAny<string>(), null))
                               .Returns(dataTable);

            // Act
            var result = _repo.GetAllExercises();

            // Assert
            result.Should().HaveCount(2);
            result[0].Name.Should().Be("Push-up");
            result[1].Difficulty.Should().Be(3);
        }

        [Fact]
        public void GetExerciseById_ShouldReturnCorrectExercise_WhenFound()
        {
            // Arrange
            int exerciseId = 1;
            var dataTable = new DataTable();
            dataTable.Columns.Add("EID", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("Difficulty", typeof(int));
            dataTable.Columns.Add("MGID", typeof(int));

            dataTable.Rows.Add(1, "Push-up", "Chest exercise", 2, 1);

            _mockDatabaseHelper.Setup(db => db.ExecuteReader(It.IsAny<string>(), It.IsAny<SqlParameter[]>()))
                               .Returns(dataTable);

            // Act
            var result = _repo.GetExerciseById(exerciseId);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be("Push-up");
            result.MuscleGroupId.Should().Be(1);
        }

        [Fact]
        public void GetExerciseById_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            int exerciseId = 999;
            var emptyTable = new DataTable();
            emptyTable.Columns.Add("EID", typeof(int));
            emptyTable.Columns.Add("Name", typeof(string));
            emptyTable.Columns.Add("Description", typeof(string));
            emptyTable.Columns.Add("Difficulty", typeof(int));
            emptyTable.Columns.Add("MGID", typeof(int));

            _mockDatabaseHelper.Setup(db => db.ExecuteReader(It.IsAny<string>(), It.IsAny<SqlParameter[]>()))
                               .Returns(emptyTable);

            // Act
            var result = _repo.GetExerciseById(exerciseId);

            // Assert
            result.Should().BeNull();
        }
    }
}
