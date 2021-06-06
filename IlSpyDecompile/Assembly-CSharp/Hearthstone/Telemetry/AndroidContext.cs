namespace Hearthstone.Telemetry
{
	internal class AndroidContext : BaseContextData
	{
		private const string s_applicationId = "com.blizzard.wtcg.hearthstone";

		protected const string s_testingApplicationId = "com.blizzard.telemetry.test";

		public override string ApplicationID => "com.blizzard.wtcg.hearthstone";
	}
}
