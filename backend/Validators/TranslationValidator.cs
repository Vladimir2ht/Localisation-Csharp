using FluentValidation;
using LocalizationNamespace.DTOs;

namespace LocalizationNamespace.Validators
{
	public class TranslationValidator : AbstractValidator<TranslationDto>
	{
		public TranslationValidator()
		{
			RuleFor(x => x.Value).NotEmpty();
		}
	}
}