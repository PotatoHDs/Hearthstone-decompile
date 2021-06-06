namespace Hearthstone.UI
{
	public class GlobalDataContext
	{
		private static DataContext s_instance;

		public static DataContext Get()
		{
			if (s_instance == null)
			{
				s_instance = new DataContext();
			}
			return s_instance;
		}
	}
}
