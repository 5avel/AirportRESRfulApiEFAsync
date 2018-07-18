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
    public class FlightsServiceTests
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Flight> repository;
        private IMapper mapper;
        private IValidator<FlightDto> validator;

        public FlightsServiceTests()
        {
            unitOfWork = new FakeUnitOfWork<Flight>();

            validator = new FlightsValidator();

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flight, FlightDto>();
                cfg.CreateMap<FlightDto, Flight>();
            }).CreateMapper();
        }


        [SetUp]
        public void Init()
        {
            
        }

        [TearDown]
        public void TestTearDown()
        {
            (unitOfWork as FakeUnitOfWork<Flight>).ClearData();
        }

        [Test]
        public void Make()
        {
            //arange
            FlightsService fs = new FlightsService(unitOfWork, mapper, validator);

            var expected = new Flight
            {
                Id = 1,
                FlightNumber = "QW11",
                DeparturePoint = "London",
                DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00"),
                DestinationPoint = "Ukraine",
                ArrivalTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") + TimeSpan.FromHours(5)
            };

            var fightDtoToMake = new FlightDto
            {
                Id = 1,
                FlightNumber = "QW11",
                DeparturePoint = "London",
                DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00"),
                DestinationPoint = "Ukraine",
                ArrivalTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") + TimeSpan.FromHours(5)
            };

            //act
            fs.Make(fightDtoToMake);

            var actual = (unitOfWork.Set<Flight>() as FakeRpository<Flight>).createdItem;

            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FlightNumber, actual.FlightNumber);
            Assert.AreEqual(expected.DestinationPoint, actual.DestinationPoint);
            Assert.AreEqual(expected.DepartureTime, actual.DepartureTime);
            Assert.AreEqual(expected.DestinationPoint, actual.DestinationPoint);
            Assert.AreEqual(expected.ArrivalTime, actual.ArrivalTime);

        }

        [Test]
        public void Update_OK()
        {
            //arange
            FlightsService fs = new FlightsService(unitOfWork, mapper, validator);

            var expected = new Flight
            {
                Id = 1,
                FlightNumber = "QW11",
                DeparturePoint = "London",
                DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00"),
                DestinationPoint = "Ukraine",
                ArrivalTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") + TimeSpan.FromHours(5)
            };

            var fightDtoToTest = new FlightDto
            {
                Id = 1,FlightNumber = "QW11", DeparturePoint = "London",
                DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00"),
                DestinationPoint = "Ukraine",
                ArrivalTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") + TimeSpan.FromHours(5)
            };

            //act
            fs.Update(fightDtoToTest);

            var actual = (unitOfWork.Set<Flight>() as FakeRpository<Flight>).updatedItem;

            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FlightNumber, actual.FlightNumber);
            Assert.AreEqual(expected.DestinationPoint, actual.DestinationPoint);
            Assert.AreEqual(expected.DepartureTime, actual.DepartureTime);
            Assert.AreEqual(expected.DestinationPoint, actual.DestinationPoint);
            Assert.AreEqual(expected.ArrivalTime, actual.ArrivalTime);
        }

        [Test]
        public void Update_Bed()
        {
            //arange
            FlightsService fs = new FlightsService(unitOfWork, mapper, validator);

            var expected = new Flight
            {
                Id = 1,
                FlightNumber = "QW11",
                DeparturePoint = "London",
                DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00"),
                DestinationPoint = "Ukraine",
                ArrivalTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") + TimeSpan.FromHours(5)
            };

            var fightDtoToTest = new FlightDto
            {
                Id = 1,
                FlightNumber = "QW",
                DeparturePoint = "London",
                DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00"),
                DestinationPoint = "Ukraine",
                ArrivalTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") + TimeSpan.FromHours(5)
            };

            //act
            fs.Update(fightDtoToTest);

            var actual = (unitOfWork.Set<Flight>() as FakeRpository<Flight>).updatedItem;

            //assert

            Assert.IsNull(actual);
        }
    }


}
