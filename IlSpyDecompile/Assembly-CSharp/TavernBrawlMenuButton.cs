using System.Collections;
using PegasusShared;
using UnityEngine;

public class TavernBrawlMenuButton : BoxMenuButton
{
	public UberText m_returnsInfo;

	public float m_hoverDelay = 0.5f;

	private bool isPoppedUp;

	public override void TriggerOver()
	{
		bool flag = IsTavernBrawlActive();
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.TavernBrawl || !TavernBrawlManager.Get().HasUnlockedAnyTavernBrawl || flag)
		{
			base.TriggerOver();
			return;
		}
		UpdateTimeText();
		StartCoroutine("DoPopup");
	}

	public IEnumerator DoPopup()
	{
		if (!UniversalInputManager.Get().IsTouchMode())
		{
			yield return new WaitForSeconds(m_hoverDelay);
		}
		isPoppedUp = true;
		if (Box.Get().m_tavernBrawlPopupSound != string.Empty)
		{
			SoundManager.Get().LoadAndPlay(Box.Get().m_tavernBrawlPopupSound);
		}
		Box.Get().m_TavernBrawlButtonVisual.GetComponent<Animator>().Play("TavernBrawl_ButtonPopup");
		yield return null;
	}

	public override void TriggerOut()
	{
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.TavernBrawl || !TavernBrawlManager.Get().HasUnlockedAnyTavernBrawl || IsTavernBrawlActive())
		{
			base.TriggerOut();
			return;
		}
		if (!UniversalInputManager.Get().IsTouchMode())
		{
			StopCoroutine("DoPopup");
		}
		if (isPoppedUp)
		{
			if (Box.Get().m_tavernBrawlPopdownSound != string.Empty)
			{
				SoundManager.Get().LoadAndPlay(Box.Get().m_tavernBrawlPopdownSound);
			}
			Box.Get().m_TavernBrawlButtonVisual.GetComponent<Animator>().Play("TavernBrawl_ButtonPopdown");
			isPoppedUp = false;
		}
	}

	public void ClearHighlightAndTooltip()
	{
		base.TriggerOut();
	}

	public override void TriggerPress()
	{
		if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.TavernBrawl && IsTavernBrawlActive() && TavernBrawlManager.Get().HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
		{
			base.TriggerPress();
		}
	}

	public override void TriggerRelease()
	{
		if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.TavernBrawl && IsTavernBrawlActive() && TavernBrawlManager.Get().HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
		{
			base.TriggerRelease();
		}
	}

	private void UpdateTimeText()
	{
		string startingTimeText = TavernBrawlManager.Get().GetStartingTimeText();
		m_returnsInfo.Text = startingTimeText;
	}

	private bool IsTavernBrawlActive()
	{
		return TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
	}
}
