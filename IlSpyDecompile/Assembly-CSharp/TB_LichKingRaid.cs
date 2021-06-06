using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_LichKingRaid : MissionEntity
{
	private Notification StartPopup;

	private Vector3 popUpPos;

	private Card m_LichKingCard;

	private Actor m_LichKingActor;

	private Player friendlySidePlayer;

	private bool once = true;

	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			10,
			new string[1] { "FB_LK_INTRO_01" }
		},
		{
			11,
			new string[1] { "FB_LK_INTRO_02" }
		},
		{
			12,
			new string[1] { "FB_LK_HEROSWITCH" }
		},
		{
			13,
			new string[1] { "FB_LK_BOSSSWITCH" }
		},
		{
			14,
			new string[1] { "FB_LK_DEAD_01" }
		},
		{
			15,
			new string[1] { "FB_LK_DEAD_02" }
		}
	};

	private void Start()
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_LichKing_Male_Human_Brawl_01.prefab:df6d7692c0d3d8c4aab91a2eec0a3d9f");
		PreloadSound("VO_LichKing_Male_Human_Brawl_03.prefab:12fd2a3bd4b0945448667db58a95f32b");
		PreloadSound("VO_LichKing_Male_Human_Brawl_05.prefab:96e0b55b99289824ebbae0d6201e936c");
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICCLichKing);
	}

	private Card GetLichKing()
	{
		foreach (Player value in GameState.Get().GetPlayerMap().Values)
		{
			Entity hero = value.GetHero();
			Card card = hero.GetCard();
			if (hero.GetCardId() == "FB_LK_Raid_Hero")
			{
				return card;
			}
		}
		return null;
	}

	private void SetPopupPosition()
	{
		if (friendlySidePlayer.IsCurrentPlayer())
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				popUpPos.z = -66f;
			}
			else
			{
				popUpPos.z = -44f;
			}
		}
		else if ((bool)UniversalInputManager.UsePhoneUI)
		{
			popUpPos.z = 66f;
		}
		else
		{
			popUpPos.z = 44f;
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		m_LichKingCard = GetLichKing();
		if (m_LichKingCard != null)
		{
			m_LichKingActor = m_LichKingCard.GetActor();
		}
		NameBanner nameBannerForSide = Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING);
		switch (missionEvent)
		{
		case 1:
			if (once && m_LichKingCard != null)
			{
				once = false;
				GameState.Get().SetBusy(busy: true);
				Notification.SpeechBubbleDirection direction = (m_LichKingActor.GetEntity().IsControlledByFriendlySidePlayer() ? Notification.SpeechBubbleDirection.BottomLeft : Notification.SpeechBubbleDirection.TopRight);
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LichKing_Male_Human_Brawl_01.prefab:df6d7692c0d3d8c4aab91a2eec0a3d9f", direction, m_LichKingActor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 3:
			if (m_LichKingCard != null)
			{
				GameState.Get().SetBusy(busy: true);
				Notification.SpeechBubbleDirection direction3 = (m_LichKingActor.GetEntity().IsControlledByFriendlySidePlayer() ? Notification.SpeechBubbleDirection.BottomLeft : Notification.SpeechBubbleDirection.TopRight);
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LichKing_Male_Human_Brawl_03.prefab:12fd2a3bd4b0945448667db58a95f32b", direction3, m_LichKingActor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 5:
			if (m_LichKingCard != null)
			{
				once = false;
				GameState.Get().SetBusy(busy: true);
				Notification.SpeechBubbleDirection direction2 = (m_LichKingActor.GetEntity().IsControlledByFriendlySidePlayer() ? Notification.SpeechBubbleDirection.BottomLeft : Notification.SpeechBubbleDirection.TopRight);
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LichKing_Male_Human_Brawl_05.prefab:96e0b55b99289824ebbae0d6201e936c", direction2, m_LichKingActor));
				yield return new WaitForSeconds(2f);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 10:
			if (m_LichKingCard != null && m_popUpInfo.ContainsKey(missionEvent))
			{
				yield return ShowPopup(m_popUpInfo[missionEvent][0]);
				GameState.Get().SetBusy(busy: true);
				yield return new WaitForSeconds(6f);
				GameState.Get().SetBusy(busy: false);
				yield return ShowPopup(m_popUpInfo[11][0]);
			}
			break;
		case 11:
			if (m_LichKingCard != null && m_popUpInfo.ContainsKey(missionEvent))
			{
				yield return ShowPopup(m_popUpInfo[missionEvent][0]);
			}
			break;
		case 12:
			if (m_LichKingCard != null && m_popUpInfo.ContainsKey(missionEvent))
			{
				yield return ShowPopup(m_popUpInfo[missionEvent][0]);
			}
			break;
		case 13:
			if (m_LichKingCard != null && m_popUpInfo.ContainsKey(missionEvent))
			{
				yield return ShowPopup(m_popUpInfo[missionEvent][0]);
			}
			break;
		case 14:
			if (m_LichKingCard != null && m_popUpInfo.ContainsKey(missionEvent))
			{
				yield return ShowPopup(m_popUpInfo[missionEvent][0]);
				GameState.Get().SetBusy(busy: true);
				yield return new WaitForSeconds(6f);
				GameState.Get().SetBusy(busy: false);
				yield return ShowPopup(m_popUpInfo[15][0]);
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
	}

	private IEnumerator ShowPopup(string displayString)
	{
		StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(displayString), convertLegacyPosition: false);
		NotificationManager.Get().DestroyNotification(StartPopup, 7f);
		GameState.Get().SetBusy(busy: true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(busy: false);
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}
}
