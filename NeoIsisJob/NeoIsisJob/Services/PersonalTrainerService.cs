﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Repositories;
using NeoIsisJob.Models;

// please add validation for the input parameters
namespace NeoIsisJob.Services
{
    public class PersonalTrainerService
    {
        private readonly PersonalTrainerRepo personalTrainerRepository;

        public PersonalTrainerService()
        {
            this.personalTrainerRepository = new PersonalTrainerRepo();
        }

        public List<PersonalTrainerModel> GetAllPersonalTrainers()
        {
            return personalTrainerRepository.GetAllPersonalTrainerModel();
        }

        public PersonalTrainerModel GetPersonalTrainerById(int personalTrainerId)
        {
            return personalTrainerRepository.GetPersonalTrainerModelById(personalTrainerId);
        }

        public void AddPersonalTrainer(PersonalTrainerModel personalTrainerModel)
        {
            personalTrainerRepository.AddPersonalTrainerModel(personalTrainerModel);
        }

        public void DeletePersonalTrainer(int personalTrainerId)
        {
            personalTrainerRepository.DeletePersonalTrainerModel(personalTrainerId);
        }

        // In case you guys need to update a personal trainer
        // create a method here that calls the UpdatePersonalTrainerModel method from the PersonalTrainerRepo +
        // create the UpdatePersonalTrainerModel method in the PersonalTrainerRepo
    }
}
