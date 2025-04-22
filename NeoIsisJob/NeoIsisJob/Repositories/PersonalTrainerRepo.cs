using NeoIsisJob.Data;
using NeoIsisJob.Data.Interfaces;
using NeoIsisJob.Models;
using NeoIsisJob.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NeoIsisJob.Repositories
{
    public class PersonalTrainerRepo : IPersonalTrainerRepo
    {
        private readonly IDatabaseHelper _databaseHelper;

        public PersonalTrainerRepo()
        {
            this._databaseHelper = new DatabaseHelper();
        }

        public PersonalTrainerRepo(IDatabaseHelper databaseHelper)
        {
            this._databaseHelper = databaseHelper;
        }

        public PersonalTrainerModel? GetPersonalTrainerModelById(int personalTrainerId)
        {
            string query = "SELECT PTID, LastName, FirstName, WorksSince FROM PersonalTrainers WHERE PTID = @ptid";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ptid", personalTrainerId)
            };

            try
            {
                var dataTable = _databaseHelper.ExecuteReader(query, parameters);

                if (dataTable.Rows.Count > 0)
                {
                    var row = dataTable.Rows[0];
                    return new PersonalTrainerModel
                    {
                        Id = Convert.ToInt32(row["PTID"]),
                        LastName = Convert.ToString(row["LastName"]) ?? string.Empty,
                        FirstName = Convert.ToString(row["FirstName"]) ?? string.Empty,
                        WorkStartDateTime = row["WorksSince"] as DateTime? ?? DateTime.MinValue
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching personal trainer by ID: " + ex.Message);
            }
        }

        public List<PersonalTrainerModel> GetAllPersonalTrainerModel()
        {
            List<PersonalTrainerModel> trainers = new List<PersonalTrainerModel>();
            string query = "SELECT PTID, LastName, FirstName, WorksSince FROM PersonalTrainers";

            try
            {
                var dataTable = _databaseHelper.ExecuteReader(query, null);

                foreach (DataRow row in dataTable.Rows)
                {
                    trainers.Add(new PersonalTrainerModel
                    {
                        Id = Convert.ToInt32(row["PTID"]),
                        LastName = Convert.ToString(row["LastName"]) ?? string.Empty,
                        FirstName = Convert.ToString(row["FirstName"]) ?? string.Empty,
                        WorkStartDateTime = row["WorksSince"] as DateTime? ?? DateTime.MinValue
                    });
                }

                return trainers;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching all personal trainers: " + ex.Message);
            }
        }

        public void AddPersonalTrainerModel(PersonalTrainerModel personalTrainer)
        {
            string query = "INSERT INTO PersonalTrainers (LastName, FirstName, WorksSince) VALUES (@lastName, @firstName, @worksSince)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@lastName", personalTrainer.LastName),
                new SqlParameter("@firstName", personalTrainer.FirstName),
                new SqlParameter("@worksSince", personalTrainer.WorkStartDateTime)
            };

            try
            {
                _databaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding personal trainer: " + ex.Message);
            }
        }

        public void DeletePersonalTrainerModel(int personalTrainerId)
        {
            string query = "DELETE FROM PersonalTrainers WHERE PTID = @ptid";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ptid", personalTrainerId)
            };

            try
            {
                _databaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting personal trainer: " + ex.Message);
            }
        }
    }
}
