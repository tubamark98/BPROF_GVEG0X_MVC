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
        readonly IRepoBase<Trainer> trainerRepo;
        readonly IRepoBase<GymClient> clientRepo;
        //IRepoBase<WorkoutDetail> detailRepo;

        public TrainerLogic(IRepoBase<Trainer> trainerRepo, IRepoBase<GymClient> clientRepo)
        {
            this.trainerRepo = trainerRepo;
            this.clientRepo = clientRepo;
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
            return trainerRepo.GetItem(trainerId);
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

            GymClient g0 = new GymClient()
            {
                GymID = Guid.NewGuid().ToString(),
                Gender = Genders.Nő,
                FullName = "Macskás Réka",
                TrainerID = "asd647",
                Verified = false,
                Age = 27,
                BeenWorkingOutFor = 0
            };
            GymClient g1 = new GymClient()
            {
                GymID = Guid.NewGuid().ToString(),
                Gender = Genders.Nő,
                FullName = "Negnyes Reheracskó",
                TrainerID = "asd647",
                Verified = false,
                Age = 27,
                BeenWorkingOutFor = 0
            };
            GymClient g2 = new GymClient()
            {
                GymID = "test01",
                Gender = Genders.Férfi,
                FullName = "Komodoros Zacsinyalogato",
                TrainerID = "klo293",
                Verified = false,
                Age = 16,
                BeenWorkingOutFor = 2
            };
            GymClient g3 = new GymClient()
            {
                GymID = "test02",
                Gender = Genders.Férfi,
                FullName = "Arnold Zacsinegger",
                TrainerID = "huge99",
                Verified = true,
                Age = 33,
                BeenWorkingOutFor = 11
            };
            GymClient g4 = new GymClient()
            {
                GymID = "test03",
                Gender = Genders.Nő,
                FullName = "Pesti veszettmacska",
                TrainerID = "asd647",
                Verified = true,
                Age = 24,
                BeenWorkingOutFor = 5
            };
            GymClient g5 = new GymClient()
            {
                GymID = "test04",
                Gender = Genders.Férfi,
                FullName = "obudai kronikuskigyo",
                TrainerID = "asd647",
                Verified = false,
                Age = 12,
                BeenWorkingOutFor = 0
            };
            GymClient g6 = new GymClient()
            {
                GymID = "test05",
                Gender = Genders.Helikopter,
                FullName = "Jay Duckler",
                TrainerID = "asd647",
                Verified = true,
                Age = 30,
                BeenWorkingOutFor = 10
            };
            GymClient g7 = new GymClient()
            {
                GymID = "test06",
                Gender = Genders.Férfi,
                FullName = "Ronnie Coalman",
                TrainerID = "asd647",
                Verified = true,
                Age = 35,
                BeenWorkingOutFor = 12
            };
            GymClient g8 = new GymClient()
            {
                GymID = "test07",
                Gender = Genders.Férfi,
                FullName = "Gizi mama",
                TrainerID = "asd647",
                Verified = false,
                Age = 67,
                BeenWorkingOutFor = 50
            };
            GymClient g9 = new GymClient()
            {
                GymID = "test08",
                Gender = Genders.Helikopter,
                FullName = "Raid: Shadow Legends",
                TrainerID = "asd647",
                Verified = true,
                Age = 2,
                BeenWorkingOutFor = 0
            };
            GymClient g10 = new GymClient()
            {
                GymID = "test09",
                Gender = Genders.Helikopter,
                FullName = "Felix shellbörg",
                TrainerID = "asd647",
                Verified = false,
                Age = 24,
                BeenWorkingOutFor = 2
            };
            GymClient g11 = new GymClient()
            {
                GymID = "test10",
                Gender = Genders.Helikopter,
                FullName = "Fill Heath",
                TrainerID = "asd647",
                Verified = true,
                Age = 17,
                BeenWorkingOutFor = 2
            };
            AddTrainer(t1);
            AddTrainer(t2);
            AddTrainer(t3);

            AddClientToTrainer(g0, t1.TrainerID);
            AddClientToTrainer(g1, t1.TrainerID);
            AddClientToTrainer(g2, t2.TrainerID);
            AddClientToTrainer(g3, t3.TrainerID);
            AddClientToTrainer(g4, t3.TrainerID);
            AddClientToTrainer(g5, t2.TrainerID);
            AddClientToTrainer(g6, t2.TrainerID);
            AddClientToTrainer(g7, t1.TrainerID);
            AddClientToTrainer(g8, t1.TrainerID);
            AddClientToTrainer(g9, t2.TrainerID);
            AddClientToTrainer(g10, t3.TrainerID);
            AddClientToTrainer(g11, t1.TrainerID);

        }
        #endregion
    }
}
