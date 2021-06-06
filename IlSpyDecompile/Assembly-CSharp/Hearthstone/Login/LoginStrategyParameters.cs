namespace Hearthstone.Login
{
	public struct LoginStrategyParameters
	{
		public IMobileAuth MobileAuth { get; set; }

		public string ChallengeUrl { get; set; }
	}
}
