using LocalizationNamespace.Data;
using LocalizationNamespace.Models;
using System.Linq;

namespace LocalizationNamespace.Services
{
	public class TranslationService
	{
		private readonly AppDbContext _db;

		public TranslationService(AppDbContext db)
		{
			_db = db;
		}

		public bool UpdateTranslation(string key, string lang, string value)
		{
			var locKey = _db.LocalizationKeys.FirstOrDefault(k => k.Key == key);
			var language = _db.Languages.FirstOrDefault(l => l.Code == lang);
			var translation = (locKey != null && language != null)
				? _db.Translations.FirstOrDefault(t => t.LocalizationKeyId == locKey.Id && t.LanguageId == language.Id)
				: null;

			if (string.IsNullOrWhiteSpace(value))
			{
				if (translation != null)
				{
					_db.Translations.Remove(translation);
					try {
						_db.SaveChanges();
						return true;
					} catch {
						return false;
					}
				}
				return true;
			}

			if (locKey == null || language == null)
				return false;

			if (translation == null)
			{
				translation = new Translation {
					LocalizationKeyId = locKey.Id,
					LanguageId = language.Id,
					Value = value
				};
				_db.Translations.Add(translation);
			}
			else
			{
				translation.Value = value;
			}

			try {
				_db.SaveChanges();
				return true;
			} catch {
				return false;
			}
		}
	}
}