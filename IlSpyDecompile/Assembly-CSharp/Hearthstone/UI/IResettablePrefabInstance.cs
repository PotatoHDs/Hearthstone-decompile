using System;

namespace Hearthstone.UI
{
	public interface IResettablePrefabInstance
	{
		void RegisterResetListener(Action reset);

		void Reset();

		bool IsInstanceOfAsset(string guid);
	}
}
