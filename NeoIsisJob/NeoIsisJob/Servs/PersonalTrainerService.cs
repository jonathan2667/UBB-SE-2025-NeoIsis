using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoIsisJob.Repos;
using NeoIsisJob.Models;

// please add validation for the input parameters

namespace NeoIsisJob.Servs
{
    public class PersonalTrainerService
    {
        private readonly PersonalTrainerRepo _personalTrainerRepo;

        public PersonalTrainerService(PersonalTrainerRepo personalTrainerRepo) { _personalTrainerRepo = personalTrainerRepo; }

        public List<PersonalTrainerModel> GetAllPersonalTrainers()
        {
            return _personalTrainerRepo.GetAllPersonalTrainerModel();
        }

        public PersonalTrainerModel GetPersonalTrainerById(int pid)
        {
            return _personalTrainerRepo.GetPersonalTrainerModelById(pid);
        }

        public void AddPersonalTrainer(PersonalTrainerModel personalTrainerModel)
        {
            _personalTrainerRepo.AddPersonalTrainerModel(personalTrainerModel);
        }

        public void DeletePersonalTrainer(int pid)
        {
            _personalTrainerRepo.DeletePersonalTrainerModel(pid);
        }

        // In case you guys need to update a personal trainer
        // create a method here that calls the UpdatePersonalTrainerModel method from the PersonalTrainerRepo +
        // create the UpdatePersonalTrainerModel method in the PersonalTrainerRepo
    }
}
