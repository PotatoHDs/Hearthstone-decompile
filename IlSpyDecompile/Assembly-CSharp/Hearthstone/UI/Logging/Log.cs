using Hearthstone.UI.Logging.Internal;

namespace Hearthstone.UI.Logging
{
	public static class Log
	{
		private static ILog s_instance;

		public static ILog Get()
		{
			if (s_instance == null)
			{
				s_instance = new RuntimeLog();
			}
			return s_instance;
		}
	}
}
