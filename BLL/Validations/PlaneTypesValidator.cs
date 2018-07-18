using AirportRESRfulApi.Shared.DTO;
using FluentValidation;

namespace AirportRESRfulApi.BLL.Validations
{
    public class PlaneTypesValidator : AbstractValidator<PlaneTypeDto>
    {
        public PlaneTypesValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Model)
              .NotNull()
              .NotEmpty()
              .MinimumLength(3)
              .MaximumLength(10);
            RuleFor(x => x.Range)
              .NotNull()
              .NotEmpty();
            RuleFor(x => x.Seats)
              .NotNull()
              .NotEmpty();
            RuleFor(x => x.Capacity)
              .NotNull()
              .NotEmpty();
            RuleFor(x => x.ServiceLife)
              .NotNull()
              .NotEmpty();
        }
        
    }
}
