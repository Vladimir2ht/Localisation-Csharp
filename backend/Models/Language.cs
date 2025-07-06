namespace LocalizationNamespace.Models

{
	public class Language
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public ICollection<Translation> Translations { get; set; }
	}
}