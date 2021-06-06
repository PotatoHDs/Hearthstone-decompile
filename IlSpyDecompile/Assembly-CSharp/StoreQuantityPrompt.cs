using UnityEngine;

public class StoreQuantityPrompt : UIBPopup
{
	public delegate void OkayListener(int quantity);

	public delegate void CancelListener();

	public UIBButton m_okayButton;

	public UIBButton m_cancelButton;

	public UberText m_messageText;

	public UberText m_quantityText;

	private int m_currentMaxQuantity;

	private OkayListener m_currentOkayListener;

	private CancelListener m_currentCancelListener;

	protected override void Awake()
	{
		base.Awake();
		m_quantityText.RichText = false;
		m_okayButton.AddEventListener(UIEventType.RELEASE, OnSubmitPressed);
		m_cancelButton.AddEventListener(UIEventType.RELEASE, OnCancelPressed);
		Debug.Log(string.Concat("show postition = ", m_showPosition, " ", m_showScale));
	}

	public bool Show(int maxQuantity, OkayListener delOkay = null, CancelListener delCancel = null)
	{
		if (m_shown)
		{
			return false;
		}
		m_currentMaxQuantity = maxQuantity;
		m_messageText.Text = GameStrings.Format("GLUE_STORE_QUANTITY_MESSAGE", maxQuantity);
		m_shown = true;
		m_currentOkayListener = delOkay;
		m_currentCancelListener = delCancel;
		m_quantityText.Text = string.Empty;
		base.gameObject.SetActive(value: true);
		Debug.Log(string.Concat("show postition2 = ", m_showPosition, " ", m_showScale));
		DoShowAnimation(ShowInput);
		return true;
	}

	protected override void Hide(bool animate)
	{
		if (m_shown)
		{
			m_shown = false;
			HideInput();
			DoHideAnimation(!animate, delegate
			{
				base.gameObject.SetActive(value: false);
			});
		}
	}

	private bool GetQuantity(out int quantity)
	{
		quantity = -1;
		if (!int.TryParse(m_quantityText.Text, out quantity))
		{
			Debug.LogWarning($"GeneralStore.OnStoreQuantityOkayPressed: invalid quantity='{m_quantityText.Text}'");
			return false;
		}
		if (quantity <= 0)
		{
			Log.Store.Print($"GeneralStore.OnStoreQuantityOkayPressed: quantity {quantity} must be positive");
			return false;
		}
		if (quantity > m_currentMaxQuantity)
		{
			Log.Store.Print($"GeneralStore.OnStoreQuantityOkayPressed: quantity {quantity} is larger than max allowed quantity ({m_currentMaxQuantity})");
			return false;
		}
		return true;
	}

	private void Submit()
	{
		Hide(animate: true);
		int quantity = -1;
		if (GetQuantity(out quantity))
		{
			FireOkayEvent(quantity);
		}
		else
		{
			FireCancelEvent();
		}
	}

	public void Cancel()
	{
		Hide(animate: true);
		FireCancelEvent();
	}

	private void OnSubmitPressed(UIEvent e)
	{
		Submit();
	}

	private void OnCancelPressed(UIEvent e)
	{
		Cancel();
	}

	private void FireOkayEvent(int quantity)
	{
		if (m_currentOkayListener != null)
		{
			m_currentOkayListener(quantity);
		}
		m_currentOkayListener = null;
	}

	private void FireCancelEvent()
	{
		if (m_currentCancelListener != null)
		{
			m_currentCancelListener();
		}
		m_currentCancelListener = null;
	}

	private void ShowInput()
	{
		m_quantityText.gameObject.SetActive(value: false);
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		Bounds bounds = m_quantityText.GetBounds();
		Rect rect = CameraUtils.CreateGUIViewportRect(camera, bounds.min, bounds.max);
		UniversalInputManager.TextInputParams parms = new UniversalInputManager.TextInputParams
		{
			m_owner = base.gameObject,
			m_number = true,
			m_rect = rect,
			m_updatedCallback = OnInputUpdated,
			m_completedCallback = OnInputComplete,
			m_canceledCallback = OnInputCanceled,
			m_font = m_quantityText.GetLocalizedFont(),
			m_alignment = TextAnchor.MiddleCenter,
			m_maxCharacters = 2,
			m_touchScreenKeyboardHideInput = true,
			m_touchScreenKeyboardType = 0
		};
		UniversalInputManager.Get().UseTextInput(parms);
	}

	private void HideInput()
	{
		UniversalInputManager.Get().CancelTextInput(base.gameObject);
		m_quantityText.gameObject.SetActive(value: true);
	}

	private void ClearInput()
	{
		UniversalInputManager.Get().SetInputText("");
	}

	private void OnInputUpdated(string input)
	{
		m_quantityText.Text = input;
	}

	private void OnInputComplete(string input)
	{
		m_quantityText.Text = input;
		Submit();
	}

	private void OnInputCanceled(bool userRequested, GameObject requester)
	{
		m_quantityText.Text = string.Empty;
		Cancel();
	}
}
