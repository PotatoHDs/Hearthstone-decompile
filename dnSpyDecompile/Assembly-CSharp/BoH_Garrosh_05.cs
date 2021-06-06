using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200051C RID: 1308
public class BoH_Garrosh_05 : BoH_Garrosh_Dungeon
{
	// Token: 0x060046E6 RID: 18150 RVA: 0x0017DC5C File Offset: 0x0017BE5C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5EmoteResponse_01,
			BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5ExchangeA_01,
			BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5ExchangeB_01,
			BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5ExchangeC_01,
			BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5HeroPower_01,
			BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5HeroPower_02,
			BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5HeroPower_03,
			BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Idle_01,
			BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Idle_02,
			BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Idle_03,
			BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Intro_01,
			BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Loss_01,
			BoH_Garrosh_05.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5ExchangeA_01,
			BoH_Garrosh_05.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5ExchangeB_01,
			BoH_Garrosh_05.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5ExchangeC_01,
			BoH_Garrosh_05.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5Intro_01,
			BoH_Garrosh_05.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5Victory_01,
			BoH_Garrosh_05.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5Victory_02,
			BoH_Garrosh_05.VO_Story_Minion_Magatha_Female_Tauren_Story_Minion_Magatha_Play_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060046E7 RID: 18151 RVA: 0x0017DDF0 File Offset: 0x0017BFF0
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x060046E8 RID: 18152 RVA: 0x0017DE04 File Offset: 0x0017C004
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

	// Token: 0x060046E9 RID: 18153 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060046EA RID: 18154 RVA: 0x0017DE79 File Offset: 0x0017C079
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Garrosh_05.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060046EB RID: 18155 RVA: 0x0017DE88 File Offset: 0x0017C088
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5IdleLines;
	}

	// Token: 0x060046EC RID: 18156 RVA: 0x0017DE90 File Offset: 0x0017C090
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5HeroPowerLines;
	}

	// Token: 0x060046ED RID: 18157 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060046EE RID: 18158 RVA: 0x0017DE98 File Offset: 0x0017C098
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5EmoteResponse_01;
	}

	// Token: 0x060046EF RID: 18159 RVA: 0x0017DEB0 File Offset: 0x0017C0B0
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

	// Token: 0x060046F0 RID: 18160 RVA: 0x0017DF34 File Offset: 0x0017C134
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
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
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		this.popUpPos = new Vector3(0f, 0f, -40f);
		if (missionEvent != 101)
		{
			if (missionEvent != 228)
			{
				switch (missionEvent)
				{
				case 501:
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_05.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5Victory_01, 2.5f);
					yield return new WaitForSeconds(1f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_05.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5Victory_02, 2.5f);
					GameState.Get().SetBusy(false);
					goto IL_347;
				case 502:
				{
					GameState.Get().SetBusy(true);
					Actor enemyActorByCardId = base.GetEnemyActorByCardId("Story_03_Magatha");
					if (enemyActorByCardId != null)
					{
						yield return base.PlayLineAlways(enemyActorByCardId, BoH_Garrosh_05.VO_Story_Minion_Magatha_Female_Tauren_Story_Minion_Magatha_Play_01, 2.5f);
					}
					GameState.Get().SetBusy(false);
					goto IL_347;
				}
				case 504:
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(actor, BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Loss_01, 2.5f);
					GameState.Get().SetBusy(false);
					goto IL_347;
				}
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(3.5f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				popup = null;
			}
		}
		else
		{
			yield return new WaitForSeconds(4f);
		}
		IL_347:
		yield break;
	}

	// Token: 0x060046F1 RID: 18161 RVA: 0x0017DF4A File Offset: 0x0017C14A
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

	// Token: 0x060046F2 RID: 18162 RVA: 0x0017DF60 File Offset: 0x0017C160
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

	// Token: 0x060046F3 RID: 18163 RVA: 0x0017DF76 File Offset: 0x0017C176
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
					yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_05.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5ExchangeC_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_05.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_05.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060046F4 RID: 18164 RVA: 0x00118E06 File Offset: 0x00117006
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_GILFinalBoss);
	}

	// Token: 0x060046F5 RID: 18165 RVA: 0x0017DF8C File Offset: 0x0017C18C
	private IEnumerator ShowPopup(string displayString)
	{
		this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x04003A53 RID: 14931
	private static readonly AssetReference VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5EmoteResponse_01.prefab:8a442b65b8b89104cbfdcbb6d007e943");

	// Token: 0x04003A54 RID: 14932
	private static readonly AssetReference VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5ExchangeA_01.prefab:bed4da777801956469f487b4871635f4");

	// Token: 0x04003A55 RID: 14933
	private static readonly AssetReference VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5ExchangeB_01.prefab:01687dfe5439a304685f5e9f36e152ad");

	// Token: 0x04003A56 RID: 14934
	private static readonly AssetReference VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5ExchangeC_01.prefab:9e77297304529fb46bbad170a005a7a5");

	// Token: 0x04003A57 RID: 14935
	private static readonly AssetReference VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5HeroPower_01.prefab:346ba20dab46b7b49bca132912144509");

	// Token: 0x04003A58 RID: 14936
	private static readonly AssetReference VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5HeroPower_02.prefab:d2248704836d7fd47accc6973322bbec");

	// Token: 0x04003A59 RID: 14937
	private static readonly AssetReference VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5HeroPower_03.prefab:dfd2c6022b3fc2342880ed125ef8d350");

	// Token: 0x04003A5A RID: 14938
	private static readonly AssetReference VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Idle_01.prefab:87b9604355dd0054a8200353f0096208");

	// Token: 0x04003A5B RID: 14939
	private static readonly AssetReference VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Idle_02.prefab:03cb5bed9238c62449be0658841e7cbe");

	// Token: 0x04003A5C RID: 14940
	private static readonly AssetReference VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Idle_03.prefab:11ae308a9a4ace94582da826fbad5a8d");

	// Token: 0x04003A5D RID: 14941
	private static readonly AssetReference VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Intro_01.prefab:b640fe272b39e46449d763cee2151aa8");

	// Token: 0x04003A5E RID: 14942
	private static readonly AssetReference VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Loss_01.prefab:e2ddc12b95ad89e4881e66c83038a6b3");

	// Token: 0x04003A5F RID: 14943
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5ExchangeA_01.prefab:2b64f188ec70c65489293a74dd9aeee2");

	// Token: 0x04003A60 RID: 14944
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5ExchangeB_01.prefab:91ebcabe52f94be48b92c2e26c0c3b2c");

	// Token: 0x04003A61 RID: 14945
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5ExchangeC_01.prefab:1da5209a13e850b4da9031bf93ab7976");

	// Token: 0x04003A62 RID: 14946
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5Intro_01.prefab:6f05bc3ace322794ca0d7fd33fe3bba5");

	// Token: 0x04003A63 RID: 14947
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5Victory_01.prefab:3273df884dc30174fae6344cb2c8f327");

	// Token: 0x04003A64 RID: 14948
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5Victory_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission5Victory_02.prefab:1aabae45355658d4abca449ab6e6fe72");

	// Token: 0x04003A65 RID: 14949
	private static readonly AssetReference VO_Story_Minion_Magatha_Female_Tauren_Story_Minion_Magatha_Play_01 = new AssetReference("VO_Story_Minion_Magatha_Female_Tauren_Story_Minion_Magatha_Play_01.prefab:58f37ce2a8384ff099e289452fe17886");

	// Token: 0x04003A66 RID: 14950
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			228,
			new string[]
			{
				"BOH_GARROSH_05"
			}
		}
	};

	// Token: 0x04003A67 RID: 14951
	private Player friendlySidePlayer;

	// Token: 0x04003A68 RID: 14952
	private Entity playerEntity;

	// Token: 0x04003A69 RID: 14953
	private float popUpScale = 1.25f;

	// Token: 0x04003A6A RID: 14954
	private Vector3 popUpPos;

	// Token: 0x04003A6B RID: 14955
	private Notification StartPopup;

	// Token: 0x04003A6C RID: 14956
	private List<string> m_VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5HeroPowerLines = new List<string>
	{
		BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5HeroPower_01,
		BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5HeroPower_02,
		BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5HeroPower_03
	};

	// Token: 0x04003A6D RID: 14957
	private List<string> m_VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5IdleLines = new List<string>
	{
		BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Idle_01,
		BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Idle_02,
		BoH_Garrosh_05.VO_Story_Hero_Cairne_Male_Tauren_Story_Garrosh_Mission5Idle_03
	};

	// Token: 0x04003A6E RID: 14958
	private HashSet<string> m_playedLines = new HashSet<string>();
}
