using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Zone
{
	private const float INTERMEDIATE_Y_OFFSET = 1.5f;

	private const float INTERMEDIATE_TRANSITION_SEC = 0.9f;

	private const float DESTROYED_WEAPON_WAIT_SEC = 1.75f;

	private const float FINAL_TRANSITION_SEC = 0.1f;

	private List<Card> m_destroyedWeapons = new List<Card>();

	public override string ToString()
	{
		return $"{base.ToString()} (Weapon)";
	}

	public override bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		if (!base.CanAcceptTags(controllerId, zoneTag, cardType, entity))
		{
			return false;
		}
		if (cardType != TAG_CARDTYPE.WEAPON)
		{
			return false;
		}
		return true;
	}

	public override int RemoveCard(Card card)
	{
		int num = base.RemoveCard(card);
		if (num >= 0 && !m_destroyedWeapons.Contains(card))
		{
			m_destroyedWeapons.Add(card);
		}
		return num;
	}

	public override void UpdateLayout()
	{
		m_updatingLayout++;
		if (GameState.Get().IsMulliganManagerActive())
		{
			UpdateLayoutFinished();
		}
		else if (IsBlockingLayout())
		{
			UpdateLayoutFinished();
		}
		else if (m_cards.Count == 0)
		{
			m_destroyedWeapons.Clear();
			UpdateLayoutFinished();
		}
		else
		{
			StartCoroutine(UpdateLayoutImpl());
		}
	}

	private IEnumerator UpdateLayoutImpl()
	{
		Card equippedWeapon = m_cards[0];
		while (equippedWeapon.IsDoNotSort())
		{
			yield return null;
		}
		equippedWeapon.ShowCard();
		equippedWeapon.EnableTransitioningZones(enable: true);
		string tweenName = ZoneMgr.Get().GetTweenName<ZoneWeapon>();
		if (m_Side == Player.Side.OPPOSING)
		{
			iTween.StopOthersByName(equippedWeapon.gameObject, tweenName);
		}
		Vector3 position = base.transform.position;
		position.y += 1.5f;
		Hashtable args = iTween.Hash("name", tweenName, "position", position, "time", 0.9f);
		iTween.MoveTo(equippedWeapon.gameObject, args);
		Hashtable args2 = iTween.Hash("name", tweenName, "rotation", base.transform.localEulerAngles, "time", 0.9f);
		iTween.RotateTo(equippedWeapon.gameObject, args2);
		Hashtable args3 = iTween.Hash("name", tweenName, "scale", base.transform.localScale, "time", 0.9f);
		iTween.ScaleTo(equippedWeapon.gameObject, args3);
		yield return new WaitForSeconds(0.9f);
		if (m_destroyedWeapons.Count > 0)
		{
			yield return new WaitForSeconds(1.75f);
		}
		m_destroyedWeapons.Clear();
		args = iTween.Hash("position", base.transform.position, "time", 0.1f, "easetype", iTween.EaseType.easeOutCubic, "name", tweenName);
		iTween.MoveTo(equippedWeapon.gameObject, args);
		StartFinishLayoutTimer(0.1f);
	}
}
