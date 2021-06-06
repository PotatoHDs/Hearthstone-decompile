using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000547 RID: 1351
public class BoH_Thrall_08 : BoH_Thrall_Dungeon
{
	// Token: 0x06004A2D RID: 18989 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004A2E RID: 18990 RVA: 0x0018B1E8 File Offset: 0x001893E8
	public BoH_Thrall_08()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Thrall_08.s_booleanOptions);
	}

	// Token: 0x06004A2F RID: 18991 RVA: 0x0018B2C0 File Offset: 0x001894C0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeA_01,
			BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_01,
			BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_02,
			BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeC_01,
			BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8Intro_01,
			BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8EmoteResponse_01,
			BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8HeroPower_01,
			BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8HeroPower_02,
			BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8HeroPower_03,
			BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Idle_01,
			BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Idle_02,
			BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Idle_03,
			BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Loss_01,
			BoH_Thrall_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeA_01,
			BoH_Thrall_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeC_01,
			BoH_Thrall_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Intro_01,
			BoH_Thrall_08.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission8Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004A30 RID: 18992 RVA: 0x0018B434 File Offset: 0x00189634
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x06004A31 RID: 18993 RVA: 0x0018B448 File Offset: 0x00189648
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

	// Token: 0x06004A32 RID: 18994 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004A33 RID: 18995 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004A34 RID: 18996 RVA: 0x0018B4BD File Offset: 0x001896BD
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Thrall_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Intro_01);
		yield return base.MissionPlayVO(enemyActor, BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8Intro_01);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004A35 RID: 18997 RVA: 0x0018B4CC File Offset: 0x001896CC
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004A36 RID: 18998 RVA: 0x0018B4D4 File Offset: 0x001896D4
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004A37 RID: 18999 RVA: 0x0018B4DC File Offset: 0x001896DC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BT;
		this.m_standardEmoteResponseLine = BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8EmoteResponse_01;
	}

	// Token: 0x06004A38 RID: 19000 RVA: 0x0018B500 File Offset: 0x00189700
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

	// Token: 0x06004A39 RID: 19001 RVA: 0x0018B584 File Offset: 0x00189784
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 228)
		{
			if (missionEvent == 102)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeC_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeC_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_26E;
			}
			if (missionEvent == 228)
			{
				yield return new WaitForSeconds(2f);
				Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
				NotificationManager.Get().DestroyNotification(notification, 5.5f);
				goto IL_26E;
			}
		}
		else
		{
			if (missionEvent == 504)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_08.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission8Victory_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_26E;
			}
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_26E;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_26E:
		yield break;
	}

	// Token: 0x06004A3A RID: 19002 RVA: 0x0018B59A File Offset: 0x0018979A
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

	// Token: 0x06004A3B RID: 19003 RVA: 0x0018B5B0 File Offset: 0x001897B0
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

	// Token: 0x06004A3C RID: 19004 RVA: 0x0018B5C6 File Offset: 0x001897C6
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 3)
		{
			if (turn == 7)
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_02, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Thrall_08.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003E77 RID: 15991
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Thrall_08.InitBooleanOptions();

	// Token: 0x04003E78 RID: 15992
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeA_01.prefab:40541853068677f4d9745730730f7ea9");

	// Token: 0x04003E79 RID: 15993
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_01.prefab:8b0b14fe3d5fbc243a062e69b3a9dd6b");

	// Token: 0x04003E7A RID: 15994
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_02.prefab:8f7d986a3a04d44458d85e98e1741f6f");

	// Token: 0x04003E7B RID: 15995
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeC_01.prefab:c24c2e172acd459498063de42600d818");

	// Token: 0x04003E7C RID: 15996
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8Intro_01.prefab:f34b97fab2460a14aadda239cc9b8e23");

	// Token: 0x04003E7D RID: 15997
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8EmoteResponse_01.prefab:342fc74826800814f89ce85462874cc0");

	// Token: 0x04003E7E RID: 15998
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8HeroPower_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8HeroPower_01.prefab:2059183819207d647b3ab0d5f50627b1");

	// Token: 0x04003E7F RID: 15999
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8HeroPower_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8HeroPower_02.prefab:90f5d2ea469112b4ab4ca8846806080e");

	// Token: 0x04003E80 RID: 16000
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8HeroPower_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8HeroPower_03.prefab:4c2c3ca32dc6fc04184e4ff9837c9762");

	// Token: 0x04003E81 RID: 16001
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Idle_01.prefab:06f5900c5e838aa4b9450a278584b463");

	// Token: 0x04003E82 RID: 16002
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Idle_02.prefab:4920648bd5eaddb4e9c0d1db5c35b76e");

	// Token: 0x04003E83 RID: 16003
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Idle_03.prefab:cc43852e58716974bad83da4b9c0429a");

	// Token: 0x04003E84 RID: 16004
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Loss_01.prefab:86f2bf5150ae3764784ffa667d8f5142");

	// Token: 0x04003E85 RID: 16005
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeA_01.prefab:e47985c6861b45f418cd59b0b2a6831f");

	// Token: 0x04003E86 RID: 16006
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeC_01.prefab:22e9cef7c11e39c4396d9cf3da90489b");

	// Token: 0x04003E87 RID: 16007
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Intro_01.prefab:9ae50e3a6180b9b4facad8b833b66f4e");

	// Token: 0x04003E88 RID: 16008
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission8Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission8Victory_01.prefab:e239773a72bed4141a9a5e47dc977cc4");

	// Token: 0x04003E89 RID: 16009
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			228,
			new string[]
			{
				"BOH_THRALL_08"
			}
		}
	};

	// Token: 0x04003E8A RID: 16010
	private Player friendlySidePlayer;

	// Token: 0x04003E8B RID: 16011
	private Entity playerEntity;

	// Token: 0x04003E8C RID: 16012
	private float popUpScale = 1.25f;

	// Token: 0x04003E8D RID: 16013
	private Vector3 popUpPos;

	// Token: 0x04003E8E RID: 16014
	private Notification StartPopup;

	// Token: 0x04003E8F RID: 16015
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8HeroPower_01,
		BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8HeroPower_02,
		BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8HeroPower_03
	};

	// Token: 0x04003E90 RID: 16016
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Idle_01,
		BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Idle_02,
		BoH_Thrall_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Thrall_Mission8Idle_03
	};

	// Token: 0x04003E91 RID: 16017
	private HashSet<string> m_playedLines = new HashSet<string>();
}
