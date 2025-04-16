using NeoIsisJob.Models;
using System;
using System.Collections.Generic;

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