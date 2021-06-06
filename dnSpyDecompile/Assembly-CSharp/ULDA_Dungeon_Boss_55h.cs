using System;
using System.Collections.Generic;

// Token: 0x020004B4 RID: 1204
public class ULDA_Dungeon_Boss_55h : ULDA_Dungeon
{
	// Token: 0x060040C2 RID: 16578 RVA: 0x00159ACC File Offset: 0x00157CCC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_55h.VO_ULDA_BOSS_55h_WaterTotingMurlocs_Birth,
			ULDA_Dungeon_Boss_55h.VO_ULDA_BOSS_55h_WaterTotingMurlocs_Death,
			ULDA_Dungeon_Boss_55h.VO_ULDA_BOSS_55h_WaterTotingMurlocs_Defeat,
			ULDA_Dungeon_Boss_55h.VO_ULDA_BOSS_55h_WaterTotingMurlocs_EmoteResponse
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060040C3 RID: 16579 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060040C4 RID: 16580 RVA: 0x00159B70 File Offset: 0x00157D70
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_55h.VO_ULDA_BOSS_55h_WaterTotingMurlocs_Birth;
		this.m_deathLine = ULDA_Dungeon_Boss_55h.VO_ULDA_BOSS_55h_WaterTotingMurlocs_Death;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_55h.VO_ULDA_BOSS_55h_WaterTotingMurlocs_EmoteResponse;
	}

	// Token: 0x04002EFE RID: 12030
	private static readonly AssetReference VO_ULDA_BOSS_55h_WaterTotingMurlocs_Birth = new AssetReference("VO_ULDA_BOSS_55h_WaterTotingMurlocs_Birth.prefab:01476de4e3df6c043ba1294641446c14");

	// Token: 0x04002EFF RID: 12031
	private static readonly AssetReference VO_ULDA_BOSS_55h_WaterTotingMurlocs_Death = new AssetReference("VO_ULDA_BOSS_55h_WaterTotingMurlocs_Death.prefab:bccb469a8623a77459b83b9d9490edad");

	// Token: 0x04002F00 RID: 12032
	private static readonly AssetReference VO_ULDA_BOSS_55h_WaterTotingMurlocs_Defeat = new AssetReference("VO_ULDA_BOSS_55h_WaterTotingMurlocs_Defeat.prefab:b2bab368c56dcd44bb2d82f0f1067dd8");

	// Token: 0x04002F01 RID: 12033
	private static readonly AssetReference VO_ULDA_BOSS_55h_WaterTotingMurlocs_EmoteResponse = new AssetReference("VO_ULDA_BOSS_55h_WaterTotingMurlocs_EmoteResponse.prefab:dfaeb48349404bd498ce431ebc244cd4");
}
