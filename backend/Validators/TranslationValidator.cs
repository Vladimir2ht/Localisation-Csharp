using FluentValidation;
using LocalizationNamespace.Data;
using LocalizationNamespace.DTOs;

namespace LocalizationNamespace.Validators
{
	public class TranslationValidator : FluentValidation.AbstractValidator<TranslationDto>
	{
		private readonly AppDbContext _db;

		public TranslationValidator(AppDbContext db)
		{
			_db = db;

			RuleFor(x => x.LocalizationKey)
				.Must(key => _db.LocalizationKeys.Any(k => k.Key == key))
				.WithMessage("Localization key does not exist.");

			RuleFor(x => x.Language)
				.Must(code => _db.Languages.Any(l => l.Code == code))
				.WithMessage("Language does not exist.");
		}
	}
}