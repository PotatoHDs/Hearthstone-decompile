using System;

namespace Hearthstone.UI
{
	// Token: 0x02000FED RID: 4077
	public interface ILayerOverridable
	{
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x0600B158 RID: 45400
		bool HandlesChildLayers { get; }

		// Token: 0x0600B159 RID: 45401
		void SetLayerOverride(GameLayer layer);

		// Token: 0x0600B15A RID: 45402
		void ClearLayerOverride();
	}
}
