using System;
using Hearthstone.DataModels;

// Token: 0x020006A7 RID: 1703
public class CurrencyCache
{
	// Token: 0x170005A1 RID: 1441
	// (get) Token: 0x06005EEA RID: 24298 RVA: 0x001EE03E File Offset: 0x001EC23E
	// (set) Token: 0x06005EEB RID: 24299 RVA: 0x001EE046 File Offset: 0x001EC246
	public PriceDataModel priceDataModel { get; private set; }

	// Token: 0x170005A2 RID: 1442
	// (get) Token: 0x06005EEC RID: 24300 RVA: 0x001EE04F File Offset: 0x001EC24F
	// (set) Token: 0x06005EED RID: 24301 RVA: 0x001EE057 File Offset: 0x001EC257
	public CurrencyType type { get; private set; }

	// Token: 0x1400003A RID: 58
	// (add) Token: 0x06005EEE RID: 24302 RVA: 0x001EE060 File Offset: 0x001EC260
	// (remove) Token: 0x06005EEF RID: 24303 RVA: 0x001EE098 File Offset: 0x001EC298
	public event Action<CurrencyBalanceChangedEventArgs> OnBalanceChanged;

	// Token: 0x1400003B RID: 59
	// (add) Token: 0x06005EF0 RID: 24304 RVA: 0x001EE0D0 File Offset: 0x001EC2D0
	// (remove) Token: 0x06005EF1 RID: 24305 RVA: 0x001EE108 File Offset: 0x001EC308
	public event Action OnFirstCache;

	// Token: 0x06005EF2 RID: 24306 RVA: 0x001EE140 File Offset: 0x001EC340
	public CurrencyCache(CurrencyType type)
	{
		this.type = type;
		this.priceDataModel = new PriceDataModel();
		this.priceDataModel.Currency = type;
		this.priceDataModel.Amount = 0f;
		this.priceDataModel.DisplayText = string.Empty;
		this.m_status = 0U;
		this.m_requestAttempts = 0;
		this.m_secondsBetweenRequests = 8f;
		this.m_lastGetBalanceRequestTime = DateTime.MinValue;
	}

	// Token: 0x06005EF3 RID: 24307 RVA: 0x001EE1B8 File Offset: 0x001EC3B8
	public void UpdateDisplayText()
	{
		this.UpdateDisplayText(this.priceDataModel.Amount.ToString());
	}

	// Token: 0x06005EF4 RID: 24308 RVA: 0x001EE1DE File Offset: 0x001EC3DE
	public void UpdateDisplayText(string text)
	{
		this.priceDataModel.DisplayText = text;
	}

	// Token: 0x06005EF5 RID: 24309 RVA: 0x001EE1EC File Offset: 0x001EC3EC
	public bool TryRefresh()
	{
		if (!this.CanRefresh())
		{
			return false;
		}
		string currencyCode = ShopUtils.GetCurrencyCode(this.type);
		HearthstoneCheckout hearthstoneCheckout = HearthstoneServices.Get<HearthstoneCheckout>();
		if (hearthstoneCheckout == null)
		{
			Log.Store.PrintError("Cannot request virtual currency balance. Checkout service unavailable", Array.Empty<object>());
			return false;
		}
		this.m_requestAttempts++;
		this.m_status |= 1U;
		this.m_lastGetBalanceRequestTime = DateTime.UtcNow;
		Log.Store.PrintDebug("Requesting Virtual Currency balance for {0} (attempt #{1})", new object[]
		{
			this.type,
			this.m_requestAttempts
		});
		if (!hearthstoneCheckout.GetVirtualCurrencyBalance(currencyCode, new HearthstoneCheckout.VirtualCurrencyBalanceCallback(this.HandleVirtualCurrencyBalanceCallback)))
		{
			Log.Store.PrintError("Failed to send getBalance request", Array.Empty<object>());
			return false;
		}
		if (this.m_requestAttempts > 0)
		{
			this.m_secondsBetweenRequests *= 2f;
			if (this.m_secondsBetweenRequests >= 64f)
			{
				this.m_secondsBetweenRequests = 64f;
				Log.Store.PrintError("Request for virtual currency type {0} is taking a very long time.", new object[]
				{
					this.type
				});
			}
		}
		return true;
	}

	// Token: 0x06005EF6 RID: 24310 RVA: 0x001EE308 File Offset: 0x001EC508
	public void UpdateBalance(long balance)
	{
		bool flag = this.IsCached();
		this.m_status = 2U;
		long num = (long)this.priceDataModel.Amount;
		this.priceDataModel.Amount = (float)balance;
		this.UpdateDisplayText();
		if (this.OnBalanceChanged != null && num != balance)
		{
			this.OnBalanceChanged(new CurrencyBalanceChangedEventArgs(this.type, num, balance));
		}
		if (!flag && this.OnFirstCache != null)
		{
			this.OnFirstCache();
		}
	}

	// Token: 0x06005EF7 RID: 24311 RVA: 0x001EE37B File Offset: 0x001EC57B
	public void MarkDirty()
	{
		this.m_status |= 4U;
	}

	// Token: 0x06005EF8 RID: 24312 RVA: 0x001EE38B File Offset: 0x001EC58B
	public bool IsDirty()
	{
		return (this.m_status & 4U) > 0U;
	}

	// Token: 0x06005EF9 RID: 24313 RVA: 0x001EE398 File Offset: 0x001EC598
	public bool IsCached()
	{
		return (this.m_status & 2U) > 0U;
	}

	// Token: 0x06005EFA RID: 24314 RVA: 0x001EE3A5 File Offset: 0x001EC5A5
	public bool IsRefreshing()
	{
		return (this.m_status & 1U) > 0U;
	}

	// Token: 0x06005EFB RID: 24315 RVA: 0x001EE3B2 File Offset: 0x001EC5B2
	public bool NeedsRefresh()
	{
		return this.IsRefreshableCurrency() && (!this.IsCached() || this.IsDirty());
	}

	// Token: 0x06005EFC RID: 24316 RVA: 0x001EE3CE File Offset: 0x001EC5CE
	private bool IsRefreshableCurrency()
	{
		return ShopUtils.IsCurrencyVirtual(this.type) && ShopUtils.IsVirtualCurrencyEnabled();
	}

	// Token: 0x06005EFD RID: 24317 RVA: 0x001EE3E4 File Offset: 0x001EC5E4
	private bool CanRefresh()
	{
		return !HearthstoneCheckout.IsClientCreationInProgress() && this.IsRefreshableCurrency() && (!this.IsRefreshing() || (DateTime.UtcNow - this.m_lastGetBalanceRequestTime).TotalSeconds >= (double)this.m_secondsBetweenRequests);
	}

	// Token: 0x06005EFE RID: 24318 RVA: 0x001EE434 File Offset: 0x001EC634
	private void HandleVirtualCurrencyBalanceCallback(string currencyCode, float balance)
	{
		Log.Store.PrintDebug("Virtual Currency balance received for {0}: {1}", new object[]
		{
			this.type,
			balance
		});
		this.m_requestAttempts = 0;
		this.m_secondsBetweenRequests = 8f;
		this.UpdateBalance((long)balance);
	}

	// Token: 0x0400500A RID: 20490
	private const float MIN_SECONDS_BETWEEN_REQUESTS = 8f;

	// Token: 0x0400500B RID: 20491
	private const float MAX_SECONDS_BETWEEN_REQUESTS = 64f;

	// Token: 0x0400500C RID: 20492
	private uint m_status;

	// Token: 0x0400500D RID: 20493
	private int m_requestAttempts;

	// Token: 0x0400500E RID: 20494
	private float m_secondsBetweenRequests;

	// Token: 0x0400500F RID: 20495
	private DateTime m_lastGetBalanceRequestTime;

	// Token: 0x020021D1 RID: 8657
	[Flags]
	private enum CurrencyFlags
	{
		// Token: 0x0400E167 RID: 57703
		UNINITIALIZED = 0,
		// Token: 0x0400E168 RID: 57704
		REFRESHING = 1,
		// Token: 0x0400E169 RID: 57705
		CACHED = 2,
		// Token: 0x0400E16A RID: 57706
		DIRTY = 4
	}
}
