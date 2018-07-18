using AirportRESRfulApi.Shared.DTO;
using FluentValidation;

namespace AirportRESRfulApi.BLL.Validations
{
    public class DeparturesValidator : AbstractValidator<DepartureDto>
    {
        public DeparturesValidator()
        {
            RuleFor(x => x.FlightId)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.FlightNumber)
              .NotNull()
              .NotEmpty()
              .MinimumLength(3)
              .MaximumLength(8);
            RuleFor(x => x.DepartureTime)
              .NotNull()
              .NotEmpty();
        }
        
    }
}
