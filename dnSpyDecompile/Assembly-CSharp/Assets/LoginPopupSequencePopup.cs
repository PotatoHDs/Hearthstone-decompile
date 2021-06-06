using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FCD RID: 4045
	public static class LoginPopupSequencePopup
	{
		// Token: 0x0600B028 RID: 45096 RVA: 0x00366E7C File Offset: 0x0036507C
		public static LoginPopupSequencePopup.LoginPopupSequencePopupType ParseLoginPopupSequencePopupTypeValue(string value)
		{
			LoginPopupSequencePopup.LoginPopupSequencePopupType result;
			EnumUtils.TryGetEnum<LoginPopupSequencePopup.LoginPopupSequencePopupType>(value, out result);
			return result;
		}

		// Token: 0x020027F3 RID: 10227
		public enum LoginPopupSequencePopupType
		{
			// Token: 0x0400F7D1 RID: 63441
			[Description("invalid")]
			INVALID,
			// Token: 0x0400F7D2 RID: 63442
			[Description("basic")]
			BASIC,
			// Token: 0x0400F7D3 RID: 63443
			[Description("featured_cards")]
			FEATURED_CARDS
		}
	}
}
