using AirportRESRfulApi.Shared.DTO;
using FluentValidation;

namespace AirportRESRfulApi.BLL.Validations
{
    public class PilotsValidator : AbstractValidator<PilotDto>
    {
        public PilotsValidator()
        {
            RuleFor(x => x.CrewId)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.FirstName)
              .NotNull()
              .NotEmpty()
              .MinimumLength(3)
              .MaximumLength(100);
            RuleFor(x => x.LastName)
              .NotNull()
              .NotEmpty()
              .MinimumLength(3)
              .MaximumLength(100);
            RuleFor(x => x.Experience)
              .NotNull()
              .NotEmpty();
            RuleFor(x => x.Birthday)
              .NotNull()
              .NotEmpty();
        }
        
    }
}
