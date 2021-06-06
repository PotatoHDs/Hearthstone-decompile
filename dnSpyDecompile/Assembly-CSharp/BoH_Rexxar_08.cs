using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200053D RID: 1341
public class BoH_Rexxar_08 : BoH_Rexxar_Dungeon
{
	// Token: 0x0600496F RID: 18799 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004970 RID: 18800 RVA: 0x00187FCC File Offset: 0x001861CC
	public BoH_Rexxar_08()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Rexxar_08.s_booleanOptions);
	}

	// Token: 0x06004971 RID: 18801 RVA: 0x00188070 File Offset: 0x00186270
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Death_01,
			BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8EmoteResponse_01,
			BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8ExchangeC_01,
			BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8ExchangeD_01,
			BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8ExchangeE_01,
			BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8HeroPower_01,
			BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8HeroPower_02,
			BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8HeroPower_03,
			BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Idle_01,
			BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Idle_02,
			BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Idle_03,
			BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Intro_01,
			BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Intro_02,
			BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Loss_01,
			BoH_Rexxar_08.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8ExchangeA_01,
			BoH_Rexxar_08.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8ExchangeB_01,
			BoH_Rexxar_08.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8ExchangeC_01,
			BoH_Rexxar_08.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8Intro_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004972 RID: 18802 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004973 RID: 18803 RVA: 0x001881F4 File Offset: 0x001863F4
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_08.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Intro_02, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004974 RID: 18804 RVA: 0x00188203 File Offset: 0x00186403
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8IdleLines;
	}

	// Token: 0x06004975 RID: 18805 RVA: 0x0018820B File Offset: 0x0018640B
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8HeroPowerLines;
	}

	// Token: 0x06004976 RID: 18806 RVA: 0x00188213 File Offset: 0x00186413
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Death_01;
		this.m_standardEmoteResponseLine = BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8EmoteResponse_01;
	}

	// Token: 0x06004977 RID: 18807 RVA: 0x0018823C File Offset: 0x0018643C
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

	// Token: 0x06004978 RID: 18808 RVA: 0x001882C5 File Offset: 0x001864C5
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 504)
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Loss_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06004979 RID: 18809 RVA: 0x001882DB File Offset: 0x001864DB
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
		if (!(cardId == "EX1_610"))
		{
			if (cardId == "Story_02_GronnTrap")
			{
				yield return base.PlayLineAlways(actor, BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8ExchangeE_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8ExchangeD_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600497A RID: 18810 RVA: 0x001882F1 File Offset: 0x001864F1
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

	// Token: 0x0600497B RID: 18811 RVA: 0x00188307 File Offset: 0x00186507
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
					yield return base.PlayLineAlways(actor, BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_08.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8ExchangeC_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_08.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_08.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600497C RID: 18812 RVA: 0x00176C37 File Offset: 0x00174E37
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}

	// Token: 0x04003D8B RID: 15755
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Rexxar_08.InitBooleanOptions();

	// Token: 0x04003D8C RID: 15756
	private static readonly AssetReference VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Death_01 = new AssetReference("VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Death_01.prefab:449307bb2d07fe749b1785dfd53ad27a");

	// Token: 0x04003D8D RID: 15757
	private static readonly AssetReference VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8EmoteResponse_01.prefab:97aa1e86626b3f44f9ea132a9c5d13ed");

	// Token: 0x04003D8E RID: 15758
	private static readonly AssetReference VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8ExchangeC_01.prefab:d0f6d06584caa3c41816e576f1c5c92a");

	// Token: 0x04003D8F RID: 15759
	private static readonly AssetReference VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8ExchangeD_01 = new AssetReference("VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8ExchangeD_01.prefab:a40693b57ecf2df4ea4fffdcd2850b7c");

	// Token: 0x04003D90 RID: 15760
	private static readonly AssetReference VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8ExchangeE_01 = new AssetReference("VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8ExchangeE_01.prefab:f8b2f1c0bb3652341b4106644aba629a");

	// Token: 0x04003D91 RID: 15761
	private static readonly AssetReference VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8HeroPower_01 = new AssetReference("VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8HeroPower_01.prefab:a322e6cb04a12a144a85e4f2bd28303d");

	// Token: 0x04003D92 RID: 15762
	private static readonly AssetReference VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8HeroPower_02 = new AssetReference("VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8HeroPower_02.prefab:17babc478105f324fb856bf54ff29b6e");

	// Token: 0x04003D93 RID: 15763
	private static readonly AssetReference VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8HeroPower_03 = new AssetReference("VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8HeroPower_03.prefab:da605a196ac4d3943948f81cafa86b14");

	// Token: 0x04003D94 RID: 15764
	private static readonly AssetReference VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Idle_01.prefab:e363859d8d7adc34aafccef5da7214de");

	// Token: 0x04003D95 RID: 15765
	private static readonly AssetReference VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Idle_02.prefab:cdd49e828c8aee743af48a78772e35c5");

	// Token: 0x04003D96 RID: 15766
	private static readonly AssetReference VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Idle_03.prefab:733e443ef2ca0a34f910c217b4f19650");

	// Token: 0x04003D97 RID: 15767
	private static readonly AssetReference VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Intro_01.prefab:f9f336cd8c868dc4b8a2f428e4c233ee");

	// Token: 0x04003D98 RID: 15768
	private static readonly AssetReference VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Intro_02 = new AssetReference("VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Intro_02.prefab:bab3445bfc52a6b4aa63866b863e49b8");

	// Token: 0x04003D99 RID: 15769
	private static readonly AssetReference VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Loss_01.prefab:03767a3f92025804e86515f0564f0f22");

	// Token: 0x04003D9A RID: 15770
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8ExchangeA_01.prefab:183ea9ddf9b75174e994a38aa2577a64");

	// Token: 0x04003D9B RID: 15771
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8ExchangeB_01.prefab:f78e10b557e14e24abd5f815bfe6abc6");

	// Token: 0x04003D9C RID: 15772
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8ExchangeC_01.prefab:98429fe2e35f24040b410ded98722053");

	// Token: 0x04003D9D RID: 15773
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission8Intro_01.prefab:a25ef61d811effd439acd640b194d7ef");

	// Token: 0x04003D9E RID: 15774
	private List<string> m_VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8HeroPowerLines = new List<string>
	{
		BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8HeroPower_01,
		BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8HeroPower_02,
		BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8HeroPower_03
	};

	// Token: 0x04003D9F RID: 15775
	private List<string> m_VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8IdleLines = new List<string>
	{
		BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Idle_01,
		BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Idle_02,
		BoH_Rexxar_08.VO_Story_Hero_Gorgrom_Male_Gronn_Story_Rexxar_Mission8Idle_03
	};

	// Token: 0x04003DA0 RID: 15776
	private HashSet<string> m_playedLines = new HashSet<string>();
}
