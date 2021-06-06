using System.Collections.Generic;

[CustomEditClass]
public class AdventurePurchaseScreen : Store
{
	public delegate void Purchase(bool success, object userdata);

	public class PurchaseListener : EventListener<Purchase>
	{
		public void Fire(bool success)
		{
			m_callback(success, m_userData);
		}
	}

	[CustomEditField(Sections = "UI")]
	public PegUIElement m_BuyDungeonButton;

	private List<PurchaseListener> m_PurchaseListeners = new List<PurchaseListener>();

	protected override void Awake()
	{
		base.Awake();
		m_buyWithMoneyButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			BuyWithMoney();
		});
		m_buyWithGoldButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			BuyWithGold();
		});
		m_BuyDungeonButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			SendToStore();
		});
	}

	public void AddPurchaseListener(Purchase dlg, object userdata)
	{
		PurchaseListener purchaseListener = new PurchaseListener();
		purchaseListener.SetCallback(dlg);
		purchaseListener.SetUserData(userdata);
		m_PurchaseListeners.Add(purchaseListener);
	}

	public void RemovePurchaseListener(Purchase dlg)
	{
		foreach (PurchaseListener purchaseListener in m_PurchaseListeners)
		{
			if (purchaseListener.GetCallback() == dlg)
			{
				m_PurchaseListeners.Remove(purchaseListener);
				break;
			}
		}
	}

	private void BuyWithMoney()
	{
		bool success = true;
		FirePurchaseEvent(success);
	}

	private void BuyWithGold()
	{
		bool success = true;
		FirePurchaseEvent(success);
	}

	private void SendToStore()
	{
		bool success = false;
		FirePurchaseEvent(success);
	}

	private void FirePurchaseEvent(bool success)
	{
		PurchaseListener[] array = m_PurchaseListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(success);
		}
	}

	protected override void ShowImpl(bool isTotallyFake)
	{
		FireOpenedEvent();
	}
}
