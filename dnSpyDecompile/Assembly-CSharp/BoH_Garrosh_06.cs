using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200051D RID: 1309
public class BoH_Garrosh_06 : BoH_Garrosh_Dungeon
{
	// Token: 0x060046FB RID: 18171 RVA: 0x0017E198 File Offset: 0x0017C398
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Garrosh_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeA_01,
			BoH_Garrosh_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeB_01,
			BoH_Garrosh_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeC_01,
			BoH_Garrosh_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeD_01,
			BoH_Garrosh_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeA_01,
			BoH_Garrosh_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Intro_01,
			BoH_Garrosh_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Victory_01,
			BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6EmoteResponse_01,
			BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeC_01,
			BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeD_01,
			BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_01,
			BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_02,
			BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_03,
			BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_01,
			BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_02,
			BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_03,
			BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Loss_01,
			BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Victory_01,
			BoH_Garrosh_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeA_01,
			BoH_Garrosh_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Intro_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060046FC RID: 18172 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060046FD RID: 18173 RVA: 0x0017E33C File Offset: 0x0017C53C
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Garrosh_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060046FE RID: 18174 RVA: 0x0017E34B File Offset: 0x0017C54B
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6IdleLines;
	}

	// Token: 0x060046FF RID: 18175 RVA: 0x0017E353 File Offset: 0x0017C553
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPowerLines;
	}

	// Token: 0x06004700 RID: 18176 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004701 RID: 18177 RVA: 0x0017E35B File Offset: 0x0017C55B
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6EmoteResponse_01;
	}

	// Token: 0x06004702 RID: 18178 RVA: 0x0017E374 File Offset: 0x0017C574
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004703 RID: 18179 RVA: 0x0017E3F8 File Offset: 0x0017C5F8
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield break;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield break;
		}
		switch (missionEvent)
		{
		case 101:
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.7f, 0.7f, 0.7f), 2f);
			yield return new WaitForSeconds(0.5f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.1f, 0.1f, 0.1f), 5f);
			yield return new WaitForSeconds(4f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.25f, 0.25f, 0.25f), 5f);
			break;
		case 102:
			yield return new WaitForSeconds(5f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.4f, 0.4f, 0.4f), 5f);
			break;
		case 103:
			yield return new WaitForSeconds(10f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.6f, 0.6f, 0.6f), 5f);
			break;
		case 104:
			yield return new WaitForSeconds(15f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.8f, 0.8f, 0.8f), 5f);
			break;
		case 105:
			yield return new WaitForSeconds(20f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(1f, 1f, 1f), 5f);
			break;
		default:
			switch (missionEvent)
			{
			case 501:
				yield return base.PlayLineAlways(actor, BoH_Garrosh_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Victory_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Victory_01, 2.5f);
				goto IL_489;
			case 502:
				this.GatesAttackedCounter++;
				if (this.GatesAttackedCounter == 4)
				{
					yield return base.PlayLineAlways(actor, BoH_Garrosh_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeB_01, 2.5f);
					goto IL_489;
				}
				yield return null;
				goto IL_489;
			case 504:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_489;
			}
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		IL_489:
		yield break;
	}

	// Token: 0x06004704 RID: 18180 RVA: 0x0017E40E File Offset: 0x0017C60E
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004705 RID: 18181 RVA: 0x0017E424 File Offset: 0x0017C624
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

	// Token: 0x06004706 RID: 18182 RVA: 0x0017E43A File Offset: 0x0017C63A
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 1:
			yield return base.PlayLineAlways(actor, BoH_Garrosh_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeA_01, 2.5f);
			break;
		case 3:
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeA_01, 2.5f);
			break;
		case 5:
			yield return base.PlayLineAlways(actor, BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeC_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeC_01, 2.5f);
			break;
		case 7:
			yield return base.PlayLineAlways(actor, BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeD_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeD_01, 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x06004707 RID: 18183 RVA: 0x00176C37 File Offset: 0x00174E37
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}

	// Token: 0x04003A6F RID: 14959
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeA_01.prefab:74f7490bd6f8be04c80fc536443bc1bf");

	// Token: 0x04003A70 RID: 14960
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeB_01.prefab:09fdce0758e3f3b45bd8dbc9ea351421");

	// Token: 0x04003A71 RID: 14961
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeC_01.prefab:6909de0b96603ae478a686ad92c3b3c6");

	// Token: 0x04003A72 RID: 14962
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeD_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission6ExchangeD_01.prefab:79f61bece774dd84482b6830ce46d88b");

	// Token: 0x04003A73 RID: 14963
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeA_01.prefab:2470fdc7240cf394b879b0967cd4f840");

	// Token: 0x04003A74 RID: 14964
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Intro_01.prefab:713307af01368b3478d7c3ebf629ddfb");

	// Token: 0x04003A75 RID: 14965
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Victory_01.prefab:ac7a73269ddf1954888c4cedbf847fa4");

	// Token: 0x04003A76 RID: 14966
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6EmoteResponse_01.prefab:2547e740d9d99034288438e91ea31e92");

	// Token: 0x04003A77 RID: 14967
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeC_01.prefab:dc30e96ccf674824e86be402ae9c3ec8");

	// Token: 0x04003A78 RID: 14968
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeD_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6ExchangeD_01.prefab:ec1b5d4d400c0ef47a881eca1e428f07");

	// Token: 0x04003A79 RID: 14969
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_01.prefab:72e2a139edf1b88479c425be20371632");

	// Token: 0x04003A7A RID: 14970
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_02.prefab:1e10320de01b5d44e86e6fe4da874eb3");

	// Token: 0x04003A7B RID: 14971
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_03.prefab:57ee2ce05ffffb54f9c7274fbe463a1f");

	// Token: 0x04003A7C RID: 14972
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_01.prefab:021b12aeb2b04864389130bfb35aaf09");

	// Token: 0x04003A7D RID: 14973
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_02.prefab:801ae8c93536b8d4a872a2376a520801");

	// Token: 0x04003A7E RID: 14974
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_03.prefab:ca8591c4a46d9384b885d55b1829ebda");

	// Token: 0x04003A7F RID: 14975
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Loss_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Loss_01.prefab:4d5f48fe314ad3843bacacf6e4114e08");

	// Token: 0x04003A80 RID: 14976
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Victory_01.prefab:bd74092d27056d747adc83466eb8e6ee");

	// Token: 0x04003A81 RID: 14977
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeA_01.prefab:b9075095164795e4f8adfca7b9df89a1");

	// Token: 0x04003A82 RID: 14978
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Intro_01.prefab:969f3f9d80615ef468767b822e1d23dd");

	// Token: 0x04003A83 RID: 14979
	private int GatesAttackedCounter;

	// Token: 0x04003A84 RID: 14980
	private List<string> m_VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPowerLines = new List<string>
	{
		BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_01,
		BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_02,
		BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6HeroPower_03
	};

	// Token: 0x04003A85 RID: 14981
	private List<string> m_VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6IdleLines = new List<string>
	{
		BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_01,
		BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_02,
		BoH_Garrosh_06.VO_Story_Hero_Jaina_Female_Human_Story_Garrosh_Mission6Idle_03
	};

	// Token: 0x04003A86 RID: 14982
	private HashSet<string> m_playedLines = new HashSet<string>();
}
