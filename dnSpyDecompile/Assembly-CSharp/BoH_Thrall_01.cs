using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000540 RID: 1344
public class BoH_Thrall_01 : BoH_Thrall_Dungeon
{
	// Token: 0x060049A7 RID: 18855 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x060049A8 RID: 18856 RVA: 0x00188BD8 File Offset: 0x00186DD8
	public BoH_Thrall_01()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Thrall_01.s_booleanOptions);
	}

	// Token: 0x060049A9 RID: 18857 RVA: 0x00188CE0 File Offset: 0x00186EE0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1EmoteResponse_01,
			BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeA_01,
			BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeB_01,
			BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_01,
			BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_02,
			BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_03,
			BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_01,
			BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_02,
			BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_03,
			BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_01,
			BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_02,
			BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Loss_01,
			BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Victory_01,
			BoH_Thrall_01.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1ExchangeC_02,
			BoH_Thrall_01.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Intro_02,
			BoH_Thrall_01.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_02,
			BoH_Thrall_01.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_04,
			BoH_Thrall_01.VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_01,
			BoH_Thrall_01.VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_03,
			BoH_Thrall_01.VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1Victory_03,
			BoH_Thrall_01.VO_Story_Minion_Taretha_Female_Human_TriggerThrallLowHP_01,
			BoH_Thrall_01.troll_crowd_play_reaction_positive_1,
			BoH_Thrall_01.troll_crowd_play_reaction_very_positive_1,
			BoH_Thrall_01.Low_Drumroll
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060049AA RID: 18858 RVA: 0x00188EC4 File Offset: 0x001870C4
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x060049AB RID: 18859 RVA: 0x00188ED8 File Offset: 0x001870D8
	private void SetPopupPosition()
	{
		if (this.friendlySidePlayer.IsCurrentPlayer())
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = -66f;
				return;
			}
			this.popUpPos.z = -44f;
			return;
		}
		else
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = 66f;
				return;
			}
			this.popUpPos.z = 44f;
			return;
		}
	}

	// Token: 0x060049AC RID: 18860 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060049AD RID: 18861 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060049AE RID: 18862 RVA: 0x00188F4D File Offset: 0x0018714D
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(enemyActor, BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_01);
		yield return base.MissionPlayVO(enemyActor, BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_02);
		yield return base.MissionPlayVO(friendlyActor, BoH_Thrall_01.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060049AF RID: 18863 RVA: 0x00188F5C File Offset: 0x0018715C
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x060049B0 RID: 18864 RVA: 0x00188F64 File Offset: 0x00187164
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x060049B1 RID: 18865 RVA: 0x00188F6C File Offset: 0x0018716C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_GILFinalBoss;
		this.m_standardEmoteResponseLine = BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1EmoteResponse_01;
	}

	// Token: 0x060049B2 RID: 18866 RVA: 0x00188F90 File Offset: 0x00187190
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

	// Token: 0x060049B3 RID: 18867 RVA: 0x00189014 File Offset: 0x00187214
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		this.popUpPos = new Vector3(0f, 0f, -40f);
		if (missionEvent <= 328)
		{
			if (missionEvent == 101)
			{
				yield return base.PlayLineAlways(BoH_Thrall_01.TarethaBrassRing, BoH_Thrall_01.VO_Story_Minion_Taretha_Female_Human_TriggerThrallLowHP_01, 2.5f);
				goto IL_3D7;
			}
			if (missionEvent == 328)
			{
				yield return new WaitForSeconds(2f);
				yield return base.MissionPlaySound(enemyActor, BoH_Thrall_01.Low_Drumroll);
				Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
				NotificationManager.Get().DestroyNotification(notification, 3.5f);
				goto IL_3D7;
			}
		}
		else
		{
			if (missionEvent == 428)
			{
				yield return new WaitForSeconds(2f);
				yield return base.MissionPlaySound(enemyActor, BoH_Thrall_01.Low_Drumroll);
				Notification notification2 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
				NotificationManager.Get().DestroyNotification(notification2, 3.5f);
				goto IL_3D7;
			}
			if (missionEvent == 504)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Victory_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_01.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_02, 2.5f);
				yield return base.PlayLineAlways(BoH_Thrall_01.TarethaBrassRing, BoH_Thrall_01.VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1Victory_03, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_01.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_04, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_3D7;
			}
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_3D7;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_3D7:
		yield break;
	}

	// Token: 0x060049B4 RID: 18868 RVA: 0x0018902A File Offset: 0x0018722A
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

	// Token: 0x060049B5 RID: 18869 RVA: 0x00189040 File Offset: 0x00187240
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

	// Token: 0x060049B6 RID: 18870 RVA: 0x00189056 File Offset: 0x00187256
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		int m_missionEventBannerID = 228;
		if (turn != 1)
		{
			if (turn != 5)
			{
				if (turn == 9)
				{
					yield return base.PlayLineAlways(BoH_Thrall_01.TarethaBrassRing, BoH_Thrall_01.VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_01.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1ExchangeC_02, 2.5f);
					yield return base.PlayLineAlways(BoH_Thrall_01.TarethaBrassRing, BoH_Thrall_01.VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_03, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeB_01, 2.5f);
				yield return base.MissionPlaySound(enemyActor, BoH_Thrall_01.troll_crowd_play_reaction_very_positive_1);
			}
		}
		else
		{
			yield return base.MissionPlaySound(enemyActor, BoH_Thrall_01.Low_Drumroll);
			Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[m_missionEventBannerID][0]), false, NotificationManager.PopupTextType.FANCY);
			NotificationManager.Get().DestroyNotification(notification, 6.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeA_01, 2.5f);
			yield return base.MissionPlaySound(enemyActor, BoH_Thrall_01.troll_crowd_play_reaction_positive_1);
		}
		yield break;
	}

	// Token: 0x04003DAD RID: 15789
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Thrall_01.InitBooleanOptions();

	// Token: 0x04003DAE RID: 15790
	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1EmoteResponse_01.prefab:6b15ee78bdb7d3e4eb817730292f67a0");

	// Token: 0x04003DAF RID: 15791
	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeA_01.prefab:94a18e1a15b525644ae701a8933e9870");

	// Token: 0x04003DB0 RID: 15792
	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeB_01.prefab:ccf658130c95a974a819427341721210");

	// Token: 0x04003DB1 RID: 15793
	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_01.prefab:48facd72d9af5094a8a0ad2fcbcb5e27");

	// Token: 0x04003DB2 RID: 15794
	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_02 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_02.prefab:89e0881c8a41774478ac837d2f916209");

	// Token: 0x04003DB3 RID: 15795
	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_03.prefab:ae94739cf04110447ba183a3247bb34c");

	// Token: 0x04003DB4 RID: 15796
	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_01.prefab:56d7656451fd55048be2545881834b29");

	// Token: 0x04003DB5 RID: 15797
	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_02.prefab:5989e805d24284d419afb8f9d2dd1268");

	// Token: 0x04003DB6 RID: 15798
	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_03.prefab:409c200008c1eff44843cb59e1361a8b");

	// Token: 0x04003DB7 RID: 15799
	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_01.prefab:2edf498dc0494924e96950f57b527726");

	// Token: 0x04003DB8 RID: 15800
	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_02.prefab:5a74c71b012609441b6cc49eb2385767");

	// Token: 0x04003DB9 RID: 15801
	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Loss_01.prefab:6cb3cb427aab3474e908bcc32171c183");

	// Token: 0x04003DBA RID: 15802
	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Victory_01.prefab:1d0196bd7fb4a6d46a2ea6bd46eb6c0b");

	// Token: 0x04003DBB RID: 15803
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1ExchangeC_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1ExchangeC_02.prefab:56ed419838689ba4b99553bf6261ac6e");

	// Token: 0x04003DBC RID: 15804
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Intro_02.prefab:a6004d0d5e0d2d5488a743006bf3c9a3");

	// Token: 0x04003DBD RID: 15805
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_02.prefab:c0ae5c6e8818e4c4a8e4c3bf7e3f0b05");

	// Token: 0x04003DBE RID: 15806
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_04 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_04.prefab:4ffbeba38c60d11449fa8fe528dbc1cf");

	// Token: 0x04003DBF RID: 15807
	private static readonly AssetReference VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_01 = new AssetReference("VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_01.prefab:31402b425176411196f5aea1ff637fee");

	// Token: 0x04003DC0 RID: 15808
	private static readonly AssetReference VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_03 = new AssetReference("VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_03.prefab:0e8b98d41d4045429f5a8abf0726cfa4");

	// Token: 0x04003DC1 RID: 15809
	private static readonly AssetReference VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1Victory_03 = new AssetReference("VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1Victory_03.prefab:f8d7924a89ce2c94a92c3e3da1d90b11");

	// Token: 0x04003DC2 RID: 15810
	private static readonly AssetReference VO_Story_Minion_Taretha_Female_Human_TriggerThrallLowHP_01 = new AssetReference("VO_Story_Minion_Taretha_Female_Human_TriggerThrallLowHP_01.prefab:90d6a3dd52cb4406986eac02dcdcba5c");

	// Token: 0x04003DC3 RID: 15811
	private static readonly AssetReference troll_crowd_play_reaction_positive_1 = new AssetReference("troll_crowd_play_reaction_positive_1.prefab:ccb1b6d185b1e2e4480ef813153f3c9f");

	// Token: 0x04003DC4 RID: 15812
	private static readonly AssetReference troll_crowd_play_reaction_very_positive_1 = new AssetReference("troll_crowd_play_reaction_very_positive_1.prefab:f69658ac1e4cacc4b94acdb1e0c38911");

	// Token: 0x04003DC5 RID: 15813
	private static readonly AssetReference Low_Drumroll = new AssetReference("Low_Drumroll.prefab:d678997d507dd9041a499af987d4ff76");

	// Token: 0x04003DC6 RID: 15814
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			228,
			new string[]
			{
				"BOH_THRALL_01"
			}
		},
		{
			328,
			new string[]
			{
				"BOH_THRALL_01a"
			}
		},
		{
			428,
			new string[]
			{
				"BOH_THRALL_01b"
			}
		}
	};

	// Token: 0x04003DC7 RID: 15815
	private Player friendlySidePlayer;

	// Token: 0x04003DC8 RID: 15816
	private Entity playerEntity;

	// Token: 0x04003DC9 RID: 15817
	private float popUpScale = 1.25f;

	// Token: 0x04003DCA RID: 15818
	private Vector3 popUpPos;

	// Token: 0x04003DCB RID: 15819
	private Notification StartPopup;

	// Token: 0x04003DCC RID: 15820
	public static readonly AssetReference TarethaBrassRing = new AssetReference("Taretha_BrassRing_Quote.prefab:683cb9ffa15e9af4cbbe387d4afe900d");

	// Token: 0x04003DCD RID: 15821
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_01,
		BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_02,
		BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_03
	};

	// Token: 0x04003DCE RID: 15822
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_01,
		BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_02,
		BoH_Thrall_01.VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_03
	};

	// Token: 0x04003DCF RID: 15823
	private HashSet<string> m_playedLines = new HashSet<string>();
}
