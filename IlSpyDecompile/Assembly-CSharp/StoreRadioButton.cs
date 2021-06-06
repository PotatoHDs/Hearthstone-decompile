using UnityEngine;

public class StoreRadioButton : FramedRadioButton
{
	public class Data
	{
		public Network.Bundle m_bundle;

		public NoGTAPPTransactionData m_noGTAPPTransactionData;
	}

	public SaleBanner m_saleBanner;

	public UberText m_cost;

	public GameObject m_bonusFrame;

	public UberText m_bonusText;

	public GameObject m_realMoneyTextRoot;

	public GameObject m_goldRoot;

	public UberText m_goldCostText;

	public UberText m_goldButtonText;

	private static readonly Color NO_SALE_TEXT_COLOR = new Color(0.239f, 0.184f, 0.098f);

	private static readonly Color ON_SALE_TEXT_COLOR = new Color(0.702f, 0.114f, 0.153f);

	private void Awake()
	{
		ActivateSale(active: false);
	}

	public override void Init(FrameType frameType, string text, int buttonID, object userData)
	{
		base.Init(frameType, text, buttonID, userData);
		Data data = userData as Data;
		if (data == null)
		{
			Debug.LogWarning(string.Format("StoreRadioButton.Init(): storeRadioButtonData is null (frameType={0}, text={1}, buttonID={2)", frameType, text, buttonID));
		}
		else if (data.m_bundle != null)
		{
			InitMoneyOption(data.m_bundle);
		}
		else if (data.m_noGTAPPTransactionData != null)
		{
			InitGoldOptionNoGTAPP(data.m_noGTAPPTransactionData);
		}
		else
		{
			Debug.LogWarning(string.Format("StoreRadioButton.Init(): storeRadioButtonData has neither gold price nor bundle data! (frameType={0}, text={1}, buttonID={2)", frameType, text, buttonID));
		}
	}

	public void ActivateSale(bool active)
	{
		m_saleBanner.m_root.SetActive(active);
		m_text.TextColor = (active ? ON_SALE_TEXT_COLOR : NO_SALE_TEXT_COLOR);
	}

	private void InitMoneyOption(Network.Bundle bundle)
	{
		m_goldRoot.SetActive(value: false);
		m_realMoneyTextRoot.SetActive(value: true);
		m_bonusFrame.SetActive(value: false);
		m_cost.Text = string.Format(GameStrings.Get("GLUE_STORE_PRODUCT_PRICE"), StoreManager.Get().FormatCostBundle(bundle));
	}

	private void InitGoldOptionNoGTAPP(NoGTAPPTransactionData noGTAPPTransactionData)
	{
		string text = string.Empty;
		if (StoreManager.Get().GetGoldCostNoGTAPP(noGTAPPTransactionData, out var cost))
		{
			text = cost.ToString();
		}
		m_goldRoot.SetActive(value: true);
		m_realMoneyTextRoot.SetActive(value: false);
		m_goldButtonText.Text = m_text.Text;
		m_goldCostText.Text = text;
	}
}
