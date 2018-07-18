using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.BLL.Tests.Fake;
using AirportRESRfulApi.Controllers;
using AirportRESRfulApi.Shared.DTO;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace AirportRESRfulApi.BLL.Tests.Controllers
{
    [TestFixture]
    public class PilotsControllerTests
    {
        private FakePilotsSrvice service;
        private PilotsController controller;
        private List<PilotDto> testList;
        public PilotsControllerTests()
        {
            service = new FakePilotsSrvice();
            controller = new PilotsController(service);
            testList = new List<PilotDto>();
            testList.AddRange(
                 new List<PilotDto>
                {
                    new PilotDto { Id = 1, CrewId = 1, FirstName = Guid.NewGuid().ToString(), LastName = "Ivanov", Experience = 3, Birthday = new DateTime(1987, 1, 24) },
                    new PilotDto { Id = 2, CrewId = 2, FirstName = Guid.NewGuid().ToString(), LastName = "Penhjd", Experience = 8, Birthday = new DateTime(1987, 1, 24) },
                    new PilotDto { Id = 3, CrewId = 3, FirstName = Guid.NewGuid().ToString(), LastName = "Maximov", Experience = 6, Birthday = new DateTime(1987, 1, 24) }
                });
        }

        [SetUp]
        public void Init()
        {
            service.Entitys.AddRange(testList);
               
        }

        [TearDown]
        public void TestTearDown()
        {
            service.Entitys.Clear();
        }

        [Test]
        public void Get()
        {
            IActionResult result = controller.Get();

            Assert.NotNull(result);
            Assert.AreEqual(((result as OkObjectResult).Value as List<PilotDto>).Count, testList.Count);
        }

        [Test]
        public void GetByID()
        {
            IActionResult result = controller.Get(1);

            Assert.NotNull(result);
            Assert.AreEqual(((result as OkObjectResult).Value as PilotDto).FirstName, testList.SingleOrDefault(x => x.Id == 1).FirstName);
        }

        [Test]
        public void Delete_Valid()
        {
            IActionResult result = controller.Delete(1);

            Assert.NotNull(result);
            Assert.AreEqual((result as OkObjectResult).StatusCode, 200);
        }

        [Test]
        public void Delete_InValid()
        {
            IActionResult result = controller.Delete(9);

            Assert.NotNull(result);
            Assert.AreEqual((result as OkObjectResult).StatusCode, 404);
        }
    }
}
