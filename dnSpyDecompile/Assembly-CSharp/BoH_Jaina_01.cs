using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000522 RID: 1314
public class BoH_Jaina_01 : BoH_Jaina_Dungeon
{
	// Token: 0x06004756 RID: 18262 RVA: 0x0016DED1 File Offset: 0x0016C0D1
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			}
		};
	}

	// Token: 0x06004757 RID: 18263 RVA: 0x0017F6D0 File Offset: 0x0017D8D0
	public BoH_Jaina_01()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Jaina_01.s_booleanOptions);
	}

	// Token: 0x06004758 RID: 18264 RVA: 0x0017F774 File Offset: 0x0017D974
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1EmoteResponse_01,
			BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeB_01,
			BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_01,
			BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_02,
			BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeD_01,
			BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_01,
			BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_02,
			BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_03,
			BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_01,
			BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_02,
			BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_03,
			BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Intro_01,
			BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Loss_01,
			BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Victory_01,
			BoH_Jaina_01.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeA_01,
			BoH_Jaina_01.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeB_01,
			BoH_Jaina_01.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeC_01,
			BoH_Jaina_01.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeD_01,
			BoH_Jaina_01.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Intro_01,
			BoH_Jaina_01.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004759 RID: 18265 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x0600475A RID: 18266 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600475B RID: 18267 RVA: 0x0017F918 File Offset: 0x0017DB18
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Jaina_01.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600475C RID: 18268 RVA: 0x0017F927 File Offset: 0x0017DB27
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1IdleLines;
	}

	// Token: 0x0600475D RID: 18269 RVA: 0x0017F930 File Offset: 0x0017DB30
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600475E RID: 18270 RVA: 0x0017F98E File Offset: 0x0017DB8E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 501)
		{
			if (missionEvent != 504)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_01.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x0600475F RID: 18271 RVA: 0x0017F9A4 File Offset: 0x0017DBA4
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "CS2_022")
		{
			yield return base.PlayLineAlways(actor, BoH_Jaina_01.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeB_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeB_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004760 RID: 18272 RVA: 0x0017F9BA File Offset: 0x0017DBBA
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

	// Token: 0x06004761 RID: 18273 RVA: 0x0017F9D0 File Offset: 0x0017DBD0
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn != 4)
			{
				if (turn == 8)
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeD_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_01.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeD_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_01.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeC_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_02, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_01.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004762 RID: 18274 RVA: 0x0017F9E6 File Offset: 0x0017DBE6
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOOT);
	}

	// Token: 0x04003ABF RID: 15039
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Jaina_01.InitBooleanOptions();

	// Token: 0x04003AC0 RID: 15040
	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1EmoteResponse_01.prefab:f1f2317b5b0ba184491b8941365e4fcf");

	// Token: 0x04003AC1 RID: 15041
	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeB_01.prefab:7679f8bc10b261847a1bbedb56703fb9");

	// Token: 0x04003AC2 RID: 15042
	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_01.prefab:85e128b518b235a4cbf3f6bdfe0b79d5");

	// Token: 0x04003AC3 RID: 15043
	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_02 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_02.prefab:82e538569752834409aae1b9cec23bd4");

	// Token: 0x04003AC4 RID: 15044
	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeD_01.prefab:7373bf47010e1b24e90794a979f8cd75");

	// Token: 0x04003AC5 RID: 15045
	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_01.prefab:c3b674754ac433a4d824c8580bd8be03");

	// Token: 0x04003AC6 RID: 15046
	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_02 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_02.prefab:54ec7e86cdc402544a65b4e0e5bbbf43");

	// Token: 0x04003AC7 RID: 15047
	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_03.prefab:3079d3a5abd135140a4da7a737043ff8");

	// Token: 0x04003AC8 RID: 15048
	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_01.prefab:876d12f76dc335b4cbe358410ea92107");

	// Token: 0x04003AC9 RID: 15049
	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_02.prefab:271fb8e968323c7438d36ff3048c77b1");

	// Token: 0x04003ACA RID: 15050
	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_03.prefab:07882d79818737540960aff35d337c3a");

	// Token: 0x04003ACB RID: 15051
	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Intro_01.prefab:0c52ab1ee86d2b34fa39057070412b0b");

	// Token: 0x04003ACC RID: 15052
	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Loss_01.prefab:d62e0c75e50c74c498b1ccbb2b3a4716");

	// Token: 0x04003ACD RID: 15053
	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Victory_01.prefab:f289444beb58ca34e92119f050293976");

	// Token: 0x04003ACE RID: 15054
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeA_01.prefab:bd746621980b018419f546ed29e7f7dc");

	// Token: 0x04003ACF RID: 15055
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeB_01.prefab:bf22297af6482f4469fa0eb00727d364");

	// Token: 0x04003AD0 RID: 15056
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeC_01.prefab:b6226a591581f224fa612b470d5fd503");

	// Token: 0x04003AD1 RID: 15057
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeD_01.prefab:4cff05949da711947a68d497b972a232");

	// Token: 0x04003AD2 RID: 15058
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Intro_01.prefab:64e77102b5930944094ca5ba65fef26b");

	// Token: 0x04003AD3 RID: 15059
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Victory_01.prefab:d44807878b072b543a03f860064d5f54");

	// Token: 0x04003AD4 RID: 15060
	private List<string> m_VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPowerLines = new List<string>
	{
		BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_01,
		BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_02,
		BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_03
	};

	// Token: 0x04003AD5 RID: 15061
	private List<string> m_VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1IdleLines = new List<string>
	{
		BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_01,
		BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_02,
		BoH_Jaina_01.VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_03
	};

	// Token: 0x04003AD6 RID: 15062
	private HashSet<string> m_playedLines = new HashSet<string>();
}
