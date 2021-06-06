using System;

// Token: 0x020009C9 RID: 2505
public class NestedPrefabPlatformOverride : NestedPrefabBase
{
	// Token: 0x060088BA RID: 35002 RVA: 0x002C096C File Offset: 0x002BEB6C
	protected override void LoadPrefab()
	{
		base.LoadPrefab(this.m_Prefab);
	}

	// Token: 0x040072D6 RID: 29398
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_Prefab;
}
