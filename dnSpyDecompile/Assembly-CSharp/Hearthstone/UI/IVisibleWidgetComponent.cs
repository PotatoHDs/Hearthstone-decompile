using System;

namespace Hearthstone.UI
{
	// Token: 0x02001017 RID: 4119
	public interface IVisibleWidgetComponent
	{
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x0600B2F0 RID: 45808
		bool IsDesiredHidden { get; }

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600B2F1 RID: 45809
		bool IsDesiredHiddenInHierarchy { get; }

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600B2F2 RID: 45810
		bool HandlesChildVisibility { get; }

		// Token: 0x0600B2F3 RID: 45811
		void SetVisibility(bool isVisible, bool isInternal);
	}
}
