using System.Collections;
using UnityEngine;

public class HeroSwapSpell : Spell
{
	public Spell m_OldHeroFX;

	public Spell m_NewHeroFX;

	public Spell m_OldHeroFX_long;

	public Spell m_NewHeroFX_long;

	public float m_FinishDelay;

	public bool removeOldStats;

	protected Card m_oldHeroCard;

	protected Card m_newHeroCard;

	protected Spell m_OldHeroFXToUse;

	protected Spell m_NewHeroFXToUse;

	public override bool AddPowerTargets()
	{
		KAR12_Portals kAR12_Portals = GameState.Get().GetGameEntity() as KAR12_Portals;
		if (kAR12_Portals != null && kAR12_Portals.ShouldPlayLongMidmissionCutscene())
		{
			m_OldHeroFXToUse = m_OldHeroFX_long;
			m_NewHeroFXToUse = m_NewHeroFX_long;
		}
		else
		{
			m_OldHeroFXToUse = m_OldHeroFX;
			m_NewHeroFXToUse = m_NewHeroFX;
		}
		m_oldHeroCard = null;
		m_newHeroCard = null;
		foreach (PowerTask task in m_taskList.GetTaskList())
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type == Network.PowerType.FULL_ENTITY)
			{
				int iD = ((Network.HistFullEntity)power).Entity.ID;
				Entity entity = GameState.Get().GetEntity(iD);
				if (entity == null)
				{
					Debug.LogWarning($"{this}.AddPowerTargets() - WARNING encountered HistFullEntity where entity id={iD} but there is no entity with that id");
					return false;
				}
				if (entity.IsHero())
				{
					m_newHeroCard = entity.GetCard();
				}
			}
			else
			{
				if (power.Type != Network.PowerType.TAG_CHANGE)
				{
					continue;
				}
				Network.HistTagChange histTagChange = (Network.HistTagChange)power;
				if (histTagChange.Tag == 49 && histTagChange.Value == 6)
				{
					int entity2 = histTagChange.Entity;
					Entity entity3 = GameState.Get().GetEntity(entity2);
					if (entity3 == null)
					{
						Debug.LogWarning($"{this}.AddPowerTargets() - WARNING encountered HistTagChange where entity id={entity2} but there is no entity with that id");
						return false;
					}
					if (entity3.IsHero())
					{
						m_oldHeroCard = entity3.GetCard();
					}
				}
			}
		}
		if (m_newHeroCard != null && m_oldHeroCard == null)
		{
			Player controller = m_newHeroCard.GetController();
			if (controller != null)
			{
				m_oldHeroCard = controller.GetHeroCard();
			}
		}
		if (!m_oldHeroCard)
		{
			m_newHeroCard = null;
			return false;
		}
		if (!m_newHeroCard)
		{
			m_oldHeroCard = null;
			return false;
		}
		return true;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		StartCoroutine(SetupHero());
	}

	private IEnumerator SetupHero()
	{
		Entity newHeroEntity = m_newHeroCard.GetEntity();
		FindFullEntityTask().DoTask();
		while (newHeroEntity.IsLoadingAssets())
		{
			yield return null;
		}
		m_newHeroCard.HideCard();
		Zone zone = ZoneMgr.Get().FindZoneForEntity(newHeroEntity);
		m_newHeroCard.TransitionToZone(zone);
		while (m_newHeroCard.IsActorLoading())
		{
			yield return null;
		}
		m_newHeroCard.GetActor().TurnOffCollider();
		m_newHeroCard.transform.position = m_newHeroCard.GetZone().transform.position;
		if (m_OldHeroFXToUse != null)
		{
			if (removeOldStats)
			{
				Actor actor = m_oldHeroCard.GetActor();
				Object.Destroy(actor.m_healthObject);
				Object.Destroy(actor.m_attackObject);
			}
			StartCoroutine(PlaySwapFx(m_OldHeroFXToUse, m_oldHeroCard));
		}
		if (m_NewHeroFXToUse != null)
		{
			StartCoroutine(PlaySwapFx(m_NewHeroFXToUse, m_newHeroCard));
		}
		yield return new WaitForSeconds(m_FinishDelay);
		Finish();
	}

	public virtual void CustomizeFXProcess(Actor heroActor)
	{
	}

	private IEnumerator PlaySwapFx(Spell heroFX, Card heroCard)
	{
		Actor actor = heroCard.GetActor();
		CustomizeFXProcess(actor);
		Spell swapSpell = Object.Instantiate(heroFX);
		SpellUtils.SetCustomSpellParent(swapSpell, actor);
		swapSpell.SetSource(heroCard.gameObject);
		swapSpell.Activate();
		while (!swapSpell.IsFinished())
		{
			yield return null;
		}
		while (swapSpell.GetActiveState() != 0)
		{
			yield return null;
		}
		Object.Destroy(swapSpell.gameObject);
	}

	private PowerTask FindFullEntityTask()
	{
		foreach (PowerTask task in m_taskList.GetTaskList())
		{
			if (task.GetPower().Type == Network.PowerType.FULL_ENTITY)
			{
				return task;
			}
		}
		return null;
	}

	private void Finish()
	{
		m_newHeroCard.GetActor().TurnOnCollider();
		m_newHeroCard.ActivateStateSpells();
		m_newHeroCard.ShowCard();
		OnSpellFinished();
	}
}
