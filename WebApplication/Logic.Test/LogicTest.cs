using Moq;
using NuGet.Frameworks;
using NUnit.Framework;
using Models;
using Repos;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Linq;

namespace Logic.Test
{
    [TestFixture]
    public class LogicTest
    {
        Mock<IRepoBase<GymClient>> clientRepository;
        //Mock<IRepoBase<WorkoutDetail>> detailRepository;
        Mock<IRepoBase<ExtraInfo>> infoRepository;
        Mock<IRepoBase<Trainer>> trainerRepository;

        [SetUp]
        public void Setup()
        {
            //Mocking
            clientRepository = new Mock<IRepoBase<GymClient>>();
            //detailRepository = new Mock<IRepoBase<WorkoutDetail>>();
            infoRepository = new Mock<IRepoBase<ExtraInfo>>();
            trainerRepository = new Mock<IRepoBase<Trainer>>();

            List<GymClient> clients = new List<GymClient>();
            //List<WorkoutDetail> details = new List<WorkoutDetail>();
            List<ExtraInfo> infos = new List<ExtraInfo>();
            List<Trainer> trainers = new List<Trainer>();

            GymClient g1 = new GymClient()
            {
                GymID = "test00",
                Gender = Genders.Nő, FullName = "Hennyes ZereGacskó", TrainerID = "asd647",
                Verified = false, Age = 27, BeenWorkingOutFor = 0
            };
            GymClient g2 = new GymClient()
            {
                GymID = "test01",
                Gender = Genders.Férfi, FullName = "Komodoros Zacsinyalogato", TrainerID = "klo293",
                Verified = false, Age = 16, BeenWorkingOutFor = 2
            };
            GymClient g3 = new GymClient()
            {
                GymID = "test02",
                Gender = Genders.Férfi, FullName = "Arnold Zacsinegger", TrainerID = "huge99",
                Verified = true, Age = 33, BeenWorkingOutFor = 11
            };
            GymClient g4 = new GymClient()
            {
                GymID = "test03",
                Gender = Genders.Nő, FullName = "Pesti veszettmacska", TrainerID = "asd647",
                Verified = true, Age = 24, BeenWorkingOutFor = 5
            };
            GymClient g5 = new GymClient()
            {
                GymID = "test04",
                Gender = Genders.Férfi, FullName = "obudai kronikuskigyo", TrainerID = "asd647",
                Verified = false, Age = 12, BeenWorkingOutFor = 0
            };

            ExtraInfo i1 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Nem szeret lábazni" };
            ExtraInfo i2 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "váll problémái vannak" };
            ExtraInfo i3 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Nem szeret élni" };
            ExtraInfo i4 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Depressziós" };

            Trainer t1 = new Trainer() { TrainerName = "Edvás Erezacsi", TrainerID = Guid.NewGuid().ToString() };
            Trainer t2 = new Trainer() { TrainerName = "Zacsi Maszíro", TrainerID = Guid.NewGuid().ToString() };
            Trainer t3 = new Trainer() { TrainerName = "Medvés Hasnyálmokus", TrainerID = Guid.NewGuid().ToString() };
            /*
            WorkoutDetail d1 = new WorkoutDetail()
            {
                WorkoutId = "detail00",
                ContestDiets = ContestDiets.lowCarb,
                WorkoutType = WorkoutTypes.calisthenics
            };
            WorkoutDetail d2 = new WorkoutDetail()
            {
                WorkoutId = "detail01",
                ContestDiets = ContestDiets.intermittentFasting,
                WorkoutType = WorkoutTypes.powerlifting
            };
            WorkoutDetail d3 = new WorkoutDetail()
            {
                WorkoutId = "detail02",
                ContestDiets = ContestDiets.carbCycling,
                WorkoutType = WorkoutTypes.calisthenics
            };
            */
            //Adding them to their own lists
            clients.Add(g1);
            clients.Add(g2);
            clients.Add(g3);
            clients.Add(g4);
            clients.Add(g5);

            /*
            details.Add(d1);
            details.Add(d2);
            details.Add(d3);
            */

            infos.Add(i1);
            infos.Add(i2);
            infos.Add(i3);
            infos.Add(i4);

            trainers.Add(t1);
            trainers.Add(t2);
            trainers.Add(t3);


            clientRepository.Setup(x => x.Read()).Returns(clients.AsQueryable());
            //detailRepository.Setup(x => x.Read()).Returns(details.AsQueryable());
            infoRepository.Setup(x => x.Read()).Returns(infos.AsQueryable());
            trainerRepository.Setup(x => x.Read()).Returns(trainers.AsQueryable());

            //Ez a későbbi teszteléshez kell majd
            clientRepository.Setup(x => x.GetItem(It.IsAny<string>())).Returns(clients[1]);
            //detailRepository.Setup(x => x.GetItem(It.IsAny<string>())).Returns(details[2]);
        }

        #region 4(+1) CRUD method tests

        //Create
        [Test]
        public void TrainerAddMethod()
        {
            TrainerLogic trainerLogic = new TrainerLogic(trainerRepository.Object, clientRepository.Object);

            Trainer trainer = new Trainer { TrainerID = "sasfalkon", TrainerName = "Ron melléfoly" };
            trainerLogic.AddTrainer(trainer);

            trainerRepository.Verify( x => x.Add(trainer), Times.Once);
        }

        //Read
        [Test]
        public void ClientReadMethodComparingToStringMethods() //Itt a GetClientet tesztelem ami egy Read method
        {
            ClientLogic clientLogic = new ClientLogic(clientRepository.Object, infoRepository.Object);

            var gymClient = clientLogic.GetClient("test01");
            GymClient proposedClient = new GymClient() {
                GymID = "test01",
                Gender = Genders.Férfi,
                FullName = "Komodoros Zacsinyalogato",
                TrainerID = "klo293",
                Verified = false,
                Age = 16,
                BeenWorkingOutFor = 2
            };

            Assert.That(gymClient.ToString(), Is.EqualTo(proposedClient.ToString()));

            clientRepository.Verify(x => x.Read(), Times.Never);
            clientRepository.Verify(x => x.GetItem(It.IsAny<string>()), Times.Once);
        }

        //Update
        [Test]
        public void UpdateWorkoutDetailMethodTesting()
        {
            /*
            WorkoutDetailLogic workoutDetailLogic = new WorkoutDetailLogic(detailRepository.Object);

            WorkoutDetail wantedDetail = new WorkoutDetail { ContestDiets = ContestDiets.intermittentFasting, WorkoutType = WorkoutTypes.powerlifting };


            workoutDetailLogic.UpdateDetail("detail02", wantedDetail);
            detailRepository.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<WorkoutDetail>()), Times.Once);
            */
        }

        // Delete
        [Test]
        public void DeleteClientFromLogic()
        {
            ClientLogic clientLogic = new ClientLogic(clientRepository.Object, infoRepository.Object);
            Assert.That(clientLogic.GetClients().Count, Is.EqualTo(5));
            
            clientLogic.DeleteClient("test01");

            clientRepository.Verify(x => x.Delete(It.IsAny<string>()), Times.Once);
        }

        //2ndDelete
        [Test]
        public void DeleteInfoFromLogic()
        {
            ExtraInfoLogic extraInfoLogic = new ExtraInfoLogic(infoRepository.Object);
            Assert.That(extraInfoLogic.GetInfos().Count, Is.EqualTo(4));

            extraInfoLogic.DeleteInfo("i4");

            infoRepository.Verify(x => x.Delete(It.IsAny<string>()), Times.Once);
        }
        #endregion

        #region 3 Non-Crud methods
        /* nem nagyon tudtam rájönni lazyloadinggal ezt hogy kellett volna megoldani
        [Test]
        public void TestInfoWithLongestName()
        {
            ClientLogic clientLogic = new ClientLogic(clientRepository.Object);
            var something = infoRepository.Object.Read().ToList();
            foreach(var item in something)
            {
                clientLogic.AddInfoToClient(item, "test01");
            }
            
            string expectedValue = "váll problémái vannak";
            string proposedValue = clientLogic.LongestInfo();

            Assert.That(expectedValue, Is.EqualTo(proposedValue));

            clientRepository.Verify(x => x.Read(), Times.Once);
        }
        */


        #endregion
    }
}
