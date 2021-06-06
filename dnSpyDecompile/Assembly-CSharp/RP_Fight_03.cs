using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000596 RID: 1430
public class RP_Fight_03 : RP_Dungeon
{
	// Token: 0x06004F72 RID: 20338 RVA: 0x001A1570 File Offset: 0x0019F770
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeA_02_01,
			RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeC_02_01,
			RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro01_01,
			RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro03_01,
			RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn01_Response_01,
			RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn02_Intro_01,
			RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Victory_01,
			RP_Fight_03.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission3_Turn01_Intro_01,
			RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeA_01_01,
			RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeB_01_01,
			RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeC_01_01,
			RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Intro02_01,
			RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Turn02_Response_01,
			RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Death_01,
			RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_EmoteResponse_01,
			RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_01_01,
			RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_02_01,
			RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_03_01,
			RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_01_01,
			RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_02_01,
			RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_03_01,
			RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Loss_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004F73 RID: 20339 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004F74 RID: 20340 RVA: 0x001A1734 File Offset: 0x0019F934
	public override List<string> GetIdleLines()
	{
		return this.m_VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_Lines;
	}

	// Token: 0x06004F75 RID: 20341 RVA: 0x001A173C File Offset: 0x0019F93C
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_Lines;
	}

	// Token: 0x06004F76 RID: 20342 RVA: 0x001A1744 File Offset: 0x0019F944
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Death_01;
	}

	// Token: 0x06004F77 RID: 20343 RVA: 0x001A175C File Offset: 0x0019F95C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004F78 RID: 20344 RVA: 0x001A17BF File Offset: 0x0019F9BF
	protected IEnumerator PlayMultipleVOLinesForEmotes(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		yield return base.PlayLineAlways(RP_Fight_03.IllidanBrassRing, RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro01_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Intro02_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro03_01, 2.5f);
		yield break;
	}

	// Token: 0x06004F79 RID: 20345 RVA: 0x001A17CE File Offset: 0x0019F9CE
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 501:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(RP_Fight_03.IllidanBrassRing, RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_23C;
		case 503:
			yield return new WaitForSeconds(7f);
			yield return base.PlayLineAlways(RP_Fight_03.IllidanBrassRing, RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro01_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Intro02_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro03_01, 2.5f);
			goto IL_23C;
		case 504:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(friendlyActor, RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Loss_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_23C;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_23C:
		yield break;
	}

	// Token: 0x06004F7A RID: 20346 RVA: 0x001A17E4 File Offset: 0x0019F9E4
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

	// Token: 0x06004F7B RID: 20347 RVA: 0x001A17FA File Offset: 0x0019F9FA
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

	// Token: 0x06004F7C RID: 20348 RVA: 0x001A1810 File Offset: 0x0019FA10
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn <= 3)
		{
			if (turn != 1)
			{
				if (turn == 3)
				{
					yield return base.PlayLineAlways(friendlyActor, RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn02_Intro_01, 2.5f);
					yield return base.PlayLineAlways(enemyActor, RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Turn02_Response_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(RP_Fight_03.MalfurionBrassRing, RP_Fight_03.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission3_Turn01_Intro_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn01_Response_01, 2.5f);
			}
		}
		else if (turn != 6)
		{
			if (turn != 11)
			{
				if (turn == 15)
				{
					yield return base.PlayLineAlways(enemyActor, RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeC_01_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeC_02_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeB_01_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(enemyActor, RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeA_01_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, RP_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeA_02_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004F7D RID: 20349 RVA: 0x00173DAB File Offset: 0x00171FAB
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHPrologueBoss);
	}

	// Token: 0x04004595 RID: 17813
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeA_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeA_02_01.prefab:864ef6f49a406a843a710af7d097b76d");

	// Token: 0x04004596 RID: 17814
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeC_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_ExchangeC_02_01.prefab:e2f719220d6340c4f9bd920d88ead4ac");

	// Token: 0x04004597 RID: 17815
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro01_01.prefab:da08bc587092cb14780174b64e830ef9");

	// Token: 0x04004598 RID: 17816
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro03_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Intro03_01.prefab:ba8d6be861f6e9e4f80ef7817b5883b3");

	// Token: 0x04004599 RID: 17817
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn01_Response_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn01_Response_01.prefab:75de0e2fe759a214c9653c305353207b");

	// Token: 0x0400459A RID: 17818
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn02_Intro_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Turn02_Intro_01.prefab:6bbad5573a5b4e4458f38c2c457b7663");

	// Token: 0x0400459B RID: 17819
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Victory_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Victory_01.prefab:86ee6faadc0e2b642bf2e14d4cb2c39e");

	// Token: 0x0400459C RID: 17820
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission3_Turn01_Intro_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission3_Turn01_Intro_01.prefab:103677f2477b355499d45c9c2196e3cb");

	// Token: 0x0400459D RID: 17821
	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeA_01_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeA_01_01.prefab:4b25ab45deac7654a8ce0c57c1970c9f");

	// Token: 0x0400459E RID: 17822
	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeB_01_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeB_01_01.prefab:c8fc5b1878323cb4f912085b640e10b4");

	// Token: 0x0400459F RID: 17823
	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeC_01_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_ExchangeC_01_01.prefab:82462f2062fad824584dbfb424029179");

	// Token: 0x040045A0 RID: 17824
	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Intro02_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Intro02_01.prefab:b151abfab37932a4c81cdd03797febf7");

	// Token: 0x040045A1 RID: 17825
	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Turn02_Response_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_RP_Mission3_Turn02_Response_01.prefab:bf5e1e8c0c3751a439ad492a386e137a");

	// Token: 0x040045A2 RID: 17826
	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Death_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Death_01.prefab:90ac6117d2587dc45b2ea5db27a3ad47");

	// Token: 0x040045A3 RID: 17827
	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_EmoteResponse_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_EmoteResponse_01.prefab:ea566184267863141969ade661c7ad46");

	// Token: 0x040045A4 RID: 17828
	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_01_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_01_01.prefab:29891c2b1e96fc744b46c350f9322cf5");

	// Token: 0x040045A5 RID: 17829
	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_02_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_02_01.prefab:49140cd1ef5700e47912e12c3a8fc256");

	// Token: 0x040045A6 RID: 17830
	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_03_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_03_01.prefab:38235784bde88da4389f99c72727e087");

	// Token: 0x040045A7 RID: 17831
	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_01_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_01_01.prefab:58ee5e57c59dbf342a42bfdc9d08028d");

	// Token: 0x040045A8 RID: 17832
	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_02_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_02_01.prefab:309f10957d14c434aa90ab69cfb672ce");

	// Token: 0x040045A9 RID: 17833
	private static readonly AssetReference VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_03_01 = new AssetReference("VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_03_01.prefab:c63dd330438410c42845403a3b755dfa");

	// Token: 0x040045AA RID: 17834
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Loss_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission3_Loss_01.prefab:cd1be84f2f62d8043ad896a21db67742");

	// Token: 0x040045AB RID: 17835
	public static readonly AssetReference IllidanBrassRing = new AssetReference("DemonHunter_Illidan_Popup_BrassRing.prefab:8c007b8e8be417c4fbd9738960e6f7f0");

	// Token: 0x040045AC RID: 17836
	public static readonly AssetReference MalfurionBrassRing = new AssetReference("YoungMalfurion_Popup_BrassRing.prefab:5544ac85196277542a7fa0b1a9b578df");

	// Token: 0x040045AD RID: 17837
	private List<string> m_VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_Lines = new List<string>
	{
		RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_01_01,
		RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_02_01,
		RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_HeroPower_03_01
	};

	// Token: 0x040045AE RID: 17838
	private List<string> m_VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_Lines = new List<string>
	{
		RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_01_01,
		RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_02_01,
		RP_Fight_03.VO_TB_ReturningPlayer_Houndmastr2_Male_Demon_TB_ReturningPlayer_Houndmastr2_Idle_03_01
	};

	// Token: 0x040045AF RID: 17839
	private HashSet<string> m_playedLines = new HashSet<string>();
}
