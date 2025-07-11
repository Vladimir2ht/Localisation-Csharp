namespace LocalizationNamespace.Models
{
	public class Translation
	{
		public string LocalizationKey { get; set; }
		public string Language { get; set; }
		public string Value { get; set; }
		public LocalizationKey LocalizationKeyNavigation { get; set; }
		public Language LanguageNavigation { get; set; }
	}
}