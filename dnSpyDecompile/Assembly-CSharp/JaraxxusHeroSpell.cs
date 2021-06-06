using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000DE RID: 222
public class JaraxxusHeroSpell : Spell
{
	// Token: 0x06000D13 RID: 3347 RVA: 0x0004B998 File Offset: 0x00049B98
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
				if (entity.IsHeroPower())
				{
					this.m_heroPowerTask = powerTask;
					this.AddTarget(entity.GetCard().gameObject);
					if (this.m_weaponTask != null)
					{
						return true;
					}
				}
				else if (entity.IsWeapon())
				{
					this.m_weaponTask = powerTask;
					this.AddTarget(entity.GetCard().gameObject);
					if (this.m_heroPowerTask != null)
					{
						return true;
					}
				}
			}
		}
		this.Reset();
		return false;
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x0004BAAC File Offset: 0x00049CAC
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.StartCoroutine(this.SetupCards());
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x0004BAC2 File Offset: 0x00049CC2
	private IEnumerator SetupCards()
	{
		Entity heroPower = this.LoadCardFromTask(this.m_heroPowerTask);
		Entity weapon = this.LoadCardFromTask(this.m_weaponTask);
		while (heroPower.IsLoadingAssets() || weapon.IsLoadingAssets())
		{
			yield return null;
		}
		Card heroPowerCard = heroPower.GetCard();
		heroPowerCard.HideCard();
		Zone zone = ZoneMgr.Get().FindZoneForEntity(heroPower);
		heroPowerCard.TransitionToZone(zone, null);
		Card weaponCard = weapon.GetCard();
		weaponCard.HideCard();
		Zone zone2 = ZoneMgr.Get().FindZoneForEntity(weapon);
		weaponCard.TransitionToZone(zone2, null);
		while (heroPowerCard.IsActorLoading() || weaponCard.IsActorLoading())
		{
			yield return null;
		}
		this.PlayCardSpells(heroPowerCard, weaponCard);
		yield break;
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x0004BAD4 File Offset: 0x00049CD4
	private Entity LoadCardFromTask(PowerTask task)
	{
		Network.Entity entity = (task.GetPower() as Network.HistFullEntity).Entity;
		int id = entity.ID;
		Entity entity2 = GameState.Get().GetEntity(id);
		entity2.LoadCard(entity.CardID, null);
		return entity2;
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x0004BB14 File Offset: 0x00049D14
	private Card GetCardFromTask(PowerTask task)
	{
		int id = (task.GetPower() as Network.HistFullEntity).Entity.ID;
		return GameState.Get().GetEntity(id).GetCard();
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x0004BB47 File Offset: 0x00049D47
	private void Reset()
	{
		this.m_heroPowerTask = null;
		this.m_weaponTask = null;
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x0004BB57 File Offset: 0x00049D57
	private void Finish()
	{
		this.Reset();
		this.OnSpellFinished();
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x0004BB65 File Offset: 0x00049D65
	private void PlayCardSpells(Card heroPowerCard, Card weaponCard)
	{
		heroPowerCard.ShowCard();
		heroPowerCard.ActivateStateSpells(false);
		heroPowerCard.ActivateActorSpell(SpellType.SUMMON_JARAXXUS, new Spell.FinishedCallback(this.OnSpellFinished_HeroPower));
		weaponCard.ActivateActorSpell(SpellType.SUMMON_JARAXXUS, new Spell.FinishedCallback(this.OnSpellFinished_Weapon));
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x0004BB9E File Offset: 0x00049D9E
	private void OnSpellFinished_HeroPower(Spell spell, object userData)
	{
		this.m_heroPowerTask.SetCompleted(true);
		if (this.m_weaponTask.IsCompleted())
		{
			this.Finish();
		}
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x0004BBBF File Offset: 0x00049DBF
	private void OnSpellFinished_Weapon(Spell spell, object userData)
	{
		Card cardFromTask = this.GetCardFromTask(this.m_weaponTask);
		cardFromTask.ShowCard();
		cardFromTask.ActivateStateSpells(false);
		this.m_weaponTask.SetCompleted(true);
		if (this.m_heroPowerTask.IsCompleted())
		{
			this.Finish();
		}
	}

	// Token: 0x040008FB RID: 2299
	private PowerTask m_heroPowerTask;

	// Token: 0x040008FC RID: 2300
	private PowerTask m_weaponTask;
}
