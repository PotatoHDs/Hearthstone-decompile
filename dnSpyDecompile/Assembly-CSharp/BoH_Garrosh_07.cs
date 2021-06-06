using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200051E RID: 1310
public class BoH_Garrosh_07 : BoH_Garrosh_Dungeon
{
	// Token: 0x0600470D RID: 18189 RVA: 0x0017E620 File Offset: 0x0017C820
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7EmoteResponse_01,
			BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01,
			BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01,
			BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02,
			BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_01,
			BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_02,
			BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_03,
			BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_01,
			BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_02,
			BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_03,
			BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01,
			BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Loss_01,
			BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01,
			BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02,
			BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01,
			BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01,
			BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01,
			BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02,
			BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03,
			BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600470E RID: 18190 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600470F RID: 18191 RVA: 0x0017E7C4 File Offset: 0x0017C9C4
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004710 RID: 18192 RVA: 0x0017E7D3 File Offset: 0x0017C9D3
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7IdleLines;
	}

	// Token: 0x06004711 RID: 18193 RVA: 0x0017E7DB File Offset: 0x0017C9DB
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPowerLines;
	}

	// Token: 0x06004712 RID: 18194 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004713 RID: 18195 RVA: 0x0017E7E3 File Offset: 0x0017C9E3
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7EmoteResponse_01;
	}

	// Token: 0x06004714 RID: 18196 RVA: 0x0017E7FC File Offset: 0x0017C9FC
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

	// Token: 0x06004715 RID: 18197 RVA: 0x0017E880 File Offset: 0x0017CA80
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 112)
		{
			if (missionEvent != 501)
			{
				if (missionEvent != 504)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(actor, BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Loss_01, 2.5f);
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor2, BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor2, this.m_missionEventTrigger502Lines);
		}
		yield break;
	}

	// Token: 0x06004716 RID: 18198 RVA: 0x0017E896 File Offset: 0x0017CA96
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

	// Token: 0x06004717 RID: 18199 RVA: 0x0017E8AC File Offset: 0x0017CAAC
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

	// Token: 0x06004718 RID: 18200 RVA: 0x0017E8C2 File Offset: 0x0017CAC2
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
			if (turn != 5)
			{
				if (turn == 9)
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01, 2.5f);
					yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004719 RID: 18201 RVA: 0x0016110D File Offset: 0x0015F30D
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRG);
	}

	// Token: 0x04003A87 RID: 14983
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7EmoteResponse_01.prefab:a932c77d70720da4589486633ed1e7e9");

	// Token: 0x04003A88 RID: 14984
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01.prefab:9571179a848137b43a0458654e27365d");

	// Token: 0x04003A89 RID: 14985
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01.prefab:e043aa0297f133e4289883dcdd532ea8");

	// Token: 0x04003A8A RID: 14986
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02.prefab:b1d5670fbbb1e4649bb92078e71120c6");

	// Token: 0x04003A8B RID: 14987
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_01.prefab:27e1ba1890eb9bb4fb374a12c35dd7a5");

	// Token: 0x04003A8C RID: 14988
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_02.prefab:b5cf47dc7e289014988bee1b5eb5dd83");

	// Token: 0x04003A8D RID: 14989
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_03.prefab:f5ff18556f3d32942820785cf22ea405");

	// Token: 0x04003A8E RID: 14990
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_01.prefab:9a19642236c02fe418745e7e11602195");

	// Token: 0x04003A8F RID: 14991
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_02.prefab:0dc91717346316847b1888503adf81ad");

	// Token: 0x04003A90 RID: 14992
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_03.prefab:5c721ed34bf80b94ca22d377a903cfe8");

	// Token: 0x04003A91 RID: 14993
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01.prefab:7d5847a2087e80941beddff135e0ce41");

	// Token: 0x04003A92 RID: 14994
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Loss_01.prefab:66f6e13da484def49ae357f5bddddf37");

	// Token: 0x04003A93 RID: 14995
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01.prefab:30f58743aba7f9e4e9c2c6c77ab5034d");

	// Token: 0x04003A94 RID: 14996
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02.prefab:e427713bab99d9e47ab6a960bb182063");

	// Token: 0x04003A95 RID: 14997
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01.prefab:6c5ffb3161419ae4490dbe6cfd1a15aa");

	// Token: 0x04003A96 RID: 14998
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01.prefab:13f81fc1c76cba04e8b15ad99a81d423");

	// Token: 0x04003A97 RID: 14999
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01.prefab:5b39f80bab51d9c4b80eb758a6f72183");

	// Token: 0x04003A98 RID: 15000
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02.prefab:64e3347a98a90114d8089589e8f51b30");

	// Token: 0x04003A99 RID: 15001
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03.prefab:b29bb9bb3386f1d42a4b4b7e54173c39");

	// Token: 0x04003A9A RID: 15002
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01.prefab:eebbe511e35670b48bc483037a87dc6d");

	// Token: 0x04003A9B RID: 15003
	private List<string> m_VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPowerLines = new List<string>
	{
		BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_01,
		BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_02,
		BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_03
	};

	// Token: 0x04003A9C RID: 15004
	private List<string> m_VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7IdleLines = new List<string>
	{
		BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_01,
		BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_02,
		BoH_Garrosh_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_03
	};

	// Token: 0x04003A9D RID: 15005
	private List<string> m_missionEventTrigger502Lines = new List<string>
	{
		BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01,
		BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02,
		BoH_Garrosh_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03
	};

	// Token: 0x04003A9E RID: 15006
	private HashSet<string> m_playedLines = new HashSet<string>();
}
