using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

public class SetRotationManager : IService
{
	public enum PostSetRotationPopupProgress
	{
		NOT_READY_TO_BE_SEEN,
		NOT_SEEN,
		SEEN
	}

	public const SpecialEventType CURRENT_SET_ROTATION_EVENT = SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021;

	public const SpecialEventType PRE_CURRENT_SET_ROTATION_EVENT = SpecialEventType.SPECIAL_EVENT_PRE_SET_ROTATION_2021;

	public const SpecialEventType UPCOMING_SET_ROTATION_EVENT = SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021;

	public const int CURRENT_SET_ROTATION_INTRO_PROGRESS = 6;

	private static SetRotationManager s_instance;

	private bool? m_currentSetRotationActive;

	private bool m_forceShowSetRotationIntroByCheat;

	public bool IsShowingSetRotationRelogPopup { get; private set; }

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		SpecialEventManager specialEventManager = serviceLocator.Get<SpecialEventManager>();
		if (specialEventManager.HasReceivedEventTimingsFromServer)
		{
			OnCurrentSetRotationEventAdded(null);
		}
		else
		{
			specialEventManager.AddEventAddedListener(OnCurrentSetRotationEventAdded, SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021);
		}
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[2]
		{
			typeof(SpecialEventManager),
			typeof(ReturningPlayerMgr)
		};
	}

	public void Shutdown()
	{
	}

	public static bool IsCurrentSetRotationEventActive()
	{
		return SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, activeIfDoesNotExist: false);
	}

	public static SetRotationManager Get()
	{
		return HearthstoneServices.Get<SetRotationManager>();
	}

	public static bool HasSeenStandardModeTutorial()
	{
		return Options.Get().GetBool(Option.HAS_SEEN_STANDARD_MODE_TUTORIAL, defaultVal: false);
	}

	public static bool ShouldShowSetRotationIntro()
	{
		if (ReturningPlayerMgr.Get().IsInReturningPlayerMode)
		{
			return false;
		}
		if (!IsCurrentSetRotationEventActive())
		{
			return false;
		}
		if (Get().IsShowingSetRotationRelogPopup)
		{
			return false;
		}
		if (Options.Get().GetInt(Option.SET_ROTATION_INTRO_PROGRESS, 0) == 6 && HasSeenStandardModeTutorial())
		{
			return false;
		}
		CollectionManager collectionManager = CollectionManager.Get();
		if (collectionManager == null)
		{
			Debug.LogError("ShouldShowSetRotationIntro: CollectionManager is NULL!");
			return false;
		}
		if (!collectionManager.ShouldAccountSeeStandardWild())
		{
			return false;
		}
		if (Cheat_AutoCompleteSetRotationIntro())
		{
			return false;
		}
		return true;
	}

	public bool CheckForSetRotationRollover()
	{
		if (!m_currentSetRotationActive.HasValue)
		{
			return false;
		}
		if (m_currentSetRotationActive.Value)
		{
			return false;
		}
		if (SceneMgr.Get() == null || SceneMgr.Get().IsInGame())
		{
			return false;
		}
		if (!SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, activeIfDoesNotExist: false))
		{
			return false;
		}
		if (HearthstoneServices.TryGet<GameMgr>(out var service) && service.IsFindingGame())
		{
			GameMgr.Get().CancelFindGame();
		}
		Log.All.Print("Set Rotation has just occurred!  Forcing the client to restart.");
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_SET_ROTATION_ROLLOVER_HEADER");
		popupInfo.m_text = GameStrings.Get(HearthstoneApplication.AllowResetFromFatalError ? "GLOBAL_SET_ROTATION_ROLLOVER_BODY_MOBILE" : "GLOBAL_SET_ROTATION_ROLLOVER_BODY_DESKTOP");
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_disableBnetBar = true;
		popupInfo.m_blurWhenShown = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		bool forceShowSetRotationByCheat = m_forceShowSetRotationIntroByCheat;
		popupInfo.m_responseCallback = delegate
		{
			if (forceShowSetRotationByCheat)
			{
				if (Options.Get().GetInt(Option.SET_ROTATION_INTRO_PROGRESS) > 0)
				{
					Options.Get().SetInt(Option.SET_ROTATION_INTRO_PROGRESS, 5);
				}
				if (Options.Get().GetInt(Option.SET_ROTATION_INTRO_PROGRESS_NEW_PLAYER) > 0)
				{
					Options.Get().SetInt(Option.SET_ROTATION_INTRO_PROGRESS_NEW_PLAYER, 5);
				}
			}
			if ((bool)HearthstoneApplication.AllowResetFromFatalError)
			{
				HearthstoneApplication.Get().Reset();
			}
			else
			{
				HearthstoneApplication.Get().Exit();
			}
		};
		DialogManager.Get().ShowPopup(popupInfo);
		IsShowingSetRotationRelogPopup = true;
		m_currentSetRotationActive = true;
		m_forceShowSetRotationIntroByCheat = false;
		return true;
	}

	private void OnCurrentSetRotationEventAdded(object userdata)
	{
		m_currentSetRotationActive = SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, activeIfDoesNotExist: true);
		if (!m_currentSetRotationActive.Value)
		{
			Processor.RunCoroutine(PollForSetRotationRollover(1f));
		}
	}

	private IEnumerator PollForSetRotationRollover(float interval)
	{
		while (!CheckForSetRotationRollover())
		{
			yield return new WaitForSeconds(interval);
		}
	}

	public static bool ShowNewPlayerSetRotationPopupIfNeeded()
	{
		if (Options.Get().GetInt(Option.SET_ROTATION_INTRO_PROGRESS_NEW_PLAYER, 0) >= 6)
		{
			return false;
		}
		if (!RankMgr.Get().IsNewPlayer())
		{
			return false;
		}
		if (!IsCurrentSetRotationEventActive())
		{
			return false;
		}
		if (!CollectionManager.Get().AccountHasRotatedBoosters(DateTime.UtcNow) && !CollectionManager.Get().AccountHasWildCards())
		{
			return false;
		}
		BasicPopup.PopupInfo popupInfo = new BasicPopup.PopupInfo();
		popupInfo.m_prefabAssetRefs.Add("SetRotationNewPlayerPopup.prefab:cb6eb3b3df79ec34f826043f13e9a609");
		popupInfo.m_blurWhenShown = true;
		if (!RankMgr.Get().UseLegacyRankedPlay())
		{
			popupInfo.m_bodyText = GameStrings.Get("GLUE_NEW_PLAYER_SET_ROTATION_POPUP_BODY_NEW");
		}
		popupInfo.m_responseCallback = delegate
		{
			Options.Get().SetInt(Option.SET_ROTATION_INTRO_PROGRESS_NEW_PLAYER, 6);
		};
		DialogManager.Get().ShowBasicPopup(UserAttentionBlocker.NONE, popupInfo);
		return true;
	}

	public void Cheat_OverrideSetRotationDate(DateTime date, bool forceShowSetRotationIntro)
	{
		if (HearthstoneApplication.IsInternal())
		{
			SpecialEventManager.Get().Cheat_OverrideSetRotationDate(date);
			OnCurrentSetRotationEventAdded(null);
			m_forceShowSetRotationIntroByCheat = forceShowSetRotationIntro;
			m_currentSetRotationActive = false;
		}
	}

	public static bool Cheat_AutoCompleteSetRotationIntro()
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return false;
		}
		if (!Options.Get().GetBool(Option.DISABLE_SET_ROTATION_INTRO, defaultVal: false))
		{
			return false;
		}
		Options.Get().SetInt(Option.SET_ROTATION_INTRO_PROGRESS, 6);
		Options.Get().SetBool(Option.HAS_SEEN_STANDARD_MODE_TUTORIAL, val: true);
		string message = "Set Rotation intro skipped due to disableSetRotationIntro=true";
		UIStatus.Get().AddInfo(message, 10f);
		return true;
	}
}
