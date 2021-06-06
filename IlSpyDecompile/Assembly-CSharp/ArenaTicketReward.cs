using System.Collections;
using UnityEngine;

public class ArenaTicketReward : Reward
{
	public GameObject m_ticketVisual;

	public GameObject m_plusSign;

	public UberText m_countLabel;

	protected override void InitData()
	{
		SetData(new ForgeTicketRewardData(), updateVisuals: false);
	}

	protected override void ShowReward(bool updateCacheValues)
	{
		string empty = string.Empty;
		string empty2 = string.Empty;
		string source = string.Empty;
		if (base.Data.Origin == NetCache.ProfileNotice.NoticeOrigin.OUT_OF_BAND_LICENSE)
		{
			ForgeTicketRewardData forgeTicketRewardData = base.Data as ForgeTicketRewardData;
			empty = GameStrings.Get("GLOBAL_REWARD_FORGE_HEADLINE");
			source = GameStrings.Format("GLOBAL_REWARD_FORGE_DETAILS_OUT_OF_BAND", forgeTicketRewardData.Quantity);
		}
		else if (base.Data.Origin == NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT && base.Data.OriginData == 56)
		{
			empty = GameStrings.Get("GLOBAL_REWARD_FORGE_UNLOCKED_HEADLINE");
			source = GameStrings.Get("GLOBAL_REWARD_FORGE_UNLOCKED_SOURCE");
		}
		else
		{
			empty = GameStrings.Get("GLOBAL_REWARD_ARENA_TICKET_HEADLINE");
		}
		SetRewardText(empty, empty2, source);
		bool active = false;
		if (m_countLabel != null)
		{
			ForgeTicketRewardData forgeTicketRewardData2 = base.Data as ForgeTicketRewardData;
			if (forgeTicketRewardData2.Quantity > 9)
			{
				m_countLabel.Text = "9";
				active = true;
			}
			else
			{
				m_countLabel.Text = forgeTicketRewardData2.Quantity.ToString();
			}
		}
		m_root.SetActive(value: true);
		if (m_plusSign != null)
		{
			m_plusSign.SetActive(active);
		}
		m_ticketVisual.transform.localEulerAngles = new Vector3(m_ticketVisual.transform.localEulerAngles.x, m_ticketVisual.transform.localEulerAngles.y, 180f);
		Hashtable args = iTween.Hash("amount", new Vector3(0f, 0f, 540f), "time", 1.5f, "easeType", iTween.EaseType.easeOutElastic, "space", Space.Self);
		iTween.RotateAdd(m_ticketVisual, args);
	}

	protected override void HideReward()
	{
		base.HideReward();
		m_root.SetActive(value: false);
	}
}
