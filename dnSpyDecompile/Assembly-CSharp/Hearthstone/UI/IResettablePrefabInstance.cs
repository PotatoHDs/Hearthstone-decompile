using System;

namespace Hearthstone.UI
{
	// Token: 0x02000FF0 RID: 4080
	public interface IResettablePrefabInstance
	{
		// Token: 0x0600B167 RID: 45415
		void RegisterResetListener(Action reset);

		// Token: 0x0600B168 RID: 45416
		void Reset();

		// Token: 0x0600B169 RID: 45417
		bool IsInstanceOfAsset(string guid);
	}
}
