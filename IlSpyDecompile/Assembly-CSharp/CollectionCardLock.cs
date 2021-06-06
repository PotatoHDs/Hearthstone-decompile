using UnityEngine;

public class CollectionCardLock : MonoBehaviour
{
	public GameObject m_allyBg;

	public GameObject m_spellBg;

	public GameObject m_weaponBg;

	public GameObject m_lockPlate;

	public GameObject m_bannedRibbon;

	public UberText m_lockText;

	public GameObject m_lockPlateBone;

	public GameObject m_weaponLockPlateBone;

	public GameObject m_heroLockPlateBone;

	private void Start()
	{
	}

	public void UpdateLockVisual(EntityDef entityDef, CollectionCardVisual.LockType lockType, string reason)
	{
		if (entityDef == null || lockType == CollectionCardVisual.LockType.NONE)
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		base.gameObject.SetActive(value: true);
		m_lockPlate.SetActive(value: true);
		m_bannedRibbon.SetActive(value: false);
		TAG_CARDTYPE cardType = entityDef.GetCardType();
		m_allyBg.SetActive(value: false);
		m_spellBg.SetActive(value: false);
		m_weaponBg.SetActive(value: false);
		GameObject gameObject;
		switch (cardType)
		{
		case TAG_CARDTYPE.SPELL:
			gameObject = m_spellBg;
			m_lockPlate.transform.localPosition = m_lockPlateBone.transform.localPosition;
			break;
		case TAG_CARDTYPE.MINION:
			gameObject = m_allyBg;
			m_lockPlate.transform.localPosition = m_lockPlateBone.transform.localPosition;
			break;
		case TAG_CARDTYPE.WEAPON:
			gameObject = m_weaponBg;
			m_lockPlate.transform.localPosition = m_weaponLockPlateBone.transform.localPosition;
			break;
		case TAG_CARDTYPE.HERO:
			gameObject = m_allyBg;
			m_lockPlate.transform.localPosition = m_heroLockPlateBone.transform.localPosition;
			break;
		default:
			gameObject = m_spellBg;
			break;
		}
		float value = 0f;
		switch (lockType)
		{
		case CollectionCardVisual.LockType.MAX_COPIES_IN_DECK:
		{
			value = 0f;
			int num = (entityDef.IsElite() ? 1 : 2);
			SetLockText(GameStrings.Format("GLUE_COLLECTION_LOCK_MAX_DECK_COPIES", num));
			break;
		}
		case CollectionCardVisual.LockType.NO_MORE_INSTANCES:
			value = 1f;
			SetLockText(GameStrings.Get("GLUE_COLLECTION_LOCK_NO_MORE_INSTANCES"));
			break;
		case CollectionCardVisual.LockType.NOT_PLAYABLE:
			value = 1f;
			SetLockText(GameStrings.Get("GLUE_COLLECTION_LOCK_CARD_NOT_PLAYABLE"));
			break;
		case CollectionCardVisual.LockType.BANNED:
			m_bannedRibbon.SetActive(value: true);
			m_lockPlate.SetActive(value: false);
			gameObject.SetActive(value: false);
			return;
		}
		SetLockText(reason);
		m_lockPlate.GetComponent<Renderer>().GetMaterial().SetFloat("_Desaturate", value);
		gameObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Desaturate", value);
		gameObject.SetActive(value: true);
	}

	public void SetLockText(string text)
	{
		m_lockText.Text = text;
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
	}
}
