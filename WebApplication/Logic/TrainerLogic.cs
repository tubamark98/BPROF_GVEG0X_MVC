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

        public float AverageAge()
        {
            int counter = 0;
            float value = 0;

            var something = GetTrainers().ToList();
            foreach(var item in something)
            {
                foreach(var client in item.GymClients)
                {
                    value += client.Age;
                    counter++;
                }
            }

            if (value == 0)
                return 0;
            else
                return value / counter;
        }

        public int[] GenderPercentage()
        {
            int[] something = new int[3];
            int counter = 0;
            for(int i=0;i<something.Length;i++)
            {
                something[i] = 0;
            }
            var helper = GetTrainers();

            foreach (var item in helper)
            {
                foreach (var client in item.GymClients)
                {
                    if(client.Gender == Genders.Nő)
                    {
                        something[0]++;
                    }
                    else if(client.Gender == Genders.Férfi)
                    {
                       something[1]++;
                    }
                    else
                    {
                        something[2]++;
                    }
                    counter++;
                }
            }

            for (int i = 0; i < something.Length; i++)
            {
                if(something[i] == 0)
                    something[i] = 0;
                else
                    something[i] = (something[i]*100)/counter;
            }

            return something;
        }

        public int AmountOfTrainers()
        {
            int counter = 0;
            var something = GetTrainers().ToList();
            foreach (var item in something)
            {
                counter++;
            }
            return counter;
        }

        public int AmountOfClients()
        {
            int counter = 0;
            var something = GetTrainers().ToList();
            foreach (var item in something)
            {
                foreach (var client in item.GymClients)
                {
                    counter++;
                }
            }
            return counter;
        }
        public int AmountOfExtraInfo()
        {
            int counter = 0;
            var something = GetTrainers().ToList();
            foreach (var item in something)
            {
                foreach (var client in item.GymClients)
                {
                    foreach (var info in client.ExtraInfos)
                        counter++;
                }
            }
            return counter;
        }
        public void AddClientToTrainer(GymClient gymClient, string trainerId)
        {
            GetTrainer(trainerId).GymClients.Add(gymClient);
            trainerRepo.Save();
        }

        public void RemoveClientFromTrainer(GymClient gymClient, string trainerId)
        {
            GetTrainer(trainerId).GymClients.Remove(gymClient);
            clientRepo.Delete(gymClient.GymID);
            trainerRepo.Save();
        }

        public void FillDbWithSamples()
        {
            Trainer t1 = new Trainer() { TrainerName = "Edvás Erezacsi" };
            Trainer t2 = new Trainer() { TrainerName = "Zacsi Maszíro" };
            Trainer t3 = new Trainer() { TrainerName = "Medvés Hasnyálmokus" };

            GymClient g0 = new GymClient()
            {
                Gender = Genders.Nő,
                FullName = "Macskás Réka",
                TrainerID = "asd647",
                Verified = false,
                Age = 27,
                BeenWorkingOutFor = 0
            };
            GymClient g1 = new GymClient()
            {
                Gender = Genders.Nő,
                FullName = "Negnyes Reheracskó",
                TrainerID = "asd647",
                Verified = false,
                Age = 27,
                BeenWorkingOutFor = 0
            };
            GymClient g2 = new GymClient()
            {
                Gender = Genders.Férfi,
                FullName = "Komodoros Zacsinyalogato",
                TrainerID = "klo293",
                Verified = false,
                Age = 16,
                BeenWorkingOutFor = 2
            };
            GymClient g3 = new GymClient()
            {
                Gender = Genders.Férfi,
                FullName = "Arnold Zacsinegger",
                TrainerID = "huge99",
                Verified = true,
                Age = 33,
                BeenWorkingOutFor = 11
            };
            GymClient g4 = new GymClient()
            {
                Gender = Genders.Nő,
                FullName = "Pesti veszettmacska",
                TrainerID = "asd647",
                Verified = true,
                Age = 24,
                BeenWorkingOutFor = 5
            };
            GymClient g5 = new GymClient()
            {
                Gender = Genders.Férfi,
                FullName = "obudai kronikuskigyo",
                TrainerID = "asd647",
                Verified = false,
                Age = 12,
                BeenWorkingOutFor = 0
            };
            GymClient g6 = new GymClient()
            {
                Gender = Genders.Helikopter,
                FullName = "Jay Duckler",
                TrainerID = "asd647",
                Verified = true,
                Age = 30,
                BeenWorkingOutFor = 10
            };
            GymClient g7 = new GymClient()
            {
                Gender = Genders.Férfi,
                FullName = "Ronnie Coalman",
                TrainerID = "asd647",
                Verified = true,
                Age = 35,
                BeenWorkingOutFor = 12
            };
            GymClient g8 = new GymClient()
            {
                Gender = Genders.Férfi,
                FullName = "Gizi mama",
                TrainerID = "asd647",
                Verified = false,
                Age = 67,
                BeenWorkingOutFor = 50
            };
            GymClient g9 = new GymClient()
            {
                Gender = Genders.Helikopter,
                FullName = "Raid: Shadow Legends",
                TrainerID = "asd647",
                Verified = true,
                Age = 2,
                BeenWorkingOutFor = 0
            };
            GymClient g10 = new GymClient()
            {
                Gender = Genders.Helikopter,
                FullName = "Felix shellbörg",
                TrainerID = "asd647",
                Verified = false,
                Age = 24,
                BeenWorkingOutFor = 2
            };
            GymClient g11 = new GymClient()
            {
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
