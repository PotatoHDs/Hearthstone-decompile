using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000B2B RID: 2859
[RequireComponent(typeof(WidgetTemplate))]
public class SetRotationRotatedBoostersPopup : BasicPopup
{
	// Token: 0x06009796 RID: 38806 RVA: 0x003103B9 File Offset: 0x0030E5B9
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
		this.BindRankedPackListDataModel();
	}

	// Token: 0x06009797 RID: 38807 RVA: 0x003103E4 File Offset: 0x0030E5E4
	public override void Show()
	{
		if (this.m_widget == null)
		{
			return;
		}
		Vector3 localScale = base.transform.localScale;
		base.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		this.m_widget.TriggerEvent("CODE_DIALOGMANAGER_SHOW", default(Widget.TriggerEventParameters));
		if (!string.IsNullOrEmpty(this.m_showAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_showAnimationSound);
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			0.3f,
			"easetype",
			iTween.EaseType.easeOutBack
		});
		iTween.ScaleTo(base.gameObject, args);
	}

	// Token: 0x06009798 RID: 38808 RVA: 0x003104B8 File Offset: 0x0030E6B8
	public override void Hide()
	{
		SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo setRotationRotatedBoostersPopupInfo = this.m_popupInfo as SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo;
		if (setRotationRotatedBoostersPopupInfo != null && setRotationRotatedBoostersPopupInfo.m_onHiddenCallback != null)
		{
			setRotationRotatedBoostersPopupInfo.m_onHiddenCallback();
		}
		this.m_widget.TriggerEvent("CODE_DIALOGMANAGER_HIDE", default(Widget.TriggerEventParameters));
	}

	// Token: 0x06009799 RID: 38809 RVA: 0x00310504 File Offset: 0x0030E704
	private void BindRankedPackListDataModel()
	{
		PackListDataModel packListDataModel = new PackListDataModel();
		SpecialEventManager events = SpecialEventManager.Get();
		List<BoosterDbfRecord> records = GameDbf.Booster.GetRecords((BoosterDbfRecord r) => events.IsEventActive(r.BuyWithGoldEvent, false), -1);
		records.Sort((BoosterDbfRecord a, BoosterDbfRecord b) => b.LatestExpansionOrder.CompareTo(a.LatestExpansionOrder));
		foreach (BoosterDbfRecord boosterDbfRecord in records)
		{
			if (GameUtils.IsBoosterRotated((BoosterDbId)boosterDbfRecord.ID, DateTime.UtcNow))
			{
				PackDataModel packDataModel = new PackDataModel();
				packDataModel.Type = (BoosterDbId)boosterDbfRecord.ID;
				packDataModel.BoosterName = boosterDbfRecord.Name;
				packListDataModel.Packs.Insert(0, packDataModel);
				if (packListDataModel.Packs.Count >= 3)
				{
					break;
				}
			}
		}
		this.m_widget.BindDataModel(packListDataModel, false);
	}

	// Token: 0x04007EEF RID: 32495
	private Widget m_widget;

	// Token: 0x04007EF0 RID: 32496
	private const int NUM_DISPLAY_PACKS = 3;

	// Token: 0x04007EF1 RID: 32497
	private const string SHOW_EVENT_NAME = "CODE_DIALOGMANAGER_SHOW";

	// Token: 0x04007EF2 RID: 32498
	private const string HIDE_EVENT_NAME = "CODE_DIALOGMANAGER_HIDE";

	// Token: 0x04007EF3 RID: 32499
	private const string HIDE_FINISHED_EVENT_NAME = "CODE_HIDE_FINISHED";

	// Token: 0x04007EF4 RID: 32500
	private SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo m_info;

	// Token: 0x0200276D RID: 10093
	public class SetRotationRotatedBoostersPopupInfo : BasicPopup.PopupInfo
	{
		// Token: 0x0400F407 RID: 62471
		public Action m_onHiddenCallback;
	}
}
