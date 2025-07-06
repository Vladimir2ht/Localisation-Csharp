using FluentValidation;
using LocalizationNamespace.DTOs;

namespace LocalizationNamespace.Validators
{
	public class LanguageValidator : AbstractValidator<LanguageDto>
	{
		public LanguageValidator()
		{
			RuleFor(x => x.Code).NotEmpty();
			RuleFor(x => x.Name).NotEmpty();
		}
	}
}