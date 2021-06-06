using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000542 RID: 1346
public class BoH_Thrall_03 : BoH_Thrall_Dungeon
{
	// Token: 0x060049D1 RID: 18897 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x060049D2 RID: 18898 RVA: 0x001898F8 File Offset: 0x00187AF8
	public BoH_Thrall_03()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Thrall_03.s_booleanOptions);
	}

	// Token: 0x060049D3 RID: 18899 RVA: 0x0018999C File Offset: 0x00187B9C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Thrall_03.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission3Victory_03,
			BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3EmoteResponse_01,
			BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeA_02,
			BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeB_01,
			BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeE_01,
			BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_01,
			BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_02,
			BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_03,
			BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_01,
			BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_02,
			BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_03,
			BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Intro_01,
			BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Loss_01,
			BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_01,
			BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_04,
			BoH_Thrall_03.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeA_01,
			BoH_Thrall_03.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeB_02,
			BoH_Thrall_03.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeC_01,
			BoH_Thrall_03.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Intro_02,
			BoH_Thrall_03.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060049D4 RID: 18900 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060049D5 RID: 18901 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060049D6 RID: 18902 RVA: 0x00189B40 File Offset: 0x00187D40
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Thrall_03.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060049D7 RID: 18903 RVA: 0x00189B4F File Offset: 0x00187D4F
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x060049D8 RID: 18904 RVA: 0x00189B57 File Offset: 0x00187D57
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x060049D9 RID: 18905 RVA: 0x00189B5F File Offset: 0x00187D5F
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		this.m_standardEmoteResponseLine = BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3EmoteResponse_01;
	}

	// Token: 0x060049DA RID: 18906 RVA: 0x00189B84 File Offset: 0x00187D84
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

	// Token: 0x060049DB RID: 18907 RVA: 0x00189C08 File Offset: 0x00187E08
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 102)
		{
			if (missionEvent != 504)
			{
				if (missionEvent == 507)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(actor, BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Loss_01, 2.5f);
					GameState.Get().SetBusy(false);
				}
				else
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_04, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_03.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Victory_02, 2.5f);
			yield return base.PlayLineAlways(BoH_Thrall_03.DrekTharBrassRing, BoH_Thrall_03.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission3Victory_03, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x060049DC RID: 18908 RVA: 0x00189C1E File Offset: 0x00187E1E
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

	// Token: 0x060049DD RID: 18909 RVA: 0x00189C34 File Offset: 0x00187E34
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

	// Token: 0x060049DE RID: 18910 RVA: 0x00189C4A File Offset: 0x00187E4A
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
					yield return base.PlayLineAlways(enemyActor, BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeB_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_03.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeB_02, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_03.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeA_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeA_02, 2.5f);
			}
		}
		else if (turn != 7)
		{
			if (turn == 11)
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeE_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_03.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeC_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003DEF RID: 15855
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Thrall_03.InitBooleanOptions();

	// Token: 0x04003DF0 RID: 15856
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission3Victory_03 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission3Victory_03.prefab:1632e3415413bae4cae8baea9ac2d463");

	// Token: 0x04003DF1 RID: 15857
	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3EmoteResponse_01.prefab:82fcb3096fc3ce848b1dccf0c6e27e9b");

	// Token: 0x04003DF2 RID: 15858
	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeA_02 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeA_02.prefab:0252e71becc21194c9ce562ea6f8c558");

	// Token: 0x04003DF3 RID: 15859
	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeB_01.prefab:7c1bb55882755044b93c2d80cf5529d3");

	// Token: 0x04003DF4 RID: 15860
	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeE_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeE_01.prefab:d37737ad76c3c7147b48064ac3e1b749");

	// Token: 0x04003DF5 RID: 15861
	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_01.prefab:6e1084a2fe77fc6429cca7e7ea814bb4");

	// Token: 0x04003DF6 RID: 15862
	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_02.prefab:fbec9d12a083e7740afbc4325788156d");

	// Token: 0x04003DF7 RID: 15863
	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_03.prefab:f9b4c1fbbd747f34ea279adcdacfff08");

	// Token: 0x04003DF8 RID: 15864
	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_01.prefab:b63242d1f26861c4f86e947894b7ddaf");

	// Token: 0x04003DF9 RID: 15865
	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_02.prefab:b3100a568a7d2f9429cc1ee8f9fd0e00");

	// Token: 0x04003DFA RID: 15866
	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_03.prefab:160f5d77dfdcfd24681e086e15b99017");

	// Token: 0x04003DFB RID: 15867
	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Intro_01.prefab:d6114deafb9b25a4abdcdc973ac0d9f6");

	// Token: 0x04003DFC RID: 15868
	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Loss_01.prefab:3987cc90ce6cc1849b8dd017d391ce50");

	// Token: 0x04003DFD RID: 15869
	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_01.prefab:f3062ae46e0e6e04682219bbbba0ddcd");

	// Token: 0x04003DFE RID: 15870
	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_04 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_04.prefab:60a3c83e99cc3a44794d766a3b6817a5");

	// Token: 0x04003DFF RID: 15871
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeA_01.prefab:e82fe160cd510f84d8e65551e6159274");

	// Token: 0x04003E00 RID: 15872
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeB_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeB_02.prefab:2ff26dd9c751a754387eda26888d8866");

	// Token: 0x04003E01 RID: 15873
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeC_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeC_01.prefab:02659523d280b5f48941973d89cab85a");

	// Token: 0x04003E02 RID: 15874
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Intro_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Intro_02.prefab:b71ba06da00c1214b9f888399dfaf0b3");

	// Token: 0x04003E03 RID: 15875
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Victory_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Victory_02.prefab:9566d837a8ca6a34eae381e5204699e6");

	// Token: 0x04003E04 RID: 15876
	public static readonly AssetReference DrekTharBrassRing = new AssetReference("DrekThar_BrassRing_Quote.prefab:5df753488b9bf7846909a8badb04d0f3");

	// Token: 0x04003E05 RID: 15877
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_01,
		BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_02,
		BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_03
	};

	// Token: 0x04003E06 RID: 15878
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_01,
		BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_02,
		BoH_Thrall_03.VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_03
	};

	// Token: 0x04003E07 RID: 15879
	private HashSet<string> m_playedLines = new HashSet<string>();
}
