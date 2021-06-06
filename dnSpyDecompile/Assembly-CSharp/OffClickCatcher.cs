using System;

// Token: 0x02000136 RID: 310
public class OffClickCatcher : PegUIElement
{
	// Token: 0x0600145E RID: 5214 RVA: 0x00004EB5 File Offset: 0x000030B5
	protected override void OnRelease()
	{
		Navigation.GoBack();
	}

	// Token: 0x0600145F RID: 5215 RVA: 0x00004EB5 File Offset: 0x000030B5
	protected override void OnRightClick()
	{
		Navigation.GoBack();
	}
}
