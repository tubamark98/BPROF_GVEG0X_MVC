using Repos;
using Models;
using Logic;
using Data;
using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TrainerRepository trainerRepo = new TrainerRepository();
            ClientRepository clientRepo = new ClientRepository();

            TrainerLogic trainerLogic = new TrainerLogic(clientRepo, trainerRepo);
            ClientLogic clientLogic = new ClientLogic(clientRepo, trainerRepo);

            Trainer trainer = new Trainer();
            trainer.TrainerID = Guid.NewGuid().ToString();
            trainer.TrainerName = "Gyuri anyabaszó";

            trainerLogic.AddTrainer(trainer);

            var id = trainerLogic.GetTrainers().First().TrainerID;

            GymClient newClient = new GymClient();
            newClient.GymID = Guid.NewGuid().ToString();
            newClient.Gender = Genders.Nő;
            newClient.FullName = "Gennyes Herezacskó";
            newClient.TrainerID = "asd647";
            newClient.Verified = false;
            newClient.Age = 27;
            newClient.BeenWorkingOutFor = 0;


            trainerLogic.AddClientToTrainer(newClient, id);

            foreach (var item in trainerLogic.GetTrainers())
            {
                Console.WriteLine(item.TrainerName);
                foreach(var client in item.GymClients)
                {
                    Console.WriteLine(client.FullName);
                }
            }

            Console.ReadLine();
        }
    }
}
