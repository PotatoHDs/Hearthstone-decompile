using System;

// Token: 0x020005B9 RID: 1465
public class TB_MartinAutoBrawl : MissionEntity
{
	// Token: 0x060050EE RID: 20718 RVA: 0x0010C851 File Offset: 0x0010AA51
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

	// Token: 0x060050EF RID: 20719 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x060050F0 RID: 20720 RVA: 0x001A94F9 File Offset: 0x001A76F9
	public TB_MartinAutoBrawl()
	{
		this.m_gameOptions.AddOptions(TB_MartinAutoBrawl.s_booleanOptions, TB_MartinAutoBrawl.s_stringOptions);
	}

	// Token: 0x060050F1 RID: 20721 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	// Token: 0x040047B3 RID: 18355
	private static Map<GameEntityOption, bool> s_booleanOptions = TB_MartinAutoBrawl.InitBooleanOptions();

	// Token: 0x040047B4 RID: 18356
	private static Map<GameEntityOption, string> s_stringOptions = TB_MartinAutoBrawl.InitStringOptions();
}
