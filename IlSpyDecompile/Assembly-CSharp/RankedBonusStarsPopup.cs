using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class RankedBonusStarsPopup : BasicPopup
{
	public class BonusStarsPopupInfo : PopupInfo
	{
		public Action m_onHiddenCallback;
	}

	public UberText m_descriptionText;

	public UberText m_finePrintText;

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
			if (eventName == "CODE_HIDE_FINISHED")
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		});
		m_widget.RegisterReadyListener(delegate
		{
			OnWidgetReady();
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
		BonusStarsPopupInfo bonusStarsPopupInfo = m_popupInfo as BonusStarsPopupInfo;
		if (bonusStarsPopupInfo != null && bonusStarsPopupInfo.m_onHiddenCallback != null)
		{
			bonusStarsPopupInfo.m_onHiddenCallback();
		}
		m_widget.TriggerEvent("CODE_DIALOGMANAGER_HIDE");
		IncrementBonusStarsPopupSeenCount();
	}

	private void OnWidgetReady()
	{
		IDataModel model = null;
		m_widget.GetDataModel(123, out model);
		RankedPlayDataModel rankedPlayDataModel = model as RankedPlayDataModel;
		if (rankedPlayDataModel != null && m_descriptionText != null)
		{
			m_descriptionText.Text = GameStrings.Format("GLUE_RANKED_BONUS_STARS_DESCRIPTION", rankedPlayDataModel.StarMultiplier);
		}
	}

	private void IncrementBonusStarsPopupSeenCount()
	{
		TranslatedMedalInfo currentMedal = RankMgr.Get().GetLocalPlayerMedalInfo().GetCurrentMedal(FormatType.FT_STANDARD);
		if (currentMedal != null)
		{
			long value = 0L;
			GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_BONUS_STARS_POPUP_SEEN_COUNT, out value);
			List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
			list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_LAST_SEASON_BONUS_STARS_POPUP_SEEN, currentMedal.seasonId));
			long[] array = new long[1];
			value = (array[0] = value + 1);
			list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_BONUS_STARS_POPUP_SEEN_COUNT, array));
			GameSaveDataManager.Get().SaveSubkeys(list);
		}
	}
}
