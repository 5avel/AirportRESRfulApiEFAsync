using AirportRESRfulApi.Shared.DTO;
using FluentValidation;

namespace AirportRESRfulApi.BLL.Validations
{
    public class TicketsValidator : AbstractValidator<TicketDto>
    {
        public TicketsValidator()
        {
            RuleFor(x => x.FlightId)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.FlightNumber)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(8);
            RuleFor(x => x.PlaseNumber)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Price)
                .NotNull()
                .NotEmpty();
        }
    }
}
