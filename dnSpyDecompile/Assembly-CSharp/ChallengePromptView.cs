using System;
using PegasusUtil;
using UnityEngine;

// Token: 0x020006ED RID: 1773
public class ChallengePromptView : ShopView.IComponent
{
	// Token: 0x170005DE RID: 1502
	// (get) Token: 0x060062B4 RID: 25268 RVA: 0x00202F63 File Offset: 0x00201163
	public bool IsLoaded
	{
		get
		{
			return this.m_challengePrompt != null;
		}
	}

	// Token: 0x170005DF RID: 1503
	// (get) Token: 0x060062B5 RID: 25269 RVA: 0x00202F71 File Offset: 0x00201171
	public bool IsShown
	{
		get
		{
			return this.IsLoaded && this.m_challengePrompt.IsShown();
		}
	}

	// Token: 0x1400004C RID: 76
	// (add) Token: 0x060062B6 RID: 25270 RVA: 0x00202F88 File Offset: 0x00201188
	// (remove) Token: 0x060062B7 RID: 25271 RVA: 0x00202FC0 File Offset: 0x002011C0
	public event Action OnComponentReady = delegate()
	{
	};

	// Token: 0x1400004D RID: 77
	// (add) Token: 0x060062B8 RID: 25272 RVA: 0x00202FF8 File Offset: 0x002011F8
	// (remove) Token: 0x060062B9 RID: 25273 RVA: 0x00203030 File Offset: 0x00201230
	public event Action<string, bool, CancelPurchase.CancelReason?, string> OnComplete = delegate(string challengeId, bool isSuccess, CancelPurchase.CancelReason? reason, string error)
	{
	};

	// Token: 0x1400004E RID: 78
	// (add) Token: 0x060062BA RID: 25274 RVA: 0x00203068 File Offset: 0x00201268
	// (remove) Token: 0x060062BB RID: 25275 RVA: 0x002030A0 File Offset: 0x002012A0
	public event Action<string> OnCancel = delegate(string challengeId)
	{
	};

	// Token: 0x060062BC RID: 25276 RVA: 0x002030D5 File Offset: 0x002012D5
	public void Load(IAssetLoader assetLoader)
	{
		if (this.IsLoaded)
		{
			return;
		}
		assetLoader.InstantiatePrefab(ShopPrefabs.ShopChallengePromptPrefab, new PrefabCallback<GameObject>(this.OnLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x060062BD RID: 25277 RVA: 0x00203104 File Offset: 0x00201304
	public void Unload()
	{
		if (!this.IsLoaded)
		{
			return;
		}
		this.m_challengePrompt.OnChallengeComplete -= this.CompleteListener;
		this.m_challengePrompt.OnCancel -= this.CancelListener;
		UnityEngine.Object.Destroy(this.m_challengePrompt.gameObject);
		this.m_challengePrompt = null;
	}

	// Token: 0x060062BE RID: 25278 RVA: 0x0020315F File Offset: 0x0020135F
	public void StartChallenge(string challengeId)
	{
		if (!this.IsLoaded)
		{
			return;
		}
		this.m_challengePrompt.StartCoroutine(this.m_challengePrompt.Show(challengeId));
	}

	// Token: 0x060062BF RID: 25279 RVA: 0x00203182 File Offset: 0x00201382
	public void Hide()
	{
		if (!this.IsShown)
		{
			return;
		}
		this.m_challengePrompt.Hide();
	}

	// Token: 0x060062C0 RID: 25280 RVA: 0x00203198 File Offset: 0x00201398
	public bool Cancel(Action<string> onCancel)
	{
		string text = this.m_challengePrompt.HideChallenge();
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		onCancel(text);
		return true;
	}

	// Token: 0x060062C1 RID: 25281 RVA: 0x002031C4 File Offset: 0x002013C4
	private void OnLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("ChallengePromptView.OnLoaded(): go is null!");
			return;
		}
		this.m_challengePrompt = go.GetComponent<StoreChallengePrompt>();
		if (this.m_challengePrompt == null)
		{
			Debug.LogError("ChallengePromptView.OnLoaded(): go has no StoreChallengePrompt component");
			return;
		}
		this.m_challengePrompt.Hide();
		this.m_challengePrompt.OnChallengeComplete += this.CompleteListener;
		this.m_challengePrompt.OnCancel += this.CancelListener;
		this.OnComponentReady();
	}

	// Token: 0x060062C2 RID: 25282 RVA: 0x0020324E File Offset: 0x0020144E
	private void CompleteListener(string challengeId, bool isSuccess, CancelPurchase.CancelReason? reason, string error)
	{
		this.OnComplete(challengeId, isSuccess, reason, error);
	}

	// Token: 0x060062C3 RID: 25283 RVA: 0x00203260 File Offset: 0x00201460
	private void CancelListener(string challengeId)
	{
		this.OnCancel(challengeId);
	}

	// Token: 0x040051FE RID: 20990
	private StoreChallengePrompt m_challengePrompt;
}
