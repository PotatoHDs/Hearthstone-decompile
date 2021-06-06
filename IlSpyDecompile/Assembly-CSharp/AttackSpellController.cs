using System.Collections;
using UnityEngine;

public class AttackSpellController : SpellController
{
	public HeroAttackDef m_HeroInfo;

	public AllyAttackDef m_AllyInfo;

	public float m_ImpactStagingPoint = 1f;

	public float m_SourceImpactOffset = -0.25f;

	public SpellValueRange[] m_ImpactDefs;

	public Spell m_DefaultImpactSpellPrefab;

	private const float PROPOSED_ATTACK_IMPACT_POINT_SCALAR = 0.5f;

	private const float WINDFURY_REMINDER_WAIT_SEC = 1.2f;

	private AttackType m_attackType;

	private Spell m_sourceAttackSpell;

	private Vector3 m_sourcePos;

	private Vector3 m_sourceToTarget;

	private Vector3 m_sourceFacing;

	private bool m_repeatProposed;

	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		m_attackType = taskList.GetAttackType();
		m_repeatProposed = taskList.IsRepeatProposedAttack();
		if (m_attackType == AttackType.INVALID)
		{
			return false;
		}
		Entity attacker = taskList.GetAttacker();
		if (attacker != null)
		{
			SetSource(attacker.GetCard());
		}
		Entity defender = taskList.GetDefender();
		if (defender != null)
		{
			AddTarget(defender.GetCard());
		}
		return true;
	}

	protected override void OnProcessTaskList()
	{
		if (m_attackType == AttackType.ONLY_PROPOSED_ATTACKER || m_attackType == AttackType.ONLY_PROPOSED_DEFENDER || m_attackType == AttackType.ONLY_ATTACKER || m_attackType == AttackType.ONLY_DEFENDER || m_attackType == AttackType.WAITING_ON_PROPOSED_ATTACKER || m_attackType == AttackType.WAITING_ON_PROPOSED_DEFENDER || m_attackType == AttackType.WAITING_ON_ATTACKER || m_attackType == AttackType.WAITING_ON_DEFENDER)
		{
			FinishEverything();
			return;
		}
		if (m_repeatProposed)
		{
			FinishEverything();
			return;
		}
		Card source = GetSource();
		if (source == null || source.GetActor() == null)
		{
			FinishEverything();
			return;
		}
		Entity entity = source.GetEntity();
		if (entity == null)
		{
			FinishEverything();
			return;
		}
		Zone zone = source.GetZone();
		if (zone == null)
		{
			zone = ZoneMgr.Get().FindZoneOfType<ZonePlay>(source.GetControllerSide());
		}
		bool flag = zone.m_Side == Player.Side.FRIENDLY;
		m_sourceAttackSpell = GetSourceAttackSpell(source, flag);
		if (m_attackType == AttackType.CANCELED)
		{
			CancelAttackSpell(entity, m_sourceAttackSpell);
			source.SetDoNotSort(on: false);
			zone.UpdateLayout();
			source.EnableAttacking(enable: false);
			FinishEverything();
			return;
		}
		if (m_sourceAttackSpell == null)
		{
			FinishEverything();
			return;
		}
		source.EnableAttacking(enable: true);
		if (entity.GetTag(GAME_TAG.IMMUNE_WHILE_ATTACKING) != 0)
		{
			source.ActivateActorSpell(SpellType.IMMUNE);
		}
		else if (!source.ShouldShowImmuneVisuals())
		{
			SpellUtils.ActivateDeathIfNecessary(source.GetActor().GetSpellIfLoaded(SpellType.IMMUNE));
		}
		m_sourceAttackSpell.AddStateStartedCallback(OnSourceAttackStateStarted);
		if (flag)
		{
			if (m_sourceAttackSpell.GetActiveState() != SpellStateType.IDLE)
			{
				m_sourceAttackSpell.ActivateState(SpellStateType.BIRTH);
			}
			else
			{
				m_sourceAttackSpell.ActivateState(SpellStateType.ACTION);
			}
		}
		else if (m_sourceAttackSpell.GetActiveState() != SpellStateType.IDLE)
		{
			m_sourceAttackSpell.ActivateState(SpellStateType.BIRTH);
		}
		else
		{
			m_sourceAttackSpell.ActivateState(SpellStateType.ACTION);
		}
	}

	private void OnSourceAttackStateStarted(Spell spell, SpellStateType prevStateType, object userData)
	{
		switch (spell.GetActiveState())
		{
		case SpellStateType.IDLE:
			spell.ActivateState(SpellStateType.ACTION);
			break;
		case SpellStateType.ACTION:
			spell.RemoveStateStartedCallback(OnSourceAttackStateStarted);
			LaunchAttack();
			break;
		}
	}

	private void LaunchAttack()
	{
		Card source = GetSource();
		Entity entity = source.GetEntity();
		Card target = GetTarget();
		bool flag = m_attackType == AttackType.PROPOSED;
		if (flag && entity.IsHero())
		{
			m_sourceAttackSpell.ActivateState(SpellStateType.IDLE);
			FinishEverything();
			return;
		}
		m_sourcePos = source.transform.position;
		m_sourceToTarget = target.transform.position - m_sourcePos;
		Vector3 impactPos = ComputeImpactPos();
		source.SetDoNotSort(on: true);
		MoveSourceToTarget(source, entity, impactPos);
		if (entity.IsHero())
		{
			OrientSourceHeroToTarget(source);
		}
		if (!flag)
		{
			target.SetDoNotSort(on: true);
			MoveTargetToSource(target, entity, impactPos);
		}
	}

	private bool HasFinishAttackSpellOnDamage()
	{
		Card source = GetSource();
		if (source == null)
		{
			return false;
		}
		Entity entity = source.GetEntity();
		if (entity == null)
		{
			return false;
		}
		if (!entity.IsHero())
		{
			return entity.HasTag(GAME_TAG.FINISH_ATTACK_SPELL_ON_DAMAGE);
		}
		Player controller = entity.GetController();
		if (controller == null)
		{
			return false;
		}
		Card weaponCard = controller.GetWeaponCard();
		if (weaponCard == null)
		{
			return false;
		}
		return weaponCard.GetEntity()?.HasTag(GAME_TAG.FINISH_ATTACK_SPELL_ON_DAMAGE) ?? false;
	}

	private void UpdateTargetOnMoveToTargetFinished(Card targetCard)
	{
		targetCard.SetDoNotSort(on: false);
		Zone zone = targetCard.GetZone();
		if (zone == null)
		{
			zone = targetCard.GetPrevZone();
			if (!targetCard.GetEntity().IsHero())
			{
				Log.Spells.PrintWarning("AttackSpellController.UpdateTargetOnMoveToTargetFinished() - Non-hero target ({0}) was moved from {1} to SETASIDE before the attack was resolved.", targetCard.name, zone.name);
			}
		}
		zone.UpdateLayout();
	}

	private void OnMoveToTargetFinished()
	{
		Card source = GetSource();
		Entity entity = source.GetEntity();
		Card target = GetTarget();
		bool flag = m_attackType == AttackType.PROPOSED;
		DoTasks(source, target);
		if (!flag)
		{
			ActivateImpactEffects(source, target);
		}
		if (entity.IsHero())
		{
			MoveSourceHeroBack(source);
			OrientSourceHeroBack(source);
			UpdateTargetOnMoveToTargetFinished(target);
			if (HasFinishAttackSpellOnDamage())
			{
				FinishHeroAttack();
			}
			return;
		}
		if (flag)
		{
			FinishEverything();
			return;
		}
		source.SetDoNotSort(on: false);
		source.GetZone().UpdateLayout();
		UpdateTargetOnMoveToTargetFinished(target);
		if (HasFinishAttackSpellOnDamage())
		{
			FinishAttackSpellController();
		}
		else
		{
			m_sourceAttackSpell.AddStateFinishedCallback(OnMinionSourceAttackStateFinished);
		}
		m_sourceAttackSpell.ActivateState(SpellStateType.DEATH);
	}

	private void DoTasks(Card sourceCard, Card targetCard)
	{
		GameUtils.DoDamageTasks(m_taskList, sourceCard, targetCard);
	}

	private void MoveSourceHeroBack(Card sourceCard)
	{
		Hashtable args = iTween.Hash("position", m_sourcePos, "time", m_HeroInfo.m_MoveBackDuration, "easetype", m_HeroInfo.m_MoveBackEaseType, "oncomplete", "OnHeroMoveBackFinished", "oncompletetarget", base.gameObject);
		iTween.MoveTo(sourceCard.gameObject, args);
	}

	private void OrientSourceHeroBack(Card sourceCard)
	{
		Quaternion quaternion = Quaternion.LookRotation(m_sourceFacing);
		Hashtable args = iTween.Hash("rotation", quaternion.eulerAngles, "time", m_HeroInfo.m_OrientBackDuration, "easetype", m_HeroInfo.m_OrientBackEaseType);
		iTween.RotateTo(sourceCard.gameObject, args);
	}

	private void OnHeroMoveBackFinished()
	{
		Card source = GetSource();
		Entity entity = source.GetEntity();
		source.SetDoNotSort(on: false);
		source.EnableAttacking(enable: false);
		if (!HasFinishAttackSpellOnDamage())
		{
			if (entity.GetController().IsLocalUser() || m_sourceAttackSpell.GetActiveState() == SpellStateType.NONE)
			{
				FinishHeroAttack();
			}
			else
			{
				m_sourceAttackSpell.AddStateFinishedCallback(OnHeroSourceAttackStateFinished);
			}
		}
	}

	private void OnHeroSourceAttackStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			spell.RemoveStateFinishedCallback(OnHeroSourceAttackStateFinished);
			FinishHeroAttack();
		}
	}

	private void FinishHeroAttack()
	{
		Card source = GetSource();
		Entity entity = source.GetEntity();
		PlayWindfuryReminderIfPossible(entity, source);
		FinishEverything();
	}

	private void OnMinionSourceAttackStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			spell.RemoveStateFinishedCallback(OnMinionSourceAttackStateFinished);
			FinishAttackSpellController();
		}
	}

	private void FinishAttackSpellController()
	{
		Card source = GetSource();
		Entity entity = source.GetEntity();
		source.EnableAttacking(enable: false);
		if (!CanPlayWindfuryReminder(entity, source))
		{
			FinishEverything();
			return;
		}
		OnFinishedTaskList();
		StartCoroutine(WaitThenPlayWindfuryReminder(entity, source));
	}

	private void FinishEverything()
	{
		Card source = GetSource();
		if (source != null && !source.ShouldShowImmuneVisuals() && (source.GetEntity() == null || !source.GetEntity().HasTag(GAME_TAG.IMMUNE_WHILE_ATTACKING) || m_attackType != AttackType.PROPOSED))
		{
			source.GetActor().ActivateSpellDeathState(SpellType.IMMUNE);
		}
		OnFinishedTaskList();
		OnFinished();
	}

	private IEnumerator WaitThenPlayWindfuryReminder(Entity entity, Card card)
	{
		yield return new WaitForSeconds(1.2f);
		PlayWindfuryReminderIfPossible(entity, card);
		OnFinished();
	}

	private bool CanPlayWindfuryReminder(Entity entity, Card card)
	{
		if (!entity.HasWindfury())
		{
			return false;
		}
		if (entity.IsExhausted())
		{
			return false;
		}
		if (entity.GetZone() != TAG_ZONE.PLAY)
		{
			return false;
		}
		if (!entity.GetController().IsCurrentPlayer())
		{
			return false;
		}
		if (card.GetActorSpell(SpellType.WINDFURY_BURST) == null)
		{
			return false;
		}
		return true;
	}

	private void PlayWindfuryReminderIfPossible(Entity entity, Card card)
	{
		if (CanPlayWindfuryReminder(entity, card))
		{
			card.ActivateActorSpell(SpellType.WINDFURY_BURST);
		}
	}

	private void MoveSourceToTarget(Card sourceCard, Entity sourceEntity, Vector3 impactPos)
	{
		Vector3 vector = ComputeImpactOffset(sourceCard, impactPos);
		Vector3 vector2 = impactPos + vector;
		float num = 0f;
		iTween.EaseType easeType = iTween.EaseType.linear;
		if (sourceEntity.IsHero())
		{
			num = m_HeroInfo.m_MoveToTargetDuration;
			easeType = m_HeroInfo.m_MoveToTargetEaseType;
		}
		else
		{
			num = m_AllyInfo.m_MoveToTargetDuration;
			easeType = m_AllyInfo.m_MoveToTargetEaseType;
		}
		Hashtable args = iTween.Hash("position", vector2, "time", num, "easetype", easeType, "oncomplete", "OnMoveToTargetFinished", "oncompletetarget", base.gameObject);
		iTween.MoveTo(sourceCard.gameObject, args);
	}

	private void OrientSourceHeroToTarget(Card sourceCard)
	{
		m_sourceFacing = sourceCard.transform.forward;
		Quaternion quaternion = ((m_sourceAttackSpell.GetSpellType() != SpellType.OPPONENT_ATTACK) ? Quaternion.LookRotation(m_sourceToTarget) : Quaternion.LookRotation(-m_sourceToTarget));
		Hashtable args = iTween.Hash("rotation", quaternion.eulerAngles, "time", m_HeroInfo.m_OrientToTargetDuration, "easetype", m_HeroInfo.m_OrientToTargetEaseType);
		iTween.RotateTo(sourceCard.gameObject, args);
	}

	private void MoveTargetToSource(Card targetCard, Entity sourceEntity, Vector3 impactPos)
	{
		float num = 0f;
		iTween.EaseType easeType = iTween.EaseType.linear;
		if (sourceEntity.IsHero())
		{
			num = m_HeroInfo.m_MoveToTargetDuration;
			easeType = m_HeroInfo.m_MoveToTargetEaseType;
		}
		else
		{
			num = m_AllyInfo.m_MoveToTargetDuration;
			easeType = m_AllyInfo.m_MoveToTargetEaseType;
		}
		Hashtable args = iTween.Hash("position", impactPos, "time", num, "easetype", easeType);
		iTween.MoveTo(targetCard.gameObject, args);
	}

	private Vector3 ComputeImpactPos()
	{
		float num = 1f;
		if (m_attackType == AttackType.PROPOSED)
		{
			num = 0.5f;
		}
		Vector3 vector = num * m_ImpactStagingPoint * m_sourceToTarget;
		return m_sourcePos + vector;
	}

	private Vector3 ComputeImpactOffset(Card sourceCard, Vector3 impactPos)
	{
		if (Mathf.Approximately(m_SourceImpactOffset, 0.5f))
		{
			return Vector3.zero;
		}
		if (sourceCard.GetActor().GetMeshRenderer() == null)
		{
			return Vector3.zero;
		}
		Bounds bounds = sourceCard.GetActor().GetMeshRenderer().bounds;
		bounds.center = m_sourcePos;
		Ray ray = new Ray(impactPos, bounds.center - impactPos);
		if (!bounds.IntersectRay(ray, out var distance))
		{
			return Vector3.zero;
		}
		Vector3 vector = ray.origin + distance * ray.direction;
		Vector3 vector2 = 2f * bounds.center - vector - vector;
		return 0.5f * vector2 - m_SourceImpactOffset * vector2;
	}

	private void ActivateImpactEffects(Card sourceCard, Card targetCard)
	{
		Spell spell = DetermineImpactSpellPrefab(sourceCard);
		if (!(spell == null))
		{
			Spell spell2 = Object.Instantiate(spell);
			spell2.SetSource(sourceCard.gameObject);
			spell2.AddTarget(targetCard.gameObject);
			Vector3 position = targetCard.transform.position;
			spell2.SetPosition(position);
			Quaternion orientation = Quaternion.LookRotation(m_sourceToTarget);
			spell2.SetOrientation(orientation);
			spell2.AddStateFinishedCallback(OnImpactSpellStateFinished);
			spell2.Activate();
		}
	}

	private Spell DetermineImpactSpellPrefab(Card sourceCard)
	{
		int aTK = sourceCard.GetEntity().GetATK();
		SpellValueRange appropriateElementAccordingToRanges = SpellUtils.GetAppropriateElementAccordingToRanges(m_ImpactDefs, (SpellValueRange x) => x.m_range, aTK);
		if (appropriateElementAccordingToRanges != null && appropriateElementAccordingToRanges.m_spellPrefab != null)
		{
			return appropriateElementAccordingToRanges.m_spellPrefab;
		}
		return m_DefaultImpactSpellPrefab;
	}

	private void OnImpactSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			Object.Destroy(spell.gameObject);
		}
	}

	protected override float GetLostFrameTimeCatchUpSeconds()
	{
		Card source = GetSource();
		if (source != null && source.GetEntity() != null && source.GetEntity().IsHero())
		{
			return 0f;
		}
		Card target = GetTarget();
		if (target != null && target.GetEntity() != null && target.GetEntity().IsHero())
		{
			return 0f;
		}
		return 0.8f;
	}

	protected override void OnFinishedTaskList()
	{
		if (m_attackType != AttackType.PROPOSED)
		{
			Card source = GetSource();
			source.SetDoNotSort(on: false);
			if (!source.GetEntity().IsHero())
			{
				Zone zone = source.GetZone();
				zone.UpdateLayout();
				if (m_sourceAttackSpell == null)
				{
					bool isSourceFriendly = zone.m_Side == Player.Side.FRIENDLY;
					m_sourceAttackSpell = GetSourceAttackSpell(source, isSourceFriendly);
				}
				if (m_sourceAttackSpell != null && (m_sourceAttackSpell.GetActiveState() == SpellStateType.BIRTH || m_sourceAttackSpell.GetActiveState() == SpellStateType.IDLE || m_sourceAttackSpell.GetActiveState() == SpellStateType.ACTION))
				{
					CancelAttackSpell(source.GetEntity(), m_sourceAttackSpell);
				}
			}
		}
		base.OnFinishedTaskList();
	}

	private void CancelAttackSpell(Entity sourceEntity, Spell attackSpell)
	{
		if (!(attackSpell == null))
		{
			if (sourceEntity == null)
			{
				attackSpell.ActivateState(SpellStateType.DEATH);
			}
			else if (sourceEntity.IsHero())
			{
				attackSpell.ActivateState(SpellStateType.CANCEL);
			}
			else
			{
				attackSpell.ActivateState(SpellStateType.DEATH);
			}
		}
	}

	private Spell GetSourceAttackSpell(Card sourceCard, bool isSourceFriendly)
	{
		if (GameState.Get().GetGameEntity().HasTag(GAME_TAG.BACON_HIGHLIGHT_ATTACKING_MINION_DURING_COMBAT))
		{
			Spell actorSpell = sourceCard.GetActorSpell(SpellType.AUTO_ATTACK_WITH_HIGHLIGHT);
			if (actorSpell != null)
			{
				return actorSpell;
			}
			return null;
		}
		if (isSourceFriendly)
		{
			return sourceCard.GetActorSpell(SpellType.FRIENDLY_ATTACK);
		}
		return sourceCard.GetActorSpell(SpellType.OPPONENT_ATTACK);
	}
}
