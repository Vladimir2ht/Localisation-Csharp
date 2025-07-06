using FluentValidation;
using LocalizationNamespace.DTOs;

namespace LocalizationNamespace.Validators
{
	public class LocalizationKeyValidator : AbstractValidator<LocalizationKeyDto>
	{
		public LocalizationKeyValidator()
		{
			RuleFor(x => x.Key).NotEmpty();
		}
	}
}