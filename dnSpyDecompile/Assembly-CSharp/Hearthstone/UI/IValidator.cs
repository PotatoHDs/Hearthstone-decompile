using System;
using System.Collections.Generic;

namespace Hearthstone.UI
{
	// Token: 0x02001016 RID: 4118
	public interface IValidator
	{
		// Token: 0x0600B2EF RID: 45807
		bool CheckValidation(ref int errorCount, ref ICollection<string> resultStrings);
	}
}
