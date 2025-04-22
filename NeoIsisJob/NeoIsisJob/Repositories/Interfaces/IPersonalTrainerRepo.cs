using System;
using System.Collections.Generic;
using NeoIsisJob.Models;

namespace NeoIsisJob.Repositories
{
    public interface IPersonalTrainerRepo
    {
        PersonalTrainerModel GetPersonalTrainerModelById(int personalTrainerId);
        List<PersonalTrainerModel> GetAllPersonalTrainerModel();
        void AddPersonalTrainerModel(PersonalTrainerModel personalTrainer);
        void DeletePersonalTrainerModel(int personalTrainerId);
    }
}