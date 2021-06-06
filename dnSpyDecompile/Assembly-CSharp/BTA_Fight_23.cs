using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000501 RID: 1281
public class BTA_Fight_23 : BTA_Dungeon_Heroic
{
	// Token: 0x060044F2 RID: 17650 RVA: 0x00175560 File Offset: 0x00173760
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Boss_Attack_01,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossDeath_01,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStart_01,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStartDemonHunter_01,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Emote_Response_01,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_Bladestorm_01,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_LightsChampion_01,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_StolenSteel_01,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_01,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_02,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_03,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_04,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleA_01,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleB_01,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleC_01,
			BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_UI_Mission_Fight_23_CoinSelect_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060044F3 RID: 17651 RVA: 0x001756C4 File Offset: 0x001738C4
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_23h_IdleLines;
	}

	// Token: 0x060044F4 RID: 17652 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060044F5 RID: 17653 RVA: 0x001756CC File Offset: 0x001738CC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossDeath_01;
		this.m_standardEmoteResponseLine = BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Emote_Response_01;
	}

	// Token: 0x060044F6 RID: 17654 RVA: 0x001756F4 File Offset: 0x001738F4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "HERO_10" || cardId == "HERO_10a" || cardId == "HERO_10b")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStartDemonHunter_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x060044F7 RID: 17655 RVA: 0x00175804 File Offset: 0x00173A04
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
				base.PlaySound(BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Boss_Attack_01, 1f, true, false);
			}
		}
		else
		{
			yield return base.PlayLineInOrderOnce(actor, this.m_VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_WeaponDestroy);
		}
		yield break;
	}

	// Token: 0x060044F8 RID: 17656 RVA: 0x0017581A File Offset: 0x00173A1A
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
		if (cardId == "BT_117")
		{
			yield return base.PlayLineOnlyOnce(actor, BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_Bladestorm_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060044F9 RID: 17657 RVA: 0x00175830 File Offset: 0x00173A30
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x060044FA RID: 17658 RVA: 0x00175846 File Offset: 0x00173A46
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

	// Token: 0x040037CD RID: 14285
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Boss_Attack_01.prefab:0ab275c1a9c09324c90c9f473801367d");

	// Token: 0x040037CE RID: 14286
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossDeath_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossDeath_01.prefab:bb35237313dfd7d488cb61ca0aff4640");

	// Token: 0x040037CF RID: 14287
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStart_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStart_01.prefab:e7c86907f2c517a4b92c1b4dddcaff05");

	// Token: 0x040037D0 RID: 14288
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStartDemonHunter_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStartDemonHunter_01.prefab:ce4f2428ca4b0ad45b52cd05492403bf");

	// Token: 0x040037D1 RID: 14289
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Emote_Response_01.prefab:2d0d469f9e1076a41a9618ffaafd542d");

	// Token: 0x040037D2 RID: 14290
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_Bladestorm_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_Bladestorm_01.prefab:7259cd5165ef06840b83cf49afa4b7b2");

	// Token: 0x040037D3 RID: 14291
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_LightsChampion_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_LightsChampion_01.prefab:c14def875a76cc14faaead0534261c29");

	// Token: 0x040037D4 RID: 14292
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_StolenSteel_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_StolenSteel_01.prefab:b72cd9ac8ca920745aecb99ab99ea920");

	// Token: 0x040037D5 RID: 14293
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_01.prefab:757a239fd530b664f8a30160ebe84160");

	// Token: 0x040037D6 RID: 14294
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_02 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_02.prefab:65e625c6021c3b040b04891fb5ad1254");

	// Token: 0x040037D7 RID: 14295
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_03 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_03.prefab:63c3f3a9c20770847a240f7d0da8e467");

	// Token: 0x040037D8 RID: 14296
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_04 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_04.prefab:6e7f6f9d93f52804295821f8db9c7943");

	// Token: 0x040037D9 RID: 14297
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleA_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleA_01.prefab:df0639a438208ce4990b599d1c5a24de");

	// Token: 0x040037DA RID: 14298
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleB_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleB_01.prefab:caa9d5ffa48b0ff47aeb88df0b875996");

	// Token: 0x040037DB RID: 14299
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleC_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleC_01.prefab:b9c83c40dfdc537468ad758526fa05d7");

	// Token: 0x040037DC RID: 14300
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_UI_Mission_Fight_23_CoinSelect_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_UI_Mission_Fight_23_CoinSelect_01.prefab:e4e2166f87a9bd04e9eb417285950635");

	// Token: 0x040037DD RID: 14301
	private List<string> m_VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_WeaponDestroy = new List<string>
	{
		BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_LightsChampion_01,
		BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_StolenSteel_01
	};

	// Token: 0x040037DE RID: 14302
	private List<string> m_missionEventTrigger507Lines = new List<string>
	{
		BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_01,
		BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_02,
		BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_03,
		BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_04
	};

	// Token: 0x040037DF RID: 14303
	private List<string> m_VO_BTA_BOSS_23h_IdleLines = new List<string>
	{
		BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleA_01,
		BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleB_01,
		BTA_Fight_23.VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleC_01
	};

	// Token: 0x040037E0 RID: 14304
	private HashSet<string> m_playedLines = new HashSet<string>();
}
