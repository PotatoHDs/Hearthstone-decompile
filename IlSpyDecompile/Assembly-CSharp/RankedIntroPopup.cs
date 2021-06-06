using System;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class RankedIntroPopup : BasicPopup
{
	public class RankedIntroPopupInfo : PopupInfo
	{
		public Action m_onHiddenCallback;
	}

	private const string SHOW_EVENT_NAME = "CODE_DIALOGMANAGER_SHOW";

	private const string HIDE_EVENT_NAME = "CODE_DIALOGMANAGER_HIDE";

	private const string HIDE_FINISHED_EVENT_NAME = "CODE_HIDE_FINISHED";

	private const string SETUP_SCENE_LOGIN = "SetUp_Scene_Login";

	private const string SETUP_SCENE_PLAYSCREEN = "SetUp_Scene_PlayScreen";

	private WidgetTemplate m_widget;

	protected override void Awake()
	{
		m_widget = GetComponent<WidgetTemplate>();
		m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == "Button_Framed_Clicked")
			{
				Hide();
			}
			if (eventName == "CODE_HIDE_FINISHED" && m_readyToDestroyCallback != null)
			{
				m_readyToDestroyCallback(this);
			}
		});
	}

	protected override void OnDestroy()
	{
		GameObject gameObject = base.transform.parent.gameObject;
		if (gameObject != null && gameObject.GetComponent<WidgetInstance>() != null)
		{
			UnityEngine.Object.Destroy(base.transform.parent.gameObject);
		}
		base.OnDestroy();
	}

	public override void Show()
	{
		if (!(m_widget == null))
		{
			UIContext.GetRoot().ShowPopup(base.gameObject);
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN || SceneMgr.Get().GetMode() == SceneMgr.Mode.HUB)
			{
				m_widget.TriggerEvent("SetUp_Scene_Login");
			}
			else if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT)
			{
				m_widget.TriggerEvent("SetUp_Scene_PlayScreen");
			}
			m_widget.TriggerEvent("CODE_DIALOGMANAGER_SHOW");
		}
	}

	public override void Hide()
	{
		RankedIntroPopupInfo rankedIntroPopupInfo = m_popupInfo as RankedIntroPopupInfo;
		if (rankedIntroPopupInfo != null && rankedIntroPopupInfo.m_onHiddenCallback != null)
		{
			rankedIntroPopupInfo.m_onHiddenCallback();
		}
		m_widget.TriggerEvent("CODE_DIALOGMANAGER_HIDE");
		IncrementRankedIntroPopupSeenCount();
	}

	private void IncrementRankedIntroPopupSeenCount()
	{
		if (RankMgr.Get().GetLocalPlayerMedalInfo().GetCurrentMedal(FormatType.FT_STANDARD) != null)
		{
			long value = 0L;
			GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_INTRO_SEEN_COUNT, out value);
			GameSaveDataManager gameSaveDataManager = GameSaveDataManager.Get();
			long[] array = new long[1];
			value = (array[0] = value + 1);
			gameSaveDataManager.SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_INTRO_SEEN_COUNT, array));
		}
	}
}
