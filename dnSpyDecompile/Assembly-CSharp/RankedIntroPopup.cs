using System;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x02000648 RID: 1608
[RequireComponent(typeof(WidgetTemplate))]
public class RankedIntroPopup : BasicPopup
{
	// Token: 0x06005AB8 RID: 23224 RVA: 0x001D99B0 File Offset: 0x001D7BB0
	protected override void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == "Button_Framed_Clicked")
			{
				this.Hide();
			}
			if (eventName == "CODE_HIDE_FINISHED" && this.m_readyToDestroyCallback != null)
			{
				this.m_readyToDestroyCallback(this);
			}
		});
	}

	// Token: 0x06005AB9 RID: 23225 RVA: 0x001D99D8 File Offset: 0x001D7BD8
	protected override void OnDestroy()
	{
		GameObject gameObject = base.transform.parent.gameObject;
		if (gameObject != null && gameObject.GetComponent<WidgetInstance>() != null)
		{
			UnityEngine.Object.Destroy(base.transform.parent.gameObject);
		}
		base.OnDestroy();
	}

	// Token: 0x06005ABA RID: 23226 RVA: 0x001D9A28 File Offset: 0x001D7C28
	public override void Show()
	{
		if (this.m_widget == null)
		{
			return;
		}
		UIContext.GetRoot().ShowPopup(base.gameObject, UIContext.BlurType.Standard);
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

	// Token: 0x06005ABB RID: 23227 RVA: 0x001D9AD0 File Offset: 0x001D7CD0
	public override void Hide()
	{
		RankedIntroPopup.RankedIntroPopupInfo rankedIntroPopupInfo = this.m_popupInfo as RankedIntroPopup.RankedIntroPopupInfo;
		if (rankedIntroPopupInfo != null && rankedIntroPopupInfo.m_onHiddenCallback != null)
		{
			rankedIntroPopupInfo.m_onHiddenCallback();
		}
		this.m_widget.TriggerEvent("CODE_DIALOGMANAGER_HIDE", default(Widget.TriggerEventParameters));
		this.IncrementRankedIntroPopupSeenCount();
	}

	// Token: 0x06005ABC RID: 23228 RVA: 0x001D9B20 File Offset: 0x001D7D20
	private void IncrementRankedIntroPopupSeenCount()
	{
		if (RankMgr.Get().GetLocalPlayerMedalInfo().GetCurrentMedal(FormatType.FT_STANDARD) != null)
		{
			long num = 0L;
			GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_INTRO_SEEN_COUNT, out num);
			GameSaveDataManager gameSaveDataManager = GameSaveDataManager.Get();
			GameSaveKeyId key = GameSaveKeyId.RANKED_PLAY;
			GameSaveKeySubkeyId subkey = GameSaveKeySubkeyId.RANKED_PLAY_INTRO_SEEN_COUNT;
			long[] array = new long[1];
			num = (array[0] = num + 1L);
			gameSaveDataManager.SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(key, subkey, array), null);
		}
	}

	// Token: 0x04004D94 RID: 19860
	private const string SHOW_EVENT_NAME = "CODE_DIALOGMANAGER_SHOW";

	// Token: 0x04004D95 RID: 19861
	private const string HIDE_EVENT_NAME = "CODE_DIALOGMANAGER_HIDE";

	// Token: 0x04004D96 RID: 19862
	private const string HIDE_FINISHED_EVENT_NAME = "CODE_HIDE_FINISHED";

	// Token: 0x04004D97 RID: 19863
	private const string SETUP_SCENE_LOGIN = "SetUp_Scene_Login";

	// Token: 0x04004D98 RID: 19864
	private const string SETUP_SCENE_PLAYSCREEN = "SetUp_Scene_PlayScreen";

	// Token: 0x04004D99 RID: 19865
	private WidgetTemplate m_widget;

	// Token: 0x02002159 RID: 8537
	public class RankedIntroPopupInfo : BasicPopup.PopupInfo
	{
		// Token: 0x0400DFFC RID: 57340
		public Action m_onHiddenCallback;
	}
}
