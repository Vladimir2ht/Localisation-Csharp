namespace LocalizationNamespace.DTOs
{
	public class TranslationsTableItemDto
	{
		public string Key { get; set; }
		public Dictionary<string, string> Translations { get; set; }
	}
}