using System;
using System.Collections;
using UnityEngine;

// Token: 0x020008E7 RID: 2279
public class LoginPopupSequencePopup : BasicPopup
{
	// Token: 0x06007E7F RID: 32383 RVA: 0x0028EF6E File Offset: 0x0028D16E
	public void SetInfo(LoginPopupSequencePopup.Info info)
	{
		this.m_info = info;
		if (this.m_info.m_callbackOnHide != null)
		{
			base.AddHideListener(this.m_info.m_callbackOnHide);
		}
	}

	// Token: 0x06007E80 RID: 32384 RVA: 0x0028EF98 File Offset: 0x0028D198
	public void LoadAssetsAndShowWhenReady()
	{
		if (!string.IsNullOrEmpty(this.m_info.m_backgroundMaterialReference))
		{
			this.m_backgroundReady = false;
			AssetLoader.Get().LoadMaterial(this.m_info.m_backgroundMaterialReference, new AssetLoader.ObjectCallback(this.OnBackgroundMaterialLoaded), null, false, false);
		}
		if (this.m_info.m_card != null)
		{
			this.m_cardReady = false;
			DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(this.m_info.m_card.CardId, null);
			AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(fullDef.EntityDef, this.m_info.m_card.PremiumType), new PrefabCallback<GameObject>(this.OnCardActorLoaded), new LoginPopupSequencePopup.CardActorLoadedData
			{
				m_fullDef = fullDef,
				m_premium = this.m_info.m_card.PremiumType
			}, AssetLoadingOptions.IgnorePrefabPosition);
		}
		base.StartCoroutine(this.ShowWhenReady());
	}

	// Token: 0x06007E81 RID: 32385 RVA: 0x0028F08D File Offset: 0x0028D28D
	public override void Show()
	{
		base.Show();
		this.SetUpPopup(this.m_info);
		DialogBase.DoBlur();
	}

	// Token: 0x06007E82 RID: 32386 RVA: 0x00284F39 File Offset: 0x00283139
	public override void Hide()
	{
		base.Hide();
		DialogBase.EndBlur();
	}

	// Token: 0x06007E83 RID: 32387 RVA: 0x0028F0A6 File Offset: 0x0028D2A6
	protected override void Awake()
	{
		base.Awake();
		this.m_cancelButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.Hide();
		});
	}

	// Token: 0x06007E84 RID: 32388 RVA: 0x0028F0C7 File Offset: 0x0028D2C7
	private void OnBackgroundMaterialLoaded(AssetReference assetRef, UnityEngine.Object obj, object callbackData)
	{
		this.m_backgroundMaterial = (Material)obj;
		this.m_backgroundReady = true;
	}

	// Token: 0x06007E85 RID: 32389 RVA: 0x0028F0DC File Offset: 0x0028D2DC
	private void OnCardActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		LoginPopupSequencePopup.CardActorLoadedData cardActorLoadedData = (LoginPopupSequencePopup.CardActorLoadedData)callbackData;
		using (cardActorLoadedData.m_fullDef)
		{
			this.m_cardReady = true;
			this.m_cardActor = go.GetComponent<Actor>();
			this.m_cardActor.SetCardDef(cardActorLoadedData.m_fullDef.DisposableCardDef);
			this.m_cardActor.SetEntityDef(cardActorLoadedData.m_fullDef.EntityDef);
			this.m_cardActor.ContactShadow(true);
			this.m_cardActor.SetPremium(cardActorLoadedData.m_premium);
			this.m_cardActor.UpdateAllComponents();
			GameUtils.SetParent(this.m_cardActor, this.m_cardBone, true);
			SceneUtils.SetLayer(this.m_cardActor, this.m_cardBone.gameObject.layer);
		}
	}

	// Token: 0x06007E86 RID: 32390 RVA: 0x0028F1A8 File Offset: 0x0028D3A8
	private IEnumerator ShowWhenReady()
	{
		while (!this.m_backgroundReady || !this.m_cardReady)
		{
			yield return new WaitForEndOfFrame();
		}
		this.Show();
		yield break;
	}

	// Token: 0x06007E87 RID: 32391 RVA: 0x0028F1B8 File Offset: 0x0028D3B8
	private void SetUpPopup(LoginPopupSequencePopup.Info info)
	{
		if (this.m_headerText != null)
		{
			this.m_headerText.Text = info.m_headerText;
		}
		if (this.m_bodyText != null)
		{
			this.m_bodyText.Text = info.m_bodyText;
		}
		if (this.m_cancelButton != null)
		{
			this.m_cancelButton.SetText(info.m_buttonText);
		}
		if (this.m_backgroundMaterial != null)
		{
			this.m_background.SetMaterial(this.m_backgroundMaterial);
		}
	}

	// Token: 0x0400662D RID: 26157
	public Transform m_cardBone;

	// Token: 0x0400662E RID: 26158
	public Renderer m_background;

	// Token: 0x0400662F RID: 26159
	private LoginPopupSequencePopup.Info m_info;

	// Token: 0x04006630 RID: 26160
	private Material m_backgroundMaterial;

	// Token: 0x04006631 RID: 26161
	private bool m_backgroundReady = true;

	// Token: 0x04006632 RID: 26162
	private Actor m_cardActor;

	// Token: 0x04006633 RID: 26163
	private bool m_cardReady = true;

	// Token: 0x02002592 RID: 9618
	public class Info
	{
		// Token: 0x0400EDF7 RID: 60919
		public string m_prefabAssetReference;

		// Token: 0x0400EDF8 RID: 60920
		public string m_headerText;

		// Token: 0x0400EDF9 RID: 60921
		public string m_bodyText;

		// Token: 0x0400EDFA RID: 60922
		public string m_buttonText;

		// Token: 0x0400EDFB RID: 60923
		public CollectibleCard m_card;

		// Token: 0x0400EDFC RID: 60924
		public AssetReference m_backgroundMaterialReference;

		// Token: 0x0400EDFD RID: 60925
		public DialogBase.HideCallback m_callbackOnHide;
	}

	// Token: 0x02002593 RID: 9619
	private struct CardActorLoadedData
	{
		// Token: 0x0400EDFE RID: 60926
		public DefLoader.DisposableFullDef m_fullDef;

		// Token: 0x0400EDFF RID: 60927
		public TAG_PREMIUM m_premium;
	}
}
