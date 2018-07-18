using AirportRESRfulApi.Shared.DTO;
using FluentValidation;

namespace AirportRESRfulApi.BLL.Validations
{
    public class FlightsValidator : AbstractValidator<FlightDto>
    {
        public FlightsValidator()
        {
            RuleFor(x => x.FlightNumber)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(10);
            RuleFor(x => x.DestinationPoint)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);
            RuleFor(x => x.DeparturePoint)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);
            RuleFor(x => x.ArrivalTime)
                .NotNull();
            RuleFor(x => x.DepartureTime)
                .NotNull();
            RuleFor(x => x).Must(f => f.DepartureTime < f.ArrivalTime)
                .WithMessage("Время прибытия не может быть раньше времени вылета( покаместь:)) )");
        }
        
    }
}
