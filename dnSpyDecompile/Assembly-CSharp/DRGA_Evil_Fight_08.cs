using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004D6 RID: 1238
public class DRGA_Evil_Fight_08 : DRGA_Dungeon
{
	// Token: 0x0600424F RID: 16975 RVA: 0x00163B04 File Offset: 0x00161D04
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_BossStart_01,
			DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_EmoteResponse_01,
			DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_Idle_01_01,
			DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_Idle_02_01,
			DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_Idle_03_01,
			DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_01_01,
			DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_02_01,
			DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_03_01,
			DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Death_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004250 RID: 16976 RVA: 0x00163BF8 File Offset: 0x00161DF8
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_HeroPowerLines;
	}

	// Token: 0x06004251 RID: 16977 RVA: 0x00163C00 File Offset: 0x00161E00
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_IdleLines;
	}

	// Token: 0x06004252 RID: 16978 RVA: 0x00163C08 File Offset: 0x00161E08
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Death_01;
	}

	// Token: 0x06004253 RID: 16979 RVA: 0x00163C20 File Offset: 0x00161E20
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004254 RID: 16980 RVA: 0x00163CB1 File Offset: 0x00161EB1
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06004255 RID: 16981 RVA: 0x00163CC7 File Offset: 0x00161EC7
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		yield break;
	}

	// Token: 0x06004256 RID: 16982 RVA: 0x00163CDD File Offset: 0x00161EDD
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		yield break;
	}

	// Token: 0x04003203 RID: 12803
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_BossStart_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_BossStart_01.prefab:ebeac07ad58ae754e8c171ebd6317504");

	// Token: 0x04003204 RID: 12804
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_EmoteResponse_01.prefab:e7b356cfaaee40d4e88bc1233435a2ec");

	// Token: 0x04003205 RID: 12805
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_Idle_01_01.prefab:a40fe4e468a6e7f439e2d3d27ccd347d");

	// Token: 0x04003206 RID: 12806
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_Idle_02_01.prefab:9cc60e17257bb9f42af260a22e946844");

	// Token: 0x04003207 RID: 12807
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_Idle_03_01.prefab:bd2012eac94e1284486522a117ce2848");

	// Token: 0x04003208 RID: 12808
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_01_01.prefab:23f2fb8953fe11e48a76432fc517c83a");

	// Token: 0x04003209 RID: 12809
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_02_01.prefab:062631af973847c46b0c110530af2373");

	// Token: 0x0400320A RID: 12810
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_03_01.prefab:773cc7e2beadfad4f9aa5ae0c2c5d212");

	// Token: 0x0400320B RID: 12811
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Death_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Death_01.prefab:8b8fcd0b96015c94c881060522820b6a");

	// Token: 0x0400320C RID: 12812
	private List<string> m_VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_IdleLines = new List<string>
	{
		DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_Idle_01_01,
		DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_Idle_02_01,
		DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_Idle_03_01
	};

	// Token: 0x0400320D RID: 12813
	private List<string> m_VO_DRGA_BOSS_30h_Male_Human_Evil_Fight_08_HeroPowerLines = new List<string>
	{
		DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_01_01,
		DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_02_01,
		DRGA_Evil_Fight_08.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_03_01
	};

	// Token: 0x0400320E RID: 12814
	private HashSet<string> m_playedLines = new HashSet<string>();
}
