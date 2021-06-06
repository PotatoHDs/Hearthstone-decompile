using UnityEngine;

namespace Hearthstone.UI
{
	public interface INestedReferenceResolver : IAsyncInitializationBehavior
	{
		Component GetComponentById(long id);

		bool GetComponentId(Component component, out long id);
	}
}
