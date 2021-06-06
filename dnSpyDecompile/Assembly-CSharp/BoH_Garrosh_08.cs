using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200051F RID: 1311
public class BoH_Garrosh_08 : BoH_Garrosh_Dungeon
{
	// Token: 0x0600471F RID: 18207 RVA: 0x0017EAE4 File Offset: 0x0017CCE4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Garrosh_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeA_01,
			BoH_Garrosh_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_01,
			BoH_Garrosh_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_02,
			BoH_Garrosh_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeC_01,
			BoH_Garrosh_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8Intro_01,
			BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8EmoteResponse_01,
			BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeA_01,
			BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeC_01,
			BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_01,
			BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_02,
			BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_03,
			BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_01,
			BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_02,
			BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_03,
			BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Intro_01,
			BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Loss_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004720 RID: 18208 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004721 RID: 18209 RVA: 0x0017EC48 File Offset: 0x0017CE48
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004722 RID: 18210 RVA: 0x0017EC57 File Offset: 0x0017CE57
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8IdleLines;
	}

	// Token: 0x06004723 RID: 18211 RVA: 0x0017EC5F File Offset: 0x0017CE5F
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPowerLines;
	}

	// Token: 0x06004724 RID: 18212 RVA: 0x0017EC67 File Offset: 0x0017CE67
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8EmoteResponse_01;
	}

	// Token: 0x06004725 RID: 18213 RVA: 0x0017EC80 File Offset: 0x0017CE80
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

	// Token: 0x06004726 RID: 18214 RVA: 0x0017ED04 File Offset: 0x0017CF04
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 502)
		{
			if (missionEvent != 504)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Garrosh_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeC_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeC_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004727 RID: 18215 RVA: 0x0017ED1A File Offset: 0x0017CF1A
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

	// Token: 0x06004728 RID: 18216 RVA: 0x0017ED30 File Offset: 0x0017CF30
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

	// Token: 0x06004729 RID: 18217 RVA: 0x0017ED46 File Offset: 0x0017CF46
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 3)
		{
			if (turn == 9)
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_02, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600472A RID: 18218 RVA: 0x0017ED5C File Offset: 0x0017CF5C
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_TRLFinalBoss);
	}

	// Token: 0x04003A9F RID: 15007
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeA_01.prefab:e5c5b670db416714c9b00b5135f3b3be");

	// Token: 0x04003AA0 RID: 15008
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_01.prefab:1168cbcb23827e44896f0cab1580d8b6");

	// Token: 0x04003AA1 RID: 15009
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_02.prefab:57b33b0e3629d4743860c5ecd806e43f");

	// Token: 0x04003AA2 RID: 15010
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeC_01.prefab:e53520db3376e6842b91969fcdb16fe7");

	// Token: 0x04003AA3 RID: 15011
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8Intro_01.prefab:a3a2d35f3727f144dac3df3258f1a938");

	// Token: 0x04003AA4 RID: 15012
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8EmoteResponse_01.prefab:74551dbaf11c0924b9571c8c77d93405");

	// Token: 0x04003AA5 RID: 15013
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeA_01.prefab:18971804117b5f346bf4901f7319f2c7");

	// Token: 0x04003AA6 RID: 15014
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeC_01.prefab:a490641c7d4016c418b5b005ec1e2427");

	// Token: 0x04003AA7 RID: 15015
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_01.prefab:dfad08cd8266489478a4186af6f330a0");

	// Token: 0x04003AA8 RID: 15016
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_02.prefab:9654500b7882a4241823e66435dee83f");

	// Token: 0x04003AA9 RID: 15017
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_03.prefab:b1f25de0583b4473acce86e0176f9dd6");

	// Token: 0x04003AAA RID: 15018
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_01.prefab:2a5a7d9c5e03292439894ff0e6292629");

	// Token: 0x04003AAB RID: 15019
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_02.prefab:ebad60e3d5c19d348a7ced60b9dfb99e");

	// Token: 0x04003AAC RID: 15020
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_03.prefab:c2e60147c7a1369459491c54859e38be");

	// Token: 0x04003AAD RID: 15021
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Intro_01.prefab:829d2a0cfaa6caa4ab12c9d9de5b0ae2");

	// Token: 0x04003AAE RID: 15022
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Loss_01.prefab:72c8a96e92e979d4291d5765c7c672f4");

	// Token: 0x04003AAF RID: 15023
	private List<string> m_VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPowerLines = new List<string>
	{
		BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_01,
		BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_02,
		BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_03
	};

	// Token: 0x04003AB0 RID: 15024
	private List<string> m_VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8IdleLines = new List<string>
	{
		BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_01,
		BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_02,
		BoH_Garrosh_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_03
	};

	// Token: 0x04003AB1 RID: 15025
	private HashSet<string> m_playedLines = new HashSet<string>();
}
