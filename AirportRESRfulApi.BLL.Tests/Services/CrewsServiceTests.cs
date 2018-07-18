using AirportRESRfulApi.BLL.Services;
using AirportRESRfulApi.BLL.Tests.Fake;
using AirportRESRfulApi.BLL.Validations;
using AirportRESRfulApi.DAL.Interfaces;
using AirportRESRfulApi.DAL.Models;
using AirportRESRfulApi.Shared.DTO;
using AutoMapper;
using FluentValidation;
using NUnit.Framework;


namespace AirportRESRfulApi.BLL.Tests.Services
{
    [TestFixture]
    public class CrewsServiceTests
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Crew> repository;
        private IMapper mapper;
        private IValidator<CrewDto> validator;

        public CrewsServiceTests()
        {
            unitOfWork = new FakeUnitOfWork<Crew>();

            validator = new CrewsValidator();

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Crew, CrewDto>();
                cfg.CreateMap<CrewDto, Crew>();
            }).CreateMapper();
        }
        [SetUp]
        public void Init()
        {

        }

        [TearDown]
        public void TestTearDown()
        {
            (unitOfWork as FakeUnitOfWork<Crew>).ClearData();
        }

        [Test]
        public void Make_Valid()
        {
            //arange
            CrewsService fs = new CrewsService(unitOfWork, mapper, validator);

            var expected = new Crew
            {
                Id = 1,
                DepartureId = 1
            };

            var DtoToMake = new CrewDto
            {
                Id = 1,
                DepartureId = 1
            };

            //act
            fs.MakeAsync(DtoToMake);

            var actual = (unitOfWork.Set<Crew>() as FakeRpository<Crew>).createdItem;

            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.DepartureId, actual.DepartureId);
        }

        [Test]
        public void Make_UnValide()
        {
            //arange
            CrewsService fs = new CrewsService(unitOfWork, mapper, validator);

            var expected = new Crew
            {
                Id = 1,
                DepartureId = default(int)
            };

            var DtoToMake = new CrewDto
            {
                Id = 1,
                DepartureId = default(int)
            };

            //act
            fs.MakeAsync(DtoToMake);

            var actual = (unitOfWork.Set<Crew>() as FakeRpository<Crew>).createdItem;

            //assert
            Assert.IsNull(actual);
        }

        [Test]
        public void Update_Valid()
        {
            //arange
            CrewsService fs = new CrewsService(unitOfWork, mapper, validator);

            var expected = new Crew
            {
                Id = 1,
                DepartureId = 1
            };

            var DtoToMake = new CrewDto
            {
                Id = 1,
                DepartureId = 1
            };

            //act
            fs.UpdateAsync(DtoToMake);

            var actual = (unitOfWork.Set<Crew>() as FakeRpository<Crew>).updatedItem;

            //assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.DepartureId, actual.DepartureId);
        }

        [Test]
        public void Update_UnValide()
        {
            //arange
            CrewsService fs = new CrewsService(unitOfWork, mapper, validator);

            var expected = new Crew
            {
                Id = 1,
                DepartureId = default(int)
            };

            var DtoToMake = new CrewDto
            {
                Id = 1,
                DepartureId = default(int)
            };

            //act
            fs.UpdateAsync(DtoToMake);

            var actual = (unitOfWork.Set<Crew>() as FakeRpository<Crew>).updatedItem;

            //assert
            Assert.IsNull(actual);
        }

    }
}
