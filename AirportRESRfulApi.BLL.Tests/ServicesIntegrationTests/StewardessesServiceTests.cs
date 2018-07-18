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
using System.Threading;

namespace AirportRESRfulApi.BLL.Tests.ServicesIntegrationTests
{
    [TestFixture]
    public class StewardessesServiceTests
    {
        private IUnitOfWork unitOfWork;
        private AirportContext context;
        private IRepository<Stewardess> repository;
        private IMapper mapper;
        private IValidator<StewardessDto> validator;
        private IStewardessesService service;
        public StewardessesServiceTests()
        {
            context = new AirportContext(
                    new DbContextOptions<AirportContext>(), null);
            unitOfWork = new UnitOfWork(context);
            repository = unitOfWork.Set<Stewardess>();
            validator = new StewardessesValidator();

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Stewardess, StewardessDto>();
                cfg.CreateMap<StewardessDto, Stewardess>();
            }).CreateMapper();

            service = new StewardessesService(unitOfWork, mapper, validator);
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
            var entity = new StewardessDto
            {
                Id = 1,
                CrewId = 1,
                FirstName = Guid.NewGuid().ToString(),
                LastName = "Ivanova",
                Birthday = new DateTime(1987, 1, 24)
            };
            //act
            service.Update(entity);

            var result = service.GetById(1);
            //assert
            Assert.NotNull(result);
            Assert.AreEqual(result.FirstName, entity.FirstName);
        }

        [Test]
        public void Make_Valid()
        {
            //arange
            var entity = new StewardessDto
            {
                CrewId = 3,
                FirstName = Guid.NewGuid().ToString(),
                LastName = "Ivanov",
                Birthday = new DateTime(1987, 1, 24)
            };
            //act
            service.Make(entity);

            var result = service.Get()
                .ToList()
                .SingleOrDefault(x => x.FirstName == entity.FirstName);
            //assert
            Assert.NotNull(result);
            Assert.AreEqual(result.FirstName, entity.FirstName);

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
