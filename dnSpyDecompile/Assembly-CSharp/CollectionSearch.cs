using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000117 RID: 279
public class CollectionSearch : MonoBehaviour
{
	// Token: 0x06001261 RID: 4705 RVA: 0x000692A8 File Offset: 0x000674A8
	private void Start()
	{
		this.m_background.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackgroundReleased));
		this.m_clearButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClearReleased));
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		touchScreenService.AddOnVirtualKeyboardShowListener(new Action(this.OnKeyboardShown));
		touchScreenService.AddOnVirtualKeyboardHideListener(new Action(this.OnKeyboardHidden));
		this.m_origSearchPos = base.transform.localPosition;
		this.UpdateBackground();
		this.UpdateSearchText();
	}

	// Token: 0x06001262 RID: 4706 RVA: 0x0006932C File Offset: 0x0006752C
	private void OnDestroy()
	{
		ITouchScreenService touchScreenService;
		if (HearthstoneServices.TryGet<ITouchScreenService>(out touchScreenService))
		{
			touchScreenService.RemoveOnVirtualKeyboardShowListener(new Action(this.OnKeyboardShown));
			touchScreenService.RemoveOnVirtualKeyboardHideListener(new Action(this.OnKeyboardHidden));
		}
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().CancelTextInput(base.gameObject, false);
		}
	}

	// Token: 0x06001263 RID: 4707 RVA: 0x0006937E File Offset: 0x0006757E
	public bool IsActive()
	{
		return this.m_isActive;
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x00069386 File Offset: 0x00067586
	public void SetActiveLayer(GameLayer activeLayer)
	{
		if (activeLayer == this.m_activeLayer)
		{
			return;
		}
		this.m_activeLayer = activeLayer;
		if (!this.IsActive())
		{
			return;
		}
		this.MoveToActiveLayer(false);
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x000693A9 File Offset: 0x000675A9
	public void SetWildModeActive(bool active)
	{
		this.m_wildModeActive = active;
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x000693B4 File Offset: 0x000675B4
	public void Activate(bool ignoreTouchMode = false)
	{
		if (this.m_isActive)
		{
			return;
		}
		this.m_background.SetEnabled(false, false);
		this.MoveToActiveLayer(true);
		this.m_isActive = true;
		this.m_prevText = this.m_text;
		CollectionSearch.ActivatedListener[] array = this.m_activatedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		if (!ignoreTouchMode && UniversalInputManager.Get().UseWindowsTouch() && touchScreenService.IsTouchSupported() && touchScreenService.IsVirtualKeyboardVisible())
		{
			this.TouchKeyboardSearchDisplay(true);
			return;
		}
		this.ShowInput(true);
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x00069448 File Offset: 0x00067648
	public void Deactivate()
	{
		if (!this.m_isActive)
		{
			return;
		}
		this.m_background.SetEnabled(true, false);
		this.MoveToOriginalLayer();
		this.m_isActive = false;
		this.HideInput();
		this.ResetSearchDisplay();
		CollectionSearch.DeactivatedListener[] array = this.m_deactivatedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this.m_prevText, this.m_text);
		}
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x000694B2 File Offset: 0x000676B2
	public void Cancel()
	{
		if (!this.m_isActive)
		{
			return;
		}
		this.m_text = this.m_prevText;
		this.UpdateSearchText();
		this.Deactivate();
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x000694D5 File Offset: 0x000676D5
	public string GetText()
	{
		return this.m_text;
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x000694DD File Offset: 0x000676DD
	public void SetText(string text)
	{
		this.m_text = text;
		this.UpdateSearchText();
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x000694EC File Offset: 0x000676EC
	public void ClearFilter(bool updateVisuals = true)
	{
		this.m_text = "";
		this.UpdateSearchText();
		this.ClearInput();
		CollectionSearch.ClearedListener[] array = this.m_clearedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](updateVisuals);
		}
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		if ((UniversalInputManager.Get().IsTouchMode() && touchScreenService.IsTouchSupported()) || touchScreenService.IsVirtualKeyboardVisible())
		{
			this.Deactivate();
		}
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x0006955B File Offset: 0x0006775B
	public void RegisterActivatedListener(CollectionSearch.ActivatedListener listener)
	{
		if (this.m_activatedListeners.Contains(listener))
		{
			return;
		}
		this.m_activatedListeners.Add(listener);
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x00069578 File Offset: 0x00067778
	public void RemoveActivatedListener(CollectionSearch.ActivatedListener listener)
	{
		this.m_activatedListeners.Remove(listener);
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x00069587 File Offset: 0x00067787
	public void RegisterDeactivatedListener(CollectionSearch.DeactivatedListener listener)
	{
		if (this.m_deactivatedListeners.Contains(listener))
		{
			return;
		}
		this.m_deactivatedListeners.Add(listener);
	}

	// Token: 0x0600126F RID: 4719 RVA: 0x000695A4 File Offset: 0x000677A4
	public void RemoveDeactivatedListener(CollectionSearch.DeactivatedListener listener)
	{
		this.m_deactivatedListeners.Remove(listener);
	}

	// Token: 0x06001270 RID: 4720 RVA: 0x000695B3 File Offset: 0x000677B3
	public void RegisterClearedListener(CollectionSearch.ClearedListener listener)
	{
		if (this.m_clearedListeners.Contains(listener))
		{
			return;
		}
		this.m_clearedListeners.Add(listener);
	}

	// Token: 0x06001271 RID: 4721 RVA: 0x000695D0 File Offset: 0x000677D0
	public void RemoveClearedListener(CollectionSearch.ClearedListener listener)
	{
		this.m_clearedListeners.Remove(listener);
	}

	// Token: 0x06001272 RID: 4722 RVA: 0x000695DF File Offset: 0x000677DF
	public void SetEnabled(bool enabled)
	{
		this.m_background.SetEnabled(enabled, false);
		this.m_clearButton.SetEnabled(enabled, false);
	}

	// Token: 0x06001273 RID: 4723 RVA: 0x000695FB File Offset: 0x000677FB
	private void OnBackgroundReleased(UIEvent e)
	{
		this.Activate(false);
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x00069604 File Offset: 0x00067804
	private void OnClearReleased(UIEvent e)
	{
		this.ClearFilter(true);
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x0006960D File Offset: 0x0006780D
	private void OnActivateAnimComplete()
	{
		this.ShowInput(true);
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x00069618 File Offset: 0x00067818
	private void OnDeactivateAnimComplete()
	{
		CollectionSearch.DeactivatedListener[] array = this.m_deactivatedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this.m_prevText, this.m_text);
		}
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x00069654 File Offset: 0x00067854
	private void ShowInput(bool fromActivate = true)
	{
		Bounds bounds = this.m_searchText.GetBounds();
		this.m_searchText.gameObject.SetActive(false);
		Rect rect = CameraUtils.CreateGUIViewportRect(Box.Get().GetCamera(), bounds.min, bounds.max);
		Color? color = null;
		if (HearthstoneServices.Get<ITouchScreenService>().IsVirtualKeyboardVisible())
		{
			color = new Color?(this.m_altSearchColor);
		}
		UniversalInputManager.TextInputParams textInputParams = new UniversalInputManager.TextInputParams
		{
			m_owner = base.gameObject,
			m_rect = rect,
			m_updatedCallback = new UniversalInputManager.TextInputUpdatedCallback(this.OnInputUpdated),
			m_completedCallback = new UniversalInputManager.TextInputCompletedCallback(this.OnInputComplete),
			m_canceledCallback = new UniversalInputManager.TextInputCanceledCallback(this.OnInputCanceled),
			m_unfocusedCallback = new UniversalInputManager.TextInputUnfocusedCallback(this.OnInputUnfocus),
			m_font = this.m_searchText.GetLocalizedFont(),
			m_text = this.m_text,
			m_color = color,
			m_touchScreenKeyboardHideInput = false
		};
		textInputParams.m_showVirtualKeyboard = fromActivate;
		UniversalInputManager.Get().UseTextInput(textInputParams, false);
	}

	// Token: 0x06001278 RID: 4728 RVA: 0x0006975C File Offset: 0x0006795C
	private void HideInput()
	{
		UniversalInputManager.Get().CancelTextInput(base.gameObject, false);
		this.m_searchText.gameObject.SetActive(true);
	}

	// Token: 0x06001279 RID: 4729 RVA: 0x00069780 File Offset: 0x00067980
	private void ClearInput()
	{
		if (!this.m_isActive)
		{
			return;
		}
		SoundManager.Get().LoadAndPlay("text_box_delete_text.prefab:b4209934f760cc745b3dba5add912398");
		UniversalInputManager.Get().SetInputText("", false);
	}

	// Token: 0x0600127A RID: 4730 RVA: 0x000694DD File Offset: 0x000676DD
	private void OnInputUpdated(string input)
	{
		this.m_text = input;
		this.UpdateSearchText();
	}

	// Token: 0x0600127B RID: 4731 RVA: 0x000697AF File Offset: 0x000679AF
	private void OnInputComplete(string input)
	{
		this.m_text = input;
		this.UpdateSearchText();
		SoundManager.Get().LoadAndPlay("text_commit.prefab:05a794ae046d3e842b87893629a826f1");
		this.Deactivate();
	}

	// Token: 0x0600127C RID: 4732 RVA: 0x000697D8 File Offset: 0x000679D8
	private void OnInputCanceled(bool userRequested, GameObject requester)
	{
		this.Cancel();
	}

	// Token: 0x0600127D RID: 4733 RVA: 0x000697E0 File Offset: 0x000679E0
	private void OnInputUnfocus()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.Deactivate();
		}
	}

	// Token: 0x0600127E RID: 4734 RVA: 0x000697F4 File Offset: 0x000679F4
	private void UpdateSearchText()
	{
		if (string.IsNullOrEmpty(this.m_text))
		{
			this.m_searchText.Text = GameStrings.Get("GLUE_COLLECTION_SEARCH");
			this.m_clearButton.gameObject.SetActive(false);
			return;
		}
		this.m_searchText.Text = this.m_text;
		this.m_clearButton.gameObject.SetActive(true);
	}

	// Token: 0x0600127F RID: 4735 RVA: 0x00069857 File Offset: 0x00067A57
	private void MoveToActiveLayer(bool saveOriginalLayer)
	{
		if (saveOriginalLayer)
		{
			this.m_originalLayer = (GameLayer)base.gameObject.layer;
		}
		SceneUtils.SetLayer(base.gameObject, this.m_activeLayer);
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x0006987E File Offset: 0x00067A7E
	private void MoveToOriginalLayer()
	{
		SceneUtils.SetLayer(base.gameObject, this.m_originalLayer);
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x00069894 File Offset: 0x00067A94
	private void TouchKeyboardSearchDisplay(bool fromActivate = false)
	{
		if (this.m_isTouchKeyboardDisplayMode)
		{
			return;
		}
		this.m_isTouchKeyboardDisplayMode = true;
		base.transform.localPosition = CollectionManager.Get().GetCollectibleDisplay().m_activeSearchBone_Win8.transform.localPosition;
		this.HideInput();
		this.ShowInput(fromActivate || HearthstoneServices.Get<ITouchScreenService>().IsVirtualKeyboardVisible());
		this.m_xMesh.GetComponent<Renderer>().GetMaterial().SetColor("_Color", this.m_altSearchColor);
		this.UpdateBackground();
	}

	// Token: 0x06001282 RID: 4738 RVA: 0x00069918 File Offset: 0x00067B18
	private void ResetSearchDisplay()
	{
		if (!this.m_isTouchKeyboardDisplayMode)
		{
			return;
		}
		this.m_isTouchKeyboardDisplayMode = false;
		base.transform.localPosition = this.m_origSearchPos;
		this.HideInput();
		this.ShowInput(false);
		this.m_xMesh.GetComponent<Renderer>().GetMaterial().SetColor("_Color", Color.white);
		this.UpdateBackground();
	}

	// Token: 0x06001283 RID: 4739 RVA: 0x00069978 File Offset: 0x00067B78
	private void OnKeyboardShown()
	{
		if (this.m_isActive && !this.m_isTouchKeyboardDisplayMode)
		{
			this.TouchKeyboardSearchDisplay(false);
		}
	}

	// Token: 0x06001284 RID: 4740 RVA: 0x00069991 File Offset: 0x00067B91
	private void OnKeyboardHidden()
	{
		if (this.m_isActive && this.m_isTouchKeyboardDisplayMode)
		{
			this.ResetSearchDisplay();
		}
	}

	// Token: 0x06001285 RID: 4741 RVA: 0x000699AC File Offset: 0x00067BAC
	private void UpdateBackground()
	{
		bool flag = this.m_backgroundWhenAtTopNormal != null || this.m_backgroundWhenAtTopWild != null;
		GameObject gameObject = (SceneMgr.Get().IsInTavernBrawlMode() && this.m_backgroundWhenAtBottomTavernBrawl != null) ? this.m_backgroundWhenAtBottomTavernBrawl : this.m_backgroundWhenAtBottom;
		if (gameObject != null)
		{
			gameObject.gameObject.SetActive(!this.m_isTouchKeyboardDisplayMode || !flag);
		}
		if (this.m_backgroundWhenAtTopNormal != null)
		{
			this.m_backgroundWhenAtTopNormal.gameObject.SetActive(this.m_isTouchKeyboardDisplayMode && (this.m_backgroundWhenAtTopWild == null || !this.m_wildModeActive));
		}
		if (this.m_backgroundWhenAtTopWild != null)
		{
			this.m_backgroundWhenAtTopWild.gameObject.SetActive(this.m_isTouchKeyboardDisplayMode && this.m_wildModeActive);
		}
	}

	// Token: 0x04000BBA RID: 3002
	public UberText m_searchText;

	// Token: 0x04000BBB RID: 3003
	public PegUIElement m_background;

	// Token: 0x04000BBC RID: 3004
	public PegUIElement m_clearButton;

	// Token: 0x04000BBD RID: 3005
	public GameObject m_xMesh;

	// Token: 0x04000BBE RID: 3006
	public GameObject m_backgroundWhenAtBottom;

	// Token: 0x04000BBF RID: 3007
	public GameObject m_backgroundWhenAtBottomTavernBrawl;

	// Token: 0x04000BC0 RID: 3008
	public GameObject m_backgroundWhenAtTopNormal;

	// Token: 0x04000BC1 RID: 3009
	public GameObject m_backgroundWhenAtTopWild;

	// Token: 0x04000BC2 RID: 3010
	public Color m_altSearchColor;

	// Token: 0x04000BC3 RID: 3011
	private const float ANIM_TIME = 0.1f;

	// Token: 0x04000BC4 RID: 3012
	private const int MAX_SEARCH_LENGTH = 31;

	// Token: 0x04000BC5 RID: 3013
	private Material m_origSearchMaterial;

	// Token: 0x04000BC6 RID: 3014
	private Vector3 m_origSearchPos;

	// Token: 0x04000BC7 RID: 3015
	private bool m_isActive;

	// Token: 0x04000BC8 RID: 3016
	private string m_prevText;

	// Token: 0x04000BC9 RID: 3017
	private string m_text;

	// Token: 0x04000BCA RID: 3018
	private bool m_wildModeActive;

	// Token: 0x04000BCB RID: 3019
	private List<CollectionSearch.ActivatedListener> m_activatedListeners = new List<CollectionSearch.ActivatedListener>();

	// Token: 0x04000BCC RID: 3020
	private List<CollectionSearch.DeactivatedListener> m_deactivatedListeners = new List<CollectionSearch.DeactivatedListener>();

	// Token: 0x04000BCD RID: 3021
	private List<CollectionSearch.ClearedListener> m_clearedListeners = new List<CollectionSearch.ClearedListener>();

	// Token: 0x04000BCE RID: 3022
	private GameLayer m_originalLayer;

	// Token: 0x04000BCF RID: 3023
	private GameLayer m_activeLayer;

	// Token: 0x04000BD0 RID: 3024
	private bool m_isTouchKeyboardDisplayMode;

	// Token: 0x0200149E RID: 5278
	// (Invoke) Token: 0x0600DB7A RID: 56186
	public delegate void ActivatedListener();

	// Token: 0x0200149F RID: 5279
	// (Invoke) Token: 0x0600DB7E RID: 56190
	public delegate void DeactivatedListener(string oldSearchText, string newSearchText);

	// Token: 0x020014A0 RID: 5280
	// (Invoke) Token: 0x0600DB82 RID: 56194
	public delegate void ClearedListener(bool updateVisuals);
}
