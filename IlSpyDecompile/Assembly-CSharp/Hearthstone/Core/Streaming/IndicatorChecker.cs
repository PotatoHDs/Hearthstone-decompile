using System.IO;
using System.Linq;

namespace Hearthstone.Core.Streaming
{
	public class IndicatorChecker : IIndicatorChecker
	{
		public bool Exists(string[] tagIndicators)
		{
			if (!PlatformSettings.IsMobileRuntimeOS)
			{
				return true;
			}
			return tagIndicators.All((string e) => File.Exists(e));
		}
	}
}
