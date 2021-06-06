using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006C4 RID: 1732
public class AttackSpellController : SpellController
{
	// Token: 0x06006104 RID: 24836 RVA: 0x001FA0C8 File Offset: 0x001F82C8
	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		this.m_attackType = taskList.GetAttackType();
		this.m_repeatProposed = taskList.IsRepeatProposedAttack();
		if (this.m_attackType == AttackType.INVALID)
		{
			return false;
		}
		Entity attacker = taskList.GetAttacker();
		if (attacker != null)
		{
			base.SetSource(attacker.GetCard());
		}
		Entity defender = taskList.GetDefender();
		if (defender != null)
		{
			base.AddTarget(defender.GetCard());
		}
		return true;
	}

	// Token: 0x06006105 RID: 24837 RVA: 0x001FA124 File Offset: 0x001F8324
	protected override void OnProcessTaskList()
	{
		if (this.m_attackType == AttackType.ONLY_PROPOSED_ATTACKER || this.m_attackType == AttackType.ONLY_PROPOSED_DEFENDER || this.m_attackType == AttackType.ONLY_ATTACKER || this.m_attackType == AttackType.ONLY_DEFENDER || this.m_attackType == AttackType.WAITING_ON_PROPOSED_ATTACKER || this.m_attackType == AttackType.WAITING_ON_PROPOSED_DEFENDER || this.m_attackType == AttackType.WAITING_ON_ATTACKER || this.m_attackType == AttackType.WAITING_ON_DEFENDER)
		{
			this.FinishEverything();
			return;
		}
		if (this.m_repeatProposed)
		{
			this.FinishEverything();
			return;
		}
		Card source = base.GetSource();
		if (source == null || source.GetActor() == null)
		{
			this.FinishEverything();
			return;
		}
		Entity entity = source.GetEntity();
		if (entity == null)
		{
			this.FinishEverything();
			return;
		}
		Zone zone = source.GetZone();
		if (zone == null)
		{
			zone = ZoneMgr.Get().FindZoneOfType<ZonePlay>(source.GetControllerSide());
		}
		bool flag = zone.m_Side == Player.Side.FRIENDLY;
		this.m_sourceAttackSpell = this.GetSourceAttackSpell(source, flag);
		if (this.m_attackType == AttackType.CANCELED)
		{
			this.CancelAttackSpell(entity, this.m_sourceAttackSpell);
			source.SetDoNotSort(false);
			zone.UpdateLayout();
			source.EnableAttacking(false);
			this.FinishEverything();
			return;
		}
		if (this.m_sourceAttackSpell == null)
		{
			this.FinishEverything();
			return;
		}
		source.EnableAttacking(true);
		if (entity.GetTag(GAME_TAG.IMMUNE_WHILE_ATTACKING) != 0)
		{
			source.ActivateActorSpell(SpellType.IMMUNE);
		}
		else if (!source.ShouldShowImmuneVisuals())
		{
			SpellUtils.ActivateDeathIfNecessary(source.GetActor().GetSpellIfLoaded(SpellType.IMMUNE));
		}
		this.m_sourceAttackSpell.AddStateStartedCallback(new Spell.StateStartedCallback(this.OnSourceAttackStateStarted));
		if (flag)
		{
			if (this.m_sourceAttackSpell.GetActiveState() != SpellStateType.IDLE)
			{
				this.m_sourceAttackSpell.ActivateState(SpellStateType.BIRTH);
				return;
			}
			this.m_sourceAttackSpell.ActivateState(SpellStateType.ACTION);
			return;
		}
		else
		{
			if (this.m_sourceAttackSpell.GetActiveState() != SpellStateType.IDLE)
			{
				this.m_sourceAttackSpell.ActivateState(SpellStateType.BIRTH);
				return;
			}
			this.m_sourceAttackSpell.ActivateState(SpellStateType.ACTION);
			return;
		}
	}

	// Token: 0x06006106 RID: 24838 RVA: 0x001FA2EC File Offset: 0x001F84EC
	private void OnSourceAttackStateStarted(Spell spell, SpellStateType prevStateType, object userData)
	{
		SpellStateType activeState = spell.GetActiveState();
		if (activeState == SpellStateType.IDLE)
		{
			spell.ActivateState(SpellStateType.ACTION);
			return;
		}
		if (activeState == SpellStateType.ACTION)
		{
			spell.RemoveStateStartedCallback(new Spell.StateStartedCallback(this.OnSourceAttackStateStarted));
			this.LaunchAttack();
		}
	}

	// Token: 0x06006107 RID: 24839 RVA: 0x001FA32C File Offset: 0x001F852C
	private void LaunchAttack()
	{
		Card source = base.GetSource();
		Entity entity = source.GetEntity();
		Card target = base.GetTarget();
		bool flag = this.m_attackType == AttackType.PROPOSED;
		if (flag && entity.IsHero())
		{
			this.m_sourceAttackSpell.ActivateState(SpellStateType.IDLE);
			this.FinishEverything();
			return;
		}
		this.m_sourcePos = source.transform.position;
		this.m_sourceToTarget = target.transform.position - this.m_sourcePos;
		Vector3 impactPos = this.ComputeImpactPos();
		source.SetDoNotSort(true);
		this.MoveSourceToTarget(source, entity, impactPos);
		if (entity.IsHero())
		{
			this.OrientSourceHeroToTarget(source);
		}
		if (flag)
		{
			return;
		}
		target.SetDoNotSort(true);
		this.MoveTargetToSource(target, entity, impactPos);
	}

	// Token: 0x06006108 RID: 24840 RVA: 0x001FA3E0 File Offset: 0x001F85E0
	private bool HasFinishAttackSpellOnDamage()
	{
		Card source = base.GetSource();
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
		Entity entity2 = weaponCard.GetEntity();
		return entity2 != null && entity2.HasTag(GAME_TAG.FINISH_ATTACK_SPELL_ON_DAMAGE);
	}

	// Token: 0x06006109 RID: 24841 RVA: 0x001FA458 File Offset: 0x001F8658
	private void UpdateTargetOnMoveToTargetFinished(Card targetCard)
	{
		targetCard.SetDoNotSort(false);
		Zone zone = targetCard.GetZone();
		if (zone == null)
		{
			zone = targetCard.GetPrevZone();
			if (!targetCard.GetEntity().IsHero())
			{
				Log.Spells.PrintWarning("AttackSpellController.UpdateTargetOnMoveToTargetFinished() - Non-hero target ({0}) was moved from {1} to SETASIDE before the attack was resolved.", new object[]
				{
					targetCard.name,
					zone.name
				});
			}
		}
		zone.UpdateLayout();
	}

	// Token: 0x0600610A RID: 24842 RVA: 0x001FA4C0 File Offset: 0x001F86C0
	private void OnMoveToTargetFinished()
	{
		Card source = base.GetSource();
		EntityBase entity = source.GetEntity();
		Card target = base.GetTarget();
		bool flag = this.m_attackType == AttackType.PROPOSED;
		this.DoTasks(source, target);
		if (!flag)
		{
			this.ActivateImpactEffects(source, target);
		}
		if (entity.IsHero())
		{
			this.MoveSourceHeroBack(source);
			this.OrientSourceHeroBack(source);
			this.UpdateTargetOnMoveToTargetFinished(target);
			if (this.HasFinishAttackSpellOnDamage())
			{
				this.FinishHeroAttack();
				return;
			}
		}
		else
		{
			if (flag)
			{
				this.FinishEverything();
				return;
			}
			source.SetDoNotSort(false);
			source.GetZone().UpdateLayout();
			this.UpdateTargetOnMoveToTargetFinished(target);
			if (this.HasFinishAttackSpellOnDamage())
			{
				this.FinishAttackSpellController();
			}
			else
			{
				this.m_sourceAttackSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnMinionSourceAttackStateFinished));
			}
			this.m_sourceAttackSpell.ActivateState(SpellStateType.DEATH);
		}
	}

	// Token: 0x0600610B RID: 24843 RVA: 0x001FA57F File Offset: 0x001F877F
	private void DoTasks(Card sourceCard, Card targetCard)
	{
		GameUtils.DoDamageTasks(this.m_taskList, sourceCard, targetCard);
	}

	// Token: 0x0600610C RID: 24844 RVA: 0x001FA590 File Offset: 0x001F8790
	private void MoveSourceHeroBack(Card sourceCard)
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_sourcePos,
			"time",
			this.m_HeroInfo.m_MoveBackDuration,
			"easetype",
			this.m_HeroInfo.m_MoveBackEaseType,
			"oncomplete",
			"OnHeroMoveBackFinished",
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(sourceCard.gameObject, args);
	}

	// Token: 0x0600610D RID: 24845 RVA: 0x001FA624 File Offset: 0x001F8824
	private void OrientSourceHeroBack(Card sourceCard)
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			Quaternion.LookRotation(this.m_sourceFacing).eulerAngles,
			"time",
			this.m_HeroInfo.m_OrientBackDuration,
			"easetype",
			this.m_HeroInfo.m_OrientBackEaseType
		});
		iTween.RotateTo(sourceCard.gameObject, args);
	}

	// Token: 0x0600610E RID: 24846 RVA: 0x001FA6A4 File Offset: 0x001F88A4
	private void OnHeroMoveBackFinished()
	{
		Card source = base.GetSource();
		Entity entity = source.GetEntity();
		source.SetDoNotSort(false);
		source.EnableAttacking(false);
		if (this.HasFinishAttackSpellOnDamage())
		{
			return;
		}
		if (entity.GetController().IsLocalUser() || this.m_sourceAttackSpell.GetActiveState() == SpellStateType.NONE)
		{
			this.FinishHeroAttack();
			return;
		}
		this.m_sourceAttackSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnHeroSourceAttackStateFinished));
	}

	// Token: 0x0600610F RID: 24847 RVA: 0x001FA70C File Offset: 0x001F890C
	private void OnHeroSourceAttackStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		spell.RemoveStateFinishedCallback(new Spell.StateFinishedCallback(this.OnHeroSourceAttackStateFinished));
		this.FinishHeroAttack();
	}

	// Token: 0x06006110 RID: 24848 RVA: 0x001FA730 File Offset: 0x001F8930
	private void FinishHeroAttack()
	{
		Card source = base.GetSource();
		Entity entity = source.GetEntity();
		this.PlayWindfuryReminderIfPossible(entity, source);
		this.FinishEverything();
	}

	// Token: 0x06006111 RID: 24849 RVA: 0x001FA759 File Offset: 0x001F8959
	private void OnMinionSourceAttackStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		spell.RemoveStateFinishedCallback(new Spell.StateFinishedCallback(this.OnMinionSourceAttackStateFinished));
		this.FinishAttackSpellController();
	}

	// Token: 0x06006112 RID: 24850 RVA: 0x001FA780 File Offset: 0x001F8980
	private void FinishAttackSpellController()
	{
		Card source = base.GetSource();
		Entity entity = source.GetEntity();
		source.EnableAttacking(false);
		if (!this.CanPlayWindfuryReminder(entity, source))
		{
			this.FinishEverything();
			return;
		}
		this.OnFinishedTaskList();
		base.StartCoroutine(this.WaitThenPlayWindfuryReminder(entity, source));
	}

	// Token: 0x06006113 RID: 24851 RVA: 0x001FA7C8 File Offset: 0x001F89C8
	private void FinishEverything()
	{
		Card source = base.GetSource();
		if (source != null && !source.ShouldShowImmuneVisuals() && (source.GetEntity() == null || !source.GetEntity().HasTag(GAME_TAG.IMMUNE_WHILE_ATTACKING) || this.m_attackType != AttackType.PROPOSED))
		{
			source.GetActor().ActivateSpellDeathState(SpellType.IMMUNE);
		}
		this.OnFinishedTaskList();
		this.OnFinished();
	}

	// Token: 0x06006114 RID: 24852 RVA: 0x001FA829 File Offset: 0x001F8A29
	private IEnumerator WaitThenPlayWindfuryReminder(Entity entity, Card card)
	{
		yield return new WaitForSeconds(1.2f);
		this.PlayWindfuryReminderIfPossible(entity, card);
		this.OnFinished();
		yield break;
	}

	// Token: 0x06006115 RID: 24853 RVA: 0x001FA848 File Offset: 0x001F8A48
	private bool CanPlayWindfuryReminder(Entity entity, Card card)
	{
		return entity.HasWindfury() && !entity.IsExhausted() && entity.GetZone() == TAG_ZONE.PLAY && entity.GetController().IsCurrentPlayer() && !(card.GetActorSpell(SpellType.WINDFURY_BURST, true) == null);
	}

	// Token: 0x06006116 RID: 24854 RVA: 0x001FA897 File Offset: 0x001F8A97
	private void PlayWindfuryReminderIfPossible(Entity entity, Card card)
	{
		if (!this.CanPlayWindfuryReminder(entity, card))
		{
			return;
		}
		card.ActivateActorSpell(SpellType.WINDFURY_BURST);
	}

	// Token: 0x06006117 RID: 24855 RVA: 0x001FA8B0 File Offset: 0x001F8AB0
	private void MoveSourceToTarget(Card sourceCard, Entity sourceEntity, Vector3 impactPos)
	{
		Vector3 b = this.ComputeImpactOffset(sourceCard, impactPos);
		Vector3 vector = impactPos + b;
		float moveToTargetDuration;
		iTween.EaseType moveToTargetEaseType;
		if (sourceEntity.IsHero())
		{
			moveToTargetDuration = this.m_HeroInfo.m_MoveToTargetDuration;
			moveToTargetEaseType = this.m_HeroInfo.m_MoveToTargetEaseType;
		}
		else
		{
			moveToTargetDuration = this.m_AllyInfo.m_MoveToTargetDuration;
			moveToTargetEaseType = this.m_AllyInfo.m_MoveToTargetEaseType;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			vector,
			"time",
			moveToTargetDuration,
			"easetype",
			moveToTargetEaseType,
			"oncomplete",
			"OnMoveToTargetFinished",
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(sourceCard.gameObject, args);
	}

	// Token: 0x06006118 RID: 24856 RVA: 0x001FA984 File Offset: 0x001F8B84
	private void OrientSourceHeroToTarget(Card sourceCard)
	{
		this.m_sourceFacing = sourceCard.transform.forward;
		Quaternion quaternion;
		if (this.m_sourceAttackSpell.GetSpellType() == SpellType.OPPONENT_ATTACK)
		{
			quaternion = Quaternion.LookRotation(-this.m_sourceToTarget);
		}
		else
		{
			quaternion = Quaternion.LookRotation(this.m_sourceToTarget);
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			quaternion.eulerAngles,
			"time",
			this.m_HeroInfo.m_OrientToTargetDuration,
			"easetype",
			this.m_HeroInfo.m_OrientToTargetEaseType
		});
		iTween.RotateTo(sourceCard.gameObject, args);
	}

	// Token: 0x06006119 RID: 24857 RVA: 0x001FAA38 File Offset: 0x001F8C38
	private void MoveTargetToSource(Card targetCard, Entity sourceEntity, Vector3 impactPos)
	{
		float moveToTargetDuration;
		iTween.EaseType moveToTargetEaseType;
		if (sourceEntity.IsHero())
		{
			moveToTargetDuration = this.m_HeroInfo.m_MoveToTargetDuration;
			moveToTargetEaseType = this.m_HeroInfo.m_MoveToTargetEaseType;
		}
		else
		{
			moveToTargetDuration = this.m_AllyInfo.m_MoveToTargetDuration;
			moveToTargetEaseType = this.m_AllyInfo.m_MoveToTargetEaseType;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			impactPos,
			"time",
			moveToTargetDuration,
			"easetype",
			moveToTargetEaseType
		});
		iTween.MoveTo(targetCard.gameObject, args);
	}

	// Token: 0x0600611A RID: 24858 RVA: 0x001FAAD4 File Offset: 0x001F8CD4
	private Vector3 ComputeImpactPos()
	{
		float num = 1f;
		if (this.m_attackType == AttackType.PROPOSED)
		{
			num = 0.5f;
		}
		Vector3 b = num * this.m_ImpactStagingPoint * this.m_sourceToTarget;
		return this.m_sourcePos + b;
	}

	// Token: 0x0600611B RID: 24859 RVA: 0x001FAB18 File Offset: 0x001F8D18
	private Vector3 ComputeImpactOffset(Card sourceCard, Vector3 impactPos)
	{
		if (Mathf.Approximately(this.m_SourceImpactOffset, 0.5f))
		{
			return Vector3.zero;
		}
		if (sourceCard.GetActor().GetMeshRenderer(false) == null)
		{
			return Vector3.zero;
		}
		Bounds bounds = sourceCard.GetActor().GetMeshRenderer(false).bounds;
		bounds.center = this.m_sourcePos;
		Ray ray = new Ray(impactPos, bounds.center - impactPos);
		float d;
		if (!bounds.IntersectRay(ray, out d))
		{
			return Vector3.zero;
		}
		Vector3 b = ray.origin + d * ray.direction;
		Vector3 a = 2f * bounds.center - b - b;
		return 0.5f * a - this.m_SourceImpactOffset * a;
	}

	// Token: 0x0600611C RID: 24860 RVA: 0x001FABF4 File Offset: 0x001F8DF4
	private void ActivateImpactEffects(Card sourceCard, Card targetCard)
	{
		Spell spell = this.DetermineImpactSpellPrefab(sourceCard);
		if (spell == null)
		{
			return;
		}
		Spell spell2 = UnityEngine.Object.Instantiate<Spell>(spell);
		spell2.SetSource(sourceCard.gameObject);
		spell2.AddTarget(targetCard.gameObject);
		Vector3 position = targetCard.transform.position;
		spell2.SetPosition(position);
		Quaternion orientation = Quaternion.LookRotation(this.m_sourceToTarget);
		spell2.SetOrientation(orientation);
		spell2.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnImpactSpellStateFinished));
		spell2.Activate();
	}

	// Token: 0x0600611D RID: 24861 RVA: 0x001FAC70 File Offset: 0x001F8E70
	private Spell DetermineImpactSpellPrefab(Card sourceCard)
	{
		int atk = sourceCard.GetEntity().GetATK();
		SpellValueRange appropriateElementAccordingToRanges = SpellUtils.GetAppropriateElementAccordingToRanges<SpellValueRange>(this.m_ImpactDefs, (SpellValueRange x) => x.m_range, atk);
		if (appropriateElementAccordingToRanges != null && appropriateElementAccordingToRanges.m_spellPrefab != null)
		{
			return appropriateElementAccordingToRanges.m_spellPrefab;
		}
		return this.m_DefaultImpactSpellPrefab;
	}

	// Token: 0x0600611E RID: 24862 RVA: 0x001FACD3 File Offset: 0x001F8ED3
	private void OnImpactSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		UnityEngine.Object.Destroy(spell.gameObject);
	}

	// Token: 0x0600611F RID: 24863 RVA: 0x001FACEC File Offset: 0x001F8EEC
	protected override float GetLostFrameTimeCatchUpSeconds()
	{
		Card source = base.GetSource();
		if (source != null && source.GetEntity() != null && source.GetEntity().IsHero())
		{
			return 0f;
		}
		Card target = base.GetTarget();
		if (target != null && target.GetEntity() != null && target.GetEntity().IsHero())
		{
			return 0f;
		}
		return 0.8f;
	}

	// Token: 0x06006120 RID: 24864 RVA: 0x001FAD54 File Offset: 0x001F8F54
	protected override void OnFinishedTaskList()
	{
		if (this.m_attackType != AttackType.PROPOSED)
		{
			Card source = base.GetSource();
			source.SetDoNotSort(false);
			if (!source.GetEntity().IsHero())
			{
				Zone zone = source.GetZone();
				zone.UpdateLayout();
				if (this.m_sourceAttackSpell == null)
				{
					bool isSourceFriendly = zone.m_Side == Player.Side.FRIENDLY;
					this.m_sourceAttackSpell = this.GetSourceAttackSpell(source, isSourceFriendly);
				}
				if (this.m_sourceAttackSpell != null && (this.m_sourceAttackSpell.GetActiveState() == SpellStateType.BIRTH || this.m_sourceAttackSpell.GetActiveState() == SpellStateType.IDLE || this.m_sourceAttackSpell.GetActiveState() == SpellStateType.ACTION))
				{
					this.CancelAttackSpell(source.GetEntity(), this.m_sourceAttackSpell);
				}
			}
		}
		base.OnFinishedTaskList();
	}

	// Token: 0x06006121 RID: 24865 RVA: 0x001FAE0D File Offset: 0x001F900D
	private void CancelAttackSpell(Entity sourceEntity, Spell attackSpell)
	{
		if (attackSpell == null)
		{
			return;
		}
		if (sourceEntity == null)
		{
			attackSpell.ActivateState(SpellStateType.DEATH);
			return;
		}
		if (sourceEntity.IsHero())
		{
			attackSpell.ActivateState(SpellStateType.CANCEL);
			return;
		}
		attackSpell.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x06006122 RID: 24866 RVA: 0x001FAE3C File Offset: 0x001F903C
	private Spell GetSourceAttackSpell(Card sourceCard, bool isSourceFriendly)
	{
		if (GameState.Get().GetGameEntity().HasTag(GAME_TAG.BACON_HIGHLIGHT_ATTACKING_MINION_DURING_COMBAT))
		{
			Spell actorSpell = sourceCard.GetActorSpell(SpellType.AUTO_ATTACK_WITH_HIGHLIGHT, true);
			if (actorSpell != null)
			{
				return actorSpell;
			}
			return null;
		}
		else
		{
			if (isSourceFriendly)
			{
				return sourceCard.GetActorSpell(SpellType.FRIENDLY_ATTACK, true);
			}
			return sourceCard.GetActorSpell(SpellType.OPPONENT_ATTACK, true);
		}
	}

	// Token: 0x04005102 RID: 20738
	public HeroAttackDef m_HeroInfo;

	// Token: 0x04005103 RID: 20739
	public AllyAttackDef m_AllyInfo;

	// Token: 0x04005104 RID: 20740
	public float m_ImpactStagingPoint = 1f;

	// Token: 0x04005105 RID: 20741
	public float m_SourceImpactOffset = -0.25f;

	// Token: 0x04005106 RID: 20742
	public SpellValueRange[] m_ImpactDefs;

	// Token: 0x04005107 RID: 20743
	public Spell m_DefaultImpactSpellPrefab;

	// Token: 0x04005108 RID: 20744
	private const float PROPOSED_ATTACK_IMPACT_POINT_SCALAR = 0.5f;

	// Token: 0x04005109 RID: 20745
	private const float WINDFURY_REMINDER_WAIT_SEC = 1.2f;

	// Token: 0x0400510A RID: 20746
	private AttackType m_attackType;

	// Token: 0x0400510B RID: 20747
	private Spell m_sourceAttackSpell;

	// Token: 0x0400510C RID: 20748
	private Vector3 m_sourcePos;

	// Token: 0x0400510D RID: 20749
	private Vector3 m_sourceToTarget;

	// Token: 0x0400510E RID: 20750
	private Vector3 m_sourceFacing;

	// Token: 0x0400510F RID: 20751
	private bool m_repeatProposed;
}
