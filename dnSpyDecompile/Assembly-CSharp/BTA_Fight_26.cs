using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000504 RID: 1284
public class BTA_Fight_26 : BTA_Dungeon_Heroic
{
	// Token: 0x0600451B RID: 17691 RVA: 0x001765D8 File Offset: 0x001747D8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_Attack_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_ChaosNova_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_InnerDemon_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_Metamorphosis_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_SkullofGuldan_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_SpectralSight_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_YouAreNotPrepared_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossDeath_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossStart_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossStartArthas_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossStartMaiev_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Emote_Response_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Hero_Akama_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_02,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_03,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_04,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_IdleA_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_IdleB_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_IdleC_01,
			BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_UI_Mission_Fight_26_CoinSelect_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600451C RID: 17692 RVA: 0x0017678C File Offset: 0x0017498C
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_26h_IdleLines;
	}

	// Token: 0x0600451D RID: 17693 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600451E RID: 17694 RVA: 0x00176794 File Offset: 0x00174994
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossDeath_01;
		this.m_standardEmoteResponseLine = BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Emote_Response_01;
	}

	// Token: 0x0600451F RID: 17695 RVA: 0x001767BC File Offset: 0x001749BC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "HERO_04b")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossStartArthas_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "HERO_03a")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossStartMaiev_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06004520 RID: 17696 RVA: 0x001768EE File Offset: 0x00174AEE
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 161)
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
				base.PlaySound(BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_Attack_01, 1f, true, false);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_InnerDemon);
		}
		yield break;
	}

	// Token: 0x06004521 RID: 17697 RVA: 0x00176904 File Offset: 0x00174B04
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
		if (cardId == "BT_713")
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Hero_Akama_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004522 RID: 17698 RVA: 0x0017691A File Offset: 0x00174B1A
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
		if (!(cardId == "BTA_BOSS_26s"))
		{
			if (!(cardId == "BT_429"))
			{
				if (!(cardId == "BT_491"))
				{
					if (cardId == "BT_601")
					{
						yield return base.PlayLineAlways(actor, BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_SkullofGuldan_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_SpectralSight_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_Metamorphosis_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_YouAreNotPrepared_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004523 RID: 17699 RVA: 0x00176930 File Offset: 0x00174B30
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

	// Token: 0x04003815 RID: 14357
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_Attack_01.prefab:ee76810f02a0ba748babf88b8d13ed4d");

	// Token: 0x04003816 RID: 14358
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_ChaosNova_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_ChaosNova_01.prefab:65614c4fdbebe8f4c99d9135a1e2b4b2");

	// Token: 0x04003817 RID: 14359
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_InnerDemon_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_InnerDemon_01.prefab:ba9911180c4c0324ebac3644c628b18a");

	// Token: 0x04003818 RID: 14360
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_Metamorphosis_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_Metamorphosis_01.prefab:8936b149faf735046b09a1f29db80a7e");

	// Token: 0x04003819 RID: 14361
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_SkullofGuldan_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_SkullofGuldan_01.prefab:82e1af5cdc70ad947b6f58d42ccace02");

	// Token: 0x0400381A RID: 14362
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_SpectralSight_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_SpectralSight_01.prefab:002f5db33f9866a4ab967f1a098c0036");

	// Token: 0x0400381B RID: 14363
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_YouAreNotPrepared_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_YouAreNotPrepared_01.prefab:c9dbf36fb40b1b64ab7780969fa65c26");

	// Token: 0x0400381C RID: 14364
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossDeath_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossDeath_01.prefab:d4ecd250d23994945b4ad186f38c8ce3");

	// Token: 0x0400381D RID: 14365
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossStart_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossStart_01.prefab:2b0459206e474d546b90d55bf9b3c317");

	// Token: 0x0400381E RID: 14366
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossStartArthas_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossStartArthas_01.prefab:d024bda905fefd64da7315a7a17f1f35");

	// Token: 0x0400381F RID: 14367
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossStartMaiev_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_BossStartMaiev_01.prefab:0a16efcd2ae2d814ca96be22ef01e5ed");

	// Token: 0x04003820 RID: 14368
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Emote_Response_01.prefab:4e5b5dd8953d3914ea93b895e5d40635");

	// Token: 0x04003821 RID: 14369
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Hero_Akama_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Hero_Akama_01.prefab:50819e3f29e238249a66796fc88e902b");

	// Token: 0x04003822 RID: 14370
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_01.prefab:b8f3cf6c5f066eb49990304505e4f72c");

	// Token: 0x04003823 RID: 14371
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_02 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_02.prefab:c7308a14d565c834fb1aadf41fe530eb");

	// Token: 0x04003824 RID: 14372
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_03 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_03.prefab:3f4a7a416d8da494f9cfc59fe244c397");

	// Token: 0x04003825 RID: 14373
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_04 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_04.prefab:bbdf279a156c844499370d4ae01cfa71");

	// Token: 0x04003826 RID: 14374
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_IdleA_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_IdleA_01.prefab:be61259e181b6dc41930db91d10c652d");

	// Token: 0x04003827 RID: 14375
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_IdleB_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_IdleB_01.prefab:f1236a66eda34ca4c801823ffaa08dfd");

	// Token: 0x04003828 RID: 14376
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_IdleC_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_IdleC_01.prefab:d9754730b5485f54c96e772d1e472982");

	// Token: 0x04003829 RID: 14377
	private static readonly AssetReference VO_BTA_BOSS_17hx_Male_NightElf_UI_Mission_Fight_26_CoinSelect_01 = new AssetReference("VO_BTA_BOSS_17hx_Male_NightElf_UI_Mission_Fight_26_CoinSelect_01.prefab:780367df262656b468fa8ae84e6e6ecb");

	// Token: 0x0400382A RID: 14378
	private List<string> m_VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_InnerDemon = new List<string>
	{
		BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_ChaosNova_01,
		BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_Boss_InnerDemon_01
	};

	// Token: 0x0400382B RID: 14379
	private List<string> m_missionEventTrigger507Lines = new List<string>
	{
		BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_01,
		BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_02,
		BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_03,
		BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_HeroPower_04
	};

	// Token: 0x0400382C RID: 14380
	private List<string> m_VO_BTA_BOSS_26h_IdleLines = new List<string>
	{
		BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_IdleA_01,
		BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_IdleB_01,
		BTA_Fight_26.VO_BTA_BOSS_17hx_Male_NightElf_Mission_Fight_26_IdleC_01
	};

	// Token: 0x0400382D RID: 14381
	private HashSet<string> m_playedLines = new HashSet<string>();
}
