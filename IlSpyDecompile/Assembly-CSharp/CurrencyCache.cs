using System;
using Hearthstone.DataModels;

public class CurrencyCache
{
	[Flags]
	private enum CurrencyFlags
	{
		UNINITIALIZED = 0x0,
		REFRESHING = 0x1,
		CACHED = 0x2,
		DIRTY = 0x4
	}

	private const float MIN_SECONDS_BETWEEN_REQUESTS = 8f;

	private const float MAX_SECONDS_BETWEEN_REQUESTS = 64f;

	private uint m_status;

	private int m_requestAttempts;

	private float m_secondsBetweenRequests;

	private DateTime m_lastGetBalanceRequestTime;

	public PriceDataModel priceDataModel { get; private set; }

	public CurrencyType type { get; private set; }

	public event Action<CurrencyBalanceChangedEventArgs> OnBalanceChanged;

	public event Action OnFirstCache;

	public CurrencyCache(CurrencyType type)
	{
		this.type = type;
		priceDataModel = new PriceDataModel();
		priceDataModel.Currency = type;
		priceDataModel.Amount = 0f;
		priceDataModel.DisplayText = string.Empty;
		m_status = 0u;
		m_requestAttempts = 0;
		m_secondsBetweenRequests = 8f;
		m_lastGetBalanceRequestTime = DateTime.MinValue;
	}

	public void UpdateDisplayText()
	{
		UpdateDisplayText(priceDataModel.Amount.ToString());
	}

	public void UpdateDisplayText(string text)
	{
		priceDataModel.DisplayText = text;
	}

	public bool TryRefresh()
	{
		if (!CanRefresh())
		{
			return false;
		}
		string currencyCode = ShopUtils.GetCurrencyCode(type);
		HearthstoneCheckout hearthstoneCheckout = HearthstoneServices.Get<HearthstoneCheckout>();
		if (hearthstoneCheckout == null)
		{
			Log.Store.PrintError("Cannot request virtual currency balance. Checkout service unavailable");
			return false;
		}
		m_requestAttempts++;
		m_status |= 1u;
		m_lastGetBalanceRequestTime = DateTime.UtcNow;
		Log.Store.PrintDebug("Requesting Virtual Currency balance for {0} (attempt #{1})", type, m_requestAttempts);
		if (!hearthstoneCheckout.GetVirtualCurrencyBalance(currencyCode, HandleVirtualCurrencyBalanceCallback))
		{
			Log.Store.PrintError("Failed to send getBalance request");
			return false;
		}
		if (m_requestAttempts > 0)
		{
			m_secondsBetweenRequests *= 2f;
			if (m_secondsBetweenRequests >= 64f)
			{
				m_secondsBetweenRequests = 64f;
				Log.Store.PrintError("Request for virtual currency type {0} is taking a very long time.", type);
			}
		}
		return true;
	}

	public void UpdateBalance(long balance)
	{
		bool num = IsCached();
		m_status = 2u;
		long num2 = (long)priceDataModel.Amount;
		priceDataModel.Amount = balance;
		UpdateDisplayText();
		if (this.OnBalanceChanged != null && num2 != balance)
		{
			this.OnBalanceChanged(new CurrencyBalanceChangedEventArgs(type, num2, balance));
		}
		if (!num && this.OnFirstCache != null)
		{
			this.OnFirstCache();
		}
	}

	public void MarkDirty()
	{
		m_status |= 4u;
	}

	public bool IsDirty()
	{
		return (m_status & 4) != 0;
	}

	public bool IsCached()
	{
		return (m_status & 2) != 0;
	}

	public bool IsRefreshing()
	{
		return (m_status & 1) != 0;
	}

	public bool NeedsRefresh()
	{
		if (IsRefreshableCurrency())
		{
			if (IsCached())
			{
				return IsDirty();
			}
			return true;
		}
		return false;
	}

	private bool IsRefreshableCurrency()
	{
		if (ShopUtils.IsCurrencyVirtual(type))
		{
			return ShopUtils.IsVirtualCurrencyEnabled();
		}
		return false;
	}

	private bool CanRefresh()
	{
		if (HearthstoneCheckout.IsClientCreationInProgress())
		{
			return false;
		}
		if (!IsRefreshableCurrency())
		{
			return false;
		}
		if (!IsRefreshing())
		{
			return true;
		}
		if ((DateTime.UtcNow - m_lastGetBalanceRequestTime).TotalSeconds < (double)m_secondsBetweenRequests)
		{
			return false;
		}
		return true;
	}

	private void HandleVirtualCurrencyBalanceCallback(string currencyCode, float balance)
	{
		Log.Store.PrintDebug("Virtual Currency balance received for {0}: {1}", type, balance);
		m_requestAttempts = 0;
		m_secondsBetweenRequests = 8f;
		UpdateBalance((long)balance);
	}
}
