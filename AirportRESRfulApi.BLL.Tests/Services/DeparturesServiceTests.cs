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
    public class DeparturesServiceTests
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Departure> repository;
        private IMapper mapper;
        private IValidator<DepartureDto> validator;

        public DeparturesServiceTests()
        {
            unitOfWork = new FakeUnitOfWork<Departure>();

            validator = new DeparturesValidator();

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Departure, DepartureDto>();
                cfg.CreateMap<DepartureDto, Departure>();
            }).CreateMapper();
        }

        [SetUp]
        public void Init()
        {

        }

        [TearDown]
        public void TestTearDown()
        {
            (unitOfWork as FakeUnitOfWork<Departure>).ClearData();
        }

        [Test]
        public void Make_Valid()
        {
            //arange
            DeparturesService service = new DeparturesService(unitOfWork, mapper, validator);

            var expected = new Departure
            { Id = 1, FlightId = 1, FlightNumber = "QW11", DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") };

            var DtoToMake = new DepartureDto
            { Id = 1, FlightId = 1, FlightNumber = "QW11", DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") };


            //act
            service.Make(DtoToMake);

            var actual = (unitOfWork.Set<Departure>() as FakeRpository<Departure>).createdItem;

            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FlightId, actual.FlightId);
            Assert.AreEqual(expected.FlightNumber, actual.FlightNumber);
            Assert.AreEqual(expected.DepartureTime, actual.DepartureTime);
        }

        [Test]
        public void Update_Valid()
        {
            //arange
            DeparturesService service = new DeparturesService(unitOfWork, mapper, validator);

            var expected = new Departure
            { Id = 1, FlightId = 1, FlightNumber = "QW11", DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") };

            var DtoToMake = new DepartureDto
            { Id = 1, FlightId = 1, FlightNumber = "QW11", DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") };


            //act
            service.Update(DtoToMake);

            var actual = (unitOfWork.Set<Departure>() as FakeRpository<Departure>).updatedItem;

            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FlightId, actual.FlightId);
            Assert.AreEqual(expected.FlightNumber, actual.FlightNumber);
            Assert.AreEqual(expected.DepartureTime, actual.DepartureTime);
        }

        [Test]
        public void Make_UnValid()
        {
            //arange
            DeparturesService service = new DeparturesService(unitOfWork, mapper, validator);


            var DtoToMake = new DepartureDto
            { Id = 1, FlightId = 1, FlightNumber = "QW", DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") };


            //act
            service.Make(DtoToMake);

            var actual = (unitOfWork.Set<Departure>() as FakeRpository<Departure>).createdItem;

            //assert
            Assert.IsNull(actual);
        }

        [Test]
        public void Update_UnValid()
        {
            //arange
            DeparturesService service = new DeparturesService(unitOfWork, mapper, validator);

            var DtoToMake = new DepartureDto
            { Id = 1, FlightId = 1, FlightNumber = "1", DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") };

            //act
            service.Update(DtoToMake);

            var actual = (unitOfWork.Set<Departure>() as FakeRpository<Departure>).updatedItem;

            //assert
            Assert.IsNull(actual);
        }
    }
}
