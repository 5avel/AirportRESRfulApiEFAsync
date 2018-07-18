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
    public class PlanesServiceTests
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Plane> repository;
        private IMapper mapper;
        private IValidator<PlaneDto> validator;

        public PlanesServiceTests()
        {
            unitOfWork = new FakeUnitOfWork<Plane>();

            validator = new PlanesValidator();

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Plane, PlaneDto>();
                cfg.CreateMap<PlaneDto, Plane>();
            }).CreateMapper();
        }

        [SetUp]
        public void Init()
        {

        }

        [TearDown]
        public void TestTearDown()
        {
            (unitOfWork as FakeUnitOfWork<Plane>).ClearData();
        }

        [Test]
        public void Make_Valid()
        {
            //arange
            PlanesService service = new PlanesService(unitOfWork, mapper, validator);

            var expected = new Plane
            { Id = 1, DepartureId = 1, PlaneTypeId = 1, Name = "dfg4456", ReleaseDate = new DateTime(1995, 1, 22) };

            var DtoToMake = new PlaneDto
            { Id = 1, DepartureId = 1, PlaneTypeId = 1, Name = "dfg4456", ReleaseDate = new DateTime(1995, 1, 22) };


            //act
            service.Make(DtoToMake);

            var actual = (unitOfWork.Set<Plane>() as FakeRpository<Plane>).createdItem;

            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.DepartureId, actual.DepartureId);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.ReleaseDate, actual.ReleaseDate);
        }

        [Test]
        public void Update_Valid()
        {
            //arange
            PlanesService service = new PlanesService(unitOfWork, mapper, validator);

            var expected = new Plane
            { Id = 1, DepartureId = 1, PlaneTypeId = 1, Name = "dfg4456", ReleaseDate = new DateTime(1995, 1, 22) };

            var DtoToMake = new PlaneDto
            { Id = 1, DepartureId = 1, PlaneTypeId = 1, Name = "dfg4456", ReleaseDate = new DateTime(1995, 1, 22) };


            //act
            service.Update(DtoToMake);

            var actual = (unitOfWork.Set<Plane>() as FakeRpository<Plane>).updatedItem;

            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.DepartureId, actual.DepartureId);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.ReleaseDate, actual.ReleaseDate);
        }

        [Test]
        public void Make_UnValid()
        {
            //arange
            PlanesService service = new PlanesService(unitOfWork, mapper, validator);


            var DtoToMake = new PlaneDto
            { Id = 1, DepartureId = 1, Name = "dfgdghdfhgdfghfdghdfhdfgh4456", ReleaseDate = new DateTime(1995, 1, 22) };


            //act
            service.Make(DtoToMake);

            var actual = (unitOfWork.Set<Plane>() as FakeRpository<Plane>).createdItem;

            //assert
            Assert.IsNull(actual);
        }

        [Test]
        public void Update_UnValid()
        {
            //arange
            PlanesService service = new PlanesService(unitOfWork, mapper, validator);

            var DtoToMake = new PlaneDto
            { Id = 1, DepartureId = default(int), Name = "", ReleaseDate = new DateTime(1995, 1, 22) };

            //act
            service.Update(DtoToMake);

            var actual = (unitOfWork.Set<Plane>() as FakeRpository<Plane>).updatedItem;

            //assert
            Assert.IsNull(actual);
        }
    }
}
