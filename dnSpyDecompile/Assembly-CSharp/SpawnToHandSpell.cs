using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000824 RID: 2084
public class SpawnToHandSpell : SuperSpell
{
	// Token: 0x06007011 RID: 28689 RVA: 0x002426DA File Offset: 0x002408DA
	public override bool AddPowerTargets()
	{
		return base.AddPowerTargetsInternal(false);
	}

	// Token: 0x06007012 RID: 28690 RVA: 0x002426E3 File Offset: 0x002408E3
	public override void RemoveAllTargets()
	{
		base.RemoveAllTargets();
		if (this.m_targetToOriginMap != null)
		{
			this.m_targetToOriginMap.Clear();
		}
	}

	// Token: 0x06007013 RID: 28691 RVA: 0x00242700 File Offset: 0x00240900
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
			Debug.LogWarning(string.Format("{0}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {1} but there is no entity with that id", this, entity.ID));
			return null;
		}
		if (entity2.GetZone() != TAG_ZONE.HAND)
		{
			return null;
		}
		return entity2.GetCard();
	}

	// Token: 0x06007014 RID: 28692 RVA: 0x0024276D File Offset: 0x0024096D
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		base.StartCoroutine(this.DoEffectWithTiming());
	}

	// Token: 0x06007015 RID: 28693 RVA: 0x00242794 File Offset: 0x00240994
	protected virtual Vector3 GetOriginForTarget(int targetIndex = 0)
	{
		if (this.m_targetToOriginMap == null)
		{
			return this.GetFallbackOriginPosition();
		}
		Card card;
		if (!this.m_targetToOriginMap.TryGetValue(targetIndex, out card))
		{
			return this.GetFallbackOriginPosition();
		}
		return card.transform.position;
	}

	// Token: 0x06007016 RID: 28694 RVA: 0x002427D2 File Offset: 0x002409D2
	protected void AddOriginForTarget(int targetIndex, Card card)
	{
		if (this.m_targetToOriginMap == null)
		{
			this.m_targetToOriginMap = new Map<int, Card>();
		}
		this.m_targetToOriginMap[targetIndex] = card;
	}

	// Token: 0x06007017 RID: 28695 RVA: 0x002427F4 File Offset: 0x002409F4
	protected bool AddUniqueOriginForTarget(int targetIndex, Card card)
	{
		if (this.m_targetToOriginMap != null && this.m_targetToOriginMap.ContainsValue(card))
		{
			return false;
		}
		this.AddOriginForTarget(targetIndex, card);
		return true;
	}

	// Token: 0x06007018 RID: 28696 RVA: 0x00242817 File Offset: 0x00240A17
	protected virtual IEnumerator DoEffectWithTiming()
	{
		GameObject sourceObject = base.GetSource();
		Actor actor = sourceObject.GetComponent<Card>().GetActor();
		if (actor && this.m_Shake)
		{
			GameObject gameObject = actor.gameObject;
			MinionShake.ShakeObject(gameObject, ShakeMinionType.RandomDirection, gameObject.transform.position, this.m_ShakeIntensity, 0.1f, 0f, this.m_ShakeDelay, true);
		}
		yield return new WaitForSeconds(this.m_CardDelay);
		this.AddTransitionDelays();
		for (int i = 0; i < this.m_targets.Count; i++)
		{
			GameObject gameObject2 = this.m_targets[i];
			Card component = gameObject2.GetComponent<Card>();
			component.transform.position = this.GetOriginForTarget(i);
			float transitionDelay = component.GetTransitionDelay();
			if (this.m_SpellPrefab != null)
			{
				Spell spell = base.CloneSpell(this.m_SpellPrefab, null, delegate(Spell s, object o)
				{
				});
				spell.SetSource(sourceObject);
				spell.AddTarget(gameObject2);
				spell.SetPosition(component.transform.position);
				base.StartCoroutine(this.ActivateSpellAfterDelay(spell, transitionDelay));
			}
			component.transform.localScale = new Vector3(this.m_CardStartScale, this.m_CardStartScale, this.m_CardStartScale);
			component.SetTransitionStyle(ZoneTransitionStyle.VERY_SLOW);
			component.SetDoNotWarpToNewZone(true);
		}
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
		yield break;
	}

	// Token: 0x06007019 RID: 28697 RVA: 0x00242826 File Offset: 0x00240A26
	protected IEnumerator ActivateSpellAfterDelay(Spell spell, float delay)
	{
		yield return new WaitForSeconds(delay);
		spell.Activate();
		yield break;
	}

	// Token: 0x0600701A RID: 28698 RVA: 0x0024283C File Offset: 0x00240A3C
	protected string GetCardIdForTarget(int targetIndex)
	{
		return this.m_targets[targetIndex].GetComponent<Card>().GetEntity().GetCardId();
	}

	// Token: 0x0600701B RID: 28699 RVA: 0x0024285C File Offset: 0x00240A5C
	protected Vector3 GetFallbackOriginPosition()
	{
		Card component = base.GetSource().GetComponent<Card>();
		if (!component.GetEntity().HasTag(GAME_TAG.USE_LEADERBOARD_AS_SPAWN_ORIGIN) || !(PlayerLeaderboardManager.Get() != null))
		{
			return base.transform.position;
		}
		PlayerLeaderboardCard tileForPlayerId = PlayerLeaderboardManager.Get().GetTileForPlayerId(component.GetEntity().GetTag(GAME_TAG.PLAYER_ID));
		if (tileForPlayerId != null)
		{
			return tileForPlayerId.m_PlayerLeaderboardTile.transform.position;
		}
		return PlayerLeaderboardManager.Get().transform.position;
	}

	// Token: 0x0600701C RID: 28700 RVA: 0x002428E4 File Offset: 0x00240AE4
	private void AddTransitionDelays()
	{
		if (this.m_CardStaggerMin <= 0f && this.m_CardStaggerMax <= 0f)
		{
			return;
		}
		if (this.m_AccumulateStagger)
		{
			float num = 0f;
			for (int i = 0; i < this.m_targets.Count; i++)
			{
				Card component = this.m_targets[i].GetComponent<Card>();
				float num2 = UnityEngine.Random.Range(this.m_CardStaggerMin, this.m_CardStaggerMax);
				num += num2;
				component.SetTransitionDelay(num);
			}
			return;
		}
		for (int j = 0; j < this.m_targets.Count; j++)
		{
			Card component2 = this.m_targets[j].GetComponent<Card>();
			float transitionDelay = UnityEngine.Random.Range(this.m_CardStaggerMin, this.m_CardStaggerMax);
			component2.SetTransitionDelay(transitionDelay);
		}
	}

	// Token: 0x040059DC RID: 23004
	public float m_CardStartScale = 0.1f;

	// Token: 0x040059DD RID: 23005
	public float m_CardDelay = 1f;

	// Token: 0x040059DE RID: 23006
	public float m_CardStaggerMin;

	// Token: 0x040059DF RID: 23007
	public float m_CardStaggerMax;

	// Token: 0x040059E0 RID: 23008
	public bool m_AccumulateStagger = true;

	// Token: 0x040059E1 RID: 23009
	public bool m_Shake = true;

	// Token: 0x040059E2 RID: 23010
	public float m_ShakeDelay;

	// Token: 0x040059E3 RID: 23011
	public ShakeMinionIntensity m_ShakeIntensity = ShakeMinionIntensity.MediumShake;

	// Token: 0x040059E4 RID: 23012
	public Spell m_SpellPrefab;

	// Token: 0x040059E5 RID: 23013
	protected Map<int, Card> m_targetToOriginMap;
}
