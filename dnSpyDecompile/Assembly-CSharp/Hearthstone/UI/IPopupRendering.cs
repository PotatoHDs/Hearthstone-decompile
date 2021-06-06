using System;

namespace Hearthstone.UI
{
	// Token: 0x02000FF5 RID: 4085
	public interface IPopupRendering
	{
		// Token: 0x0600B180 RID: 45440
		void EnablePopupRendering(PopupRoot popupRoot);

		// Token: 0x0600B181 RID: 45441
		void DisablePopupRendering();

		// Token: 0x0600B182 RID: 45442
		bool ShouldPropagatePopupRendering();
	}
}
