using UnityEngine;

namespace Hearthstone.UI.Tests
{
	[AddComponentMenu("")]
	public class UIFrameworkTests_AsyncReference : MonoBehaviour
	{
		public AsyncReference ReferenceToAsyncBehavior;

		public AsyncReference ReferenceToRegularBehavior;

		public AsyncReference MissingReference;
	}
}
