using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000DF RID: 223
public class JaraxxusMinionSpell : Spell
{
	// Token: 0x06000D1E RID: 3358 RVA: 0x0004BC00 File Offset: 0x00049E00
	public override bool AddPowerTargets()
	{
		foreach (PowerTask powerTask in this.m_taskList.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.FULL_ENTITY)
			{
				int id = (power as Network.HistFullEntity).Entity.ID;
				Entity entity = GameState.Get().GetEntity(id);
				if (entity == null)
				{
					Debug.LogWarning(string.Format("{0}.AddPowerTargets() - WARNING encountered HistFullEntity where entity id={1} but there is no entity with that id", this, id));
					return false;
				}
				if (!entity.IsHero())
				{
					Debug.LogWarning(string.Format("{0}.AddPowerTargets() - WARNING HistFullEntity where entity id={1} is not a hero", this, id));
					return false;
				}
				this.AddTarget(entity.GetCard().gameObject);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x0004BCE0 File Offset: 0x00049EE0
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.StartCoroutine(this.SetupHero());
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x0004BCF6 File Offset: 0x00049EF6
	private IEnumerator SetupHero()
	{
		Card minionCard = base.GetSourceCard();
		Card heroCard = base.GetTargetCard();
		Entity heroEntity = heroCard.GetEntity();
		minionCard.SuppressDeathEffects(true);
		minionCard.GetActor().TurnOffCollider();
		this.FindFullEntityTask().DoTask();
		while (heroEntity.IsLoadingAssets())
		{
			yield return null;
		}
		heroCard.HideCard();
		Zone zone = ZoneMgr.Get().FindZoneForEntity(heroEntity);
		heroCard.TransitionToZone(zone, null);
		while (heroCard.IsActorLoading())
		{
			yield return null;
		}
		heroCard.GetActor().TurnOffCollider();
		base.StartCoroutine(this.PlaySummoningSpells(minionCard, heroCard));
		yield break;
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x0004BD08 File Offset: 0x00049F08
	private PowerTask FindFullEntityTask()
	{
		foreach (PowerTask powerTask in this.m_taskList.GetTaskList())
		{
			if (powerTask.GetPower().Type == Network.PowerType.FULL_ENTITY)
			{
				return powerTask;
			}
		}
		return null;
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x0004BD70 File Offset: 0x00049F70
	private void Finish()
	{
		base.GetSourceCard().GetActor().TurnOnCollider();
		Card targetCard = base.GetTargetCard();
		targetCard.GetActor().TurnOnCollider();
		targetCard.ActivateStateSpells(false);
		targetCard.ShowCard();
		this.OnSpellFinished();
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x0004BDA5 File Offset: 0x00049FA5
	private IEnumerator PlaySummoningSpells(Card minionCard, Card heroCard)
	{
		heroCard.transform.position = minionCard.transform.position;
		minionCard.ActivateActorSpell(SpellType.SUMMON_JARAXXUS);
		heroCard.ActivateActorSpell(SpellType.SUMMON_JARAXXUS);
		yield return new WaitForSeconds(this.m_MoveToLocationDelay);
		this.MoveToSpellLocation(minionCard, heroCard);
		yield return new WaitForSeconds(this.m_MoveToHeroSpotDelay);
		this.MoveToHeroSpot(minionCard, heroCard);
		yield break;
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x0004BDC4 File Offset: 0x00049FC4
	private void MoveToSpellLocation(Card minionCard, Card heroCard)
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			base.transform.position,
			"time",
			this.m_MoveToLocationDuration,
			"easetype",
			this.m_MoveToLocationEaseType
		});
		iTween.MoveTo(minionCard.gameObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"position",
			base.transform.position,
			"time",
			this.m_MoveToLocationDuration,
			"easetype",
			this.m_MoveToLocationEaseType
		});
		iTween.MoveTo(heroCard.gameObject, args2);
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x0004BE90 File Offset: 0x0004A090
	private void MoveToHeroSpot(Card minionCard, Card heroCard)
	{
		ZoneHero heroZone = heroCard.GetController().GetHeroZone();
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			heroZone.transform.position,
			"time",
			this.m_MoveToHeroSpotDuration,
			"easetype",
			this.m_MoveToHeroSpotEaseType
		});
		iTween.MoveTo(minionCard.gameObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"position",
			heroZone.transform.position,
			"time",
			this.m_MoveToHeroSpotDuration,
			"easetype",
			this.m_MoveToHeroSpotEaseType,
			"oncomplete",
			"OnMoveToHeroSpotComplete",
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(heroCard.gameObject, args2);
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x0004BF8A File Offset: 0x0004A18A
	private void OnMoveToHeroSpotComplete()
	{
		this.Finish();
	}

	// Token: 0x040008FD RID: 2301
	public float m_MoveToLocationDelay;

	// Token: 0x040008FE RID: 2302
	public float m_MoveToLocationDuration = 1.5f;

	// Token: 0x040008FF RID: 2303
	public iTween.EaseType m_MoveToLocationEaseType = iTween.EaseType.linear;

	// Token: 0x04000900 RID: 2304
	public float m_MoveToHeroSpotDelay = 3.5f;

	// Token: 0x04000901 RID: 2305
	public float m_MoveToHeroSpotDuration = 0.3f;

	// Token: 0x04000902 RID: 2306
	public iTween.EaseType m_MoveToHeroSpotEaseType = iTween.EaseType.linear;
}
