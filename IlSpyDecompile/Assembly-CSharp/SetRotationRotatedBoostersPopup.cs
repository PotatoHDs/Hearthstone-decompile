using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class SetRotationRotatedBoostersPopup : BasicPopup
{
	public class SetRotationRotatedBoostersPopupInfo : PopupInfo
	{
		public Action m_onHiddenCallback;
	}

	private Widget m_widget;

	private const int NUM_DISPLAY_PACKS = 3;

	private const string SHOW_EVENT_NAME = "CODE_DIALOGMANAGER_SHOW";

	private const string HIDE_EVENT_NAME = "CODE_DIALOGMANAGER_HIDE";

	private const string HIDE_FINISHED_EVENT_NAME = "CODE_HIDE_FINISHED";

	private SetRotationRotatedBoostersPopupInfo m_info;

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
		BindRankedPackListDataModel();
	}

	public override void Show()
	{
		if (!(m_widget == null))
		{
			Vector3 localScale = base.transform.localScale;
			base.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
			m_widget.TriggerEvent("CODE_DIALOGMANAGER_SHOW");
			if (!string.IsNullOrEmpty(m_showAnimationSound))
			{
				SoundManager.Get().LoadAndPlay(m_showAnimationSound);
			}
			Hashtable args = iTween.Hash("scale", localScale, "time", 0.3f, "easetype", iTween.EaseType.easeOutBack);
			iTween.ScaleTo(base.gameObject, args);
		}
	}

	public override void Hide()
	{
		SetRotationRotatedBoostersPopupInfo setRotationRotatedBoostersPopupInfo = m_popupInfo as SetRotationRotatedBoostersPopupInfo;
		if (setRotationRotatedBoostersPopupInfo != null && setRotationRotatedBoostersPopupInfo.m_onHiddenCallback != null)
		{
			setRotationRotatedBoostersPopupInfo.m_onHiddenCallback();
		}
		m_widget.TriggerEvent("CODE_DIALOGMANAGER_HIDE");
	}

	private void BindRankedPackListDataModel()
	{
		PackListDataModel packListDataModel = new PackListDataModel();
		SpecialEventManager events = SpecialEventManager.Get();
		List<BoosterDbfRecord> records = GameDbf.Booster.GetRecords((BoosterDbfRecord r) => events.IsEventActive(r.BuyWithGoldEvent, activeIfDoesNotExist: false));
		records.Sort((BoosterDbfRecord a, BoosterDbfRecord b) => b.LatestExpansionOrder.CompareTo(a.LatestExpansionOrder));
		foreach (BoosterDbfRecord item in records)
		{
			if (GameUtils.IsBoosterRotated((BoosterDbId)item.ID, DateTime.UtcNow))
			{
				PackDataModel packDataModel = new PackDataModel();
				packDataModel.Type = (BoosterDbId)item.ID;
				packDataModel.BoosterName = item.Name;
				packListDataModel.Packs.Insert(0, packDataModel);
				if (packListDataModel.Packs.Count >= 3)
				{
					break;
				}
			}
		}
		m_widget.BindDataModel(packListDataModel);
	}
}
