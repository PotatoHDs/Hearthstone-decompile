namespace bgs
{
	public class Log
	{
		public static Logger BattleNet = new Logger();

		public static Logger Party = new Logger();

		private static Log s_instance;

		public static Log Get()
		{
			if (s_instance == null)
			{
				s_instance = new Log();
				s_instance.Initialize();
			}
			return s_instance;
		}

		private void Initialize()
		{
		}
	}
}
