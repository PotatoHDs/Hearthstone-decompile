using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005B6 RID: 1462
public class TB_LichKingRaid : MissionEntity
{
	// Token: 0x060050DE RID: 20702 RVA: 0x001A922A File Offset: 0x001A742A
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x060050DF RID: 20703 RVA: 0x001A923C File Offset: 0x001A743C
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LichKing_Male_Human_Brawl_01.prefab:df6d7692c0d3d8c4aab91a2eec0a3d9f");
		base.PreloadSound("VO_LichKing_Male_Human_Brawl_03.prefab:12fd2a3bd4b0945448667db58a95f32b");
		base.PreloadSound("VO_LichKing_Male_Human_Brawl_05.prefab:96e0b55b99289824ebbae0d6201e936c");
	}

	// Token: 0x060050E0 RID: 20704 RVA: 0x0010F5C8 File Offset: 0x0010D7C8
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICCLichKing);
	}

	// Token: 0x060050E1 RID: 20705 RVA: 0x001A9260 File Offset: 0x001A7460
	private Card GetLichKing()
	{
		foreach (Player player in GameState.Get().GetPlayerMap().Values)
		{
			Entity hero = player.GetHero();
			Card card = hero.GetCard();
			if (hero.GetCardId() == "FB_LK_Raid_Hero")
			{
				return card;
			}
		}
		return null;
	}

	// Token: 0x060050E2 RID: 20706 RVA: 0x001A92D8 File Offset: 0x001A74D8
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

	// Token: 0x060050E3 RID: 20707 RVA: 0x001A934D File Offset: 0x001A754D
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.m_LichKingCard = this.GetLichKing();
		if (this.m_LichKingCard != null)
		{
			this.m_LichKingActor = this.m_LichKingCard.GetActor();
		}
		NameBanner nameBannerForSide = Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING);
		switch (missionEvent)
		{
		case 1:
			if (this.once && this.m_LichKingCard != null)
			{
				this.once = false;
				GameState.Get().SetBusy(true);
				Notification.SpeechBubbleDirection direction = this.m_LichKingActor.GetEntity().IsControlledByFriendlySidePlayer() ? Notification.SpeechBubbleDirection.BottomLeft : Notification.SpeechBubbleDirection.TopRight;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LichKing_Male_Human_Brawl_01.prefab:df6d7692c0d3d8c4aab91a2eec0a3d9f", direction, this.m_LichKingActor, 3f, 1f, true, false, 0f));
				GameState.Get().SetBusy(false);
			}
			break;
		case 3:
			if (this.m_LichKingCard != null)
			{
				GameState.Get().SetBusy(true);
				Notification.SpeechBubbleDirection direction2 = this.m_LichKingActor.GetEntity().IsControlledByFriendlySidePlayer() ? Notification.SpeechBubbleDirection.BottomLeft : Notification.SpeechBubbleDirection.TopRight;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LichKing_Male_Human_Brawl_03.prefab:12fd2a3bd4b0945448667db58a95f32b", direction2, this.m_LichKingActor, 3f, 1f, true, false, 0f));
				GameState.Get().SetBusy(false);
			}
			break;
		case 5:
			if (this.m_LichKingCard != null)
			{
				this.once = false;
				GameState.Get().SetBusy(true);
				Notification.SpeechBubbleDirection direction3 = this.m_LichKingActor.GetEntity().IsControlledByFriendlySidePlayer() ? Notification.SpeechBubbleDirection.BottomLeft : Notification.SpeechBubbleDirection.TopRight;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LichKing_Male_Human_Brawl_05.prefab:96e0b55b99289824ebbae0d6201e936c", direction3, this.m_LichKingActor, 3f, 1f, true, false, 0f));
				yield return new WaitForSeconds(2f);
				GameState.Get().SetBusy(false);
			}
			break;
		case 10:
			if (this.m_LichKingCard != null && this.m_popUpInfo.ContainsKey(missionEvent))
			{
				yield return this.ShowPopup(this.m_popUpInfo[missionEvent][0]);
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(6f);
				GameState.Get().SetBusy(false);
				yield return this.ShowPopup(this.m_popUpInfo[11][0]);
			}
			break;
		case 11:
			if (this.m_LichKingCard != null && this.m_popUpInfo.ContainsKey(missionEvent))
			{
				yield return this.ShowPopup(this.m_popUpInfo[missionEvent][0]);
			}
			break;
		case 12:
			if (this.m_LichKingCard != null && this.m_popUpInfo.ContainsKey(missionEvent))
			{
				yield return this.ShowPopup(this.m_popUpInfo[missionEvent][0]);
			}
			break;
		case 13:
			if (this.m_LichKingCard != null && this.m_popUpInfo.ContainsKey(missionEvent))
			{
				yield return this.ShowPopup(this.m_popUpInfo[missionEvent][0]);
			}
			break;
		case 14:
			if (this.m_LichKingCard != null && this.m_popUpInfo.ContainsKey(missionEvent))
			{
				yield return this.ShowPopup(this.m_popUpInfo[missionEvent][0]);
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(6f);
				GameState.Get().SetBusy(false);
				yield return this.ShowPopup(this.m_popUpInfo[15][0]);
			}
			break;
		case 20:
			if (nameBannerForSide != null)
			{
				nameBannerForSide.SetName(GameStrings.Get("FB_LK_HERO_02"));
			}
			break;
		case 21:
			if (nameBannerForSide != null)
			{
				nameBannerForSide.SetName(GameStrings.Get("FB_LK_HERO_03"));
			}
			break;
		case 22:
			if (nameBannerForSide != null)
			{
				nameBannerForSide.SetName(GameStrings.Get("FB_LK_HERO_04"));
			}
			break;
		case 23:
			if (nameBannerForSide != null)
			{
				nameBannerForSide.SetName(GameStrings.Get("FB_LK_HERO_05"));
			}
			break;
		case 24:
			if (nameBannerForSide != null)
			{
				nameBannerForSide.SetName(GameStrings.Get("FB_LK_HERO_01"));
			}
			break;
		}
		yield break;
	}

	// Token: 0x060050E4 RID: 20708 RVA: 0x001A9363 File Offset: 0x001A7563
	private IEnumerator ShowPopup(string displayString)
	{
		this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(displayString), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060050E5 RID: 20709 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x0400479D RID: 18333
	private Notification StartPopup;

	// Token: 0x0400479E RID: 18334
	private Vector3 popUpPos;

	// Token: 0x0400479F RID: 18335
	private Card m_LichKingCard;

	// Token: 0x040047A0 RID: 18336
	private Actor m_LichKingActor;

	// Token: 0x040047A1 RID: 18337
	private Player friendlySidePlayer;

	// Token: 0x040047A2 RID: 18338
	private bool once = true;

	// Token: 0x040047A3 RID: 18339
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			10,
			new string[]
			{
				"FB_LK_INTRO_01"
			}
		},
		{
			11,
			new string[]
			{
				"FB_LK_INTRO_02"
			}
		},
		{
			12,
			new string[]
			{
				"FB_LK_HEROSWITCH"
			}
		},
		{
			13,
			new string[]
			{
				"FB_LK_BOSSSWITCH"
			}
		},
		{
			14,
			new string[]
			{
				"FB_LK_DEAD_01"
			}
		},
		{
			15,
			new string[]
			{
				"FB_LK_DEAD_02"
			}
		}
	};
}
