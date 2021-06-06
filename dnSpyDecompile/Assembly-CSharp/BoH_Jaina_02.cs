using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000523 RID: 1315
public class BoH_Jaina_02 : BoH_Jaina_Dungeon
{
	// Token: 0x06004767 RID: 18279 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004768 RID: 18280 RVA: 0x0017FB58 File Offset: 0x0017DD58
	public BoH_Jaina_02()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Jaina_02.s_booleanOptions);
	}

	// Token: 0x06004769 RID: 18281 RVA: 0x0017FBFC File Offset: 0x0017DDFC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Jaina_02.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeA_01,
			BoH_Jaina_02.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeB_01,
			BoH_Jaina_02.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeC_01,
			BoH_Jaina_02.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Intro_01,
			BoH_Jaina_02.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Victory_01,
			BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission1EmoteResponse_01,
			BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeA_01,
			BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeB_01,
			BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_01,
			BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_02,
			BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_01,
			BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_02,
			BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_03,
			BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_01,
			BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_02,
			BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_03,
			BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Intro_01,
			BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Loss_01,
			BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600476A RID: 18282 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600476B RID: 18283 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x0600476C RID: 18284 RVA: 0x0017FD90 File Offset: 0x0017DF90
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_02.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600476D RID: 18285 RVA: 0x0017FD9F File Offset: 0x0017DF9F
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2IdleLines;
	}

	// Token: 0x0600476E RID: 18286 RVA: 0x0017FDA8 File Offset: 0x0017DFA8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission1EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600476F RID: 18287 RVA: 0x0017FE06 File Offset: 0x0017E006
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
				yield return base.PlayLineAlways(actor, BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_02.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004770 RID: 18288 RVA: 0x0017FE1C File Offset: 0x0017E01C
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

	// Token: 0x06004771 RID: 18289 RVA: 0x0017FE32 File Offset: 0x0017E032
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

	// Token: 0x06004772 RID: 18290 RVA: 0x0017FE48 File Offset: 0x0017E048
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
				if (turn == 7)
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_02.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(enemyActor, BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_02, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_02.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(enemyActor, BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_02.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004773 RID: 18291 RVA: 0x0017FE5E File Offset: 0x0017E05E
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_Karazhan);
	}

	// Token: 0x04003AD7 RID: 15063
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Jaina_02.InitBooleanOptions();

	// Token: 0x04003AD8 RID: 15064
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeA_01.prefab:32b4d155b3bd7a34bb39aefe2fc6530b");

	// Token: 0x04003AD9 RID: 15065
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeB_01.prefab:cea475a6fa82c344cbfac3b3bd381325");

	// Token: 0x04003ADA RID: 15066
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeC_01.prefab:a67a1b32b9ae9d54283996e038f07745");

	// Token: 0x04003ADB RID: 15067
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Intro_01.prefab:8f771159bea1bbe40810215964797a8a");

	// Token: 0x04003ADC RID: 15068
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Victory_01.prefab:e7f55d97437688c4cbe0b286a7ada7ba");

	// Token: 0x04003ADD RID: 15069
	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission1EmoteResponse_01.prefab:a565ea69aeb20074fb73c1a7eca1866f");

	// Token: 0x04003ADE RID: 15070
	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeA_01.prefab:815d9dbcff0ecce4e984b48da6ab5f53");

	// Token: 0x04003ADF RID: 15071
	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeB_01.prefab:dca6d1d61215e7446afe84643082779e");

	// Token: 0x04003AE0 RID: 15072
	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_01.prefab:5c0be8feedf10d749a3e776bf61831f2");

	// Token: 0x04003AE1 RID: 15073
	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_02.prefab:67439fcb09d8b7d408acd5644dc77913");

	// Token: 0x04003AE2 RID: 15074
	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_01.prefab:27c51b6237312534dae795d2ce02d3d4");

	// Token: 0x04003AE3 RID: 15075
	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_02.prefab:93713c64656c29847a669cdaba7860a6");

	// Token: 0x04003AE4 RID: 15076
	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_03.prefab:82fa6b2ba04141b40890af58dbbded3d");

	// Token: 0x04003AE5 RID: 15077
	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_01.prefab:afff785af98ce0d4d831d96e3ff82322");

	// Token: 0x04003AE6 RID: 15078
	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_02 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_02.prefab:2798ac2782e0b634581e59b498e05e89");

	// Token: 0x04003AE7 RID: 15079
	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_03 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_03.prefab:38c35d1826da5bb49a63c5eb08dec98e");

	// Token: 0x04003AE8 RID: 15080
	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Intro_01.prefab:7ce759b2460180e4481f1eef84492108");

	// Token: 0x04003AE9 RID: 15081
	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Loss_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Loss_01.prefab:d20943824575eb04ab3d1e420b17f163");

	// Token: 0x04003AEA RID: 15082
	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Victory_01.prefab:fb5d7df277fa9be4ba3da1d3209157a4");

	// Token: 0x04003AEB RID: 15083
	private List<string> m_VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPowerLines = new List<string>
	{
		BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_01,
		BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_02,
		BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_03
	};

	// Token: 0x04003AEC RID: 15084
	private List<string> m_VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2IdleLines = new List<string>
	{
		BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_01,
		BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_02,
		BoH_Jaina_02.VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_03
	};

	// Token: 0x04003AED RID: 15085
	private HashSet<string> m_playedLines = new HashSet<string>();
}
