using System;
using System.Collections.Generic;

// Token: 0x02000047 RID: 71
[CustomEditClass]
public class AdventurePurchaseScreen : Store
{
	// Token: 0x060003F1 RID: 1009 RVA: 0x000182B4 File Offset: 0x000164B4
	protected override void Awake()
	{
		base.Awake();
		this.m_buyWithMoneyButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.BuyWithMoney();
		});
		this.m_buyWithGoldButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.BuyWithGold();
		});
		this.m_BuyDungeonButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.SendToStore();
		});
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00018314 File Offset: 0x00016514
	public void AddPurchaseListener(AdventurePurchaseScreen.Purchase dlg, object userdata)
	{
		AdventurePurchaseScreen.PurchaseListener purchaseListener = new AdventurePurchaseScreen.PurchaseListener();
		purchaseListener.SetCallback(dlg);
		purchaseListener.SetUserData(userdata);
		this.m_PurchaseListeners.Add(purchaseListener);
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00018344 File Offset: 0x00016544
	public void RemovePurchaseListener(AdventurePurchaseScreen.Purchase dlg)
	{
		foreach (AdventurePurchaseScreen.PurchaseListener purchaseListener in this.m_PurchaseListeners)
		{
			if (purchaseListener.GetCallback() == dlg)
			{
				this.m_PurchaseListeners.Remove(purchaseListener);
				break;
			}
		}
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x000183B0 File Offset: 0x000165B0
	private void BuyWithMoney()
	{
		bool success = true;
		this.FirePurchaseEvent(success);
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x000183C8 File Offset: 0x000165C8
	private void BuyWithGold()
	{
		bool success = true;
		this.FirePurchaseEvent(success);
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x000183E0 File Offset: 0x000165E0
	private void SendToStore()
	{
		bool success = false;
		this.FirePurchaseEvent(success);
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x000183F8 File Offset: 0x000165F8
	private void FirePurchaseEvent(bool success)
	{
		AdventurePurchaseScreen.PurchaseListener[] array = this.m_PurchaseListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(success);
		}
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00018428 File Offset: 0x00016628
	protected override void ShowImpl(bool isTotallyFake)
	{
		base.FireOpenedEvent();
	}

	// Token: 0x040002B4 RID: 692
	[CustomEditField(Sections = "UI")]
	public PegUIElement m_BuyDungeonButton;

	// Token: 0x040002B5 RID: 693
	private List<AdventurePurchaseScreen.PurchaseListener> m_PurchaseListeners = new List<AdventurePurchaseScreen.PurchaseListener>();

	// Token: 0x0200130F RID: 4879
	// (Invoke) Token: 0x0600D65F RID: 54879
	public delegate void Purchase(bool success, object userdata);

	// Token: 0x02001310 RID: 4880
	public class PurchaseListener : EventListener<AdventurePurchaseScreen.Purchase>
	{
		// Token: 0x0600D662 RID: 54882 RVA: 0x003EA554 File Offset: 0x003E8754
		public void Fire(bool success)
		{
			this.m_callback(success, this.m_userData);
		}
	}
}
