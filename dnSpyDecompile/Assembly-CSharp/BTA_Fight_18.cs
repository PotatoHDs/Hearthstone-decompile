using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004FC RID: 1276
public class BTA_Fight_18 : BTA_Dungeon_Heroic
{
	// Token: 0x060044AE RID: 17582 RVA: 0x00173F04 File Offset: 0x00172104
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_Attack_01,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_DarkPortal_01,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_WarlockAOEDamageSpell_01,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossDeath_01,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStart_01,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartDemonHunter_01,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartPaladin_01,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Emote_Response_01,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_SummonDemon_01,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_TimeOut_01,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_01,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_02,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_03,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_04,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleA_01,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleB_01,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleC_01,
			BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_UI_Mission_Fight_18_CoinSelect_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060044AF RID: 17583 RVA: 0x00174088 File Offset: 0x00172288
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_18h_IdleLines;
	}

	// Token: 0x060044B0 RID: 17584 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060044B1 RID: 17585 RVA: 0x00174090 File Offset: 0x00172290
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossDeath_01;
		this.m_standardEmoteResponseLine = BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Emote_Response_01;
	}

	// Token: 0x060044B2 RID: 17586 RVA: 0x001740B8 File Offset: 0x001722B8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "HERO_04" || cardId == "HERO_04a" || cardId == "HERO_04b" || cardId == "HERO_04c" || cardId == "HERO_04d")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartPaladin_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "HERO_10" || cardId == "HERO_10a" || cardId == "HERO_10b")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartDemonHunter_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x060044B3 RID: 17587 RVA: 0x00174238 File Offset: 0x00172438
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 110)
		{
			if (missionEvent != 500)
			{
				if (missionEvent != 507)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_missionEventTrigger507Lines);
				}
			}
			else
			{
				base.PlaySound(BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_Attack_01, 1f, true, false);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_SummonDemon_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060044B4 RID: 17588 RVA: 0x0017424E File Offset: 0x0017244E
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "TRL_302")
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_TimeOut_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060044B5 RID: 17589 RVA: 0x00174264 File Offset: 0x00172464
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "CS2_062") && !(cardId == "CFM_094") && !(cardId == "OG_239"))
		{
			if (cardId == "GVG_021")
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_DarkPortal_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_WarlockAOEDamageSpell_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060044B6 RID: 17590 RVA: 0x0017427A File Offset: 0x0017247A
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x0400376F RID: 14191
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_Attack_01.prefab:2729a0c015a55274b891ae8277c95b0a");

	// Token: 0x04003770 RID: 14192
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_DarkPortal_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_DarkPortal_01.prefab:9cd0a99019fbb9d459a8c0c6f55e1531");

	// Token: 0x04003771 RID: 14193
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_WarlockAOEDamageSpell_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_WarlockAOEDamageSpell_01.prefab:2f2888b711f125849b2f5db9d0b84c2f");

	// Token: 0x04003772 RID: 14194
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossDeath_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossDeath_01.prefab:a5de9613feb01ae49b61cb49a1790fa2");

	// Token: 0x04003773 RID: 14195
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStart_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStart_01.prefab:a1ebbc7b04832254aba457dc07dbbc0e");

	// Token: 0x04003774 RID: 14196
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartDemonHunter_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartDemonHunter_01.prefab:bd6ffec198d8a534daa7ea496ce7622e");

	// Token: 0x04003775 RID: 14197
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartPaladin_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartPaladin_01.prefab:2dde42aa99035b84a87fe231fc9958d4");

	// Token: 0x04003776 RID: 14198
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Emote_Response_01.prefab:6aa175905aaeb224fa558a2601f935bc");

	// Token: 0x04003777 RID: 14199
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_SummonDemon_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_SummonDemon_01.prefab:8a7d80ff30e579246b292953df7114c4");

	// Token: 0x04003778 RID: 14200
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_TimeOut_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_TimeOut_01.prefab:7915a7abac1d8bb4488208ee2d4f3b82");

	// Token: 0x04003779 RID: 14201
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_01.prefab:e1f2814555dd181499a920c1f5754778");

	// Token: 0x0400377A RID: 14202
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_02 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_02.prefab:621950d7bfeccb8468b796482864d6ca");

	// Token: 0x0400377B RID: 14203
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_03 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_03.prefab:7f153a6f89399ae42ac06bb3b1071c91");

	// Token: 0x0400377C RID: 14204
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_04 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_04.prefab:b342b44aae612854d845079d88b02b20");

	// Token: 0x0400377D RID: 14205
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleA_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleA_01.prefab:2092f9498d5a2a045ae5ba7859716689");

	// Token: 0x0400377E RID: 14206
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleB_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleB_01.prefab:dae654dff87dadc418bce653feb3eeb0");

	// Token: 0x0400377F RID: 14207
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleC_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleC_01.prefab:3ea279c040760a940bc9139ec5ba674c");

	// Token: 0x04003780 RID: 14208
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_UI_Mission_Fight_18_CoinSelect_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_UI_Mission_Fight_18_CoinSelect_01.prefab:618497dc5902b37498570cf7c9795ac7");

	// Token: 0x04003781 RID: 14209
	private List<string> m_missionEventTrigger507Lines = new List<string>
	{
		BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_01,
		BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_02,
		BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_03,
		BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_04
	};

	// Token: 0x04003782 RID: 14210
	private List<string> m_VO_BTA_BOSS_18h_IdleLines = new List<string>
	{
		BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleA_01,
		BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleB_01,
		BTA_Fight_18.VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleC_01
	};

	// Token: 0x04003783 RID: 14211
	private HashSet<string> m_playedLines = new HashSet<string>();
}
