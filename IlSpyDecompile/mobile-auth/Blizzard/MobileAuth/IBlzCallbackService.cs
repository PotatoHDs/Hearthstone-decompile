using System.Collections;

namespace Blizzard.MobileAuth
{
	public interface IBlzCallbackService
	{
		void StartCoroutine(IEnumerator operation);
	}
}
