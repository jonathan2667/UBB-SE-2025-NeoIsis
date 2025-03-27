using NeoIsisJob.Models;
using NeoIsisJob.Repos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NeoIsisJob.Servs
{
    public class WorkoutTypeService
    {
        private readonly WorkoutTypeRepo _workoutTypeRepo;

        public WorkoutTypeService() { this._workoutTypeRepo = new WorkoutTypeRepo(); }

        public void InsertWorkoutType(String name) 
        {
            //NAME HAS TO BE UNIQUE
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Workout type name cannot be empty or null.");

            try
            {
                this._workoutTypeRepo.InsertWorkoutType(name);
            }
            catch (SqlException ex) when (ex.Number == 2627) // SQL Server unique constraint violation
            {
                throw new Exception("A workout type with this name already exists.");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting workout type.", ex);
            }
        }

        public void DeleteWorkoutType(int wtid)
        {
            this._workoutTypeRepo.DeleteWorkoutType(wtid);
        }

        public WorkoutTypeModel GetWorkoutType(int wtid) 
        {
            return this._workoutTypeRepo.GetWorkoutTypeById(wtid);
        }

        public IList<WorkoutTypeModel> GetAllWorkoutTypes()
        {
            return this._workoutTypeRepo.GetAllWorkoutTypes();
        }
    }
}
