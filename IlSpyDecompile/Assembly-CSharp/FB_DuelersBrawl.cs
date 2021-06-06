using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_DuelersBrawl : MissionEntity
{
	public struct PopupMessage
	{
		public string Message;

		public float Delay;
	}

	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private Notification m_popup;

	private static readonly Dictionary<int, PopupMessage> popupMsgs;

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.HANDLE_COIN,
			false
		} };
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	public FB_DuelersBrawl()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	public override void PreloadAssets()
	{
		PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 10)
		{
			if (GameState.Get().IsFriendlySidePlayerTurn())
			{
				TurnStartManager.Get().BeginListeningForTurnEvents();
			}
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(busy: false);
			yield break;
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
		yield return ShowPopup(GameStrings.Get(popupMsgs[missionEvent].Message), popupMsgs[missionEvent].Delay, popUpPos);
		popUpPos = default(Vector3);
	}

	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos)
	{
		m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.4f, GameStrings.Get(stringID), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
		NotificationManager.Get().DestroyNotification(m_popup, popupDuration);
		yield return new WaitForSeconds(1f);
	}

	static FB_DuelersBrawl()
	{
		Dictionary<int, PopupMessage> dictionary = new Dictionary<int, PopupMessage>();
		PopupMessage value = new PopupMessage
		{
			Message = "TB_DUELERSBRAWL_TAKE_PACES",
			Delay = 6f
		};
		dictionary.Add(1, value);
		value = new PopupMessage
		{
			Message = "TB_DUELERSBRAWL_SUDDEN_DEATH",
			Delay = 6f
		};
		dictionary.Add(2, value);
		popupMsgs = dictionary;
	}
}
