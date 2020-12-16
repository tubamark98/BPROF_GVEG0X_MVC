using Moq;
using NUnit.Framework;
using Models;
using Repos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Logic.Test
{
    [TestFixture]
    public class LogicTest
    {
        Mock<ClientRepository> clientRepository;
        Mock<DetailRepository> detailRepository;
        Mock<InfoRepository> infoRepository;
        Mock<TrainerRepository> trainerRepository;

        [SetUp]
        public void Initialize()
        {
            //Mocking
            clientRepository = new Mock<ClientRepository>();
            detailRepository = new Mock<DetailRepository>();
            infoRepository = new Mock<InfoRepository>();
            trainerRepository = new Mock<TrainerRepository>();

            List<GymClient> clients = new List<GymClient>();
            List<WorkoutDetail> details = new List<WorkoutDetail>();
            List<ExtraInfo> infos = new List<ExtraInfo>();
            List<Trainer> trainers = new List<Trainer>();

            GymClient g1 = new GymClient()
            {
                GymID = Guid.NewGuid().ToString(), Gender = Genders.Nő, FullName = "Gennyes Herezacskó", TrainerID = "asd647",
                Verified = false, Age = 27, BeenWorkingOutFor = 0
            };
            GymClient g2 = new GymClient()
            {
                GymID = Guid.NewGuid().ToString(), Gender = Genders.Férfi, FullName = "Komodoros Zacsinyalogato", TrainerID = "klo293",
                Verified = false, Age = 16, BeenWorkingOutFor = 2
            };
            GymClient g3 = new GymClient()
            {
                GymID = Guid.NewGuid().ToString(), Gender = Genders.Férfi, FullName = "Arnold Zacsinegger", TrainerID = "huge99",
                Verified = true, Age = 33, BeenWorkingOutFor = 11
            };
            GymClient g4 = new GymClient()
            {
                GymID = Guid.NewGuid().ToString(), Gender = Genders.Nő, FullName = "Pesti veszettmacska", TrainerID = "asd647",
                Verified = true, Age = 24, BeenWorkingOutFor = 5
            };
            GymClient g5 = new GymClient()
            {
                GymID = Guid.NewGuid().ToString(), Gender = Genders.Férfi, FullName = "obudai kronikuskigyo", TrainerID = "asd647",
                Verified = false, Age = 12, BeenWorkingOutFor = 0
            };

            ExtraInfo i1 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Nem szeret lábazni" };
            ExtraInfo i2 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "váll problémái vannak" };
            ExtraInfo i3 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Nem szeret lábazni" };
            ExtraInfo i4 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Nem szeret lábazni" };

            Trainer t1 = new Trainer() { TrainerName = "Edvás Erezacsi", TrainerID = Guid.NewGuid().ToString() };
            Trainer t2 = new Trainer() { TrainerName = "Zacsi Maszíro", TrainerID = Guid.NewGuid().ToString() };
            Trainer t3 = new Trainer() { TrainerName = "Medvés Hasnyálmokus", TrainerID = Guid.NewGuid().ToString() };

            WorkoutDetail d1 = new WorkoutDetail()
            {
                WorkoutId = Guid.NewGuid().ToString(),
                ContestDiets = ContestDiets.lowCarb,
                WorkoutType = WorkoutTypes.calisthenics
            };
            WorkoutDetail d2 = new WorkoutDetail()
            {
                WorkoutId = Guid.NewGuid().ToString(),
                ContestDiets = ContestDiets.intermittentFasting,
                WorkoutType = WorkoutTypes.powerlifting
            };
            WorkoutDetail d3 = new WorkoutDetail()
            {
                WorkoutId = Guid.NewGuid().ToString(),
                ContestDiets = ContestDiets.carbCycling,
                WorkoutType = WorkoutTypes.calisthenics
            };

            //Adding them to their own lists
            clients.Add(g1);
            clients.Add(g2);
            clients.Add(g3);
            clients.Add(g4);
            clients.Add(g5);

            details.Add(d1);
            details.Add(d2);
            details.Add(d3);

            infos.Add(i1);
            infos.Add(i2);
            infos.Add(i3);
            infos.Add(i4);

            trainers.Add(t1);
            trainers.Add(t2);
            trainers.Add(t3);

            clientRepository.Setup(x => x.Read()).Returns(clients.AsQueryable());
            detailRepository.Setup(x => x.Read()).Returns(details.AsQueryable());
            infoRepository.Setup(x => x.Read()).Returns(infos.AsQueryable());
            trainerRepository.Setup(x => x.Read()).Returns(trainers.AsQueryable());


        }

    }
}
