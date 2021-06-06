using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007F9 RID: 2041
public class HeroSwapSpell : Spell
{
	// Token: 0x06006EEF RID: 28399 RVA: 0x0023BE34 File Offset: 0x0023A034
	public override bool AddPowerTargets()
	{
		KAR12_Portals kar12_Portals = GameState.Get().GetGameEntity() as KAR12_Portals;
		if (kar12_Portals != null && kar12_Portals.ShouldPlayLongMidmissionCutscene())
		{
			this.m_OldHeroFXToUse = this.m_OldHeroFX_long;
			this.m_NewHeroFXToUse = this.m_NewHeroFX_long;
		}
		else
		{
			this.m_OldHeroFXToUse = this.m_OldHeroFX;
			this.m_NewHeroFXToUse = this.m_NewHeroFX;
		}
		this.m_oldHeroCard = null;
		this.m_newHeroCard = null;
		foreach (PowerTask powerTask in this.m_taskList.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.FULL_ENTITY)
			{
				int id = ((Network.HistFullEntity)power).Entity.ID;
				Entity entity = GameState.Get().GetEntity(id);
				if (entity == null)
				{
					Debug.LogWarning(string.Format("{0}.AddPowerTargets() - WARNING encountered HistFullEntity where entity id={1} but there is no entity with that id", this, id));
					return false;
				}
				if (entity.IsHero())
				{
					this.m_newHeroCard = entity.GetCard();
				}
			}
			else if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)power;
				if (histTagChange.Tag == 49 && histTagChange.Value == 6)
				{
					int entity2 = histTagChange.Entity;
					Entity entity3 = GameState.Get().GetEntity(entity2);
					if (entity3 == null)
					{
						Debug.LogWarning(string.Format("{0}.AddPowerTargets() - WARNING encountered HistTagChange where entity id={1} but there is no entity with that id", this, entity2));
						return false;
					}
					if (entity3.IsHero())
					{
						this.m_oldHeroCard = entity3.GetCard();
					}
				}
			}
		}
		if (this.m_newHeroCard != null && this.m_oldHeroCard == null)
		{
			Player controller = this.m_newHeroCard.GetController();
			if (controller != null)
			{
				this.m_oldHeroCard = controller.GetHeroCard();
			}
		}
		if (!this.m_oldHeroCard)
		{
			this.m_newHeroCard = null;
			return false;
		}
		if (!this.m_newHeroCard)
		{
			this.m_oldHeroCard = null;
			return false;
		}
		return true;
	}

	// Token: 0x06006EF0 RID: 28400 RVA: 0x0023C030 File Offset: 0x0023A230
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.StartCoroutine(this.SetupHero());
	}

	// Token: 0x06006EF1 RID: 28401 RVA: 0x0023C046 File Offset: 0x0023A246
	private IEnumerator SetupHero()
	{
		Entity newHeroEntity = this.m_newHeroCard.GetEntity();
		this.FindFullEntityTask().DoTask();
		while (newHeroEntity.IsLoadingAssets())
		{
			yield return null;
		}
		this.m_newHeroCard.HideCard();
		Zone zone = ZoneMgr.Get().FindZoneForEntity(newHeroEntity);
		this.m_newHeroCard.TransitionToZone(zone, null);
		while (this.m_newHeroCard.IsActorLoading())
		{
			yield return null;
		}
		this.m_newHeroCard.GetActor().TurnOffCollider();
		this.m_newHeroCard.transform.position = this.m_newHeroCard.GetZone().transform.position;
		if (this.m_OldHeroFXToUse != null)
		{
			if (this.removeOldStats)
			{
				Actor actor = this.m_oldHeroCard.GetActor();
				UnityEngine.Object.Destroy(actor.m_healthObject);
				UnityEngine.Object.Destroy(actor.m_attackObject);
			}
			base.StartCoroutine(this.PlaySwapFx(this.m_OldHeroFXToUse, this.m_oldHeroCard));
		}
		if (this.m_NewHeroFXToUse != null)
		{
			base.StartCoroutine(this.PlaySwapFx(this.m_NewHeroFXToUse, this.m_newHeroCard));
		}
		yield return new WaitForSeconds(this.m_FinishDelay);
		this.Finish();
		yield break;
	}

	// Token: 0x06006EF2 RID: 28402 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void CustomizeFXProcess(Actor heroActor)
	{
	}

	// Token: 0x06006EF3 RID: 28403 RVA: 0x0023C055 File Offset: 0x0023A255
	private IEnumerator PlaySwapFx(Spell heroFX, Card heroCard)
	{
		Actor actor = heroCard.GetActor();
		this.CustomizeFXProcess(actor);
		Spell swapSpell = UnityEngine.Object.Instantiate<Spell>(heroFX);
		SpellUtils.SetCustomSpellParent(swapSpell, actor);
		swapSpell.SetSource(heroCard.gameObject);
		swapSpell.Activate();
		while (!swapSpell.IsFinished())
		{
			yield return null;
		}
		while (swapSpell.GetActiveState() != SpellStateType.NONE)
		{
			yield return null;
		}
		UnityEngine.Object.Destroy(swapSpell.gameObject);
		yield break;
	}

	// Token: 0x06006EF4 RID: 28404 RVA: 0x0023C074 File Offset: 0x0023A274
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

	// Token: 0x06006EF5 RID: 28405 RVA: 0x0023C0DC File Offset: 0x0023A2DC
	private void Finish()
	{
		this.m_newHeroCard.GetActor().TurnOnCollider();
		this.m_newHeroCard.ActivateStateSpells(false);
		this.m_newHeroCard.ShowCard();
		this.OnSpellFinished();
	}

	// Token: 0x040058F6 RID: 22774
	public Spell m_OldHeroFX;

	// Token: 0x040058F7 RID: 22775
	public Spell m_NewHeroFX;

	// Token: 0x040058F8 RID: 22776
	public Spell m_OldHeroFX_long;

	// Token: 0x040058F9 RID: 22777
	public Spell m_NewHeroFX_long;

	// Token: 0x040058FA RID: 22778
	public float m_FinishDelay;

	// Token: 0x040058FB RID: 22779
	public bool removeOldStats;

	// Token: 0x040058FC RID: 22780
	protected Card m_oldHeroCard;

	// Token: 0x040058FD RID: 22781
	protected Card m_newHeroCard;

	// Token: 0x040058FE RID: 22782
	protected Spell m_OldHeroFXToUse;

	// Token: 0x040058FF RID: 22783
	protected Spell m_NewHeroFXToUse;
}
