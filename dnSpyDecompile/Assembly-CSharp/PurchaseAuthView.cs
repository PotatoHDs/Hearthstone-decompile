using System;
using UnityEngine;

// Token: 0x02000718 RID: 1816
public class PurchaseAuthView : ShopView.IComponent
{
	// Token: 0x170005FA RID: 1530
	// (get) Token: 0x06006534 RID: 25908 RVA: 0x0020FE8A File Offset: 0x0020E08A
	public bool IsLoaded
	{
		get
		{
			return this.m_purchaseAuth != null;
		}
	}

	// Token: 0x170005FB RID: 1531
	// (get) Token: 0x06006535 RID: 25909 RVA: 0x0020FE98 File Offset: 0x0020E098
	public bool IsShown
	{
		get
		{
			return this.IsLoaded && this.m_purchaseAuth.IsShown();
		}
	}

	// Token: 0x14000058 RID: 88
	// (add) Token: 0x06006536 RID: 25910 RVA: 0x0020FEB0 File Offset: 0x0020E0B0
	// (remove) Token: 0x06006537 RID: 25911 RVA: 0x0020FEE8 File Offset: 0x0020E0E8
	public event Action OnComponentReady = delegate()
	{
	};

	// Token: 0x14000059 RID: 89
	// (add) Token: 0x06006538 RID: 25912 RVA: 0x0020FF20 File Offset: 0x0020E120
	// (remove) Token: 0x06006539 RID: 25913 RVA: 0x0020FF58 File Offset: 0x0020E158
	public event Action<bool, MoneyOrGTAPPTransaction> OnPurchaseResultAcknowledged = delegate(bool success, MoneyOrGTAPPTransaction transaction)
	{
	};

	// Token: 0x1400005A RID: 90
	// (add) Token: 0x0600653A RID: 25914 RVA: 0x0020FF90 File Offset: 0x0020E190
	// (remove) Token: 0x0600653B RID: 25915 RVA: 0x0020FFC8 File Offset: 0x0020E1C8
	public event Action OnAuthExit = delegate()
	{
	};

	// Token: 0x0600653C RID: 25916 RVA: 0x0020FFFD File Offset: 0x0020E1FD
	public void Load(IAssetLoader assetLoader)
	{
		if (this.IsLoaded)
		{
			return;
		}
		assetLoader.InstantiatePrefab(ShopPrefabs.ShopPurchaseAuthPrefab, new PrefabCallback<GameObject>(this.OnLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x0600653D RID: 25917 RVA: 0x0021002C File Offset: 0x0020E22C
	public void Unload()
	{
		if (!this.IsLoaded)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_purchaseAuth.gameObject);
		this.m_purchaseAuth = null;
	}

	// Token: 0x0600653E RID: 25918 RVA: 0x0021004E File Offset: 0x0020E24E
	public void Show(MoneyOrGTAPPTransaction transaction, bool enableBack, bool isZeroCostLicense)
	{
		if (!this.IsLoaded)
		{
			return;
		}
		this.m_purchaseAuth.Show(transaction, enableBack, isZeroCostLicense);
	}

	// Token: 0x0600653F RID: 25919 RVA: 0x00210067 File Offset: 0x0020E267
	public void Hide()
	{
		if (!this.IsShown)
		{
			return;
		}
		this.m_purchaseAuth.Hide();
	}

	// Token: 0x06006540 RID: 25920 RVA: 0x0021007D File Offset: 0x0020E27D
	public void StartNewTransaction(MoneyOrGTAPPTransaction transaction, bool enableBack, bool isZeroCostLicense)
	{
		if (!this.IsLoaded)
		{
			return;
		}
		this.m_purchaseAuth.StartNewTransaction(transaction, enableBack, isZeroCostLicense);
	}

	// Token: 0x06006541 RID: 25921 RVA: 0x00210096 File Offset: 0x0020E296
	public void ShowPreviousPurchaseSuccess(MoneyOrGTAPPTransaction transaction, bool enableBack)
	{
		if (!this.IsLoaded)
		{
			return;
		}
		this.m_purchaseAuth.ShowPreviousPurchaseSuccess(transaction, enableBack);
	}

	// Token: 0x06006542 RID: 25922 RVA: 0x002100AE File Offset: 0x0020E2AE
	public void ShowPreviousPurchaseFailure(MoneyOrGTAPPTransaction transaction, string details, bool enableBack, Network.PurchaseErrorInfo.ErrorType error)
	{
		if (!this.IsLoaded)
		{
			return;
		}
		this.m_purchaseAuth.ShowPurchaseMethodFailure(transaction, details, enableBack, error);
	}

	// Token: 0x06006543 RID: 25923 RVA: 0x002100C9 File Offset: 0x0020E2C9
	public bool CompletePurchaseSuccess(MoneyOrGTAPPTransaction transaction)
	{
		return this.IsLoaded && this.m_purchaseAuth.CompletePurchaseSuccess(transaction);
	}

	// Token: 0x06006544 RID: 25924 RVA: 0x002100E1 File Offset: 0x0020E2E1
	public bool CompletePurchaseFailure(MoneyOrGTAPPTransaction transaction, string details, Network.PurchaseErrorInfo.ErrorType error)
	{
		return this.IsLoaded && this.m_purchaseAuth.CompletePurchaseFailure(transaction, details, error);
	}

	// Token: 0x06006545 RID: 25925 RVA: 0x002100FC File Offset: 0x0020E2FC
	private void OnLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("PurchaseAuthView.OnLoaded(): go is null!");
			return;
		}
		this.m_purchaseAuth = go.GetComponent<StorePurchaseAuth>();
		if (this.m_purchaseAuth == null)
		{
			Debug.LogError("PurchaseAuthView.OnLoaded(): go has no StorePurchaseAuth component");
			return;
		}
		this.m_purchaseAuth.Hide();
		this.m_purchaseAuth.RegisterAckPurchaseResultListener(delegate(bool success, MoneyOrGTAPPTransaction transaction)
		{
			this.OnPurchaseResultAcknowledged(success, transaction);
		});
		this.m_purchaseAuth.RegisterExitListener(delegate
		{
			this.OnAuthExit();
		});
		this.OnComponentReady();
	}

	// Token: 0x040053FC RID: 21500
	private StorePurchaseAuth m_purchaseAuth;
}
