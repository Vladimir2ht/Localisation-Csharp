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

			RuleFor(x => x.LocalizationKeyId)
				.Must(id => _db.LocalizationKeys.Any(k => k.Id == id))
				.WithMessage("Localization key does not exist.");

			RuleFor(x => x.LanguageId)
				.Must(id => _db.Languages.Any(l => l.Id == id))
				.WithMessage("Language does not exist.");
		}
	}
}