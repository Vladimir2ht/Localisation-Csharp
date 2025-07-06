namespace LocalizationNamespace.DTOs
{
	public class TranslationDto
	{
		public int Id { get; set; }
		public int LocalizationKeyId { get; set; }
		public int LanguageId { get; set; }
		public string Value { get; set; }
	}
}