using AirportRESRfulApi.Shared.DTO;
using FluentValidation;

namespace AirportRESRfulApi.BLL.Validations
{
    public class PlanesValidator : AbstractValidator<PlaneDto>
    {
        public PlanesValidator()
        {
            RuleFor(x => x.DepartureId)
                .NotEmpty();
            RuleFor(x => x.Name)
              .NotNull()
              .NotEmpty()
              .MinimumLength(3)
              .MaximumLength(10);
            RuleFor(x => x.ReleaseDate)
              .NotNull()
              .NotEmpty();
            RuleFor(x => x.PlaneTypeId)
              .NotEmpty();
        }

    }
}
