using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200054D RID: 1357
public class BoH_Uther_04 : BoH_Uther_Dungeon
{
	// Token: 0x06004ABC RID: 19132 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004ABD RID: 19133 RVA: 0x0018D078 File Offset: 0x0018B278
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Uther_04.Story_04_Darkportal_Death,
			BoH_Uther_04.Story_04_Darkportal_EmoteResponse,
			BoH_Uther_04.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Intro_01,
			BoH_Uther_04.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeA_01,
			BoH_Uther_04.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeB_01,
			BoH_Uther_04.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeD_01,
			BoH_Uther_04.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Victory_01,
			BoH_Uther_04.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeA_01,
			BoH_Uther_04.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeB_01,
			BoH_Uther_04.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeC_01,
			BoH_Uther_04.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Victory_01,
			BoH_Uther_04.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Intro_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004ABE RID: 19134 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004ABF RID: 19135 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004AC0 RID: 19136 RVA: 0x0018D19C File Offset: 0x0018B39C
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(BoH_Uther_04.TuralyonBrassRing, BoH_Uther_04.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Uther_04.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004AC1 RID: 19137 RVA: 0x0018D1AB File Offset: 0x0018B3AB
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004AC2 RID: 19138 RVA: 0x0018D1B3 File Offset: 0x0018B3B3
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Uther_04.Story_04_Darkportal_EmoteResponse;
	}

	// Token: 0x06004AC3 RID: 19139 RVA: 0x0018D1CC File Offset: 0x0018B3CC
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

	// Token: 0x06004AC4 RID: 19140 RVA: 0x0018D255 File Offset: 0x0018B455
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 501:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor2, BoH_Uther_04.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Victory_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Uther_04.TuralyonBrassRing, BoH_Uther_04.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Victory_01, 2.5f);
			GameState.Get().SetBusy(true);
			goto IL_1CB;
		case 502:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor2, BoH_Uther_04.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeD_01, 2.5f);
			GameState.Get().SetBusy(true);
			goto IL_1CB;
		case 504:
			yield return base.PlayLineAlways(actor, BoH_Uther_04.Story_04_Darkportal_EmoteResponse, 2.5f);
			goto IL_1CB;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_1CB:
		yield break;
	}

	// Token: 0x06004AC5 RID: 19141 RVA: 0x0018D26B File Offset: 0x0018B46B
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

	// Token: 0x06004AC6 RID: 19142 RVA: 0x0018D281 File Offset: 0x0018B481
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

	// Token: 0x06004AC7 RID: 19143 RVA: 0x0018D297 File Offset: 0x0018B497
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 3)
		{
			if (turn != 9)
			{
				if (turn == 15)
				{
					yield return base.PlayLineAlways(BoH_Uther_04.TuralyonBrassRing, BoH_Uther_04.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeC_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(BoH_Uther_04.TuralyonBrassRing, BoH_Uther_04.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Uther_04.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(BoH_Uther_04.TuralyonBrassRing, BoH_Uther_04.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_04.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004AC8 RID: 19144 RVA: 0x0018D2AD File Offset: 0x0018B4AD
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.Store_PacksBT);
	}

	// Token: 0x04003F1F RID: 16159
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Uther_04.InitBooleanOptions();

	// Token: 0x04003F20 RID: 16160
	private static readonly AssetReference Story_04_Darkportal_Death = new AssetReference("Story_04_Darkportal_Death.prefab:2457ed615b87f4644af4528837554e4e");

	// Token: 0x04003F21 RID: 16161
	private static readonly AssetReference Story_04_Darkportal_EmoteResponse = new AssetReference("Story_04_Darkportal_EmoteResponse.prefab:c1d824da692e02840a52d831053755a0");

	// Token: 0x04003F22 RID: 16162
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeA_01.prefab:554a329cd72b44f41884fd1d13335aaf");

	// Token: 0x04003F23 RID: 16163
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeB_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeB_01.prefab:d8ef4d696d3295544818b9e29dbc6849");

	// Token: 0x04003F24 RID: 16164
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeD_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeD_01.prefab:ddb028020f81a45449aa25548c53df7f");

	// Token: 0x04003F25 RID: 16165
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Victory_01.prefab:ebe5c6adbe40a7441a8d115618aba3eb");

	// Token: 0x04003F26 RID: 16166
	private static readonly AssetReference VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeA_01 = new AssetReference("VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeA_01.prefab:7c183759d499460429614809e924efb2");

	// Token: 0x04003F27 RID: 16167
	private static readonly AssetReference VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeB_01 = new AssetReference("VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeB_01.prefab:6dcda9c2639256b47a3940d75fd0640d");

	// Token: 0x04003F28 RID: 16168
	private static readonly AssetReference VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeC_01 = new AssetReference("VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeC_01.prefab:9705efe18dcb35b4e8a05805b708f253");

	// Token: 0x04003F29 RID: 16169
	private static readonly AssetReference VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Victory_01 = new AssetReference("VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Victory_01.prefab:2845658c04680684bb569b5edc83941d");

	// Token: 0x04003F2A RID: 16170
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Intro_01.prefab:22bd16f3f61d4ff4b87ebbae46f20abe");

	// Token: 0x04003F2B RID: 16171
	private static readonly AssetReference VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Intro_01 = new AssetReference("VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Intro_01.prefab:582f47b3809509c48ab4257a6aff6cad");

	// Token: 0x04003F2C RID: 16172
	public static readonly AssetReference TuralyonBrassRing = new AssetReference("Turalyon_BrassRing_Quote.prefab:40afbe0d5b4da0643baf2ebf5756548d");

	// Token: 0x04003F2D RID: 16173
	private List<string> m_HeroPowerLines = new List<string>
	{
		BoH_Uther_04.Story_04_Darkportal_EmoteResponse
	};

	// Token: 0x04003F2E RID: 16174
	private HashSet<string> m_playedLines = new HashSet<string>();
}
