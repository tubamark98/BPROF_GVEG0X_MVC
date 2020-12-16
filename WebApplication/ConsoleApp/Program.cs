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

            TrainerLogic trainerLogic = new TrainerLogic(trainerRepo);
            ClientLogic clientLogic = new ClientLogic(clientRepo);

            trainerLogic.FillDbWithSamples();

            foreach (var item in trainerLogic.GetTrainers())
            {
                Console.WriteLine(item.TrainerName); 
                foreach(var client in item.GymClients)
                {
                    Console.WriteLine("Client: " + client.FullName);
                }
            }

            Console.ReadLine();
        }
    }
}
