using System;
using System.Collections.Generic;

// Token: 0x020004A9 RID: 1193
public class ULDA_Dungeon_Boss_44h : ULDA_Dungeon
{
	// Token: 0x06004048 RID: 16456 RVA: 0x00156FF4 File Offset: 0x001551F4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_44h.VO_ULDA_BOSS_44h_TrapRoom_Death,
			ULDA_Dungeon_Boss_44h.VO_ULDA_BOSS_44h_TrapRoom_Defeat,
			ULDA_Dungeon_Boss_44h.VO_ULDA_BOSS_44h_TrapRoom_EmoteResponse,
			ULDA_Dungeon_Boss_44h.VO_ULDA_BOSS_44h_TrapRoom_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004049 RID: 16457 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600404A RID: 16458 RVA: 0x00157098 File Offset: 0x00155298
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_44h.VO_ULDA_BOSS_44h_TrapRoom_Intro;
		this.m_deathLine = ULDA_Dungeon_Boss_44h.VO_ULDA_BOSS_44h_TrapRoom_Death;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_44h.VO_ULDA_BOSS_44h_TrapRoom_EmoteResponse;
	}

	// Token: 0x04002E3F RID: 11839
	private static readonly AssetReference VO_ULDA_BOSS_44h_TrapRoom_Death = new AssetReference("VO_ULDA_BOSS_44h_TrapRoom_Death.prefab:c367791e544f2bb468526474bd808762");

	// Token: 0x04002E40 RID: 11840
	private static readonly AssetReference VO_ULDA_BOSS_44h_TrapRoom_Defeat = new AssetReference("VO_ULDA_BOSS_44h_TrapRoom_Defeat.prefab:3953c061798e64743bb98bbf7ca37d30");

	// Token: 0x04002E41 RID: 11841
	private static readonly AssetReference VO_ULDA_BOSS_44h_TrapRoom_EmoteResponse = new AssetReference("VO_ULDA_BOSS_44h_TrapRoom_EmoteResponse.prefab:4f8b7ac1c85dab140a10c08793e0ee75");

	// Token: 0x04002E42 RID: 11842
	private static readonly AssetReference VO_ULDA_BOSS_44h_TrapRoom_Intro = new AssetReference("VO_ULDA_BOSS_44h_TrapRoom_Intro.prefab:a58d8dc07b57b3d4ca8f7e403461209b");
}
