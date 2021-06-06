using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000366 RID: 870
public class ZoneWeapon : Zone
{
	// Token: 0x06003312 RID: 13074 RVA: 0x001065D4 File Offset: 0x001047D4
	public override string ToString()
	{
		return string.Format("{0} (Weapon)", base.ToString());
	}

	// Token: 0x06003313 RID: 13075 RVA: 0x001065E6 File Offset: 0x001047E6
	public override bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		return base.CanAcceptTags(controllerId, zoneTag, cardType, entity) && cardType == TAG_CARDTYPE.WEAPON;
	}

	// Token: 0x06003314 RID: 13076 RVA: 0x001065FE File Offset: 0x001047FE
	public override int RemoveCard(Card card)
	{
		int num = base.RemoveCard(card);
		if (num >= 0 && !this.m_destroyedWeapons.Contains(card))
		{
			this.m_destroyedWeapons.Add(card);
		}
		return num;
	}

	// Token: 0x06003315 RID: 13077 RVA: 0x00106628 File Offset: 0x00104828
	public override void UpdateLayout()
	{
		this.m_updatingLayout++;
		if (GameState.Get().IsMulliganManagerActive())
		{
			base.UpdateLayoutFinished();
			return;
		}
		if (base.IsBlockingLayout())
		{
			base.UpdateLayoutFinished();
			return;
		}
		if (this.m_cards.Count == 0)
		{
			this.m_destroyedWeapons.Clear();
			base.UpdateLayoutFinished();
			return;
		}
		base.StartCoroutine(this.UpdateLayoutImpl());
	}

	// Token: 0x06003316 RID: 13078 RVA: 0x00106691 File Offset: 0x00104891
	private IEnumerator UpdateLayoutImpl()
	{
		Card equippedWeapon = this.m_cards[0];
		while (equippedWeapon.IsDoNotSort())
		{
			yield return null;
		}
		equippedWeapon.ShowCard();
		equippedWeapon.EnableTransitioningZones(true);
		string tweenName = ZoneMgr.Get().GetTweenName<ZoneWeapon>();
		if (this.m_Side == Player.Side.OPPOSING)
		{
			iTween.StopOthersByName(equippedWeapon.gameObject, tweenName, false);
		}
		Vector3 position = base.transform.position;
		position.y += 1.5f;
		Hashtable args = iTween.Hash(new object[]
		{
			"name",
			tweenName,
			"position",
			position,
			"time",
			0.9f
		});
		iTween.MoveTo(equippedWeapon.gameObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"name",
			tweenName,
			"rotation",
			base.transform.localEulerAngles,
			"time",
			0.9f
		});
		iTween.RotateTo(equippedWeapon.gameObject, args2);
		Hashtable args3 = iTween.Hash(new object[]
		{
			"name",
			tweenName,
			"scale",
			base.transform.localScale,
			"time",
			0.9f
		});
		iTween.ScaleTo(equippedWeapon.gameObject, args3);
		yield return new WaitForSeconds(0.9f);
		if (this.m_destroyedWeapons.Count > 0)
		{
			yield return new WaitForSeconds(1.75f);
		}
		this.m_destroyedWeapons.Clear();
		args = iTween.Hash(new object[]
		{
			"position",
			base.transform.position,
			"time",
			0.1f,
			"easetype",
			iTween.EaseType.easeOutCubic,
			"name",
			tweenName
		});
		iTween.MoveTo(equippedWeapon.gameObject, args);
		base.StartFinishLayoutTimer(0.1f);
		yield break;
	}

	// Token: 0x04001C0E RID: 7182
	private const float INTERMEDIATE_Y_OFFSET = 1.5f;

	// Token: 0x04001C0F RID: 7183
	private const float INTERMEDIATE_TRANSITION_SEC = 0.9f;

	// Token: 0x04001C10 RID: 7184
	private const float DESTROYED_WEAPON_WAIT_SEC = 1.75f;

	// Token: 0x04001C11 RID: 7185
	private const float FINAL_TRANSITION_SEC = 0.1f;

	// Token: 0x04001C12 RID: 7186
	private List<Card> m_destroyedWeapons = new List<Card>();
}
