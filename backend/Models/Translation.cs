namespace LocalizationNamespace.Models

{
	public class Translation
	{
		public int Id { get; set; }
		public int LocalizationKeyId { get; set; }
		public int LanguageId { get; set; }
		public string Value { get; set; }
		public LocalizationKey LocalizationKey { get; set; }
		public Language Language { get; set; }
	}
}