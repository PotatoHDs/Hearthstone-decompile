using System.Collections.Generic;
using PegasusUtil;
using UnityEngine;

[CustomEditClass]
public class StoreSummary : UIBPopup
{
	public delegate void ConfirmCallback(int quantity, object userData);

	private class ConfirmListener : EventListener<ConfirmCallback>
	{
		public void Fire(int quantity)
		{
			m_callback(quantity, m_userData);
		}
	}

	public delegate void CancelCallback(object userData);

	private class CancelListener : EventListener<CancelCallback>
	{
		public void Fire()
		{
			m_callback(m_userData);
		}
	}

	public delegate void InfoCallback(object userData);

	private class InfoListener : EventListener<InfoCallback>
	{
		public void Fire()
		{
			m_callback(m_userData);
		}
	}

	public delegate void PaymentAndTOSCallback(object userData);

	private class PaymentAndTOSListener : EventListener<PaymentAndTOSCallback>
	{
		public void Fire()
		{
			m_callback(m_userData);
		}
	}

	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_confirmButton;

	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_cancelButton;

	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_infoButton;

	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_termsOfSaleButton;

	[CustomEditField(Sections = "Text")]
	public UberText m_headlineText;

	[CustomEditField(Sections = "Text")]
	public UberText m_itemsHeadlineText;

	[CustomEditField(Sections = "Text")]
	public UberText m_itemsText;

	[CustomEditField(Sections = "Text")]
	public UberText m_priceHeadlineText;

	[CustomEditField(Sections = "Text")]
	public UberText m_priceText;

	[CustomEditField(Sections = "Text")]
	public UberText m_taxDisclaimerText;

	[CustomEditField(Sections = "Text")]
	public UberText m_chargeDetailsText;

	[CustomEditField(Sections = "Objects")]
	public GameObject m_bottomSectionRoot;

	[CustomEditField(Sections = "Objects")]
	public GameObject m_confirmButtonCheckMark;

	[CustomEditField(Sections = "Objects")]
	public GameObject m_midMesh;

	[CustomEditField(Sections = "Materials")]
	public Material m_enabledConfirmButtonMaterial;

	[CustomEditField(Sections = "Materials")]
	public Material m_disabledConfirmButtonMaterial;

	[CustomEditField(Sections = "Materials")]
	public Material m_enabledConfirmCheckMarkMaterial;

	[CustomEditField(Sections = "Materials")]
	public Material m_disabledConfirmCheckMarkMaterial;

	[CustomEditField(Sections = "Korean Specific Info")]
	public GameObject m_koreanRequirementRoot;

	[CustomEditField(Sections = "Korean Specific Info")]
	public Transform m_koreanBottomSectionBone;

	[CustomEditField(Sections = "Korean Specific Info")]
	public UberText m_koreanAgreementTermsText;

	[CustomEditField(Sections = "Korean Specific Info")]
	public CheckBox m_koreanAgreementCheckBox;

	[CustomEditField(Sections = "Korean Specific Info")]
	public float m_koreanBodySize;

	[CustomEditField(Sections = "Terms of Sale")]
	public GameObject m_termsOfSaleRoot;

	[CustomEditField(Sections = "Terms of Sale")]
	public Transform m_termsOfSaleBottomSectionBone;

	[CustomEditField(Sections = "Terms of Sale")]
	public UberText m_termsOfSaleAgreementText;

	[CustomEditField(Sections = "Terms of Sale")]
	public float m_termsBodySize;

	[CustomEditField(Sections = "Click Catchers")]
	public PegUIElement m_offClickCatcher;

	private static readonly Color ENABLED_CONFIRM_BUTTON_TEXT_COLOR = new Color(0.239f, 0.184f, 0.098f);

	private static readonly Color DISABLED_CONFIRM_BUTTON_TEXT_COLOR = new Color(0.1176f, 0.1176f, 0.1176f);

	private Network.Bundle m_bundle;

	private int m_quantity;

	private bool m_staticTextResized;

	private bool m_confirmButtonEnabled = true;

	private bool m_textInitialized;

	private List<ConfirmListener> m_confirmListeners = new List<ConfirmListener>();

	private List<CancelListener> m_cancelListeners = new List<CancelListener>();

	private List<InfoListener> m_infoListeners = new List<InfoListener>();

	private List<PaymentAndTOSListener> m_paymentAndTOSListeners = new List<PaymentAndTOSListener>();

	protected override void Awake()
	{
		base.Awake();
		m_confirmButton.AddEventListener(UIEventType.RELEASE, OnConfirmPressed);
		m_cancelButton.AddEventListener(UIEventType.RELEASE, OnCancelPressed);
		if (m_offClickCatcher != null)
		{
			m_offClickCatcher.AddEventListener(UIEventType.RELEASE, OnCancelPressed);
		}
		m_infoButton.AddEventListener(UIEventType.RELEASE, OnInfoPressed);
		m_termsOfSaleButton.AddEventListener(UIEventType.RELEASE, OnTermsOfSalePressed);
		m_koreanAgreementCheckBox.AddEventListener(UIEventType.RELEASE, ToggleKoreanAgreement);
	}

	public void Show(long pmtProductID, int quantity, string paymentMethodName)
	{
		SetDetails(pmtProductID, quantity, paymentMethodName);
		Show();
	}

	public bool RegisterConfirmListener(ConfirmCallback callback, object userData = null)
	{
		ConfirmListener confirmListener = new ConfirmListener();
		confirmListener.SetCallback(callback);
		confirmListener.SetUserData(userData);
		if (m_confirmListeners.Contains(confirmListener))
		{
			return false;
		}
		m_confirmListeners.Add(confirmListener);
		return true;
	}

	public bool RemoveConfirmListener(ConfirmCallback callback, object userData = null)
	{
		ConfirmListener confirmListener = new ConfirmListener();
		confirmListener.SetCallback(callback);
		confirmListener.SetUserData(userData);
		return m_confirmListeners.Remove(confirmListener);
	}

	public bool RegisterCancelListener(CancelCallback callback, object userData = null)
	{
		CancelListener cancelListener = new CancelListener();
		cancelListener.SetCallback(callback);
		cancelListener.SetUserData(userData);
		if (m_cancelListeners.Contains(cancelListener))
		{
			return false;
		}
		m_cancelListeners.Add(cancelListener);
		return true;
	}

	public bool RemoveCancelListener(CancelCallback callback, object userData = null)
	{
		CancelListener cancelListener = new CancelListener();
		cancelListener.SetCallback(callback);
		cancelListener.SetUserData(userData);
		return m_cancelListeners.Remove(cancelListener);
	}

	public bool RegisterInfoListener(InfoCallback callback, object userData = null)
	{
		InfoListener infoListener = new InfoListener();
		infoListener.SetCallback(callback);
		infoListener.SetUserData(userData);
		if (m_infoListeners.Contains(infoListener))
		{
			return false;
		}
		m_infoListeners.Add(infoListener);
		return true;
	}

	public bool RemoveInfoListener(InfoCallback callback, object userData = null)
	{
		InfoListener infoListener = new InfoListener();
		infoListener.SetCallback(callback);
		infoListener.SetUserData(userData);
		return m_infoListeners.Remove(infoListener);
	}

	public bool RegisterPaymentAndTOSListener(PaymentAndTOSCallback callback, object userData = null)
	{
		PaymentAndTOSListener paymentAndTOSListener = new PaymentAndTOSListener();
		paymentAndTOSListener.SetCallback(callback);
		paymentAndTOSListener.SetUserData(userData);
		if (m_paymentAndTOSListeners.Contains(paymentAndTOSListener))
		{
			return false;
		}
		m_paymentAndTOSListeners.Add(paymentAndTOSListener);
		return true;
	}

	public bool RemovePaymentAndTOSListener(PaymentAndTOSCallback callback, object userData = null)
	{
		PaymentAndTOSListener paymentAndTOSListener = new PaymentAndTOSListener();
		paymentAndTOSListener.SetCallback(callback);
		paymentAndTOSListener.SetUserData(userData);
		return m_paymentAndTOSListeners.Remove(paymentAndTOSListener);
	}

	private void SetDetails(long pmtProductID, int quantity, string paymentMethodName)
	{
		m_bundle = StoreManager.Get().GetBundleFromPmtProductId(pmtProductID);
		m_quantity = quantity;
		m_itemsText.Text = GetItemsText();
		m_priceText.Text = GetPriceText();
		m_taxDisclaimerText.Text = StoreManager.Get().GetTaxText();
		m_chargeDetailsText.Text = ((paymentMethodName == null) ? string.Empty : GameStrings.Format("GLUE_STORE_SUMMARY_CHARGE_DETAILS", paymentMethodName));
		string text = string.Empty;
		HashSet<ProductType> productsInBundle = StoreManager.Get().GetProductsInBundle(m_bundle);
		if (productsInBundle.Contains(ProductType.PRODUCT_TYPE_BOOSTER))
		{
			text = (StoreManager.Get().IsProductPrePurchase(m_bundle) ? GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_PACK_PREORDER") : ((!StoreManager.Get().IsProductFirstPurchaseBundle(m_bundle)) ? GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_EXPERT_PACK") : GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_FIRST_PURCHASE_BUNDLE")));
		}
		else if (productsInBundle.Contains(ProductType.PRODUCT_TYPE_DRAFT))
		{
			text = GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_FORGE_TICKET");
		}
		else if (productsInBundle.Contains(ProductType.PRODUCT_TYPE_NAXX) || productsInBundle.Contains(ProductType.PRODUCT_TYPE_BRM) || productsInBundle.Contains(ProductType.PRODUCT_TYPE_LOE) || productsInBundle.Contains(ProductType.PRODUCT_TYPE_WING))
		{
			text = ((1 != m_bundle.Items.Count) ? GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_ADVENTURE_BUNDLE") : GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_ADVENTURE_SINGLE"));
		}
		else if (productsInBundle.Contains(ProductType.PRODUCT_TYPE_HERO))
		{
			text = GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_HERO");
		}
		else if (productsInBundle.Contains(ProductType.PRODUCT_TYPE_TAVERN_BRAWL_TICKET))
		{
			text = GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_TAVERN_BRAWL_TICKET");
		}
		m_koreanAgreementTermsText.Text = text;
	}

	private string GetItemsText()
	{
		string productName = StoreManager.Get().GetProductName(m_bundle);
		return GameStrings.Format("GLUE_STORE_SUMMARY_ITEM_ORDERED", m_quantity, productName);
	}

	private string GetPriceText()
	{
		return StoreManager.Get().FormatCostBundle(m_bundle);
	}

	protected void UpdateMidMeshScale(float newSize)
	{
		Vector3 localScale = m_midMesh.transform.localScale;
		m_midMesh.transform.localScale = new Vector3(localScale.x, localScale.y, newSize);
	}

	public override void Show()
	{
		if (!m_shown)
		{
			m_infoButton.SetEnabled(enabled: true);
			m_termsOfSaleButton.SetEnabled(enabled: true);
			m_cancelButton.SetEnabled(enabled: true);
			m_confirmButton.SetEnabled(enabled: true);
			m_cancelButton.GetComponent<UIBHighlight>().Reset();
			m_confirmButton.GetComponent<UIBHighlight>().Reset();
			bool flag = StoreManager.Get().IsEuropeanCustomer();
			if (!m_textInitialized)
			{
				m_textInitialized = true;
				m_headlineText.Text = GameStrings.Get("GLUE_STORE_SUMMARY_HEADLINE");
				m_itemsHeadlineText.Text = GameStrings.Get("GLUE_STORE_SUMMARY_ITEMS_ORDERED_HEADLINE");
				m_priceHeadlineText.Text = GameStrings.Get("GLUE_STORE_SUMMARY_PRICE_HEADLINE");
				m_infoButton.SetText(GameStrings.Get("GLUE_STORE_INFO_BUTTON_TEXT"));
				string text = GameStrings.Get("GLUE_STORE_TERMS_OF_SALE_BUTTON_TEXT");
				m_termsOfSaleButton.SetText(text);
				string text2 = GameStrings.Get("GLUE_STORE_SUMMARY_PAY_NOW_TEXT");
				m_confirmButton.SetText(text2);
				m_cancelButton.SetText(GameStrings.Get("GLOBAL_CANCEL"));
				string key = (flag ? "GLUE_STORE_SUMMARY_TOS_AGREEMENT_EU" : "GLUE_STORE_SUMMARY_TOS_AGREEMENT");
				m_termsOfSaleAgreementText.Text = GameStrings.Format(key, text2, text);
			}
			if (StoreManager.Get().IsKoreanCustomer())
			{
				m_bottomSectionRoot.transform.localPosition = m_koreanBottomSectionBone.localPosition;
				m_koreanRequirementRoot.gameObject.SetActive(value: true);
				m_koreanAgreementCheckBox.SetChecked(isChecked: false);
				m_infoButton.gameObject.SetActive(value: true);
				UpdateMidMeshScale(m_koreanBodySize);
				EnableConfirmButton(enabled: false);
			}
			else
			{
				m_koreanRequirementRoot.gameObject.SetActive(value: false);
				m_infoButton.gameObject.SetActive(value: false);
				EnableConfirmButton(enabled: true);
			}
			if (flag || StoreManager.Get().IsNorthAmericanCustomer())
			{
				m_bottomSectionRoot.transform.localPosition = m_termsOfSaleBottomSectionBone.localPosition;
				UpdateMidMeshScale(m_termsBodySize);
				m_termsOfSaleRoot.gameObject.SetActive(value: true);
				m_termsOfSaleButton.gameObject.SetActive(value: true);
			}
			else
			{
				m_termsOfSaleRoot.gameObject.SetActive(value: false);
				m_termsOfSaleButton.gameObject.SetActive(value: false);
			}
			PreRender();
			m_shown = true;
			DoShowAnimation();
		}
	}

	protected override void Hide(bool animate)
	{
		if (m_shown)
		{
			m_termsOfSaleButton.SetEnabled(enabled: false);
			m_infoButton.SetEnabled(enabled: false);
			m_cancelButton.SetEnabled(enabled: false);
			m_confirmButton.SetEnabled(enabled: false);
			m_shown = false;
			DoHideAnimation(!animate);
		}
	}

	private void OnConfirmPressed(UIEvent e)
	{
		if (m_confirmButtonEnabled)
		{
			long pmtProductId = ((m_bundle == null) ? 0 : (m_bundle.PMTProductID.HasValue ? m_bundle.PMTProductID.Value : 0));
			TelemetryManager.Client().SendPurchasePayNowClicked(pmtProductId);
			Hide(animate: true);
			ConfirmListener[] array = m_confirmListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire(m_quantity);
			}
		}
	}

	private void OnCancelPressed(UIEvent e)
	{
		long pmtProductId = ((m_bundle == null) ? 0 : (m_bundle.PMTProductID.HasValue ? m_bundle.PMTProductID.Value : 0));
		TelemetryManager.Client().SendPurchaseCancelClicked(pmtProductId);
		Hide(animate: true);
		CancelListener[] array = m_cancelListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	private void OnInfoPressed(UIEvent e)
	{
		Hide(animate: true);
		InfoListener[] array = m_infoListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	private void OnTermsOfSalePressed(UIEvent e)
	{
		Hide(animate: true);
		PaymentAndTOSListener[] array = m_paymentAndTOSListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	private void PreRender()
	{
		m_itemsText.UpdateNow();
		m_priceText.UpdateNow();
		m_koreanAgreementTermsText.UpdateNow();
		if (!m_staticTextResized)
		{
			m_headlineText.UpdateNow();
			m_itemsHeadlineText.UpdateNow();
			m_priceHeadlineText.UpdateNow();
			m_taxDisclaimerText.UpdateNow();
			m_koreanAgreementCheckBox.m_uberText.UpdateNow();
			m_staticTextResized = true;
		}
	}

	private void ToggleKoreanAgreement(UIEvent e)
	{
		bool flag = m_koreanAgreementCheckBox.IsChecked();
		EnableConfirmButton(flag);
	}

	private void EnableConfirmButton(bool enabled)
	{
		m_confirmButtonEnabled = enabled;
		Material material = (m_confirmButtonEnabled ? m_enabledConfirmButtonMaterial : m_disabledConfirmButtonMaterial);
		MultiSliceElement[] componentsInChildren = m_confirmButton.m_RootObject.GetComponentsInChildren<MultiSliceElement>();
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
		Material material2 = (m_confirmButtonEnabled ? m_enabledConfirmCheckMarkMaterial : m_disabledConfirmCheckMarkMaterial);
		m_confirmButtonCheckMark.GetComponent<MeshRenderer>().SetMaterial(material2);
		Color textColor = (m_confirmButtonEnabled ? ENABLED_CONFIRM_BUTTON_TEXT_COLOR : DISABLED_CONFIRM_BUTTON_TEXT_COLOR);
		m_confirmButton.m_ButtonText.TextColor = textColor;
	}
}
