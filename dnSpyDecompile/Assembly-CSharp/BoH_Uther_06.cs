using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200054F RID: 1359
public class BoH_Uther_06 : BoH_Uther_Dungeon
{
	// Token: 0x06004AE1 RID: 19169 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004AE2 RID: 19170 RVA: 0x0018D8D0 File Offset: 0x0018BAD0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Uther_06.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeB_01,
			BoH_Uther_06.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeC_01,
			BoH_Uther_06.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Intro_01,
			BoH_Uther_06.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_01,
			BoH_Uther_06.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_02,
			BoH_Uther_06.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeA_01,
			BoH_Uther_06.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_01,
			BoH_Uther_06.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_02,
			BoH_Uther_06.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_01,
			BoH_Uther_06.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_02,
			BoH_Uther_06.VO_Story_Hero_Uther_Male_Human_Story_Uther_PreMission7_01,
			BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Death_01,
			BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6EmoteResponse_01,
			BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_01,
			BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_02,
			BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_03,
			BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_01,
			BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_02,
			BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_03,
			BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Intro_01,
			BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Loss_01,
			BoH_Uther_06.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6Intro_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004AE3 RID: 19171 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004AE4 RID: 19172 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004AE5 RID: 19173 RVA: 0x0018DA94 File Offset: 0x0018BC94
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(friendlyActor, BoH_Uther_06.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6Intro_01, 2.5f);
		yield return base.PlayLineAlways(BoH_Uther_06.ArthasBrassRing, BoH_Uther_06.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Uther_06.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeA_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004AE6 RID: 19174 RVA: 0x0018DAA3 File Offset: 0x0018BCA3
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004AE7 RID: 19175 RVA: 0x0018DAAB File Offset: 0x0018BCAB
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004AE8 RID: 19176 RVA: 0x0018DAB3 File Offset: 0x0018BCB3
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6EmoteResponse_01;
	}

	// Token: 0x06004AE9 RID: 19177 RVA: 0x0018DACC File Offset: 0x0018BCCC
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

	// Token: 0x06004AEA RID: 19178 RVA: 0x0018DB55 File Offset: 0x0018BD55
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
			if (missionEvent == 504)
			{
				yield return base.PlayLineAlways(actor, BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Loss_01, 2.5f);
			}
			else
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(BoH_Uther_06.ArthasBrassRing, BoH_Uther_06.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_06.VO_Story_Hero_Uther_Male_Human_Story_Uther_PreMission7_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Uther_06.ArthasBrassRing, BoH_Uther_06.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_02, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004AEB RID: 19179 RVA: 0x0018DB6B File Offset: 0x0018BD6B
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

	// Token: 0x06004AEC RID: 19180 RVA: 0x0018DB81 File Offset: 0x0018BD81
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

	// Token: 0x06004AED RID: 19181 RVA: 0x0018DB97 File Offset: 0x0018BD97
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn != 7)
			{
				if (turn == 13)
				{
					yield return base.PlayLineAlways(friendlyActor, BoH_Uther_06.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(BoH_Uther_06.ArthasBrassRing, BoH_Uther_06.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Uther_06.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_02, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Uther_06.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Uther_06.ArthasBrassRing, BoH_Uther_06.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Uther_06.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_02, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Intro_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004AEE RID: 19182 RVA: 0x0011204E File Offset: 0x0011024E
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICC);
	}

	// Token: 0x04003F4B RID: 16203
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Uther_06.InitBooleanOptions();

	// Token: 0x04003F4C RID: 16204
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeB_01.prefab:6b4d78f2c08c3e24bb02bfcffb5c3dbf");

	// Token: 0x04003F4D RID: 16205
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeC_01.prefab:6ed031e98908a7b419e5ea8bf58be0f7");

	// Token: 0x04003F4E RID: 16206
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_01.prefab:2dd4ef73dc8f5d742a162ed09bd273de");

	// Token: 0x04003F4F RID: 16207
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_02 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_02.prefab:d717febb3b9490447ad4ecace144c4db");

	// Token: 0x04003F50 RID: 16208
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeA_01.prefab:46fce0cf2095c7a468a4dae72891cf18");

	// Token: 0x04003F51 RID: 16209
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_01.prefab:58483dc4d172b1a4ca9780af0d9a87d9");

	// Token: 0x04003F52 RID: 16210
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_02.prefab:43800732d733bb344a82e6fc81f36ad2");

	// Token: 0x04003F53 RID: 16211
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_01.prefab:48e5641d70a242845b815fb020c714d6");

	// Token: 0x04003F54 RID: 16212
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_02.prefab:6022b0d04fffefc4e84a92de7e09f334");

	// Token: 0x04003F55 RID: 16213
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_PreMission7_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_PreMission7_01.prefab:31d91ac77b39a3846841b7dca75739ef");

	// Token: 0x04003F56 RID: 16214
	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Death_01 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Death_01.prefab:846e08becce366c4cbd0bde29a079d0a");

	// Token: 0x04003F57 RID: 16215
	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6EmoteResponse_01.prefab:be9d19bcf3bb9c244a03e29657f72892");

	// Token: 0x04003F58 RID: 16216
	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_01.prefab:57512907dc75f8647a3c2ae291b436ee");

	// Token: 0x04003F59 RID: 16217
	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_02.prefab:01c5d34766cc48248934085ec8e10d8c");

	// Token: 0x04003F5A RID: 16218
	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_03 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_03.prefab:da4f79c830369b645b5d3c67986c2e00");

	// Token: 0x04003F5B RID: 16219
	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_01 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_01.prefab:d7ed78ccc5ea9c34f93edf78061decd1");

	// Token: 0x04003F5C RID: 16220
	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_02 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_02.prefab:5fcaecfcb9e0024478171423617dd916");

	// Token: 0x04003F5D RID: 16221
	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_03 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_03.prefab:a5d589e7d9414694fbf9c1653ab4f303");

	// Token: 0x04003F5E RID: 16222
	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Loss_01 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Loss_01.prefab:8152d60c14fdad443999fd65abc4a66c");

	// Token: 0x04003F5F RID: 16223
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6Intro_01.prefab:05b676e20eb09354cb9ef3bdd6cfc178");

	// Token: 0x04003F60 RID: 16224
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Intro_01.prefab:8f8ede68cb0a7a4419aaaf20bad98871");

	// Token: 0x04003F61 RID: 16225
	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Intro_01.prefab:1f658df40030a754a9e413d516c28d21");

	// Token: 0x04003F62 RID: 16226
	public static readonly AssetReference ArthasBrassRing = new AssetReference("Arthas_BrassRing_Quote.prefab:49bb0ac905ae3c54cbf3624451b62ab4");

	// Token: 0x04003F63 RID: 16227
	private List<string> m_HeroPowerLines = new List<string>
	{
		BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_01,
		BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_02,
		BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_03
	};

	// Token: 0x04003F64 RID: 16228
	private List<string> m_IdleLines = new List<string>
	{
		BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_01,
		BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_02,
		BoH_Uther_06.VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_03
	};

	// Token: 0x04003F65 RID: 16229
	private HashSet<string> m_playedLines = new HashSet<string>();
}
