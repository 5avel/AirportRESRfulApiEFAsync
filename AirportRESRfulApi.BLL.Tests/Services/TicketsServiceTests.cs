namespace AirportRESRfulApi.BLL.Tests.Services
{
    using AirportRESRfulApi.BLL.Services;
    using AirportRESRfulApi.DAL.Interfaces;
    using AirportRESRfulApi.DAL.Models;
    using AutoMapper;
    using FakeItEasy;
    using FluentValidation;
    using NUnit.Framework;
    using Shared.DTO;
    using System.Collections.Generic;
    using System.Linq;


    [TestFixture]
    public class TicketsServiceTests
    {
        private  IUnitOfWork unitOfWork;
        private  IRepository<Ticket> repository;
        private  IMapper mapper;
        private  IValidator<TicketDto> validator;

        private List<Ticket> ticketList;

        public TicketsServiceTests()
        {
            
        } 

        [SetUp]
        public void Init()
        {
            unitOfWork = A.Fake<IUnitOfWork>();
            repository = A.Fake<IRepository<Ticket>>();
            validator = A.Fake<IValidator<TicketDto>>();

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Ticket, TicketDto>();
                cfg.CreateMap<TicketDto, Ticket>();
            }).CreateMapper();

            A.CallTo(unitOfWork)
                .Where(call => call.Method.Name == "Set")
                .WithReturnType<IRepository<Ticket>>()
                .Returns(repository);

            ticketList = new List<Ticket>
                 {
                    new Ticket
                    { Id = 1, FlightId = 1,IsSold = false, PlaseNumber = 1,FlightNumber = "TY26", Price = 200},
                    new Ticket
                    { Id = 2, FlightId = 1,IsSold = false, PlaseNumber = 2,FlightNumber = "TY26", Price = 200},
                    new Ticket
                    { Id = 3, FlightId = 1,IsSold = true, PlaseNumber = 3,FlightNumber = "QW11", Price = 200},
                    new Ticket
                    { Id = 4, FlightId = 1,IsSold = true, PlaseNumber = 4,FlightNumber = "QW11", Price = 200}
                 };
        }

        [TearDown]
        public void TestTearDown()
        {
            // Clean up data in our fake dependencies.
           
        }

        private void SetCollection(int id)
        {
            A.CallTo(repository)
               .Where(call => call.Method.Name == "Get")
               .WithReturnType<IEnumerable<Ticket>>()
               .Returns(ticketList.Where(x => x.Id == id));
        }


        [TestCase(1)]
        [TestCase(2)]
        public void BuyById_TicketId_TicketIsSold_true(int id)
        {
            //arange
            SetCollection(id);
            TicketsService ts = new TicketsService(unitOfWork, mapper, validator);
            //act
            var result = ts.BuyById(id);
            //assert
            Assert.IsTrue(result.IsSold);
        }

        [TestCase(3)]
        [TestCase(4)]
        public void BuyById_TicketId_Ticket_null(int id)
        {
            //arange
            SetCollection(id);
            TicketsService ts = new TicketsService(unitOfWork, mapper, validator);
            //act
            var result = ts.BuyById(id);
            //assert
            Assert.IsNull(result);
        }

        [TestCase(4)]
        [TestCase(3)]
        public void ReturnById_TicketId_TicketIsSold_true(int id)
        {
            //arange
            SetCollection(id);
            TicketsService ts = new TicketsService(unitOfWork, mapper, validator);
            //act
            var result = ts.ReturnById(id);
            //assert
            Assert.IsFalse(result.IsSold);
        }

        [TestCase(2)]
        [TestCase(1)]
        public void ReturnById_TicketId_Ticket_null(int id)
        {
            //arange
            SetCollection(id);
            TicketsService ts = new TicketsService(unitOfWork, mapper, validator);
            //act
            var result = ts.ReturnById(id);
            //assert
            Assert.IsNull(result);
        }

        [Test]
        public void MakeListTickets_UfSaveChages_Once()
        {
            //arange
            var ticketsDto = new List<TicketDto>
                 {
                    new TicketDto { Id = 1, FlightId = 1,IsSold = false, PlaseNumber = 1,FlightNumber = "TY26", Price = 200},
                    new TicketDto { Id = 2, FlightId = 1,IsSold = false, PlaseNumber = 2,FlightNumber = "TY26", Price = 200},
                    new TicketDto { Id = 3, FlightId = 1,IsSold = true, PlaseNumber = 3,FlightNumber = "QW11", Price = 200},
                    new TicketDto { Id = 4, FlightId = 1,IsSold = true, PlaseNumber = 4,FlightNumber = "QW11", Price = 200}
                 };

            TicketsService ts = new TicketsService(unitOfWork, mapper, validator);

            //act
            ts.Make(ticketsDto);

            //assert
            A.CallTo(() => unitOfWork.SaveChages()).MustHaveHappened(Repeated.Exactly.Once);

        }
    }
}
