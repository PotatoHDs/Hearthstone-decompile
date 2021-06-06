using System.Collections.Generic;
using UnityEngine;

public class GeneralStoreContent : MonoBehaviour
{
	public delegate void BuyEvent();

	public delegate void BundleChanged(NoGTAPPTransactionData newGoldBundle, Network.Bundle newMoneyBundle);

	protected GeneralStore m_parentStore;

	private bool m_isContentActive;

	private NoGTAPPTransactionData m_currentGoldBundle;

	private Network.Bundle m_currentMoneyBundle;

	private List<BundleChanged> m_bundleChangedListeners = new List<BundleChanged>();

	public void SetParentStore(GeneralStore parentStore)
	{
		m_parentStore = parentStore;
	}

	public void SetContentActive(bool active)
	{
		m_isContentActive = active;
	}

	public bool IsContentActive()
	{
		if (m_isContentActive)
		{
			if (!(m_parentStore == null))
			{
				return !m_parentStore.IsCovered();
			}
			return true;
		}
		return false;
	}

	public void SetCurrentGoldBundle(NoGTAPPTransactionData bundle)
	{
		if (m_currentGoldBundle != bundle)
		{
			m_currentMoneyBundle = null;
			m_currentGoldBundle = bundle;
			OnBundleChanged(m_currentGoldBundle, m_currentMoneyBundle);
			FireBundleChangedEvent();
		}
	}

	public NoGTAPPTransactionData GetCurrentGoldBundle()
	{
		return m_currentGoldBundle;
	}

	public void SetCurrentMoneyBundle(Network.Bundle bundle, bool force = false)
	{
		if (force || m_currentMoneyBundle != bundle || bundle == null)
		{
			m_currentGoldBundle = null;
			m_currentMoneyBundle = bundle;
			OnBundleChanged(m_currentGoldBundle, m_currentMoneyBundle);
			FireBundleChangedEvent();
		}
	}

	public Network.Bundle GetCurrentMoneyBundle()
	{
		return m_currentMoneyBundle;
	}

	public void Refresh()
	{
		OnRefresh();
	}

	public bool HasBundleSet()
	{
		if (m_currentMoneyBundle == null)
		{
			return m_currentGoldBundle != null;
		}
		return true;
	}

	public void RegisterCurrentBundleChanged(BundleChanged dlg)
	{
		m_bundleChangedListeners.Add(dlg);
	}

	public void UnregisterCurrentBundleChanged(BundleChanged dlg)
	{
		m_bundleChangedListeners.Remove(dlg);
	}

	public virtual bool AnimateEntranceStart()
	{
		return true;
	}

	public virtual bool AnimateEntranceEnd()
	{
		return true;
	}

	public virtual bool AnimateExitStart()
	{
		return true;
	}

	public virtual bool AnimateExitEnd()
	{
		return true;
	}

	public virtual void PreStoreFlipIn()
	{
	}

	public virtual void PostStoreFlipIn(bool animatedFlipIn)
	{
	}

	public virtual void PreStoreFlipOut()
	{
	}

	public virtual void PostStoreFlipOut()
	{
	}

	public virtual void TryBuyWithMoney(Network.Bundle bundle, BuyEvent successBuyCB, BuyEvent failedBuyCB)
	{
		successBuyCB?.Invoke();
	}

	public virtual void TryBuyWithGold(BuyEvent successBuyCB = null, BuyEvent failedBuyCB = null)
	{
		successBuyCB?.Invoke();
	}

	public virtual void StoreShown(bool isCurrent)
	{
	}

	public virtual void StoreHidden(bool isCurrent)
	{
	}

	public virtual void OnCoverStateChanged(bool coverActive)
	{
	}

	public virtual bool IsPurchaseDisabled()
	{
		return false;
	}

	public virtual string GetMoneyDisplayOwnedText()
	{
		return string.Empty;
	}

	protected virtual void OnBundleChanged(NoGTAPPTransactionData goldBundle, Network.Bundle moneyBundle)
	{
	}

	protected virtual void OnRefresh()
	{
	}

	private void FireBundleChangedEvent()
	{
		if (IsContentActive())
		{
			BundleChanged[] array = m_bundleChangedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i](m_currentGoldBundle, m_currentMoneyBundle);
			}
		}
	}
}
