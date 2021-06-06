using System;
using UnityEngine;

// Token: 0x020006EF RID: 1775
public class DoneWithBamView : ShopView.IComponent
{
	// Token: 0x170005E7 RID: 1511
	// (get) Token: 0x060062DB RID: 25307 RVA: 0x002034E2 File Offset: 0x002016E2
	public bool IsLoaded
	{
		get
		{
			return this.m_doneWithBam != null;
		}
	}

	// Token: 0x170005E8 RID: 1512
	// (get) Token: 0x060062DC RID: 25308 RVA: 0x002034F0 File Offset: 0x002016F0
	public bool IsShown
	{
		get
		{
			return this.IsLoaded && this.m_doneWithBam.IsShown();
		}
	}

	// Token: 0x1400004F RID: 79
	// (add) Token: 0x060062DD RID: 25309 RVA: 0x00203508 File Offset: 0x00201708
	// (remove) Token: 0x060062DE RID: 25310 RVA: 0x00203540 File Offset: 0x00201740
	public event Action OnComponentReady = delegate()
	{
	};

	// Token: 0x14000050 RID: 80
	// (add) Token: 0x060062DF RID: 25311 RVA: 0x00203578 File Offset: 0x00201778
	// (remove) Token: 0x060062E0 RID: 25312 RVA: 0x002035B0 File Offset: 0x002017B0
	public event Action OnOkay = delegate()
	{
	};

	// Token: 0x060062E1 RID: 25313 RVA: 0x002035E5 File Offset: 0x002017E5
	public void Load(IAssetLoader assetLoader)
	{
		if (this.IsLoaded)
		{
			return;
		}
		assetLoader.InstantiatePrefab(ShopPrefabs.ShopDoneWithBamPrefab, new PrefabCallback<GameObject>(this.OnLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x060062E2 RID: 25314 RVA: 0x00203614 File Offset: 0x00201814
	public void Unload()
	{
		if (!this.IsLoaded)
		{
			return;
		}
		this.m_doneWithBam.RemoveOkayListener(new StoreDoneWithBAM.ButtonPressedListener(this.OnOkayListener));
		UnityEngine.Object.Destroy(this.m_doneWithBam.gameObject);
		this.m_doneWithBam = null;
	}

	// Token: 0x060062E3 RID: 25315 RVA: 0x0020364D File Offset: 0x0020184D
	public void Show()
	{
		if (!this.IsLoaded)
		{
			return;
		}
		this.m_doneWithBam.Show();
	}

	// Token: 0x060062E4 RID: 25316 RVA: 0x00203663 File Offset: 0x00201863
	public void Hide()
	{
		if (!this.IsShown)
		{
			return;
		}
		this.m_doneWithBam.Hide();
	}

	// Token: 0x060062E5 RID: 25317 RVA: 0x0020367C File Offset: 0x0020187C
	private void OnLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("DoneWithBamView.OnLoaded(): go is null!");
			return;
		}
		this.m_doneWithBam = go.GetComponent<StoreDoneWithBAM>();
		if (this.m_doneWithBam == null)
		{
			Debug.LogError("DoneWithBamView.OnLoaded(): go has no StoreDoneWithBAM component");
			return;
		}
		this.m_doneWithBam.Hide();
		this.m_doneWithBam.RegisterOkayListener(new StoreDoneWithBAM.ButtonPressedListener(this.OnOkayListener));
		this.OnComponentReady();
	}

	// Token: 0x060062E6 RID: 25318 RVA: 0x002036EF File Offset: 0x002018EF
	private void OnOkayListener()
	{
		this.OnOkay();
	}

	// Token: 0x0400520A RID: 21002
	private StoreDoneWithBAM m_doneWithBam;
}
