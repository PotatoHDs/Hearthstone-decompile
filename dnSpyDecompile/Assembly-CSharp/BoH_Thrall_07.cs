using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000546 RID: 1350
public class BoH_Thrall_07 : BoH_Thrall_Dungeon
{
	// Token: 0x06004A1B RID: 18971 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004A1C RID: 18972 RVA: 0x0018AD2C File Offset: 0x00188F2C
	public BoH_Thrall_07()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Thrall_07.s_booleanOptions);
	}

	// Token: 0x06004A1D RID: 18973 RVA: 0x0018ADD0 File Offset: 0x00188FD0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7EmoteResponse_01,
			BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7HeroPower_01,
			BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7HeroPower_02,
			BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7HeroPower_03,
			BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Idle_01,
			BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Idle_02,
			BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Idle_03,
			BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Loss_01,
			BoH_Thrall_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeA_01,
			BoH_Thrall_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeB_01,
			BoH_Thrall_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeC_01,
			BoH_Thrall_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Intro_01,
			BoH_Thrall_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_01,
			BoH_Thrall_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_02,
			BoH_Thrall_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeA_01,
			BoH_Thrall_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeB_01,
			BoH_Thrall_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Intro_01,
			BoH_Thrall_07.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission7Victory_05,
			BoH_Thrall_07.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_01,
			BoH_Thrall_07.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004A1E RID: 18974 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004A1F RID: 18975 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004A20 RID: 18976 RVA: 0x0018AF74 File Offset: 0x00189174
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Thrall_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Intro_01);
		yield return base.MissionPlayVO(enemyActor, BoH_Thrall_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Intro_01);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004A21 RID: 18977 RVA: 0x0018AF83 File Offset: 0x00189183
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004A22 RID: 18978 RVA: 0x0018AF8B File Offset: 0x0018918B
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004A23 RID: 18979 RVA: 0x0018AF93 File Offset: 0x00189193
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_DRG;
		this.m_standardEmoteResponseLine = BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7EmoteResponse_01;
	}

	// Token: 0x06004A24 RID: 18980 RVA: 0x0018AFB8 File Offset: 0x001891B8
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

	// Token: 0x06004A25 RID: 18981 RVA: 0x0018B03C File Offset: 0x0018923C
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 504)
		{
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Loss_01, 2.5f);
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
			yield return base.PlayLineAlways(BoH_Thrall_07.KalecBrassRing, BoH_Thrall_07.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Thrall_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Thrall_07.KalecBrassRing, BoH_Thrall_07.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_02, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Thrall_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_02, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_07.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission7Victory_05, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004A26 RID: 18982 RVA: 0x0018B052 File Offset: 0x00189252
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

	// Token: 0x06004A27 RID: 18983 RVA: 0x0018B068 File Offset: 0x00189268
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

	// Token: 0x06004A28 RID: 18984 RVA: 0x0018B07E File Offset: 0x0018927E
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
				if (turn == 11)
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Thrall_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeC_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(enemyActor, BoH_Thrall_07.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_07.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003E5E RID: 15966
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Thrall_07.InitBooleanOptions();

	// Token: 0x04003E5F RID: 15967
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7EmoteResponse_01.prefab:29d04d98f2139ee4b8ed9be78849a6c3");

	// Token: 0x04003E60 RID: 15968
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7HeroPower_01.prefab:a1771c2114113dc4799fe2950b7fe962");

	// Token: 0x04003E61 RID: 15969
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7HeroPower_02.prefab:4590b4043aebd2049bd41a17b6048dd5");

	// Token: 0x04003E62 RID: 15970
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7HeroPower_03.prefab:e687658b25852074f939282780a2be5e");

	// Token: 0x04003E63 RID: 15971
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Idle_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Idle_01.prefab:2f9f0c222d89c60468637b8c00d8dd47");

	// Token: 0x04003E64 RID: 15972
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Idle_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Idle_02.prefab:8e901a4089eaa1b47a844a2e6b14edb0");

	// Token: 0x04003E65 RID: 15973
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Idle_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Idle_03.prefab:2a2e0b4e4e51fc14fa6799029155bfc8");

	// Token: 0x04003E66 RID: 15974
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Loss_01.prefab:9e45336303bd3734ea4055deee1078eb");

	// Token: 0x04003E67 RID: 15975
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeA_01.prefab:980dcad4747710348b8e2a7117577207");

	// Token: 0x04003E68 RID: 15976
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeB_01.prefab:4fc9e8f8c3aac4148bc046d43ce9d350");

	// Token: 0x04003E69 RID: 15977
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeC_01.prefab:48fa0915dce887740a4e4c4b9d2823da");

	// Token: 0x04003E6A RID: 15978
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Intro_01.prefab:f28cd05ec9568ce4384e017c39d3cf13");

	// Token: 0x04003E6B RID: 15979
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_01.prefab:525262ba5777e8b48af81a62307d2ff7");

	// Token: 0x04003E6C RID: 15980
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_02 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_02.prefab:af80af5ad0c24bd43a1d04f54833ce0d");

	// Token: 0x04003E6D RID: 15981
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeA_01.prefab:e70ae11b2378456468d6c9c0cbdccb3e");

	// Token: 0x04003E6E RID: 15982
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeB_01.prefab:043d0360388b6c24c9426d3e0a4f6bc7");

	// Token: 0x04003E6F RID: 15983
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Intro_01.prefab:c6244eb1776cd42448f80845893eff5c");

	// Token: 0x04003E70 RID: 15984
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission7Victory_05 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission7Victory_05.prefab:b7638baf36b08374ba3562e6752b4b0b");

	// Token: 0x04003E71 RID: 15985
	private static readonly AssetReference VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_01 = new AssetReference("VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_01.prefab:c997a6c7c909d90469040fc6b5523ff2");

	// Token: 0x04003E72 RID: 15986
	private static readonly AssetReference VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_02 = new AssetReference("VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_02.prefab:499dc5892f61dd3459174735dd2a4f7c");

	// Token: 0x04003E73 RID: 15987
	public static readonly AssetReference KalecBrassRing = new AssetReference("Kalec_BrassRing_Quote.prefab:b96062478a5eccd47bd5e94f1ad7312f");

	// Token: 0x04003E74 RID: 15988
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7HeroPower_01,
		BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7HeroPower_02,
		BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7HeroPower_03
	};

	// Token: 0x04003E75 RID: 15989
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Idle_01,
		BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Idle_02,
		BoH_Thrall_07.VO_Story_Hero_Jaina_Female_Human_Story_Thrall_Mission7Idle_03
	};

	// Token: 0x04003E76 RID: 15990
	private HashSet<string> m_playedLines = new HashSet<string>();
}
