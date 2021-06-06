using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalusDeathrattleSpell : Spell
{
	public Spell m_CustomDeathSpell;

	public float m_DelayBeforeStart = 1f;

	public float m_DelayDistanceModifier = 1f;

	public float m_RiseTime = 0.5f;

	public float m_HangTime = 1f;

	public float m_LiftHeightMin = 2f;

	public float m_LiftHeightMax = 3f;

	public float m_LiftRotMin = -15f;

	public float m_LiftRotMax = 15f;

	public float m_SlamTime = 0.15f;

	public float m_Bounceness = 0.2f;

	public float m_DelayAfterSpellFinish = 3f;

	private GameObject[] m_TargetActorGameObjects;

	private Actor[] m_TargetActors;

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		List<Card> list = new List<Card>();
		List<Entity> list2 = new List<Entity>();
		foreach (GameObject visualTarget in GetVisualTargets())
		{
			if (!(visualTarget == null))
			{
				Card component = visualTarget.GetComponent<Card>();
				list.Add(component);
				list2.Add(component.GetEntity());
			}
		}
		List<Entity> entitiesKilledBySourceAmongstTargets = GameUtils.GetEntitiesKilledBySourceAmongstTargets(GetSourceCard().GetEntity().GetEntityId(), list2);
		foreach (Card targetCard in list)
		{
			if (entitiesKilledBySourceAmongstTargets.Exists((Entity killedEntity) => killedEntity.GetEntityId() == targetCard.GetEntity().GetEntityId()))
			{
				targetCard.OverrideCustomDeathSpell(Object.Instantiate(m_CustomDeathSpell));
			}
		}
		StartCoroutine(AnimateMinions());
	}

	private IEnumerator AnimateMinions()
	{
		if (m_source == null)
		{
			yield break;
		}
		yield return new WaitForSeconds(m_DelayBeforeStart);
		float num = 0f;
		OnSpellFinished();
		m_TargetActorGameObjects = new GameObject[m_targets.Count];
		m_TargetActors = new Actor[m_targets.Count];
		for (int i = 0; i < m_targets.Count; i++)
		{
			GameObject gameObject = m_targets[i];
			if (gameObject == null)
			{
				continue;
			}
			Card component = gameObject.GetComponent<Card>();
			if (component == null)
			{
				continue;
			}
			Actor actor = component.GetActor();
			if (actor == null)
			{
				continue;
			}
			m_TargetActors[i] = actor;
			GameObject gameObject2 = actor.gameObject;
			if (!(gameObject2 == null))
			{
				m_TargetActorGameObjects[i] = gameObject2;
				_ = gameObject2.transform.localPosition;
				Quaternion localRotation = gameObject2.transform.localRotation;
				float num2 = Vector3.Distance(m_source.transform.position, gameObject2.transform.position);
				float num3 = num2 * m_DelayDistanceModifier;
				if (num < num3)
				{
					num = num3;
				}
				float y = Random.Range(m_LiftHeightMin, m_LiftHeightMax);
				Hashtable args = iTween.Hash("time", m_RiseTime, "delay", num2 * m_DelayDistanceModifier, "position", new Vector3(0f, y, 0f), "easetype", iTween.EaseType.easeOutExpo, "islocal", true, "name", $"Lift_{gameObject2.name}_{i}");
				iTween.MoveTo(gameObject2, args);
				Vector3 eulerAngles = localRotation.eulerAngles;
				eulerAngles.x += Random.Range(m_LiftRotMin, m_LiftRotMax);
				eulerAngles.z += Random.Range(m_LiftRotMin, m_LiftRotMax);
				Hashtable args2 = iTween.Hash("time", m_RiseTime + m_HangTime, "delay", num2 * m_DelayDistanceModifier, "rotation", eulerAngles, "easetype", iTween.EaseType.easeOutQuad, "islocal", true, "name", $"LiftRot_{gameObject2.name}_{i}");
				iTween.RotateTo(gameObject2, args2);
			}
		}
		yield return new WaitForSeconds(num);
		for (int j = 0; j < m_targets.Count; j++)
		{
			GameObject gameObject3 = m_TargetActorGameObjects[j];
			if (gameObject3 == null)
			{
				continue;
			}
			GameObject gameObject4 = m_targets[j];
			if (gameObject4 == null)
			{
				continue;
			}
			Card component2 = gameObject4.GetComponent<Card>();
			if (component2 == null)
			{
				continue;
			}
			if (component2.GetZone().m_ServerTag == TAG_ZONE.GRAVEYARD)
			{
				Actor actor2 = m_TargetActors[j];
				if (actor2 == null)
				{
					continue;
				}
				actor2.DoCardDeathVisuals();
			}
			float num4 = 0f;
			Hashtable args3 = iTween.Hash("time", m_SlamTime, "delay", m_DelayAfterSpellFinish + num4, "position", Vector3.zero, "easetype", iTween.EaseType.easeInCubic, "islocal", true, "name", $"SlamPos_{gameObject3.name}_{j}");
			iTween.MoveTo(gameObject3, args3);
			Hashtable args4 = iTween.Hash("time", m_SlamTime * 0.8f, "delay", m_DelayAfterSpellFinish + num4 + m_SlamTime * 0.2f, "rotation", Vector3.zero, "easetype", iTween.EaseType.easeInQuad, "islocal", true, "name", $"SlamRot_{gameObject3.name}_{j}");
			iTween.RotateTo(gameObject3, args4);
		}
	}
}
