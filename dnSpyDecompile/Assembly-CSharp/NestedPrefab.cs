using System;

// Token: 0x020009C7 RID: 2503
public class NestedPrefab : NestedPrefabBase
{
	// Token: 0x060088AF RID: 34991 RVA: 0x002C06DE File Offset: 0x002BE8DE
	protected override void LoadPrefab()
	{
		base.LoadPrefab(this.m_Prefab);
	}

	// Token: 0x040072D2 RID: 29394
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_Prefab;
}
