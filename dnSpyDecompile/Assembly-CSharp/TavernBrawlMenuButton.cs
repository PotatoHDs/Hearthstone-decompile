using System;
using System.Collections;
using PegasusShared;
using UnityEngine;

// Token: 0x02000739 RID: 1849
public class TavernBrawlMenuButton : BoxMenuButton
{
	// Token: 0x060068B5 RID: 26805 RVA: 0x0022217C File Offset: 0x0022037C
	public override void TriggerOver()
	{
		bool flag = this.IsTavernBrawlActive();
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.TavernBrawl || !TavernBrawlManager.Get().HasUnlockedAnyTavernBrawl || flag)
		{
			base.TriggerOver();
			return;
		}
		this.UpdateTimeText();
		base.StartCoroutine("DoPopup");
	}

	// Token: 0x060068B6 RID: 26806 RVA: 0x002221D3 File Offset: 0x002203D3
	public IEnumerator DoPopup()
	{
		if (!UniversalInputManager.Get().IsTouchMode())
		{
			yield return new WaitForSeconds(this.m_hoverDelay);
		}
		this.isPoppedUp = true;
		if (Box.Get().m_tavernBrawlPopupSound != string.Empty)
		{
			SoundManager.Get().LoadAndPlay(Box.Get().m_tavernBrawlPopupSound);
		}
		Box.Get().m_TavernBrawlButtonVisual.GetComponent<Animator>().Play("TavernBrawl_ButtonPopup");
		yield return null;
		yield break;
	}

	// Token: 0x060068B7 RID: 26807 RVA: 0x002221E4 File Offset: 0x002203E4
	public override void TriggerOut()
	{
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.TavernBrawl || !TavernBrawlManager.Get().HasUnlockedAnyTavernBrawl || this.IsTavernBrawlActive())
		{
			base.TriggerOut();
			return;
		}
		if (!UniversalInputManager.Get().IsTouchMode())
		{
			base.StopCoroutine("DoPopup");
		}
		if (this.isPoppedUp)
		{
			if (Box.Get().m_tavernBrawlPopdownSound != string.Empty)
			{
				SoundManager.Get().LoadAndPlay(Box.Get().m_tavernBrawlPopdownSound);
			}
			Box.Get().m_TavernBrawlButtonVisual.GetComponent<Animator>().Play("TavernBrawl_ButtonPopdown");
			this.isPoppedUp = false;
		}
	}

	// Token: 0x060068B8 RID: 26808 RVA: 0x00222290 File Offset: 0x00220490
	public void ClearHighlightAndTooltip()
	{
		base.TriggerOut();
	}

	// Token: 0x060068B9 RID: 26809 RVA: 0x00222298 File Offset: 0x00220498
	public override void TriggerPress()
	{
		if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.TavernBrawl && this.IsTavernBrawlActive() && TavernBrawlManager.Get().HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
		{
			base.TriggerPress();
			return;
		}
	}

	// Token: 0x060068BA RID: 26810 RVA: 0x002222CC File Offset: 0x002204CC
	public override void TriggerRelease()
	{
		if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.TavernBrawl && this.IsTavernBrawlActive() && TavernBrawlManager.Get().HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
		{
			base.TriggerRelease();
			return;
		}
	}

	// Token: 0x060068BB RID: 26811 RVA: 0x00222300 File Offset: 0x00220500
	private void UpdateTimeText()
	{
		string startingTimeText = TavernBrawlManager.Get().GetStartingTimeText(false);
		this.m_returnsInfo.Text = startingTimeText;
	}

	// Token: 0x060068BC RID: 26812 RVA: 0x00222325 File Offset: 0x00220525
	private bool IsTavernBrawlActive()
	{
		return TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
	}

	// Token: 0x040055D4 RID: 21972
	public UberText m_returnsInfo;

	// Token: 0x040055D5 RID: 21973
	public float m_hoverDelay = 0.5f;

	// Token: 0x040055D6 RID: 21974
	private bool isPoppedUp;
}
