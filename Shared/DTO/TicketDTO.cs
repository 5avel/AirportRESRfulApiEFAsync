namespace AirportRESRfulApi.Shared.DTO
{
    public class TicketDto : BaseDto
    {
       
        public int FlightId { set; get; }
        public string FlightNumber { set; get; }
        public decimal Price { set; get; }
        public int PlaseNumber { set; get; }
        public bool IsSold { set; get; }
    }
}