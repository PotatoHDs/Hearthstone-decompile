using System;
using UnityEngine;

// Token: 0x02000731 RID: 1841
public class SummaryView : ShopView.IComponent
{
	// Token: 0x17000615 RID: 1557
	// (get) Token: 0x06006753 RID: 26451 RVA: 0x0021AECF File Offset: 0x002190CF
	public bool IsLoaded
	{
		get
		{
			return this.m_summary != null;
		}
	}

	// Token: 0x17000616 RID: 1558
	// (get) Token: 0x06006754 RID: 26452 RVA: 0x0021AEDD File Offset: 0x002190DD
	public bool IsShown
	{
		get
		{
			return this.IsLoaded && this.m_summary.IsShown();
		}
	}

	// Token: 0x1400006C RID: 108
	// (add) Token: 0x06006755 RID: 26453 RVA: 0x0021AEF4 File Offset: 0x002190F4
	// (remove) Token: 0x06006756 RID: 26454 RVA: 0x0021AF2C File Offset: 0x0021912C
	public event Action OnComponentReady = delegate()
	{
	};

	// Token: 0x1400006D RID: 109
	// (add) Token: 0x06006757 RID: 26455 RVA: 0x0021AF64 File Offset: 0x00219164
	// (remove) Token: 0x06006758 RID: 26456 RVA: 0x0021AF9C File Offset: 0x0021919C
	public event Action<int, object> OnSummaryConfirm = delegate(int quantity, object userData)
	{
	};

	// Token: 0x1400006E RID: 110
	// (add) Token: 0x06006759 RID: 26457 RVA: 0x0021AFD4 File Offset: 0x002191D4
	// (remove) Token: 0x0600675A RID: 26458 RVA: 0x0021B00C File Offset: 0x0021920C
	public event Action<object> OnSummaryCancel = delegate(object userData)
	{
	};

	// Token: 0x1400006F RID: 111
	// (add) Token: 0x0600675B RID: 26459 RVA: 0x0021B044 File Offset: 0x00219244
	// (remove) Token: 0x0600675C RID: 26460 RVA: 0x0021B07C File Offset: 0x0021927C
	public event Action<object> OnSummaryInfo = delegate(object userData)
	{
	};

	// Token: 0x14000070 RID: 112
	// (add) Token: 0x0600675D RID: 26461 RVA: 0x0021B0B4 File Offset: 0x002192B4
	// (remove) Token: 0x0600675E RID: 26462 RVA: 0x0021B0EC File Offset: 0x002192EC
	public event Action<object> OnSummaryPaymentAndTos = delegate(object userData)
	{
	};

	// Token: 0x0600675F RID: 26463 RVA: 0x0021B121 File Offset: 0x00219321
	public void Load(IAssetLoader assetLoader)
	{
		if (this.IsLoaded)
		{
			return;
		}
		assetLoader.InstantiatePrefab(ShopPrefabs.ShopSummaryPrefab, new PrefabCallback<GameObject>(this.OnLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x06006760 RID: 26464 RVA: 0x0021B150 File Offset: 0x00219350
	public void Unload()
	{
		if (!this.IsLoaded)
		{
			return;
		}
		this.m_summary.RemoveConfirmListener(new StoreSummary.ConfirmCallback(this.ConfirmListener), null);
		this.m_summary.RemoveCancelListener(new StoreSummary.CancelCallback(this.CancelListener), null);
		this.m_summary.RemoveInfoListener(new StoreSummary.InfoCallback(this.InfoListener), null);
		this.m_summary.RemovePaymentAndTOSListener(new StoreSummary.PaymentAndTOSCallback(this.PaymentAndTosListener), null);
		UnityEngine.Object.Destroy(this.m_summary.gameObject);
		this.m_summary = null;
	}

	// Token: 0x06006761 RID: 26465 RVA: 0x0021B1E1 File Offset: 0x002193E1
	public void Show(long pmtProductID, int quantity, string paymentMethodName)
	{
		if (!this.IsLoaded)
		{
			return;
		}
		this.m_summary.Show(pmtProductID, quantity, paymentMethodName);
	}

	// Token: 0x06006762 RID: 26466 RVA: 0x0021B1FA File Offset: 0x002193FA
	public void Hide()
	{
		if (!this.IsShown)
		{
			return;
		}
		this.m_summary.Hide();
	}

	// Token: 0x06006763 RID: 26467 RVA: 0x0021B210 File Offset: 0x00219410
	private void OnLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("SummaryView.OnLoaded(): go is null!");
			return;
		}
		this.m_summary = go.GetComponent<StoreSummary>();
		if (this.m_summary == null)
		{
			Debug.LogError("SummaryView.OnLoaded(): go has no StoreSummary component");
			return;
		}
		this.m_summary.Hide();
		this.m_summary.RegisterConfirmListener(new StoreSummary.ConfirmCallback(this.ConfirmListener), null);
		this.m_summary.RegisterCancelListener(new StoreSummary.CancelCallback(this.CancelListener), null);
		this.m_summary.RegisterInfoListener(new StoreSummary.InfoCallback(this.InfoListener), null);
		this.m_summary.RegisterPaymentAndTOSListener(new StoreSummary.PaymentAndTOSCallback(this.PaymentAndTosListener), null);
		this.OnComponentReady();
	}

	// Token: 0x06006764 RID: 26468 RVA: 0x0021B2D0 File Offset: 0x002194D0
	private void ConfirmListener(int quantity, object userData)
	{
		this.OnSummaryConfirm(quantity, userData);
	}

	// Token: 0x06006765 RID: 26469 RVA: 0x0021B2DF File Offset: 0x002194DF
	private void CancelListener(object userData)
	{
		this.OnSummaryCancel(userData);
	}

	// Token: 0x06006766 RID: 26470 RVA: 0x0021B2ED File Offset: 0x002194ED
	private void InfoListener(object userData)
	{
		this.OnSummaryInfo(userData);
	}

	// Token: 0x06006767 RID: 26471 RVA: 0x0021B2FB File Offset: 0x002194FB
	private void PaymentAndTosListener(object userData)
	{
		this.OnSummaryPaymentAndTos(userData);
	}

	// Token: 0x04005527 RID: 21799
	private StoreSummary m_summary;
}
