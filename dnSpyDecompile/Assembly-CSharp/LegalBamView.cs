using System;
using UnityEngine;

// Token: 0x0200070C RID: 1804
public class LegalBamView : ShopView.IComponent
{
	// Token: 0x170005ED RID: 1517
	// (get) Token: 0x060064D3 RID: 25811 RVA: 0x0020E84F File Offset: 0x0020CA4F
	public bool IsLoaded
	{
		get
		{
			return this.m_legalBam != null;
		}
	}

	// Token: 0x170005EE RID: 1518
	// (get) Token: 0x060064D4 RID: 25812 RVA: 0x0020E85D File Offset: 0x0020CA5D
	public bool IsShown
	{
		get
		{
			return this.IsLoaded && this.m_legalBam.IsShown();
		}
	}

	// Token: 0x14000055 RID: 85
	// (add) Token: 0x060064D5 RID: 25813 RVA: 0x0020E874 File Offset: 0x0020CA74
	// (remove) Token: 0x060064D6 RID: 25814 RVA: 0x0020E8AC File Offset: 0x0020CAAC
	public event Action OnComponentReady = delegate()
	{
	};

	// Token: 0x14000056 RID: 86
	// (add) Token: 0x060064D7 RID: 25815 RVA: 0x0020E8E4 File Offset: 0x0020CAE4
	// (remove) Token: 0x060064D8 RID: 25816 RVA: 0x0020E91C File Offset: 0x0020CB1C
	public event Action<StoreLegalBAMLinks.BAMReason> OnOkay = delegate(StoreLegalBAMLinks.BAMReason reason)
	{
	};

	// Token: 0x14000057 RID: 87
	// (add) Token: 0x060064D9 RID: 25817 RVA: 0x0020E954 File Offset: 0x0020CB54
	// (remove) Token: 0x060064DA RID: 25818 RVA: 0x0020E98C File Offset: 0x0020CB8C
	public event Action OnCancel = delegate()
	{
	};

	// Token: 0x060064DB RID: 25819 RVA: 0x0020E9C1 File Offset: 0x0020CBC1
	public void Load(IAssetLoader assetLoader)
	{
		if (this.IsLoaded)
		{
			return;
		}
		assetLoader.InstantiatePrefab(ShopPrefabs.ShopLegalBamLinksPrefab, new PrefabCallback<GameObject>(this.OnLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x060064DC RID: 25820 RVA: 0x0020E9F0 File Offset: 0x0020CBF0
	public void Unload()
	{
		if (!this.IsLoaded)
		{
			return;
		}
		this.m_legalBam.RemoveSendToBAMListener(new StoreLegalBAMLinks.SendToBAMListener(this.OkayListener));
		this.m_legalBam.RemoveCancelListener(new StoreLegalBAMLinks.CancelListener(this.CancelListener));
		UnityEngine.Object.Destroy(this.m_legalBam.gameObject);
		this.m_legalBam = null;
	}

	// Token: 0x060064DD RID: 25821 RVA: 0x0020EA4B File Offset: 0x0020CC4B
	public void Show()
	{
		if (!this.IsLoaded)
		{
			return;
		}
		this.m_legalBam.Show();
	}

	// Token: 0x060064DE RID: 25822 RVA: 0x0020EA61 File Offset: 0x0020CC61
	public void Hide()
	{
		if (!this.IsShown)
		{
			return;
		}
		this.m_legalBam.Hide();
	}

	// Token: 0x060064DF RID: 25823 RVA: 0x0020EA78 File Offset: 0x0020CC78
	private void OnLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("LegalBamView.OnLoaded(): go is null!");
			return;
		}
		this.m_legalBam = go.GetComponent<StoreLegalBAMLinks>();
		if (this.m_legalBam == null)
		{
			Debug.LogError("LegalBamView.OnLoaded(): go has no StoreLegalBAMLinks component");
			return;
		}
		this.m_legalBam.Hide();
		this.m_legalBam.RegisterSendToBAMListener(new StoreLegalBAMLinks.SendToBAMListener(this.OkayListener));
		this.m_legalBam.RegisterCancelListener(new StoreLegalBAMLinks.CancelListener(this.CancelListener));
		this.OnComponentReady();
	}

	// Token: 0x060064E0 RID: 25824 RVA: 0x0020EB02 File Offset: 0x0020CD02
	private void OkayListener(StoreLegalBAMLinks.BAMReason reason)
	{
		this.OnOkay(reason);
	}

	// Token: 0x060064E1 RID: 25825 RVA: 0x0020EB10 File Offset: 0x0020CD10
	private void CancelListener()
	{
		this.OnCancel();
	}

	// Token: 0x040053CA RID: 21450
	private StoreLegalBAMLinks m_legalBam;
}
