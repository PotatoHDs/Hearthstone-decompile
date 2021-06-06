using System;
using System.Collections.Generic;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000730 RID: 1840
[CustomEditClass]
public class StoreSummary : UIBPopup
{
	// Token: 0x0600673A RID: 26426 RVA: 0x0021A3C8 File Offset: 0x002185C8
	protected override void Awake()
	{
		base.Awake();
		this.m_confirmButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnConfirmPressed));
		this.m_cancelButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCancelPressed));
		if (this.m_offClickCatcher != null)
		{
			this.m_offClickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCancelPressed));
		}
		this.m_infoButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInfoPressed));
		this.m_termsOfSaleButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnTermsOfSalePressed));
		this.m_koreanAgreementCheckBox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ToggleKoreanAgreement));
	}

	// Token: 0x0600673B RID: 26427 RVA: 0x0021A47F File Offset: 0x0021867F
	public void Show(long pmtProductID, int quantity, string paymentMethodName)
	{
		this.SetDetails(pmtProductID, quantity, paymentMethodName);
		this.Show();
	}

	// Token: 0x0600673C RID: 26428 RVA: 0x0021A490 File Offset: 0x00218690
	public bool RegisterConfirmListener(StoreSummary.ConfirmCallback callback, object userData = null)
	{
		StoreSummary.ConfirmListener confirmListener = new StoreSummary.ConfirmListener();
		confirmListener.SetCallback(callback);
		confirmListener.SetUserData(userData);
		if (this.m_confirmListeners.Contains(confirmListener))
		{
			return false;
		}
		this.m_confirmListeners.Add(confirmListener);
		return true;
	}

	// Token: 0x0600673D RID: 26429 RVA: 0x0021A4D0 File Offset: 0x002186D0
	public bool RemoveConfirmListener(StoreSummary.ConfirmCallback callback, object userData = null)
	{
		StoreSummary.ConfirmListener confirmListener = new StoreSummary.ConfirmListener();
		confirmListener.SetCallback(callback);
		confirmListener.SetUserData(userData);
		return this.m_confirmListeners.Remove(confirmListener);
	}

	// Token: 0x0600673E RID: 26430 RVA: 0x0021A500 File Offset: 0x00218700
	public bool RegisterCancelListener(StoreSummary.CancelCallback callback, object userData = null)
	{
		StoreSummary.CancelListener cancelListener = new StoreSummary.CancelListener();
		cancelListener.SetCallback(callback);
		cancelListener.SetUserData(userData);
		if (this.m_cancelListeners.Contains(cancelListener))
		{
			return false;
		}
		this.m_cancelListeners.Add(cancelListener);
		return true;
	}

	// Token: 0x0600673F RID: 26431 RVA: 0x0021A540 File Offset: 0x00218740
	public bool RemoveCancelListener(StoreSummary.CancelCallback callback, object userData = null)
	{
		StoreSummary.CancelListener cancelListener = new StoreSummary.CancelListener();
		cancelListener.SetCallback(callback);
		cancelListener.SetUserData(userData);
		return this.m_cancelListeners.Remove(cancelListener);
	}

	// Token: 0x06006740 RID: 26432 RVA: 0x0021A570 File Offset: 0x00218770
	public bool RegisterInfoListener(StoreSummary.InfoCallback callback, object userData = null)
	{
		StoreSummary.InfoListener infoListener = new StoreSummary.InfoListener();
		infoListener.SetCallback(callback);
		infoListener.SetUserData(userData);
		if (this.m_infoListeners.Contains(infoListener))
		{
			return false;
		}
		this.m_infoListeners.Add(infoListener);
		return true;
	}

	// Token: 0x06006741 RID: 26433 RVA: 0x0021A5B0 File Offset: 0x002187B0
	public bool RemoveInfoListener(StoreSummary.InfoCallback callback, object userData = null)
	{
		StoreSummary.InfoListener infoListener = new StoreSummary.InfoListener();
		infoListener.SetCallback(callback);
		infoListener.SetUserData(userData);
		return this.m_infoListeners.Remove(infoListener);
	}

	// Token: 0x06006742 RID: 26434 RVA: 0x0021A5E0 File Offset: 0x002187E0
	public bool RegisterPaymentAndTOSListener(StoreSummary.PaymentAndTOSCallback callback, object userData = null)
	{
		StoreSummary.PaymentAndTOSListener paymentAndTOSListener = new StoreSummary.PaymentAndTOSListener();
		paymentAndTOSListener.SetCallback(callback);
		paymentAndTOSListener.SetUserData(userData);
		if (this.m_paymentAndTOSListeners.Contains(paymentAndTOSListener))
		{
			return false;
		}
		this.m_paymentAndTOSListeners.Add(paymentAndTOSListener);
		return true;
	}

	// Token: 0x06006743 RID: 26435 RVA: 0x0021A620 File Offset: 0x00218820
	public bool RemovePaymentAndTOSListener(StoreSummary.PaymentAndTOSCallback callback, object userData = null)
	{
		StoreSummary.PaymentAndTOSListener paymentAndTOSListener = new StoreSummary.PaymentAndTOSListener();
		paymentAndTOSListener.SetCallback(callback);
		paymentAndTOSListener.SetUserData(userData);
		return this.m_paymentAndTOSListeners.Remove(paymentAndTOSListener);
	}

	// Token: 0x06006744 RID: 26436 RVA: 0x0021A650 File Offset: 0x00218850
	private void SetDetails(long pmtProductID, int quantity, string paymentMethodName)
	{
		this.m_bundle = StoreManager.Get().GetBundleFromPmtProductId(pmtProductID);
		this.m_quantity = quantity;
		this.m_itemsText.Text = this.GetItemsText();
		this.m_priceText.Text = this.GetPriceText();
		this.m_taxDisclaimerText.Text = StoreManager.Get().GetTaxText();
		this.m_chargeDetailsText.Text = ((paymentMethodName == null) ? string.Empty : GameStrings.Format("GLUE_STORE_SUMMARY_CHARGE_DETAILS", new object[]
		{
			paymentMethodName
		}));
		string text = string.Empty;
		HashSet<ProductType> productsInBundle = StoreManager.Get().GetProductsInBundle(this.m_bundle);
		if (productsInBundle.Contains(ProductType.PRODUCT_TYPE_BOOSTER))
		{
			if (StoreManager.Get().IsProductPrePurchase(this.m_bundle))
			{
				text = GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_PACK_PREORDER");
			}
			else if (StoreManager.Get().IsProductFirstPurchaseBundle(this.m_bundle))
			{
				text = GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_FIRST_PURCHASE_BUNDLE");
			}
			else
			{
				text = GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_EXPERT_PACK");
			}
		}
		else if (productsInBundle.Contains(ProductType.PRODUCT_TYPE_DRAFT))
		{
			text = GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_FORGE_TICKET");
		}
		else if (productsInBundle.Contains(ProductType.PRODUCT_TYPE_NAXX) || productsInBundle.Contains(ProductType.PRODUCT_TYPE_BRM) || productsInBundle.Contains(ProductType.PRODUCT_TYPE_LOE) || productsInBundle.Contains(ProductType.PRODUCT_TYPE_WING))
		{
			if (1 == this.m_bundle.Items.Count)
			{
				text = GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_ADVENTURE_SINGLE");
			}
			else
			{
				text = GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_ADVENTURE_BUNDLE");
			}
		}
		else if (productsInBundle.Contains(ProductType.PRODUCT_TYPE_HERO))
		{
			text = GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_HERO");
		}
		else if (productsInBundle.Contains(ProductType.PRODUCT_TYPE_TAVERN_BRAWL_TICKET))
		{
			text = GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_TAVERN_BRAWL_TICKET");
		}
		this.m_koreanAgreementTermsText.Text = text;
	}

	// Token: 0x06006745 RID: 26437 RVA: 0x0021A7E8 File Offset: 0x002189E8
	private string GetItemsText()
	{
		string productName = StoreManager.Get().GetProductName(this.m_bundle);
		return GameStrings.Format("GLUE_STORE_SUMMARY_ITEM_ORDERED", new object[]
		{
			this.m_quantity,
			productName
		});
	}

	// Token: 0x06006746 RID: 26438 RVA: 0x0021A828 File Offset: 0x00218A28
	private string GetPriceText()
	{
		return StoreManager.Get().FormatCostBundle(this.m_bundle);
	}

	// Token: 0x06006747 RID: 26439 RVA: 0x0021A83C File Offset: 0x00218A3C
	protected void UpdateMidMeshScale(float newSize)
	{
		Vector3 localScale = this.m_midMesh.transform.localScale;
		this.m_midMesh.transform.localScale = new Vector3(localScale.x, localScale.y, newSize);
	}

	// Token: 0x06006748 RID: 26440 RVA: 0x0021A87C File Offset: 0x00218A7C
	public override void Show()
	{
		if (this.m_shown)
		{
			return;
		}
		this.m_infoButton.SetEnabled(true, false);
		this.m_termsOfSaleButton.SetEnabled(true, false);
		this.m_cancelButton.SetEnabled(true, false);
		this.m_confirmButton.SetEnabled(true, false);
		this.m_cancelButton.GetComponent<UIBHighlight>().Reset();
		this.m_confirmButton.GetComponent<UIBHighlight>().Reset();
		bool flag = StoreManager.Get().IsEuropeanCustomer();
		if (!this.m_textInitialized)
		{
			this.m_textInitialized = true;
			this.m_headlineText.Text = GameStrings.Get("GLUE_STORE_SUMMARY_HEADLINE");
			this.m_itemsHeadlineText.Text = GameStrings.Get("GLUE_STORE_SUMMARY_ITEMS_ORDERED_HEADLINE");
			this.m_priceHeadlineText.Text = GameStrings.Get("GLUE_STORE_SUMMARY_PRICE_HEADLINE");
			this.m_infoButton.SetText(GameStrings.Get("GLUE_STORE_INFO_BUTTON_TEXT"));
			string text = GameStrings.Get("GLUE_STORE_TERMS_OF_SALE_BUTTON_TEXT");
			this.m_termsOfSaleButton.SetText(text);
			string text2 = GameStrings.Get("GLUE_STORE_SUMMARY_PAY_NOW_TEXT");
			this.m_confirmButton.SetText(text2);
			this.m_cancelButton.SetText(GameStrings.Get("GLOBAL_CANCEL"));
			string key = flag ? "GLUE_STORE_SUMMARY_TOS_AGREEMENT_EU" : "GLUE_STORE_SUMMARY_TOS_AGREEMENT";
			this.m_termsOfSaleAgreementText.Text = GameStrings.Format(key, new object[]
			{
				text2,
				text
			});
		}
		if (StoreManager.Get().IsKoreanCustomer())
		{
			this.m_bottomSectionRoot.transform.localPosition = this.m_koreanBottomSectionBone.localPosition;
			this.m_koreanRequirementRoot.gameObject.SetActive(true);
			this.m_koreanAgreementCheckBox.SetChecked(false);
			this.m_infoButton.gameObject.SetActive(true);
			this.UpdateMidMeshScale(this.m_koreanBodySize);
			this.EnableConfirmButton(false);
		}
		else
		{
			this.m_koreanRequirementRoot.gameObject.SetActive(false);
			this.m_infoButton.gameObject.SetActive(false);
			this.EnableConfirmButton(true);
		}
		if (flag || StoreManager.Get().IsNorthAmericanCustomer())
		{
			this.m_bottomSectionRoot.transform.localPosition = this.m_termsOfSaleBottomSectionBone.localPosition;
			this.UpdateMidMeshScale(this.m_termsBodySize);
			this.m_termsOfSaleRoot.gameObject.SetActive(true);
			this.m_termsOfSaleButton.gameObject.SetActive(true);
		}
		else
		{
			this.m_termsOfSaleRoot.gameObject.SetActive(false);
			this.m_termsOfSaleButton.gameObject.SetActive(false);
		}
		this.PreRender();
		this.m_shown = true;
		base.DoShowAnimation(null);
	}

	// Token: 0x06006749 RID: 26441 RVA: 0x0021AAEC File Offset: 0x00218CEC
	protected override void Hide(bool animate)
	{
		if (!this.m_shown)
		{
			return;
		}
		this.m_termsOfSaleButton.SetEnabled(false, false);
		this.m_infoButton.SetEnabled(false, false);
		this.m_cancelButton.SetEnabled(false, false);
		this.m_confirmButton.SetEnabled(false, false);
		this.m_shown = false;
		base.DoHideAnimation(!animate, null);
	}

	// Token: 0x0600674A RID: 26442 RVA: 0x0021AB48 File Offset: 0x00218D48
	private void OnConfirmPressed(UIEvent e)
	{
		if (!this.m_confirmButtonEnabled)
		{
			return;
		}
		long pmtProductId = (this.m_bundle == null) ? 0L : ((this.m_bundle.PMTProductID != null) ? this.m_bundle.PMTProductID.Value : 0L);
		TelemetryManager.Client().SendPurchasePayNowClicked(pmtProductId);
		this.Hide(true);
		StoreSummary.ConfirmListener[] array = this.m_confirmListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(this.m_quantity);
		}
	}

	// Token: 0x0600674B RID: 26443 RVA: 0x0021ABD4 File Offset: 0x00218DD4
	private void OnCancelPressed(UIEvent e)
	{
		long pmtProductId = (this.m_bundle == null) ? 0L : ((this.m_bundle.PMTProductID != null) ? this.m_bundle.PMTProductID.Value : 0L);
		TelemetryManager.Client().SendPurchaseCancelClicked(pmtProductId);
		this.Hide(true);
		StoreSummary.CancelListener[] array = this.m_cancelListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	// Token: 0x0600674C RID: 26444 RVA: 0x0021AC50 File Offset: 0x00218E50
	private void OnInfoPressed(UIEvent e)
	{
		this.Hide(true);
		StoreSummary.InfoListener[] array = this.m_infoListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	// Token: 0x0600674D RID: 26445 RVA: 0x0021AC88 File Offset: 0x00218E88
	private void OnTermsOfSalePressed(UIEvent e)
	{
		this.Hide(true);
		StoreSummary.PaymentAndTOSListener[] array = this.m_paymentAndTOSListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	// Token: 0x0600674E RID: 26446 RVA: 0x0021ACC0 File Offset: 0x00218EC0
	private void PreRender()
	{
		this.m_itemsText.UpdateNow(false);
		this.m_priceText.UpdateNow(false);
		this.m_koreanAgreementTermsText.UpdateNow(false);
		if (this.m_staticTextResized)
		{
			return;
		}
		this.m_headlineText.UpdateNow(false);
		this.m_itemsHeadlineText.UpdateNow(false);
		this.m_priceHeadlineText.UpdateNow(false);
		this.m_taxDisclaimerText.UpdateNow(false);
		this.m_koreanAgreementCheckBox.m_uberText.UpdateNow(false);
		this.m_staticTextResized = true;
	}

	// Token: 0x0600674F RID: 26447 RVA: 0x0021AD44 File Offset: 0x00218F44
	private void ToggleKoreanAgreement(UIEvent e)
	{
		bool enabled = this.m_koreanAgreementCheckBox.IsChecked();
		this.EnableConfirmButton(enabled);
	}

	// Token: 0x06006750 RID: 26448 RVA: 0x0021AD64 File Offset: 0x00218F64
	private void EnableConfirmButton(bool enabled)
	{
		this.m_confirmButtonEnabled = enabled;
		Material material = this.m_confirmButtonEnabled ? this.m_enabledConfirmButtonMaterial : this.m_disabledConfirmButtonMaterial;
		MultiSliceElement[] componentsInChildren = this.m_confirmButton.m_RootObject.GetComponentsInChildren<MultiSliceElement>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			foreach (MultiSliceElement.Slice slice in componentsInChildren[i].m_slices)
			{
				MeshRenderer component = slice.m_slice.GetComponent<MeshRenderer>();
				if (component != null)
				{
					component.SetMaterial(material);
				}
			}
		}
		Material material2 = this.m_confirmButtonEnabled ? this.m_enabledConfirmCheckMarkMaterial : this.m_disabledConfirmCheckMarkMaterial;
		this.m_confirmButtonCheckMark.GetComponent<MeshRenderer>().SetMaterial(material2);
		Color textColor = this.m_confirmButtonEnabled ? StoreSummary.ENABLED_CONFIRM_BUTTON_TEXT_COLOR : StoreSummary.DISABLED_CONFIRM_BUTTON_TEXT_COLOR;
		this.m_confirmButton.m_ButtonText.TextColor = textColor;
	}

	// Token: 0x04005500 RID: 21760
	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_confirmButton;

	// Token: 0x04005501 RID: 21761
	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_cancelButton;

	// Token: 0x04005502 RID: 21762
	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_infoButton;

	// Token: 0x04005503 RID: 21763
	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_termsOfSaleButton;

	// Token: 0x04005504 RID: 21764
	[CustomEditField(Sections = "Text")]
	public UberText m_headlineText;

	// Token: 0x04005505 RID: 21765
	[CustomEditField(Sections = "Text")]
	public UberText m_itemsHeadlineText;

	// Token: 0x04005506 RID: 21766
	[CustomEditField(Sections = "Text")]
	public UberText m_itemsText;

	// Token: 0x04005507 RID: 21767
	[CustomEditField(Sections = "Text")]
	public UberText m_priceHeadlineText;

	// Token: 0x04005508 RID: 21768
	[CustomEditField(Sections = "Text")]
	public UberText m_priceText;

	// Token: 0x04005509 RID: 21769
	[CustomEditField(Sections = "Text")]
	public UberText m_taxDisclaimerText;

	// Token: 0x0400550A RID: 21770
	[CustomEditField(Sections = "Text")]
	public UberText m_chargeDetailsText;

	// Token: 0x0400550B RID: 21771
	[CustomEditField(Sections = "Objects")]
	public GameObject m_bottomSectionRoot;

	// Token: 0x0400550C RID: 21772
	[CustomEditField(Sections = "Objects")]
	public GameObject m_confirmButtonCheckMark;

	// Token: 0x0400550D RID: 21773
	[CustomEditField(Sections = "Objects")]
	public GameObject m_midMesh;

	// Token: 0x0400550E RID: 21774
	[CustomEditField(Sections = "Materials")]
	public Material m_enabledConfirmButtonMaterial;

	// Token: 0x0400550F RID: 21775
	[CustomEditField(Sections = "Materials")]
	public Material m_disabledConfirmButtonMaterial;

	// Token: 0x04005510 RID: 21776
	[CustomEditField(Sections = "Materials")]
	public Material m_enabledConfirmCheckMarkMaterial;

	// Token: 0x04005511 RID: 21777
	[CustomEditField(Sections = "Materials")]
	public Material m_disabledConfirmCheckMarkMaterial;

	// Token: 0x04005512 RID: 21778
	[CustomEditField(Sections = "Korean Specific Info")]
	public GameObject m_koreanRequirementRoot;

	// Token: 0x04005513 RID: 21779
	[CustomEditField(Sections = "Korean Specific Info")]
	public Transform m_koreanBottomSectionBone;

	// Token: 0x04005514 RID: 21780
	[CustomEditField(Sections = "Korean Specific Info")]
	public UberText m_koreanAgreementTermsText;

	// Token: 0x04005515 RID: 21781
	[CustomEditField(Sections = "Korean Specific Info")]
	public CheckBox m_koreanAgreementCheckBox;

	// Token: 0x04005516 RID: 21782
	[CustomEditField(Sections = "Korean Specific Info")]
	public float m_koreanBodySize;

	// Token: 0x04005517 RID: 21783
	[CustomEditField(Sections = "Terms of Sale")]
	public GameObject m_termsOfSaleRoot;

	// Token: 0x04005518 RID: 21784
	[CustomEditField(Sections = "Terms of Sale")]
	public Transform m_termsOfSaleBottomSectionBone;

	// Token: 0x04005519 RID: 21785
	[CustomEditField(Sections = "Terms of Sale")]
	public UberText m_termsOfSaleAgreementText;

	// Token: 0x0400551A RID: 21786
	[CustomEditField(Sections = "Terms of Sale")]
	public float m_termsBodySize;

	// Token: 0x0400551B RID: 21787
	[CustomEditField(Sections = "Click Catchers")]
	public PegUIElement m_offClickCatcher;

	// Token: 0x0400551C RID: 21788
	private static readonly Color ENABLED_CONFIRM_BUTTON_TEXT_COLOR = new Color(0.239f, 0.184f, 0.098f);

	// Token: 0x0400551D RID: 21789
	private static readonly Color DISABLED_CONFIRM_BUTTON_TEXT_COLOR = new Color(0.1176f, 0.1176f, 0.1176f);

	// Token: 0x0400551E RID: 21790
	private Network.Bundle m_bundle;

	// Token: 0x0400551F RID: 21791
	private int m_quantity;

	// Token: 0x04005520 RID: 21792
	private bool m_staticTextResized;

	// Token: 0x04005521 RID: 21793
	private bool m_confirmButtonEnabled = true;

	// Token: 0x04005522 RID: 21794
	private bool m_textInitialized;

	// Token: 0x04005523 RID: 21795
	private List<StoreSummary.ConfirmListener> m_confirmListeners = new List<StoreSummary.ConfirmListener>();

	// Token: 0x04005524 RID: 21796
	private List<StoreSummary.CancelListener> m_cancelListeners = new List<StoreSummary.CancelListener>();

	// Token: 0x04005525 RID: 21797
	private List<StoreSummary.InfoListener> m_infoListeners = new List<StoreSummary.InfoListener>();

	// Token: 0x04005526 RID: 21798
	private List<StoreSummary.PaymentAndTOSListener> m_paymentAndTOSListeners = new List<StoreSummary.PaymentAndTOSListener>();

	// Token: 0x020022DF RID: 8927
	// (Invoke) Token: 0x060128E6 RID: 76006
	public delegate void ConfirmCallback(int quantity, object userData);

	// Token: 0x020022E0 RID: 8928
	private class ConfirmListener : EventListener<StoreSummary.ConfirmCallback>
	{
		// Token: 0x060128E9 RID: 76009 RVA: 0x0050EA12 File Offset: 0x0050CC12
		public void Fire(int quantity)
		{
			this.m_callback(quantity, this.m_userData);
		}
	}

	// Token: 0x020022E1 RID: 8929
	// (Invoke) Token: 0x060128EC RID: 76012
	public delegate void CancelCallback(object userData);

	// Token: 0x020022E2 RID: 8930
	private class CancelListener : EventListener<StoreSummary.CancelCallback>
	{
		// Token: 0x060128EF RID: 76015 RVA: 0x0050EA2E File Offset: 0x0050CC2E
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}

	// Token: 0x020022E3 RID: 8931
	// (Invoke) Token: 0x060128F2 RID: 76018
	public delegate void InfoCallback(object userData);

	// Token: 0x020022E4 RID: 8932
	private class InfoListener : EventListener<StoreSummary.InfoCallback>
	{
		// Token: 0x060128F5 RID: 76021 RVA: 0x0050EA49 File Offset: 0x0050CC49
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}

	// Token: 0x020022E5 RID: 8933
	// (Invoke) Token: 0x060128F8 RID: 76024
	public delegate void PaymentAndTOSCallback(object userData);

	// Token: 0x020022E6 RID: 8934
	private class PaymentAndTOSListener : EventListener<StoreSummary.PaymentAndTOSCallback>
	{
		// Token: 0x060128FB RID: 76027 RVA: 0x0050EA64 File Offset: 0x0050CC64
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}
}
