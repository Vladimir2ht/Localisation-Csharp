namespace LocalizationNamespace.Models

{
	public class LocalizationKey
	{
		public int Id { get; set; }
		public string Key { get; set; }
		public ICollection<Translation> Translations { get; set; }
	}
}