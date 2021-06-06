using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200043B RID: 1083
public class DALA_Dungeon_Boss_14h : DALA_Dungeon
{
	// Token: 0x06003AFB RID: 15099 RVA: 0x00131D60 File Offset: 0x0012FF60
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_02,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_02,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_03,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_04,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_Death_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_DefeatPlayer_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_EmoteResponse_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_02,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_03,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_04,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_Idle_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_Idle_02,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_Idle_03,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_Intro_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_IntroChu_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_IntroGeorge_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_IntroOlBarkeye_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_PlayerBarista_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_PlayerBelligerentGnome_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_PlayerBrawl_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_PlayerBrawl_02,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_PlayerFriendlyBartender_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_PlayerInnkeeper_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003AFC RID: 15100 RVA: 0x00131F64 File Offset: 0x00130164
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_01,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_02,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_03,
			DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_04
		};
	}

	// Token: 0x06003AFD RID: 15101 RVA: 0x00131FB6 File Offset: 0x001301B6
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_14h.m_IdleLines;
	}

	// Token: 0x06003AFE RID: 15102 RVA: 0x00131FBD File Offset: 0x001301BD
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_EmoteResponse_01;
	}

	// Token: 0x06003AFF RID: 15103 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003B00 RID: 15104 RVA: 0x00131FF8 File Offset: 0x001301F8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		else
		{
			if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x06003B01 RID: 15105 RVA: 0x0013211E File Offset: 0x0013031E
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (!(cardId == "EX1_407"))
		{
			if (!(cardId == "CFM_654"))
			{
				if (!(cardId == "DAL_546"))
				{
					if (cardId == "DAL_560")
					{
						yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_PlayerInnkeeper_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_PlayerBarista_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_PlayerFriendlyBartender_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_14h.m_PlayerBrawl);
		}
		yield break;
	}

	// Token: 0x06003B02 RID: 15106 RVA: 0x00132134 File Offset: 0x00130334
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
		if (!(cardId == "EX1_407"))
		{
			if (cardId == "UNG_929" || cardId == "LOOT_367" || cardId == "EX1_604" || cardId == "BRM_019")
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_14h.m_BossMinions);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_14h.m_BossBrawl);
		}
		yield break;
	}

	// Token: 0x0400234A RID: 9034
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_01.prefab:0387c38aaa4828946bf1afe52d5b57d7");

	// Token: 0x0400234B RID: 9035
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_02 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_02.prefab:1055c46b195ef0b429b98c7f148ca354");

	// Token: 0x0400234C RID: 9036
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_01.prefab:3529c6612f600d945ba2a63b20cfac30");

	// Token: 0x0400234D RID: 9037
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_02 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_02.prefab:c559926c12ee89a468ff1f560e6c7357");

	// Token: 0x0400234E RID: 9038
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_03 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_03.prefab:2130ce377fd22474aa87e5f7bd86034b");

	// Token: 0x0400234F RID: 9039
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_04 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_04.prefab:e20958b1ba708404fb103bbc82d682e4");

	// Token: 0x04002350 RID: 9040
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_Death_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_Death_01.prefab:7e25486f6ed6630468c87ca07757b98c");

	// Token: 0x04002351 RID: 9041
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_DefeatPlayer_01.prefab:ba15377f29b7aed44922214b13a53cb0");

	// Token: 0x04002352 RID: 9042
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_EmoteResponse_01.prefab:0c5757ee092f237499e93532dabb4862");

	// Token: 0x04002353 RID: 9043
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_01.prefab:9616a94095992594ab4e26cb130a84bb");

	// Token: 0x04002354 RID: 9044
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_02 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_02.prefab:33f12ace376cdd4439784dbf89c15372");

	// Token: 0x04002355 RID: 9045
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_03 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_03.prefab:37a0706354e36fe49966a1efc4f06034");

	// Token: 0x04002356 RID: 9046
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_04 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_04.prefab:085da2b55d007914aa390c165073bcd9");

	// Token: 0x04002357 RID: 9047
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_Idle_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_Idle_01.prefab:7741e3f93c779f04fa7f6b0492330c37");

	// Token: 0x04002358 RID: 9048
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_Idle_02 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_Idle_02.prefab:dad14c9ae939c1648970bfb583a481cc");

	// Token: 0x04002359 RID: 9049
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_Idle_03 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_Idle_03.prefab:a10f0db716b36914e80189de9837b05c");

	// Token: 0x0400235A RID: 9050
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_Intro_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_Intro_01.prefab:e4c01fcf044ade142af03ede6bdc6aa5");

	// Token: 0x0400235B RID: 9051
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_IntroChu_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_IntroChu_01.prefab:761a30e5ac47dce4ebdb58bc89d5aa97");

	// Token: 0x0400235C RID: 9052
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_IntroGeorge_01.prefab:a9a436580dab9f1428fc79e0cd301149");

	// Token: 0x0400235D RID: 9053
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_IntroOlBarkeye_01.prefab:cbcaeacaba94312429e33d6daa659829");

	// Token: 0x0400235E RID: 9054
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_PlayerBarista_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_PlayerBarista_01.prefab:6d2e50276607aa34db9c2a5a7a2641fa");

	// Token: 0x0400235F RID: 9055
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_PlayerBelligerentGnome_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_PlayerBelligerentGnome_01.prefab:296063de5dab691408cf8659b9fac8b7");

	// Token: 0x04002360 RID: 9056
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_PlayerBrawl_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_PlayerBrawl_01.prefab:aba174b9c03c2b542859aa3902ffb5c0");

	// Token: 0x04002361 RID: 9057
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_PlayerBrawl_02 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_PlayerBrawl_02.prefab:37273bbda399c7e4696b3eade4e5df6d");

	// Token: 0x04002362 RID: 9058
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_PlayerFriendlyBartender_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_PlayerFriendlyBartender_01.prefab:baaa7281105b85f499284e3513b3e834");

	// Token: 0x04002363 RID: 9059
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_PlayerInnkeeper_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_PlayerInnkeeper_01.prefab:9d19a40eedb9dac47ae55a62077a3e73");

	// Token: 0x04002364 RID: 9060
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_Idle_01,
		DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_Idle_02,
		DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_Idle_03
	};

	// Token: 0x04002365 RID: 9061
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04002366 RID: 9062
	private static List<string> m_PlayerBrawl = new List<string>
	{
		DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_Idle_01,
		DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_Idle_02,
		DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_Idle_03
	};

	// Token: 0x04002367 RID: 9063
	private static List<string> m_BossMinions = new List<string>
	{
		DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_01,
		DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_02,
		DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_03,
		DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_04
	};

	// Token: 0x04002368 RID: 9064
	private static List<string> m_BossBrawl = new List<string>
	{
		DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_01,
		DALA_Dungeon_Boss_14h.VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_02
	};
}
