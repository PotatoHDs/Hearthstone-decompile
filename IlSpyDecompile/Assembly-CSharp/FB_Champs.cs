using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_Champs : MissionEntity
{
	public struct PopupMessage
	{
		public string Message;

		public float Delay;

		public string Champion;
	}

	private Notification m_popup;

	public bool doPopup;

	private static readonly Dictionary<int, PopupMessage> popupMsgs;

	public override void PreloadAssets()
	{
		PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	public override string GetNameBannerOverride(Player.Side playerSide)
	{
		int tag = GameState.Get().GetPlayerBySide(playerSide).GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		return popupMsgs[tag].Champion;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		doPopup = true;
		if (missionEvent == 10000)
		{
			doPopup = false;
		}
		Vector3 popUpPos = default(Vector3);
		if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
		{
			popUpPos.z = (UniversalInputManager.UsePhoneUI ? 27f : 18f);
		}
		else
		{
			popUpPos.z = (UniversalInputManager.UsePhoneUI ? (-18f) : (-12f));
			yield return new WaitForSeconds(3f);
		}
		if (doPopup)
		{
			yield return ShowPopup(GameStrings.Get(popupMsgs[missionEvent].Message), popupMsgs[missionEvent].Delay, popUpPos);
		}
	}

	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos)
	{
		m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.4f, GameStrings.Get(stringID), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
		NotificationManager.Get().DestroyNotification(m_popup, popupDuration);
		GameState.Get().SetBusy(busy: true);
		yield return new WaitForSeconds(5f);
		GameState.Get().SetBusy(busy: false);
	}

	static FB_Champs()
	{
		Dictionary<int, PopupMessage> dictionary = new Dictionary<int, PopupMessage>();
		PopupMessage value = new PopupMessage
		{
			Message = "FB_CHAMPS_PAVEL_DRUID",
			Delay = 6f,
			Champion = "Pavel"
		};
		dictionary.Add(1235, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_PAVEL_MAGE",
			Delay = 6f,
			Champion = "Pavel"
		};
		dictionary.Add(1236, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_PAVEL_SHAMAN",
			Delay = 6f,
			Champion = "Pavel"
		};
		dictionary.Add(1237, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_PAVEL_ROGUE",
			Delay = 6f,
			Champion = "Pavel"
		};
		dictionary.Add(1238, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_PAVEL_WARRIOR",
			Delay = 6f,
			Champion = "Pavel"
		};
		dictionary.Add(1239, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_OSTKAKA_ROGUE",
			Delay = 6f,
			Champion = "Ostkaka"
		};
		dictionary.Add(1671, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_OSTKAKA_WARRIOR",
			Delay = 6f,
			Champion = "Ostkaka"
		};
		dictionary.Add(1672, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_OSTKAKA_MAGE",
			Delay = 6f,
			Champion = "Ostkaka"
		};
		dictionary.Add(1673, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_FIREBAT_ROGUE",
			Delay = 6f,
			Champion = "Firebat"
		};
		dictionary.Add(1675, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_FIREBAT_HUNTER",
			Delay = 6f,
			Champion = "Firebat"
		};
		dictionary.Add(1676, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_FIREBAT_DRUID",
			Delay = 6f,
			Champion = "Firebat"
		};
		dictionary.Add(1678, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_FIREBAT_WARLOCK",
			Delay = 6f,
			Champion = "Firebat"
		};
		dictionary.Add(1679, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_TOM60229_WARLOCK",
			Delay = 6f,
			Champion = "tom60229"
		};
		dictionary.Add(2173, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_TOM60229_DRUID",
			Delay = 6f,
			Champion = "tom60229"
		};
		dictionary.Add(2174, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_TOM60229_ROGUE",
			Delay = 6f,
			Champion = "tom60229"
		};
		dictionary.Add(2175, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_TOM60229_PRIEST",
			Delay = 6f,
			Champion = "tom60229"
		};
		dictionary.Add(2176, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_VKLIOOON_SHAMAN",
			Delay = 6f,
			Champion = "VKLiooon"
		};
		dictionary.Add(2838, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_VKLIOOON_HUNTER",
			Delay = 6f,
			Champion = "VKLiooon"
		};
		dictionary.Add(2839, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_VKLIOOON_PRIEST",
			Delay = 6f,
			Champion = "VKLiooon"
		};
		dictionary.Add(2840, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_VKLIOOON_DRUID",
			Delay = 6f,
			Champion = "VKLiooon"
		};
		dictionary.Add(2841, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_HUNTERACE_SHAMAN",
			Delay = 6f,
			Champion = "Hunterace"
		};
		dictionary.Add(2842, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_HUNTERACE_ROGUE",
			Delay = 6f,
			Champion = "Hunterace"
		};
		dictionary.Add(2843, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_HUNTERACE_MAGE",
			Delay = 6f,
			Champion = "Hunterace"
		};
		dictionary.Add(2844, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_HUNTERACE_WARRIOR",
			Delay = 6f,
			Champion = "Hunterace"
		};
		dictionary.Add(2845, value);
		value = new PopupMessage
		{
			Message = "FB_CHAMPS_MERC14",
			Delay = 6f,
			Champion = "Mercenaries 14"
		};
		dictionary.Add(2847, value);
		popupMsgs = dictionary;
	}
}
