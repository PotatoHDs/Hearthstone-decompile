namespace bgs
{
	public class ExternalChallenge
	{
		public string PayLoadType { get; set; }

		public string URL { get; set; }

		public ExternalChallenge Next { get; set; }
	}
}
