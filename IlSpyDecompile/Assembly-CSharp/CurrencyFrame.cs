using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.UI;
using UnityEngine;

public class CurrencyFrame : MonoBehaviour
{
	public enum State
	{
		ANIMATE_IN,
		ANIMATE_OUT,
		HIDDEN,
		SHOWN
	}

	public GameObject m_dustFX;

	public GameObject m_explodeFX_Common;

	public GameObject m_explodeFX_Rare;

	public GameObject m_explodeFX_Epic;

	public GameObject m_explodeFX_Legendary;

	[SerializeField]
	protected GameObject m_currencyIconContainer;

	[SerializeField]
	protected Clickable m_clickable;

	private Widget m_widget;

	private State m_state = State.SHOWN;

	private CurrencyType m_showingCurrency;

	private bool m_blockedStatus;

	private void Awake()
	{
		m_widget = GetComponent<Widget>();
		m_widget.RegisterEventListener(OnWidgetEvent);
		if (m_clickable != null)
		{
			m_clickable.AddEventListener(UIEventType.ROLLOVER, OnFrameMouseOver);
			m_clickable.AddEventListener(UIEventType.ROLLOUT, OnFrameMouseOut);
		}
	}

	private void Start()
	{
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar != null)
		{
			bnetBar.RegisterCurrencyFrame(this);
		}
		ShowImmediate(CurrencyType.NONE);
	}

	public void DeactivateCurrencyFrame()
	{
		base.gameObject.SetActive(value: false);
		m_state = State.HIDDEN;
	}

	public GameObject GetCurrencyIconContainer()
	{
		return m_currencyIconContainer;
	}

	public void SetBlockedStatus(bool blockedStatus)
	{
		m_blockedStatus = blockedStatus;
		if (!blockedStatus)
		{
			RefreshContents();
		}
	}

	public void RefreshContents()
	{
		if (this == null || base.gameObject == null)
		{
			Log.Store.PrintWarning("Attempted to call RefreshContents() on a destroyed CurrencyFrame");
		}
		else if (!m_blockedStatus)
		{
			CurrencyType currencyToShow = GetCurrencyToShow();
			Show(currencyToShow);
		}
	}

	public void HideTemporarily()
	{
		Hashtable args = iTween.Hash("amount", 0f, "time", 0.25f, "easeType", iTween.EaseType.easeOutCubic);
		iTween.FadeTo(base.gameObject, args);
		m_state = State.HIDDEN;
		BindCurrency(CurrencyType.NONE);
	}

	public GameObject GetTooltipObject()
	{
		TooltipZone component = GetComponent<TooltipZone>();
		if (component != null)
		{
			return component.GetTooltipObject();
		}
		return null;
	}

	private void ShowImmediate(CurrencyType currencyType)
	{
		bool flag = currencyType != CurrencyType.NONE;
		BindCurrency(currencyType);
		base.gameObject.SetActive(flag);
		iTween.Stop(base.gameObject, includechildren: true);
		Renderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<Renderer>();
		float a = (flag ? 1f : 0f);
		Renderer[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GetMaterial().color = new Color(1f, 1f, 1f, a);
		}
		m_state = (flag ? State.SHOWN : State.HIDDEN);
	}

	private void Show(CurrencyType currencyType)
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			ShowImmediate(currencyType);
			return;
		}
		bool flag = currencyType != CurrencyType.NONE;
		if (DemoMgr.Get() != null && !DemoMgr.Get().IsCurrencyEnabled())
		{
			flag = false;
		}
		if (flag)
		{
			if (m_state == State.SHOWN || m_state == State.ANIMATE_IN)
			{
				ShowCurrencyType(currencyType);
				return;
			}
			m_state = State.ANIMATE_IN;
			base.gameObject.SetActive(value: true);
			Hashtable args = iTween.Hash("amount", 1f, "delay", 0f, "time", 0.25f, "easeType", iTween.EaseType.easeOutCubic, "oncomplete", "ActivateCurrencyFrame", "oncompletetarget", base.gameObject);
			iTween.Stop(base.gameObject);
			iTween.FadeTo(base.gameObject, args);
			ShowCurrencyType(currencyType);
		}
		else if (m_state == State.HIDDEN || m_state == State.ANIMATE_OUT)
		{
			ShowCurrencyType(currencyType);
		}
		else
		{
			m_state = State.ANIMATE_OUT;
			Hashtable args2 = iTween.Hash("amount", 0f, "delay", 0f, "time", 0.25f, "easeType", iTween.EaseType.easeOutCubic, "oncomplete", "DeactivateCurrencyFrame", "oncompletetarget", base.gameObject);
			iTween.Stop(base.gameObject);
			iTween.FadeTo(base.gameObject, args2);
			ShowCurrencyType(currencyType);
		}
	}

	private void BindCurrency(CurrencyType type)
	{
		if (!(Shop.Get() == null))
		{
			m_showingCurrency = type;
			Widget.TriggerEventParameters triggerEventParameters = default(Widget.TriggerEventParameters);
			triggerEventParameters.NoDownwardPropagation = true;
			Widget.TriggerEventParameters parameters = triggerEventParameters;
			switch (type)
			{
			case CurrencyType.RUNESTONES:
				m_widget.TriggerEvent("VIRTUAL_CURRENCY", parameters);
				break;
			case CurrencyType.ARCANE_ORBS:
				m_widget.TriggerEvent("BOOSTER_CURRENCY", parameters);
				break;
			case CurrencyType.GOLD:
				m_widget.TriggerEvent("GOLD", parameters);
				break;
			case CurrencyType.DUST:
				m_widget.TriggerEvent("DUST", parameters);
				break;
			default:
				m_widget.TriggerEvent("NONE", parameters);
				break;
			}
		}
	}

	private CurrencyType GetCurrencyToShow()
	{
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar == null)
		{
			return CurrencyType.NONE;
		}
		int currencyFrameIndex = bnetBar.GetCurrencyFrameIndex(this);
		if (currencyFrameIndex < 0)
		{
			return CurrencyType.NONE;
		}
		bool flag = (SceneMgr.Get()?.GetMode() ?? SceneMgr.Mode.INVALID) != SceneMgr.Mode.HUB;
		m_widget.TriggerEvent(flag ? "FADE_BACKGROUND" : "SOLID_BACKGROUND");
		return GetVisibleCurrencies().ElementAtOrDefault(currencyFrameIndex);
	}

	private static IEnumerable<CurrencyType> GetVisibleCurrencies()
	{
		IStore currentStore = StoreManager.Get().GetCurrentStore();
		if (currentStore != null && currentStore.IsOpen())
		{
			return currentStore.GetVisibleCurrencies();
		}
		List<CurrencyType> list = new List<CurrencyType>();
		switch (SceneMgr.Get()?.GetMode() ?? SceneMgr.Mode.INVALID)
		{
		case SceneMgr.Mode.LOGIN:
		case SceneMgr.Mode.FATAL_ERROR:
		case SceneMgr.Mode.RESET:
			return list;
		case SceneMgr.Mode.HUB:
			list.Add(CurrencyType.GOLD);
			break;
		case SceneMgr.Mode.PACKOPENING:
		case SceneMgr.Mode.TOURNAMENT:
		case SceneMgr.Mode.FRIENDLY:
		case SceneMgr.Mode.DRAFT:
		case SceneMgr.Mode.ADVENTURE:
		case SceneMgr.Mode.BACON:
			if (!UniversalInputManager.UsePhoneUI)
			{
				list.Add(CurrencyType.GOLD);
			}
			break;
		case SceneMgr.Mode.PVP_DUNGEON_RUN:
			if (!UniversalInputManager.UsePhoneUI || (PvPDungeonRunScene.Get() != null && PvPDungeonRunScene.Get().GetPopupManager() != null && PvPDungeonRunScene.Get().GetPopupManager().ShouldShowCoinCounter()))
			{
				list.Add(CurrencyType.GOLD);
				if (ShopUtils.IsVirtualCurrencyEnabled())
				{
					list.Add(CurrencyType.RUNESTONES);
				}
			}
			break;
		case SceneMgr.Mode.COLLECTIONMANAGER:
			list.Add(CurrencyType.DUST);
			break;
		case SceneMgr.Mode.TAVERN_BRAWL:
		case SceneMgr.Mode.FIRESIDE_GATHERING:
			if (!UniversalInputManager.UsePhoneUI)
			{
				TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
				if (tavernBrawlDisplay != null && tavernBrawlDisplay.IsInDeckEditMode())
				{
					list.Add(CurrencyType.DUST);
				}
				else
				{
					list.Add(CurrencyType.GOLD);
				}
			}
			break;
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			list.Remove(CurrencyType.DUST);
		}
		return list;
	}

	private void ShowCurrencyType(CurrencyType currencyType)
	{
		if (m_showingCurrency != currencyType)
		{
			BindCurrency(currencyType);
		}
	}

	private void ActivateCurrencyFrame()
	{
		m_state = State.SHOWN;
	}

	private void OnFrameMouseOver(UIEvent e)
	{
		string text = "";
		string key = "";
		switch (m_showingCurrency)
		{
		case CurrencyType.DUST:
			text = "GLUE_CRAFTING_ARCANEDUST";
			key = "GLUE_CRAFTING_ARCANEDUST_DESCRIPTION";
			break;
		case CurrencyType.GOLD:
			text = "GLUE_TOOLTIP_GOLD_HEADER";
			key = "GLUE_TOOLTIP_GOLD_DESCRIPTION";
			break;
		case CurrencyType.RUNESTONES:
			text = "GLUE_TOOLTIP_VIRTUAL_CURRENCY_HEADER";
			key = "GLUE_TOOLTIP_VIRTUAL_CURRENCY_DESCRIPTION";
			break;
		case CurrencyType.ARCANE_ORBS:
			text = "GLUE_TOOLTIP_BOOSTER_CURRENCY_HEADER";
			key = "GLUE_TOOLTIP_BOOSTER_CURRENCY_DESCRIPTION";
			break;
		}
		if (!(text == ""))
		{
			TooltipPanel tooltipPanel = GetComponent<TooltipZone>().ShowTooltip(GameStrings.Get(text), GameStrings.Get(key), 0.7f);
			SceneUtils.SetLayer(tooltipPanel.gameObject, GameLayer.BattleNet);
			tooltipPanel.transform.localEulerAngles = new Vector3(270f, 0f, 0f);
			tooltipPanel.transform.localScale = new Vector3(70f, 70f, 70f);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				TransformUtil.SetPoint(tooltipPanel, Anchor.TOP, m_clickable, Anchor.BOTTOM, Vector3.zero);
			}
			else
			{
				TransformUtil.SetPoint(tooltipPanel, Anchor.BOTTOM, m_clickable, Anchor.TOP, Vector3.zero);
			}
		}
	}

	private void OnFrameMouseOut(UIEvent e)
	{
		GetComponent<TooltipZone>().HideTooltip();
	}

	private void OnWidgetEvent(string eventName)
	{
		if (eventName == "RECHARGE")
		{
			OnAttemptRecharge();
		}
	}

	private void OnAttemptRecharge()
	{
		Shop shop = Shop.Get();
		if (!(shop == null) && shop.CanSafelyOpenCurrencyPage())
		{
			switch (m_showingCurrency)
			{
			case CurrencyType.RUNESTONES:
				shop.OpenVirtualCurrencyPurchase();
				break;
			case CurrencyType.ARCANE_ORBS:
				shop.OpenBoosterCurrencyPurchase();
				break;
			}
		}
	}
}
