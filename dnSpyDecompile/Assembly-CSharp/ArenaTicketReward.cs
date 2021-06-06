using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200065E RID: 1630
public class ArenaTicketReward : Reward
{
	// Token: 0x06005BDE RID: 23518 RVA: 0x001DDF9C File Offset: 0x001DC19C
	protected override void InitData()
	{
		base.SetData(new ForgeTicketRewardData(), false);
	}

	// Token: 0x06005BDF RID: 23519 RVA: 0x001DDFAC File Offset: 0x001DC1AC
	protected override void ShowReward(bool updateCacheValues)
	{
		string headline = string.Empty;
		string empty = string.Empty;
		string source = string.Empty;
		if (base.Data.Origin == NetCache.ProfileNotice.NoticeOrigin.OUT_OF_BAND_LICENSE)
		{
			ForgeTicketRewardData forgeTicketRewardData = base.Data as ForgeTicketRewardData;
			headline = GameStrings.Get("GLOBAL_REWARD_FORGE_HEADLINE");
			source = GameStrings.Format("GLOBAL_REWARD_FORGE_DETAILS_OUT_OF_BAND", new object[]
			{
				forgeTicketRewardData.Quantity
			});
		}
		else if (base.Data.Origin == NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT && base.Data.OriginData == 56L)
		{
			headline = GameStrings.Get("GLOBAL_REWARD_FORGE_UNLOCKED_HEADLINE");
			source = GameStrings.Get("GLOBAL_REWARD_FORGE_UNLOCKED_SOURCE");
		}
		else
		{
			headline = GameStrings.Get("GLOBAL_REWARD_ARENA_TICKET_HEADLINE");
		}
		base.SetRewardText(headline, empty, source);
		bool active = false;
		if (this.m_countLabel != null)
		{
			ForgeTicketRewardData forgeTicketRewardData2 = base.Data as ForgeTicketRewardData;
			if (forgeTicketRewardData2.Quantity > 9)
			{
				this.m_countLabel.Text = "9";
				active = true;
			}
			else
			{
				this.m_countLabel.Text = forgeTicketRewardData2.Quantity.ToString();
			}
		}
		this.m_root.SetActive(true);
		if (this.m_plusSign != null)
		{
			this.m_plusSign.SetActive(active);
		}
		this.m_ticketVisual.transform.localEulerAngles = new Vector3(this.m_ticketVisual.transform.localEulerAngles.x, this.m_ticketVisual.transform.localEulerAngles.y, 180f);
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			new Vector3(0f, 0f, 540f),
			"time",
			1.5f,
			"easeType",
			iTween.EaseType.easeOutElastic,
			"space",
			Space.Self
		});
		iTween.RotateAdd(this.m_ticketVisual, args);
	}

	// Token: 0x06005BE0 RID: 23520 RVA: 0x001DDEDB File Offset: 0x001DC0DB
	protected override void HideReward()
	{
		base.HideReward();
		this.m_root.SetActive(false);
	}

	// Token: 0x04004E4F RID: 20047
	public GameObject m_ticketVisual;

	// Token: 0x04004E50 RID: 20048
	public GameObject m_plusSign;

	// Token: 0x04004E51 RID: 20049
	public UberText m_countLabel;
}
