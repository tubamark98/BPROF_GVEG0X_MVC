using Models;
using Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class TrainerLogic
    {
        IRepoBase<GymClient> clientRepo;
        IRepoBase<Trainer> trainerRepo;

        public TrainerLogic(IRepoBase<GymClient> clientRepo, IRepoBase<Trainer> trainerRepo)
        {
            this.clientRepo = clientRepo;
            this.trainerRepo = trainerRepo;
        }


        #region CRUD methods
        public void AddTrainer(Trainer trainer)
        {
            this.trainerRepo.Add(trainer);
        }

        public void DeleteTrainer(string trainerId)
        {
            this.trainerRepo.Delete(trainerId);
        }
        public IQueryable<Trainer> GetTrainers()
        {
            return trainerRepo.Read();
        }
        public Trainer GetTrainer(string trainerId)
        {
            return trainerRepo.Read(trainerId);
        }
        public void UpdateTrainer(string trainerId, Trainer newTrainer)
        {
            trainerRepo.Update(trainerId, newTrainer);
        }

        #endregion

        #region Non-CRUD methods

        public void AddClientToTrainer(GymClient gymClient, string trainerId)
        {
            GetTrainer(trainerId).GymClients.Add(gymClient);
            trainerRepo.Save();
        }

        public void RemoveClientFromTrainer(GymClient gymClient, string trainerId)
        {
            GetTrainer(trainerId).GymClients.Remove(gymClient);
            trainerRepo.Save();
        }

        public void FillDbWithSamples()
        {
            Trainer t1 = new Trainer() { TrainerName = "Edvás Erezacsi", TrainerID = Guid.NewGuid().ToString()};
            Trainer t2 = new Trainer() { TrainerName = "Zacsi Maszíro", TrainerID = Guid.NewGuid().ToString() };
            Trainer t3 = new Trainer() { TrainerName = "Medvés Hasnyálmokus", TrainerID = Guid.NewGuid().ToString() };

            GymClient g1 = new GymClient()
            {
                GymID = Guid.NewGuid().ToString(),
                Gender = Genders.Nő,
                FullName = "Gennyes Herezacskó",
                TrainerID = "asd647",
                Verified = false,
                Age = 27,
                BeenWorkingOutFor = 0
            };
            GymClient g2 = new GymClient()
            {
                GymID = Guid.NewGuid().ToString(),
                Gender = Genders.Férfi,
                FullName = "Komodoros Zacsinyalogato",
                TrainerID = "klo293",
                Verified = false,
                Age = 16,
                BeenWorkingOutFor = 2
            };
            GymClient g3 = new GymClient()
            {
                GymID = Guid.NewGuid().ToString(),
                Gender = Genders.Férfi,
                FullName = "Arnold Zacsinegger",
                TrainerID = "huge99",
                Verified = true,
                Age = 33,
                BeenWorkingOutFor = 11
            };
            GymClient g4 = new GymClient()
            {
                GymID = Guid.NewGuid().ToString(),
                Gender = Genders.Nő,
                FullName = "Pesti veszettmacska",
                TrainerID = "asd647",
                Verified = true,
                Age = 24,
                BeenWorkingOutFor = 5
            };
            GymClient g5 = new GymClient()
            {
                GymID = Guid.NewGuid().ToString(),
                Gender = Genders.Férfi,
                FullName = "obudai kronikuskigyo",
                TrainerID = "asd647",
                Verified = false,
                Age = 12,
                BeenWorkingOutFor = 0
            };

            AddTrainer(t1);
            AddTrainer(t2);
            AddTrainer(t3);

            AddClientToTrainer(g1, t1.TrainerID);
            AddClientToTrainer(g2, t2.TrainerID);
            AddClientToTrainer(g3, t3.TrainerID);
            AddClientToTrainer(g4, t3.TrainerID);
            AddClientToTrainer(g5, t2.TrainerID);

        }
        #endregion
    }
}
