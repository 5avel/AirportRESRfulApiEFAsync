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
    class StewardessesServiceTests
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Stewardess> repository;
        private IMapper mapper;
        private IValidator<StewardessDto> validator;

        public StewardessesServiceTests()
        {
            unitOfWork = new FakeUnitOfWork<Stewardess>();

            validator = new StewardessesValidator();

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Stewardess, StewardessDto>();
                cfg.CreateMap<StewardessDto, Stewardess>();
            }).CreateMapper();
        }

        [SetUp]
        public void Init()
        {

        }

        [TearDown]
        public void TestTearDown()
        {
            (unitOfWork as FakeUnitOfWork<Stewardess>).ClearData();
        }

        [Test]
        public void Make_Valid()
        {
            //arange
            StewardessesService service = new StewardessesService(unitOfWork, mapper, validator);

            var expected = new Stewardess
            { Id = 1, CrewId = 1, FirstName = "Ivan", LastName = "Ivanov", Birthday = new DateTime(1987, 1, 24) };

            var DtoToMake = new StewardessDto
            { Id = 1, CrewId = 1, FirstName = "Ivan", LastName = "Ivanov", Birthday = new DateTime(1987, 1, 24) };


            //act
            service.Make(DtoToMake);

            var actual = (unitOfWork.Set<Stewardess>() as FakeRpository<Stewardess>).createdItem;

            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.CrewId, actual.CrewId);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Birthday, actual.Birthday);
        }

        [Test]
        public void Update_Valid()
        {
            //arange
            StewardessesService service = new StewardessesService(unitOfWork, mapper, validator);

            var expected = new Stewardess
            { Id = 1, CrewId = 1, FirstName = "Ivan", LastName = "Ivanov", Birthday = new DateTime(1987, 1, 24) };

            var DtoToMake = new StewardessDto
            { Id = 1, CrewId = 1, FirstName = "Ivan", LastName = "Ivanov", Birthday = new DateTime(1987, 1, 24) };


            //act
            service.Update(DtoToMake);

            var actual = (unitOfWork.Set<Stewardess>() as FakeRpository<Stewardess>).updatedItem;

            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.CrewId, actual.CrewId);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Birthday, actual.Birthday);
        }

        [Test]
        public void Make_UnValid()
        {
            //arange
            StewardessesService service = new StewardessesService(unitOfWork, mapper, validator);


            var DtoToMake = new StewardessDto
            { Id = 1, CrewId = 1, FirstName = "Iv", LastName = "Iv", Birthday = new DateTime(1987, 1, 24) };


            //act
            service.Make(DtoToMake);

            var actual = (unitOfWork.Set<Stewardess>() as FakeRpository<Stewardess>).createdItem;

            //assert
            Assert.IsNull(actual);
        }

        [Test]
        public void Update_UnValid()
        {
            //arange
            StewardessesService service = new StewardessesService(unitOfWork, mapper, validator);

            var DtoToMake = new StewardessDto
            { Id = 1, CrewId = 1, FirstName = "an", LastName = "ov",  Birthday = new DateTime(1987, 1, 24) };

            //act
            service.Update(DtoToMake);

            var actual = (unitOfWork.Set<Stewardess>() as FakeRpository<Stewardess>).updatedItem;

            //assert
            Assert.IsNull(actual);
        }
    }
}
