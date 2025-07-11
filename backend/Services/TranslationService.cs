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
			var translation = _db.Translations.FirstOrDefault(t => t.LocalizationKey == key && t.Language == lang);

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

			if (translation == null)
			{
				translation = new Translation {
					LocalizationKey = key,
					Language = lang,
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