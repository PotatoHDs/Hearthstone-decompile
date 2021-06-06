using System;

// Token: 0x020006C8 RID: 1736
public class MoveMinionSpellController : SpellController
{
	// Token: 0x0600614D RID: 24909 RVA: 0x001FC3FA File Offset: 0x001FA5FA
	protected override void OnProcessTaskList()
	{
		this.OnFinishedTaskList();
		this.OnFinished();
	}
}
