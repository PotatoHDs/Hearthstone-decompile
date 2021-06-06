using System;

// Token: 0x02000B1C RID: 2844
public class LoginPointer : PegUIElement
{
	// Token: 0x06009722 RID: 38690 RVA: 0x0030D856 File Offset: 0x0030BA56
	protected override void OnPress()
	{
		GameUtils.LogoutConfirmation();
	}
}
