using AirportRESRfulApi.BLL.Interfaces;
using AirportRESRfulApi.BLL.Services;
using AirportRESRfulApi.BLL.Validations;
using AirportRESRfulApi.DAL;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace AirportRESRfulApi.BLL.Tests.ServicesIntegrationTests
{
    [TestFixture]
    public class PlanesServiceTests
    {
        private IUnitOfWork unitOfWork;
        private AirportContext context;
        private IRepository<Plane> repository;
        private IMapper mapper;
        private IValidator<PlaneDto> validator;
        private IPlanesService service;
        public PlanesServiceTests()
        {
            context = new AirportContext(
                    new DbContextOptions<AirportContext>(), null);
            unitOfWork = new UnitOfWork(context);
            repository = unitOfWork.Set<Plane>();
            validator = new PlanesValidator();

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Plane, PlaneDto>();
                cfg.CreateMap<PlaneDto, Plane>();
            }).CreateMapper();

            service = new PlanesService(unitOfWork, mapper, validator);
        }

        [SetUp]
        public void Init()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [TearDown]
        public void TestTearDown()
        {

        }

        [Test]
        public void Update_Valid()
        {
            //arange
            var entity = new PlaneDto
            {
                Id = 1,
                DepartureId = 1,
                PlaneTypeId = 1,
                Name = "432432",
                ReleaseDate = new DateTime(1995, 1, 22)
            };
            //act
            service.Update(entity);

            var result = service.GetById(1);
            //assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Name, entity.Name);
        }

        [Test]
        public void Make_Valid()
        {
            //arange
            var entity = new PlaneDto
            {
               
                DepartureId = 3,
                PlaneTypeId = 3,
                Name = "456456",
                ReleaseDate = new DateTime(1995, 1, 22)
            };
            //act
            service.Make(entity);

            var result = service.Get()
                .ToList()
                .SingleOrDefault(x => x.Name == entity.Name);
            //assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Name, entity.Name);

        }

       

        [Test]
        public void Delete_Valid()
        {
            //arange

            //act
            service.Delete(1);

            var result = service.GetById(1);
                
            //assert
            Assert.Null(result);
            
        }

        [Test]
        public void Delete_UnValid()
        {
            //arange

            //act
            service.Delete(5);

            var result = service.GetById(5);

            //assert
            Assert.Null(result);
        }
    }
}
