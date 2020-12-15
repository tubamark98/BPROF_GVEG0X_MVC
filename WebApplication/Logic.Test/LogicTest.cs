using Moq;
using NUnit.Framework;
using Models;
using Repos;
using System;
using System.Collections.Generic;
using System.Text;

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

        }


    }
}
