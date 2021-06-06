using System;
using System.IO;
using System.Linq;

namespace Hearthstone.Core.Streaming
{
	// Token: 0x02001092 RID: 4242
	public class IndicatorChecker : IIndicatorChecker
	{
		// Token: 0x0600B7F0 RID: 47088 RVA: 0x00385A39 File Offset: 0x00383C39
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
