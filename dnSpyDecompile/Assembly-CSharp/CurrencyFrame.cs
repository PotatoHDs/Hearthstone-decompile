using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000084 RID: 132
public class CurrencyFrame : MonoBehaviour
{
	// Token: 0x06000786 RID: 1926 RVA: 0x0002AB60 File Offset: 0x00028D60
	private void Awake()
	{
		this.m_widget = base.GetComponent<Widget>();
		this.m_widget.RegisterEventListener(new Widget.EventListenerDelegate(this.OnWidgetEvent));
		if (this.m_clickable != null)
		{
			this.m_clickable.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnFrameMouseOver));
			this.m_clickable.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnFrameMouseOut));
		}
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x0002ABD0 File Offset: 0x00028DD0
	private void Start()
	{
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar != null)
		{
			bnetBar.RegisterCurrencyFrame(this);
		}
		this.ShowImmediate(CurrencyType.NONE);
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x0002ABFA File Offset: 0x00028DFA
	public void DeactivateCurrencyFrame()
	{
		base.gameObject.SetActive(false);
		this.m_state = CurrencyFrame.State.HIDDEN;
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x0002AC0F File Offset: 0x00028E0F
	public GameObject GetCurrencyIconContainer()
	{
		return this.m_currencyIconContainer;
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x0002AC17 File Offset: 0x00028E17
	public void SetBlockedStatus(bool blockedStatus)
	{
		this.m_blockedStatus = blockedStatus;
		if (!blockedStatus)
		{
			this.RefreshContents();
		}
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x0002AC2C File Offset: 0x00028E2C
	public void RefreshContents()
	{
		if (this == null || base.gameObject == null)
		{
			Log.Store.PrintWarning("Attempted to call RefreshContents() on a destroyed CurrencyFrame", Array.Empty<object>());
			return;
		}
		if (!this.m_blockedStatus)
		{
			CurrencyType currencyToShow = this.GetCurrencyToShow();
			this.Show(currencyToShow);
		}
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x0002AC7C File Offset: 0x00028E7C
	public void HideTemporarily()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			0f,
			"time",
			0.25f,
			"easeType",
			iTween.EaseType.easeOutCubic
		});
		iTween.FadeTo(base.gameObject, args);
		this.m_state = CurrencyFrame.State.HIDDEN;
		this.BindCurrency(CurrencyType.NONE);
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x0002ACEC File Offset: 0x00028EEC
	public GameObject GetTooltipObject()
	{
		TooltipZone component = base.GetComponent<TooltipZone>();
		if (component != null)
		{
			return component.GetTooltipObject(0);
		}
		return null;
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x0002AD14 File Offset: 0x00028F14
	private void ShowImmediate(CurrencyType currencyType)
	{
		bool flag = currencyType > CurrencyType.NONE;
		this.BindCurrency(currencyType);
		base.gameObject.SetActive(flag);
		iTween.Stop(base.gameObject, true);
		Renderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<Renderer>();
		float a = flag ? 1f : 0f;
		Renderer[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GetMaterial().color = new Color(1f, 1f, 1f, a);
		}
		this.m_state = (flag ? CurrencyFrame.State.SHOWN : CurrencyFrame.State.HIDDEN);
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0002ADA0 File Offset: 0x00028FA0
	private void Show(CurrencyType currencyType)
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			this.ShowImmediate(currencyType);
			return;
		}
		bool flag = currencyType > CurrencyType.NONE;
		if (DemoMgr.Get() != null && !DemoMgr.Get().IsCurrencyEnabled())
		{
			flag = false;
		}
		if (flag)
		{
			if (this.m_state == CurrencyFrame.State.SHOWN || this.m_state == CurrencyFrame.State.ANIMATE_IN)
			{
				this.ShowCurrencyType(currencyType);
				return;
			}
			this.m_state = CurrencyFrame.State.ANIMATE_IN;
			base.gameObject.SetActive(true);
			Hashtable args = iTween.Hash(new object[]
			{
				"amount",
				1f,
				"delay",
				0f,
				"time",
				0.25f,
				"easeType",
				iTween.EaseType.easeOutCubic,
				"oncomplete",
				"ActivateCurrencyFrame",
				"oncompletetarget",
				base.gameObject
			});
			iTween.Stop(base.gameObject);
			iTween.FadeTo(base.gameObject, args);
			this.ShowCurrencyType(currencyType);
			return;
		}
		else
		{
			if (this.m_state == CurrencyFrame.State.HIDDEN || this.m_state == CurrencyFrame.State.ANIMATE_OUT)
			{
				this.ShowCurrencyType(currencyType);
				return;
			}
			this.m_state = CurrencyFrame.State.ANIMATE_OUT;
			Hashtable args2 = iTween.Hash(new object[]
			{
				"amount",
				0f,
				"delay",
				0f,
				"time",
				0.25f,
				"easeType",
				iTween.EaseType.easeOutCubic,
				"oncomplete",
				"DeactivateCurrencyFrame",
				"oncompletetarget",
				base.gameObject
			});
			iTween.Stop(base.gameObject);
			iTween.FadeTo(base.gameObject, args2);
			this.ShowCurrencyType(currencyType);
			return;
		}
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x0002AF70 File Offset: 0x00029170
	private void BindCurrency(CurrencyType type)
	{
		if (Shop.Get() == null)
		{
			return;
		}
		this.m_showingCurrency = type;
		Widget.TriggerEventParameters parameters = new Widget.TriggerEventParameters
		{
			NoDownwardPropagation = true
		};
		switch (type)
		{
		case CurrencyType.GOLD:
			this.m_widget.TriggerEvent("GOLD", parameters);
			return;
		case CurrencyType.DUST:
			this.m_widget.TriggerEvent("DUST", parameters);
			return;
		case CurrencyType.RUNESTONES:
			this.m_widget.TriggerEvent("VIRTUAL_CURRENCY", parameters);
			return;
		case CurrencyType.ARCANE_ORBS:
			this.m_widget.TriggerEvent("BOOSTER_CURRENCY", parameters);
			return;
		}
		this.m_widget.TriggerEvent("NONE", parameters);
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x0002B020 File Offset: 0x00029220
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
		SceneMgr sceneMgr = SceneMgr.Get();
		bool flag = ((sceneMgr == null) ? SceneMgr.Mode.INVALID : sceneMgr.GetMode()) != SceneMgr.Mode.HUB;
		this.m_widget.TriggerEvent(flag ? "FADE_BACKGROUND" : "SOLID_BACKGROUND", default(Widget.TriggerEventParameters));
		return CurrencyFrame.GetVisibleCurrencies().ElementAtOrDefault(currencyFrameIndex);
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x0002B098 File Offset: 0x00029298
	private static IEnumerable<CurrencyType> GetVisibleCurrencies()
	{
		IStore currentStore = StoreManager.Get().GetCurrentStore();
		if (currentStore != null && currentStore.IsOpen())
		{
			return currentStore.GetVisibleCurrencies();
		}
		List<CurrencyType> list = new List<CurrencyType>();
		SceneMgr sceneMgr = SceneMgr.Get();
		switch ((sceneMgr == null) ? SceneMgr.Mode.INVALID : sceneMgr.GetMode())
		{
		case SceneMgr.Mode.LOGIN:
		case SceneMgr.Mode.FATAL_ERROR:
		case SceneMgr.Mode.RESET:
			return list;
		case SceneMgr.Mode.HUB:
			list.Add(CurrencyType.GOLD);
			break;
		case SceneMgr.Mode.COLLECTIONMANAGER:
			list.Add(CurrencyType.DUST);
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
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			list.Remove(CurrencyType.DUST);
		}
		return list;
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x0002B1FC File Offset: 0x000293FC
	private void ShowCurrencyType(CurrencyType currencyType)
	{
		if (this.m_showingCurrency == currencyType)
		{
			return;
		}
		this.BindCurrency(currencyType);
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x0002B20F File Offset: 0x0002940F
	private void ActivateCurrencyFrame()
	{
		this.m_state = CurrencyFrame.State.SHOWN;
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x0002B218 File Offset: 0x00029418
	private void OnFrameMouseOver(UIEvent e)
	{
		string text = "";
		string key = "";
		switch (this.m_showingCurrency)
		{
		case CurrencyType.GOLD:
			text = "GLUE_TOOLTIP_GOLD_HEADER";
			key = "GLUE_TOOLTIP_GOLD_DESCRIPTION";
			break;
		case CurrencyType.DUST:
			text = "GLUE_CRAFTING_ARCANEDUST";
			key = "GLUE_CRAFTING_ARCANEDUST_DESCRIPTION";
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
		if (text == "")
		{
			return;
		}
		TooltipPanel tooltipPanel = base.GetComponent<TooltipZone>().ShowTooltip(GameStrings.Get(text), GameStrings.Get(key), 0.7f, 0);
		SceneUtils.SetLayer(tooltipPanel.gameObject, GameLayer.BattleNet);
		tooltipPanel.transform.localEulerAngles = new Vector3(270f, 0f, 0f);
		tooltipPanel.transform.localScale = new Vector3(70f, 70f, 70f);
		if (UniversalInputManager.UsePhoneUI)
		{
			TransformUtil.SetPoint(tooltipPanel, Anchor.TOP, this.m_clickable, Anchor.BOTTOM, Vector3.zero);
			return;
		}
		TransformUtil.SetPoint(tooltipPanel, Anchor.BOTTOM, this.m_clickable, Anchor.TOP, Vector3.zero);
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x0002B336 File Offset: 0x00029536
	private void OnFrameMouseOut(UIEvent e)
	{
		base.GetComponent<TooltipZone>().HideTooltip();
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x0002B343 File Offset: 0x00029543
	private void OnWidgetEvent(string eventName)
	{
		if (eventName == "RECHARGE")
		{
			this.OnAttemptRecharge();
		}
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x0002B358 File Offset: 0x00029558
	private void OnAttemptRecharge()
	{
		Shop shop = Shop.Get();
		if (shop == null || !shop.CanSafelyOpenCurrencyPage())
		{
			return;
		}
		CurrencyType showingCurrency = this.m_showingCurrency;
		if (showingCurrency == CurrencyType.RUNESTONES)
		{
			shop.OpenVirtualCurrencyPurchase(0f, false);
			return;
		}
		if (showingCurrency != CurrencyType.ARCANE_ORBS)
		{
			return;
		}
		shop.OpenBoosterCurrencyPurchase(0f, false);
	}

	// Token: 0x0400051D RID: 1309
	public GameObject m_dustFX;

	// Token: 0x0400051E RID: 1310
	public GameObject m_explodeFX_Common;

	// Token: 0x0400051F RID: 1311
	public GameObject m_explodeFX_Rare;

	// Token: 0x04000520 RID: 1312
	public GameObject m_explodeFX_Epic;

	// Token: 0x04000521 RID: 1313
	public GameObject m_explodeFX_Legendary;

	// Token: 0x04000522 RID: 1314
	[SerializeField]
	protected GameObject m_currencyIconContainer;

	// Token: 0x04000523 RID: 1315
	[SerializeField]
	protected Clickable m_clickable;

	// Token: 0x04000524 RID: 1316
	private Widget m_widget;

	// Token: 0x04000525 RID: 1317
	private CurrencyFrame.State m_state = CurrencyFrame.State.SHOWN;

	// Token: 0x04000526 RID: 1318
	private CurrencyType m_showingCurrency;

	// Token: 0x04000527 RID: 1319
	private bool m_blockedStatus;

	// Token: 0x02001382 RID: 4994
	public enum State
	{
		// Token: 0x0400A6EA RID: 42730
		ANIMATE_IN,
		// Token: 0x0400A6EB RID: 42731
		ANIMATE_OUT,
		// Token: 0x0400A6EC RID: 42732
		HIDDEN,
		// Token: 0x0400A6ED RID: 42733
		SHOWN
	}
}
