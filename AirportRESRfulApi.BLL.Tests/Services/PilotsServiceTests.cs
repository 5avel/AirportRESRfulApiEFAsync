using AirportRESRfulApi.BLL.Services;
using AirportRESRfulApi.BLL.Tests.Fake;
using AirportRESRfulApi.BLL.Validations;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;
using NUnit.Framework;
using System;

namespace AirportRESRfulApi.BLL.Tests.Services
{
    [TestFixture]
    public class PilotsServiceTests
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Pilot> repository;
        private IMapper mapper;
        private IValidator<PilotDto> validator;

        public PilotsServiceTests()
        {
            unitOfWork = new FakeUnitOfWork<Pilot>();

            validator = new PilotsValidator();

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Pilot, PilotDto>();
                cfg.CreateMap<PilotDto, Pilot>();
            }).CreateMapper();
        }

        [SetUp]
        public void Init()
        {

        }

        [TearDown]
        public void TestTearDown()
        {
            (unitOfWork as FakeUnitOfWork<Pilot>).ClearData();
        }

        [Test]
        public void Make_Valid()
        {
            //arange
            PilotsService service = new PilotsService(unitOfWork, mapper, validator);

            var expected = new Pilot
            { Id = 1, CrewId = 1, FirstName = "Ivan", LastName = "Ivanov", Experience = 3, Birthday = new DateTime(1987, 1, 24) };

            var DtoToMake = new PilotDto
            { Id = 1, CrewId = 1, FirstName = "Ivan", LastName = "Ivanov", Experience = 3, Birthday = new DateTime(1987, 1, 24) };


            //act
            service.Make(DtoToMake);

            var actual = (unitOfWork.Set<Pilot>() as FakeRpository<Pilot>).createdItem;

            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.CrewId, actual.CrewId);
            Assert.AreEqual(expected.Experience, actual.Experience);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Birthday, actual.Birthday);
        }

        [Test]
        public void Update_Valid()
        {
            //arange
            PilotsService service = new PilotsService(unitOfWork, mapper, validator);

            var expected = new Pilot
            { Id = 1, CrewId = 1, FirstName = "Ivan", LastName = "Ivanov", Experience = 3, Birthday = new DateTime(1987, 1, 24) };

            var DtoToMake = new PilotDto
            { Id = 1, CrewId = 1, FirstName = "Ivan", LastName = "Ivanov", Experience = 3, Birthday = new DateTime(1987, 1, 24) };


            //act
            service.Update(DtoToMake);

            var actual = (unitOfWork.Set<Pilot>() as FakeRpository<Pilot>).updatedItem;

            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.CrewId, actual.CrewId);
            Assert.AreEqual(expected.Experience, actual.Experience);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Birthday, actual.Birthday);
        }

        [Test]
        public void Make_UnValid()
        {
            //arange
            PilotsService service = new PilotsService(unitOfWork, mapper, validator);


            var DtoToMake = new PilotDto
            { Id = 1, CrewId = 1, FirstName = "Iv", LastName = "Iv", Experience = 3, Birthday = new DateTime(1987, 1, 24) };


            //act
            service.Make(DtoToMake);

            var actual = (unitOfWork.Set<Pilot>() as FakeRpository<Pilot>).createdItem;

            //assert
            Assert.IsNull(actual);
        }

        [Test]
        public void Update_UnValid()
        {
            //arange
            PilotsService service = new PilotsService(unitOfWork, mapper, validator);

            var DtoToMake = new PilotDto
            { Id = 1, CrewId = 1, FirstName = "an", LastName = "ov", Experience = 3, Birthday = new DateTime(1987, 1, 24) };

            //act
            service.Update(DtoToMake);

            var actual = (unitOfWork.Set<Pilot>() as FakeRpository<Pilot>).updatedItem;

            //assert
            Assert.IsNull(actual);
        }
    }
}
