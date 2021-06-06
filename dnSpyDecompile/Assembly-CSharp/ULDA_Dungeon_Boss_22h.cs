using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000493 RID: 1171
public class ULDA_Dungeon_Boss_22h : ULDA_Dungeon
{
	// Token: 0x06003F35 RID: 16181 RVA: 0x0014F448 File Offset: 0x0014D648
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_BossTriggerColossusoftheMoon_01,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_BossTriggerPlagueofWrath_01,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_BossTriggerWhirlwind_01,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_Death_01,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_DefeatPlayer_01,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_EmoteResponse_01,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_01,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_02,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_03,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_04,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_Idle_01,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_Idle_02,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_Idle_03,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_Intro_01,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_PlayerTriggerColossusoftheMoon_01,
			ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_PlayerTriggerDesertObelisk_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003F36 RID: 16182 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003F37 RID: 16183 RVA: 0x0014F5AC File Offset: 0x0014D7AC
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003F38 RID: 16184 RVA: 0x0014F5B4 File Offset: 0x0014D7B4
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003F39 RID: 16185 RVA: 0x0014F5BC File Offset: 0x0014D7BC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_EmoteResponse_01;
	}

	// Token: 0x06003F3A RID: 16186 RVA: 0x0014F5F4 File Offset: 0x0014D7F4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003F3B RID: 16187 RVA: 0x0014F67D File Offset: 0x0014D87D
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003F3C RID: 16188 RVA: 0x0014F693 File Offset: 0x0014D893
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "ULD_721"))
		{
			if (cardId == "ULD_703")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_PlayerTriggerDesertObelisk_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_PlayerTriggerColossusoftheMoon_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003F3D RID: 16189 RVA: 0x0014F6A9 File Offset: 0x0014D8A9
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "ULD_721"))
		{
			if (!(cardId == "ULD_707"))
			{
				if (cardId == "DAL_742")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_BossTriggerWhirlwind_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_BossTriggerPlagueofWrath_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_BossTriggerColossusoftheMoon_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002BE7 RID: 11239
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_BossTriggerColossusoftheMoon_01 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_BossTriggerColossusoftheMoon_01.prefab:9259ea2253654b141be338f1f6076bee");

	// Token: 0x04002BE8 RID: 11240
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_BossTriggerPlagueofWrath_01 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_BossTriggerPlagueofWrath_01.prefab:9dbe5fa83d15c5b45a7c96e8211ead4d");

	// Token: 0x04002BE9 RID: 11241
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_BossTriggerWhirlwind_01 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_BossTriggerWhirlwind_01.prefab:b820478293db40b4db21fb618a123425");

	// Token: 0x04002BEA RID: 11242
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_Death_01 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_Death_01.prefab:68bb7ce0753c67147a4486322687cb54");

	// Token: 0x04002BEB RID: 11243
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_DefeatPlayer_01.prefab:40d3f74e4297a5a428a0ff991098e2ee");

	// Token: 0x04002BEC RID: 11244
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_EmoteResponse_01.prefab:0f31018a56fb287418c6facbcf56b70f");

	// Token: 0x04002BED RID: 11245
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_01.prefab:5c395f44e48c879408ca6166e55c1c2f");

	// Token: 0x04002BEE RID: 11246
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_02.prefab:eb91a602cecc11648b8f52b968118af7");

	// Token: 0x04002BEF RID: 11247
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_03.prefab:820107cdf7a9c82498fe7924df52a846");

	// Token: 0x04002BF0 RID: 11248
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_04.prefab:53254ced693d02142a3682e00fca15f4");

	// Token: 0x04002BF1 RID: 11249
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_Idle_01 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_Idle_01.prefab:b0c8522d14ca1ab479e52e465b87aa68");

	// Token: 0x04002BF2 RID: 11250
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_Idle_02 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_Idle_02.prefab:abdaaf5c642080e46a9f874af1964579");

	// Token: 0x04002BF3 RID: 11251
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_Idle_03 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_Idle_03.prefab:6db522e445aedc147abfb056c79e8aa1");

	// Token: 0x04002BF4 RID: 11252
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_Intro_01 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_Intro_01.prefab:f47f899252b20df4593256132352aff7");

	// Token: 0x04002BF5 RID: 11253
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_PlayerTriggerColossusoftheMoon_01 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_PlayerTriggerColossusoftheMoon_01.prefab:025ebb40085e43a4391e6795d4c47dec");

	// Token: 0x04002BF6 RID: 11254
	private static readonly AssetReference VO_ULDA_BOSS_22h_Male_Colossus_PlayerTriggerDesertObelisk_01 = new AssetReference("VO_ULDA_BOSS_22h_Male_Colossus_PlayerTriggerDesertObelisk_01.prefab:3d59177a4432427478bf4c2a32f31193");

	// Token: 0x04002BF7 RID: 11255
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_01,
		ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_02,
		ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_03,
		ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_HeroPower_04
	};

	// Token: 0x04002BF8 RID: 11256
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_Idle_01,
		ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_Idle_02,
		ULDA_Dungeon_Boss_22h.VO_ULDA_BOSS_22h_Male_Colossus_Idle_03
	};

	// Token: 0x04002BF9 RID: 11257
	private HashSet<string> m_playedLines = new HashSet<string>();
}
