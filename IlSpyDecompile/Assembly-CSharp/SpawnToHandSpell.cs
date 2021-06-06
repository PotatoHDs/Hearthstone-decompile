using System.Collections;
using UnityEngine;

public class SpawnToHandSpell : SuperSpell
{
	public float m_CardStartScale = 0.1f;

	public float m_CardDelay = 1f;

	public float m_CardStaggerMin;

	public float m_CardStaggerMax;

	public bool m_AccumulateStagger = true;

	public bool m_Shake = true;

	public float m_ShakeDelay;

	public ShakeMinionIntensity m_ShakeIntensity = ShakeMinionIntensity.MediumShake;

	public Spell m_SpellPrefab;

	protected Map<int, Card> m_targetToOriginMap;

	public override bool AddPowerTargets()
	{
		return AddPowerTargetsInternal(fallbackToStartBlockTarget: false);
	}

	public override void RemoveAllTargets()
	{
		base.RemoveAllTargets();
		if (m_targetToOriginMap != null)
		{
			m_targetToOriginMap.Clear();
		}
	}

	protected override Card GetTargetCardFromPowerTask(int index, PowerTask task)
	{
		Network.PowerHistory power = task.GetPower();
		if (power.Type != Network.PowerType.FULL_ENTITY)
		{
			return null;
		}
		Network.Entity entity = (power as Network.HistFullEntity).Entity;
		Entity entity2 = GameState.Get().GetEntity(entity.ID);
		if (entity2 == null)
		{
			Debug.LogWarning($"{this}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {entity.ID} but there is no entity with that id");
			return null;
		}
		if (entity2.GetZone() != TAG_ZONE.HAND)
		{
			return null;
		}
		return entity2.GetCard();
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		StartCoroutine(DoEffectWithTiming());
	}

	protected virtual Vector3 GetOriginForTarget(int targetIndex = 0)
	{
		if (m_targetToOriginMap == null)
		{
			return GetFallbackOriginPosition();
		}
		if (!m_targetToOriginMap.TryGetValue(targetIndex, out var value))
		{
			return GetFallbackOriginPosition();
		}
		return value.transform.position;
	}

	protected void AddOriginForTarget(int targetIndex, Card card)
	{
		if (m_targetToOriginMap == null)
		{
			m_targetToOriginMap = new Map<int, Card>();
		}
		m_targetToOriginMap[targetIndex] = card;
	}

	protected bool AddUniqueOriginForTarget(int targetIndex, Card card)
	{
		if (m_targetToOriginMap != null && m_targetToOriginMap.ContainsValue(card))
		{
			return false;
		}
		AddOriginForTarget(targetIndex, card);
		return true;
	}

	protected virtual IEnumerator DoEffectWithTiming()
	{
		GameObject sourceObject = GetSource();
		Actor actor = sourceObject.GetComponent<Card>().GetActor();
		if ((bool)actor && m_Shake)
		{
			GameObject gameObject = actor.gameObject;
			MinionShake.ShakeObject(gameObject, ShakeMinionType.RandomDirection, gameObject.transform.position, m_ShakeIntensity, 0.1f, 0f, m_ShakeDelay, ignoreAnimationPlaying: true);
		}
		yield return new WaitForSeconds(m_CardDelay);
		AddTransitionDelays();
		for (int i = 0; i < m_targets.Count; i++)
		{
			GameObject gameObject2 = m_targets[i];
			Card component = gameObject2.GetComponent<Card>();
			component.transform.position = GetOriginForTarget(i);
			float transitionDelay = component.GetTransitionDelay();
			if (m_SpellPrefab != null)
			{
				Spell spell = CloneSpell(m_SpellPrefab, null, delegate
				{
				});
				spell.SetSource(sourceObject);
				spell.AddTarget(gameObject2);
				spell.SetPosition(component.transform.position);
				StartCoroutine(ActivateSpellAfterDelay(spell, transitionDelay));
			}
			component.transform.localScale = new Vector3(m_CardStartScale, m_CardStartScale, m_CardStartScale);
			component.SetTransitionStyle(ZoneTransitionStyle.VERY_SLOW);
			component.SetDoNotWarpToNewZone(on: true);
		}
		m_effectsPendingFinish--;
		FinishIfPossible();
	}

	protected IEnumerator ActivateSpellAfterDelay(Spell spell, float delay)
	{
		yield return new WaitForSeconds(delay);
		spell.Activate();
	}

	protected string GetCardIdForTarget(int targetIndex)
	{
		return m_targets[targetIndex].GetComponent<Card>().GetEntity().GetCardId();
	}

	protected Vector3 GetFallbackOriginPosition()
	{
		Card component = GetSource().GetComponent<Card>();
		if (component.GetEntity().HasTag(GAME_TAG.USE_LEADERBOARD_AS_SPAWN_ORIGIN) && PlayerLeaderboardManager.Get() != null)
		{
			PlayerLeaderboardCard tileForPlayerId = PlayerLeaderboardManager.Get().GetTileForPlayerId(component.GetEntity().GetTag(GAME_TAG.PLAYER_ID));
			if (tileForPlayerId != null)
			{
				return tileForPlayerId.m_PlayerLeaderboardTile.transform.position;
			}
			return PlayerLeaderboardManager.Get().transform.position;
		}
		return base.transform.position;
	}

	private void AddTransitionDelays()
	{
		if (m_CardStaggerMin <= 0f && m_CardStaggerMax <= 0f)
		{
			return;
		}
		if (m_AccumulateStagger)
		{
			float num = 0f;
			for (int i = 0; i < m_targets.Count; i++)
			{
				Card component = m_targets[i].GetComponent<Card>();
				float num2 = Random.Range(m_CardStaggerMin, m_CardStaggerMax);
				num += num2;
				component.SetTransitionDelay(num);
			}
		}
		else
		{
			for (int j = 0; j < m_targets.Count; j++)
			{
				Card component2 = m_targets[j].GetComponent<Card>();
				float transitionDelay = Random.Range(m_CardStaggerMin, m_CardStaggerMax);
				component2.SetTransitionDelay(transitionDelay);
			}
		}
	}
}
