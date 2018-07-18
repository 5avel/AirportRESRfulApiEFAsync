using AirportRESRfulApi.Shared.DTO;
using FluentValidation;

namespace AirportRESRfulApi.BLL.Validations
{
    public class CrewsValidator : AbstractValidator<CrewDto>
    {
        public CrewsValidator()
        {
            //RuleFor(x => x.DepartureId)
            //    .NotNull()
            //    .NotEmpty();
        }
        
    }
}
