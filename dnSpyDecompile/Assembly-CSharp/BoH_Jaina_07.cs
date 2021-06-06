using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000528 RID: 1320
public class BoH_Jaina_07 : BoH_Jaina_Dungeon
{
	// Token: 0x060047C0 RID: 18368 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x060047C1 RID: 18369 RVA: 0x00181338 File Offset: 0x0017F538
	public BoH_Jaina_07()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Jaina_07.s_booleanOptions);
	}

	// Token: 0x060047C2 RID: 18370 RVA: 0x001813DC File Offset: 0x0017F5DC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Jaina_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeA_01,
			BoH_Jaina_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeB_01,
			BoH_Jaina_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeC_01,
			BoH_Jaina_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Intro_01,
			BoH_Jaina_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_01,
			BoH_Jaina_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_02,
			BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7EmoteResponse_01,
			BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeA_01,
			BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeB_01,
			BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_01,
			BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_02,
			BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_03,
			BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_01,
			BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_02,
			BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_03,
			BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Intro_01,
			BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Loss_01,
			BoH_Jaina_07.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_01,
			BoH_Jaina_07.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060047C3 RID: 18371 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060047C4 RID: 18372 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060047C5 RID: 18373 RVA: 0x00181570 File Offset: 0x0017F770
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060047C6 RID: 18374 RVA: 0x0018157F File Offset: 0x0017F77F
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7IdleLines;
	}

	// Token: 0x060047C7 RID: 18375 RVA: 0x00181587 File Offset: 0x0017F787
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPowerLines;
	}

	// Token: 0x060047C8 RID: 18376 RVA: 0x0018158F File Offset: 0x0017F78F
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7EmoteResponse_01;
	}

	// Token: 0x060047C9 RID: 18377 RVA: 0x001815A8 File Offset: 0x0017F7A8
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

	// Token: 0x060047CA RID: 18378 RVA: 0x0018162C File Offset: 0x0017F82C
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
				yield return base.PlayLineAlways(actor, BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(BoH_Jaina_07.KalecBrassRing, BoH_Jaina_07.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Jaina_07.KalecBrassRing, BoH_Jaina_07.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_02, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_02, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x060047CB RID: 18379 RVA: 0x00181642 File Offset: 0x0017F842
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

	// Token: 0x060047CC RID: 18380 RVA: 0x00181658 File Offset: 0x0017F858
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

	// Token: 0x060047CD RID: 18381 RVA: 0x0018166E File Offset: 0x0017F86E
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 3)
		{
			if (turn != 7)
			{
				if (turn == 9)
				{
					yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeC_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060047CE RID: 18382 RVA: 0x0016110D File Offset: 0x0015F30D
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRG);
	}

	// Token: 0x04003B5B RID: 15195
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Jaina_07.InitBooleanOptions();

	// Token: 0x04003B5C RID: 15196
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeA_01.prefab:1f19bfe8b5ddcd246aa050ae86508b09");

	// Token: 0x04003B5D RID: 15197
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeB_01.prefab:38e7d6f401987a547a39180eb2072f6a");

	// Token: 0x04003B5E RID: 15198
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeC_01.prefab:f9c93dd912750894e9ee8ef94b7fa479");

	// Token: 0x04003B5F RID: 15199
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Intro_01.prefab:d9cef84e5f616ae468d0cc5979ffee9a");

	// Token: 0x04003B60 RID: 15200
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_01.prefab:dc056acc266b425498a52076612375cd");

	// Token: 0x04003B61 RID: 15201
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_02 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_02.prefab:c9cd52aef700a2b44a0e0b1ddc951ad6");

	// Token: 0x04003B62 RID: 15202
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7EmoteResponse_01.prefab:0fad4ad6c9ce43528394167e0c695273");

	// Token: 0x04003B63 RID: 15203
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeA_01.prefab:413f79be89324bd1a3656208a4ab8583");

	// Token: 0x04003B64 RID: 15204
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeB_01.prefab:041c5edd5da146f79be49351b9d40f06");

	// Token: 0x04003B65 RID: 15205
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_01.prefab:0c0d47fce9d640899d6f3de7387d0c3c");

	// Token: 0x04003B66 RID: 15206
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_02.prefab:63efd08947604319825b373c3f6ef34d");

	// Token: 0x04003B67 RID: 15207
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_03.prefab:0394ea7e8546499f99e131c81eb3838f");

	// Token: 0x04003B68 RID: 15208
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_01.prefab:8806dda480c643ceaad85b1c17847688");

	// Token: 0x04003B69 RID: 15209
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_02.prefab:90b47f4662f444e98605cfcd0bfd2650");

	// Token: 0x04003B6A RID: 15210
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_03.prefab:c079ea3ffd0447aa8adf6f50b50258e4");

	// Token: 0x04003B6B RID: 15211
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Intro_01.prefab:332526943cb8417b8aeedc7a711ff3ba");

	// Token: 0x04003B6C RID: 15212
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Loss_01.prefab:a8fc5d1186da47b6a71a5485e3df777b");

	// Token: 0x04003B6D RID: 15213
	private static readonly AssetReference VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_01 = new AssetReference("VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_01.prefab:4505771016b158046b160e46154bf301");

	// Token: 0x04003B6E RID: 15214
	private static readonly AssetReference VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_02 = new AssetReference("VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_02.prefab:3b7d18ca0cd88274f8731b794f044f80");

	// Token: 0x04003B6F RID: 15215
	public static readonly AssetReference KalecBrassRing = new AssetReference("Kalec_BrassRing_Quote.prefab:b96062478a5eccd47bd5e94f1ad7312f");

	// Token: 0x04003B70 RID: 15216
	private List<string> m_VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPowerLines = new List<string>
	{
		BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_01,
		BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_02,
		BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_03
	};

	// Token: 0x04003B71 RID: 15217
	private List<string> m_VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7IdleLines = new List<string>
	{
		BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_01,
		BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_02,
		BoH_Jaina_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_03
	};

	// Token: 0x04003B72 RID: 15218
	private HashSet<string> m_playedLines = new HashSet<string>();
}
