using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x020006A0 RID: 1696
public class SetRotationManager : IService
{
	// Token: 0x1700059C RID: 1436
	// (get) Token: 0x06005EAD RID: 24237 RVA: 0x001EC761 File Offset: 0x001EA961
	// (set) Token: 0x06005EAE RID: 24238 RVA: 0x001EC769 File Offset: 0x001EA969
	public bool IsShowingSetRotationRelogPopup { get; private set; }

	// Token: 0x06005EAF RID: 24239 RVA: 0x001EC772 File Offset: 0x001EA972
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		SpecialEventManager specialEventManager = serviceLocator.Get<SpecialEventManager>();
		if (specialEventManager.HasReceivedEventTimingsFromServer)
		{
			this.OnCurrentSetRotationEventAdded(null);
		}
		else
		{
			specialEventManager.AddEventAddedListener(new SpecialEventManager.EventAddedCallback(this.OnCurrentSetRotationEventAdded), SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, null);
		}
		yield break;
	}

	// Token: 0x06005EB0 RID: 24240 RVA: 0x001EC788 File Offset: 0x001EA988
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(SpecialEventManager),
			typeof(ReturningPlayerMgr)
		};
	}

	// Token: 0x06005EB1 RID: 24241 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06005EB2 RID: 24242 RVA: 0x001EC7AA File Offset: 0x001EA9AA
	public static bool IsCurrentSetRotationEventActive()
	{
		return SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, false);
	}

	// Token: 0x06005EB3 RID: 24243 RVA: 0x001EC7B9 File Offset: 0x001EA9B9
	public static SetRotationManager Get()
	{
		return HearthstoneServices.Get<SetRotationManager>();
	}

	// Token: 0x06005EB4 RID: 24244 RVA: 0x001EC7C0 File Offset: 0x001EA9C0
	public static bool HasSeenStandardModeTutorial()
	{
		return Options.Get().GetBool(Option.HAS_SEEN_STANDARD_MODE_TUTORIAL, false);
	}

	// Token: 0x06005EB5 RID: 24245 RVA: 0x001EC7D4 File Offset: 0x001EA9D4
	public static bool ShouldShowSetRotationIntro()
	{
		if (ReturningPlayerMgr.Get().IsInReturningPlayerMode)
		{
			return false;
		}
		if (!SetRotationManager.IsCurrentSetRotationEventActive())
		{
			return false;
		}
		if (SetRotationManager.Get().IsShowingSetRotationRelogPopup)
		{
			return false;
		}
		if (Options.Get().GetInt(Option.SET_ROTATION_INTRO_PROGRESS, 0) == 6 && SetRotationManager.HasSeenStandardModeTutorial())
		{
			return false;
		}
		CollectionManager collectionManager = CollectionManager.Get();
		if (collectionManager == null)
		{
			Debug.LogError("ShouldShowSetRotationIntro: CollectionManager is NULL!");
			return false;
		}
		return collectionManager.ShouldAccountSeeStandardWild() && !SetRotationManager.Cheat_AutoCompleteSetRotationIntro();
	}

	// Token: 0x06005EB6 RID: 24246 RVA: 0x001EC84C File Offset: 0x001EAA4C
	public bool CheckForSetRotationRollover()
	{
		if (this.m_currentSetRotationActive == null)
		{
			return false;
		}
		if (this.m_currentSetRotationActive.Value)
		{
			return false;
		}
		if (SceneMgr.Get() == null || SceneMgr.Get().IsInGame())
		{
			return false;
		}
		if (!SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, false))
		{
			return false;
		}
		GameMgr gameMgr;
		if (HearthstoneServices.TryGet<GameMgr>(out gameMgr) && gameMgr.IsFindingGame())
		{
			GameMgr.Get().CancelFindGame();
		}
		Log.All.Print("Set Rotation has just occurred!  Forcing the client to restart.", Array.Empty<object>());
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_SET_ROTATION_ROLLOVER_HEADER");
		popupInfo.m_text = GameStrings.Get(HearthstoneApplication.AllowResetFromFatalError ? "GLOBAL_SET_ROTATION_ROLLOVER_BODY_MOBILE" : "GLOBAL_SET_ROTATION_ROLLOVER_BODY_DESKTOP");
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_disableBnetBar = true;
		popupInfo.m_blurWhenShown = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		bool forceShowSetRotationByCheat = this.m_forceShowSetRotationIntroByCheat;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
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
			if (HearthstoneApplication.AllowResetFromFatalError)
			{
				HearthstoneApplication.Get().Reset();
				return;
			}
			HearthstoneApplication.Get().Exit();
		};
		DialogManager.Get().ShowPopup(popupInfo);
		this.IsShowingSetRotationRelogPopup = true;
		this.m_currentSetRotationActive = new bool?(true);
		this.m_forceShowSetRotationIntroByCheat = false;
		return true;
	}

	// Token: 0x06005EB7 RID: 24247 RVA: 0x001EC973 File Offset: 0x001EAB73
	private void OnCurrentSetRotationEventAdded(object userdata)
	{
		this.m_currentSetRotationActive = new bool?(SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, true));
		if (!this.m_currentSetRotationActive.Value)
		{
			Processor.RunCoroutine(this.PollForSetRotationRollover(1f), null);
		}
	}

	// Token: 0x06005EB8 RID: 24248 RVA: 0x001EC9AC File Offset: 0x001EABAC
	private IEnumerator PollForSetRotationRollover(float interval)
	{
		while (!this.CheckForSetRotationRollover())
		{
			yield return new WaitForSeconds(interval);
		}
		yield break;
	}

	// Token: 0x06005EB9 RID: 24249 RVA: 0x001EC9C4 File Offset: 0x001EABC4
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
		if (!SetRotationManager.IsCurrentSetRotationEventActive())
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
		popupInfo.m_responseCallback = delegate(BasicPopup.Response response, object userData)
		{
			Options.Get().SetInt(Option.SET_ROTATION_INTRO_PROGRESS_NEW_PLAYER, 6);
		};
		DialogManager.Get().ShowBasicPopup(UserAttentionBlocker.NONE, popupInfo);
		return true;
	}

	// Token: 0x06005EBA RID: 24250 RVA: 0x001ECA88 File Offset: 0x001EAC88
	public void Cheat_OverrideSetRotationDate(DateTime date, bool forceShowSetRotationIntro)
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return;
		}
		SpecialEventManager.Get().Cheat_OverrideSetRotationDate(date);
		this.OnCurrentSetRotationEventAdded(null);
		this.m_forceShowSetRotationIntroByCheat = forceShowSetRotationIntro;
		this.m_currentSetRotationActive = new bool?(false);
	}

	// Token: 0x06005EBB RID: 24251 RVA: 0x001ECAB8 File Offset: 0x001EACB8
	public static bool Cheat_AutoCompleteSetRotationIntro()
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return false;
		}
		if (!Options.Get().GetBool(Option.DISABLE_SET_ROTATION_INTRO, false))
		{
			return false;
		}
		Options.Get().SetInt(Option.SET_ROTATION_INTRO_PROGRESS, 6);
		Options.Get().SetBool(Option.HAS_SEEN_STANDARD_MODE_TUTORIAL, true);
		string message = "Set Rotation intro skipped due to disableSetRotationIntro=true";
		UIStatus.Get().AddInfo(message, 10f);
		return true;
	}

	// Token: 0x04004FD4 RID: 20436
	public const SpecialEventType CURRENT_SET_ROTATION_EVENT = SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021;

	// Token: 0x04004FD5 RID: 20437
	public const SpecialEventType PRE_CURRENT_SET_ROTATION_EVENT = SpecialEventType.SPECIAL_EVENT_PRE_SET_ROTATION_2021;

	// Token: 0x04004FD6 RID: 20438
	public const SpecialEventType UPCOMING_SET_ROTATION_EVENT = SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021;

	// Token: 0x04004FD7 RID: 20439
	public const int CURRENT_SET_ROTATION_INTRO_PROGRESS = 6;

	// Token: 0x04004FD8 RID: 20440
	private static SetRotationManager s_instance;

	// Token: 0x04004FD9 RID: 20441
	private bool? m_currentSetRotationActive;

	// Token: 0x04004FDA RID: 20442
	private bool m_forceShowSetRotationIntroByCheat;

	// Token: 0x020021C6 RID: 8646
	public enum PostSetRotationPopupProgress
	{
		// Token: 0x0400E147 RID: 57671
		NOT_READY_TO_BE_SEEN,
		// Token: 0x0400E148 RID: 57672
		NOT_SEEN,
		// Token: 0x0400E149 RID: 57673
		SEEN
	}
}
