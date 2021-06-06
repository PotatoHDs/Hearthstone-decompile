using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AFA RID: 2810
public abstract class ButtonListMenu : MonoBehaviour
{
	// Token: 0x060095B1 RID: 38321 RVA: 0x00307C80 File Offset: 0x00305E80
	protected virtual void Awake()
	{
		GameObject gameObject = (GameObject)GameUtils.InstantiateGameObject(this.m_menuDefPrefab, null, false);
		this.m_menu = gameObject.GetComponent<ButtonListMenuDef>();
		OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		this.SetTransform();
		GameObject gameObject2 = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(gameObject.layer), "GameMenuInputBlocker", this, gameObject.transform, 10f);
		this.m_blocker = gameObject2.AddComponent<PegUIElement>();
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		this.m_blocker.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBlockerRelease));
	}

	// Token: 0x060095B2 RID: 38322 RVA: 0x00307D23 File Offset: 0x00305F23
	protected virtual void OnDestroy()
	{
		FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x060095B3 RID: 38323 RVA: 0x00307D3C File Offset: 0x00305F3C
	public virtual void Show()
	{
		UniversalInputManager.Get().CancelTextInput(base.gameObject, true);
		this.SetTransform();
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		base.gameObject.SetActive(true);
		UniversalInputManager.Get().SetGameDialogActive(true);
		this.HideAllButtons();
		this.LayoutMenu();
		this.m_isShown = true;
		Bounds textBounds = this.m_menu.m_headerText.GetTextBounds();
		TransformUtil.SetLocalScaleToWorldDimension(this.m_menu.m_headerMiddle, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(textBounds.size.x, 0)
		});
		this.m_menu.m_header.UpdateSlices();
		if (this.m_showAnimation)
		{
			AnimationUtil.ShowWithPunch(this.m_menu.gameObject, ButtonListMenu.HIDDEN_SCALE, this.PUNCH_SCALE * this.NORMAL_SCALE, this.NORMAL_SCALE, null, true, null, null, null);
		}
	}

	// Token: 0x060095B4 RID: 38324 RVA: 0x00307E27 File Offset: 0x00306027
	public virtual void Hide()
	{
		if (base.gameObject != null)
		{
			base.gameObject.SetActive(false);
		}
		UniversalInputManager.Get().SetGameDialogActive(false);
		this.m_isShown = false;
	}

	// Token: 0x060095B5 RID: 38325 RVA: 0x00307E55 File Offset: 0x00306055
	public bool IsShown()
	{
		return this.m_isShown;
	}

	// Token: 0x060095B6 RID: 38326 RVA: 0x00307E5D File Offset: 0x0030605D
	public UIBButton CreateMenuButton(string name, string buttonTextString, UIEvent.Handler releaseHandler)
	{
		return this.CreateMenuButton(name, buttonTextString, releaseHandler, this.m_menu.m_templateButton);
	}

	// Token: 0x060095B7 RID: 38327 RVA: 0x00307E74 File Offset: 0x00306074
	public UIBButton CreateMenuButton(string name, string buttonTextString, UIEvent.Handler releaseHandler, UIBButton buttonTemplate)
	{
		UIBButton uibbutton = (UIBButton)GameUtils.Instantiate(buttonTemplate, this.m_menu.m_buttonContainer.gameObject, false);
		uibbutton.SetText(GameStrings.Get(buttonTextString));
		if (name != null)
		{
			uibbutton.gameObject.name = name;
		}
		uibbutton.AddEventListener(UIEventType.RELEASE, releaseHandler);
		uibbutton.transform.localRotation = buttonTemplate.transform.localRotation;
		this.m_allButtons.Add(uibbutton);
		return uibbutton;
	}

	// Token: 0x060095B8 RID: 38328 RVA: 0x00307EE7 File Offset: 0x003060E7
	public void DisableInputBlocker()
	{
		this.m_blocker.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBlockerRelease));
	}

	// Token: 0x060095B9 RID: 38329
	protected abstract List<UIBButton> GetButtons();

	// Token: 0x060095BA RID: 38330 RVA: 0x00307F04 File Offset: 0x00306104
	protected void SetTransform()
	{
		if (this.m_menuParent == null)
		{
			this.m_menuParent = base.transform;
		}
		TransformUtil.AttachAndPreserveLocalTransform(this.m_menu.transform, this.m_menuParent);
		if (this.m_blocker != null)
		{
			this.m_blocker.transform.localPosition = new Vector3(0f, -5f, 0f);
			this.m_blocker.transform.eulerAngles = new Vector3(90f, 0f, 0f);
		}
		SceneUtils.SetLayer(this, this.m_targetLayer);
		this.m_menu.gameObject.transform.localScale = this.NORMAL_SCALE;
	}

	// Token: 0x060095BB RID: 38331 RVA: 0x00307FBE File Offset: 0x003061BE
	protected virtual void LayoutMenu()
	{
		this.LayoutMenuButtons();
		this.m_menu.m_buttonContainer.UpdateSlices();
		this.LayoutMenuBackground();
	}

	// Token: 0x060095BC RID: 38332 RVA: 0x00307FDC File Offset: 0x003061DC
	protected void LayoutMenuButtons()
	{
		List<UIBButton> buttons = this.GetButtons();
		this.m_menu.m_buttonContainer.ClearSlices();
		int i = 0;
		int num = 0;
		while (i < buttons.Count)
		{
			UIBButton uibbutton = buttons[i];
			Vector3 minLocalPadding = Vector3.zero;
			bool reverse = false;
			GameObject gameObject2;
			if (uibbutton == null)
			{
				GameObject gameObject;
				if (num >= this.m_horizontalDividers.Count)
				{
					gameObject = (GameObject)GameUtils.Instantiate(this.m_menu.m_templateHorizontalDivider, this.m_menu.m_buttonContainer.gameObject, false);
					gameObject.transform.localRotation = this.m_menu.m_templateHorizontalDivider.transform.localRotation;
					this.m_horizontalDividers.Add(gameObject);
				}
				else
				{
					gameObject = this.m_horizontalDividers[num];
				}
				num++;
				gameObject2 = gameObject;
				minLocalPadding = this.m_menu.m_horizontalDividerMinPadding;
			}
			else
			{
				gameObject2 = uibbutton.gameObject;
			}
			this.m_menu.m_buttonContainer.AddSlice(gameObject2, minLocalPadding, Vector3.zero, reverse);
			gameObject2.SetActive(true);
			i++;
		}
	}

	// Token: 0x060095BD RID: 38333 RVA: 0x003080F0 File Offset: 0x003062F0
	protected void LayoutMenuBackground()
	{
		OrientedBounds orientedBounds = TransformUtil.ComputeOrientedWorldBounds(this.m_menu.m_buttonContainer.gameObject, true);
		float width = orientedBounds.Extents[0].magnitude * 2f;
		float height = orientedBounds.Extents[2].magnitude * 2f;
		this.m_menu.m_background.SetSize(width, height);
		this.m_menu.m_border.SetSize(width, height);
	}

	// Token: 0x060095BE RID: 38334 RVA: 0x00308168 File Offset: 0x00306368
	protected static void MakeButtonRed(UIBButton button, Material materialOverride)
	{
		MultiSliceElement component = button.m_RootObject.GetComponent<MultiSliceElement>();
		if (component == null)
		{
			Error.AddDevFatal("ButtonListMenu.MakeButtonRed() - Attempting to make button red, but the button does not have a multi slice element component!", Array.Empty<object>());
			return;
		}
		foreach (MultiSliceElement.Slice slice in component.m_slices)
		{
			GameObject slice2 = slice.m_slice;
			if (slice2 != null)
			{
				slice2.GetComponent<Renderer>().SetMaterial(materialOverride);
			}
		}
		if (button.m_ButtonText != null)
		{
			button.m_ButtonText.TextColor = Color.white;
		}
	}

	// Token: 0x060095BF RID: 38335 RVA: 0x00308214 File Offset: 0x00306414
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		if (SceneMgr.Get().GetNextMode() == SceneMgr.Mode.FATAL_ERROR)
		{
			this.Hide();
		}
	}

	// Token: 0x060095C0 RID: 38336 RVA: 0x0030822C File Offset: 0x0030642C
	private void HideAllButtons()
	{
		for (int i = 0; i < this.m_allButtons.Count; i++)
		{
			this.m_allButtons[i].gameObject.SetActive(false);
		}
		for (int j = 0; j < this.m_horizontalDividers.Count; j++)
		{
			this.m_horizontalDividers[j].SetActive(false);
		}
	}

	// Token: 0x060095C1 RID: 38337 RVA: 0x0030828E File Offset: 0x0030648E
	private void OnBlockerRelease(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		this.Hide();
	}

	// Token: 0x04007D6D RID: 32109
	protected ButtonListMenuDef m_menu;

	// Token: 0x04007D6E RID: 32110
	protected GameLayer m_targetLayer = GameLayer.UI;

	// Token: 0x04007D6F RID: 32111
	private bool m_isShown;

	// Token: 0x04007D70 RID: 32112
	private List<UIBButton> m_allButtons = new List<UIBButton>();

	// Token: 0x04007D71 RID: 32113
	private List<GameObject> m_horizontalDividers = new List<GameObject>();

	// Token: 0x04007D72 RID: 32114
	protected PegUIElement m_blocker;

	// Token: 0x04007D73 RID: 32115
	protected Transform m_menuParent;

	// Token: 0x04007D74 RID: 32116
	protected float PUNCH_SCALE = 1.08f;

	// Token: 0x04007D75 RID: 32117
	protected Vector3 NORMAL_SCALE = Vector3.one;

	// Token: 0x04007D76 RID: 32118
	protected static readonly Vector3 HIDDEN_SCALE = 0.01f * Vector3.one;

	// Token: 0x04007D77 RID: 32119
	protected string m_menuDefPrefab = "ButtonListMenuDef.prefab:1ab57b5c429373a4b8b4e0c0c706ca3e";

	// Token: 0x04007D78 RID: 32120
	protected bool m_showAnimation = true;
}
