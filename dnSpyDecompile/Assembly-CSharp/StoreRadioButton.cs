using System;
using UnityEngine;

// Token: 0x0200072E RID: 1838
public class StoreRadioButton : FramedRadioButton
{
	// Token: 0x06006724 RID: 26404 RVA: 0x00219BF0 File Offset: 0x00217DF0
	private void Awake()
	{
		this.ActivateSale(false);
	}

	// Token: 0x06006725 RID: 26405 RVA: 0x00219BFC File Offset: 0x00217DFC
	public override void Init(FramedRadioButton.FrameType frameType, string text, int buttonID, object userData)
	{
		base.Init(frameType, text, buttonID, userData);
		StoreRadioButton.Data data = userData as StoreRadioButton.Data;
		if (data == null)
		{
			Debug.LogWarning(string.Format("StoreRadioButton.Init(): storeRadioButtonData is null (frameType={0}, text={1}, buttonID={2)", frameType, text, buttonID));
			return;
		}
		if (data.m_bundle != null)
		{
			this.InitMoneyOption(data.m_bundle);
			return;
		}
		if (data.m_noGTAPPTransactionData != null)
		{
			this.InitGoldOptionNoGTAPP(data.m_noGTAPPTransactionData);
			return;
		}
		Debug.LogWarning(string.Format("StoreRadioButton.Init(): storeRadioButtonData has neither gold price nor bundle data! (frameType={0}, text={1}, buttonID={2)", frameType, text, buttonID));
	}

	// Token: 0x06006726 RID: 26406 RVA: 0x00219C82 File Offset: 0x00217E82
	public void ActivateSale(bool active)
	{
		this.m_saleBanner.m_root.SetActive(active);
		this.m_text.TextColor = (active ? StoreRadioButton.ON_SALE_TEXT_COLOR : StoreRadioButton.NO_SALE_TEXT_COLOR);
	}

	// Token: 0x06006727 RID: 26407 RVA: 0x00219CB0 File Offset: 0x00217EB0
	private void InitMoneyOption(Network.Bundle bundle)
	{
		this.m_goldRoot.SetActive(false);
		this.m_realMoneyTextRoot.SetActive(true);
		this.m_bonusFrame.SetActive(false);
		this.m_cost.Text = string.Format(GameStrings.Get("GLUE_STORE_PRODUCT_PRICE"), StoreManager.Get().FormatCostBundle(bundle));
	}

	// Token: 0x06006728 RID: 26408 RVA: 0x00219D08 File Offset: 0x00217F08
	private void InitGoldOptionNoGTAPP(NoGTAPPTransactionData noGTAPPTransactionData)
	{
		string text = string.Empty;
		long num;
		if (StoreManager.Get().GetGoldCostNoGTAPP(noGTAPPTransactionData, out num))
		{
			text = num.ToString();
		}
		this.m_goldRoot.SetActive(true);
		this.m_realMoneyTextRoot.SetActive(false);
		this.m_goldButtonText.Text = this.m_text.Text;
		this.m_goldCostText.Text = text;
	}

	// Token: 0x040054E1 RID: 21729
	public SaleBanner m_saleBanner;

	// Token: 0x040054E2 RID: 21730
	public UberText m_cost;

	// Token: 0x040054E3 RID: 21731
	public GameObject m_bonusFrame;

	// Token: 0x040054E4 RID: 21732
	public UberText m_bonusText;

	// Token: 0x040054E5 RID: 21733
	public GameObject m_realMoneyTextRoot;

	// Token: 0x040054E6 RID: 21734
	public GameObject m_goldRoot;

	// Token: 0x040054E7 RID: 21735
	public UberText m_goldCostText;

	// Token: 0x040054E8 RID: 21736
	public UberText m_goldButtonText;

	// Token: 0x040054E9 RID: 21737
	private static readonly Color NO_SALE_TEXT_COLOR = new Color(0.239f, 0.184f, 0.098f);

	// Token: 0x040054EA RID: 21738
	private static readonly Color ON_SALE_TEXT_COLOR = new Color(0.702f, 0.114f, 0.153f);

	// Token: 0x020022D9 RID: 8921
	public class Data
	{
		// Token: 0x0400E4FC RID: 58620
		public Network.Bundle m_bundle;

		// Token: 0x0400E4FD RID: 58621
		public NoGTAPPTransactionData m_noGTAPPTransactionData;
	}
}
