using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004D5 RID: 1237
public class DRGA_Evil_Fight_07 : DRGA_Dungeon
{
	// Token: 0x06004242 RID: 16962 RVA: 0x00163668 File Offset: 0x00161868
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Death_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_01_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_02_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_03_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_01_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_02_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossAttack_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossStart_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_EmoteResponse_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_01_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_02_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_03_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_01_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_02_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_03_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_01,
			DRGA_Evil_Fight_07.VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004243 RID: 16963 RVA: 0x001637DC File Offset: 0x001619DC
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_IdleLines;
	}

	// Token: 0x06004244 RID: 16964 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004245 RID: 16965 RVA: 0x001637E4 File Offset: 0x001619E4
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Death_01;
	}

	// Token: 0x06004246 RID: 16966 RVA: 0x001637FC File Offset: 0x001619FC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004247 RID: 16967 RVA: 0x0016388D File Offset: 0x00161A8D
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_missionEventTrigger100Lines);
			break;
		case 101:
			yield return base.PlayLineOnlyOnce(actor, DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossAttack_01, 2.5f);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor2, this.m_VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPowerLines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06004248 RID: 16968 RVA: 0x001638A3 File Offset: 0x00161AA3
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "DRG_033" && !this.m_Heroic)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreathLines);
		}
		yield break;
	}

	// Token: 0x06004249 RID: 16969 RVA: 0x001638B9 File Offset: 0x00161AB9
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
		if (cardId == "DRGA_BOSS_23t")
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsakenLines);
		}
		yield break;
	}

	// Token: 0x040031EC RID: 12780
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Death_01.prefab:952a81bfdab47284994a01faa45590ce");

	// Token: 0x040031ED RID: 12781
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_01_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_01_01.prefab:148c6825171d8cd41bb624f2cff122ca");

	// Token: 0x040031EE RID: 12782
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_02_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_02_01.prefab:6cd702754cada6740b1edfe85ff66cbd");

	// Token: 0x040031EF RID: 12783
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_03_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_03_01.prefab:ecfc1e70bff9db048a6c78da1592aca7");

	// Token: 0x040031F0 RID: 12784
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_01_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_01_01.prefab:128a69a740c056949b3b5c74e9e6d6d7");

	// Token: 0x040031F1 RID: 12785
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_02_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_02_01.prefab:149a30c34f666b54d9e6e3575a21a6c7");

	// Token: 0x040031F2 RID: 12786
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossAttack_01.prefab:bdb9ac51e8a91d241878865afeee5913");

	// Token: 0x040031F3 RID: 12787
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossStart_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossStart_01.prefab:4afb1fc74492b304e990584ff1d69d49");

	// Token: 0x040031F4 RID: 12788
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_EmoteResponse_01.prefab:3eb5f7ea36b24f24ba737f6eb82b2125");

	// Token: 0x040031F5 RID: 12789
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_01_01.prefab:6d4e17b4f71c9d0468a6bbc77dcbfe32");

	// Token: 0x040031F6 RID: 12790
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_02_01.prefab:f0d5700f148c49543b6377448242ff00");

	// Token: 0x040031F7 RID: 12791
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_03_01.prefab:c66d382d2faa5d340ae55d0e9da4f7cc");

	// Token: 0x040031F8 RID: 12792
	private static readonly AssetReference VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_01_01.prefab:85b01537507184842adce860f2de1ef1");

	// Token: 0x040031F9 RID: 12793
	private static readonly AssetReference VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_02_01.prefab:50510f94bd9f8d043ac97bdbe15284dd");

	// Token: 0x040031FA RID: 12794
	private static readonly AssetReference VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_03_01.prefab:7df8ee0d6492d3c40bdf4fd1a8985e3f");

	// Token: 0x040031FB RID: 12795
	private static readonly AssetReference VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_01 = new AssetReference("VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_01.prefab:b803192afa357e64b9c0a98b07341c15");

	// Token: 0x040031FC RID: 12796
	private static readonly AssetReference VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_02 = new AssetReference("VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_02.prefab:b728a370c2350a440933191ef9c5f34b");

	// Token: 0x040031FD RID: 12797
	private List<string> m_missionEventTrigger100Lines = new List<string>
	{
		DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_01_01,
		DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_02_01,
		DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_03_01
	};

	// Token: 0x040031FE RID: 12798
	private List<string> m_VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsakenLines = new List<string>
	{
		DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_01_01,
		DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_02_01
	};

	// Token: 0x040031FF RID: 12799
	private List<string> m_VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_IdleLines = new List<string>
	{
		DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_01_01,
		DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_02_01,
		DRGA_Evil_Fight_07.VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_03_01
	};

	// Token: 0x04003200 RID: 12800
	private List<string> m_VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPowerLines = new List<string>
	{
		DRGA_Evil_Fight_07.VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_01_01,
		DRGA_Evil_Fight_07.VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_02_01,
		DRGA_Evil_Fight_07.VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_03_01
	};

	// Token: 0x04003201 RID: 12801
	private List<string> m_VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreathLines = new List<string>
	{
		DRGA_Evil_Fight_07.VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_01,
		DRGA_Evil_Fight_07.VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_02
	};

	// Token: 0x04003202 RID: 12802
	private HashSet<string> m_playedLines = new HashSet<string>();
}
