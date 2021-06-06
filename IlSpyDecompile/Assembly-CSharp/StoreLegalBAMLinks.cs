using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreLegalBAMLinks : UIBPopup
{
	public enum BAMReason
	{
		CHANGE_PAYMENT_METHOD,
		READ_TERMS_OF_SALE
	}

	public delegate void SendToBAMListener(BAMReason urlType);

	public delegate void CancelListener();

	public GameObject m_root;

	public UIBButton m_paymentMethodButton;

	public UIBButton m_termsOfSaleButton;

	public PegUIElement m_offClickCatcher;

	private static readonly string SEND_TO_BAM_THEN_HIDE_COROUTINE = "SendToBAMThenHide";

	private List<SendToBAMListener> m_sendToBAMListeners = new List<SendToBAMListener>();

	private List<CancelListener> m_cancelListeners = new List<CancelListener>();

	protected override void Awake()
	{
		base.Awake();
		m_termsOfSaleButton.AddEventListener(UIEventType.RELEASE, OnTermsOfSalePressed);
		m_paymentMethodButton.AddEventListener(UIEventType.RELEASE, OnPaymentMethodPressed);
		m_offClickCatcher.AddEventListener(UIEventType.RELEASE, OnClickCatcherPressed);
	}

	public override void Show()
	{
		base.Show();
		EnableButtons(enabled: true);
		_ = m_shown;
	}

	public void RegisterSendToBAMListener(SendToBAMListener listener)
	{
		if (!m_sendToBAMListeners.Contains(listener))
		{
			m_sendToBAMListeners.Add(listener);
		}
	}

	public void RemoveSendToBAMListener(SendToBAMListener listener)
	{
		m_sendToBAMListeners.Remove(listener);
	}

	public void RegisterCancelListener(CancelListener listener)
	{
		if (!m_cancelListeners.Contains(listener))
		{
			m_cancelListeners.Add(listener);
		}
	}

	public void RemoveCancelListener(CancelListener listener)
	{
		m_cancelListeners.Remove(listener);
	}

	private void OnTermsOfSalePressed(UIEvent e)
	{
		StopCoroutine(SEND_TO_BAM_THEN_HIDE_COROUTINE);
		StartCoroutine(SEND_TO_BAM_THEN_HIDE_COROUTINE, BAMReason.READ_TERMS_OF_SALE);
	}

	private void OnPaymentMethodPressed(UIEvent e)
	{
		StopCoroutine(SEND_TO_BAM_THEN_HIDE_COROUTINE);
		StartCoroutine(SEND_TO_BAM_THEN_HIDE_COROUTINE, BAMReason.CHANGE_PAYMENT_METHOD);
	}

	private void OnClickCatcherPressed(UIEvent e)
	{
		Hide(animate: true);
		CancelListener[] array = m_cancelListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	private IEnumerator SendToBAMThenHide(BAMReason reason)
	{
		string text = null;
		EnableButtons(enabled: false);
		switch (reason)
		{
		case BAMReason.CHANGE_PAYMENT_METHOD:
			text = ExternalUrlService.Get().GetAddPaymentLink();
			break;
		case BAMReason.READ_TERMS_OF_SALE:
			text = ExternalUrlService.Get().GetTermsOfSaleLink();
			break;
		}
		if (!string.IsNullOrEmpty(text))
		{
			Application.OpenURL(text);
		}
		yield return new WaitForSeconds(2f);
		Hide(animate: true);
		SendToBAMListener[] array = m_sendToBAMListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](reason);
		}
	}

	private void EnableButtons(bool enabled)
	{
		m_termsOfSaleButton.SetEnabled(enabled);
		m_paymentMethodButton.SetEnabled(enabled);
	}
}
