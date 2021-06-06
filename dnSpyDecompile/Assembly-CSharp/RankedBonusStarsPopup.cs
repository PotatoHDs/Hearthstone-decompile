using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x02000646 RID: 1606
[RequireComponent(typeof(WidgetTemplate))]
public class RankedBonusStarsPopup : BasicPopup
{
	// Token: 0x06005A9D RID: 23197 RVA: 0x001D9298 File Offset: 0x001D7498
	protected override void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == "Button_Framed_Clicked")
			{
				this.Hide();
			}
			if (eventName == "CODE_HIDE_FINISHED")
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		});
		this.m_widget.RegisterReadyListener(delegate(object _)
		{
			this.OnWidgetReady();
		}, null, true);
	}

	// Token: 0x06005A9E RID: 23198 RVA: 0x001D92D8 File Offset: 0x001D74D8
	protected override void OnDestroy()
	{
		GameObject gameObject = base.transform.parent.gameObject;
		if (gameObject != null && gameObject.GetComponent<WidgetInstance>() != null)
		{
			UnityEngine.Object.Destroy(base.transform.parent.gameObject);
		}
		base.OnDestroy();
	}

	// Token: 0x06005A9F RID: 23199 RVA: 0x001D9328 File Offset: 0x001D7528
	public override void Show()
	{
		if (this.m_widget == null)
		{
			return;
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN || SceneMgr.Get().GetMode() == SceneMgr.Mode.HUB)
		{
			this.m_widget.TriggerEvent("SetUp_Scene_Login", default(Widget.TriggerEventParameters));
		}
		else if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT)
		{
			this.m_widget.TriggerEvent("SetUp_Scene_PlayScreen", default(Widget.TriggerEventParameters));
		}
		this.m_widget.TriggerEvent("CODE_DIALOGMANAGER_SHOW", default(Widget.TriggerEventParameters));
	}

	// Token: 0x06005AA0 RID: 23200 RVA: 0x001D93BC File Offset: 0x001D75BC
	public override void Hide()
	{
		RankedBonusStarsPopup.BonusStarsPopupInfo bonusStarsPopupInfo = this.m_popupInfo as RankedBonusStarsPopup.BonusStarsPopupInfo;
		if (bonusStarsPopupInfo != null && bonusStarsPopupInfo.m_onHiddenCallback != null)
		{
			bonusStarsPopupInfo.m_onHiddenCallback();
		}
		this.m_widget.TriggerEvent("CODE_DIALOGMANAGER_HIDE", default(Widget.TriggerEventParameters));
		this.IncrementBonusStarsPopupSeenCount();
	}

	// Token: 0x06005AA1 RID: 23201 RVA: 0x001D940C File Offset: 0x001D760C
	private void OnWidgetReady()
	{
		IDataModel dataModel = null;
		this.m_widget.GetDataModel(123, out dataModel);
		RankedPlayDataModel rankedPlayDataModel = dataModel as RankedPlayDataModel;
		if (rankedPlayDataModel == null)
		{
			return;
		}
		if (this.m_descriptionText != null)
		{
			this.m_descriptionText.Text = GameStrings.Format("GLUE_RANKED_BONUS_STARS_DESCRIPTION", new object[]
			{
				rankedPlayDataModel.StarMultiplier
			});
		}
	}

	// Token: 0x06005AA2 RID: 23202 RVA: 0x001D9470 File Offset: 0x001D7670
	private void IncrementBonusStarsPopupSeenCount()
	{
		TranslatedMedalInfo currentMedal = RankMgr.Get().GetLocalPlayerMedalInfo().GetCurrentMedal(FormatType.FT_STANDARD);
		if (currentMedal != null)
		{
			long num = 0L;
			GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_BONUS_STARS_POPUP_SEEN_COUNT, out num);
			List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
			list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_LAST_SEASON_BONUS_STARS_POPUP_SEEN, new long[]
			{
				(long)currentMedal.seasonId
			}));
			List<GameSaveDataManager.SubkeySaveRequest> list2 = list;
			GameSaveKeyId key = GameSaveKeyId.RANKED_PLAY;
			GameSaveKeySubkeyId subkey = GameSaveKeySubkeyId.RANKED_PLAY_BONUS_STARS_POPUP_SEEN_COUNT;
			long[] array = new long[1];
			num = (array[0] = num + 1L);
			list2.Add(new GameSaveDataManager.SubkeySaveRequest(key, subkey, array));
			GameSaveDataManager.Get().SaveSubkeys(list, null);
		}
	}

	// Token: 0x04004D7D RID: 19837
	public UberText m_descriptionText;

	// Token: 0x04004D7E RID: 19838
	public UberText m_finePrintText;

	// Token: 0x04004D7F RID: 19839
	private const string SHOW_EVENT_NAME = "CODE_DIALOGMANAGER_SHOW";

	// Token: 0x04004D80 RID: 19840
	private const string HIDE_EVENT_NAME = "CODE_DIALOGMANAGER_HIDE";

	// Token: 0x04004D81 RID: 19841
	private const string HIDE_FINISHED_EVENT_NAME = "CODE_HIDE_FINISHED";

	// Token: 0x04004D82 RID: 19842
	private const string SETUP_SCENE_LOGIN = "SetUp_Scene_Login";

	// Token: 0x04004D83 RID: 19843
	private const string SETUP_SCENE_PLAYSCREEN = "SetUp_Scene_PlayScreen";

	// Token: 0x04004D84 RID: 19844
	private WidgetTemplate m_widget;

	// Token: 0x02002155 RID: 8533
	public class BonusStarsPopupInfo : BasicPopup.PopupInfo
	{
		// Token: 0x0400DFF2 RID: 57330
		public Action m_onHiddenCallback;
	}
}
