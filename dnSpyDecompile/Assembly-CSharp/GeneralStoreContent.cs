using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006F7 RID: 1783
public class GeneralStoreContent : MonoBehaviour
{
	// Token: 0x06006374 RID: 25460 RVA: 0x00206AEE File Offset: 0x00204CEE
	public void SetParentStore(GeneralStore parentStore)
	{
		this.m_parentStore = parentStore;
	}

	// Token: 0x06006375 RID: 25461 RVA: 0x00206AF7 File Offset: 0x00204CF7
	public void SetContentActive(bool active)
	{
		this.m_isContentActive = active;
	}

	// Token: 0x06006376 RID: 25462 RVA: 0x00206B00 File Offset: 0x00204D00
	public bool IsContentActive()
	{
		return this.m_isContentActive && (this.m_parentStore == null || !this.m_parentStore.IsCovered());
	}

	// Token: 0x06006377 RID: 25463 RVA: 0x00206B2A File Offset: 0x00204D2A
	public void SetCurrentGoldBundle(NoGTAPPTransactionData bundle)
	{
		if (this.m_currentGoldBundle == bundle)
		{
			return;
		}
		this.m_currentMoneyBundle = null;
		this.m_currentGoldBundle = bundle;
		this.OnBundleChanged(this.m_currentGoldBundle, this.m_currentMoneyBundle);
		this.FireBundleChangedEvent();
	}

	// Token: 0x06006378 RID: 25464 RVA: 0x00206B5C File Offset: 0x00204D5C
	public NoGTAPPTransactionData GetCurrentGoldBundle()
	{
		return this.m_currentGoldBundle;
	}

	// Token: 0x06006379 RID: 25465 RVA: 0x00206B64 File Offset: 0x00204D64
	public void SetCurrentMoneyBundle(Network.Bundle bundle, bool force = false)
	{
		if (!force && this.m_currentMoneyBundle == bundle && bundle != null)
		{
			return;
		}
		this.m_currentGoldBundle = null;
		this.m_currentMoneyBundle = bundle;
		this.OnBundleChanged(this.m_currentGoldBundle, this.m_currentMoneyBundle);
		this.FireBundleChangedEvent();
	}

	// Token: 0x0600637A RID: 25466 RVA: 0x00206B9C File Offset: 0x00204D9C
	public Network.Bundle GetCurrentMoneyBundle()
	{
		return this.m_currentMoneyBundle;
	}

	// Token: 0x0600637B RID: 25467 RVA: 0x00206BA4 File Offset: 0x00204DA4
	public void Refresh()
	{
		this.OnRefresh();
	}

	// Token: 0x0600637C RID: 25468 RVA: 0x00206BAC File Offset: 0x00204DAC
	public bool HasBundleSet()
	{
		return this.m_currentMoneyBundle != null || this.m_currentGoldBundle != null;
	}

	// Token: 0x0600637D RID: 25469 RVA: 0x00206BC1 File Offset: 0x00204DC1
	public void RegisterCurrentBundleChanged(GeneralStoreContent.BundleChanged dlg)
	{
		this.m_bundleChangedListeners.Add(dlg);
	}

	// Token: 0x0600637E RID: 25470 RVA: 0x00206BCF File Offset: 0x00204DCF
	public void UnregisterCurrentBundleChanged(GeneralStoreContent.BundleChanged dlg)
	{
		this.m_bundleChangedListeners.Remove(dlg);
	}

	// Token: 0x0600637F RID: 25471 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool AnimateEntranceStart()
	{
		return true;
	}

	// Token: 0x06006380 RID: 25472 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool AnimateEntranceEnd()
	{
		return true;
	}

	// Token: 0x06006381 RID: 25473 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool AnimateExitStart()
	{
		return true;
	}

	// Token: 0x06006382 RID: 25474 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool AnimateExitEnd()
	{
		return true;
	}

	// Token: 0x06006383 RID: 25475 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void PreStoreFlipIn()
	{
	}

	// Token: 0x06006384 RID: 25476 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void PostStoreFlipIn(bool animatedFlipIn)
	{
	}

	// Token: 0x06006385 RID: 25477 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void PreStoreFlipOut()
	{
	}

	// Token: 0x06006386 RID: 25478 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void PostStoreFlipOut()
	{
	}

	// Token: 0x06006387 RID: 25479 RVA: 0x00206BDE File Offset: 0x00204DDE
	public virtual void TryBuyWithMoney(Network.Bundle bundle, GeneralStoreContent.BuyEvent successBuyCB, GeneralStoreContent.BuyEvent failedBuyCB)
	{
		if (successBuyCB != null)
		{
			successBuyCB();
		}
	}

	// Token: 0x06006388 RID: 25480 RVA: 0x002055F2 File Offset: 0x002037F2
	public virtual void TryBuyWithGold(GeneralStoreContent.BuyEvent successBuyCB = null, GeneralStoreContent.BuyEvent failedBuyCB = null)
	{
		if (successBuyCB != null)
		{
			successBuyCB();
		}
	}

	// Token: 0x06006389 RID: 25481 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void StoreShown(bool isCurrent)
	{
	}

	// Token: 0x0600638A RID: 25482 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void StoreHidden(bool isCurrent)
	{
	}

	// Token: 0x0600638B RID: 25483 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnCoverStateChanged(bool coverActive)
	{
	}

	// Token: 0x0600638C RID: 25484 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public virtual bool IsPurchaseDisabled()
	{
		return false;
	}

	// Token: 0x0600638D RID: 25485 RVA: 0x0019DE03 File Offset: 0x0019C003
	public virtual string GetMoneyDisplayOwnedText()
	{
		return string.Empty;
	}

	// Token: 0x0600638E RID: 25486 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnBundleChanged(NoGTAPPTransactionData goldBundle, Network.Bundle moneyBundle)
	{
	}

	// Token: 0x0600638F RID: 25487 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnRefresh()
	{
	}

	// Token: 0x06006390 RID: 25488 RVA: 0x00206BEC File Offset: 0x00204DEC
	private void FireBundleChangedEvent()
	{
		if (!this.IsContentActive())
		{
			return;
		}
		GeneralStoreContent.BundleChanged[] array = this.m_bundleChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this.m_currentGoldBundle, this.m_currentMoneyBundle);
		}
	}

	// Token: 0x0400527E RID: 21118
	protected GeneralStore m_parentStore;

	// Token: 0x0400527F RID: 21119
	private bool m_isContentActive;

	// Token: 0x04005280 RID: 21120
	private NoGTAPPTransactionData m_currentGoldBundle;

	// Token: 0x04005281 RID: 21121
	private Network.Bundle m_currentMoneyBundle;

	// Token: 0x04005282 RID: 21122
	private List<GeneralStoreContent.BundleChanged> m_bundleChangedListeners = new List<GeneralStoreContent.BundleChanged>();

	// Token: 0x0200226E RID: 8814
	// (Invoke) Token: 0x0601272E RID: 75566
	public delegate void BuyEvent();

	// Token: 0x0200226F RID: 8815
	// (Invoke) Token: 0x06012732 RID: 75570
	public delegate void BundleChanged(NoGTAPPTransactionData newGoldBundle, Network.Bundle newMoneyBundle);
}
