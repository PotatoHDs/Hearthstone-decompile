using System;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class CollectionCardLock : MonoBehaviour
{
	// Token: 0x06000E86 RID: 3718 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x00051980 File Offset: 0x0004FB80
	public void UpdateLockVisual(EntityDef entityDef, CollectionCardVisual.LockType lockType, string reason)
	{
		if (entityDef == null || lockType == CollectionCardVisual.LockType.NONE)
		{
			base.gameObject.SetActive(false);
			return;
		}
		base.gameObject.SetActive(true);
		this.m_lockPlate.SetActive(true);
		this.m_bannedRibbon.SetActive(false);
		TAG_CARDTYPE cardType = entityDef.GetCardType();
		this.m_allyBg.SetActive(false);
		this.m_spellBg.SetActive(false);
		this.m_weaponBg.SetActive(false);
		GameObject gameObject;
		switch (cardType)
		{
		case TAG_CARDTYPE.HERO:
			gameObject = this.m_allyBg;
			this.m_lockPlate.transform.localPosition = this.m_heroLockPlateBone.transform.localPosition;
			goto IL_131;
		case TAG_CARDTYPE.MINION:
			gameObject = this.m_allyBg;
			this.m_lockPlate.transform.localPosition = this.m_lockPlateBone.transform.localPosition;
			goto IL_131;
		case TAG_CARDTYPE.SPELL:
			gameObject = this.m_spellBg;
			this.m_lockPlate.transform.localPosition = this.m_lockPlateBone.transform.localPosition;
			goto IL_131;
		case TAG_CARDTYPE.WEAPON:
			gameObject = this.m_weaponBg;
			this.m_lockPlate.transform.localPosition = this.m_weaponLockPlateBone.transform.localPosition;
			goto IL_131;
		}
		gameObject = this.m_spellBg;
		IL_131:
		float value = 0f;
		switch (lockType)
		{
		case CollectionCardVisual.LockType.MAX_COPIES_IN_DECK:
		{
			value = 0f;
			int num = entityDef.IsElite() ? 1 : 2;
			this.SetLockText(GameStrings.Format("GLUE_COLLECTION_LOCK_MAX_DECK_COPIES", new object[]
			{
				num
			}));
			break;
		}
		case CollectionCardVisual.LockType.NO_MORE_INSTANCES:
			value = 1f;
			this.SetLockText(GameStrings.Get("GLUE_COLLECTION_LOCK_NO_MORE_INSTANCES"));
			break;
		case CollectionCardVisual.LockType.NOT_PLAYABLE:
			value = 1f;
			this.SetLockText(GameStrings.Get("GLUE_COLLECTION_LOCK_CARD_NOT_PLAYABLE"));
			break;
		case CollectionCardVisual.LockType.BANNED:
			this.m_bannedRibbon.SetActive(true);
			this.m_lockPlate.SetActive(false);
			gameObject.SetActive(false);
			return;
		}
		this.SetLockText(reason);
		this.m_lockPlate.GetComponent<Renderer>().GetMaterial().SetFloat("_Desaturate", value);
		gameObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Desaturate", value);
		gameObject.SetActive(true);
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x00051BA4 File Offset: 0x0004FDA4
	public void SetLockText(string text)
	{
		this.m_lockText.Text = text;
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x00028167 File Offset: 0x00026367
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000A05 RID: 2565
	public GameObject m_allyBg;

	// Token: 0x04000A06 RID: 2566
	public GameObject m_spellBg;

	// Token: 0x04000A07 RID: 2567
	public GameObject m_weaponBg;

	// Token: 0x04000A08 RID: 2568
	public GameObject m_lockPlate;

	// Token: 0x04000A09 RID: 2569
	public GameObject m_bannedRibbon;

	// Token: 0x04000A0A RID: 2570
	public UberText m_lockText;

	// Token: 0x04000A0B RID: 2571
	public GameObject m_lockPlateBone;

	// Token: 0x04000A0C RID: 2572
	public GameObject m_weaponLockPlateBone;

	// Token: 0x04000A0D RID: 2573
	public GameObject m_heroLockPlateBone;
}
