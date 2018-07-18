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
    public class PlaneTypesServiceTests
    {
        private IUnitOfWork unitOfWork;
        private IRepository<PlaneType> repository;
        private IMapper mapper;
        private IValidator<PlaneTypeDto> validator;

        public PlaneTypesServiceTests()
        {
            unitOfWork = new FakeUnitOfWork<PlaneType>();

            validator = new PlaneTypesValidator();

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PlaneType, PlaneTypeDto>();
                cfg.CreateMap<PlaneTypeDto, PlaneType>();
            }).CreateMapper();
        }

        [SetUp]
        public void Init()
        {

        }

        [TearDown]
        public void TestTearDown()
        {
            (unitOfWork as FakeUnitOfWork<PlaneType>).ClearData();
        }

        [Test]
        public void Make_Valid()
        {
            //arange
            PlaneTypesService service = new PlaneTypesService(unitOfWork, mapper, validator);

            var expected = new PlaneType
            { Id = 1, Model = "AN140", Capacity = 5000, Seats = 23, Range = 2345, ServiceLife = new TimeSpan(200, 0, 0, 0) };

            var DtoToMake = new PlaneTypeDto
            { Id = 1, Model = "AN140", Capacity = 5000, Seats = 23, Range = 2345, ServiceLife = new TimeSpan(200, 0, 0, 0) };


            //act
            service.Make(DtoToMake);

            var actual = (unitOfWork.Set<PlaneType>() as FakeRpository<PlaneType>).createdItem;

            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Capacity, actual.Capacity);
            Assert.AreEqual(expected.Model, actual.Model);
            Assert.AreEqual(expected.Range, actual.Range);
            Assert.AreEqual(expected.Seats, actual.Seats);
            Assert.AreEqual(expected.ServiceLife, actual.ServiceLife);
        }

        [Test]
        public void Update_Valid()
        {
            //arange
            PlaneTypesService service = new PlaneTypesService(unitOfWork, mapper, validator);

            var expected = new PlaneType
            { Id = 1, Model = "AN140", Capacity = 5000, Seats = 23, Range = 2345, ServiceLife = new TimeSpan(200, 0, 0, 0) };

            var DtoToMake = new PlaneTypeDto
            { Id = 1, Model = "AN140", Capacity = 5000, Seats = 23, Range = 2345, ServiceLife = new TimeSpan(200, 0, 0, 0) };


            //act
            service.Update(DtoToMake);

            var actual = (unitOfWork.Set<PlaneType>() as FakeRpository<PlaneType>).updatedItem;

            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Capacity, actual.Capacity);
            Assert.AreEqual(expected.Model, actual.Model);
            Assert.AreEqual(expected.Range, actual.Range);
            Assert.AreEqual(expected.Seats, actual.Seats);
            Assert.AreEqual(expected.ServiceLife, actual.ServiceLife);
        }

        [Test]
        public void Make_UnValid()
        {
            //arange
            PlaneTypesService service = new PlaneTypesService(unitOfWork, mapper, validator);


            var DtoToMake = new PlaneTypeDto
            { Id = 1, Model = "", Capacity = 5000, Seats = 23, Range = 2345, ServiceLife = new TimeSpan(200, 0, 0, 0) };


            //act
            service.Make(DtoToMake);

            var actual = (unitOfWork.Set<PlaneType>() as FakeRpository<PlaneType>).createdItem;

            //assert
            Assert.IsNull(actual);
        }

        [Test]
        public void Update_UnValid()
        {
            //arange
            PlaneTypesService service = new PlaneTypesService(unitOfWork, mapper, validator);

            var DtoToMake = new PlaneTypeDto
            { Id = 1, Model = "", Capacity = 5000, Seats = 23, Range = 2345, ServiceLife = new TimeSpan(200, 0, 0, 0) };

            //act
            service.Update(DtoToMake);

            var actual = (unitOfWork.Set<PlaneType>() as FakeRpository<PlaneType>).updatedItem;

            //assert
            Assert.IsNull(actual);
        }
    }
}
