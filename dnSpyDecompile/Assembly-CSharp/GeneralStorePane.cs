using System;
using UnityEngine;

// Token: 0x02000703 RID: 1795
[CustomEditClass]
public class GeneralStorePane : MonoBehaviour
{
	// Token: 0x06006493 RID: 25747 RVA: 0x0020E33A File Offset: 0x0020C53A
	public void Refresh()
	{
		this.OnRefresh();
	}

	// Token: 0x06006494 RID: 25748 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool AnimateEntranceStart()
	{
		return true;
	}

	// Token: 0x06006495 RID: 25749 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool AnimateEntranceEnd()
	{
		return true;
	}

	// Token: 0x06006496 RID: 25750 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool AnimateExitStart()
	{
		return true;
	}

	// Token: 0x06006497 RID: 25751 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool AnimateExitEnd()
	{
		return true;
	}

	// Token: 0x06006498 RID: 25752 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void PrePaneSwappedIn()
	{
	}

	// Token: 0x06006499 RID: 25753 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void PostPaneSwappedIn()
	{
	}

	// Token: 0x0600649A RID: 25754 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void PrePaneSwappedOut()
	{
	}

	// Token: 0x0600649B RID: 25755 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void PostPaneSwappedOut()
	{
	}

	// Token: 0x0600649C RID: 25756 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnPurchaseFinished()
	{
	}

	// Token: 0x0600649D RID: 25757 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void StoreShown(bool isCurrent)
	{
	}

	// Token: 0x0600649E RID: 25758 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void StoreHidden(bool isCurrent)
	{
	}

	// Token: 0x0600649F RID: 25759 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnRefresh()
	{
	}

	// Token: 0x040053A3 RID: 21411
	public GeneralStoreContent m_parentContent;

	// Token: 0x040053A4 RID: 21412
	public GameObject m_paneContainer;
}
