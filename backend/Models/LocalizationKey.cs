namespace LocalizationNamespace.Models
{
	public class LocalizationKey
	{
		public string Key { get; set; }
		public ICollection<Translation> Translations { get; set; }
	}
}