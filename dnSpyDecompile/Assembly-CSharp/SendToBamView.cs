using System;
using UnityEngine;

// Token: 0x02000719 RID: 1817
public class SendToBamView : ShopView.IComponent
{
	// Token: 0x170005FC RID: 1532
	// (get) Token: 0x06006549 RID: 25929 RVA: 0x00210226 File Offset: 0x0020E426
	public bool IsLoaded
	{
		get
		{
			return this.m_sendToBam != null;
		}
	}

	// Token: 0x170005FD RID: 1533
	// (get) Token: 0x0600654A RID: 25930 RVA: 0x00210234 File Offset: 0x0020E434
	public bool IsShown
	{
		get
		{
			return this.IsLoaded && this.m_sendToBam.IsShown();
		}
	}

	// Token: 0x1400005B RID: 91
	// (add) Token: 0x0600654B RID: 25931 RVA: 0x0021024C File Offset: 0x0020E44C
	// (remove) Token: 0x0600654C RID: 25932 RVA: 0x00210284 File Offset: 0x0020E484
	public event Action OnComponentReady = delegate()
	{
	};

	// Token: 0x1400005C RID: 92
	// (add) Token: 0x0600654D RID: 25933 RVA: 0x002102BC File Offset: 0x0020E4BC
	// (remove) Token: 0x0600654E RID: 25934 RVA: 0x002102F4 File Offset: 0x0020E4F4
	public event Action<MoneyOrGTAPPTransaction, StoreSendToBAM.BAMReason> OnOkay = delegate(MoneyOrGTAPPTransaction transaction, StoreSendToBAM.BAMReason reason)
	{
	};

	// Token: 0x1400005D RID: 93
	// (add) Token: 0x0600654F RID: 25935 RVA: 0x0021032C File Offset: 0x0020E52C
	// (remove) Token: 0x06006550 RID: 25936 RVA: 0x00210364 File Offset: 0x0020E564
	public event Action<MoneyOrGTAPPTransaction> OnCancel = delegate(MoneyOrGTAPPTransaction transaction)
	{
	};

	// Token: 0x06006551 RID: 25937 RVA: 0x00210399 File Offset: 0x0020E599
	public void Load(IAssetLoader assetLoader)
	{
		if (this.IsLoaded)
		{
			return;
		}
		assetLoader.InstantiatePrefab(ShopPrefabs.ShopSendToBamPrefab, new PrefabCallback<GameObject>(this.OnLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x06006552 RID: 25938 RVA: 0x002103C8 File Offset: 0x0020E5C8
	public void Unload()
	{
		if (!this.IsLoaded)
		{
			return;
		}
		this.m_sendToBam.RemoveOkayListener(new StoreSendToBAM.DelOKListener(this.OkayListener));
		this.m_sendToBam.RemoveCancelListener(new StoreSendToBAM.DelCancelListener(this.CancelListener));
		UnityEngine.Object.Destroy(this.m_sendToBam.gameObject);
		this.m_sendToBam = null;
	}

	// Token: 0x06006553 RID: 25939 RVA: 0x00210423 File Offset: 0x0020E623
	public void Show(MoneyOrGTAPPTransaction transaction, StoreSendToBAM.BAMReason reason, string errorCode, bool fromPreviousPurchase)
	{
		if (!this.IsLoaded)
		{
			return;
		}
		this.m_sendToBam.Show(transaction, reason, errorCode, fromPreviousPurchase);
	}

	// Token: 0x06006554 RID: 25940 RVA: 0x0021043E File Offset: 0x0020E63E
	public void Hide()
	{
		if (!this.IsShown)
		{
			return;
		}
		this.m_sendToBam.Hide();
	}

	// Token: 0x06006555 RID: 25941 RVA: 0x00210454 File Offset: 0x0020E654
	private void OnLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("SendToBAMView.OnLoaded(): go is null!");
			return;
		}
		this.m_sendToBam = go.GetComponent<StoreSendToBAM>();
		if (this.m_sendToBam == null)
		{
			Debug.LogError("SendToBAMView.OnLoaded(): go has no StoreSendToBAM component");
			return;
		}
		this.m_sendToBam.Hide();
		this.m_sendToBam.RegisterOkayListener(new StoreSendToBAM.DelOKListener(this.OkayListener));
		this.m_sendToBam.RegisterCancelListener(new StoreSendToBAM.DelCancelListener(this.CancelListener));
		this.OnComponentReady();
	}

	// Token: 0x06006556 RID: 25942 RVA: 0x002104DE File Offset: 0x0020E6DE
	private void OkayListener(MoneyOrGTAPPTransaction transaction, StoreSendToBAM.BAMReason reason)
	{
		this.OnOkay(transaction, reason);
	}

	// Token: 0x06006557 RID: 25943 RVA: 0x002104ED File Offset: 0x0020E6ED
	private void CancelListener(MoneyOrGTAPPTransaction transaction)
	{
		this.OnCancel(transaction);
	}

	// Token: 0x04005400 RID: 21504
	private StoreSendToBAM m_sendToBam;
}
