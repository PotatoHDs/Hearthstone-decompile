using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000724 RID: 1828
public class StoreLegalBAMLinks : UIBPopup
{
	// Token: 0x060065DA RID: 26074 RVA: 0x00211E14 File Offset: 0x00210014
	protected override void Awake()
	{
		base.Awake();
		this.m_termsOfSaleButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnTermsOfSalePressed));
		this.m_paymentMethodButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPaymentMethodPressed));
		this.m_offClickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClickCatcherPressed));
	}

	// Token: 0x060065DB RID: 26075 RVA: 0x00211E72 File Offset: 0x00210072
	public override void Show()
	{
		base.Show();
		this.EnableButtons(true);
		bool shown = this.m_shown;
	}

	// Token: 0x060065DC RID: 26076 RVA: 0x00211E88 File Offset: 0x00210088
	public void RegisterSendToBAMListener(StoreLegalBAMLinks.SendToBAMListener listener)
	{
		if (this.m_sendToBAMListeners.Contains(listener))
		{
			return;
		}
		this.m_sendToBAMListeners.Add(listener);
	}

	// Token: 0x060065DD RID: 26077 RVA: 0x00211EA5 File Offset: 0x002100A5
	public void RemoveSendToBAMListener(StoreLegalBAMLinks.SendToBAMListener listener)
	{
		this.m_sendToBAMListeners.Remove(listener);
	}

	// Token: 0x060065DE RID: 26078 RVA: 0x00211EB4 File Offset: 0x002100B4
	public void RegisterCancelListener(StoreLegalBAMLinks.CancelListener listener)
	{
		if (this.m_cancelListeners.Contains(listener))
		{
			return;
		}
		this.m_cancelListeners.Add(listener);
	}

	// Token: 0x060065DF RID: 26079 RVA: 0x00211ED1 File Offset: 0x002100D1
	public void RemoveCancelListener(StoreLegalBAMLinks.CancelListener listener)
	{
		this.m_cancelListeners.Remove(listener);
	}

	// Token: 0x060065E0 RID: 26080 RVA: 0x00211EE0 File Offset: 0x002100E0
	private void OnTermsOfSalePressed(UIEvent e)
	{
		base.StopCoroutine(StoreLegalBAMLinks.SEND_TO_BAM_THEN_HIDE_COROUTINE);
		base.StartCoroutine(StoreLegalBAMLinks.SEND_TO_BAM_THEN_HIDE_COROUTINE, StoreLegalBAMLinks.BAMReason.READ_TERMS_OF_SALE);
	}

	// Token: 0x060065E1 RID: 26081 RVA: 0x00211EFF File Offset: 0x002100FF
	private void OnPaymentMethodPressed(UIEvent e)
	{
		base.StopCoroutine(StoreLegalBAMLinks.SEND_TO_BAM_THEN_HIDE_COROUTINE);
		base.StartCoroutine(StoreLegalBAMLinks.SEND_TO_BAM_THEN_HIDE_COROUTINE, StoreLegalBAMLinks.BAMReason.CHANGE_PAYMENT_METHOD);
	}

	// Token: 0x060065E2 RID: 26082 RVA: 0x00211F20 File Offset: 0x00210120
	private void OnClickCatcherPressed(UIEvent e)
	{
		this.Hide(true);
		StoreLegalBAMLinks.CancelListener[] array = this.m_cancelListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x060065E3 RID: 26083 RVA: 0x00211F56 File Offset: 0x00210156
	private IEnumerator SendToBAMThenHide(StoreLegalBAMLinks.BAMReason reason)
	{
		string text = null;
		this.EnableButtons(false);
		if (reason != StoreLegalBAMLinks.BAMReason.CHANGE_PAYMENT_METHOD)
		{
			if (reason == StoreLegalBAMLinks.BAMReason.READ_TERMS_OF_SALE)
			{
				text = ExternalUrlService.Get().GetTermsOfSaleLink();
			}
		}
		else
		{
			text = ExternalUrlService.Get().GetAddPaymentLink();
		}
		if (!string.IsNullOrEmpty(text))
		{
			Application.OpenURL(text);
		}
		yield return new WaitForSeconds(2f);
		this.Hide(true);
		StoreLegalBAMLinks.SendToBAMListener[] array = this.m_sendToBAMListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](reason);
		}
		yield break;
	}

	// Token: 0x060065E4 RID: 26084 RVA: 0x00211F6C File Offset: 0x0021016C
	private void EnableButtons(bool enabled)
	{
		this.m_termsOfSaleButton.SetEnabled(enabled, false);
		this.m_paymentMethodButton.SetEnabled(enabled, false);
	}

	// Token: 0x04005474 RID: 21620
	public GameObject m_root;

	// Token: 0x04005475 RID: 21621
	public UIBButton m_paymentMethodButton;

	// Token: 0x04005476 RID: 21622
	public UIBButton m_termsOfSaleButton;

	// Token: 0x04005477 RID: 21623
	public PegUIElement m_offClickCatcher;

	// Token: 0x04005478 RID: 21624
	private static readonly string SEND_TO_BAM_THEN_HIDE_COROUTINE = "SendToBAMThenHide";

	// Token: 0x04005479 RID: 21625
	private List<StoreLegalBAMLinks.SendToBAMListener> m_sendToBAMListeners = new List<StoreLegalBAMLinks.SendToBAMListener>();

	// Token: 0x0400547A RID: 21626
	private List<StoreLegalBAMLinks.CancelListener> m_cancelListeners = new List<StoreLegalBAMLinks.CancelListener>();

	// Token: 0x020022B9 RID: 8889
	public enum BAMReason
	{
		// Token: 0x0400E47B RID: 58491
		CHANGE_PAYMENT_METHOD,
		// Token: 0x0400E47C RID: 58492
		READ_TERMS_OF_SALE
	}

	// Token: 0x020022BA RID: 8890
	// (Invoke) Token: 0x06012856 RID: 75862
	public delegate void SendToBAMListener(StoreLegalBAMLinks.BAMReason urlType);

	// Token: 0x020022BB RID: 8891
	// (Invoke) Token: 0x0601285A RID: 75866
	public delegate void CancelListener();
}
