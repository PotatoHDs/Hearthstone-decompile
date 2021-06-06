using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200054A RID: 1354
public class BoH_Uther_01 : BoH_Uther_Dungeon
{
	// Token: 0x06004A83 RID: 19075 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004A84 RID: 19076 RVA: 0x0018C224 File Offset: 0x0018A424
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1EmoteResponse_01,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeA_01,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeB_01,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeC_01,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeD_01,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeE_01,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_01,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_02,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_03,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_01,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_02,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_03,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Intro_01,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Loss_01,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_01,
			BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_02,
			BoH_Uther_01.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeA_01,
			BoH_Uther_01.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeB_01,
			BoH_Uther_01.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Victory_01,
			BoH_Uther_01.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Intro_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004A85 RID: 19077 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004A86 RID: 19078 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004A87 RID: 19079 RVA: 0x0018C3C8 File Offset: 0x0018A5C8
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Uther_01.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Intro_02, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004A88 RID: 19080 RVA: 0x0018C3D7 File Offset: 0x0018A5D7
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004A89 RID: 19081 RVA: 0x0018C3DF File Offset: 0x0018A5DF
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004A8A RID: 19082 RVA: 0x0018C3E7 File Offset: 0x0018A5E7
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1EmoteResponse_01;
	}

	// Token: 0x06004A8B RID: 19083 RVA: 0x0018C400 File Offset: 0x0018A600
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004A8C RID: 19084 RVA: 0x0018C489 File Offset: 0x0018A689
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 501:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(enemyActor, BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_02, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_1D1;
		case 502:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Uther_01.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_1D1;
		case 504:
			yield return base.PlayLineAlways(enemyActor, BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Loss_01, 2.5f);
			goto IL_1D1;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_1D1:
		yield break;
	}

	// Token: 0x06004A8D RID: 19085 RVA: 0x0018C49F File Offset: 0x0018A69F
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
		if (!(cardId == "CS1_112"))
		{
			if (cardId == "GIL_134")
			{
				yield return base.PlayLineOnlyOnce(actor, BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeD_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeE_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004A8E RID: 19086 RVA: 0x0018C4B5 File Offset: 0x0018A6B5
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

	// Token: 0x06004A8F RID: 19087 RVA: 0x0018C4CB File Offset: 0x0018A6CB
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 3:
			yield return base.PlayLineAlways(enemyActor, BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_01.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeA_01, 2.5f);
			break;
		case 5:
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_01.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeB_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeB_01, 2.5f);
			break;
		case 7:
			yield return base.PlayLineAlways(enemyActor, BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeC_01, 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x06004A90 RID: 19088 RVA: 0x0018C4E1 File Offset: 0x0018A6E1
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_Default);
	}

	// Token: 0x04003ED3 RID: 16083
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Uther_01.InitBooleanOptions();

	// Token: 0x04003ED4 RID: 16084
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1EmoteResponse_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1EmoteResponse_01.prefab:6ebb0fea32ff4c349a5f8b7aae619843");

	// Token: 0x04003ED5 RID: 16085
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeA_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeA_01.prefab:2ba6e169726790d4081bb1cac793f094");

	// Token: 0x04003ED6 RID: 16086
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeB_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeB_01.prefab:b8152f45c74e36a45a9fa9c45aa1c557");

	// Token: 0x04003ED7 RID: 16087
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeC_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeC_01.prefab:f5dfede0433c44e419a78a6bf5fe4a15");

	// Token: 0x04003ED8 RID: 16088
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeD_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeD_01.prefab:7ff78212de1509b47b49209e8ebc8633");

	// Token: 0x04003ED9 RID: 16089
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeE_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeE_01.prefab:9741febdfc8cb7a479d151e11aec3fd2");

	// Token: 0x04003EDA RID: 16090
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_01.prefab:b2acb68be5981164d9931948276c8890");

	// Token: 0x04003EDB RID: 16091
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_02 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_02.prefab:208df8702b71e4b48993e93d43c4dfcc");

	// Token: 0x04003EDC RID: 16092
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_03 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_03.prefab:39424ef839485854f82d6c7d6e72b488");

	// Token: 0x04003EDD RID: 16093
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Intro_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Intro_01.prefab:3bbe739b088c05f44806dca970eac7bd");

	// Token: 0x04003EDE RID: 16094
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_01.prefab:df701de681b58da459fc60154affd95e");

	// Token: 0x04003EDF RID: 16095
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_02 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_02.prefab:6675540aeaa9e07449cdc618884bc1e4");

	// Token: 0x04003EE0 RID: 16096
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_03 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_03.prefab:4a74f92b61f445b448ee28e2472bd1a2");

	// Token: 0x04003EE1 RID: 16097
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Loss_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Loss_01.prefab:b85153205ec2b90428275f66eb2f8934");

	// Token: 0x04003EE2 RID: 16098
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_01.prefab:53b892d5f2654a64398284400c50f79d");

	// Token: 0x04003EE3 RID: 16099
	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_02 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_02.prefab:e2eea205b5c49df4da4c4222644b215f");

	// Token: 0x04003EE4 RID: 16100
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeA_01.prefab:95a4e131e994da644bc27f987e6027a3");

	// Token: 0x04003EE5 RID: 16101
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeB_01.prefab:1de7f27b13ca5c7429bbd315732a63f3");

	// Token: 0x04003EE6 RID: 16102
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Victory_01.prefab:e1e380f1f4b12b848885890489d6eb9a");

	// Token: 0x04003EE7 RID: 16103
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Intro_02.prefab:b686e817e1c779d43acb076469b6d72f");

	// Token: 0x04003EE8 RID: 16104
	private List<string> m_HeroPowerLines = new List<string>
	{
		BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_01,
		BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_02,
		BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_03
	};

	// Token: 0x04003EE9 RID: 16105
	private List<string> m_IdleLines = new List<string>
	{
		BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_01,
		BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_02,
		BoH_Uther_01.VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_03
	};

	// Token: 0x04003EEA RID: 16106
	private HashSet<string> m_playedLines = new HashSet<string>();
}
