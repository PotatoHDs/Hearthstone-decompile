using System;
using UnityEngine;

// Token: 0x0200072C RID: 1836
public class StoreQuantityPrompt : UIBPopup
{
	// Token: 0x06006711 RID: 26385 RVA: 0x00219808 File Offset: 0x00217A08
	protected override void Awake()
	{
		base.Awake();
		this.m_quantityText.RichText = false;
		this.m_okayButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnSubmitPressed));
		this.m_cancelButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCancelPressed));
		Debug.Log(string.Concat(new object[]
		{
			"show postition = ",
			this.m_showPosition,
			" ",
			this.m_showScale
		}));
	}

	// Token: 0x06006712 RID: 26386 RVA: 0x00219898 File Offset: 0x00217A98
	public bool Show(int maxQuantity, StoreQuantityPrompt.OkayListener delOkay = null, StoreQuantityPrompt.CancelListener delCancel = null)
	{
		if (this.m_shown)
		{
			return false;
		}
		this.m_currentMaxQuantity = maxQuantity;
		this.m_messageText.Text = GameStrings.Format("GLUE_STORE_QUANTITY_MESSAGE", new object[]
		{
			maxQuantity
		});
		this.m_shown = true;
		this.m_currentOkayListener = delOkay;
		this.m_currentCancelListener = delCancel;
		this.m_quantityText.Text = string.Empty;
		base.gameObject.SetActive(true);
		Debug.Log(string.Concat(new object[]
		{
			"show postition2 = ",
			this.m_showPosition,
			" ",
			this.m_showScale
		}));
		base.DoShowAnimation(new UIBPopup.OnAnimationComplete(this.ShowInput));
		return true;
	}

	// Token: 0x06006713 RID: 26387 RVA: 0x0021995A File Offset: 0x00217B5A
	protected override void Hide(bool animate)
	{
		if (!this.m_shown)
		{
			return;
		}
		this.m_shown = false;
		this.HideInput();
		base.DoHideAnimation(!animate, delegate()
		{
			base.gameObject.SetActive(false);
		});
	}

	// Token: 0x06006714 RID: 26388 RVA: 0x00219988 File Offset: 0x00217B88
	private bool GetQuantity(out int quantity)
	{
		quantity = -1;
		if (!int.TryParse(this.m_quantityText.Text, out quantity))
		{
			Debug.LogWarning(string.Format("GeneralStore.OnStoreQuantityOkayPressed: invalid quantity='{0}'", this.m_quantityText.Text));
			return false;
		}
		if (quantity <= 0)
		{
			Log.Store.Print(string.Format("GeneralStore.OnStoreQuantityOkayPressed: quantity {0} must be positive", quantity), Array.Empty<object>());
			return false;
		}
		if (quantity > this.m_currentMaxQuantity)
		{
			Log.Store.Print(string.Format("GeneralStore.OnStoreQuantityOkayPressed: quantity {0} is larger than max allowed quantity ({1})", quantity, this.m_currentMaxQuantity), Array.Empty<object>());
			return false;
		}
		return true;
	}

	// Token: 0x06006715 RID: 26389 RVA: 0x00219A28 File Offset: 0x00217C28
	private void Submit()
	{
		this.Hide(true);
		int quantity = -1;
		if (this.GetQuantity(out quantity))
		{
			this.FireOkayEvent(quantity);
			return;
		}
		this.FireCancelEvent();
	}

	// Token: 0x06006716 RID: 26390 RVA: 0x00219A56 File Offset: 0x00217C56
	public void Cancel()
	{
		this.Hide(true);
		this.FireCancelEvent();
	}

	// Token: 0x06006717 RID: 26391 RVA: 0x00219A65 File Offset: 0x00217C65
	private void OnSubmitPressed(UIEvent e)
	{
		this.Submit();
	}

	// Token: 0x06006718 RID: 26392 RVA: 0x00219A6D File Offset: 0x00217C6D
	private void OnCancelPressed(UIEvent e)
	{
		this.Cancel();
	}

	// Token: 0x06006719 RID: 26393 RVA: 0x00219A75 File Offset: 0x00217C75
	private void FireOkayEvent(int quantity)
	{
		if (this.m_currentOkayListener != null)
		{
			this.m_currentOkayListener(quantity);
		}
		this.m_currentOkayListener = null;
	}

	// Token: 0x0600671A RID: 26394 RVA: 0x00219A92 File Offset: 0x00217C92
	private void FireCancelEvent()
	{
		if (this.m_currentCancelListener != null)
		{
			this.m_currentCancelListener();
		}
		this.m_currentCancelListener = null;
	}

	// Token: 0x0600671B RID: 26395 RVA: 0x00219AB0 File Offset: 0x00217CB0
	private void ShowInput()
	{
		this.m_quantityText.gameObject.SetActive(false);
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		Bounds bounds = this.m_quantityText.GetBounds();
		Rect rect = CameraUtils.CreateGUIViewportRect(camera, bounds.min, bounds.max);
		UniversalInputManager.TextInputParams parms = new UniversalInputManager.TextInputParams
		{
			m_owner = base.gameObject,
			m_number = true,
			m_rect = rect,
			m_updatedCallback = new UniversalInputManager.TextInputUpdatedCallback(this.OnInputUpdated),
			m_completedCallback = new UniversalInputManager.TextInputCompletedCallback(this.OnInputComplete),
			m_canceledCallback = new UniversalInputManager.TextInputCanceledCallback(this.OnInputCanceled),
			m_font = this.m_quantityText.GetLocalizedFont(),
			m_alignment = new TextAnchor?(TextAnchor.MiddleCenter),
			m_maxCharacters = 2,
			m_touchScreenKeyboardHideInput = true,
			m_touchScreenKeyboardType = 0
		};
		UniversalInputManager.Get().UseTextInput(parms, false);
	}

	// Token: 0x0600671C RID: 26396 RVA: 0x00219B92 File Offset: 0x00217D92
	private void HideInput()
	{
		UniversalInputManager.Get().CancelTextInput(base.gameObject, false);
		this.m_quantityText.gameObject.SetActive(true);
	}

	// Token: 0x0600671D RID: 26397 RVA: 0x00211C4A File Offset: 0x0020FE4A
	private void ClearInput()
	{
		UniversalInputManager.Get().SetInputText("", false);
	}

	// Token: 0x0600671E RID: 26398 RVA: 0x00219BB6 File Offset: 0x00217DB6
	private void OnInputUpdated(string input)
	{
		this.m_quantityText.Text = input;
	}

	// Token: 0x0600671F RID: 26399 RVA: 0x00219BC4 File Offset: 0x00217DC4
	private void OnInputComplete(string input)
	{
		this.m_quantityText.Text = input;
		this.Submit();
	}

	// Token: 0x06006720 RID: 26400 RVA: 0x00219BD8 File Offset: 0x00217DD8
	private void OnInputCanceled(bool userRequested, GameObject requester)
	{
		this.m_quantityText.Text = string.Empty;
		this.Cancel();
	}

	// Token: 0x040054D8 RID: 21720
	public UIBButton m_okayButton;

	// Token: 0x040054D9 RID: 21721
	public UIBButton m_cancelButton;

	// Token: 0x040054DA RID: 21722
	public UberText m_messageText;

	// Token: 0x040054DB RID: 21723
	public UberText m_quantityText;

	// Token: 0x040054DC RID: 21724
	private int m_currentMaxQuantity;

	// Token: 0x040054DD RID: 21725
	private StoreQuantityPrompt.OkayListener m_currentOkayListener;

	// Token: 0x040054DE RID: 21726
	private StoreQuantityPrompt.CancelListener m_currentCancelListener;

	// Token: 0x020022D7 RID: 8919
	// (Invoke) Token: 0x060128CA RID: 75978
	public delegate void OkayListener(int quantity);

	// Token: 0x020022D8 RID: 8920
	// (Invoke) Token: 0x060128CE RID: 75982
	public delegate void CancelListener();
}
