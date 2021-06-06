using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007CE RID: 1998
public class AnomalusDeathrattleSpell : Spell
{
	// Token: 0x06006DFE RID: 28158 RVA: 0x00237380 File Offset: 0x00235580
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		List<Card> list = new List<Card>();
		List<Entity> list2 = new List<Entity>();
		foreach (GameObject gameObject in this.GetVisualTargets())
		{
			if (!(gameObject == null))
			{
				Card component = gameObject.GetComponent<Card>();
				list.Add(component);
				list2.Add(component.GetEntity());
			}
		}
		List<Entity> entitiesKilledBySourceAmongstTargets = GameUtils.GetEntitiesKilledBySourceAmongstTargets(base.GetSourceCard().GetEntity().GetEntityId(), list2);
		using (List<Card>.Enumerator enumerator2 = list.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				Card targetCard = enumerator2.Current;
				if (entitiesKilledBySourceAmongstTargets.Exists((Entity killedEntity) => killedEntity.GetEntityId() == targetCard.GetEntity().GetEntityId()))
				{
					targetCard.OverrideCustomDeathSpell(UnityEngine.Object.Instantiate<Spell>(this.m_CustomDeathSpell));
				}
			}
		}
		base.StartCoroutine(this.AnimateMinions());
	}

	// Token: 0x06006DFF RID: 28159 RVA: 0x0023749C File Offset: 0x0023569C
	private IEnumerator AnimateMinions()
	{
		if (this.m_source == null)
		{
			yield break;
		}
		yield return new WaitForSeconds(this.m_DelayBeforeStart);
		float num = 0f;
		this.OnSpellFinished();
		this.m_TargetActorGameObjects = new GameObject[this.m_targets.Count];
		this.m_TargetActors = new Actor[this.m_targets.Count];
		for (int i = 0; i < this.m_targets.Count; i++)
		{
			GameObject gameObject = this.m_targets[i];
			if (!(gameObject == null))
			{
				Card component = gameObject.GetComponent<Card>();
				if (!(component == null))
				{
					Actor actor = component.GetActor();
					if (!(actor == null))
					{
						this.m_TargetActors[i] = actor;
						GameObject gameObject2 = actor.gameObject;
						if (!(gameObject2 == null))
						{
							this.m_TargetActorGameObjects[i] = gameObject2;
							Vector3 localPosition = gameObject2.transform.localPosition;
							Quaternion localRotation = gameObject2.transform.localRotation;
							float num2 = Vector3.Distance(this.m_source.transform.position, gameObject2.transform.position);
							float num3 = num2 * this.m_DelayDistanceModifier;
							if (num < num3)
							{
								num = num3;
							}
							float y = UnityEngine.Random.Range(this.m_LiftHeightMin, this.m_LiftHeightMax);
							Hashtable args = iTween.Hash(new object[]
							{
								"time",
								this.m_RiseTime,
								"delay",
								num2 * this.m_DelayDistanceModifier,
								"position",
								new Vector3(0f, y, 0f),
								"easetype",
								iTween.EaseType.easeOutExpo,
								"islocal",
								true,
								"name",
								string.Format("Lift_{0}_{1}", gameObject2.name, i)
							});
							iTween.MoveTo(gameObject2, args);
							Vector3 eulerAngles = localRotation.eulerAngles;
							eulerAngles.x += UnityEngine.Random.Range(this.m_LiftRotMin, this.m_LiftRotMax);
							eulerAngles.z += UnityEngine.Random.Range(this.m_LiftRotMin, this.m_LiftRotMax);
							Hashtable args2 = iTween.Hash(new object[]
							{
								"time",
								this.m_RiseTime + this.m_HangTime,
								"delay",
								num2 * this.m_DelayDistanceModifier,
								"rotation",
								eulerAngles,
								"easetype",
								iTween.EaseType.easeOutQuad,
								"islocal",
								true,
								"name",
								string.Format("LiftRot_{0}_{1}", gameObject2.name, i)
							});
							iTween.RotateTo(gameObject2, args2);
						}
					}
				}
			}
		}
		yield return new WaitForSeconds(num);
		for (int j = 0; j < this.m_targets.Count; j++)
		{
			GameObject gameObject3 = this.m_TargetActorGameObjects[j];
			if (!(gameObject3 == null))
			{
				GameObject gameObject4 = this.m_targets[j];
				if (!(gameObject4 == null))
				{
					Card component2 = gameObject4.GetComponent<Card>();
					if (!(component2 == null))
					{
						if (component2.GetZone().m_ServerTag == TAG_ZONE.GRAVEYARD)
						{
							Actor actor2 = this.m_TargetActors[j];
							if (actor2 == null)
							{
								goto IL_515;
							}
							actor2.DoCardDeathVisuals();
						}
						float num4 = 0f;
						Hashtable args3 = iTween.Hash(new object[]
						{
							"time",
							this.m_SlamTime,
							"delay",
							this.m_DelayAfterSpellFinish + num4,
							"position",
							Vector3.zero,
							"easetype",
							iTween.EaseType.easeInCubic,
							"islocal",
							true,
							"name",
							string.Format("SlamPos_{0}_{1}", gameObject3.name, j)
						});
						iTween.MoveTo(gameObject3, args3);
						Hashtable args4 = iTween.Hash(new object[]
						{
							"time",
							this.m_SlamTime * 0.8f,
							"delay",
							this.m_DelayAfterSpellFinish + num4 + this.m_SlamTime * 0.2f,
							"rotation",
							Vector3.zero,
							"easetype",
							iTween.EaseType.easeInQuad,
							"islocal",
							true,
							"name",
							string.Format("SlamRot_{0}_{1}", gameObject3.name, j)
						});
						iTween.RotateTo(gameObject3, args4);
					}
				}
			}
			IL_515:;
		}
		yield break;
	}

	// Token: 0x04005812 RID: 22546
	public Spell m_CustomDeathSpell;

	// Token: 0x04005813 RID: 22547
	public float m_DelayBeforeStart = 1f;

	// Token: 0x04005814 RID: 22548
	public float m_DelayDistanceModifier = 1f;

	// Token: 0x04005815 RID: 22549
	public float m_RiseTime = 0.5f;

	// Token: 0x04005816 RID: 22550
	public float m_HangTime = 1f;

	// Token: 0x04005817 RID: 22551
	public float m_LiftHeightMin = 2f;

	// Token: 0x04005818 RID: 22552
	public float m_LiftHeightMax = 3f;

	// Token: 0x04005819 RID: 22553
	public float m_LiftRotMin = -15f;

	// Token: 0x0400581A RID: 22554
	public float m_LiftRotMax = 15f;

	// Token: 0x0400581B RID: 22555
	public float m_SlamTime = 0.15f;

	// Token: 0x0400581C RID: 22556
	public float m_Bounceness = 0.2f;

	// Token: 0x0400581D RID: 22557
	public float m_DelayAfterSpellFinish = 3f;

	// Token: 0x0400581E RID: 22558
	private GameObject[] m_TargetActorGameObjects;

	// Token: 0x0400581F RID: 22559
	private Actor[] m_TargetActors;
}
