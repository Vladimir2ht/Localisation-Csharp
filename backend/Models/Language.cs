namespace LocalizationNamespace.Models
{
	public class Language
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public bool InUse { get; set; } // Новое поле
		public ICollection<Translation> Translations { get; set; }
	}
}