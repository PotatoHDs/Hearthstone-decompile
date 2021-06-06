using System.Collections.Generic;

namespace Hearthstone.UI
{
	public interface IValidator
	{
		bool CheckValidation(ref int errorCount, ref ICollection<string> resultStrings);
	}
}
