using System;

// Token: 0x020005A2 RID: 1442
public class TB_AutoBrawl : MissionEntity
{
	// Token: 0x06004FC3 RID: 20419 RVA: 0x0010C851 File Offset: 0x0010AA51
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.HANDLE_COIN,
				false
			}
		};
	}

	// Token: 0x06004FC4 RID: 20420 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x06004FC5 RID: 20421 RVA: 0x001A2D82 File Offset: 0x001A0F82
	public TB_AutoBrawl()
	{
		this.m_gameOptions.AddOptions(TB_AutoBrawl.s_booleanOptions, TB_AutoBrawl.s_stringOptions);
	}

	// Token: 0x06004FC6 RID: 20422 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	// Token: 0x040045DA RID: 17882
	private static Map<GameEntityOption, bool> s_booleanOptions = TB_AutoBrawl.InitBooleanOptions();

	// Token: 0x040045DB RID: 17883
	private static Map<GameEntityOption, string> s_stringOptions = TB_AutoBrawl.InitStringOptions();
}
