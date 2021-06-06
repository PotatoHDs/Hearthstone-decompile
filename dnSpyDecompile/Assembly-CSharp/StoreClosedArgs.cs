using System;

// Token: 0x02000707 RID: 1799
public class StoreClosedArgs
{
	// Token: 0x060064B8 RID: 25784 RVA: 0x0020E83B File Offset: 0x0020CA3B
	public StoreClosedArgs(bool authorizationBackButtonPressed = false)
	{
		this.authorizationBackButtonPressed = new bool?(authorizationBackButtonPressed);
	}

	// Token: 0x040053C0 RID: 21440
	public bool? authorizationBackButtonPressed;
}
