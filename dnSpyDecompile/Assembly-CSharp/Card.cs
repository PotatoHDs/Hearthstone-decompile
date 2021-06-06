using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using PegasusGame;
using UnityEngine;

// Token: 0x0200086B RID: 2155
public class Card : MonoBehaviour
{
	// Token: 0x170006E2 RID: 1762
	// (get) Token: 0x06007444 RID: 29764 RVA: 0x00254694 File Offset: 0x00252894
	// (set) Token: 0x06007445 RID: 29765 RVA: 0x0025469C File Offset: 0x0025289C
	public bool IsBeingDragged { get; set; }

	// Token: 0x06007446 RID: 29766 RVA: 0x002546A5 File Offset: 0x002528A5
	public override string ToString()
	{
		if (this.m_entity != null)
		{
			return this.m_entity.ToString();
		}
		return "UNKNOWN CARD";
	}

	// Token: 0x06007447 RID: 29767 RVA: 0x002546C0 File Offset: 0x002528C0
	public global::Entity GetEntity()
	{
		return this.m_entity;
	}

	// Token: 0x06007448 RID: 29768 RVA: 0x002546C8 File Offset: 0x002528C8
	public void SetEntity(global::Entity entity)
	{
		this.m_entity = entity;
	}

	// Token: 0x06007449 RID: 29769 RVA: 0x002546D1 File Offset: 0x002528D1
	public void Destroy()
	{
		if (this.m_actor != null)
		{
			this.m_actor.Destroy();
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600744A RID: 29770 RVA: 0x002546F7 File Offset: 0x002528F7
	public global::Player GetController()
	{
		if (this.m_entity == null)
		{
			return null;
		}
		return this.m_entity.GetController();
	}

	// Token: 0x0600744B RID: 29771 RVA: 0x0025470E File Offset: 0x0025290E
	public global::Player.Side GetControllerSide()
	{
		if (this.m_entity == null)
		{
			return global::Player.Side.NEUTRAL;
		}
		return this.m_entity.GetControllerSide();
	}

	// Token: 0x0600744C RID: 29772 RVA: 0x00254728 File Offset: 0x00252928
	public global::Entity GetHero()
	{
		global::Player controller = this.GetController();
		if (controller == null)
		{
			return null;
		}
		return controller.GetHero();
	}

	// Token: 0x0600744D RID: 29773 RVA: 0x00254748 File Offset: 0x00252948
	public Card GetHeroCard()
	{
		global::Entity hero = this.GetHero();
		if (hero == null)
		{
			return null;
		}
		return hero.GetCard();
	}

	// Token: 0x0600744E RID: 29774 RVA: 0x00254768 File Offset: 0x00252968
	public global::Entity GetHeroPower()
	{
		global::Player controller = this.GetController();
		if (controller == null)
		{
			return null;
		}
		return controller.GetHeroPower();
	}

	// Token: 0x0600744F RID: 29775 RVA: 0x00254788 File Offset: 0x00252988
	public Card GetHeroPowerCard()
	{
		global::Entity heroPower = this.GetHeroPower();
		if (heroPower == null)
		{
			return null;
		}
		return heroPower.GetCard();
	}

	// Token: 0x06007450 RID: 29776 RVA: 0x002547A7 File Offset: 0x002529A7
	public TAG_PREMIUM GetPremium()
	{
		if (this.m_entity == null)
		{
			return TAG_PREMIUM.NORMAL;
		}
		return this.m_entity.GetPremiumType();
	}

	// Token: 0x06007451 RID: 29777 RVA: 0x002547BE File Offset: 0x002529BE
	public bool IsOverPlayfield()
	{
		return this.m_overPlayfield;
	}

	// Token: 0x06007452 RID: 29778 RVA: 0x002547C6 File Offset: 0x002529C6
	public void NotifyOverPlayfield()
	{
		this.m_overPlayfield = true;
		this.UpdateActorState(false);
	}

	// Token: 0x06007453 RID: 29779 RVA: 0x002547D6 File Offset: 0x002529D6
	public void NotifyLeftPlayfield()
	{
		this.m_overPlayfield = false;
		this.UpdateActorState(false);
	}

	// Token: 0x06007454 RID: 29780 RVA: 0x002547E6 File Offset: 0x002529E6
	public void OnDestroy()
	{
		this.ReleaseAssets();
		if (this.m_mousedOver)
		{
			if (GameState.Get() == null)
			{
				return;
			}
			if (InputManager.Get() == null)
			{
				return;
			}
			InputManager.Get().NotifyCardDestroyed(this);
		}
	}

	// Token: 0x06007455 RID: 29781 RVA: 0x00254818 File Offset: 0x00252A18
	public void NotifyMousedOver()
	{
		this.m_mousedOver = true;
		this.UpdateActorState(false);
		this.UpdateProposedManaUsage();
		if (RemoteActionHandler.Get() && TargetReticleManager.Get())
		{
			RemoteActionHandler.Get().NotifyOpponentOfMouseOverEntity(this.GetEntity().GetCard());
		}
		if (GameState.Get() != null)
		{
			GameState.Get().GetGameEntity().NotifyOfCardMousedOver(this.GetEntity());
		}
		if (this.m_zone is ZoneHand)
		{
			Spell actorSpell = this.GetActorSpell(SpellType.SPELL_POWER_HINT_BURST, true);
			if (actorSpell != null)
			{
				actorSpell.Deactivate();
			}
			Spell actorSpell2 = this.GetActorSpell(SpellType.SPELL_POWER_HINT_IDLE, true);
			if (actorSpell2 != null)
			{
				actorSpell2.Deactivate();
			}
			Spell actorSpell3 = this.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST, true);
			if (actorSpell3 != null)
			{
				actorSpell3.Deactivate();
			}
			Spell actorSpell4 = this.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_IDLE, true);
			if (actorSpell4 != null)
			{
				actorSpell4.Deactivate();
			}
			this.GetActorSpell(SpellType.LIFESTEAL_DOES_DAMAGE_HINT_IDLE, true);
			if (actorSpell4 != null)
			{
				actorSpell4.Deactivate();
			}
			if (GameState.Get() != null && GameState.Get().IsMulliganManagerActive())
			{
				SoundManager.Get().LoadAndPlay("collection_manager_card_mouse_over.prefab:0d4e20bc78956bc48b5e2963ec39211c", base.gameObject);
			}
			if (this.ShouldShowHeroPowerTooltip())
			{
				this.m_heroPowerTooltip.NotifyMousedOver();
			}
		}
		if (this.m_entity.IsControlledByFriendlySidePlayer() && (this.m_entity.IsHero() || this.m_zone is ZonePlay) && !this.m_transitioningZones)
		{
			bool flag = this.m_entity.HasSpellPower() || this.m_entity.HasSpellPowerDouble();
			bool flag2 = this.m_entity.HasHeroPowerDamage();
			if (flag || flag2)
			{
				Spell actorSpell5 = this.GetActorSpell(SpellType.SPELL_POWER_HINT_BURST, true);
				if (actorSpell5 != null)
				{
					actorSpell5.Reactivate();
				}
				if (flag)
				{
					ZoneMgr.Get().OnSpellPowerEntityMousedOver(this.m_entity.GetSpellPowerSchool());
				}
			}
			if (this.m_entity.HasHealingDoesDamageHint())
			{
				Spell actorSpell6 = this.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST, true);
				if (actorSpell6 != null)
				{
					actorSpell6.Reactivate();
				}
				ZoneMgr.Get().OnHealingDoesDamageEntityMousedOver();
			}
			if (this.m_entity.HasLifestealDoesDamageHint())
			{
				Spell actorSpell7 = this.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST, true);
				if (actorSpell7 != null)
				{
					actorSpell7.Reactivate();
				}
				ZoneMgr.Get().OnLifestealDoesDamageEntityMousedOver();
			}
		}
		if (this.m_entity.IsSidekickHero() && this.m_zone is ZoneHero && this.m_heroPowerTooltip != null && !TargetReticleManager.Get().IsActive())
		{
			this.m_heroPowerTooltip.NotifyMousedOver();
		}
		if (this.m_entity.IsWeapon() && this.m_entity.IsExhausted() && this.m_actor != null && this.m_actor.GetAttackObject() != null)
		{
			this.m_actor.GetAttackObject().Enlarge(1f);
		}
		if (this.m_entity.IsQuest() && this.m_zone is ZoneSecret)
		{
			QuestController component = this.m_actor.GetComponent<QuestController>();
			if (component != null)
			{
				component.NotifyMousedOver();
			}
		}
		if (this.m_entity.IsPuzzle() && this.m_zone is ZoneSecret)
		{
			PuzzleController component2 = this.m_actor.GetComponent<PuzzleController>();
			if (component2 != null)
			{
				component2.NotifyMousedOver();
			}
		}
		if (this.m_entity.IsRulebook() && this.m_zone is ZoneSecret)
		{
			RulebookController component3 = this.m_actor.GetComponent<RulebookController>();
			if (component3 != null)
			{
				component3.NotifyMousedOver();
			}
		}
	}

	// Token: 0x06007456 RID: 29782 RVA: 0x00254B9C File Offset: 0x00252D9C
	public void NotifyMousedOut()
	{
		this.m_mousedOver = false;
		this.UpdateActorState(false);
		this.UpdateProposedManaUsage();
		if (RemoteActionHandler.Get())
		{
			RemoteActionHandler.Get().NotifyOpponentOfMouseOut();
		}
		if (TooltipPanelManager.Get())
		{
			TooltipPanelManager.Get().HideKeywordHelp();
		}
		if (CardTypeBanner.Get())
		{
			CardTypeBanner.Get().Hide(this);
		}
		if (GameState.Get() != null)
		{
			GameState.Get().GetGameEntity().NotifyOfCardMousedOff(this.GetEntity());
		}
		if (this.m_entity.IsControlledByFriendlySidePlayer() && (this.m_entity.IsHero() || this.m_zone is ZonePlay))
		{
			if (this.m_entity.HasSpellPower())
			{
				ZoneMgr.Get().OnSpellPowerEntityMousedOut(this.m_entity.GetSpellPowerSchool());
			}
			if (this.m_entity.HasHealingDoesDamageHint())
			{
				ZoneMgr.Get().OnHealingDoesDamageEntityMousedOut();
			}
			if (this.m_entity.HasLifestealDoesDamageHint())
			{
				ZoneMgr.Get().OnLifestealDoesDamageEntityMousedOut();
			}
		}
		if (this.m_entity.IsWeapon() && this.m_entity.IsExhausted() && this.m_actor != null && this.m_actor.GetAttackObject() != null)
		{
			this.m_actor.GetAttackObject().ScaleToZero();
		}
		if (this.m_entity.IsQuest() && this.m_zone is ZoneSecret)
		{
			QuestController component = this.m_actor.GetComponent<QuestController>();
			if (component != null)
			{
				component.NotifyMousedOut();
			}
		}
		if (this.m_entity.IsPuzzle() && this.m_zone is ZoneSecret && this.m_actor != null)
		{
			PuzzleController component2 = this.m_actor.GetComponent<PuzzleController>();
			if (component2 != null)
			{
				component2.NotifyMousedOut();
			}
		}
		if (this.m_entity.IsRulebook() && this.m_zone is ZoneSecret && this.m_actor != null)
		{
			RulebookController component3 = this.m_actor.GetComponent<RulebookController>();
			if (component3 != null)
			{
				component3.NotifyMousedOut();
			}
		}
		if (this.m_heroPowerTooltip != null)
		{
			this.m_heroPowerTooltip.NotifyMousedOut();
		}
	}

	// Token: 0x06007457 RID: 29783 RVA: 0x00254DB2 File Offset: 0x00252FB2
	public bool IsMousedOver()
	{
		return this.m_mousedOver;
	}

	// Token: 0x06007458 RID: 29784 RVA: 0x00254DBA File Offset: 0x00252FBA
	public void NotifyOpponentMousedOverThisCard()
	{
		this.m_mousedOverByOpponent = true;
		this.UpdateActorState(false);
	}

	// Token: 0x06007459 RID: 29785 RVA: 0x00254DCA File Offset: 0x00252FCA
	public void NotifyOpponentMousedOffThisCard()
	{
		this.m_mousedOverByOpponent = false;
		this.UpdateActorState(false);
	}

	// Token: 0x0600745A RID: 29786 RVA: 0x00254DDA File Offset: 0x00252FDA
	public void NotifyPickedUp()
	{
		this.m_transitioningZones = false;
		if (this.GetZone() is ZoneHand)
		{
			this.CutoffFriendlyCardDraw();
		}
		if (this.ShouldShowHeroPowerTooltip())
		{
			this.m_heroPowerTooltip.NotifyPickedUp();
		}
	}

	// Token: 0x0600745B RID: 29787 RVA: 0x00254E0C File Offset: 0x0025300C
	public void NotifyTargetingCanceled()
	{
		if (this.m_entity.IsCharacter() && !this.IsAttacking())
		{
			Spell actorAttackSpellForInput = this.GetActorAttackSpellForInput();
			if (actorAttackSpellForInput != null)
			{
				if (!this.ShouldShowImmuneVisuals())
				{
					this.GetActor().ActivateSpellDeathState(SpellType.IMMUNE);
				}
				SpellStateType activeState = actorAttackSpellForInput.GetActiveState();
				if (activeState != SpellStateType.NONE && activeState != SpellStateType.CANCEL)
				{
					actorAttackSpellForInput.ActivateState(SpellStateType.CANCEL);
				}
			}
		}
		this.ActivateHandStateSpells(false);
	}

	// Token: 0x0600745C RID: 29788 RVA: 0x00254E6F File Offset: 0x0025306F
	public bool IsInputEnabled()
	{
		if (this.m_entity != null)
		{
			if (this.m_entity.HasQueuedChangeEntity())
			{
				return false;
			}
			if (this.m_entity.IsHeroPower() && this.m_entity.HasQueuedControllerTagChange())
			{
				return false;
			}
		}
		return this.m_inputEnabled;
	}

	// Token: 0x0600745D RID: 29789 RVA: 0x00254EAA File Offset: 0x002530AA
	public void SetInputEnabled(bool enabled)
	{
		this.m_inputEnabled = enabled;
		this.UpdateActorState(false);
	}

	// Token: 0x0600745E RID: 29790 RVA: 0x00254EBC File Offset: 0x002530BC
	public bool IsAllowedToShowTooltip()
	{
		if (this.m_zone == null)
		{
			return false;
		}
		if (this.m_zone.m_ServerTag != TAG_ZONE.PLAY && this.m_zone.m_ServerTag != TAG_ZONE.SECRET && this.m_zone.m_ServerTag == TAG_ZONE.HAND && this.m_zone.m_Side != global::Player.Side.OPPOSING)
		{
			return false;
		}
		if (GameState.Get() != null)
		{
			if (this.m_entity.IsSidekickHero())
			{
				if (this.m_entity.GetZone() != TAG_ZONE.PLAY)
				{
					return false;
				}
			}
			else if (this.m_entity.IsHero() && this.m_entity.GetZone() == TAG_ZONE.PLAY && !GameState.Get().GetBooleanGameOption(GameEntityOption.SHOW_HERO_TOOLTIPS))
			{
				return false;
			}
		}
		return !this.m_entity.IsQuest() && !this.m_entity.IsPuzzle() && !this.m_entity.IsRulebook();
	}

	// Token: 0x0600745F RID: 29791 RVA: 0x00254F8C File Offset: 0x0025318C
	public bool IsAbleToShowTooltip()
	{
		return this.m_entity != null && !(this.m_actor == null) && !(BigCard.Get() == null);
	}

	// Token: 0x06007460 RID: 29792 RVA: 0x00254FB8 File Offset: 0x002531B8
	public bool GetShouldShowTooltip()
	{
		return this.m_shouldShowTooltip;
	}

	// Token: 0x06007461 RID: 29793 RVA: 0x00254FC0 File Offset: 0x002531C0
	public void SetShouldShowTooltip()
	{
		if (!this.IsAllowedToShowTooltip())
		{
			return;
		}
		if (this.m_shouldShowTooltip)
		{
			return;
		}
		this.m_shouldShowTooltip = true;
	}

	// Token: 0x06007462 RID: 29794 RVA: 0x00254FDB File Offset: 0x002531DB
	public void ShowTooltip()
	{
		if (this.m_showTooltip)
		{
			return;
		}
		this.m_showTooltip = true;
		this.UpdateTooltip();
	}

	// Token: 0x06007463 RID: 29795 RVA: 0x00254FF3 File Offset: 0x002531F3
	public void HideTooltip()
	{
		this.m_shouldShowTooltip = false;
		if (!this.m_showTooltip)
		{
			return;
		}
		this.m_showTooltip = false;
		this.UpdateTooltip();
	}

	// Token: 0x06007464 RID: 29796 RVA: 0x00255012 File Offset: 0x00253212
	public bool IsShowingTooltip()
	{
		return this.m_showTooltip;
	}

	// Token: 0x06007465 RID: 29797 RVA: 0x0025501C File Offset: 0x0025321C
	private void ShowMouseOverSpell()
	{
		if (this.m_entity == null)
		{
			return;
		}
		if (this.m_actor == null)
		{
			return;
		}
		if (this.m_entity.HasTag(GAME_TAG.VOODOO_LINK) || this.m_entity.DoEnchantmentsHaveVoodooLink())
		{
			Spell spell = this.m_actor.GetSpell(SpellType.VOODOO_LINK);
			if (spell)
			{
				spell.SetSource(base.gameObject);
				spell.Activate();
			}
		}
		string cardId = this.m_entity.GetCardId();
		if (cardId == MagtheridonLinkToHellfireWardersSpell.MagtheridonId || cardId == MagtheridonLinkToHellfireWardersSpell.HellfireWarderId)
		{
			Spell spell2 = this.m_actor.GetSpell(SpellType.MAGTHERIDON_LINK);
			if (spell2)
			{
				spell2.SetSource(base.gameObject);
				spell2.Activate();
			}
		}
	}

	// Token: 0x06007466 RID: 29798 RVA: 0x002550DC File Offset: 0x002532DC
	private void HideMouseOverSpell()
	{
		if (this.m_actor == null)
		{
			return;
		}
		Spell spellIfLoaded = this.m_actor.GetSpellIfLoaded(SpellType.VOODOO_LINK);
		if (spellIfLoaded)
		{
			spellIfLoaded.Deactivate();
		}
		spellIfLoaded = this.m_actor.GetSpellIfLoaded(SpellType.MAGTHERIDON_LINK);
		if (spellIfLoaded)
		{
			spellIfLoaded.Deactivate();
		}
	}

	// Token: 0x06007467 RID: 29799 RVA: 0x00255134 File Offset: 0x00253334
	public void UpdateTooltip()
	{
		if (this.GetShouldShowTooltip() && this.IsAllowedToShowTooltip() && this.IsAbleToShowTooltip() && this.m_showTooltip)
		{
			this.ShowMouseOverSpell();
			if (BigCard.Get() != null)
			{
				BigCard.Get().Show(this);
				return;
			}
		}
		else
		{
			this.m_showTooltip = false;
			this.m_shouldShowTooltip = false;
			this.HideMouseOverSpell();
			if (BigCard.Get() != null)
			{
				BigCard.Get().Hide(this);
			}
		}
	}

	// Token: 0x06007468 RID: 29800 RVA: 0x002551B0 File Offset: 0x002533B0
	public bool IsAttacking()
	{
		return this.m_attacking;
	}

	// Token: 0x06007469 RID: 29801 RVA: 0x002551B8 File Offset: 0x002533B8
	public void EnableAttacking(bool enable)
	{
		this.m_attacking = enable;
	}

	// Token: 0x0600746A RID: 29802 RVA: 0x002551C1 File Offset: 0x002533C1
	public bool IsMoving()
	{
		return this.m_moving;
	}

	// Token: 0x0600746B RID: 29803 RVA: 0x002551C9 File Offset: 0x002533C9
	public void EnableMoving(bool enable)
	{
		this.m_moving = enable;
	}

	// Token: 0x0600746C RID: 29804 RVA: 0x002551D2 File Offset: 0x002533D2
	public bool WillIgnoreDeath()
	{
		return this.m_ignoreDeath;
	}

	// Token: 0x0600746D RID: 29805 RVA: 0x002551DA File Offset: 0x002533DA
	public void IgnoreDeath(bool ignore)
	{
		this.m_ignoreDeath = ignore;
	}

	// Token: 0x0600746E RID: 29806 RVA: 0x002551E3 File Offset: 0x002533E3
	public bool WillSuppressDeathEffects()
	{
		return this.m_suppressDeathEffects;
	}

	// Token: 0x0600746F RID: 29807 RVA: 0x002551EB File Offset: 0x002533EB
	public void SuppressDeathEffects(bool suppress)
	{
		this.m_suppressDeathEffects = suppress;
	}

	// Token: 0x06007470 RID: 29808 RVA: 0x002551F4 File Offset: 0x002533F4
	public bool WillSuppressDeathSounds()
	{
		return this.m_suppressDeathSounds;
	}

	// Token: 0x06007471 RID: 29809 RVA: 0x002551FC File Offset: 0x002533FC
	public void SuppressDeathSounds(bool suppress)
	{
		this.m_suppressDeathSounds = suppress;
	}

	// Token: 0x06007472 RID: 29810 RVA: 0x00255205 File Offset: 0x00253405
	public bool WillSuppressKeywordDeaths()
	{
		return this.m_suppressKeywordDeaths;
	}

	// Token: 0x06007473 RID: 29811 RVA: 0x0025520D File Offset: 0x0025340D
	public void SuppressKeywordDeaths(bool suppress)
	{
		this.m_suppressKeywordDeaths = suppress;
	}

	// Token: 0x06007474 RID: 29812 RVA: 0x00255216 File Offset: 0x00253416
	public float GetKeywordDeathDelaySec()
	{
		return this.m_keywordDeathDelaySec;
	}

	// Token: 0x06007475 RID: 29813 RVA: 0x0025521E File Offset: 0x0025341E
	public void SetKeywordDeathDelaySec(float sec)
	{
		this.m_keywordDeathDelaySec = sec;
	}

	// Token: 0x06007476 RID: 29814 RVA: 0x00255227 File Offset: 0x00253427
	public bool WillSuppressActorTriggerSpell()
	{
		return this.m_suppressActorTriggerSpell;
	}

	// Token: 0x06007477 RID: 29815 RVA: 0x0025522F File Offset: 0x0025342F
	public void SuppressActorTriggerSpell(bool suppress)
	{
		this.m_suppressActorTriggerSpell = suppress;
	}

	// Token: 0x06007478 RID: 29816 RVA: 0x00255238 File Offset: 0x00253438
	public bool WillSuppressPlaySounds()
	{
		return (this.GetEntity() != null && this.GetEntity().HasTag(GAME_TAG.SUPPRESS_ALL_SUMMON_VO)) || this.GetController().HasTag(GAME_TAG.SUPPRESS_SUMMON_VO_FOR_PLAYER) || this.m_suppressPlaySoundCount > 0;
	}

	// Token: 0x06007479 RID: 29817 RVA: 0x00255271 File Offset: 0x00253471
	public bool WillSuppressCustomSpells()
	{
		return GameState.Get().GetGameEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_SPELLS) || this.GetController().HasTag(GAME_TAG.FORCE_NO_CUSTOM_SPELLS) || this.GetEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_SPELLS);
	}

	// Token: 0x0600747A RID: 29818 RVA: 0x002552AD File Offset: 0x002534AD
	public bool WillSuppressCustomSummonSpells()
	{
		return GameState.Get().GetGameEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_SUMMON_SPELLS) || this.GetController().HasTag(GAME_TAG.FORCE_NO_CUSTOM_SUMMON_SPELLS) || this.GetEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_SUMMON_SPELLS);
	}

	// Token: 0x0600747B RID: 29819 RVA: 0x002552E9 File Offset: 0x002534E9
	public bool WillSuppressCustomLifetimeSpells()
	{
		return GameState.Get().GetGameEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_LIFETIME_SPELLS) || this.GetController().HasTag(GAME_TAG.FORCE_NO_CUSTOM_LIFETIME_SPELLS) || this.GetEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_LIFETIME_SPELLS);
	}

	// Token: 0x0600747C RID: 29820 RVA: 0x00255325 File Offset: 0x00253525
	public bool WillSuppressCustomKeywordSpells()
	{
		return GameState.Get().GetGameEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_KEYWORD_SPELLS) || this.GetController().HasTag(GAME_TAG.FORCE_NO_CUSTOM_KEYWORD_SPELLS) || this.GetEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_KEYWORD_SPELLS);
	}

	// Token: 0x0600747D RID: 29821 RVA: 0x00255364 File Offset: 0x00253564
	public void SuppressPlaySounds(bool suppress)
	{
		if (suppress)
		{
			this.m_suppressPlaySoundCount++;
			return;
		}
		int num = this.m_suppressPlaySoundCount - 1;
		this.m_suppressPlaySoundCount = num;
		if (num < 0)
		{
			this.m_suppressPlaySoundCount = 0;
		}
	}

	// Token: 0x0600747E RID: 29822 RVA: 0x0025539E File Offset: 0x0025359E
	public void SuppressHandToDeckTransition()
	{
		this.m_suppressHandToDeckTransition = true;
	}

	// Token: 0x0600747F RID: 29823 RVA: 0x002553A7 File Offset: 0x002535A7
	public bool IsShown()
	{
		return this.m_shown;
	}

	// Token: 0x06007480 RID: 29824 RVA: 0x002553AF File Offset: 0x002535AF
	public void ShowCard()
	{
		if (this.m_shown)
		{
			return;
		}
		this.m_shown = true;
		this.ShowImpl();
	}

	// Token: 0x06007481 RID: 29825 RVA: 0x002553C7 File Offset: 0x002535C7
	private void ShowImpl()
	{
		if (this.m_actor == null)
		{
			return;
		}
		this.m_actor.Show();
		this.RefreshActor();
	}

	// Token: 0x06007482 RID: 29826 RVA: 0x002553E9 File Offset: 0x002535E9
	public void HideCard()
	{
		if (!this.m_shown || this.m_actorLoading)
		{
			return;
		}
		this.m_shown = false;
		this.HideImpl();
	}

	// Token: 0x06007483 RID: 29827 RVA: 0x00255409 File Offset: 0x00253609
	private void HideImpl()
	{
		if (this.m_actor == null)
		{
			return;
		}
		this.m_actor.Hide();
	}

	// Token: 0x06007484 RID: 29828 RVA: 0x00255428 File Offset: 0x00253628
	public void SetBattleCrySource(bool source)
	{
		this.m_isBattleCrySource = source;
		if (this.m_actor != null)
		{
			if (source)
			{
				SceneUtils.SetLayer(this.m_actor.gameObject, GameLayer.IgnoreFullScreenEffects);
				return;
			}
			SceneUtils.SetLayer(this.m_actor.gameObject, GameLayer.Default);
			SceneUtils.SetLayer(this.m_actor.GetMeshRenderer(false).gameObject, GameLayer.CardRaycast);
		}
	}

	// Token: 0x06007485 RID: 29829 RVA: 0x00255488 File Offset: 0x00253688
	public void DoTauntNotification()
	{
		if (this.m_activeSpawnSpell != null && this.m_activeSpawnSpell.IsActive())
		{
			return;
		}
		iTween.PunchScale(this.m_actor.gameObject, new Vector3(0.2f, 0.2f, 0.2f), 0.5f);
	}

	// Token: 0x06007486 RID: 29830 RVA: 0x002554DC File Offset: 0x002536DC
	public void UpdateProposedManaUsage()
	{
		if (GameState.Get() == null)
		{
			return;
		}
		if (GameState.Get().GetSelectedOption() != -1)
		{
			return;
		}
		global::Player player = GameState.Get().GetPlayer(this.GetEntity().GetControllerId());
		if (player == null)
		{
			return;
		}
		if (!player.IsFriendlySide())
		{
			return;
		}
		if (!player.HasTag(GAME_TAG.CURRENT_PLAYER))
		{
			return;
		}
		if (!this.m_mousedOver)
		{
			player.CancelAllProposedMana(this.m_entity);
			return;
		}
		bool flag = this.m_entity.GetZone() == TAG_ZONE.HAND;
		bool flag2 = this.m_entity.IsHeroPowerOrGameModeButton();
		if (!flag && !flag2)
		{
			return;
		}
		if (!GameState.Get().IsValidOption(this.m_entity))
		{
			return;
		}
		if (this.m_entity.IsSpell() && player.HasTag(GAME_TAG.SPELLS_COST_HEALTH))
		{
			return;
		}
		if (this.m_entity.HasTag(GAME_TAG.CARD_COSTS_HEALTH))
		{
			return;
		}
		player.ProposeManaCrystalUsage(this.m_entity);
	}

	// Token: 0x06007487 RID: 29831 RVA: 0x002555AE File Offset: 0x002537AE
	public void SetMagneticPlayData(MagneticPlayData data)
	{
		if (data == null)
		{
			return;
		}
		if (this.m_magneticPlayData != null)
		{
			Log.Gameplay.PrintError("{0}.SetMagneticPlayData: m_magneticPlayData is already set! {1}", new object[]
			{
				this,
				this.m_magneticPlayData
			});
		}
		this.m_magneticPlayData = data;
	}

	// Token: 0x06007488 RID: 29832 RVA: 0x002555E5 File Offset: 0x002537E5
	public MagneticPlayData GetMagneticPlayData()
	{
		return this.m_magneticPlayData;
	}

	// Token: 0x06007489 RID: 29833 RVA: 0x002555F0 File Offset: 0x002537F0
	public void DetermineIfOverrideDrawTimeScale()
	{
		if (this.m_drawTimeScale == null)
		{
			if (this.m_cardDrawTracker < 3)
			{
				this.m_drawTimeScale = new float?(1f);
				return;
			}
			if (this.m_cardDrawTracker <= 6)
			{
				float num = -0.111f;
				this.m_drawTimeScale = new float?(1f + num * (float)(this.m_cardDrawTracker + 1 - 3));
				return;
			}
			this.m_drawTimeScale = new float?(0.556f);
		}
	}

	// Token: 0x0600748A RID: 29834 RVA: 0x00255662 File Offset: 0x00253862
	public void ResetCardDrawTimeScale()
	{
		this.m_drawTimeScale = null;
	}

	// Token: 0x0600748B RID: 29835 RVA: 0x00255670 File Offset: 0x00253870
	public bool CanPlayHealingDoesDamageHint()
	{
		return this.IsShown() && this.m_entity != null && !(this.m_actor == null) && this.m_actor.IsShown() && (this.m_entity.HasTag(GAME_TAG.AFFECTED_BY_HEALING_DOES_DAMAGE) || this.m_entity.HasTag(GAME_TAG.LIFESTEAL) || this.m_entity.GetCardTextBuilder().ContainsBonusHealingToken(this.m_entity));
	}

	// Token: 0x0600748C RID: 29836 RVA: 0x002556EC File Offset: 0x002538EC
	public bool CanPlayLifestealDoesDamageHint()
	{
		return this.IsShown() && this.m_entity != null && !(this.m_actor == null) && this.m_actor.IsShown() && this.m_entity.HasTag(GAME_TAG.LIFESTEAL);
	}

	// Token: 0x0600748D RID: 29837 RVA: 0x0025573C File Offset: 0x0025393C
	public bool CanPlaySpellPowerHint(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		return this.IsShown() && !(this.m_actor == null) && this.m_actor.IsShown() && this.m_entity != null && ((this.m_entity.IsAffectedBySpellPower() && spellSchool == TAG_SPELL_SCHOOL.NONE) || ((spellSchool == TAG_SPELL_SCHOOL.NONE || spellSchool == this.m_entity.GetSpellSchool()) && this.m_entity.GetCardTextBuilder().ContainsBonusDamageToken(this.m_entity)));
	}

	// Token: 0x0600748E RID: 29838 RVA: 0x002557B5 File Offset: 0x002539B5
	public DefLoader.DisposableCardDef ShareDisposableCardDef()
	{
		DefLoader.DisposableCardDef cardDef = this.m_cardDef;
		if (cardDef == null)
		{
			return null;
		}
		return cardDef.Share();
	}

	// Token: 0x0600748F RID: 29839 RVA: 0x002557C8 File Offset: 0x002539C8
	public void SetCardDef(DefLoader.DisposableCardDef cardDef, bool updateActor)
	{
		DefLoader.DisposableCardDef cardDef2 = this.m_cardDef;
		if (((cardDef2 != null) ? cardDef2.CardDef : null) == ((cardDef != null) ? cardDef.CardDef : null))
		{
			return;
		}
		this.ReleaseCardDef();
		this.m_cardDef = cardDef.Share();
		this.InitCardDefAssets();
		if (this.m_actor != null && !updateActor)
		{
			this.m_actor.SetCardDef(this.m_cardDef);
			this.m_actor.UpdateAllComponents();
		}
	}

	// Token: 0x06007490 RID: 29840 RVA: 0x00255840 File Offset: 0x00253A40
	public void PurgeSpells()
	{
		foreach (CardEffect cardEffect in this.m_allEffects)
		{
			cardEffect.PurgeSpells();
		}
	}

	// Token: 0x06007491 RID: 29841 RVA: 0x00255890 File Offset: 0x00253A90
	private bool ShouldPreloadCardAssets()
	{
		return !HearthstoneApplication.IsPublic() && Options.Get().GetBool(global::Option.PRELOAD_CARD_ASSETS, false);
	}

	// Token: 0x06007492 RID: 29842 RVA: 0x002558A8 File Offset: 0x00253AA8
	public void OverrideCustomSpawnSpell(Spell spell)
	{
		if (spell == null)
		{
			Debug.LogErrorFormat("Tried to set OverrideCustomSpawnSpell to null!", Array.Empty<object>());
			return;
		}
		this.m_customSpawnSpellOverride = this.SetupOverrideSpell(this.m_customSpawnSpellOverride, spell);
	}

	// Token: 0x06007493 RID: 29843 RVA: 0x002558D6 File Offset: 0x00253AD6
	public void OverrideCustomDeathSpell(Spell spell)
	{
		if (spell == null)
		{
			Debug.LogErrorFormat("Tried to set OverrideCustomDeathSpell to null!", Array.Empty<object>());
			return;
		}
		this.m_customDeathSpellOverride = this.SetupOverrideSpell(this.m_customDeathSpellOverride, spell);
	}

	// Token: 0x06007494 RID: 29844 RVA: 0x00255904 File Offset: 0x00253B04
	public void OverrideCustomDiscardSpell(Spell spell)
	{
		if (spell == null)
		{
			Debug.LogErrorFormat("Tried to set OverrideCustomDiscardSpell to null!", Array.Empty<object>());
			return;
		}
		this.m_customDiscardSpellOverride = this.SetupOverrideSpell(this.m_customDiscardSpellOverride, spell);
	}

	// Token: 0x06007495 RID: 29845 RVA: 0x00255932 File Offset: 0x00253B32
	public Texture GetPortraitTexture()
	{
		DefLoader.DisposableCardDef cardDef = this.m_cardDef;
		if (!(((cardDef != null) ? cardDef.CardDef : null) == null))
		{
			return this.m_cardDef.CardDef.GetPortraitTexture();
		}
		return null;
	}

	// Token: 0x06007496 RID: 29846 RVA: 0x00255960 File Offset: 0x00253B60
	public Material GetGoldenMaterial()
	{
		DefLoader.DisposableCardDef cardDef = this.m_cardDef;
		if (!(((cardDef != null) ? cardDef.CardDef : null) == null))
		{
			return this.m_cardDef.CardDef.GetPremiumPortraitMaterial();
		}
		return null;
	}

	// Token: 0x06007497 RID: 29847 RVA: 0x0025598E File Offset: 0x00253B8E
	public CardEffect GetPlayEffect(int index)
	{
		if (index <= 0)
		{
			return this.m_playEffect;
		}
		if (--index >= this.m_additionalPlayEffects.Count)
		{
			return null;
		}
		return this.m_additionalPlayEffects[index];
	}

	// Token: 0x06007498 RID: 29848 RVA: 0x002559BC File Offset: 0x00253BBC
	public CardEffect GetOrCreateProxyEffect(Network.HistBlockStart blockStart, CardEffectDef proxyEffectDef)
	{
		if (this.m_proxyEffects == null)
		{
			this.m_proxyEffects = new Map<Network.HistBlockStart, CardEffect>();
		}
		if (this.m_proxyEffects.ContainsKey(blockStart))
		{
			return this.m_proxyEffects[blockStart];
		}
		CardEffect cardEffect = new CardEffect(proxyEffectDef, this);
		this.InitEffect(proxyEffectDef, ref cardEffect);
		this.m_proxyEffects.Add(blockStart, cardEffect);
		return cardEffect;
	}

	// Token: 0x06007499 RID: 29849 RVA: 0x00255A18 File Offset: 0x00253C18
	public void DeactivatePlaySpell()
	{
		global::Entity entity = this.GetEntity();
		global::Entity parentEntity = entity.GetParentEntity();
		Spell spell;
		if (parentEntity == null)
		{
			spell = this.GetPlaySpell(0, false);
		}
		else
		{
			Card card = parentEntity.GetCard();
			int subCardIndex = parentEntity.GetSubCardIndex(entity);
			spell = card.GetSubOptionSpell(subCardIndex, 0, false);
		}
		if (spell != null && spell.GetActiveState() != SpellStateType.NONE)
		{
			spell.SafeActivateState(SpellStateType.CANCEL);
		}
	}

	// Token: 0x0600749A RID: 29850 RVA: 0x00255A74 File Offset: 0x00253C74
	public Spell GetPlaySpell(int index, bool loadIfNeeded = true)
	{
		CardEffect playEffect = this.GetPlayEffect(index);
		if (playEffect == null)
		{
			return null;
		}
		return playEffect.GetSpell(loadIfNeeded);
	}

	// Token: 0x0600749B RID: 29851 RVA: 0x00255A98 File Offset: 0x00253C98
	public List<CardSoundSpell> GetPlaySoundSpells(int index, bool loadIfNeeded = true)
	{
		CardEffect playEffect = this.GetPlayEffect(index);
		if (playEffect == null)
		{
			return null;
		}
		return playEffect.GetSoundSpells(loadIfNeeded);
	}

	// Token: 0x0600749C RID: 29852 RVA: 0x00255AB9 File Offset: 0x00253CB9
	public Spell GetAttackSpell(bool loadIfNeeded = true)
	{
		if (this.m_attackEffect == null)
		{
			return null;
		}
		return this.m_attackEffect.GetSpell(loadIfNeeded);
	}

	// Token: 0x0600749D RID: 29853 RVA: 0x00255AD1 File Offset: 0x00253CD1
	public List<CardSoundSpell> GetAttackSoundSpells(bool loadIfNeeded = true)
	{
		if (this.m_attackEffect == null)
		{
			return null;
		}
		return this.m_attackEffect.GetSoundSpells(loadIfNeeded);
	}

	// Token: 0x0600749E RID: 29854 RVA: 0x00255AE9 File Offset: 0x00253CE9
	public List<CardSoundSpell> GetDeathSoundSpells(bool loadIfNeeded = true)
	{
		if (this.m_deathEffect == null)
		{
			return null;
		}
		return this.m_deathEffect.GetSoundSpells(loadIfNeeded);
	}

	// Token: 0x0600749F RID: 29855 RVA: 0x00255B01 File Offset: 0x00253D01
	public Spell GetLifetimeSpell(bool loadIfNeeded = true)
	{
		if (this.m_lifetimeEffect == null)
		{
			return null;
		}
		return this.m_lifetimeEffect.GetSpell(loadIfNeeded);
	}

	// Token: 0x060074A0 RID: 29856 RVA: 0x00255B19 File Offset: 0x00253D19
	public List<CardSoundSpell> GetLifetimeSoundSpells(bool loadIfNeeded = true)
	{
		if (this.m_lifetimeEffect == null)
		{
			return null;
		}
		return this.m_lifetimeEffect.GetSoundSpells(loadIfNeeded);
	}

	// Token: 0x060074A1 RID: 29857 RVA: 0x00255B34 File Offset: 0x00253D34
	public CardEffect GetSubOptionEffect(int suboption, int index)
	{
		if (suboption < 0)
		{
			return null;
		}
		if (index > 0)
		{
			if (this.m_additionalSubOptionEffects == null)
			{
				return null;
			}
			if (suboption >= this.m_additionalSubOptionEffects.Count)
			{
				return null;
			}
			List<CardEffect> list = this.m_additionalSubOptionEffects[suboption];
			if (list == null)
			{
				return null;
			}
			if (--index >= list.Count)
			{
				return null;
			}
			return list[index];
		}
		else
		{
			if (this.m_subOptionEffects == null)
			{
				return null;
			}
			if (suboption >= this.m_subOptionEffects.Count)
			{
				return null;
			}
			return this.m_subOptionEffects[suboption];
		}
	}

	// Token: 0x060074A2 RID: 29858 RVA: 0x00255BB8 File Offset: 0x00253DB8
	public Spell GetSubOptionSpell(int suboption, int index, bool loadIfNeeded = true)
	{
		CardEffect subOptionEffect = this.GetSubOptionEffect(suboption, index);
		if (subOptionEffect == null)
		{
			return null;
		}
		return subOptionEffect.GetSpell(loadIfNeeded);
	}

	// Token: 0x060074A3 RID: 29859 RVA: 0x00255BDC File Offset: 0x00253DDC
	public List<CardSoundSpell> GetSubOptionSoundSpells(int suboption, int index, bool loadIfNeeded = true)
	{
		CardEffect subOptionEffect = this.GetSubOptionEffect(suboption, index);
		if (subOptionEffect == null)
		{
			return null;
		}
		return subOptionEffect.GetSoundSpells(loadIfNeeded);
	}

	// Token: 0x060074A4 RID: 29860 RVA: 0x00255BFE File Offset: 0x00253DFE
	public CardEffect GetTriggerEffect(int index)
	{
		if (this.m_triggerEffects == null)
		{
			return null;
		}
		if (index < 0)
		{
			return null;
		}
		if (index >= this.m_triggerEffects.Count)
		{
			return null;
		}
		return this.m_triggerEffects[index];
	}

	// Token: 0x060074A5 RID: 29861 RVA: 0x00255C2C File Offset: 0x00253E2C
	public CardEffect GetResetGameEffect(int index)
	{
		if (this.m_resetGameEffects == null)
		{
			return null;
		}
		if (index < 0)
		{
			return null;
		}
		if (index >= this.m_resetGameEffects.Count)
		{
			return null;
		}
		return this.m_resetGameEffects[index];
	}

	// Token: 0x060074A6 RID: 29862 RVA: 0x00255C5C File Offset: 0x00253E5C
	public Spell GetTriggerSpell(int index, bool loadIfNeeded = true)
	{
		CardEffect triggerEffect = this.GetTriggerEffect(index);
		if (triggerEffect == null)
		{
			return null;
		}
		return triggerEffect.GetSpell(loadIfNeeded);
	}

	// Token: 0x060074A7 RID: 29863 RVA: 0x00255C80 File Offset: 0x00253E80
	public List<CardSoundSpell> GetTriggerSoundSpells(int index, bool loadIfNeeded = true)
	{
		CardEffect triggerEffect = this.GetTriggerEffect(index);
		if (triggerEffect == null)
		{
			return null;
		}
		return triggerEffect.GetSoundSpells(loadIfNeeded);
	}

	// Token: 0x060074A8 RID: 29864 RVA: 0x00255CA1 File Offset: 0x00253EA1
	public Spell GetCustomKeywordSpell()
	{
		if (this.m_customKeywordEffect == null)
		{
			return null;
		}
		return this.m_customKeywordEffect.GetSpell(true);
	}

	// Token: 0x060074A9 RID: 29865 RVA: 0x00255CB9 File Offset: 0x00253EB9
	public Spell GetCustomSummonSpell()
	{
		return this.m_customSummonSpell;
	}

	// Token: 0x060074AA RID: 29866 RVA: 0x00255CC1 File Offset: 0x00253EC1
	public Spell GetCustomSpawnSpell()
	{
		return this.m_customSpawnSpell;
	}

	// Token: 0x060074AB RID: 29867 RVA: 0x00255CC9 File Offset: 0x00253EC9
	public Spell GetCustomDeathSpell()
	{
		return this.m_customDeathSpell;
	}

	// Token: 0x060074AC RID: 29868 RVA: 0x00255CD1 File Offset: 0x00253ED1
	public Spell GetCustomDeathSpellOverride()
	{
		return this.m_customDeathSpellOverride;
	}

	// Token: 0x060074AD RID: 29869 RVA: 0x00255CD9 File Offset: 0x00253ED9
	public Spell GetCustomChoiceRevealSpell()
	{
		if (this.m_customChoiceRevealEffect == null)
		{
			return null;
		}
		return this.m_customChoiceRevealEffect.GetSpell(true);
	}

	// Token: 0x060074AE RID: 29870 RVA: 0x00255CF1 File Offset: 0x00253EF1
	public Spell GetCustomChoiceConcealSpell()
	{
		if (this.m_customChoiceConcealEffect == null)
		{
			return null;
		}
		return this.m_customChoiceConcealEffect.GetSpell(true);
	}

	// Token: 0x060074AF RID: 29871 RVA: 0x00255D0C File Offset: 0x00253F0C
	public Spell GetSpellTableOverride(SpellType spellType)
	{
		CardEffect cardEffect = null;
		if (this.m_spellTableOverrideEffects.TryGetValue(spellType, out cardEffect))
		{
			return cardEffect.GetSpell(true);
		}
		foreach (SpellTableOverride spellTableOverride in this.m_cardDef.CardDef.m_SpellTableOverrides)
		{
			if (spellTableOverride.m_Type == spellType)
			{
				if (string.IsNullOrEmpty(spellTableOverride.m_SpellPrefabName))
				{
					break;
				}
				CardEffect cardEffect2 = null;
				this.InitEffect(spellTableOverride.m_SpellPrefabName, ref cardEffect2);
				if (cardEffect2 != null)
				{
					this.m_spellTableOverrideEffects[spellType] = cardEffect2;
					return cardEffect2.GetSpell(true);
				}
			}
		}
		return null;
	}

	// Token: 0x060074B0 RID: 29872 RVA: 0x00255DC4 File Offset: 0x00253FC4
	public AudioSource GetAnnouncerLine(Card.AnnouncerLineType type)
	{
		CardSound cardSound = this.m_announcerLine[(int)type];
		if (cardSound == null || cardSound.GetSound(true) == null)
		{
			if (this.m_announcerLine[0] == null)
			{
				string message = string.Format("Card.GetAnnouncerLine(AnnouncerLineType type) - Failed to load announcer audio source.", Array.Empty<object>());
				if (HearthstoneApplication.UseDevWorkarounds())
				{
					Debug.LogError(message);
				}
				return SoundManager.Get().GetPlaceholderSource();
			}
			cardSound = this.m_announcerLine[0];
		}
		return cardSound.GetSound(true);
	}

	// Token: 0x060074B1 RID: 29873 RVA: 0x00255E30 File Offset: 0x00254030
	public EmoteEntry GetEmoteEntry(EmoteType emoteType)
	{
		if (this.m_emotes == null)
		{
			return null;
		}
		bool flag = emoteType == EmoteType.GREETINGS || emoteType == EmoteType.MIRROR_GREETINGS;
		if (SpecialEventManager.Get().IsEventActive(SpecialEventType.LUNAR_NEW_YEAR, false))
		{
			if (!flag)
			{
				goto IL_284;
			}
			using (List<EmoteEntry>.Enumerator enumerator = this.m_emotes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EmoteEntry emoteEntry = enumerator.Current;
					if (emoteEntry.GetEmoteType() == EmoteType.HAPPY_NEW_YEAR_LUNAR)
					{
						return emoteEntry;
					}
				}
				goto IL_284;
			}
		}
		if (SpecialEventManager.Get().IsEventActive(SpecialEventType.FEAST_OF_WINTER_VEIL, false))
		{
			if (!flag)
			{
				goto IL_284;
			}
			using (List<EmoteEntry>.Enumerator enumerator = this.m_emotes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EmoteEntry emoteEntry2 = enumerator.Current;
					if (emoteEntry2.GetEmoteType() == EmoteType.HAPPY_HOLIDAYS)
					{
						return emoteEntry2;
					}
				}
				goto IL_284;
			}
		}
		if (SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_FIRE_FESTIVAL_EMOTES_EVERGREEN, false))
		{
			if (flag)
			{
				using (List<EmoteEntry>.Enumerator enumerator = this.m_emotes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EmoteEntry emoteEntry3 = enumerator.Current;
						if (emoteEntry3.GetEmoteType() == EmoteType.FIRE_FESTIVAL)
						{
							return emoteEntry3;
						}
					}
					goto IL_284;
				}
			}
			if (emoteType != EmoteType.WOW)
			{
				goto IL_284;
			}
			using (List<EmoteEntry>.Enumerator enumerator = this.m_emotes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EmoteEntry emoteEntry4 = enumerator.Current;
					if (emoteEntry4.GetEmoteType() == EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_THREE)
					{
						return emoteEntry4;
					}
				}
				goto IL_284;
			}
		}
		if (SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_HAPPY_NEW_YEAR, false))
		{
			if (!flag)
			{
				goto IL_284;
			}
			using (List<EmoteEntry>.Enumerator enumerator = this.m_emotes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EmoteEntry emoteEntry5 = enumerator.Current;
					if (emoteEntry5.GetEmoteType() == EmoteType.HAPPY_NEW_YEAR)
					{
						return emoteEntry5;
					}
				}
				goto IL_284;
			}
		}
		if (SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_PIRATE_DAY, false))
		{
			if (!flag)
			{
				goto IL_284;
			}
			using (List<EmoteEntry>.Enumerator enumerator = this.m_emotes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EmoteEntry emoteEntry6 = enumerator.Current;
					if (emoteEntry6.GetEmoteType() == EmoteType.PIRATE_DAY)
					{
						return emoteEntry6;
					}
				}
				goto IL_284;
			}
		}
		if (SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_NOBLEGARDEN, false) && flag)
		{
			foreach (EmoteEntry emoteEntry7 in this.m_emotes)
			{
				if (emoteEntry7.GetEmoteType() == EmoteType.HAPPY_NOBLEGARDEN)
				{
					return emoteEntry7;
				}
			}
		}
		IL_284:
		foreach (EmoteEntry emoteEntry8 in this.m_emotes)
		{
			if (emoteEntry8.GetEmoteType() == emoteType)
			{
				return emoteEntry8;
			}
		}
		return null;
	}

	// Token: 0x060074B2 RID: 29874 RVA: 0x00256168 File Offset: 0x00254368
	public Spell GetBestSummonSpell()
	{
		bool flag;
		return this.GetBestSummonSpell(out flag);
	}

	// Token: 0x060074B3 RID: 29875 RVA: 0x00256180 File Offset: 0x00254380
	public Spell GetBestSummonSpell(out bool standard)
	{
		if (this.m_customSummonSpell != null && this.GetMagneticPlayData() == null && this.GetEntity() != null && !this.GetEntity().HasTag(GAME_TAG.CARD_DOES_NOTHING) && !this.WillSuppressCustomSpells() && !this.WillSuppressCustomSummonSpells())
		{
			standard = false;
			return this.m_customSummonSpell;
		}
		standard = true;
		DefLoader.DisposableCardDef cardDef = this.m_cardDef;
		if (((cardDef != null) ? cardDef.CardDef : null) == null)
		{
			Log.Gameplay.PrintError("Cannot determine best summon spell. Missing CardDef", Array.Empty<object>());
			return null;
		}
		bool useFastAnimations = GameState.Get() != null && GameState.Get().GetGameEntity().HasTag(GAME_TAG.BACON_USE_FAST_ANIMATIONS);
		SpellType spellType = this.m_cardDef.CardDef.DetermineSummonInSpell_HandToPlay(this, useFastAnimations);
		return this.GetActorSpell(spellType, true);
	}

	// Token: 0x060074B4 RID: 29876 RVA: 0x00256244 File Offset: 0x00254444
	public Spell GetBestSpawnSpell()
	{
		bool flag;
		return this.GetBestSpawnSpell(out flag);
	}

	// Token: 0x060074B5 RID: 29877 RVA: 0x0025625C File Offset: 0x0025445C
	public Spell GetBestSpawnSpell(out bool standard)
	{
		standard = false;
		if (this.m_entity.HasTag(GAME_TAG.HAS_BEEN_REBORN))
		{
			Spell actorSpell = this.GetActorSpell(SpellType.REBORN_SPAWN, true);
			if (actorSpell != null)
			{
				return actorSpell;
			}
		}
		if (this.m_customSpawnSpellOverride)
		{
			return this.m_customSpawnSpellOverride;
		}
		if (this.m_customSpawnSpell)
		{
			return this.m_customSpawnSpell;
		}
		standard = true;
		if (this.m_entity.IsControlledByFriendlySidePlayer())
		{
			return this.GetActorSpell(SpellType.FRIENDLY_SPAWN_MINION, true);
		}
		return this.GetActorSpell(SpellType.OPPONENT_SPAWN_MINION, true);
	}

	// Token: 0x060074B6 RID: 29878 RVA: 0x002562E4 File Offset: 0x002544E4
	public Spell GetBestDeathSpell()
	{
		bool flag;
		return this.GetBestDeathSpell(out flag);
	}

	// Token: 0x060074B7 RID: 29879 RVA: 0x002562F9 File Offset: 0x002544F9
	public Spell GetBestDeathSpell(out bool standard)
	{
		return this.GetBestDeathSpell(this.m_actor, out standard);
	}

	// Token: 0x060074B8 RID: 29880 RVA: 0x00256308 File Offset: 0x00254508
	private Spell GetBestDeathSpell(Actor actor)
	{
		bool flag;
		return this.GetBestDeathSpell(actor, out flag);
	}

	// Token: 0x060074B9 RID: 29881 RVA: 0x00256320 File Offset: 0x00254520
	private Spell GetBestDeathSpell(Actor actor, out bool standard)
	{
		standard = false;
		if (this.m_prevZone is ZoneHand && this.m_zone is ZoneGraveyard)
		{
			if (this.m_customDiscardSpellOverride)
			{
				return this.m_customDiscardSpellOverride;
			}
			if (this.m_customDiscardSpell && !this.m_entity.IsSilenced())
			{
				return this.m_customDiscardSpell;
			}
		}
		else
		{
			if (this.m_customDeathSpellOverride)
			{
				return this.m_customDeathSpellOverride;
			}
			if (this.m_customDeathSpell && !this.m_entity.IsSilenced())
			{
				return this.m_customDeathSpell;
			}
		}
		standard = true;
		return actor.GetSpell(SpellType.DEATH);
	}

	// Token: 0x060074BA RID: 29882 RVA: 0x002563BE File Offset: 0x002545BE
	public void ActivateCharacterPlayEffects()
	{
		if (!this.WillSuppressPlaySounds())
		{
			this.ActivateSoundSpellList(this.m_playEffect.GetSoundSpells(true));
		}
		this.SuppressPlaySounds(false);
		this.ActivateLifetimeEffects();
	}

	// Token: 0x060074BB RID: 29883 RVA: 0x002563E8 File Offset: 0x002545E8
	public void ActivateCharacterAttackEffects()
	{
		this.ActivateSoundSpellList(this.m_attackEffect.GetSoundSpells(true));
	}

	// Token: 0x060074BC RID: 29884 RVA: 0x00256400 File Offset: 0x00254600
	public void ActivateCharacterDeathEffects()
	{
		if (this.m_suppressDeathEffects)
		{
			return;
		}
		if (!this.m_suppressDeathSounds)
		{
			int num;
			if (this.m_emotes != null)
			{
				num = this.m_emotes.FindIndex((EmoteEntry e) => e != null && e.GetEmoteType() == EmoteType.DEATH_LINE);
			}
			else
			{
				num = -1;
			}
			if (num >= 0)
			{
				this.PlayEmote(EmoteType.DEATH_LINE);
			}
			else
			{
				this.ActivateSoundSpellList(this.m_deathEffect.GetSoundSpells(true));
			}
		}
		this.m_suppressDeathSounds = false;
		this.DeactivateLifetimeEffects();
	}

	// Token: 0x060074BD RID: 29885 RVA: 0x00256484 File Offset: 0x00254684
	public void ActivateLifetimeEffects()
	{
		if (this.m_lifetimeEffect == null)
		{
			return;
		}
		if (this.m_entity.IsSilenced() || this.m_entity.HasTag(GAME_TAG.CARD_DOES_NOTHING) || this.WillSuppressCustomSpells() || this.WillSuppressCustomLifetimeSpells())
		{
			return;
		}
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		if (gameEntity != null && gameEntity.HasTag(GAME_TAG.SQUELCH_LIFETIME_EFFECTS))
		{
			return;
		}
		Spell spell = this.m_lifetimeEffect.GetSpell(true);
		if (spell != null)
		{
			spell.Deactivate();
			spell.ActivateState(SpellStateType.BIRTH);
		}
		if (this.m_lifetimeEffect.GetSoundSpells(true) != null)
		{
			this.ActivateSoundSpellList(this.m_lifetimeEffect.GetSoundSpells(true));
		}
	}

	// Token: 0x060074BE RID: 29886 RVA: 0x0025652C File Offset: 0x0025472C
	public void DeactivateLifetimeEffects()
	{
		if (this.m_lifetimeEffect == null)
		{
			return;
		}
		Spell spell = this.m_lifetimeEffect.GetSpell(true);
		if (spell != null)
		{
			SpellStateType activeState = spell.GetActiveState();
			if (activeState != SpellStateType.NONE && activeState != SpellStateType.DEATH)
			{
				spell.ActivateState(SpellStateType.DEATH);
			}
		}
	}

	// Token: 0x060074BF RID: 29887 RVA: 0x00256570 File Offset: 0x00254770
	public void ActivateCustomKeywordEffect()
	{
		if (this.m_customKeywordEffect == null)
		{
			return;
		}
		if (this.GetEntity() != null && (this.GetEntity().HasTag(GAME_TAG.CARD_DOES_NOTHING) || this.WillSuppressCustomSpells() || this.WillSuppressCustomKeywordSpells()))
		{
			return;
		}
		Spell spell = this.m_customKeywordEffect.GetSpell(true);
		if (spell == null)
		{
			Debug.LogWarning(string.Format("Card.ActivateCustomKeywordEffect() -- failed to load custom keyword spell for card {0}", this.ToString()));
			return;
		}
		if (spell.DoesBlockServerEvents())
		{
			GameState.Get().AddServerBlockingSpell(spell);
		}
		TransformUtil.AttachAndPreserveLocalTransform(spell.transform, this.m_actor.transform);
		spell.ActivateState(SpellStateType.BIRTH);
	}

	// Token: 0x060074C0 RID: 29888 RVA: 0x00256610 File Offset: 0x00254810
	public void DeactivateCustomKeywordEffect()
	{
		if (this.m_customKeywordEffect == null)
		{
			return;
		}
		Spell spell = this.m_customKeywordEffect.GetSpell(false);
		if (spell == null)
		{
			return;
		}
		if (!spell.IsActive())
		{
			return;
		}
		spell.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x060074C1 RID: 29889 RVA: 0x00256650 File Offset: 0x00254850
	public bool ActivateSoundSpellList(List<CardSoundSpell> soundSpells)
	{
		if (soundSpells == null)
		{
			return false;
		}
		if (soundSpells.Count == 0)
		{
			return false;
		}
		bool result = false;
		for (int i = 0; i < soundSpells.Count; i++)
		{
			CardSoundSpell soundSpell = soundSpells[i];
			this.ActivateSoundSpell(soundSpell);
			result = true;
		}
		return result;
	}

	// Token: 0x060074C2 RID: 29890 RVA: 0x00256694 File Offset: 0x00254894
	public bool ActivateSoundSpell(CardSoundSpell soundSpell)
	{
		if (soundSpell == null || this.GetEntity().HasTag(GAME_TAG.CARD_DOES_NOTHING))
		{
			return false;
		}
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		if (gameEntity == null)
		{
			return false;
		}
		if (gameEntity.GetGameOptions().GetBooleanOption(GameEntityOption.DELAY_CARD_SOUND_SPELLS))
		{
			base.StartCoroutine(this.WaitThenActivateSoundSpell(soundSpell));
		}
		else
		{
			soundSpell.Reactivate();
		}
		return true;
	}

	// Token: 0x060074C3 RID: 29891 RVA: 0x002566F4 File Offset: 0x002548F4
	public bool HasActiveEmoteSound()
	{
		if (this.m_emotes == null)
		{
			return false;
		}
		foreach (EmoteEntry emoteEntry in this.m_emotes)
		{
			CardSoundSpell soundSpell = emoteEntry.GetSoundSpell(false);
			if (soundSpell != null && soundSpell.IsActive())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060074C4 RID: 29892 RVA: 0x00256768 File Offset: 0x00254968
	public EmoteEntry GetActiveEmoteSound()
	{
		if (this.m_emotes == null)
		{
			return null;
		}
		foreach (EmoteEntry emoteEntry in this.m_emotes)
		{
			CardSoundSpell soundSpell = emoteEntry.GetSoundSpell(false);
			if (soundSpell != null && soundSpell.IsActive())
			{
				return emoteEntry;
			}
		}
		return null;
	}

	// Token: 0x060074C5 RID: 29893 RVA: 0x002567E0 File Offset: 0x002549E0
	public bool HasUnfinishedEmoteSpell()
	{
		if (this.m_emotes == null)
		{
			return false;
		}
		foreach (EmoteEntry emoteEntry in this.m_emotes)
		{
			Spell spell = emoteEntry.GetSpell(false);
			if (spell != null && !spell.IsFinished())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060074C6 RID: 29894 RVA: 0x00256854 File Offset: 0x00254A54
	public CardSoundSpell PlayEmote(EmoteType emoteType)
	{
		return this.PlayEmote(emoteType, Notification.SpeechBubbleDirection.None);
	}

	// Token: 0x060074C7 RID: 29895 RVA: 0x00256860 File Offset: 0x00254A60
	public CardSoundSpell PlayEmote(EmoteType emoteType, Notification.SpeechBubbleDirection overrideDirection)
	{
		EmoteEntry emoteEntry = this.GetEmoteEntry(emoteType);
		CardSoundSpell cardSoundSpell = (emoteEntry != null) ? emoteEntry.GetSoundSpell(true) : null;
		Spell spell = (emoteEntry != null) ? emoteEntry.GetSpell(true) : null;
		if (!this.m_entity.IsHero())
		{
			return null;
		}
		if (this.m_actor == null)
		{
			return null;
		}
		if (cardSoundSpell != null)
		{
			cardSoundSpell.Reactivate();
			if (cardSoundSpell.IsActive())
			{
				for (int i = 0; i < this.m_emotes.Count; i++)
				{
					EmoteEntry emoteEntry2 = this.m_emotes[i];
					if (emoteEntry2 != emoteEntry)
					{
						Spell soundSpell = emoteEntry2.GetSoundSpell(false);
						if (soundSpell)
						{
							soundSpell.Deactivate();
						}
					}
				}
			}
			GameState.Get().GetGameEntity().OnEmotePlayed(this, emoteType, cardSoundSpell);
		}
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.BottomLeft;
		if (this.GetEntity().IsControlledByOpposingSidePlayer())
		{
			direction = Notification.SpeechBubbleDirection.TopRight;
		}
		if (overrideDirection != Notification.SpeechBubbleDirection.None)
		{
			direction = overrideDirection;
		}
		string text = null;
		if (cardSoundSpell != null)
		{
			text = string.Empty;
			if (cardSoundSpell is CardSpecificVoSpell)
			{
				CardSpecificVoData bestVoiceData = ((CardSpecificVoSpell)cardSoundSpell).GetBestVoiceData();
				if (bestVoiceData != null && !string.IsNullOrEmpty(bestVoiceData.m_GameStringKey))
				{
					text = GameStrings.Get(bestVoiceData.m_GameStringKey);
				}
			}
		}
		if (string.IsNullOrEmpty(text) && emoteEntry != null && !string.IsNullOrEmpty(emoteEntry.GetGameStringKey()))
		{
			text = GameStrings.Get(emoteEntry.GetGameStringKey());
		}
		Notification notification = null;
		if (!string.IsNullOrEmpty(text))
		{
			notification = NotificationManager.Get().CreateSpeechBubble(text, direction, this.m_actor, true, true, 0f);
			float num = 1.5f;
			if (cardSoundSpell)
			{
				AudioSource activeAudioSource = cardSoundSpell.GetActiveAudioSource();
				if (activeAudioSource && activeAudioSource.clip && num < activeAudioSource.clip.length)
				{
					num = activeAudioSource.clip.length;
				}
			}
			NotificationManager.Get().DestroyNotification(notification, num);
		}
		if (spell != null)
		{
			VisualEmoteSpell visualEmoteSpell = spell as VisualEmoteSpell;
			if (visualEmoteSpell != null && visualEmoteSpell.m_PositionOnSpeechBubble && notification != null)
			{
				visualEmoteSpell.SetSource(notification.gameObject);
				visualEmoteSpell.Reactivate();
			}
			else
			{
				spell.Reactivate();
			}
		}
		return cardSoundSpell;
	}

	// Token: 0x060074C8 RID: 29896 RVA: 0x00256A74 File Offset: 0x00254C74
	private void InitCardDefAssets()
	{
		this.InitEffect(this.m_cardDef.CardDef.m_PlayEffectDef, ref this.m_playEffect);
		this.InitEffectList(this.m_cardDef.CardDef.m_AdditionalPlayEffectDefs, ref this.m_additionalPlayEffects);
		this.InitEffect(this.m_cardDef.CardDef.m_AttackEffectDef, ref this.m_attackEffect);
		this.InitEffect(this.m_cardDef.CardDef.m_DeathEffectDef, ref this.m_deathEffect);
		this.InitEffect(this.m_cardDef.CardDef.m_LifetimeEffectDef, ref this.m_lifetimeEffect);
		this.InitEffect(this.m_cardDef.CardDef.m_CustomKeywordSpellPath, ref this.m_customKeywordEffect);
		this.InitEffect(this.m_cardDef.CardDef.m_CustomChoiceRevealSpellPath, ref this.m_customChoiceRevealEffect);
		this.InitEffect(this.m_cardDef.CardDef.m_CustomChoiceConcealSpellPath, ref this.m_customChoiceConcealEffect);
		this.InitEffectList(this.m_cardDef.CardDef.m_SubOptionEffectDefs, ref this.m_subOptionEffects);
		this.InitEffectListList(this.m_cardDef.CardDef.m_AdditionalSubOptionEffectDefs, ref this.m_additionalSubOptionEffects);
		this.InitEffectList(this.m_cardDef.CardDef.m_TriggerEffectDefs, ref this.m_triggerEffects);
		this.InitEffectList(this.m_cardDef.CardDef.m_ResetGameEffectDefs, ref this.m_resetGameEffects);
		this.InitSound(this.m_cardDef.CardDef.m_AnnouncerLinePath, ref this.m_announcerLine[0], true);
		this.InitSound(this.m_cardDef.CardDef.m_AnnouncerLineBeforeVersusPath, ref this.m_announcerLine[1], false);
		this.InitSound(this.m_cardDef.CardDef.m_AnnouncerLineAfterVersusPath, ref this.m_announcerLine[2], false);
		this.InitEmoteList();
	}

	// Token: 0x060074C9 RID: 29897 RVA: 0x00256C40 File Offset: 0x00254E40
	private void InitEffect(CardEffectDef effectDef, ref CardEffect effect)
	{
		this.DestroyCardEffect(ref effect);
		if (effectDef == null)
		{
			return;
		}
		effect = new CardEffect(effectDef, this);
		if (this.m_allEffects == null)
		{
			this.m_allEffects = new List<CardEffect>();
		}
		this.m_allEffects.Add(effect);
		if (this.ShouldPreloadCardAssets())
		{
			effect.LoadAll();
		}
	}

	// Token: 0x060074CA RID: 29898 RVA: 0x00256C90 File Offset: 0x00254E90
	private void InitEffect(string spellPath, ref CardEffect effect)
	{
		this.DestroyCardEffect(ref effect);
		if (string.IsNullOrEmpty(spellPath))
		{
			return;
		}
		effect = new CardEffect(spellPath, this);
		if (this.m_allEffects == null)
		{
			this.m_allEffects = new List<CardEffect>();
		}
		this.m_allEffects.Add(effect);
		if (this.ShouldPreloadCardAssets())
		{
			effect.LoadAll();
		}
	}

	// Token: 0x060074CB RID: 29899 RVA: 0x00256CE8 File Offset: 0x00254EE8
	private void InitEffectList(List<CardEffectDef> effectDefs, ref List<CardEffect> effects)
	{
		this.DestroyCardEffectList(ref effects);
		if (effectDefs == null)
		{
			return;
		}
		effects = new List<CardEffect>();
		for (int i = 0; i < effectDefs.Count; i++)
		{
			CardEffectDef cardEffectDef = effectDefs[i];
			CardEffect cardEffect = null;
			if (cardEffectDef != null)
			{
				cardEffect = new CardEffect(cardEffectDef, this);
				if (this.m_allEffects == null)
				{
					this.m_allEffects = new List<CardEffect>();
				}
				this.m_allEffects.Add(cardEffect);
				if (this.ShouldPreloadCardAssets())
				{
					cardEffect.LoadAll();
				}
			}
			effects.Add(cardEffect);
		}
	}

	// Token: 0x060074CC RID: 29900 RVA: 0x00256D64 File Offset: 0x00254F64
	private void InitEffectListList(List<List<CardEffectDef>> effectDefs, ref List<List<CardEffect>> effects)
	{
		if (effects != null)
		{
			for (int i = 0; i < effects.Count; i++)
			{
				List<CardEffect> list = effects[i];
				this.DestroyCardEffectList(ref list);
			}
			effects = null;
		}
		if (effectDefs != null)
		{
			effects = new List<List<CardEffect>>();
			for (int j = 0; j < effectDefs.Count; j++)
			{
				List<CardEffect> list2 = effects[j];
				this.InitEffectList(effectDefs[j], ref list2);
			}
		}
	}

	// Token: 0x060074CD RID: 29901 RVA: 0x00256DCE File Offset: 0x00254FCE
	private void InitSound(string path, ref CardSound cardSound, bool alwaysValid)
	{
		this.DestroyCardSound(ref cardSound);
		if (string.IsNullOrEmpty(path))
		{
			return;
		}
		cardSound = new CardSound(path, this, alwaysValid);
		if (this.ShouldPreloadCardAssets())
		{
			cardSound.GetSound(true);
		}
	}

	// Token: 0x060074CE RID: 29902 RVA: 0x00256DFC File Offset: 0x00254FFC
	private void InitEmoteList()
	{
		this.DestroyEmoteList();
		if (this.m_cardDef.CardDef.m_EmoteDefs == null)
		{
			return;
		}
		this.m_emotes = new List<EmoteEntry>();
		for (int i = 0; i < this.m_cardDef.CardDef.m_EmoteDefs.Count; i++)
		{
			EmoteEntryDef emoteEntryDef = this.m_cardDef.CardDef.m_EmoteDefs[i];
			EmoteEntry emoteEntry = new EmoteEntry(emoteEntryDef.m_emoteType, emoteEntryDef.m_emoteSpellPath, emoteEntryDef.m_emoteSoundSpellPath, emoteEntryDef.m_emoteGameStringKey, this);
			if (this.ShouldPreloadCardAssets())
			{
				emoteEntry.GetSoundSpell(true);
				emoteEntry.GetSpell(true);
			}
			this.m_emotes.Add(emoteEntry);
		}
	}

	// Token: 0x060074CF RID: 29903 RVA: 0x00256EA8 File Offset: 0x002550A8
	private Spell SetupOverrideSpell(Spell existingSpell, Spell spell)
	{
		if (existingSpell != null)
		{
			if (existingSpell.IsActive())
			{
				Log.Gameplay.PrintError("destroying active spell {0} currently in state {1} with source card {2}.", new object[]
				{
					existingSpell,
					existingSpell.GetActiveState(),
					existingSpell.GetSourceCard()
				});
			}
			UnityEngine.Object.Destroy(existingSpell.gameObject);
		}
		SpellUtils.SetupSpell(spell, this);
		return spell;
	}

	// Token: 0x060074D0 RID: 29904 RVA: 0x00256F09 File Offset: 0x00255109
	private void ReleaseAssets()
	{
		this.ReleaseCardDef();
		this.DestroyCardDefAssets();
	}

	// Token: 0x060074D1 RID: 29905 RVA: 0x00256F17 File Offset: 0x00255117
	private void ReleaseCardDef()
	{
		DefLoader.DisposableCardDef cardDef = this.m_cardDef;
		if (cardDef != null)
		{
			cardDef.Dispose();
		}
		this.m_cardDef = null;
	}

	// Token: 0x060074D2 RID: 29906 RVA: 0x00256F34 File Offset: 0x00255134
	private void DestroyCardDefAssets()
	{
		this.DestroyCardEffect(ref this.m_playEffect);
		this.DestroyCardEffect(ref this.m_attackEffect);
		this.DestroyCardEffect(ref this.m_deathEffect);
		this.DestroyCardEffect(ref this.m_lifetimeEffect);
		this.DestroyCardEffectList(ref this.m_subOptionEffects);
		this.DestroyCardEffectList(ref this.m_triggerEffects);
		this.DestroyCardEffectList(ref this.m_resetGameEffects);
		foreach (CardEffect cardEffect in this.m_spellTableOverrideEffects.Values)
		{
			cardEffect.Clear();
		}
		this.m_spellTableOverrideEffects.Clear();
		if (this.m_proxyEffects != null)
		{
			List<CardEffect> list = new List<CardEffect>(this.m_proxyEffects.Values);
			this.DestroyCardEffectList(ref list);
			this.m_proxyEffects.Clear();
		}
		this.DestroyCardEffect(ref this.m_customKeywordEffect);
		this.DestroyCardEffect(ref this.m_customChoiceRevealEffect);
		this.DestroyCardEffect(ref this.m_customChoiceConcealEffect);
		for (int i = 0; i < this.m_announcerLine.Count<CardSound>(); i++)
		{
			this.DestroyCardSound(ref this.m_announcerLine[i]);
		}
		this.DestroyEmoteList();
		this.DestroyCardAsset<Spell>(ref this.m_customSummonSpell);
		this.DestroyCardAsset<Spell>(ref this.m_customSpawnSpell);
		this.DestroyCardAsset<Spell>(ref this.m_customSpawnSpellOverride);
		this.DestroyCardAsset<Spell>(ref this.m_customDeathSpell);
		this.DestroyCardAsset<Spell>(ref this.m_customDeathSpellOverride);
		this.DestroyCardAsset<Spell>(ref this.m_customDiscardSpell);
		this.DestroyCardAsset<Spell>(ref this.m_customDiscardSpellOverride);
	}

	// Token: 0x060074D3 RID: 29907 RVA: 0x002570BC File Offset: 0x002552BC
	public void DestroyCardDefAssetsOnEntityChanged()
	{
		this.DeactivateLifetimeEffects();
		this.DestroyCardAsset<Spell>(ref this.m_customDeathSpell);
		this.DestroyCardEffect(ref this.m_lifetimeEffect);
	}

	// Token: 0x060074D4 RID: 29908 RVA: 0x002570DC File Offset: 0x002552DC
	private void DestroyCardEffect(ref CardEffect effect)
	{
		if (effect == null)
		{
			return;
		}
		effect.PurgeSpells();
		effect = null;
	}

	// Token: 0x060074D5 RID: 29909 RVA: 0x002570ED File Offset: 0x002552ED
	private void DestroyCardSound(ref CardSound cardSound)
	{
		if (cardSound == null)
		{
			return;
		}
		cardSound.Clear();
		cardSound = null;
	}

	// Token: 0x060074D6 RID: 29910 RVA: 0x00257100 File Offset: 0x00255300
	private void DestroyCardEffectList(ref List<CardEffect> effects)
	{
		if (effects == null)
		{
			return;
		}
		foreach (CardEffect cardEffect in effects)
		{
			cardEffect.PurgeSpells();
		}
		effects = null;
	}

	// Token: 0x060074D7 RID: 29911 RVA: 0x00257154 File Offset: 0x00255354
	private void DestroyCardAsset<T>(ref T asset) where T : Component
	{
		if (asset == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(asset.gameObject);
		asset = default(T);
	}

	// Token: 0x060074D8 RID: 29912 RVA: 0x00257182 File Offset: 0x00255382
	private void DestroyCardAsset<T>(T asset) where T : Component
	{
		if (asset == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(asset.gameObject);
	}

	// Token: 0x060074D9 RID: 29913 RVA: 0x002571A4 File Offset: 0x002553A4
	private void DestroySpellList<T>(List<T> spells) where T : Spell
	{
		if (spells == null)
		{
			return;
		}
		for (int i = 0; i < spells.Count; i++)
		{
			this.DestroyCardAsset<T>(spells[i]);
		}
		spells = null;
	}

	// Token: 0x060074DA RID: 29914 RVA: 0x002571D8 File Offset: 0x002553D8
	private void DestroyEmoteList()
	{
		if (this.m_emotes == null)
		{
			return;
		}
		for (int i = 0; i < this.m_emotes.Count; i++)
		{
			this.m_emotes[i].Clear();
		}
		this.m_emotes = null;
	}

	// Token: 0x060074DB RID: 29915 RVA: 0x0025721C File Offset: 0x0025541C
	public void CancelActiveSpells()
	{
		SpellUtils.ActivateCancelIfNecessary(this.GetPlaySpell(0, false));
		if (this.m_subOptionEffects != null)
		{
			foreach (CardEffect cardEffect in this.m_subOptionEffects)
			{
				SpellUtils.ActivateCancelIfNecessary(cardEffect.GetSpell(false));
			}
		}
		if (this.m_triggerEffects != null)
		{
			foreach (CardEffect cardEffect2 in this.m_triggerEffects)
			{
				SpellUtils.ActivateCancelIfNecessary(cardEffect2.GetSpell(false));
			}
		}
	}

	// Token: 0x060074DC RID: 29916 RVA: 0x002572D8 File Offset: 0x002554D8
	public void CancelCustomSpells()
	{
		SpellUtils.ActivateCancelIfNecessary(this.m_customSummonSpell);
		SpellUtils.ActivateCancelIfNecessary(this.m_customSpawnSpell);
		SpellUtils.ActivateCancelIfNecessary(this.m_customSpawnSpellOverride);
		SpellUtils.ActivateCancelIfNecessary(this.m_customDeathSpell);
		SpellUtils.ActivateCancelIfNecessary(this.m_customDeathSpellOverride);
		SpellUtils.ActivateCancelIfNecessary(this.m_customDiscardSpell);
		SpellUtils.ActivateCancelIfNecessary(this.m_customDiscardSpellOverride);
	}

	// Token: 0x060074DD RID: 29917 RVA: 0x00257339 File Offset: 0x00255539
	private IEnumerator WaitThenActivateSoundSpell(CardSoundSpell soundSpell)
	{
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		while (gameEntity.GetGameOptions().GetBooleanOption(GameEntityOption.DELAY_CARD_SOUND_SPELLS))
		{
			yield return null;
		}
		soundSpell.Reactivate();
		yield break;
	}

	// Token: 0x060074DE RID: 29918 RVA: 0x00257348 File Offset: 0x00255548
	public void OnTagsChanged(TagDeltaList changeList, bool fromShowEntity)
	{
		bool flag = false;
		int i = 0;
		while (i < changeList.Count)
		{
			TagDelta tagDelta = changeList[i];
			GAME_TAG tag = (GAME_TAG)tagDelta.tag;
			if (tag <= GAME_TAG.DURABILITY)
			{
				if (tag != GAME_TAG.HEALTH && tag - GAME_TAG.ATK > 1 && tag != GAME_TAG.DURABILITY)
				{
					goto IL_61;
				}
				goto IL_5D;
			}
			else if (tag <= GAME_TAG.HEALTH_DISPLAY)
			{
				if (tag != GAME_TAG.ARMOR && tag != GAME_TAG.HEALTH_DISPLAY)
				{
					goto IL_61;
				}
				goto IL_5D;
			}
			else
			{
				if (tag == GAME_TAG.ENABLE_HEALTH_DISPLAY || tag == GAME_TAG.HEALTH_DISPLAY_COLOR)
				{
					goto IL_5D;
				}
				goto IL_61;
			}
			IL_69:
			i++;
			continue;
			IL_5D:
			flag = true;
			goto IL_69;
			IL_61:
			this.OnTagChanged(tagDelta, fromShowEntity);
			goto IL_69;
		}
		if (flag && !this.m_entity.IsLoadingAssets() && this.IsActorReady())
		{
			this.UpdateActorComponents();
		}
	}

	// Token: 0x060074DF RID: 29919 RVA: 0x002573EC File Offset: 0x002555EC
	public void OnMetaData(Network.HistMetaData metaData)
	{
		if (metaData.MetaType == HistoryMeta.Type.DAMAGE || metaData.MetaType == HistoryMeta.Type.HEALING || metaData.MetaType == HistoryMeta.Type.POISONOUS)
		{
			if (!this.CanShowActorVisuals())
			{
				return;
			}
			if (this.m_entity.GetZone() != TAG_ZONE.PLAY)
			{
				return;
			}
			Spell actorSpell = this.GetActorSpell(SpellType.DAMAGE, true);
			if (actorSpell == null)
			{
				this.UpdateActorComponents();
				return;
			}
			actorSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnSpellFinished_UpdateActorComponents));
			if (this.m_entity.IsCharacter())
			{
				int damage = (metaData.MetaType == HistoryMeta.Type.HEALING) ? (-metaData.Data) : metaData.Data;
				DamageSplatSpell damageSplatSpell = (DamageSplatSpell)actorSpell;
				damageSplatSpell.SetDamage(damage);
				if (metaData.MetaType == HistoryMeta.Type.POISONOUS)
				{
					if (damageSplatSpell.IsPoisonous())
					{
						return;
					}
					damageSplatSpell.SetPoisonous(true);
				}
				else
				{
					damageSplatSpell.SetPoisonous(false);
				}
				actorSpell.ActivateState(SpellStateType.ACTION);
				BoardEvents boardEvents = BoardEvents.Get();
				if (boardEvents != null)
				{
					if (metaData.MetaType == HistoryMeta.Type.HEALING)
					{
						boardEvents.HealEvent(this, (float)(-(float)metaData.Data));
						return;
					}
					boardEvents.DamageEvent(this, (float)metaData.Data);
					return;
				}
			}
			else
			{
				actorSpell.Activate();
			}
		}
	}

	// Token: 0x060074E0 RID: 29920 RVA: 0x002574FC File Offset: 0x002556FC
	public void HandleCardExhaustedTagChanged(TagDelta change)
	{
		if (this.m_entity.IsSecret())
		{
			if (!this.CanShowSecretActorVisuals())
			{
				return;
			}
		}
		else if (!this.CanShowActorVisuals())
		{
			return;
		}
		if (this.m_entity.IsHeroPower() && this.m_entity.GetController() != null && this.m_entity.GetController().GetTag(GAME_TAG.HERO_POWER_DISABLED) != 0)
		{
			change.newValue = 1;
		}
		if (change.newValue == change.oldValue)
		{
			return;
		}
		if (GameState.Get().IsTurnStartManagerActive() && this.m_entity.IsControlledByFriendlySidePlayer())
		{
			TurnStartManager.Get().NotifyOfExhaustedChange(this, change);
			return;
		}
		this.ShowExhaustedChange(change.newValue);
	}

	// Token: 0x060074E1 RID: 29921 RVA: 0x002575A4 File Offset: 0x002557A4
	public void OnTagChanged(TagDelta change, bool fromShowEntity)
	{
		if (TagVisualConfiguration.Get() != null)
		{
			TagVisualConfiguration.Get().ProcessTagChange((GAME_TAG)change.tag, this, fromShowEntity, change);
		}
		int tag = change.tag;
		this.m_entity.GetCardTextBuilder().OnTagChange(this, change);
		if (this.m_actor != null)
		{
			this.m_actor.UpdateDiamondCardArt();
		}
	}

	// Token: 0x060074E2 RID: 29922 RVA: 0x00257604 File Offset: 0x00255804
	public void ActivateDormantStateVisual()
	{
		this.m_actor.ActivateSpellBirthState(SpellType.DORMANT);
		if (this.m_entity.IsFrozen())
		{
			this.m_actor.ActivateSpellDeathState(SpellType.FROZEN);
		}
		if (this.m_entity.IsSilenced())
		{
			this.m_actor.ActivateSpellDeathState(SpellType.SILENCE);
		}
		this.DeactivateLifetimeEffects();
	}

	// Token: 0x060074E3 RID: 29923 RVA: 0x0025765C File Offset: 0x0025585C
	public void DeactivateDormantStateVisual()
	{
		this.m_actor.ActivateSpellDeathState(SpellType.DORMANT);
		if (this.m_entity.IsFrozen())
		{
			this.m_actor.ActivateSpellBirthState(SpellType.FROZEN);
		}
		if (this.m_entity.IsSilenced())
		{
			this.m_actor.ActivateSpellBirthState(SpellType.SILENCE);
		}
		this.ActivateLifetimeEffects();
		this.ActivateActorSpell(SpellType.AWAKEN_FROM_DORMANT);
		if (this.m_entity.IsControlledByFriendlySidePlayer())
		{
			if (this.m_entity.GetRealTimeSpellpower() > 0 || this.m_entity.GetRealTimeSpellpowerDouble())
			{
				ZoneMgr.Get().OnSpellPowerEntityEnteredPlay(this.m_entity.GetSpellPowerSchool());
			}
			if (this.m_entity.GetRealTimeHealingDoeDamageHint())
			{
				ZoneMgr.Get().OnHealingDoesDamageEntityEnteredPlay();
			}
			if (this.m_entity.GetRealTimeLifestealDoesDamageHint())
			{
				ZoneMgr.Get().OnLifestealDoesDamageEntityEnteredPlay();
			}
		}
		if (this.m_entity.IsAsleep())
		{
			this.m_actor.ActivateSpellBirthState(SpellType.Zzz);
		}
	}

	// Token: 0x060074E4 RID: 29924 RVA: 0x00257748 File Offset: 0x00255948
	public void UpdateQuestUI()
	{
		if (this.m_entity == null || !this.m_entity.IsQuest())
		{
			return;
		}
		if (this.m_actor == null)
		{
			return;
		}
		QuestController component = this.m_actor.GetComponent<QuestController>();
		if (component == null)
		{
			Log.Gameplay.PrintError("Quest card {0} does not have a QuestController component.", new object[]
			{
				this
			});
			return;
		}
		component.UpdateQuestUI();
	}

	// Token: 0x060074E5 RID: 29925 RVA: 0x002577B0 File Offset: 0x002559B0
	public void UpdateSideQuestUI(bool allowQuestComplete)
	{
		if (this.m_entity == null || !this.m_entity.IsSideQuest())
		{
			return;
		}
		if (this.m_actor == null)
		{
			return;
		}
		SideQuestController component = this.m_actor.GetComponent<SideQuestController>();
		if (component == null)
		{
			Log.Gameplay.PrintError("SideQuest card {0} does not have a SideQuestController component.", new object[]
			{
				this
			});
			return;
		}
		component.UpdateQuestUI(allowQuestComplete);
	}

	// Token: 0x060074E6 RID: 29926 RVA: 0x00257818 File Offset: 0x00255A18
	public void UpdatePuzzleUI()
	{
		if (this.m_entity == null || !this.m_entity.IsPuzzle())
		{
			return;
		}
		if (this.m_actor == null)
		{
			return;
		}
		PuzzleController component = this.m_actor.GetComponent<PuzzleController>();
		if (component == null)
		{
			Log.Gameplay.PrintError("Puzzle card {0} does not have a PuzzleController component.", new object[]
			{
				this
			});
			return;
		}
		component.UpdatePuzzleUI();
	}

	// Token: 0x060074E7 RID: 29927 RVA: 0x00257880 File Offset: 0x00255A80
	public void UpdateCardCostHealth(TagDelta change)
	{
		if (this.m_entity.IsControlledByFriendlySidePlayer())
		{
			Card mousedOverCard = InputManager.Get().GetMousedOverCard();
			if (mousedOverCard != null)
			{
				global::Entity entity = mousedOverCard.GetEntity();
				if (entity == this.m_entity)
				{
					if (change.newValue > 0)
					{
						ManaCrystalMgr.Get().CancelAllProposedMana(entity);
					}
					else
					{
						ManaCrystalMgr.Get().ProposeManaCrystalUsage(entity);
					}
				}
			}
		}
		if (this.CanShowActorVisuals() && change.newValue > 0)
		{
			this.m_actor.ActivateSpellBirthState(SpellType.SPELLS_COST_HEALTH);
			return;
		}
		this.m_actor.ActivateSpellDeathState(SpellType.SPELLS_COST_HEALTH);
	}

	// Token: 0x060074E8 RID: 29928 RVA: 0x0025790C File Offset: 0x00255B0C
	public bool CanShowActorVisuals()
	{
		return !this.m_entity.IsLoadingAssets() && !(this.m_actor == null) && this.m_actor.IsShown();
	}

	// Token: 0x060074E9 RID: 29929 RVA: 0x0025793D File Offset: 0x00255B3D
	private bool CanShowSecretActorVisuals()
	{
		return !this.m_entity.IsLoadingAssets() && !(this.m_actor == null) && (!this.m_actorReady || this.m_actor.IsShown());
	}

	// Token: 0x060074EA RID: 29930 RVA: 0x00257976 File Offset: 0x00255B76
	public bool ShouldShowImmuneVisuals()
	{
		return this.m_entity != null && this.m_entity.HasTag(GAME_TAG.IMMUNE) && !this.m_entity.HasTag(GAME_TAG.DONT_SHOW_IMMUNE);
	}

	// Token: 0x060074EB RID: 29931 RVA: 0x002579A8 File Offset: 0x00255BA8
	public void ActivateStateSpells(bool forceActivate = false)
	{
		if (this.m_actor == null)
		{
			return;
		}
		if (this.m_entity.GetController() != null && !this.m_entity.GetController().IsFriendlySide() && this.m_entity.IsObfuscated())
		{
			return;
		}
		if (this.m_entity != null && this.m_entity.IsHeroPower())
		{
			this.UpdateHeroPowerRelatedVisual();
		}
		TagVisualConfiguration.Get().ActivateStateSpells(this);
		TAG_ZONE tag_ZONE = (this.GetZone() != null) ? this.GetZone().m_ServerTag : TAG_ZONE.SETASIDE;
		if (tag_ZONE == TAG_ZONE.HAND)
		{
			this.ActivateHandStateSpells(forceActivate);
			return;
		}
		if (this.m_entity != null && (tag_ZONE == TAG_ZONE.PLAY || tag_ZONE == TAG_ZONE.SECRET))
		{
			bool exhausted = this.m_entity.IsExhausted();
			if (this.m_entity.IsHeroPower() && this.m_entity.GetController() != null && this.m_entity.GetController().HasTag(GAME_TAG.HERO_POWER_DISABLED))
			{
				exhausted = true;
			}
			this.ShowExhaustedChange(exhausted);
		}
	}

	// Token: 0x060074EC RID: 29932 RVA: 0x00257A98 File Offset: 0x00255C98
	public void UpdateHeroPowerRelatedVisual()
	{
		if (!this.m_entity.IsHeroPower())
		{
			return;
		}
		global::Player controller = this.m_entity.GetController();
		if (controller == null)
		{
			return;
		}
		if (controller.HasTag(GAME_TAG.STEADY_SHOT_CAN_TARGET) && this.m_entity.GetClasses(null).Contains(TAG_CLASS.HUNTER))
		{
			this.m_actor.ActivateSpellBirthState(SpellType.STEADY_SHOT_CAN_TARGET);
		}
		else
		{
			this.m_actor.ActivateSpellDeathState(SpellType.STEADY_SHOT_CAN_TARGET);
		}
		if (controller.HasTag(GAME_TAG.CURRENT_HEROPOWER_DAMAGE_BONUS) && controller.IsHeroPowerAffectedByBonusDamage())
		{
			this.m_actor.ActivateSpellBirthState(SpellType.CURRENT_HEROPOWER_DAMAGE_BONUS);
			return;
		}
		this.m_actor.ActivateSpellDeathState(SpellType.CURRENT_HEROPOWER_DAMAGE_BONUS);
	}

	// Token: 0x060074ED RID: 29933 RVA: 0x00257B34 File Offset: 0x00255D34
	public void ActivateHandStateSpells(bool forceActivate = false)
	{
		this.m_entity.GetController();
		if ((this.m_entity.IsHeroPowerOrGameModeButton() || this.m_entity.IsSpell()) && this.m_playEffect != null)
		{
			SpellUtils.ActivateCancelIfNecessary(this.m_playEffect.GetSpell(false));
		}
		if (this.m_entity.IsSpell())
		{
			SpellUtils.ActivateCancelIfNecessary(this.GetActorSpell(SpellType.POWER_UP, false));
		}
		if (TagVisualConfiguration.Get() != null)
		{
			TagVisualConfiguration.Get().ActivateHandStateSpells(this, forceActivate);
		}
	}

	// Token: 0x060074EE RID: 29934 RVA: 0x00257BB8 File Offset: 0x00255DB8
	public void DeactivateHandStateSpells(Actor actor = null)
	{
		if (actor == null)
		{
			if (this.m_actor == null)
			{
				return;
			}
			actor = this.m_actor;
		}
		if (TagVisualConfiguration.Get() != null)
		{
			TagVisualConfiguration.Get().DeactivateHandStateSpells(this, actor);
		}
		if (actor.UseTechLevelManaGem())
		{
			actor.DestroySpell(SpellType.TECH_LEVEL_MANA_GEM);
		}
		if (actor.UseCoinManaGem())
		{
			actor.DestroySpell(SpellType.COIN_MANA_GEM);
		}
	}

	// Token: 0x060074EF RID: 29935 RVA: 0x00257C24 File Offset: 0x00255E24
	public void ActivateActorArmsDealingSpell()
	{
		if (!this.CardStandInIsInteractive())
		{
			Spell spell = this.m_actor.GetSpell(SpellType.ARMS_DEALING);
			if (spell != null)
			{
				spell.ActivateState(SpellStateType.IDLE);
			}
			return;
		}
		PowerTaskList currentTaskList = GameState.Get().GetPowerProcessor().GetCurrentTaskList();
		if (currentTaskList != null && currentTaskList.IsBlock())
		{
			base.StartCoroutine(this.WaitPowerTaskListAndActivateArmsDealing(currentTaskList));
			return;
		}
		this.m_actor.ActivateSpellBirthState(SpellType.ARMS_DEALING);
	}

	// Token: 0x060074F0 RID: 29936 RVA: 0x00257C90 File Offset: 0x00255E90
	private IEnumerator WaitPowerTaskListAndActivateArmsDealing(PowerTaskList curPowerTaskList)
	{
		while (!curPowerTaskList.IsComplete())
		{
			yield return null;
		}
		if (this.GetZone() is ZoneHand)
		{
			this.m_actor.ActivateSpellBirthState(SpellType.ARMS_DEALING);
		}
		yield break;
	}

	// Token: 0x060074F1 RID: 29937 RVA: 0x00257CA6 File Offset: 0x00255EA6
	public void ToggleDeathrattle(bool on)
	{
		if (on)
		{
			this.m_actor.ActivateSpellBirthState(SpellType.DEATHRATTLE_IDLE);
			return;
		}
		this.m_actor.ActivateSpellDeathState(SpellType.DEATHRATTLE_IDLE);
	}

	// Token: 0x060074F2 RID: 29938 RVA: 0x00257CC8 File Offset: 0x00255EC8
	public void UpdateBauble()
	{
		if (this.IsBaubleAnimating())
		{
			return;
		}
		this.DeactivateBaubles();
		SpellType prioritizedBaubleSpellType = this.m_entity.GetPrioritizedBaubleSpellType();
		if (prioritizedBaubleSpellType != SpellType.NONE && this.m_actor != null)
		{
			this.m_actor.ActivateSpellBirthState(prioritizedBaubleSpellType);
		}
	}

	// Token: 0x060074F3 RID: 29939 RVA: 0x00257D10 File Offset: 0x00255F10
	public void DeactivateBaubles()
	{
		SpellType prioritizedBaubleSpellType = this.m_entity.GetPrioritizedBaubleSpellType();
		foreach (SpellType spellType in new SpellType[]
		{
			SpellType.TRIGGER,
			SpellType.POISONOUS,
			SpellType.POISONOUS_INSTANT,
			SpellType.INSPIRE,
			SpellType.LIFESTEAL,
			SpellType.OVERKILL,
			SpellType.SPELLBURST,
			SpellType.FRENZY
		})
		{
			if (prioritizedBaubleSpellType != spellType)
			{
				SpellUtils.ActivateDeathIfNecessary(this.GetActorSpell(spellType, false));
			}
		}
	}

	// Token: 0x060074F4 RID: 29940 RVA: 0x00257D5F File Offset: 0x00255F5F
	public bool IsBaubleAnimating()
	{
		return this.m_isBaubleAnimating;
	}

	// Token: 0x060074F5 RID: 29941 RVA: 0x00257D67 File Offset: 0x00255F67
	public void SetIsBaubleAnimating(bool isAnimating)
	{
		this.m_isBaubleAnimating = isAnimating;
	}

	// Token: 0x060074F6 RID: 29942 RVA: 0x00257D70 File Offset: 0x00255F70
	public void ShowExhaustedChange(int val)
	{
		bool exhausted = val == 1;
		this.ShowExhaustedChange(exhausted);
	}

	// Token: 0x060074F7 RID: 29943 RVA: 0x00257D8C File Offset: 0x00255F8C
	public void ShowExhaustedChange(bool exhausted)
	{
		if (this.m_entity.IsHeroPower())
		{
			base.StopCoroutine("PlayHeroPowerAnimation");
			base.StartCoroutine("PlayHeroPowerAnimation", exhausted);
			return;
		}
		if (!this.m_entity.IsWeapon())
		{
			if (this.m_entity.IsSecret())
			{
				base.StartCoroutine(this.ShowSecretExhaustedChange(exhausted));
			}
			return;
		}
		if (exhausted)
		{
			this.SheatheWeapon();
			return;
		}
		this.UnSheatheWeapon();
	}

	// Token: 0x060074F8 RID: 29944 RVA: 0x00257DFD File Offset: 0x00255FFD
	public void DisableHeroPowerFlipSoundOnce()
	{
		this.m_disableHeroPowerFlipSoundOnce = true;
	}

	// Token: 0x060074F9 RID: 29945 RVA: 0x00257E06 File Offset: 0x00256006
	private IEnumerator PlayHeroPowerAnimation(bool exhausted)
	{
		string animationName;
		if (exhausted)
		{
			animationName = (UniversalInputManager.UsePhoneUI ? "HeroPower_Used_phone" : "HeroPower_Used");
			if (this.m_actor != null && this.m_actor.UseCoinManaGem())
			{
				Spell spellIfLoaded = this.m_actor.GetSpellIfLoaded(SpellType.COIN_MANA_GEM);
				if (spellIfLoaded != null)
				{
					spellIfLoaded.Deactivate();
				}
			}
		}
		else
		{
			animationName = (UniversalInputManager.UsePhoneUI ? "HeroPower_Restore_phone" : "HeroPower_Restore");
			if (this.m_actor != null && this.m_actor.UseCoinManaGem())
			{
				Spell spellIfLoaded2 = this.m_actor.GetSpellIfLoaded(SpellType.COIN_MANA_GEM);
				if (spellIfLoaded2 != null)
				{
					spellIfLoaded2.Reactivate();
				}
			}
		}
		this.SetInputEnabled(false);
		MinionShake shake = this.m_actor.gameObject.GetComponentInChildren<MinionShake>();
		if (shake == null)
		{
			yield break;
		}
		while (shake.isShaking())
		{
			yield return null;
		}
		while (this.m_actor.gameObject.transform.parent != base.transform)
		{
			yield return null;
		}
		if (this.m_disableHeroPowerFlipSoundOnce)
		{
			this.m_disableHeroPowerFlipSoundOnce = false;
		}
		else
		{
			string input = exhausted ? "hero_power_icon_flip_off.prefab:621ead6ff672f5b4bbfd6578ee217a42" : "hero_power_icon_flip_on.prefab:e1491b367801f6b4395dc63ce0b08f0a";
			SoundManager.Get().LoadAndPlay(input);
		}
		this.m_actor.GetComponent<Animation>().Play(animationName);
		Spell spell = this.GetPlaySpell(0, true);
		if (spell != null)
		{
			while (spell.GetActiveState() != SpellStateType.NONE)
			{
				yield return null;
			}
		}
		this.SetInputEnabled(true);
		if (animationName.Contains("Used") && GameState.Get().IsValidOption(this.m_entity) && !this.m_entity.HasSubCards() && spell != null)
		{
			this.SetInputEnabled(false);
		}
		yield break;
	}

	// Token: 0x060074FA RID: 29946 RVA: 0x00257E1C File Offset: 0x0025601C
	private void SheatheWeapon()
	{
		if (this.GetZone() is ZoneWeapon)
		{
			this.m_actor.GetAttackObject().ScaleToZero();
			this.ActivateActorSpell(SpellType.SHEATHE);
			return;
		}
		if (!(this.GetZone() == null) && !(this.GetZone() is ZoneGraveyard))
		{
			Log.Gameplay.PrintError("Failed to process Card.SheatheWeapon() card:{0} zone:{1}", new object[]
			{
				this,
				this.GetZone()
			});
		}
	}

	// Token: 0x060074FB RID: 29947 RVA: 0x00257E90 File Offset: 0x00256090
	private void UnSheatheWeapon()
	{
		if (this.GetZone() is ZoneWeapon)
		{
			this.m_actor.GetAttackObject().Enlarge(1f);
			this.ActivateActorSpell(SpellType.UNSHEATHE);
			return;
		}
		if (!(this.GetZone() == null) && !(this.GetZone() is ZoneGraveyard))
		{
			Log.Gameplay.PrintError("Failed to process Card.UnSheatheWeapon() card:{0} zone:{1}", new object[]
			{
				this,
				this.GetZone()
			});
		}
	}

	// Token: 0x060074FC RID: 29948 RVA: 0x00257F06 File Offset: 0x00256106
	private IEnumerator ShowSecretExhaustedChange(bool exhausted)
	{
		while (!this.m_actorReady)
		{
			yield return null;
		}
		if (this.m_entity.IsDarkWandererSecret())
		{
			yield break;
		}
		Spell spell = this.m_actor.GetComponent<Spell>();
		while (spell.GetActiveState() != SpellStateType.NONE)
		{
			yield return null;
		}
		if (!this.CanShowSecretZoneCard())
		{
			yield break;
		}
		if (exhausted)
		{
			this.SheatheSecret(spell);
		}
		else
		{
			this.UnSheatheSecret(spell);
		}
		yield break;
	}

	// Token: 0x060074FD RID: 29949 RVA: 0x00257F1C File Offset: 0x0025611C
	private void SheatheSecret(Spell spell)
	{
		if (this.m_secretSheathed)
		{
			return;
		}
		if (!this.m_entity.IsExhausted())
		{
			return;
		}
		this.m_secretSheathed = true;
		spell.ActivateState(SpellStateType.IDLE);
	}

	// Token: 0x060074FE RID: 29950 RVA: 0x00257F43 File Offset: 0x00256143
	private void UnSheatheSecret(Spell spell)
	{
		if (!this.m_secretSheathed)
		{
			return;
		}
		if (this.m_entity.IsExhausted())
		{
			return;
		}
		this.m_secretSheathed = false;
		spell.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x060074FF RID: 29951 RVA: 0x00257F6C File Offset: 0x0025616C
	public void OnEnchantmentAdded(int oldEnchantmentCount, global::Entity enchantment)
	{
		if (this.CanShowActorVisuals() && this.IsActorReady())
		{
			this.UpdateBauble();
		}
		Spell spell = null;
		if (GameState.Get() != null && GameState.Get().GetGameEntity() != null && GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_ENCHANTMENT_SPARKLES))
		{
			TAG_ENCHANTMENT_VISUAL enchantmentBirthVisual = enchantment.GetEnchantmentBirthVisual();
			if (enchantmentBirthVisual == TAG_ENCHANTMENT_VISUAL.POSITIVE)
			{
				spell = this.GetActorSpell(SpellType.ENCHANT_POSITIVE, true);
			}
			else if (enchantmentBirthVisual == TAG_ENCHANTMENT_VISUAL.NEGATIVE)
			{
				spell = this.GetActorSpell(SpellType.ENCHANT_NEGATIVE, true);
			}
			else if (enchantmentBirthVisual == TAG_ENCHANTMENT_VISUAL.NEUTRAL)
			{
				spell = this.GetActorSpell(SpellType.ENCHANT_NEUTRAL, true);
			}
		}
		if (spell == null)
		{
			this.UpdateEnchantments();
			this.UpdateTooltip();
			return;
		}
		spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnEnchantmentSpellStateFinished));
		spell.ActivateState(SpellStateType.BIRTH);
	}

	// Token: 0x06007500 RID: 29952 RVA: 0x00258018 File Offset: 0x00256218
	public void OnEnchantmentRemoved(int oldEnchantmentCount, global::Entity enchantment)
	{
		if (this.CanShowActorVisuals())
		{
			this.UpdateBauble();
		}
		Spell spell = null;
		if (GameState.Get() != null && GameState.Get().GetGameEntity() != null && GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_ENCHANTMENT_SPARKLES))
		{
			TAG_ENCHANTMENT_VISUAL enchantmentBirthVisual = enchantment.GetEnchantmentBirthVisual();
			if (enchantmentBirthVisual == TAG_ENCHANTMENT_VISUAL.POSITIVE)
			{
				spell = this.GetActorSpell(SpellType.ENCHANT_POSITIVE, true);
			}
			else if (enchantmentBirthVisual == TAG_ENCHANTMENT_VISUAL.NEGATIVE)
			{
				spell = this.GetActorSpell(SpellType.ENCHANT_NEGATIVE, true);
			}
			else if (enchantmentBirthVisual == TAG_ENCHANTMENT_VISUAL.NEUTRAL)
			{
				spell = this.GetActorSpell(SpellType.ENCHANT_NEUTRAL, true);
			}
		}
		if (spell == null)
		{
			this.UpdateEnchantments();
			this.UpdateTooltip();
			return;
		}
		spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnEnchantmentSpellStateFinished));
		spell.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x06007501 RID: 29953 RVA: 0x002580BA File Offset: 0x002562BA
	private void OnEnchantmentSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (prevStateType != SpellStateType.BIRTH && prevStateType != SpellStateType.DEATH)
		{
			return;
		}
		spell.RemoveStateFinishedCallback(new Spell.StateFinishedCallback(this.OnEnchantmentSpellStateFinished));
		this.UpdateEnchantments();
		this.UpdateTooltip();
	}

	// Token: 0x06007502 RID: 29954 RVA: 0x002580E4 File Offset: 0x002562E4
	public void UpdateEnchantments()
	{
		if (GameState.Get() != null && GameState.Get().GetGameEntity() != null && !GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_ENCHANTMENT_SPARKLES))
		{
			return;
		}
		List<global::Entity> enchantments = this.m_entity.GetEnchantments();
		Spell actorSpell = this.GetActorSpell(SpellType.ENCHANT_POSITIVE, true);
		Spell actorSpell2 = this.GetActorSpell(SpellType.ENCHANT_NEGATIVE, true);
		Spell actorSpell3 = this.GetActorSpell(SpellType.ENCHANT_NEUTRAL, true);
		Spell spell = null;
		if (actorSpell != null && actorSpell.GetActiveState() == SpellStateType.IDLE)
		{
			spell = actorSpell;
		}
		else if (actorSpell2 != null && actorSpell2.GetActiveState() == SpellStateType.IDLE)
		{
			spell = actorSpell2;
		}
		else if (actorSpell3 != null && actorSpell3.GetActiveState() == SpellStateType.IDLE)
		{
			spell = actorSpell3;
		}
		if (enchantments.Count == 0)
		{
			if (spell != null)
			{
				spell.ActivateState(SpellStateType.DEATH);
			}
			return;
		}
		int num = 0;
		bool flag = false;
		foreach (global::Entity entity in enchantments)
		{
			TAG_ENCHANTMENT_VISUAL enchantmentIdleVisual = entity.GetEnchantmentIdleVisual();
			if (enchantmentIdleVisual == TAG_ENCHANTMENT_VISUAL.POSITIVE)
			{
				num++;
			}
			else if (enchantmentIdleVisual == TAG_ENCHANTMENT_VISUAL.NEGATIVE)
			{
				num--;
			}
			if (enchantmentIdleVisual != TAG_ENCHANTMENT_VISUAL.INVALID)
			{
				flag = true;
			}
		}
		Spell spell2 = null;
		if (num > 0)
		{
			spell2 = actorSpell;
		}
		else if (num < 0)
		{
			spell2 = actorSpell2;
		}
		else if (flag)
		{
			spell2 = actorSpell3;
		}
		if (spell != null && spell != spell2)
		{
			spell.Deactivate();
		}
		if (spell2 != null)
		{
			spell2.ActivateState(SpellStateType.BIRTH);
		}
	}

	// Token: 0x06007503 RID: 29955 RVA: 0x00258250 File Offset: 0x00256450
	public Spell GetActorSpell(SpellType spellType, bool loadIfNeeded = true)
	{
		if (this.m_actor == null)
		{
			return null;
		}
		Spell result;
		if (loadIfNeeded)
		{
			result = this.m_actor.GetSpell(spellType);
		}
		else
		{
			result = this.m_actor.GetSpellIfLoaded(spellType);
		}
		return result;
	}

	// Token: 0x06007504 RID: 29956 RVA: 0x0025828D File Offset: 0x0025648D
	public Spell ActivateActorSpell(SpellType spellType)
	{
		return this.ActivateActorSpell(this.m_actor, spellType, null, null);
	}

	// Token: 0x06007505 RID: 29957 RVA: 0x0025829E File Offset: 0x0025649E
	public Spell ActivateActorSpell(SpellType spellType, Spell.FinishedCallback finishedCallback)
	{
		return this.ActivateActorSpell(this.m_actor, spellType, finishedCallback, null);
	}

	// Token: 0x06007506 RID: 29958 RVA: 0x002582AF File Offset: 0x002564AF
	public Spell ActivateActorSpell(SpellType spellType, Spell.FinishedCallback finishedCallback, Spell.StateFinishedCallback stateFinishedCallback)
	{
		return this.ActivateActorSpell(this.m_actor, spellType, finishedCallback, stateFinishedCallback);
	}

	// Token: 0x06007507 RID: 29959 RVA: 0x002582C0 File Offset: 0x002564C0
	private Spell ActivateActorSpell(Actor actor, SpellType spellType)
	{
		return this.ActivateActorSpell(actor, spellType, null, null);
	}

	// Token: 0x06007508 RID: 29960 RVA: 0x002582CC File Offset: 0x002564CC
	private Spell ActivateActorSpell(Actor actor, SpellType spellType, Spell.FinishedCallback finishedCallback)
	{
		return this.ActivateActorSpell(actor, spellType, finishedCallback, null);
	}

	// Token: 0x06007509 RID: 29961 RVA: 0x002582D8 File Offset: 0x002564D8
	private Spell ActivateActorSpell(Actor actor, SpellType spellType, Spell.FinishedCallback finishedCallback, Spell.StateFinishedCallback stateFinishedCallback)
	{
		if (actor == null)
		{
			Log.Gameplay.Print(string.Format("{0}.ActivateActorSpell() - actor IS NULL spellType={1}", this, spellType), Array.Empty<object>());
			return null;
		}
		Spell spell = actor.GetSpell(spellType);
		if (spell == null)
		{
			Log.Gameplay.Print(string.Format("{0}.ActivateActorSpell() - spell IS NULL actor={1} spellType={2}", this, actor, spellType), Array.Empty<object>());
			return null;
		}
		this.ActivateSpell(spell, finishedCallback, stateFinishedCallback);
		return spell;
	}

	// Token: 0x0600750A RID: 29962 RVA: 0x0025834F File Offset: 0x0025654F
	private void ActivateSpell(Spell spell, Spell.FinishedCallback finishedCallback)
	{
		this.ActivateSpell(spell, finishedCallback, null, null, null);
	}

	// Token: 0x0600750B RID: 29963 RVA: 0x0025835C File Offset: 0x0025655C
	private void ActivateSpell(Spell spell, Spell.FinishedCallback finishedCallback, Spell.StateFinishedCallback stateFinishedCallback)
	{
		this.ActivateSpell(spell, finishedCallback, null, stateFinishedCallback, null);
	}

	// Token: 0x0600750C RID: 29964 RVA: 0x00258369 File Offset: 0x00256569
	private void ActivateSpell(Spell spell, Spell.FinishedCallback finishedCallback, object finishedUserData, Spell.StateFinishedCallback stateFinishedCallback)
	{
		this.ActivateSpell(spell, finishedCallback, finishedUserData, stateFinishedCallback, null);
	}

	// Token: 0x0600750D RID: 29965 RVA: 0x00258377 File Offset: 0x00256577
	private void ActivateSpell(Spell spell, Spell.FinishedCallback finishedCallback, object finishedUserData, Spell.StateFinishedCallback stateFinishedCallback, object stateFinishedUserData)
	{
		if (finishedCallback != null)
		{
			spell.AddFinishedCallback(finishedCallback, finishedUserData);
		}
		if (stateFinishedCallback != null)
		{
			spell.AddStateFinishedCallback(stateFinishedCallback, stateFinishedUserData);
		}
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			spell.Activate();
		}
	}

	// Token: 0x0600750E RID: 29966 RVA: 0x002583A0 File Offset: 0x002565A0
	public Spell GetActorAttackSpellForInput()
	{
		if (this.m_actor == null)
		{
			Log.Gameplay.Print("{0}.GetActorAttackSpellForInput() - m_actor IS NULL", new object[]
			{
				this
			});
			return null;
		}
		if (this.m_zone == null)
		{
			Log.Gameplay.Print("{0}.GetActorAttackSpellForInput() - m_zone IS NULL", new object[]
			{
				this
			});
			return null;
		}
		Spell spell = this.m_actor.GetSpell(SpellType.FRIENDLY_ATTACK);
		if (spell == null)
		{
			Log.Gameplay.Print("{0}.GetActorAttackSpellForInput() - {1} spell is null", new object[]
			{
				this,
				SpellType.FRIENDLY_ATTACK
			});
			return null;
		}
		return spell;
	}

	// Token: 0x0600750F RID: 29967 RVA: 0x0025843C File Offset: 0x0025663C
	public void FakeDeath()
	{
		if (!this.m_suppressKeywordDeaths)
		{
			base.StartCoroutine(this.WaitAndPrepareForDeathAnimation(this.m_actor));
		}
		this.ActivateDeathSpell(this.m_actor);
	}

	// Token: 0x06007510 RID: 29968 RVA: 0x00258468 File Offset: 0x00256668
	private Spell ActivateDeathSpell(Actor actor)
	{
		bool flag;
		Spell bestDeathSpell = this.GetBestDeathSpell(actor, out flag);
		if (bestDeathSpell == null)
		{
			Debug.LogError(string.Format("{0}.ActivateDeathSpell() - {1} is null", this, SpellType.DEATH));
			return null;
		}
		this.CleanUpCustomSpell(bestDeathSpell, ref this.m_customDeathSpell);
		this.CleanUpCustomSpell(bestDeathSpell, ref this.m_customDeathSpellOverride);
		this.m_activeDeathEffectCount++;
		if (flag)
		{
			if (this.m_actor != actor)
			{
				bestDeathSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSpellStateFinished_DestroyActor));
			}
		}
		else
		{
			bestDeathSpell.SetSource(base.gameObject);
			if (this.m_actor != actor)
			{
				bestDeathSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSpellStateFinished_CustomDeath));
			}
			SpellUtils.SetCustomSpellParent(bestDeathSpell, actor);
		}
		bestDeathSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnSpellFinished_Death));
		bestDeathSpell.Activate();
		BoardEvents boardEvents = BoardEvents.Get();
		if (boardEvents != null)
		{
			boardEvents.DeathEvent(this);
		}
		return bestDeathSpell;
	}

	// Token: 0x06007511 RID: 29969 RVA: 0x00258550 File Offset: 0x00256750
	private Spell ActivateHandSpawnSpell()
	{
		if (this.m_customSpawnSpellOverride == null)
		{
			return this.ActivateDefaultSpawnSpell(new Spell.FinishedCallback(this.OnSpellFinished_DefaultHandSpawn));
		}
		global::Entity creator = this.m_entity.GetCreator();
		Card card = null;
		if (creator != null && creator.IsMinion())
		{
			card = creator.GetCard();
		}
		if (card != null)
		{
			TransformUtil.CopyWorld(base.transform, card.transform);
		}
		this.ActivateCustomHandSpawnSpell(this.m_customSpawnSpellOverride, card);
		return this.m_customSpawnSpellOverride;
	}

	// Token: 0x06007512 RID: 29970 RVA: 0x002585CC File Offset: 0x002567CC
	private void ActivatePlaySpawnEffects_HeroPowerOrWeapon()
	{
		Spell spell = this.m_customSpawnSpellOverride;
		if (spell == null)
		{
			spell = this.m_customSpawnSpell;
			if (spell == null)
			{
				this.ActivateDefaultSpawnSpell(new Spell.FinishedCallback(this.OnSpellFinished_DefaultPlaySpawn));
				return;
			}
		}
		if (this.m_zone is ZoneHeroPower)
		{
			this.m_actor.Hide();
		}
		this.ActivateCustomSpawnSpell(spell);
	}

	// Token: 0x06007513 RID: 29971 RVA: 0x0025862C File Offset: 0x0025682C
	private Spell ActivateDefaultSpawnSpell(Spell.FinishedCallback finishedCallback)
	{
		this.m_inputEnabled = false;
		this.m_actor.ToggleForceIdle(true);
		int tag = this.m_entity.GetTag(GAME_TAG.PREMIUM);
		SpellType spellType = SpellType.SUMMON_IN;
		if (tag == 2)
		{
			spellType = SpellType.SUMMON_IN_DIAMOND;
		}
		if (this.m_zone is ZoneHand && this.m_entity.HasTag(GAME_TAG.GHOSTLY))
		{
			spellType = SpellType.GHOSTLY_SUMMON_IN;
		}
		else if (this.m_zone is ZoneHand && this.m_entity.HasTag(GAME_TAG.CREATOR))
		{
			global::Entity entity = GameState.Get().GetEntity(this.m_entity.GetTag(GAME_TAG.CREATOR));
			if (entity != null && entity.HasTag(GAME_TAG.TWINSPELL) && entity.GetTag(GAME_TAG.TWINSPELL_COPY) == GameUtils.TranslateCardIdToDbId(this.m_entity.GetCardId(), false))
			{
				spellType = ((GameState.Get().GetGameEntity().GetTag(GAME_TAG.BACON_USE_FAST_ANIMATIONS) > 0) ? SpellType.TWINSPELL_SUMMON_IN_FAST : SpellType.TWINSPELL_SUMMON_IN);
			}
		}
		else if (this.m_entity.IsWeapon() && (this.m_zone is ZoneWeapon || this.m_zone is ZoneHeroPower))
		{
			spellType = (this.m_entity.IsControlledByFriendlySidePlayer() ? SpellType.SUMMON_IN_FRIENDLY : SpellType.SUMMON_IN_OPPONENT);
		}
		Spell spell = this.ActivateActorSpell(spellType, finishedCallback);
		if (spell == null)
		{
			Debug.LogError(string.Format("{0}.ActivateDefaultSpawnSpell() - {1} is null", this, spellType));
			return null;
		}
		return spell;
	}

	// Token: 0x06007514 RID: 29972 RVA: 0x0025878C File Offset: 0x0025698C
	private void ActivateCustomSpawnSpell(Spell spell)
	{
		spell.SetSource(base.gameObject);
		spell.RemoveAllTargets();
		spell.AddTarget(base.gameObject);
		spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSpellStateFinished_DestroySpell));
		SpellUtils.SetCustomSpellParent(spell, this.m_actor);
		spell.AddFinishedCallback(new Spell.FinishedCallback(this.OnSpellFinished_CustomPlaySpawn));
		spell.Activate();
	}

	// Token: 0x06007515 RID: 29973 RVA: 0x002587F0 File Offset: 0x002569F0
	private void ActivateCustomHandSpawnSpell(Spell spell, Card creatorCard)
	{
		GameObject source = (creatorCard == null) ? base.gameObject : creatorCard.gameObject;
		spell.SetSource(source);
		spell.RemoveAllTargets();
		spell.AddTarget(base.gameObject);
		spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSpellStateFinished_DestroySpell));
		SpellUtils.SetCustomSpellParent(spell, this.m_actor);
		spell.AddFinishedCallback(new Spell.FinishedCallback(this.OnSpellFinished_CustomHandSpawn));
		spell.Activate();
	}

	// Token: 0x06007516 RID: 29974 RVA: 0x00258864 File Offset: 0x00256A64
	private void ActivateMinionSpawnEffects()
	{
		global::Entity creator = this.m_entity.GetCreator();
		Card card = null;
		if (creator != null && creator.IsMinion())
		{
			card = creator.GetCard();
		}
		if (card != null && !(card.GetZone() is ZonePlay) && !(card.GetZone() is ZoneGraveyard))
		{
			card = null;
		}
		if (card != null)
		{
			TransformUtil.CopyWorld(base.transform, card.transform);
		}
		bool flag;
		Spell bestSpawnSpell = this.GetBestSpawnSpell(out flag);
		if (!flag)
		{
			this.ActivateCustomSpawnMinionSpell(bestSpawnSpell, card);
			return;
		}
		if (card == null)
		{
			this.ActivateStandardSpawnMinionSpell();
			return;
		}
		base.StartCoroutine(this.ActivateCreatorSpawnMinionSpell(creator, card));
	}

	// Token: 0x06007517 RID: 29975 RVA: 0x00258904 File Offset: 0x00256B04
	private IEnumerator ActivateCreatorSpawnMinionSpell(global::Entity creator, Card creatorCard)
	{
		while (creator.IsLoadingAssets() || !creatorCard.IsActorReady())
		{
			yield return 0;
		}
		if (creatorCard.ActivateCreatorSpawnMinionSpell() != null)
		{
			yield return new WaitForSeconds(0.9f);
		}
		this.ActivateStandardSpawnMinionSpell();
		yield break;
	}

	// Token: 0x06007518 RID: 29976 RVA: 0x00258921 File Offset: 0x00256B21
	private Spell ActivateCreatorSpawnMinionSpell()
	{
		if (this.m_entity.IsControlledByFriendlySidePlayer())
		{
			return this.ActivateActorSpell(SpellType.FRIENDLY_SPAWN_MINION);
		}
		return this.ActivateActorSpell(SpellType.OPPONENT_SPAWN_MINION);
	}

	// Token: 0x06007519 RID: 29977 RVA: 0x00258944 File Offset: 0x00256B44
	private void ActivateStandardSpawnMinionSpell()
	{
		if (this.m_entity.IsControlledByFriendlySidePlayer())
		{
			this.m_activeSpawnSpell = this.ActivateActorSpell(SpellType.FRIENDLY_SPAWN_MINION, new Spell.FinishedCallback(this.OnSpellFinished_StandardSpawnCharacter));
		}
		else
		{
			this.m_activeSpawnSpell = this.ActivateActorSpell(SpellType.OPPONENT_SPAWN_MINION, new Spell.FinishedCallback(this.OnSpellFinished_StandardSpawnCharacter));
		}
		this.ActivateCharacterPlayEffects();
	}

	// Token: 0x0600751A RID: 29978 RVA: 0x0025899C File Offset: 0x00256B9C
	private void ActivateStandardSpawnHeroSpell()
	{
		if (this.m_entity.IsControlledByFriendlySidePlayer())
		{
			this.m_activeSpawnSpell = this.ActivateActorSpell(SpellType.FRIENDLY_SPAWN_HERO, new Spell.FinishedCallback(this.OnSpellFinished_StandardSpawnCharacter));
			return;
		}
		this.m_activeSpawnSpell = this.ActivateActorSpell(SpellType.OPPONENT_SPAWN_HERO, new Spell.FinishedCallback(this.OnSpellFinished_StandardSpawnCharacter));
	}

	// Token: 0x0600751B RID: 29979 RVA: 0x002589F4 File Offset: 0x00256BF4
	private void ActivateCustomSpawnMinionSpell(Spell spell, Card creatorCard)
	{
		this.m_activeSpawnSpell = spell;
		GameObject source = (creatorCard == null) ? base.gameObject : creatorCard.gameObject;
		spell.SetSource(source);
		spell.RemoveAllTargets();
		spell.AddTarget(base.gameObject);
		spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSpellStateFinished_DestroySpell));
		SpellUtils.SetCustomSpellParent(spell, this.m_actor);
		spell.AddFinishedCallback(new Spell.FinishedCallback(this.OnSpellFinished_CustomSpawnMinion));
		spell.Activate();
	}

	// Token: 0x0600751C RID: 29980 RVA: 0x00258A6F File Offset: 0x00256C6F
	private IEnumerator ActivateReviveSpell()
	{
		while (this.m_activeDeathEffectCount > 0)
		{
			yield return 0;
		}
		this.ActivateStandardSpawnMinionSpell();
		yield break;
	}

	// Token: 0x0600751D RID: 29981 RVA: 0x00258A7E File Offset: 0x00256C7E
	private IEnumerator ActivateActorBattlecrySpell()
	{
		Spell battlecrySpell = this.GetActorSpell(SpellType.BATTLECRY, true);
		if (battlecrySpell == null)
		{
			yield break;
		}
		if (!(this.m_zone is ZonePlay))
		{
			yield break;
		}
		if (InputManager.Get() == null)
		{
			yield break;
		}
		if (InputManager.Get().GetBattlecrySourceCard() != this)
		{
			yield break;
		}
		yield return new WaitForSeconds(0.01f);
		if (InputManager.Get() == null)
		{
			yield break;
		}
		if (InputManager.Get().GetBattlecrySourceCard() != this)
		{
			yield break;
		}
		if (battlecrySpell.GetActiveState() == SpellStateType.NONE)
		{
			battlecrySpell.ActivateState(SpellStateType.BIRTH);
		}
		Spell playSpell = this.GetPlaySpell(0, true);
		if (playSpell)
		{
			playSpell.ActivateState(SpellStateType.BIRTH);
		}
		yield break;
	}

	// Token: 0x0600751E RID: 29982 RVA: 0x00258A8D File Offset: 0x00256C8D
	private void CleanUpCustomSpell(Spell chosenSpell, ref Spell customSpell)
	{
		if (!customSpell)
		{
			return;
		}
		if (chosenSpell == customSpell)
		{
			customSpell = null;
			return;
		}
		UnityEngine.Object.Destroy(customSpell.gameObject);
	}

	// Token: 0x0600751F RID: 29983 RVA: 0x00258AB4 File Offset: 0x00256CB4
	private void OnSpellFinished_StandardSpawnCharacter(Spell spell, object userData)
	{
		this.m_actorReady = true;
		this.m_inputEnabled = true;
		this.m_actor.Show();
		this.ActivateStateSpells(false);
		this.RefreshActor();
		this.UpdateActorComponents();
		BoardEvents boardEvents = BoardEvents.Get();
		if (boardEvents != null)
		{
			boardEvents.SummonedEvent(this);
		}
	}

	// Token: 0x06007520 RID: 29984 RVA: 0x00258B03 File Offset: 0x00256D03
	private void OnSpellFinished_CustomSpawnMinion(Spell spell, object userData)
	{
		this.OnSpellFinished_StandardSpawnCharacter(spell, userData);
		this.CleanUpCustomSpell(spell, ref this.m_customSpawnSpell);
		this.CleanUpCustomSpell(spell, ref this.m_customSpawnSpellOverride);
		this.ActivateCharacterPlayEffects();
	}

	// Token: 0x06007521 RID: 29985 RVA: 0x00258B2D File Offset: 0x00256D2D
	private void OnSpellFinished_DefaultHandSpawn(Spell spell, object userData)
	{
		this.m_actor.ToggleForceIdle(false);
		this.m_inputEnabled = true;
		this.ActivateStateSpells(false);
		this.RefreshActor();
		this.UpdateActorComponents();
	}

	// Token: 0x06007522 RID: 29986 RVA: 0x00258B55 File Offset: 0x00256D55
	private void OnSpellFinished_CustomHandSpawn(Spell spell, object userData)
	{
		this.OnSpellFinished_DefaultHandSpawn(spell, userData);
		this.CleanUpCustomSpell(spell, ref this.m_customSpawnSpellOverride);
	}

	// Token: 0x06007523 RID: 29987 RVA: 0x00258B6C File Offset: 0x00256D6C
	private void OnSpellFinished_DefaultPlaySpawn(Spell spell, object userData)
	{
		this.m_actor.ToggleForceIdle(false);
		this.m_inputEnabled = true;
		if (this.m_zone != null)
		{
			this.ActivateStateSpells(false);
		}
		this.RefreshActor();
		this.UpdateActorComponents();
	}

	// Token: 0x06007524 RID: 29988 RVA: 0x00258BA2 File Offset: 0x00256DA2
	private void OnSpellFinished_CustomPlaySpawn(Spell spell, object userData)
	{
		this.OnSpellFinished_DefaultPlaySpawn(spell, userData);
		this.CleanUpCustomSpell(spell, ref this.m_customSpawnSpell);
		this.CleanUpCustomSpell(spell, ref this.m_customSpawnSpellOverride);
	}

	// Token: 0x06007525 RID: 29989 RVA: 0x00258BC6 File Offset: 0x00256DC6
	private void OnSpellFinished_StandardCardSummon(Spell spell, object userData)
	{
		this.m_actorReady = true;
		this.m_inputEnabled = true;
		this.ActivateStateSpells(false);
		this.RefreshActor();
		this.UpdateActorComponents();
	}

	// Token: 0x06007526 RID: 29990 RVA: 0x00258BE9 File Offset: 0x00256DE9
	private void OnSpellFinished_UpdateActorComponents(Spell spell, object userData)
	{
		this.UpdateActorComponents();
	}

	// Token: 0x06007527 RID: 29991 RVA: 0x00258BF1 File Offset: 0x00256DF1
	private void OnSpellFinished_Death(Spell spell, object userData)
	{
		this.m_suppressKeywordDeaths = false;
		this.m_keywordDeathDelaySec = 0.6f;
		this.m_activeDeathEffectCount--;
		GameState.Get().ClearCardBeingDrawn(this);
	}

	// Token: 0x06007528 RID: 29992 RVA: 0x00258C20 File Offset: 0x00256E20
	private void OnSpellStateFinished_DestroyActor(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		if (this.m_zone is ZoneGraveyard)
		{
			this.PurgeSpells();
		}
		Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(spell.gameObject);
		if (actor == null)
		{
			Debug.LogWarning(string.Format("Card.OnSpellStateFinished_DestroyActor() - spell {0} on Card {1} has no Actor ancestor", spell, this));
			return;
		}
		actor.Destroy();
	}

	// Token: 0x06007529 RID: 29993 RVA: 0x001FACD3 File Offset: 0x001F8ED3
	private void OnSpellStateFinished_DestroySpell(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		UnityEngine.Object.Destroy(spell.gameObject);
	}

	// Token: 0x0600752A RID: 29994 RVA: 0x00258C78 File Offset: 0x00256E78
	private void OnSpellStateFinished_CustomDeath(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(spell.gameObject);
		if (actor == null)
		{
			Debug.LogWarning(string.Format("Card.OnSpellStateFinished_CustomDeath() - spell {0} on Card {1} has no Actor ancestor", spell, this));
			return;
		}
		actor.Destroy();
	}

	// Token: 0x0600752B RID: 29995 RVA: 0x00258CBC File Offset: 0x00256EBC
	public void UpdateActorState(bool forceHighlightRefresh = false)
	{
		if (this.m_actor == null)
		{
			return;
		}
		if (!this.m_shown)
		{
			return;
		}
		if (this.m_entity.IsBusy())
		{
			return;
		}
		if (this.m_zone is ZoneGraveyard)
		{
			return;
		}
		if (!this.m_inputEnabled || (this.m_zone != null && !this.m_zone.IsInputEnabled()))
		{
			this.m_actor.SetActorState(ActorStateType.CARD_IDLE);
			return;
		}
		if (this.m_overPlayfield)
		{
			this.m_actor.SetActorState(ActorStateType.CARD_OVER_PLAYFIELD);
			return;
		}
		GameState gameState = GameState.Get();
		if (gameState != null && gameState.IsEntityInputEnabled(this.m_entity))
		{
			if (forceHighlightRefresh)
			{
				this.m_actor.SetActorState(ActorStateType.CARD_IDLE);
			}
			switch (gameState.GetResponseMode())
			{
			case GameState.ResponseMode.OPTION:
				if (this.DoOptionHighlight(gameState))
				{
					return;
				}
				break;
			case GameState.ResponseMode.SUB_OPTION:
				if (this.DoSubOptionHighlight(gameState))
				{
					return;
				}
				break;
			case GameState.ResponseMode.OPTION_TARGET:
				if (this.DoOptionTargetHighlight(gameState))
				{
					return;
				}
				break;
			case GameState.ResponseMode.CHOICE:
				if (this.DoChoiceHighlight(gameState))
				{
					return;
				}
				break;
			}
		}
		if (this.m_mousedOver && !(this.m_zone is ZoneHand))
		{
			this.m_actor.SetActorState(ActorStateType.CARD_MOUSE_OVER);
			return;
		}
		if (this.m_mousedOverByOpponent)
		{
			this.m_actor.SetActorState(ActorStateType.CARD_OPPONENT_MOUSE_OVER);
			return;
		}
		this.m_actor.SetActorState(ActorStateType.CARD_IDLE);
	}

	// Token: 0x0600752C RID: 29996 RVA: 0x00258DF4 File Offset: 0x00256FF4
	private bool DoChoiceHighlight(GameState state)
	{
		if (state.GetChosenEntities().Contains(this.m_entity))
		{
			if (this.m_mousedOver)
			{
				this.m_actor.SetActorState(ActorStateType.CARD_PLAYABLE_MOUSE_OVER);
			}
			else
			{
				this.m_actor.SetActorState(ActorStateType.CARD_SELECTED);
			}
			return true;
		}
		int entityId = this.m_entity.GetEntityId();
		if (state.GetFriendlyEntityChoices().Entities.Contains(entityId))
		{
			if (GameState.Get().IsMulliganManagerActive())
			{
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
				{
					if (this.m_mousedOver)
					{
						this.m_actor.SetActorState(GameState.Get().GetGameEntity().GetMulliganChoiceHighlightState());
					}
					else
					{
						this.m_actor.SetActorState(ActorStateType.CARD_IDLE);
					}
				}
				else
				{
					this.m_actor.SetActorState(GameState.Get().GetGameEntity().GetMulliganChoiceHighlightState());
				}
			}
			else
			{
				this.m_actor.SetActorState(ActorStateType.CARD_SELECTABLE);
			}
			return true;
		}
		return false;
	}

	// Token: 0x0600752D RID: 29997 RVA: 0x00258ED0 File Offset: 0x002570D0
	private bool DoOptionHighlight(GameState state)
	{
		if (!GameState.Get().IsValidOption(this.m_entity))
		{
			return false;
		}
		bool flag = this.m_entity.GetZone() == TAG_ZONE.HAND;
		bool flag2 = this.m_entity.GetController().IsRealTimeComboActive();
		if ((flag || this.m_entity.IsHeroPowerOrGameModeButton()) && flag2 && this.m_entity.HasTag(GAME_TAG.COMBO))
		{
			this.m_actor.SetActorState(ActorStateType.CARD_COMBO);
			return true;
		}
		bool realTimePoweredUp = this.m_entity.GetRealTimePoweredUp();
		if ((flag || this.m_entity.IsHeroPowerOrGameModeButton()) && realTimePoweredUp)
		{
			this.m_actor.SetActorState(ActorStateType.CARD_POWERED_UP);
			return true;
		}
		if ((this.m_entity.GetZone() == TAG_ZONE.PLAY || (this.m_latestZoneChange != null && this.m_latestZoneChange.GetDestinationZone() != null && this.m_latestZoneChange.GetDestinationZone().m_ServerTag == TAG_ZONE.PLAY)) && state.GetGameEntity().GetTag(GAME_TAG.ALLOW_MOVE_MINION) > 0 && this.m_entity.IsMinion())
		{
			if (!GameState.Get().HasEnoughManaForMoveMinionHoverTarget(this.m_entity))
			{
				if (this.m_mousedOver)
				{
					this.m_actor.SetActorState(ActorStateType.CARD_MOUSE_OVER);
				}
				else
				{
					this.m_actor.SetActorState(ActorStateType.CARD_IDLE);
				}
				return true;
			}
			if (this.m_mousedOver)
			{
				this.m_actor.SetActorState(ActorStateType.CARD_MOVEABLE_MOUSE_OVER);
			}
			else
			{
				this.m_actor.SetActorState(ActorStateType.CARD_MOVEABLE);
			}
			return true;
		}
		else
		{
			if (!flag && this.m_mousedOver)
			{
				if (this.m_entity.GetRealTimeAttackableByRush())
				{
					this.m_actor.SetActorState(ActorStateType.CARD_ATTACKABLE_BY_RUSH_MOUSE_OVER);
				}
				else
				{
					this.m_actor.SetActorState(ActorStateType.CARD_PLAYABLE_MOUSE_OVER);
				}
				return true;
			}
			if (this.m_entity.GetRealTimeAttackableByRush())
			{
				this.m_actor.SetActorState(ActorStateType.CARD_ATTACKABLE_BY_RUSH);
			}
			else
			{
				this.m_actor.SetActorState(ActorStateType.CARD_PLAYABLE);
			}
			return true;
		}
	}

	// Token: 0x0600752E RID: 29998 RVA: 0x00259098 File Offset: 0x00257298
	private bool DoSubOptionHighlight(GameState state)
	{
		Network.Options.Option selectedNetworkOption = state.GetSelectedNetworkOption();
		int entityId = this.m_entity.GetEntityId();
		foreach (Network.Options.Option.SubOption subOption in selectedNetworkOption.Subs)
		{
			if (entityId == subOption.ID)
			{
				if (!subOption.PlayErrorInfo.IsValid())
				{
					return false;
				}
				if (this.m_mousedOver)
				{
					this.m_actor.SetActorState(ActorStateType.CARD_PLAYABLE_MOUSE_OVER);
				}
				else
				{
					this.m_actor.SetActorState(ActorStateType.CARD_PLAYABLE);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600752F RID: 29999 RVA: 0x00259138 File Offset: 0x00257338
	private bool DoOptionTargetHighlight(GameState state)
	{
		Network.Options.Option.SubOption selectedNetworkSubOption = state.GetSelectedNetworkSubOption();
		int entityId = this.m_entity.GetEntityId();
		if (selectedNetworkSubOption.IsValidTarget(entityId))
		{
			if (this.m_mousedOver)
			{
				this.m_actor.SetActorState(ActorStateType.CARD_VALID_TARGET_MOUSE_OVER);
			}
			else
			{
				this.m_actor.SetActorState(ActorStateType.CARD_VALID_TARGET);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06007530 RID: 30000 RVA: 0x00259186 File Offset: 0x00257386
	public Actor GetActor()
	{
		return this.m_actor;
	}

	// Token: 0x06007531 RID: 30001 RVA: 0x0025918E File Offset: 0x0025738E
	public void SetActor(Actor actor)
	{
		this.m_actor = actor;
	}

	// Token: 0x06007532 RID: 30002 RVA: 0x00259197 File Offset: 0x00257397
	public string GetActorAssetPath()
	{
		return this.m_actorPath;
	}

	// Token: 0x06007533 RID: 30003 RVA: 0x0025919F File Offset: 0x0025739F
	public void SetActorAssetPath(string actorName)
	{
		this.m_actorPath = actorName;
	}

	// Token: 0x06007534 RID: 30004 RVA: 0x002591A8 File Offset: 0x002573A8
	public bool IsActorReady()
	{
		return this.m_actorReady;
	}

	// Token: 0x06007535 RID: 30005 RVA: 0x002591B0 File Offset: 0x002573B0
	public bool IsActorLoading()
	{
		return this.m_actorLoading;
	}

	// Token: 0x06007536 RID: 30006 RVA: 0x002591B8 File Offset: 0x002573B8
	public void UpdateActorComponents()
	{
		if (this.m_actor == null)
		{
			return;
		}
		this.m_actor.UpdateAllComponents();
	}

	// Token: 0x06007537 RID: 30007 RVA: 0x002591D4 File Offset: 0x002573D4
	public void RefreshActor()
	{
		this.UpdateActorState(false);
		if (this.m_entity.IsEnchanted())
		{
			this.UpdateEnchantments();
		}
		this.UpdateTooltip();
	}

	// Token: 0x06007538 RID: 30008 RVA: 0x002591F6 File Offset: 0x002573F6
	public Zone GetZone()
	{
		return this.m_zone;
	}

	// Token: 0x06007539 RID: 30009 RVA: 0x002591FE File Offset: 0x002573FE
	public Zone GetPrevZone()
	{
		return this.m_prevZone;
	}

	// Token: 0x0600753A RID: 30010 RVA: 0x00259206 File Offset: 0x00257406
	public void SetZone(Zone zone)
	{
		this.m_zone = zone;
	}

	// Token: 0x0600753B RID: 30011 RVA: 0x0025920F File Offset: 0x0025740F
	public int GetZonePosition()
	{
		return this.m_zonePosition;
	}

	// Token: 0x0600753C RID: 30012 RVA: 0x00259217 File Offset: 0x00257417
	public void SetZonePosition(int pos)
	{
		this.m_zonePosition = pos;
	}

	// Token: 0x0600753D RID: 30013 RVA: 0x00259220 File Offset: 0x00257420
	public int GetPredictedZonePosition()
	{
		return this.m_predictedZonePosition;
	}

	// Token: 0x0600753E RID: 30014 RVA: 0x00259228 File Offset: 0x00257428
	public void SetPredictedZonePosition(int pos)
	{
		this.m_predictedZonePosition = pos;
	}

	// Token: 0x0600753F RID: 30015 RVA: 0x00259231 File Offset: 0x00257431
	public ZoneTransitionStyle GetTransitionStyle()
	{
		return this.m_transitionStyle;
	}

	// Token: 0x06007540 RID: 30016 RVA: 0x00259239 File Offset: 0x00257439
	public void SetTransitionStyle(ZoneTransitionStyle style)
	{
		this.m_transitionStyle = style;
	}

	// Token: 0x06007541 RID: 30017 RVA: 0x00259242 File Offset: 0x00257442
	public bool IsTransitioningZones()
	{
		return this.m_transitioningZones;
	}

	// Token: 0x06007542 RID: 30018 RVA: 0x0025924A File Offset: 0x0025744A
	public void EnableTransitioningZones(bool enable)
	{
		this.m_transitioningZones = enable;
	}

	// Token: 0x06007543 RID: 30019 RVA: 0x00259253 File Offset: 0x00257453
	public bool HasBeenGrabbedByEnemyActionHandler()
	{
		return this.m_hasBeenGrabbedByEnemyActionHandler;
	}

	// Token: 0x06007544 RID: 30020 RVA: 0x0025925B File Offset: 0x0025745B
	public void MarkAsGrabbedByEnemyActionHandler(bool enable)
	{
		Log.FaceDownCard.Print("Card.MarkAsGrabbedByEnemyActionHandler() - card={0} enable={1}", new object[]
		{
			this,
			enable
		});
		this.m_hasBeenGrabbedByEnemyActionHandler = enable;
	}

	// Token: 0x06007545 RID: 30021 RVA: 0x00259286 File Offset: 0x00257486
	public bool IsDoNotSort()
	{
		return this.m_doNotSort;
	}

	// Token: 0x06007546 RID: 30022 RVA: 0x0025928E File Offset: 0x0025748E
	public void SetDoNotSort(bool on)
	{
		if (this.m_entity.IsControlledByOpposingSidePlayer())
		{
			Log.FaceDownCard.Print("Card.SetDoNotSort() - card={0} on={1}", new object[]
			{
				this,
				on
			});
		}
		this.m_doNotSort = on;
	}

	// Token: 0x06007547 RID: 30023 RVA: 0x002592C6 File Offset: 0x002574C6
	public bool IsDoNotWarpToNewZone()
	{
		return this.m_doNotWarpToNewZone;
	}

	// Token: 0x06007548 RID: 30024 RVA: 0x002592CE File Offset: 0x002574CE
	public void SetDoNotWarpToNewZone(bool on)
	{
		this.m_doNotWarpToNewZone = on;
	}

	// Token: 0x06007549 RID: 30025 RVA: 0x002592D7 File Offset: 0x002574D7
	public float GetTransitionDelay()
	{
		return this.m_transitionDelay;
	}

	// Token: 0x0600754A RID: 30026 RVA: 0x002592DF File Offset: 0x002574DF
	public void SetTransitionDelay(float delay)
	{
		this.m_transitionDelay = delay;
	}

	// Token: 0x0600754B RID: 30027 RVA: 0x002592E8 File Offset: 0x002574E8
	public void UpdateZoneFromTags()
	{
		this.m_zonePosition = this.m_entity.GetZonePosition();
		Zone zone = ZoneMgr.Get().FindZoneForEntity(this.m_entity);
		this.TransitionToZone(zone, null);
		if (zone != null)
		{
			zone.UpdateLayout();
		}
	}

	// Token: 0x0600754C RID: 30028 RVA: 0x00259330 File Offset: 0x00257530
	public void TransitionToZone(Zone zone, ZoneChange zoneChange = null)
	{
		this.m_latestZoneChange = zoneChange;
		if (this.m_zone == zone)
		{
			Log.Gameplay.Print("Card.TransitionToZone() - card={0} already in target zone", new object[]
			{
				this
			});
			return;
		}
		if (zone == null)
		{
			this.m_zone.RemoveCard(this);
			this.m_prevZone = this.m_zone;
			this.m_zone = null;
			this.DeactivateLifetimeEffects();
			this.DeactivateCustomKeywordEffect();
			if (this.m_prevZone is ZoneHand)
			{
				this.DeactivateHandStateSpells(null);
			}
			if (this.m_prevZone is ZoneHeroPower)
			{
				foreach (Card card in this.m_prevZone.GetCards())
				{
					if (!(card == this) && card.GetEntity().GetTag(GAME_TAG.LINKED_ENTITY) == this.m_entity.GetEntityId() && card.m_customSpawnSpellOverride != null)
					{
						if (this.m_actor != null)
						{
							this.m_actor.DeactivateAllSpells();
						}
						return;
					}
				}
			}
			if (this.m_prevZone is ZoneHero)
			{
				global::Player controller = this.m_prevZone.GetController();
				if (controller.GetHero() != null && controller.GetHero().GetCard() != null)
				{
					controller.GetHero().GetCard().ShowCard();
				}
			}
			this.DoNullZoneVisuals();
			return;
		}
		if (this.m_zone is ZoneSecret && this.m_entity != null && this.m_entity.IsQuest())
		{
			this.NotifyMousedOut();
		}
		this.m_prevZone = this.m_zone;
		this.m_zone = zone;
		if (this.m_prevZone is ZoneDeck && this.m_zone is ZoneHand)
		{
			if (this.m_zone.m_Side == global::Player.Side.FRIENDLY)
			{
				this.m_cardDrawTracker = GameState.Get().GetFriendlyCardDrawCounter();
				GameState.Get().IncrementFriendlyCardDrawCounter();
			}
			else
			{
				this.m_cardDrawTracker = GameState.Get().GetOpponentCardDrawCounter();
				GameState.Get().IncrementOpponentCardDrawCounter();
			}
		}
		if (this.m_prevZone != null)
		{
			this.m_prevZone.RemoveCard(this);
		}
		this.m_zone.AddCard(this);
		if ((this.m_zone is ZonePlay || this.m_zone is ZoneHero) && this.m_prevZone is ZoneHand && this.m_entity.IsHero() && GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS) && MulliganManager.Get() != null && MulliganManager.Get().IsMulliganActive())
		{
			this.m_actorReady = true;
			return;
		}
		if (this.m_zone is ZoneGraveyard && this.m_actor != null && this.m_actor.UseCoinManaGem())
		{
			this.m_actor.DestroySpell(SpellType.COIN_MANA_GEM);
		}
		if (this.m_zone is ZoneGraveyard && GameState.Get().IsBeingDrawn(this))
		{
			this.m_actorReady = true;
			this.DiscardCardBeingDrawn();
			return;
		}
		if (this.m_zone is ZoneGraveyard && this.m_ignoreDeath)
		{
			this.m_actorReady = true;
			return;
		}
		if (this.m_zone is ZoneGraveyard && this.m_actor != null && this.m_actorReady && this.m_entity.IsSpell())
		{
			this.m_actorReady = false;
			base.StartCoroutine(this.LoadActorAndSpellsAfterPowerUpFinishes());
			return;
		}
		this.m_actorReady = false;
		this.LoadActorAndSpells();
	}

	// Token: 0x0600754D RID: 30029 RVA: 0x0025969C File Offset: 0x0025789C
	public void UpdateActor(bool forceIfNullZone = false, string actorPath = null)
	{
		if (!forceIfNullZone && this.m_zone == null)
		{
			return;
		}
		TAG_ZONE zone = this.m_entity.GetZone();
		if (actorPath == null)
		{
			actorPath = this.m_cardDef.CardDef.DetermineActorPathForZone(this.m_entity, zone);
		}
		if (this.m_actor != null && this.m_actorPath == actorPath)
		{
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(actorPath, AssetLoadingOptions.IgnorePrefabPosition);
		if (!gameObject)
		{
			Debug.LogWarningFormat("Card.UpdateActor() - FAILED to load actor \"{0}\"", new object[]
			{
				actorPath
			});
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarningFormat("Card.UpdateActor() - ERROR actor \"{0}\" has no Actor component", new object[]
			{
				actorPath
			});
			return;
		}
		if (this.m_actor != null)
		{
			this.m_actor.Destroy();
		}
		this.m_actor = component;
		this.m_actorPath = actorPath;
		this.m_actor.SetEntity(this.m_entity);
		this.m_actor.SetCard(this);
		this.m_actor.SetCardDef(this.m_cardDef);
		this.m_actor.UpdateAllComponents();
		if (this.m_shown)
		{
			this.ShowImpl();
		}
		else
		{
			this.HideImpl();
		}
		this.RefreshActor();
	}

	// Token: 0x0600754E RID: 30030 RVA: 0x002597D0 File Offset: 0x002579D0
	private IEnumerator LoadActorAndSpellsAfterPowerUpFinishes()
	{
		this.m_actorLoading = true;
		Spell spell = this.m_actor.GetSpell(SpellType.POWER_UP);
		if (spell != null)
		{
			while (spell.GetActiveState() != SpellStateType.NONE && spell.GetActiveState() != SpellStateType.IDLE)
			{
				yield return null;
			}
		}
		this.LoadActorAndSpells();
		yield break;
	}

	// Token: 0x0600754F RID: 30031 RVA: 0x002597E0 File Offset: 0x002579E0
	private void LoadActorAndSpells()
	{
		this.m_actorLoading = true;
		List<Card.SpellLoadRequest> list = new List<Card.SpellLoadRequest>();
		if (this.m_prevZone is ZoneHand && (this.m_zone is ZonePlay || this.m_zone is ZoneHero || this.m_zone is ZoneWeapon))
		{
			Card.SpellLoadRequest spellLoadRequest = this.MakeCustomSpellLoadRequest(this.m_cardDef.CardDef.m_CustomSummonSpellPath, this.m_cardDef.CardDef.m_GoldenCustomSummonSpellPath, new PrefabCallback<GameObject>(this.OnCustomSummonSpellLoaded));
			if (spellLoadRequest != null)
			{
				list.Add(spellLoadRequest);
			}
		}
		if (!this.m_customDeathSpell && (this.m_zone is ZoneHand || this.m_zone is ZonePlay))
		{
			Card.SpellLoadRequest spellLoadRequest2 = this.MakeCustomSpellLoadRequest(this.m_cardDef.CardDef.m_CustomDeathSpellPath, this.m_cardDef.CardDef.m_GoldenCustomDeathSpellPath, new PrefabCallback<GameObject>(this.OnCustomDeathSpellLoaded));
			if (spellLoadRequest2 != null)
			{
				list.Add(spellLoadRequest2);
			}
		}
		if (!this.m_customDiscardSpell && (this.m_zone is ZoneHand || this.m_zone is ZoneGraveyard))
		{
			Card.SpellLoadRequest spellLoadRequest3 = this.MakeCustomSpellLoadRequest(this.m_cardDef.CardDef.m_CustomDiscardSpellPath, this.m_cardDef.CardDef.m_GoldenCustomDiscardSpellPath, new PrefabCallback<GameObject>(this.OnCustomDiscardSpellLoaded));
			if (spellLoadRequest3 != null)
			{
				list.Add(spellLoadRequest3);
			}
		}
		if (!this.m_customSpawnSpell && (this.m_zone is ZonePlay || this.m_zone is ZoneWeapon))
		{
			Card.SpellLoadRequest spellLoadRequest4 = this.MakeCustomSpellLoadRequest(this.m_cardDef.CardDef.m_CustomSpawnSpellPath, this.m_cardDef.CardDef.m_GoldenCustomSpawnSpellPath, new PrefabCallback<GameObject>(this.OnCustomSpawnSpellLoaded));
			if (spellLoadRequest4 != null)
			{
				list.Add(spellLoadRequest4);
			}
		}
		this.m_spellLoadCount = list.Count;
		if (list.Count == 0)
		{
			this.LoadActor();
			return;
		}
		foreach (Card.SpellLoadRequest spellLoadRequest5 in list)
		{
			AssetLoader.Get().InstantiatePrefab(spellLoadRequest5.m_path, spellLoadRequest5.m_loadCallback, null, AssetLoadingOptions.None);
		}
	}

	// Token: 0x06007550 RID: 30032 RVA: 0x00259A14 File Offset: 0x00257C14
	private Card.SpellLoadRequest MakeCustomSpellLoadRequest(string customPath, string goldenCustomPath, PrefabCallback<GameObject> loadCallback)
	{
		string text = customPath;
		if (this.m_entity.GetPremiumType() == TAG_PREMIUM.GOLDEN && !string.IsNullOrEmpty(goldenCustomPath))
		{
			text = goldenCustomPath;
		}
		else if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		return new Card.SpellLoadRequest
		{
			m_path = text,
			m_loadCallback = loadCallback
		};
	}

	// Token: 0x06007551 RID: 30033 RVA: 0x00259A5C File Offset: 0x00257C5C
	private void OnCustomSummonSpellLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Error.AddDevFatal("Card.OnCustomSummonSpellLoaded() - FAILED to load \"{0}\" for card {1}", new object[]
			{
				assetRef,
				this
			});
			this.FinishSpellLoad();
			return;
		}
		this.m_customSummonSpell = go.GetComponent<Spell>();
		if (this.m_customSummonSpell == null)
		{
			this.FinishSpellLoad();
			return;
		}
		SpellUtils.SetupSpell(this.m_customSummonSpell, this);
		this.FinishSpellLoad();
	}

	// Token: 0x06007552 RID: 30034 RVA: 0x00259AC4 File Offset: 0x00257CC4
	private void OnCustomDeathSpellLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Error.AddDevFatal("Card.OnCustomDeathSpellLoaded() - FAILED to load \"{0}\" for card {1}", new object[]
			{
				assetRef,
				this
			});
			this.FinishSpellLoad();
			return;
		}
		this.m_customDeathSpell = go.GetComponent<Spell>();
		if (this.m_customDeathSpell == null)
		{
			this.FinishSpellLoad();
			return;
		}
		SpellUtils.SetupSpell(this.m_customDeathSpell, this);
		this.FinishSpellLoad();
	}

	// Token: 0x06007553 RID: 30035 RVA: 0x00259B2C File Offset: 0x00257D2C
	private void OnCustomDiscardSpellLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Error.AddDevFatal("Card.OnCustomDiscardSpellLoaded() - FAILED to load \"{0}\" for card {1}", new object[]
			{
				assetRef,
				this
			});
			this.FinishSpellLoad();
			return;
		}
		this.m_customDiscardSpell = go.GetComponent<Spell>();
		if (this.m_customDiscardSpell == null)
		{
			this.FinishSpellLoad();
			return;
		}
		SpellUtils.SetupSpell(this.m_customDiscardSpell, this);
		this.FinishSpellLoad();
	}

	// Token: 0x06007554 RID: 30036 RVA: 0x00259B94 File Offset: 0x00257D94
	private void OnCustomSpawnSpellLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Error.AddDevFatal("Card.OnCustomSpawnSpellLoaded() - FAILED to load \"{0}\" for card {1}", new object[]
			{
				assetRef,
				this
			});
			this.FinishSpellLoad();
			return;
		}
		this.m_customSpawnSpell = go.GetComponent<Spell>();
		if (this.m_customSpawnSpell == null)
		{
			this.FinishSpellLoad();
			return;
		}
		SpellUtils.SetupSpell(this.m_customSpawnSpell, this);
		this.FinishSpellLoad();
	}

	// Token: 0x06007555 RID: 30037 RVA: 0x00259BFC File Offset: 0x00257DFC
	private void FinishSpellLoad()
	{
		this.m_spellLoadCount--;
		if (this.m_spellLoadCount > 0)
		{
			return;
		}
		this.LoadActor();
	}

	// Token: 0x06007556 RID: 30038 RVA: 0x00259C1C File Offset: 0x00257E1C
	private void LoadActor()
	{
		this.RefreshHeroPowerTooltip();
		string text = this.m_cardDef.CardDef.DetermineActorPathForZone(this.m_entity, this.m_zone.m_ServerTag);
		if (this.m_actorPath == text || text == null)
		{
			this.m_actorPath = text;
			this.FinishActorLoad(this.m_actor);
			return;
		}
		AssetLoader.Get().InstantiatePrefab(text, new PrefabCallback<GameObject>(this.OnActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06007557 RID: 30039 RVA: 0x00259C95 File Offset: 0x00257E95
	private bool ShouldShowHeroPowerTooltip()
	{
		return this.m_heroPowerTooltip != null && this.m_zone is ZoneHand && this.m_entity.IsControlledByFriendlySidePlayer();
	}

	// Token: 0x06007558 RID: 30040 RVA: 0x00259CBF File Offset: 0x00257EBF
	private void CreateHeroPowerTooltip()
	{
		if (this.m_heroPowerTooltip == null)
		{
			this.m_heroPowerTooltip = base.gameObject.AddComponent<HeroPowerTooltip>();
			this.m_heroPowerTooltip.Setup(this);
		}
	}

	// Token: 0x06007559 RID: 30041 RVA: 0x00259CEC File Offset: 0x00257EEC
	private void DestroyHeroPowerTooltip()
	{
		if (this.m_heroPowerTooltip != null)
		{
			UnityEngine.Object.Destroy(this.m_heroPowerTooltip);
			this.m_heroPowerTooltip = null;
		}
	}

	// Token: 0x0600755A RID: 30042 RVA: 0x00259D10 File Offset: 0x00257F10
	public void RefreshHeroPowerTooltip()
	{
		this.DestroyHeroPowerTooltip();
		if (this.m_entity.IsHero() && this.m_zone is ZoneHand)
		{
			this.CreateHeroPowerTooltip();
			return;
		}
		if (this.m_entity.IsSidekickHero() && this.m_zone is ZoneHero)
		{
			this.CreateHeroPowerTooltip();
			return;
		}
		if (this.m_entity.HasTag(GAME_TAG.DISPLAY_CARD_ON_MOUSEOVER) && this.m_zone is ZoneHand)
		{
			this.CreateHeroPowerTooltip();
		}
	}

	// Token: 0x0600755B RID: 30043 RVA: 0x00259D8C File Offset: 0x00257F8C
	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("Card.OnActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning(string.Format("Card.OnActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		Actor actor = this.m_actor;
		this.m_actor = component;
		this.m_actorPath = assetRef.ToString();
		this.m_actor.SetEntity(this.m_entity);
		this.m_actor.SetCard(this);
		this.m_actor.SetCardDef(this.m_cardDef);
		this.m_actor.UpdateAllComponents();
		this.FinishActorLoad(actor);
	}

	// Token: 0x0600755C RID: 30044 RVA: 0x00259E2E File Offset: 0x0025802E
	private void FinishActorLoad(Actor oldActor)
	{
		this.m_actorLoading = false;
		this.OnZoneChanged();
		this.OnActorChanged(oldActor);
		if (this.m_isBattleCrySource)
		{
			SceneUtils.SetLayer(this.m_actor.gameObject, GameLayer.IgnoreFullScreenEffects);
		}
		this.RefreshActor();
	}

	// Token: 0x0600755D RID: 30045 RVA: 0x00259E64 File Offset: 0x00258064
	public void ForceLoadHandActor()
	{
		string text = this.m_cardDef.CardDef.DetermineActorPathForZone(this.m_entity, TAG_ZONE.HAND);
		if (this.m_actor != null && this.m_actorPath == text)
		{
			this.ShowCard();
			this.m_actor.Show();
			this.RefreshActor();
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("Card.ForceLoadHandActor() - FAILED to load actor \"{0}\"", new object[]
			{
				text
			});
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarningFormat("Card.ForceLoadHandActor() - ERROR actor \"{0}\" has no Actor component", new object[]
			{
				text
			});
			return;
		}
		if (this.m_actor != null)
		{
			this.m_actor.Destroy();
		}
		this.m_actor = component;
		this.m_actorPath = text;
		this.m_actor.SetEntity(this.m_entity);
		this.m_actor.SetCard(this);
		this.m_actor.SetCardDef(this.m_cardDef);
		this.m_actor.UpdateAllComponents();
		if (this.m_shown)
		{
			this.ShowImpl();
		}
		else
		{
			this.HideImpl();
		}
		this.RefreshActor();
	}

	// Token: 0x0600755E RID: 30046 RVA: 0x00259F8E File Offset: 0x0025818E
	private void HideHeroPowerTooltip()
	{
		if (this.m_heroPowerTooltip != null)
		{
			this.m_heroPowerTooltip.NotifyMousedOut();
		}
	}

	// Token: 0x0600755F RID: 30047 RVA: 0x00259FAC File Offset: 0x002581AC
	private void OnZoneChanged()
	{
		if (this.m_prevZone is ZoneHand && this.m_zone is ZoneGraveyard)
		{
			if (this.m_mousedOver)
			{
				this.NotifyMousedOut();
			}
			this.DoDiscardAnimation();
			this.HideHeroPowerTooltip();
		}
		else if (this.m_prevZone is ZoneHand)
		{
			if (this.m_mousedOver)
			{
				this.NotifyMousedOut();
			}
		}
		else if (this.m_zone is ZoneGraveyard)
		{
			if (this.m_entity.IsHero())
			{
				if (this.m_entity.HasTag(GAME_TAG.SIDEKICK))
				{
					this.DoNullZoneVisuals();
				}
				else
				{
					this.m_doNotSort = true;
				}
			}
		}
		else if (this.m_zone is ZoneHand)
		{
			if (!this.m_doNotSort)
			{
				this.ShowCard();
			}
			if (this.m_prevZone is ZoneGraveyard && this.m_entity.IsSpell())
			{
				this.m_actor.Hide();
				this.ActivateActorSpell(SpellType.SUMMON_IN, new Spell.FinishedCallback(this.OnSpellFinished_DefaultHandSpawn));
			}
		}
		else if ((this.m_prevZone is ZoneGraveyard || this.m_prevZone is ZoneDeck) && this.m_zone.m_ServerTag == TAG_ZONE.PLAY)
		{
			this.ShowCard();
		}
		if (!(this.m_zone is ZonePlay) && this.m_magneticPlayData != null)
		{
			SpellUtils.ActivateDeathIfNecessary(this.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_RIGHT, true));
			SpellUtils.ActivateDeathIfNecessary(this.m_magneticPlayData.m_targetMech.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT, true));
			SpellUtils.ActivateDeathIfNecessary(this.m_magneticPlayData.m_beamSpell);
			this.m_magneticPlayData = null;
		}
	}

	// Token: 0x06007560 RID: 30048 RVA: 0x0025A13C File Offset: 0x0025833C
	private void OnActorChanged(Actor oldActor)
	{
		this.HideTooltip();
		bool flag = false;
		bool flag2 = GameState.Get().IsGameCreating();
		if (this.m_prevZone == null && this.m_zone is ZoneGraveyard)
		{
			if (oldActor != null && oldActor != this.m_actor)
			{
				oldActor.Destroy();
			}
			if (this.IsShown())
			{
				this.HideCard();
			}
			else
			{
				this.HideImpl();
			}
			this.DeactivateHandStateSpells(null);
			flag = true;
			this.m_actorReady = true;
		}
		else if (oldActor == null)
		{
			bool flag3 = GameState.Get().IsMulliganPhaseNowOrPending();
			if (this.m_zone is ZoneHand && GameState.Get().IsBeginPhase())
			{
				bool flag4 = this.m_entity.GetCardId() == CoinManager.Get().GetFavoriteCoinCardId();
				if (flag3 && !GameState.Get().HasTheCoinBeenSpawned())
				{
					if (flag4)
					{
						GameState.Get().NotifyOfCoinSpawn();
						this.m_actor.TurnOffCollider();
						this.m_actor.Hide();
						this.m_actorReady = true;
						flag = true;
						base.transform.position = Vector3.zero;
						this.m_doNotWarpToNewZone = true;
						this.m_doNotSort = true;
					}
					else
					{
						global::Player controller = this.m_entity.GetController();
						if (controller.IsOpposingSide() && this == this.m_zone.GetLastCard() && !controller.HasTag(GAME_TAG.FIRST_PLAYER))
						{
							GameState.Get().NotifyOfCoinSpawn();
							this.m_actor.TurnOffCollider();
							this.m_actorReady = true;
							flag = true;
						}
					}
				}
				if (!flag4)
				{
					ZoneMgr.Get().FindZoneOfType<ZoneDeck>(this.m_zone.m_Side).SetCardToInDeckState(this);
				}
			}
			else if (flag2)
			{
				TransformUtil.CopyWorld(base.transform, this.m_zone.transform);
				if (this.m_zone is ZonePlay || this.m_zone is ZoneHero || this.m_zone is ZoneHeroPower || this.m_zone is ZoneWeapon)
				{
					this.ActivateLifetimeEffects();
				}
			}
			else
			{
				if (!this.m_doNotWarpToNewZone)
				{
					TransformUtil.CopyWorld(base.transform, this.m_zone.transform);
				}
				if (this.m_zone is ZoneHand)
				{
					if (!this.m_doNotWarpToNewZone)
					{
						ZoneHand zoneHand = (ZoneHand)this.m_zone;
						base.transform.localScale = zoneHand.GetCardScale();
						base.transform.localEulerAngles = zoneHand.GetCardRotation(this);
						base.transform.position = zoneHand.GetCardPosition(this);
					}
					if (this.m_entity.HasTag(GAME_TAG.LINKED_ENTITY))
					{
						int tag = this.m_entity.GetTag(GAME_TAG.LINKED_ENTITY);
						global::Entity entity = GameState.Get().GetEntity(tag);
						if (entity != null && entity.GetCard() != null)
						{
							this.m_actor.Hide();
							this.m_doNotSort = true;
							flag = true;
						}
					}
					else if (this.m_entity.HasTag(GAME_TAG.CREATOR) && GameState.Get().GetEntity(this.m_entity.GetTag(GAME_TAG.CREATOR)) != null && GameState.Get().GetEntity(this.m_entity.GetTag(GAME_TAG.CREATOR)).HasTag(GAME_TAG.TWINSPELL))
					{
						this.m_transitionStyle = ZoneTransitionStyle.INSTANT;
						this.ActivateHandSpawnSpell();
						InputManager.Get().GetFriendlyHand().ActivateTwinspellSpellDeath();
						InputManager.Get().GetFriendlyHand().ClearReservedCard();
					}
					else
					{
						this.m_actorReady = true;
						this.m_shown = true;
						if (!this.m_doNotWarpToNewZone)
						{
							this.m_actor.Hide();
							this.ActivateHandSpawnSpell();
							flag = true;
						}
					}
				}
				if (this.m_prevZone == null && this.m_zone is ZonePlay)
				{
					if (!this.m_doNotWarpToNewZone)
					{
						ZonePlay zonePlay = (ZonePlay)this.m_zone;
						base.transform.position = zonePlay.GetCardPosition(this);
					}
					if (this.m_cardDef.CardDef.m_SuppressPlaySoundsDuringMulligan && GameState.Get().IsMulliganPhaseNowOrPending())
					{
						this.SuppressPlaySounds(true);
					}
					if (this.m_entity.HasTag(GAME_TAG.LINKED_ENTITY))
					{
						if (this.m_customSpawnSpellOverride)
						{
							this.ActivateMinionSpawnEffects();
						}
						else
						{
							this.m_transitionStyle = ZoneTransitionStyle.INSTANT;
							Transform transform = Board.Get().FindBone("SpawnOffscreen");
							base.transform.position = transform.position;
							this.ActivateCharacterPlayEffects();
							this.OnSpellFinished_StandardSpawnCharacter(null, null);
						}
					}
					else
					{
						this.m_actor.Hide();
						this.ActivateMinionSpawnEffects();
					}
					flag = true;
				}
				else if (!flag3 && (this.m_zone is ZoneHeroPower || this.m_zone is ZoneWeapon))
				{
					if (this.IsShown())
					{
						this.ActivatePlaySpawnEffects_HeroPowerOrWeapon();
						flag = true;
						this.m_actorReady = true;
					}
				}
				else if (this.m_prevZone == null && this.m_zone is ZoneHero)
				{
					global::Entity entity2 = this.m_entity;
					if (entity2.HasTag(GAME_TAG.SIDEKICK))
					{
						this.ActivateStandardSpawnHeroSpell();
						flag = true;
					}
					else if (entity2.HasTag(GAME_TAG.TREAT_AS_PLAYED_HERO_CARD))
					{
						Card oldHeroCard = HeroCustomSummonSpell.GetOldHeroCard(entity2.GetCard());
						if (oldHeroCard != null)
						{
							entity2.GetCard().GetActor().Hide();
							HeroCustomSummonSpell.HideStats(oldHeroCard);
							oldHeroCard.SetDelayBeforeHideInNullZoneVisuals(0.8f);
						}
						this.ActivateStandardSpawnHeroSpell();
						flag = true;
					}
				}
			}
		}
		else if (this.m_prevZone == null && (this.m_zone is ZoneHeroPower || this.m_zone is ZoneWeapon))
		{
			oldActor.Destroy();
			TransformUtil.CopyWorld(base.transform, this.m_zone.transform);
			this.m_transitionStyle = ZoneTransitionStyle.INSTANT;
			this.ActivatePlaySpawnEffects_HeroPowerOrWeapon();
			flag = true;
			this.m_actorReady = true;
		}
		else if (this.m_prevZone == null && this.m_zone is ZoneHand && oldActor == this.m_actor && !this.m_goingThroughDeathrattleReturnfromGraveyard)
		{
			this.ActivateHandStateSpells(false);
			flag = true;
			this.m_actorReady = true;
		}
		else if (this.m_prevZone is ZoneHand && (this.m_zone is ZonePlay || this.m_zone is ZoneHero))
		{
			if (this.m_entity.IsObfuscated())
			{
				flag = true;
				this.m_actorReady = true;
			}
			else
			{
				this.ActivateActorSpells_HandToPlay(oldActor);
				if (this.m_cardDef.CardDef.m_SuppressPlaySoundsOnSummon || this.m_entity.HasTag(GAME_TAG.CARD_DOES_NOTHING))
				{
					this.SuppressPlaySounds(true);
				}
				this.ActivateCharacterPlayEffects();
				this.m_actor.Hide();
				flag = true;
				if (CardTypeBanner.Get() != null && CardTypeBanner.Get().HasCardDef && CardTypeBanner.Get().HasSameCardDef(this.m_cardDef.CardDef))
				{
					CardTypeBanner.Get().Hide();
				}
			}
		}
		else if (this.m_prevZone is ZoneHand && this.m_zone is ZoneWeapon)
		{
			if (this.ActivateActorSpells_HandToWeapon(oldActor))
			{
				this.m_actor.Hide();
				flag = true;
				if (CardTypeBanner.Get() != null && CardTypeBanner.Get().HasCardDef && CardTypeBanner.Get().HasSameCardDef(this.m_cardDef.CardDef))
				{
					CardTypeBanner.Get().Hide();
				}
			}
		}
		else if ((this.m_prevZone is ZonePlay || this.m_prevZone is ZoneHero) && this.m_zone is ZoneHand)
		{
			this.DeactivateLifetimeEffects();
			if (this.m_mousedOver && this.m_entity.IsControlledByFriendlySidePlayer())
			{
				if (this.m_entity.HasSpellPower())
				{
					ZoneMgr.Get().OnSpellPowerEntityMousedOut(this.m_entity.GetSpellPowerSchool());
				}
				if (this.m_entity.HasHealingDoesDamageHint())
				{
					ZoneMgr.Get().OnHealingDoesDamageEntityMousedOut();
				}
			}
			bool useFastAnimations = GameState.Get().GetGameEntity().GetTag(GAME_TAG.BACON_USE_FAST_ANIMATIONS) > 0;
			if (this.DoPlayToHandTransition(oldActor, false, useFastAnimations))
			{
				flag = true;
			}
		}
		else if (this.m_prevZone is ZoneHero && this.m_zone is ZoneGraveyard)
		{
			oldActor.DoCardDeathVisuals();
			this.DeactivateCustomKeywordEffect();
			flag = true;
			this.m_actorReady = true;
		}
		else if (this.m_prevZone != null && (this.m_prevZone is ZonePlay || this.m_prevZone is ZoneWeapon || this.m_prevZone is ZoneHeroPower) && this.m_zone is ZoneGraveyard)
		{
			if (this.m_mousedOver && this.m_entity.IsControlledByFriendlySidePlayer() && this.m_prevZone is ZonePlay)
			{
				if (this.m_entity.HasSpellPower())
				{
					ZoneMgr.Get().OnSpellPowerEntityMousedOut(this.m_entity.GetSpellPowerSchool());
				}
				if (this.m_entity.HasHealingDoesDamageHint())
				{
					ZoneMgr.Get().OnHealingDoesDamageEntityMousedOut();
				}
			}
			if (this.m_entity.HasTag(GAME_TAG.DEATHRATTLE_RETURN_ZONE) && this.DoesCardReturnFromGraveyard())
			{
				this.m_playZoneBlockerSide = new global::Player.Side?(this.m_prevZone.m_Side);
				this.m_prevZone.AddLayoutBlocker();
				this.m_goingThroughDeathrattleReturnfromGraveyard = true;
				TAG_ZONE tag2 = this.m_entity.GetTag<TAG_ZONE>(GAME_TAG.DEATHRATTLE_RETURN_ZONE);
				int cardFutureController = this.GetCardFutureController();
				Zone zone = ZoneMgr.Get().FindZoneForTags(cardFutureController, tag2, this.m_entity.GetCardType(), this.m_entity);
				if (zone is ZoneDeck)
				{
					zone.AddLayoutBlocker();
				}
				this.m_actorWaitingToBeReplaced = oldActor;
				this.m_actor.Hide();
				flag = true;
				this.m_actorReady = true;
			}
			else if (this.HandlePlayActorDeath(oldActor))
			{
				flag = true;
			}
		}
		else if (this.m_prevZone is ZoneDeck && this.m_zone is ZoneHand)
		{
			if (this.m_zone.m_Side == global::Player.Side.FRIENDLY)
			{
				if (GameState.Get().IsPastBeginPhase())
				{
					this.m_actorWaitingToBeReplaced = oldActor;
					this.m_cardStandInInteractive = false;
					if (!TurnStartManager.Get().IsCardDrawHandled(this))
					{
						this.DrawFriendlyCard();
					}
					flag = true;
				}
				else
				{
					this.m_actor.TurnOffCollider();
					this.m_actor.SetActorState(ActorStateType.CARD_IDLE);
				}
			}
			else if (GameState.Get().IsPastBeginPhase())
			{
				if (oldActor != null)
				{
					oldActor.Destroy();
				}
				this.DrawOpponentCard();
				flag = true;
			}
		}
		else if (this.m_prevZone is ZoneSecret && this.m_zone is ZoneGraveyard && this.m_entity.IsSecret())
		{
			flag = true;
			this.m_actorReady = true;
			if (UniversalInputManager.UsePhoneUI)
			{
				this.m_shown = false;
				this.m_actor.Hide();
			}
			else
			{
				this.ShowSecretDeath(oldActor);
			}
		}
		else if (this.m_prevZone is ZoneGraveyard && this.m_zone is ZonePlay)
		{
			this.m_actor.Hide();
			base.StartCoroutine(this.ActivateReviveSpell());
			flag = true;
		}
		else if (this.m_prevZone is ZoneDeck && this.m_zone is ZoneGraveyard)
		{
			this.MillCard();
			flag = true;
		}
		else if (this.m_prevZone is ZoneDeck && this.m_zone is ZonePlay)
		{
			if (oldActor != null)
			{
				oldActor.Destroy();
			}
			this.AnimateDeckToPlay();
			flag = true;
		}
		else if (this.m_prevZone is ZonePlay && this.m_zone is ZoneDeck)
		{
			this.DeactivateLifetimeEffects();
			this.m_playZoneBlockerSide = new global::Player.Side?(this.m_prevZone.m_Side);
			this.m_prevZone.AddLayoutBlocker();
			ZoneMgr.Get().FindZoneOfType<ZoneDeck>(this.m_zone.m_Side).AddLayoutBlocker();
			this.DoPlayToDeckTransition(oldActor);
			flag = true;
		}
		else if (this.m_prevZone is ZoneHand && this.m_zone is ZoneDeck && GameState.Get().IsPastBeginPhase())
		{
			if (!this.m_suppressHandToDeckTransition)
			{
				base.StartCoroutine(this.DoHandToDeckTransition(oldActor));
			}
			else
			{
				oldActor.Destroy();
				this.m_actorReady = true;
			}
			this.m_suppressHandToDeckTransition = false;
			flag = true;
		}
		else if (this.m_goingThroughDeathrattleReturnfromGraveyard && this.m_zone is ZoneDeck)
		{
			this.m_goingThroughDeathrattleReturnfromGraveyard = false;
			if (this.HandleGraveyardToDeck(oldActor))
			{
				flag = true;
			}
		}
		else if (this.m_goingThroughDeathrattleReturnfromGraveyard && this.m_zone is ZoneHand)
		{
			this.m_goingThroughDeathrattleReturnfromGraveyard = false;
			if (this.HandleGraveyardToHand(oldActor))
			{
				flag = true;
			}
		}
		if (!flag && oldActor == this.m_actor)
		{
			if (this.m_prevZone != null && this.m_prevZone.m_Side != this.m_zone.m_Side && this.m_prevZone is ZoneSecret && this.m_zone is ZoneSecret)
			{
				base.StartCoroutine(this.SwitchSecretSides());
				flag = true;
			}
			if (!flag)
			{
				this.m_actorReady = true;
			}
			return;
		}
		if (!flag && this.m_zone is ZoneSecret)
		{
			this.m_shown = true;
			if (oldActor)
			{
				oldActor.Destroy();
			}
			this.m_transitionStyle = ZoneTransitionStyle.INSTANT;
			this.m_zone.UpdateLayout();
			this.ShowSecretQuestBirth();
			flag = true;
			this.m_actorReady = true;
			if (flag2)
			{
				this.ActivateStateSpells(false);
			}
		}
		if (!flag)
		{
			if (oldActor)
			{
				oldActor.Destroy();
			}
			bool flag5 = this.m_zone.m_ServerTag == TAG_ZONE.PLAY || this.m_zone.m_ServerTag == TAG_ZONE.SECRET || this.m_zone.m_ServerTag == TAG_ZONE.HAND;
			if (this.IsShown() && flag5)
			{
				this.ActivateStateSpells(false);
			}
			this.m_actorReady = true;
			if (this.IsShown())
			{
				this.ShowImpl();
				return;
			}
			this.HideImpl();
		}
	}

	// Token: 0x06007561 RID: 30049 RVA: 0x0025AEAB File Offset: 0x002590AB
	private bool HandleGraveyardToDeck(Actor oldActor)
	{
		if (this.m_actorWaitingToBeReplaced)
		{
			if (oldActor)
			{
				oldActor.Destroy();
			}
			oldActor = this.m_actorWaitingToBeReplaced;
			this.m_actorWaitingToBeReplaced = null;
			this.DoPlayToDeckTransition(oldActor);
			return true;
		}
		return false;
	}

	// Token: 0x06007562 RID: 30050 RVA: 0x0025AEE4 File Offset: 0x002590E4
	private bool HandleGraveyardToHand(Actor oldActor)
	{
		if (this.m_actorWaitingToBeReplaced)
		{
			if (oldActor && oldActor != this.m_actor)
			{
				oldActor.Destroy();
			}
			oldActor = this.m_actorWaitingToBeReplaced;
			this.m_actorWaitingToBeReplaced = null;
			bool useFastAnimations = GameState.Get().GetGameEntity().GetTag(GAME_TAG.BACON_USE_FAST_ANIMATIONS) > 0;
			if (this.DoPlayToHandTransition(oldActor, true, useFastAnimations))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06007563 RID: 30051 RVA: 0x0025AF4F File Offset: 0x0025914F
	public bool CardStandInIsInteractive()
	{
		return this.m_cardStandInInteractive;
	}

	// Token: 0x06007564 RID: 30052 RVA: 0x0025AF57 File Offset: 0x00259157
	private void ReadyCardForDraw()
	{
		this.GetController().GetDeckZone().SetCardToInDeckState(this);
	}

	// Token: 0x06007565 RID: 30053 RVA: 0x0025AF6A File Offset: 0x0025916A
	public void DrawFriendlyCard()
	{
		base.StartCoroutine(this.DrawFriendlyCardWithTiming());
	}

	// Token: 0x06007566 RID: 30054 RVA: 0x0025AF79 File Offset: 0x00259179
	private IEnumerator DrawFriendlyCardWithTiming()
	{
		this.m_doNotSort = true;
		this.m_transitionStyle = ZoneTransitionStyle.SLOW;
		this.m_actor.Hide();
		while (GameState.Get().GetFriendlyCardBeingDrawn())
		{
			yield return null;
		}
		GameState.Get().SetFriendlyCardBeingDrawn(this);
		this.ReadyCardForDraw();
		Actor cardDrawStandIn = Gameplay.Get().GetCardDrawStandIn();
		cardDrawStandIn.transform.parent = this.m_actor.transform.parent;
		cardDrawStandIn.transform.localPosition = Vector3.zero;
		cardDrawStandIn.transform.localScale = Vector3.one;
		cardDrawStandIn.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		cardDrawStandIn.Show();
		cardDrawStandIn.GetRootObject().GetComponentInChildren<CardBackDisplay>().SetCardBack(CardBackManager.CardBackSlot.FRIENDLY);
		if (this.m_actorWaitingToBeReplaced != null)
		{
			this.m_actorWaitingToBeReplaced.Destroy();
			this.m_actorWaitingToBeReplaced = null;
		}
		this.DetermineIfOverrideDrawTimeScale();
		Transform transform = Board.Get().FindBone("FriendlyDrawCard");
		Vector3[] array = new Vector3[]
		{
			base.gameObject.transform.position,
			base.gameObject.transform.position + Card.ABOVE_DECK_OFFSET,
			transform.position
		};
		float num = 1.5f * this.m_drawTimeScale.Value;
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"path",
			array,
			"time",
			num,
			"easetype",
			iTween.EaseType.easeInSineOutExpo
		}));
		base.gameObject.transform.localEulerAngles = new Vector3(270f, 270f, 0f);
		Vector3 vector = new Vector3(0f, 0f, 357f);
		float num2 = 1.35f * this.m_drawTimeScale.Value;
		float num3 = 0.15f * this.m_drawTimeScale.Value;
		iTween.RotateTo(base.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			vector,
			"time",
			num2,
			"delay",
			num3
		}));
		float num4 = 0.75f * this.m_drawTimeScale.Value;
		float num5 = 0.15f * this.m_drawTimeScale.Value;
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			transform.localScale,
			"time",
			num4,
			"delay",
			num5
		}));
		SoundManager.Get().LoadAndPlay("draw_card_1.prefab:19dd221ebfed9754e85ef1f104e0fddb", base.gameObject);
		cardDrawStandIn.transform.parent = null;
		cardDrawStandIn.Hide();
		this.m_actor.Show();
		this.m_actor.TurnOffCollider();
		GameState.Get().GetFriendlySidePlayer().GetDeckZone().UpdateLayout();
		while (iTween.Count(base.gameObject) > 0)
		{
			yield return null;
		}
		this.m_actorReady = true;
		if (this.ShouldCardDrawWaitForTurnStartSpells())
		{
			yield return base.StartCoroutine(this.WaitForCardDrawBlockingTurnStartSpells());
		}
		else
		{
			PowerTask cardDrawBlockingTask = this.GetPowerTaskToBlockCardDraw();
			if (cardDrawBlockingTask != null)
			{
				while (!cardDrawBlockingTask.IsCompleted())
				{
					yield return null;
				}
			}
			cardDrawBlockingTask = null;
		}
		this.m_doNotSort = false;
		GameState.Get().ClearCardBeingDrawn(this);
		this.ResetCardDrawTimeScale();
		if (this.m_zone != null && this.m_zone is ZoneHand)
		{
			ZoneHand handZone = (ZoneHand)this.m_zone;
			SoundManager.Get().LoadAndPlay("add_card_to_hand_1.prefab:bf6b149b859734c4faf9a96356c53646", base.gameObject);
			this.ActivateStateSpells(false);
			this.RefreshActor();
			this.m_zone.UpdateLayout();
			yield return new WaitForSeconds(0.3f);
			this.m_cardStandInInteractive = true;
			handZone.MakeStandInInteractive(this);
			handZone = null;
		}
		yield break;
	}

	// Token: 0x06007567 RID: 30055 RVA: 0x0025AF88 File Offset: 0x00259188
	public bool IsBeingDrawnByOpponent()
	{
		return this.m_beingDrawnByOpponent;
	}

	// Token: 0x06007568 RID: 30056 RVA: 0x0025AF90 File Offset: 0x00259190
	private void DrawOpponentCard()
	{
		base.StartCoroutine(this.DrawOpponentCardWithTiming());
	}

	// Token: 0x06007569 RID: 30057 RVA: 0x0025AF9F File Offset: 0x0025919F
	private IEnumerator DrawOpponentCardWithTiming()
	{
		this.m_doNotSort = true;
		this.m_beingDrawnByOpponent = true;
		this.m_actor.Hide();
		while (GameState.Get().GetOpponentCardBeingDrawn())
		{
			yield return null;
		}
		if (this.GetZonePosition() == 0)
		{
			yield return null;
		}
		this.m_actor.Show();
		GameState.Get().SetOpponentCardBeingDrawn(this);
		this.ReadyCardForDraw();
		ZoneHand zoneHand = (ZoneHand)this.m_zone;
		zoneHand.UpdateLayout();
		if (this.m_entity.HasTag(GAME_TAG.REVEALED))
		{
			base.StartCoroutine(this.DrawKnownOpponentCard(zoneHand));
		}
		else
		{
			base.StartCoroutine(this.DrawUnknownOpponentCard(zoneHand));
		}
		yield break;
	}

	// Token: 0x0600756A RID: 30058 RVA: 0x0025AFAE File Offset: 0x002591AE
	private IEnumerator DrawUnknownOpponentCard(ZoneHand handZone)
	{
		SoundManager.Get().LoadAndPlay("draw_card_and_add_to_hand_opp_1.prefab:5a05fbb2c5833a94182e1b454647d5c8", base.gameObject);
		base.gameObject.transform.rotation = Card.IN_DECK_HIDDEN_ROTATION;
		this.DetermineIfOverrideDrawTimeScale();
		Transform transform = Board.Get().FindBone("OpponentDrawCard");
		Vector3[] array = new Vector3[]
		{
			base.gameObject.transform.position,
			base.gameObject.transform.position + Card.ABOVE_DECK_OFFSET,
			transform.position,
			handZone.GetCardPosition(this)
		};
		float num = 1.75f * this.m_drawTimeScale.Value;
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"path",
			array,
			"time",
			num,
			"easetype",
			iTween.EaseType.easeInOutQuart
		}));
		float num2 = 0.7f * this.m_drawTimeScale.Value;
		float num3 = 0.8f * this.m_drawTimeScale.Value;
		iTween.RotateTo(base.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			handZone.GetCardRotation(this),
			"time",
			num2,
			"delay",
			num3,
			"easetype",
			iTween.EaseType.easeInOutCubic
		}));
		float num4 = 0.7f * this.m_drawTimeScale.Value;
		float num5 = 0.8f * this.m_drawTimeScale.Value;
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			handZone.GetCardScale(),
			"time",
			num4,
			"delay",
			num5,
			"easetype",
			iTween.EaseType.easeInOutQuint
		}));
		GameState.Get().GetOpposingSidePlayer().GetDeckZone().UpdateLayout();
		yield return new WaitForSeconds(0.2f);
		this.m_actorReady = true;
		yield return new WaitForSeconds(0.6f);
		GameState.Get().UpdateOptionHighlights();
		while (iTween.Count(base.gameObject) > 0)
		{
			yield return null;
		}
		this.m_doNotSort = false;
		this.m_beingDrawnByOpponent = false;
		GameState.Get().SetOpponentCardBeingDrawn(null);
		this.ResetCardDrawTimeScale();
		handZone.UpdateLayout();
		yield break;
	}

	// Token: 0x0600756B RID: 30059 RVA: 0x0025AFC4 File Offset: 0x002591C4
	private IEnumerator DrawKnownOpponentCard(ZoneHand handZone)
	{
		Actor handActor = null;
		bool loadingActor = true;
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			loadingActor = false;
			if (go == null)
			{
				Error.AddDevFatal("Card.DrawKnownOpponentCard() - failed to load {0}", new object[]
				{
					assetRef
				});
				return;
			}
			handActor = go.GetComponent<Actor>();
			if (handActor == null)
			{
				Error.AddDevFatal("Card.DrawKnownOpponentCard() - instance of {0} has no Actor component", new object[]
				{
					this.name
				});
				return;
			}
		};
		string actorPath = ActorNames.GetHandActor(this.m_entity);
		AssetLoader.Get().InstantiatePrefab(actorPath, callback, null, AssetLoadingOptions.IgnorePrefabPosition);
		while (loadingActor)
		{
			yield return null;
		}
		if (handActor)
		{
			handActor.SetEntity(this.m_entity);
			handActor.SetCardDef(this.m_cardDef);
			handActor.UpdateAllComponents();
			base.StartCoroutine(this.RevealDrawnOpponentCard(actorPath, handActor, handZone));
		}
		else
		{
			base.StartCoroutine(this.DrawUnknownOpponentCard(handZone));
		}
		yield break;
	}

	// Token: 0x0600756C RID: 30060 RVA: 0x0025AFDA File Offset: 0x002591DA
	private IEnumerator RevealDrawnOpponentCard(string handActorPath, Actor handActor, ZoneHand handZone)
	{
		SoundManager.Get().LoadAndPlay("draw_card_1.prefab:19dd221ebfed9754e85ef1f104e0fddb", base.gameObject);
		handActor.transform.parent = this.m_actor.transform.parent;
		TransformUtil.CopyLocal(handActor, this.m_actor);
		this.m_actor.Hide();
		this.DetermineIfOverrideDrawTimeScale();
		base.gameObject.transform.localEulerAngles = new Vector3(270f, 90f, 0f);
		string text = "OpponentDrawCardAndReveal";
		if (UniversalInputManager.UsePhoneUI)
		{
			text += "_phone";
		}
		Transform transform = Board.Get().FindBone(text);
		Vector3[] array = new Vector3[]
		{
			base.gameObject.transform.position,
			base.gameObject.transform.position + Card.ABOVE_DECK_OFFSET,
			transform.position
		};
		float num = 1.75f * this.m_drawTimeScale.Value;
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"path",
			array,
			"time",
			num,
			"easetype",
			iTween.EaseType.easeInOutQuart
		}));
		float num2 = 0.7f * this.m_drawTimeScale.Value;
		float num3 = 0.8f * this.m_drawTimeScale.Value;
		iTween.RotateTo(base.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			transform.eulerAngles,
			"time",
			num2,
			"delay",
			num3,
			"easetype",
			iTween.EaseType.easeInOutCubic
		}));
		float num4 = 0.7f * this.m_drawTimeScale.Value;
		float num5 = 0.8f * this.m_drawTimeScale.Value;
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			transform.localScale,
			"time",
			num4,
			"delay",
			num5,
			"easetype",
			iTween.EaseType.easeInOutQuint
		}));
		GameState.Get().GetOpposingSidePlayer().GetDeckZone().UpdateLayout();
		yield return new WaitForSeconds(1.75f);
		this.m_actorReady = true;
		this.m_beingDrawnByOpponent = false;
		string actorName = this.m_actorPath;
		this.m_actorWaitingToBeReplaced = this.m_actor;
		this.m_actorPath = handActorPath;
		this.m_actor = handActor;
		PowerTask cardDrawBlockingTask = this.GetPowerTaskToBlockCardDraw();
		if (cardDrawBlockingTask != null)
		{
			while (!cardDrawBlockingTask.IsCompleted())
			{
				yield return null;
			}
			if (handActor == null)
			{
				handActor = this.m_actor;
			}
		}
		if (this.m_entity.GetZone() != TAG_ZONE.HAND)
		{
			this.m_doNotSort = false;
			GameState.Get().ClearCardBeingDrawn(this);
			this.ResetCardDrawTimeScale();
			yield break;
		}
		this.m_actor = this.m_actorWaitingToBeReplaced;
		this.m_actorPath = actorName;
		this.m_actorWaitingToBeReplaced = null;
		this.m_beingDrawnByOpponent = true;
		yield return base.StartCoroutine(this.HideRevealedOpponentCard(handActor));
		yield break;
	}

	// Token: 0x0600756D RID: 30061 RVA: 0x0025AFF7 File Offset: 0x002591F7
	private IEnumerator HideRevealedOpponentCard(Actor handActor)
	{
		float num = 0.5f;
		float num2 = 0.525f * num;
		if (!this.GetController().IsRevealed())
		{
			float num3 = 180f;
			TransformUtil.SetEulerAngleZ(this.m_actor.gameObject, -num3);
			if (handActor != null)
			{
				iTween.RotateAdd(handActor.gameObject, iTween.Hash(new object[]
				{
					"z",
					num3,
					"time",
					num,
					"easetype",
					iTween.EaseType.easeInOutCubic
				}));
			}
			iTween.RotateAdd(this.m_actor.gameObject, iTween.Hash(new object[]
			{
				"z",
				num3,
				"time",
				num,
				"easetype",
				iTween.EaseType.easeInOutCubic
			}));
		}
		Action<object> action = delegate(object obj)
		{
			if (handActor != null)
			{
				UnityEngine.Object.Destroy(handActor.gameObject);
			}
			this.m_actor.Show();
		};
		iTween.Timer(this.m_actor.gameObject, iTween.Hash(new object[]
		{
			"time",
			num2,
			"oncomplete",
			action
		}));
		yield return new WaitForSeconds(num);
		this.m_doNotSort = false;
		this.m_beingDrawnByOpponent = false;
		GameState.Get().SetOpponentCardBeingDrawn(null);
		this.ResetCardDrawTimeScale();
		SoundManager.Get().LoadAndPlay("add_card_to_hand_1.prefab:bf6b149b859734c4faf9a96356c53646", base.gameObject);
		this.ActivateStateSpells(false);
		this.RefreshActor();
		this.m_zone.UpdateLayout();
		yield break;
	}

	// Token: 0x0600756E RID: 30062 RVA: 0x0025B010 File Offset: 0x00259210
	private void AnimateDeckToPlay()
	{
		if (this.m_customSpawnSpellOverride == null)
		{
			((ZonePlay)this.m_zone).AddLayoutBlocker();
			ZoneDeck zoneDeck = ZoneMgr.Get().FindZoneOfType<ZoneDeck>(this.m_zone.m_Side);
			if (this.m_latestZoneChange != null && this.m_latestZoneChange.GetSourceControllerId() != 0 && this.m_latestZoneChange.GetSourceControllerId() != this.m_latestZoneChange.GetDestinationControllerId() && this.m_latestZoneChange.GetSourceZone() is ZoneDeck)
			{
				zoneDeck = (ZoneDeck)this.m_latestZoneChange.GetSourceZone();
			}
			zoneDeck.SetCardToInDeckState(this);
			this.m_doNotSort = true;
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(this.m_entity), AssetLoadingOptions.IgnorePrefabPosition);
			Actor component = gameObject.GetComponent<Actor>();
			this.SetupDeckToPlayActor(component, gameObject);
			SpellType spellType = this.m_cardDef.CardDef.DetermineSummonOutSpell_HandToPlay(this);
			Spell spell = component.GetSpell(spellType);
			GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", AssetLoadingOptions.IgnorePrefabPosition);
			Actor component2 = gameObject2.GetComponent<Actor>();
			this.SetupDeckToPlayActor(component2, gameObject2);
			base.StartCoroutine(this.AnimateDeckToPlay(component, spell, component2));
			return;
		}
		this.m_actor.Hide();
		Zone zone = (ZonePlay)this.m_zone;
		this.SetTransitionStyle(ZoneTransitionStyle.INSTANT);
		zone.UpdateLayout();
		this.ActivateMinionSpawnEffects();
	}

	// Token: 0x0600756F RID: 30063 RVA: 0x0025B160 File Offset: 0x00259360
	private void SetupDeckToPlayActor(Actor actor, GameObject actorObject)
	{
		actor.SetEntity(this.m_entity);
		actor.SetCardDef(this.m_cardDef);
		actor.UpdateAllComponents();
		actorObject.transform.parent = base.transform;
		actorObject.transform.localPosition = Vector3.zero;
		actorObject.transform.localScale = Vector3.one;
		actorObject.transform.localRotation = Quaternion.identity;
	}

	// Token: 0x06007570 RID: 30064 RVA: 0x0025B1CC File Offset: 0x002593CC
	private IEnumerator AnimateDeckToPlay(Actor cardFaceActor, Spell outSpell, Actor hiddenActor)
	{
		cardFaceActor.Hide();
		this.m_actor.Hide();
		hiddenActor.Hide();
		this.m_inputEnabled = false;
		SoundManager.Get().LoadAndPlay("draw_card_into_play.prefab:52139cc25c53e184fab47b23c72df0d1", base.gameObject);
		base.gameObject.transform.localEulerAngles = new Vector3(270f, 90f, 0f);
		iTween.MoveTo(base.gameObject, base.gameObject.transform.position + Card.ABOVE_DECK_OFFSET, 0.6f);
		iTween.RotateTo(base.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 0f, 0f),
			"time",
			0.7f,
			"delay",
			0.6f,
			"easetype",
			iTween.EaseType.easeInOutCubic,
			"islocal",
			true
		}));
		hiddenActor.Show();
		yield return new WaitForSeconds(0.4f);
		iTween.MoveTo(hiddenActor.gameObject, iTween.Hash(new object[]
		{
			"position",
			new Vector3(0f, 3f, 0f),
			"time",
			1f,
			"delay",
			0f,
			"islocal",
			true
		}));
		this.m_doNotSort = false;
		ZonePlay zonePlay = (ZonePlay)this.m_zone;
		zonePlay.RemoveLayoutBlocker();
		zonePlay.SetTransitionTime(1.6f);
		zonePlay.UpdateLayout();
		yield return new WaitForSeconds(0.2f);
		float cardFlipTime = 0.35f;
		iTween.RotateTo(hiddenActor.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 0f, -90f),
			"time",
			cardFlipTime,
			"delay",
			0f,
			"easetype",
			iTween.EaseType.easeInCubic,
			"islocal",
			true
		}));
		yield return new WaitForSeconds(cardFlipTime);
		hiddenActor.Destroy();
		cardFaceActor.Show();
		cardFaceActor.gameObject.transform.localPosition = new Vector3(0f, 3f, 0f);
		cardFaceActor.gameObject.transform.Rotate(new Vector3(0f, 0f, 90f));
		iTween.RotateTo(cardFaceActor.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 0f, 0f),
			"time",
			cardFlipTime,
			"delay",
			0f,
			"easetype",
			iTween.EaseType.easeOutCubic,
			"islocal",
			true
		}));
		this.m_actor.gameObject.transform.localPosition = new Vector3(0f, 2.86f, 0f);
		cardFaceActor.gameObject.transform.localPosition = new Vector3(0f, 2.86f, 0f);
		iTween.MoveTo(hiddenActor.gameObject, iTween.Hash(new object[]
		{
			"position",
			Vector3.zero,
			"time",
			1f,
			"delay",
			0f,
			"islocal",
			true
		}));
		this.ActivateSpell(outSpell, new Spell.FinishedCallback(this.OnSpellFinished_HandToPlay_SummonOut), null, new Spell.StateFinishedCallback(this.OnSpellStateFinished_DestroyActor));
		this.ActivateCharacterPlayEffects();
		this.m_actor.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		yield break;
	}

	// Token: 0x06007571 RID: 30065 RVA: 0x0025B1F0 File Offset: 0x002593F0
	public void SetSkipMilling(bool skipMilling)
	{
		this.m_skipMilling = skipMilling;
	}

	// Token: 0x06007572 RID: 30066 RVA: 0x0025B1F9 File Offset: 0x002593F9
	private void MillCard()
	{
		if (this.m_skipMilling)
		{
			this.m_actor.Hide();
			return;
		}
		base.StartCoroutine(this.MillCardWithTiming());
	}

	// Token: 0x06007573 RID: 30067 RVA: 0x0025B21C File Offset: 0x0025941C
	private IEnumerator MillCardWithTiming()
	{
		this.SetDoNotSort(true);
		this.ReadyCardForDraw();
		global::Player cardOwner = this.m_entity.GetController();
		string name;
		if (cardOwner.IsFriendlySide())
		{
			while (GameState.Get().GetFriendlyCardBeingDrawn())
			{
				yield return null;
			}
			GameState.Get().SetFriendlyCardBeingDrawn(this);
			name = "FriendlyMillCard";
		}
		else
		{
			while (GameState.Get().GetOpponentCardBeingDrawn())
			{
				yield return null;
			}
			GameState.Get().SetOpponentCardBeingDrawn(this);
			name = "OpponentMillCard";
		}
		int turn = GameState.Get().GetTurn();
		if (turn != GameState.Get().GetLastTurnRemindedOfFullHand() && cardOwner.GetHandZone().GetCardCount() >= 10)
		{
			GameState.Get().SetLastTurnRemindedOfFullHand(turn);
			cardOwner.GetHeroCard().PlayEmote(EmoteType.ERROR_HAND_FULL);
		}
		this.m_actor.Show();
		this.m_actor.TurnOffCollider();
		Transform transform = Board.Get().FindBone(name);
		Vector3[] array = new Vector3[]
		{
			base.gameObject.transform.position,
			base.gameObject.transform.position + Card.ABOVE_DECK_OFFSET,
			transform.position
		};
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"path",
			array,
			"time",
			1.5f,
			"easetype",
			iTween.EaseType.easeInSineOutExpo
		}));
		base.gameObject.transform.localEulerAngles = new Vector3(270f, 270f, 0f);
		iTween.RotateTo(base.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 0f, 357f),
			"time",
			1.35f,
			"delay",
			0.15f
		}));
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			transform.localScale,
			"time",
			0.75f,
			"delay",
			0.15f
		}));
		while (iTween.Count(base.gameObject) > 0)
		{
			yield return null;
		}
		this.m_actorReady = true;
		this.RefreshActor();
		Spell spell = this.m_actor.GetSpell(SpellType.HANDFULL);
		spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSpellStateFinished_DestroyActor));
		spell.Activate();
		GameState.Get().ClearCardBeingDrawn(this);
		this.SetDoNotSort(false);
		yield break;
	}

	// Token: 0x06007574 RID: 30068 RVA: 0x0025B22C File Offset: 0x0025942C
	private void ActivateActorSpells_HandToPlay(Actor oldActor)
	{
		if (oldActor == null)
		{
			Debug.LogError(string.Format("{0}.ActivateActorSpells_HandToPlay() - oldActor=null", this));
			return;
		}
		if (this.m_cardDef == null)
		{
			Debug.LogError(string.Format("{0}.ActivateActorSpells_HandToPlay() - m_cardDef=null", this));
			return;
		}
		if (this.m_actor == null)
		{
			Debug.LogError(string.Format("{0}.ActivateActorSpells_HandToPlay() - m_actor=null", this));
			return;
		}
		this.DeactivateHandStateSpells(oldActor);
		SpellType spellType = this.m_cardDef.CardDef.DetermineSummonOutSpell_HandToPlay(this);
		Spell spell = oldActor.GetSpell(spellType);
		if (spell == null)
		{
			Debug.LogError(string.Format("{0}.ActivateActorSpells_HandToPlay() - outSpell=null outSpellType={1}", this, spellType));
			this.m_actorReady = true;
			return;
		}
		bool flag;
		if (this.GetBestSummonSpell(out flag) == null)
		{
			Debug.LogError(string.Format("{0}.ActivateActorSpells_HandToPlay() - inSpell=null standard={1}", this, flag));
			return;
		}
		this.m_inputEnabled = false;
		spell.SetSource(base.gameObject);
		this.ActivateSpell(spell, new Spell.FinishedCallback(this.OnSpellFinished_HandToPlay_SummonOut), oldActor, new Spell.StateFinishedCallback(this.OnSpellStateFinished_DestroyActor));
	}

	// Token: 0x06007575 RID: 30069 RVA: 0x0025B330 File Offset: 0x00259530
	private void OnSpellFinished_HandToPlay_SummonOut(Spell spell, object userData)
	{
		Actor actor = userData as Actor;
		this.m_actor.Show();
		if (this.m_magneticPlayData != null)
		{
			SpellUtils.ActivateDeathIfNecessary(actor.GetSpellIfLoaded(SpellType.MAGNETIC_HAND_LINKED_RIGHT));
			this.ActivateActorSpell(SpellType.MAGNETIC_PLAY_LINKED_RIGHT);
		}
		bool flag;
		Spell bestSummonSpell = this.GetBestSummonSpell(out flag);
		if (bestSummonSpell == null)
		{
			Debug.LogErrorFormat("{0}.OnSpellFinished_HandToPlay_SummonOut() - inSpell=null standard={1}", new object[]
			{
				this,
				flag
			});
			return;
		}
		if (!flag)
		{
			bestSummonSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSpellStateFinished_DestroySpell));
			SpellUtils.SetCustomSpellParent(bestSummonSpell, this.m_actor);
		}
		bestSummonSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnSpellFinished_HandToPlay_SummonIn));
		bestSummonSpell.Activate();
	}

	// Token: 0x06007576 RID: 30070 RVA: 0x0025B3E0 File Offset: 0x002595E0
	private void OnSpellFinished_HandToPlay_SummonIn(Spell spell, object userData)
	{
		this.m_actorReady = true;
		this.m_inputEnabled = true;
		this.ActivateStateSpells(false);
		this.RefreshActor();
		if (this.m_entity.IsControlledByFriendlySidePlayer() && !this.m_entity.GetRealTimeIsDormant())
		{
			if (this.m_entity.HasSpellPower() || this.m_entity.HasSpellPowerDouble())
			{
				ZoneMgr.Get().OnSpellPowerEntityEnteredPlay(this.m_entity.GetSpellPowerSchool());
			}
			if (this.m_entity.HasHealingDoesDamageHint())
			{
				ZoneMgr.Get().OnHealingDoesDamageEntityEnteredPlay();
			}
			if (this.m_entity.HasLifestealDoesDamageHint())
			{
				ZoneMgr.Get().OnLifestealDoesDamageEntityEnteredPlay();
			}
		}
		if (this.m_entity.HasWindfury())
		{
			this.ActivateActorSpell(SpellType.WINDFURY_BURST);
		}
		base.StartCoroutine(this.ActivateActorBattlecrySpell());
		BoardEvents boardEvents = BoardEvents.Get();
		if (boardEvents != null)
		{
			boardEvents.SummonedEvent(this);
		}
	}

	// Token: 0x06007577 RID: 30071 RVA: 0x0025B4B8 File Offset: 0x002596B8
	private bool ActivateActorSpells_HandToWeapon(Actor oldActor)
	{
		if (oldActor == null)
		{
			Debug.LogError(string.Format("{0}.ActivateActorSpells_HandToWeapon() - oldActor=null", this));
			return false;
		}
		if (this.m_actor == null)
		{
			Debug.LogError(string.Format("{0}.ActivateActorSpells_HandToWeapon() - m_actor=null", this));
			return false;
		}
		this.DeactivateHandStateSpells(oldActor);
		oldActor.SetActorState(ActorStateType.CARD_IDLE);
		SpellType spellType = SpellType.SUMMON_OUT_WEAPON;
		Spell spell = oldActor.GetSpell(spellType);
		if (spell == null)
		{
			Debug.LogError(string.Format("{0}.ActivateActorSpells_HandToWeapon() - outSpell=null outSpellType={1}", this, spellType));
			return false;
		}
		Spell spell2 = this.m_customSummonSpell;
		if (spell2 == null)
		{
			SpellType spellType2 = this.m_entity.IsControlledByFriendlySidePlayer() ? SpellType.SUMMON_IN_FRIENDLY : SpellType.SUMMON_IN_OPPONENT;
			spell2 = this.GetActorSpell(spellType2, true);
			if (spell2 == null)
			{
				Debug.LogError(string.Format("{0}.ActivateActorSpells_HandToWeapon() - inSpell=null inSpellType={1}", this, spellType2));
				return false;
			}
		}
		this.m_inputEnabled = false;
		this.ActivateSpell(spell, new Spell.FinishedCallback(this.OnSpellFinished_HandToWeapon_SummonOut), spell2, new Spell.StateFinishedCallback(this.OnSpellStateFinished_DestroyActor));
		return true;
	}

	// Token: 0x06007578 RID: 30072 RVA: 0x0025B5B0 File Offset: 0x002597B0
	private void OnSpellFinished_HandToWeapon_SummonOut(Spell spell, object userData)
	{
		this.m_actor.Show();
		Spell spell2 = this.m_customSummonSpell;
		if (spell2 == null)
		{
			spell2 = (Spell)userData;
		}
		else
		{
			spell2.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSpellStateFinished_DestroySpell));
			SpellUtils.SetCustomSpellParent(spell2, this.m_actor);
		}
		this.ActivateSpell(spell2, new Spell.FinishedCallback(this.OnSpellFinished_StandardCardSummon));
	}

	// Token: 0x06007579 RID: 30073 RVA: 0x0025B614 File Offset: 0x00259814
	private void DiscardCardBeingDrawn()
	{
		if (this == GameState.Get().GetOpponentCardBeingDrawn())
		{
			this.m_actorWaitingToBeReplaced.Destroy();
			this.m_actorWaitingToBeReplaced = null;
		}
		if (this.m_actor.IsShown())
		{
			this.ActivateDeathSpell(this.m_actor);
			return;
		}
		GameState.Get().ClearCardBeingDrawn(this);
	}

	// Token: 0x0600757A RID: 30074 RVA: 0x0025B66C File Offset: 0x0025986C
	private void DoDiscardAnimation()
	{
		ZoneHand zoneHand = this.m_prevZone as ZoneHand;
		this.m_actor.SetBlockTextComponentUpdate(true);
		this.m_doNotSort = true;
		iTween.Stop(base.gameObject);
		float num = 3f;
		if (this.GetEntity().IsControlledByOpposingSidePlayer())
		{
			num = -num;
		}
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + num);
		iTween.MoveTo(base.gameObject, position, 3f);
		Vector3 a = base.transform.localScale;
		if (zoneHand != null)
		{
			a = zoneHand.GetCardScale();
		}
		iTween.ScaleTo(base.gameObject, a * 1.5f, 3f);
		base.StartCoroutine(this.ActivateGraveyardActorDeathSpellAfterDelay(1f, 4f, null));
	}

	// Token: 0x0600757B RID: 30075 RVA: 0x0025B752 File Offset: 0x00259952
	private bool DoPlayToHandTransition(Actor oldActor, bool wasInGraveyard = false, bool useFastAnimations = false)
	{
		bool flag = this.ActivateActorSpells_PlayToHand(oldActor, wasInGraveyard, useFastAnimations);
		if (flag)
		{
			this.m_actor.Hide();
		}
		return flag;
	}

	// Token: 0x0600757C RID: 30076 RVA: 0x0025B76C File Offset: 0x0025996C
	private bool ActivateActorSpells_PlayToHand(Actor oldActor, bool wasInGraveyard, bool useFastAnimations)
	{
		if (oldActor == null)
		{
			Debug.LogError(string.Format("{0}.ActivateActorSpells_PlayToHand() - oldActor=null", this));
			return false;
		}
		if (this.m_actor == null)
		{
			Debug.LogError(string.Format("{0}.ActivateActorSpells_PlayToHand() - m_actor=null", this));
			return false;
		}
		SpellType spellType = useFastAnimations ? SpellType.BOUNCE_OUT_FAST : SpellType.BOUNCE_OUT;
		Spell outSpell = oldActor.GetSpell(spellType);
		if (outSpell == null)
		{
			Debug.LogError(string.Format("{0}.ActivateActorSpells_PlayToHand() - outSpell=null outSpellType={1}", this, spellType));
			return false;
		}
		SpellType spellType2 = SpellType.BOUNCE_IN;
		if (this.m_actor.UseTechLevelManaGem())
		{
			spellType2 = SpellType.BOUNCE_IN_TECH_LEVEL;
		}
		else if (useFastAnimations)
		{
			spellType2 = SpellType.BOUNCE_IN_FAST;
		}
		Spell inSpell = this.GetActorSpell(spellType2, true);
		if (inSpell == null)
		{
			Debug.LogError(string.Format("{0}.ActivateActorSpells_PlayToHand() - inSpell=null inSpellType={1}", this, spellType2));
			return false;
		}
		this.m_inputEnabled = false;
		outSpell.SetSource(base.gameObject);
		if (this.m_entity.IsControlledByFriendlySidePlayer())
		{
			Spell.FinishedCallback finishedCallback = wasInGraveyard ? new Spell.FinishedCallback(this.OnSpellFinished_PlayToHand_SummonOut_FromGraveyard) : new Spell.FinishedCallback(this.OnSpellFinished_PlayToHand_SummonOut);
			Spell.StateFinishedCallback callback = delegate(Spell spell, SpellStateType prevStateType, object userData)
			{
				if (prevStateType == SpellStateType.CANCEL)
				{
					this.ActivateSpell(outSpell, finishedCallback, inSpell, new Spell.StateFinishedCallback(this.OnSpellStateFinished_DestroyActor));
				}
			};
			if (!this.CancelCustomSummonSpell(callback))
			{
				this.ActivateSpell(outSpell, finishedCallback, inSpell, new Spell.StateFinishedCallback(this.OnSpellStateFinished_DestroyActor));
			}
		}
		else
		{
			if (this.m_entity.IsControlledByOpposingSidePlayer())
			{
				Log.FaceDownCard.Print("Card.ActivateActorSpells_PlayToHand() - {0} - {1} on {2}", new object[]
				{
					this,
					spellType,
					oldActor
				});
				Log.FaceDownCard.Print("Card.ActivateActorSpells_PlayToHand() - {0} - {1} on {2}", new object[]
				{
					this,
					spellType2,
					this.m_actor
				});
			}
			Spell.FinishedCallback finishedCallback2 = wasInGraveyard ? delegate(Spell spell, object userData)
			{
				this.ResumeLayoutForPlayZone();
			} : null;
			this.ActivateSpell(outSpell, finishedCallback2, null, new Spell.StateFinishedCallback(this.OnSpellStateFinished_PlayToHand_OldActor_SummonOut));
			this.ActivateSpell(inSpell, new Spell.FinishedCallback(this.OnSpellFinished_PlayToHand_SummonIn));
		}
		return true;
	}

	// Token: 0x0600757D RID: 30077 RVA: 0x0025B9A0 File Offset: 0x00259BA0
	private bool CancelCustomSummonSpell(Spell.StateFinishedCallback callback)
	{
		if (this.m_customSummonSpell == null)
		{
			return false;
		}
		if (!this.m_customSummonSpell.HasUsableState(SpellStateType.CANCEL))
		{
			return false;
		}
		if (this.m_customSummonSpell.GetActiveState() == SpellStateType.NONE)
		{
			return false;
		}
		if (this.m_customSummonSpell.GetActiveState() == SpellStateType.CANCEL)
		{
			return false;
		}
		this.m_customSummonSpell.AddStateFinishedCallback(callback);
		this.m_customSummonSpell.ActivateState(SpellStateType.CANCEL);
		return true;
	}

	// Token: 0x0600757E RID: 30078 RVA: 0x0025BA08 File Offset: 0x00259C08
	private void OnSpellFinished_PlayToHand_SummonOut(Spell spell, object userData)
	{
		Spell spell2 = (Spell)userData;
		this.ActivateSpell(spell2, new Spell.FinishedCallback(this.OnSpellFinished_StandardCardSummon));
	}

	// Token: 0x0600757F RID: 30079 RVA: 0x0025BA2F File Offset: 0x00259C2F
	private void OnSpellFinished_PlayToHand_SummonOut_FromGraveyard(Spell spell, object userData)
	{
		this.OnSpellFinished_PlayToHand_SummonOut(spell, userData);
		this.ResumeLayoutForPlayZone();
	}

	// Token: 0x06007580 RID: 30080 RVA: 0x0025BA40 File Offset: 0x00259C40
	private void ResumeLayoutForPlayZone()
	{
		global::Player.Side side = (this.m_playZoneBlockerSide != null) ? this.m_playZoneBlockerSide.Value : this.m_zone.m_Side;
		this.m_playZoneBlockerSide = null;
		ZonePlay zonePlay = ZoneMgr.Get().FindZoneOfType<ZonePlay>(side);
		zonePlay.RemoveLayoutBlocker();
		zonePlay.UpdateLayout();
	}

	// Token: 0x06007581 RID: 30081 RVA: 0x0025BA95 File Offset: 0x00259C95
	private void OnSpellStateFinished_PlayToHand_OldActor_SummonOut(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (this.m_entity.IsControlledByOpposingSidePlayer())
		{
			Log.FaceDownCard.Print("Card.OnSpellStateFinished_PlayToHand_OldActor_SummonOut() - {0} stateType={1}", new object[]
			{
				this,
				spell.GetActiveState()
			});
		}
		this.OnSpellStateFinished_DestroyActor(spell, prevStateType, userData);
	}

	// Token: 0x06007582 RID: 30082 RVA: 0x0025BAD4 File Offset: 0x00259CD4
	private void OnSpellFinished_PlayToHand_SummonIn(Spell spell, object userData)
	{
		if (this.m_entity.IsControlledByOpposingSidePlayer())
		{
			Log.FaceDownCard.Print("Card.OnSpellFinished_PlayToHand_SummonIn() - {0}", new object[]
			{
				this
			});
		}
		this.OnSpellFinished_StandardCardSummon(spell, userData);
	}

	// Token: 0x06007583 RID: 30083 RVA: 0x0025BB04 File Offset: 0x00259D04
	private IEnumerator DoHandToDeckTransition(Actor handActor)
	{
		this.m_doNotSort = true;
		this.DeactivateHandStateSpells(null);
		ZoneDeck deckZone = this.m_zone as ZoneDeck;
		ZoneHand handZone = this.m_prevZone as ZoneHand;
		deckZone.AddLayoutBlocker();
		float num = handZone.GetController().IsFriendlySide() ? 3f : -3f;
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + num);
		iTween.MoveTo(base.gameObject, position, 1.75f);
		iTween.ScaleTo(base.gameObject, base.transform.localScale * 1.5f, 1.75f);
		yield return new WaitForSeconds(1.85f);
		yield return this.AnimatePlayToDeck(base.gameObject, deckZone, !handZone.GetController().IsFriendlySide(), 1f);
		handActor.Destroy();
		this.m_actorReady = true;
		this.m_doNotSort = false;
		deckZone.RemoveLayoutBlocker();
		deckZone.UpdateLayout();
		yield break;
	}

	// Token: 0x06007584 RID: 30084 RVA: 0x0025BB1A File Offset: 0x00259D1A
	private void DoPlayToDeckTransition(Actor playActor)
	{
		this.m_doNotSort = true;
		this.m_actor.Hide();
		base.StartCoroutine(this.AnimatePlayToDeck(playActor));
	}

	// Token: 0x06007585 RID: 30085 RVA: 0x0025BB3C File Offset: 0x00259D3C
	private IEnumerator AnimatePlayToDeck(Actor playActor)
	{
		Actor handActor = null;
		bool loadingActor = true;
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			loadingActor = false;
			if (go == null)
			{
				Error.AddDevFatal("Card.AnimatePlayToGraveyardToDeck() - failed to load {0}", new object[]
				{
					assetRef
				});
				return;
			}
			handActor = go.GetComponent<Actor>();
			if (handActor == null)
			{
				Error.AddDevFatal("Card.AnimatePlayToGraveyardToDeck() - instance of {0} has no Actor component", new object[]
				{
					this.name
				});
				return;
			}
		};
		AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(this.m_entity), callback, null, AssetLoadingOptions.IgnorePrefabPosition);
		while (loadingActor)
		{
			yield return null;
		}
		if (handActor == null)
		{
			playActor.Destroy();
			yield break;
		}
		handActor.SetEntity(this.m_entity);
		handActor.SetCardDef(this.m_cardDef);
		handActor.UpdateAllComponents();
		handActor.transform.parent = playActor.GetCard().transform;
		TransformUtil.Identity(handActor);
		handActor.Hide();
		SpellType spellType = SpellType.SUMMON_OUT;
		Spell spell2 = playActor.GetSpell(spellType);
		if (spell2 == null)
		{
			Error.AddDevFatal("{0}.AnimatePlayToGraveyardToDeck() - outSpell=null outSpellType={1}", new object[]
			{
				this,
				spellType
			});
			yield break;
		}
		SpellType spellType2 = SpellType.SUMMON_IN;
		Spell inSpell = handActor.GetSpell(spellType2);
		if (inSpell == null)
		{
			Error.AddDevFatal("{0}.AnimatePlayToGraveyardToDeck() - inSpell=null inSpellType={1}", new object[]
			{
				this,
				spellType2
			});
			yield break;
		}
		bool waitForSpells = true;
		Spell.FinishedCallback callback2 = delegate(Spell spell, object userData)
		{
			waitForSpells = false;
		};
		Spell.StateFinishedCallback callback3 = delegate(Spell spell, SpellStateType prevStateType, object userData)
		{
			if (spell.GetActiveState() != SpellStateType.NONE)
			{
				return;
			}
			playActor.Destroy();
		};
		Spell.FinishedCallback callback4 = delegate(Spell spell, object userData)
		{
			inSpell.Activate();
			this.ResumeLayoutForPlayZone();
		};
		inSpell.AddFinishedCallback(callback2);
		spell2.AddFinishedCallback(callback4);
		spell2.AddStateFinishedCallback(callback3);
		this.PrepareForDeathAnimation(playActor);
		spell2.Activate();
		while (waitForSpells)
		{
			yield return 0;
		}
		ZoneDeck deckZone = (ZoneDeck)this.m_zone;
		yield return base.StartCoroutine(this.AnimatePlayToDeck(base.gameObject, deckZone, false, 1f));
		handActor.Destroy();
		this.m_actorReady = true;
		this.m_doNotSort = false;
		deckZone.RemoveLayoutBlocker();
		deckZone.UpdateLayout();
		yield break;
	}

	// Token: 0x06007586 RID: 30086 RVA: 0x0025BB52 File Offset: 0x00259D52
	public IEnumerator AnimatePlayToDeck(GameObject mover, ZoneDeck deckZone, bool hideBackSide = false, float timeScale = 1f)
	{
		SoundManager.Get().LoadAndPlay("MinionToDeck_transition.prefab:8063f1b133f28e34aaeade8fcabe250c");
		Vector3 vector = deckZone.GetThicknessForLayout().GetMeshRenderer(false).bounds.center + Card.IN_DECK_OFFSET;
		Vector3 vector2 = vector + Card.ABOVE_DECK_OFFSET;
		Vector3 vector3 = new Vector3(0f, Card.IN_DECK_ANGLES.y, 0f);
		Vector3 in_DECK_ANGLES = Card.IN_DECK_ANGLES;
		Vector3 in_DECK_SCALE = Card.IN_DECK_SCALE;
		float num = 0.3f;
		if (hideBackSide)
		{
			vector3.y = (in_DECK_ANGLES.y = -Card.IN_DECK_ANGLES.y);
			num = 0.5f;
		}
		float num2 = 1f;
		if (timeScale > 0f)
		{
			num2 *= 1f / timeScale;
		}
		Actor component = mover.GetComponent<Actor>();
		iTween.MoveTo(mover, iTween.Hash(new object[]
		{
			"position",
			vector2,
			"delay",
			0f * num2,
			"time",
			0.7f * num2,
			"easetype",
			iTween.EaseType.easeInOutCubic
		}));
		iTween.RotateTo(mover, iTween.Hash(new object[]
		{
			"rotation",
			vector3,
			"delay",
			0f * num2,
			"time",
			0.2f * num2,
			"easetype",
			iTween.EaseType.easeInOutCubic
		}));
		iTween.MoveTo(mover, iTween.Hash(new object[]
		{
			"position",
			vector,
			"delay",
			0.7f * num2,
			"time",
			0.7f * num2,
			"easetype",
			iTween.EaseType.easeOutCubic
		}));
		iTween.ScaleTo(mover, iTween.Hash(new object[]
		{
			"scale",
			in_DECK_SCALE,
			"delay",
			0.7f * num2,
			"time",
			0.6f * num2,
			"easetype",
			iTween.EaseType.easeInCubic
		}));
		if (base.gameObject != null && component != null)
		{
			iTween.RotateTo(mover, iTween.Hash(new object[]
			{
				"rotation",
				in_DECK_ANGLES,
				"delay",
				0.2f * num2,
				"time",
				num * num2,
				"easetype",
				iTween.EaseType.easeOutCubic,
				"oncomplete",
				"OnCardRotateIntoDeckComplete",
				"oncompleteparams",
				component,
				"oncompletetarget",
				base.gameObject
			}));
		}
		else
		{
			iTween.RotateTo(mover, iTween.Hash(new object[]
			{
				"rotation",
				in_DECK_ANGLES,
				"delay",
				0.2f * num2,
				"time",
				num * num2,
				"easetype",
				iTween.EaseType.easeOutCubic
			}));
		}
		while (iTween.HasTween(mover))
		{
			yield return 0;
		}
		yield break;
	}

	// Token: 0x06007587 RID: 30087 RVA: 0x0025BB80 File Offset: 0x00259D80
	private void OnCardRotateIntoDeckComplete(Actor cardActor)
	{
		if (base.gameObject != null && cardActor != null)
		{
			if (cardActor.m_eliteObject != null)
			{
				cardActor.m_eliteObject.SetActive(false);
			}
			if (cardActor.m_portraitMesh != null)
			{
				cardActor.m_portraitMesh.SetActive(false);
			}
			if (cardActor.m_manaObject != null)
			{
				cardActor.m_manaObject.SetActive(false);
			}
			if (cardActor.m_costTextMesh != null)
			{
				cardActor.m_costTextMesh.Hide();
			}
		}
	}

	// Token: 0x06007588 RID: 30088 RVA: 0x0025BC0B File Offset: 0x00259E0B
	public void SetSecretTriggered(bool set)
	{
		this.m_secretTriggered = set;
	}

	// Token: 0x06007589 RID: 30089 RVA: 0x0025BC14 File Offset: 0x00259E14
	public bool WasSecretTriggered()
	{
		return this.m_secretTriggered;
	}

	// Token: 0x0600758A RID: 30090 RVA: 0x0025BC1C File Offset: 0x00259E1C
	public bool CanShowSecretTrigger()
	{
		return !UniversalInputManager.UsePhoneUI || this.m_zone.IsOnlyCard(this);
	}

	// Token: 0x0600758B RID: 30091 RVA: 0x0025BC3D File Offset: 0x00259E3D
	public void ShowSecretTrigger()
	{
		this.m_actor.GetComponent<Spell>().ActivateState(SpellStateType.ACTION);
	}

	// Token: 0x0600758C RID: 30092 RVA: 0x0025BC50 File Offset: 0x00259E50
	private bool CanShowSecretZoneCard()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return true;
		}
		ZoneSecret zoneSecret = this.m_zone as ZoneSecret;
		return !(zoneSecret == null) && ((this.m_entity != null && this.m_entity.IsQuest()) || (this.m_entity != null && this.m_entity.IsPuzzle()) || (this.m_entity != null && this.m_entity.IsRulebook()) || (this.m_entity != null && this.m_entity.IsSigil()) || zoneSecret.GetSecretCards().IndexOf(this) == 0 || zoneSecret.GetSideQuestCards().IndexOf(this) == 0);
	}

	// Token: 0x0600758D RID: 30093 RVA: 0x0025BD00 File Offset: 0x00259F00
	private void ShowSecretQuestBirth()
	{
		Spell component = this.m_actor.GetComponent<Spell>();
		if (!this.CanShowSecretZoneCard())
		{
			Spell.StateFinishedCallback callback = delegate(Spell thisSpell, SpellStateType prevStateType, object userData)
			{
				if (thisSpell.GetActiveState() != SpellStateType.NONE)
				{
					return;
				}
				if (!this.CanShowSecretZoneCard())
				{
					this.HideCard();
				}
			};
			component.AddStateFinishedCallback(callback);
		}
		component.ActivateState(SpellStateType.BIRTH);
	}

	// Token: 0x0600758E RID: 30094 RVA: 0x0025BD3C File Offset: 0x00259F3C
	public bool CanShowSecretDeath()
	{
		return !UniversalInputManager.UsePhoneUI || this.m_prevZone.GetCardCount() == 0;
	}

	// Token: 0x0600758F RID: 30095 RVA: 0x0025BD5C File Offset: 0x00259F5C
	public void ShowSecretDeath(Actor oldActor)
	{
		Spell component = oldActor.GetComponent<Spell>();
		if (this.m_secretTriggered)
		{
			this.m_secretTriggered = false;
			if (component.GetActiveState() == SpellStateType.NONE)
			{
				oldActor.Destroy();
				return;
			}
			component.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSpellStateFinished_DestroyActor));
			return;
		}
		else
		{
			component.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSpellStateFinished_DestroyActor));
			component.ActivateState(SpellStateType.ACTION);
			oldActor.transform.parent = null;
			this.m_doNotSort = true;
			if (UniversalInputManager.UsePhoneUI)
			{
				return;
			}
			iTween.Stop(base.gameObject);
			this.m_actor.Hide();
			base.StartCoroutine(this.WaitAndThenShowDestroyedSecret());
			return;
		}
	}

	// Token: 0x06007590 RID: 30096 RVA: 0x0025BDFD File Offset: 0x00259FFD
	private IEnumerator WaitAndThenShowDestroyedSecret()
	{
		yield return new WaitForSeconds(0.5f);
		float num = 2f;
		if (this.GetEntity().IsControlledByOpposingSidePlayer())
		{
			num = -num;
		}
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z + num);
		this.m_actor.Show();
		iTween.MoveTo(base.gameObject, position, 3f);
		base.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
		base.transform.localEulerAngles = new Vector3(0f, 0f, 357f);
		iTween.ScaleTo(base.gameObject, new Vector3(1.25f, 0.2f, 1.25f), 3f);
		base.StartCoroutine(this.ActivateGraveyardActorDeathSpellAfterDelay(1f, 4f, null));
		yield break;
	}

	// Token: 0x06007591 RID: 30097 RVA: 0x0025BE0C File Offset: 0x0025A00C
	private IEnumerator SwitchSecretSides()
	{
		this.m_doNotSort = true;
		Actor newActor = null;
		bool loadingActor = true;
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			loadingActor = false;
			if (go == null)
			{
				Error.AddDevFatal("Card.SwitchSecretSides() - failed to load {0}", new object[]
				{
					assetRef
				});
				return;
			}
			newActor = go.GetComponent<Actor>();
			if (newActor == null)
			{
				Error.AddDevFatal("Card.SwitchSecretSides() - instance of {0} has no Actor component", new object[]
				{
					this.name
				});
				return;
			}
		};
		AssetLoader.Get().InstantiatePrefab(this.m_actorPath, callback, null, AssetLoadingOptions.IgnorePrefabPosition);
		while (loadingActor)
		{
			yield return null;
		}
		if (newActor)
		{
			Actor oldActor = this.m_actor;
			this.m_actor = newActor;
			this.m_actor.SetEntity(this.m_entity);
			this.m_actor.SetCard(this);
			this.m_actor.SetCardDef(this.m_cardDef);
			this.m_actor.UpdateAllComponents();
			this.m_actor.transform.parent = oldActor.transform.parent;
			TransformUtil.Identity(this.m_actor);
			this.m_actor.Hide();
			if (!this.CanShowSecretDeath())
			{
				oldActor.Destroy();
			}
			else
			{
				Card.<>c__DisplayClass427_2 CS$<>8__locals3 = new Card.<>c__DisplayClass427_2();
				oldActor.transform.parent = base.transform.parent;
				this.m_transitionStyle = ZoneTransitionStyle.INSTANT;
				CS$<>8__locals3.oldActorFinished = false;
				Spell.FinishedCallback callback2 = delegate(Spell spell, object userData)
				{
					CS$<>8__locals3.oldActorFinished = true;
				};
				Spell.StateFinishedCallback callback3 = delegate(Spell spell, SpellStateType prevStateType, object userData)
				{
					if (spell.GetActiveState() == SpellStateType.NONE)
					{
						oldActor.Destroy();
					}
				};
				Spell component = oldActor.GetComponent<Spell>();
				component.AddFinishedCallback(callback2);
				component.AddStateFinishedCallback(callback3);
				component.ActivateState(SpellStateType.ACTION);
				while (!CS$<>8__locals3.oldActorFinished)
				{
					yield return null;
				}
				CS$<>8__locals3 = null;
			}
			this.m_shown = true;
			this.m_actor.Show();
			this.ShowSecretQuestBirth();
		}
		this.m_actorReady = true;
		this.m_doNotSort = false;
		this.m_zone.UpdateLayout();
		this.ActivateStateSpells(false);
		yield break;
	}

	// Token: 0x06007592 RID: 30098 RVA: 0x0025BE1C File Offset: 0x0025A01C
	private bool ShouldCardDrawWaitForTurnStartSpells()
	{
		SpellController spellController = TurnStartManager.Get().GetSpellController();
		return !(spellController == null) && (spellController.IsSource(this) || spellController.IsTarget(this));
	}

	// Token: 0x06007593 RID: 30099 RVA: 0x0025BE56 File Offset: 0x0025A056
	private IEnumerator WaitForCardDrawBlockingTurnStartSpells()
	{
		while (this.ShouldCardDrawWaitForTurnStartSpells())
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06007594 RID: 30100 RVA: 0x0025BE68 File Offset: 0x0025A068
	private PowerTask GetPowerTaskToBlockCardDraw()
	{
		if (this.m_latestZoneChange == null)
		{
			return null;
		}
		PowerTaskList taskList = this.m_latestZoneChange.GetParentList().GetTaskList();
		if (taskList == null)
		{
			return null;
		}
		if (taskList.IsEndOfBlock() && taskList.IsComplete())
		{
			return null;
		}
		PowerTask result = null;
		PowerTaskList currentTaskList = GameState.Get().GetPowerProcessor().GetCurrentTaskList();
		if (currentTaskList != null && currentTaskList.IsDescendantOfBlock(taskList))
		{
			this.DoesTaskListBlockCardDraw(currentTaskList, out result);
		}
		foreach (PowerTaskList powerTaskList in GameState.Get().GetPowerProcessor().GetPowerQueue())
		{
			PowerTask powerTask;
			if (powerTaskList.IsDescendantOfBlock(taskList) && this.DoesTaskListBlockCardDraw(powerTaskList, out powerTask))
			{
				if (!this.CanPowerTaskListBlockCardDraw(powerTaskList))
				{
					break;
				}
				result = powerTask;
			}
		}
		return result;
	}

	// Token: 0x06007595 RID: 30101 RVA: 0x0025BF38 File Offset: 0x0025A138
	private bool CanPowerTaskListBlockCardDraw(PowerTaskList blockingPowerTaskList)
	{
		PowerTaskList currentTaskList = GameState.Get().GetPowerProcessor().GetCurrentTaskList();
		if (currentTaskList != null && (currentTaskList.HasCardDraw() || currentTaskList.HasCardMill() || currentTaskList.HasFatigue()))
		{
			return false;
		}
		foreach (PowerTaskList powerTaskList in GameState.Get().GetPowerProcessor().GetPowerQueue())
		{
			if (powerTaskList == blockingPowerTaskList)
			{
				break;
			}
			if (powerTaskList.HasCardDraw() || powerTaskList.HasCardMill() || powerTaskList.HasFatigue())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06007596 RID: 30102 RVA: 0x0025BFDC File Offset: 0x0025A1DC
	private bool DoesTaskListBlockCardDraw(PowerTaskList taskList, out PowerTask blockingTask)
	{
		blockingTask = this.GetPowerTaskBlockingCardDraw(taskList);
		if (blockingTask == null)
		{
			return false;
		}
		foreach (PowerTask powerTask in taskList.GetTaskList())
		{
			if (powerTask == blockingTask)
			{
				break;
			}
			if (powerTask.IsCardDraw() || powerTask.IsCardMill() || powerTask.IsFatigue())
			{
				blockingTask = null;
				return false;
			}
		}
		return true;
	}

	// Token: 0x06007597 RID: 30103 RVA: 0x0025C060 File Offset: 0x0025A260
	private PowerTask GetPowerTaskBlockingCardDraw(PowerTaskList taskList)
	{
		if (taskList == null)
		{
			return null;
		}
		if (taskList.IsComplete())
		{
			return null;
		}
		Network.HistBlockStart blockStart = taskList.GetBlockStart();
		if (blockStart == null)
		{
			return null;
		}
		if (blockStart.BlockType != HistoryBlock.Type.POWER && blockStart.BlockType != HistoryBlock.Type.TRIGGER)
		{
			return null;
		}
		int entityId = this.m_entity.GetEntityId();
		List<PowerTask> taskList2 = taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			PowerTask powerTask = taskList2[i];
			if (!powerTask.IsCompleted())
			{
				Network.PowerHistory power = powerTask.GetPower();
				int num = 0;
				switch (power.Type)
				{
				case Network.PowerType.SHOW_ENTITY:
				{
					Network.HistShowEntity histShowEntity = (Network.HistShowEntity)power;
					if (histShowEntity.Entity.ID == entityId)
					{
						Network.Entity.Tag tag = histShowEntity.Entity.Tags.Find((Network.Entity.Tag currTag) => currTag.Name == 49);
						if (tag != null)
						{
							num = tag.Value;
						}
					}
					break;
				}
				case Network.PowerType.HIDE_ENTITY:
				{
					Network.HistHideEntity histHideEntity = (Network.HistHideEntity)power;
					if (histHideEntity.Entity == entityId)
					{
						num = histHideEntity.Zone;
					}
					break;
				}
				case Network.PowerType.TAG_CHANGE:
				{
					Network.HistTagChange histTagChange = (Network.HistTagChange)power;
					if (histTagChange.Entity == entityId && histTagChange.Tag == 49)
					{
						num = histTagChange.Value;
					}
					break;
				}
				case Network.PowerType.META_DATA:
				{
					Network.HistMetaData histMetaData = (Network.HistMetaData)power;
					if (histMetaData.MetaType == HistoryMeta.Type.HOLD_DRAWN_CARD && histMetaData.Info.Count == 1 && histMetaData.Info[0] == entityId)
					{
						return powerTask;
					}
					break;
				}
				case Network.PowerType.CHANGE_ENTITY:
					if (((Network.HistChangeEntity)power).Entity.ID == entityId)
					{
						return powerTask;
					}
					break;
				}
				if (num != 0 && num != 3)
				{
					return powerTask;
				}
			}
		}
		return null;
	}

	// Token: 0x06007598 RID: 30104 RVA: 0x0025C224 File Offset: 0x0025A424
	private void CutoffFriendlyCardDraw()
	{
		if (this.m_actorReady)
		{
			return;
		}
		if (this.m_actorWaitingToBeReplaced != null)
		{
			this.m_actorWaitingToBeReplaced.Destroy();
			this.m_actorWaitingToBeReplaced = null;
		}
		this.m_actor.Show();
		this.m_actor.TurnOffCollider();
		this.m_doNotSort = false;
		this.m_actorReady = true;
		this.ActivateStateSpells(false);
		this.RefreshActor();
		GameState.Get().ClearCardBeingDrawn(this);
		this.m_zone.UpdateLayout();
	}

	// Token: 0x06007599 RID: 30105 RVA: 0x0025C2A2 File Offset: 0x0025A4A2
	private IEnumerator WaitAndPrepareForDeathAnimation(Actor dyingActor)
	{
		yield return new WaitForSeconds(this.m_keywordDeathDelaySec);
		this.PrepareForDeathAnimation(dyingActor);
		yield break;
	}

	// Token: 0x0600759A RID: 30106 RVA: 0x0025C2B8 File Offset: 0x0025A4B8
	private void PrepareForDeathAnimation(Actor dyingActor)
	{
		dyingActor.ToggleCollider(false);
		dyingActor.ToggleForceIdle(true);
		dyingActor.SetActorState(ActorStateType.CARD_IDLE);
		dyingActor.DoCardDeathVisuals();
		this.DeactivateCustomKeywordEffect();
	}

	// Token: 0x0600759B RID: 30107 RVA: 0x0025C2DB File Offset: 0x0025A4DB
	private IEnumerator ActivateGraveyardActorDeathSpellAfterDelay(float predelay, float postdelay, Card.ActivateGraveyardActorDeathSpellAfterDelayCallback finishedCallback = null)
	{
		this.m_actor.DoCardDeathVisuals();
		Spell chosenSpell = this.GetBestDeathSpell();
		if (chosenSpell.DoesBlockServerEvents())
		{
			GameState.Get().AddServerBlockingSpell(chosenSpell);
		}
		yield return new WaitForSeconds(predelay);
		this.ActivateSpell(chosenSpell, null);
		this.CleanUpCustomSpell(chosenSpell, ref this.m_customDiscardSpell);
		this.CleanUpCustomSpell(chosenSpell, ref this.m_customDiscardSpellOverride);
		yield return new WaitForSeconds(postdelay);
		this.m_doNotSort = false;
		this.m_actor.SetBlockTextComponentUpdate(false);
		if (finishedCallback != null)
		{
			finishedCallback();
		}
		yield break;
	}

	// Token: 0x0600759C RID: 30108 RVA: 0x0025C300 File Offset: 0x0025A500
	private bool HandlePlayActorDeath(Actor oldActor)
	{
		bool result = false;
		if (!this.m_cardDef.CardDef.m_SuppressDeathrattleDeath && this.m_entity.HasDeathrattle() && !this.m_entity.IsDeathrattleDisabled())
		{
			this.ActivateActorSpell(oldActor, SpellType.DEATHRATTLE_DEATH);
		}
		if (!this.m_cardDef.CardDef.m_SuppressDeathrattleDeath && this.m_entity.HasTag(GAME_TAG.REBORN))
		{
			this.ActivateActorSpell(oldActor, SpellType.REBORN_DEATH);
		}
		if (this.m_suppressDeathEffects)
		{
			if (oldActor)
			{
				oldActor.Destroy();
			}
			if (this.IsShown())
			{
				this.ShowImpl();
			}
			else
			{
				this.HideImpl();
			}
			result = true;
			this.m_actorReady = true;
		}
		else
		{
			if (!this.m_suppressKeywordDeaths)
			{
				base.StartCoroutine(this.WaitAndPrepareForDeathAnimation(oldActor));
			}
			if (this.ActivateDeathSpell(oldActor) != null)
			{
				this.m_actor.Hide();
				result = true;
				this.m_actorReady = true;
			}
		}
		return result;
	}

	// Token: 0x0600759D RID: 30109 RVA: 0x0025C3E8 File Offset: 0x0025A5E8
	private bool DoesCardReturnFromGraveyard()
	{
		foreach (PowerTaskList taskList in GameState.Get().GetPowerProcessor().GetPowerQueue())
		{
			if (this.DoesTaskListReturnCardFromGraveyard(taskList))
			{
				Log.Gameplay.PrintInfo("Found the task for returning entity {0} from graveyard!", new object[]
				{
					this.m_entity
				});
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600759E RID: 30110 RVA: 0x0025C468 File Offset: 0x0025A668
	private bool DoesTaskListReturnCardFromGraveyard(PowerTaskList taskList)
	{
		if (!taskList.IsTriggerBlock())
		{
			return false;
		}
		foreach (PowerTask powerTask in taskList.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = power as Network.HistTagChange;
				if (histTagChange.Tag == 49 && histTagChange.Entity == this.m_entity.GetEntityId())
				{
					if (histTagChange.Value == 6)
					{
						return false;
					}
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600759F RID: 30111 RVA: 0x0025C504 File Offset: 0x0025A704
	private int GetCardFutureController()
	{
		foreach (PowerTaskList taskList in GameState.Get().GetPowerProcessor().GetPowerQueue())
		{
			int cardFutureControllerFromTaskList = this.GetCardFutureControllerFromTaskList(taskList);
			if (cardFutureControllerFromTaskList != this.m_entity.GetControllerId())
			{
				return cardFutureControllerFromTaskList;
			}
		}
		return this.m_entity.GetControllerId();
	}

	// Token: 0x060075A0 RID: 30112 RVA: 0x0025C57C File Offset: 0x0025A77C
	private int GetCardFutureControllerFromTaskList(PowerTaskList taskList)
	{
		foreach (PowerTask powerTask in taskList.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = power as Network.HistTagChange;
				if (histTagChange.Tag == 50 && histTagChange.Entity == this.m_entity.GetEntityId())
				{
					return histTagChange.Value;
				}
			}
		}
		return this.m_entity.GetControllerId();
	}

	// Token: 0x060075A1 RID: 30113 RVA: 0x0025C610 File Offset: 0x0025A810
	public void SetDelayBeforeHideInNullZoneVisuals(float delay)
	{
		this.m_delayBeforeHideInNullZoneVisuals = delay;
	}

	// Token: 0x060075A2 RID: 30114 RVA: 0x0025C619 File Offset: 0x0025A819
	private void DoNullZoneVisuals()
	{
		base.StartCoroutine(this.DoNullZoneVisualsWithTiming());
	}

	// Token: 0x060075A3 RID: 30115 RVA: 0x0025C628 File Offset: 0x0025A828
	private IEnumerator DoNullZoneVisualsWithTiming()
	{
		if (this.m_delayBeforeHideInNullZoneVisuals > 0f)
		{
			yield return new WaitForSeconds(this.m_delayBeforeHideInNullZoneVisuals);
		}
		Spell nullZoneSpell = this.GetBestNullZoneSpell();
		if (nullZoneSpell != null)
		{
			nullZoneSpell.Activate();
			while (nullZoneSpell.GetActiveState() != SpellStateType.NONE)
			{
				yield return null;
			}
		}
		if (this.m_actor != null)
		{
			this.m_actor.DeactivateAllSpells();
		}
		this.HideCard();
		yield break;
	}

	// Token: 0x060075A4 RID: 30116 RVA: 0x0025C638 File Offset: 0x0025A838
	private Spell GetBestNullZoneSpell()
	{
		if (this.m_entity.HasTag(GAME_TAG.GHOSTLY) && this.GetControllerSide() == global::Player.Side.FRIENDLY && this.m_prevZone is ZoneHand && this.m_actor != null)
		{
			return this.m_actor.GetSpell(SpellType.GHOSTLY_DEATH);
		}
		if (this.m_entity.IsSpell() && this.m_prevZone is ZoneHand && this.m_actor != null && this.m_zone is ZoneGraveyard)
		{
			return this.m_actor.GetSpell(SpellType.POWER_UP);
		}
		return null;
	}

	// Token: 0x060075A5 RID: 30117 RVA: 0x0025C6CD File Offset: 0x0025A8CD
	public void SetDrawTimeScale(float scale)
	{
		this.m_drawTimeScale = new float?(scale);
	}

	// Token: 0x170006E3 RID: 1763
	// (get) Token: 0x060075A6 RID: 30118 RVA: 0x0025C6DB File Offset: 0x0025A8DB
	public bool HasCardDef
	{
		get
		{
			DefLoader.DisposableCardDef cardDef = this.m_cardDef;
			return ((cardDef != null) ? cardDef.CardDef : null) != null;
		}
	}

	// Token: 0x060075A7 RID: 30119 RVA: 0x0025C6F5 File Offset: 0x0025A8F5
	public bool HasSameCardDef(CardDef cardDef)
	{
		DefLoader.DisposableCardDef cardDef2 = this.m_cardDef;
		return ((cardDef2 != null) ? cardDef2.CardDef : null) == cardDef;
	}

	// Token: 0x170006E4 RID: 1764
	// (get) Token: 0x060075A8 RID: 30120 RVA: 0x0025C70F File Offset: 0x0025A90F
	public bool HasHiddenCardDef
	{
		get
		{
			DefLoader.DisposableCardDef cardDef = this.m_cardDef;
			return ((cardDef != null) ? cardDef.CardDef : null) is HiddenCard;
		}
	}

	// Token: 0x060075A9 RID: 30121 RVA: 0x0025C72C File Offset: 0x0025A92C
	public T GetCardDefComponent<T>()
	{
		if (!this.HasCardDef)
		{
			return default(T);
		}
		return this.m_cardDef.CardDef.GetComponent<T>();
	}

	// Token: 0x170006E5 RID: 1765
	// (get) Token: 0x060075AA RID: 30122 RVA: 0x0025C75B File Offset: 0x0025A95B
	public string CustomHeroPhoneManaGem
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDef.CardDef.m_CustomHeroPhoneManaGem;
		}
	}

	// Token: 0x170006E6 RID: 1766
	// (get) Token: 0x060075AB RID: 30123 RVA: 0x0025C777 File Offset: 0x0025A977
	public string CustomHeroTray
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDef.CardDef.m_CustomHeroTray;
		}
	}

	// Token: 0x170006E7 RID: 1767
	// (get) Token: 0x060075AC RID: 30124 RVA: 0x0025C793 File Offset: 0x0025A993
	public string CustomHeroTrayGolden
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDef.CardDef.m_CustomHeroTrayGolden;
		}
	}

	// Token: 0x170006E8 RID: 1768
	// (get) Token: 0x060075AD RID: 30125 RVA: 0x0025C7AF File Offset: 0x0025A9AF
	public string CustomHeroPhoneTray
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDef.CardDef.m_CustomHeroPhoneTray;
		}
	}

	// Token: 0x170006E9 RID: 1769
	// (get) Token: 0x060075AE RID: 30126 RVA: 0x0025C7CB File Offset: 0x0025A9CB
	public ref string DiamondCustomSpawnSpellPath
	{
		get
		{
			return ref this.m_cardDef.CardDef.m_DiamondCustomSpawnSpellPath;
		}
	}

	// Token: 0x170006EA RID: 1770
	// (get) Token: 0x060075AF RID: 30127 RVA: 0x0025C7DD File Offset: 0x0025A9DD
	public ref string GoldenCustomSpawnSpellPath
	{
		get
		{
			return ref this.m_cardDef.CardDef.m_GoldenCustomSpawnSpellPath;
		}
	}

	// Token: 0x170006EB RID: 1771
	// (get) Token: 0x060075B0 RID: 30128 RVA: 0x0025C7EF File Offset: 0x0025A9EF
	public ref string CustomSpawnSpellPath
	{
		get
		{
			return ref this.m_cardDef.CardDef.m_CustomSpawnSpellPath;
		}
	}

	// Token: 0x170006EC RID: 1772
	// (get) Token: 0x060075B1 RID: 30129 RVA: 0x0025C801 File Offset: 0x0025AA01
	public ref string DiamondCustomSummonSpellPath
	{
		get
		{
			return ref this.m_cardDef.CardDef.m_DiamondCustomSummonSpellPath;
		}
	}

	// Token: 0x170006ED RID: 1773
	// (get) Token: 0x060075B2 RID: 30130 RVA: 0x0025C813 File Offset: 0x0025AA13
	public ref string GoldenCustomSummonSpellPath
	{
		get
		{
			return ref this.m_cardDef.CardDef.m_GoldenCustomSummonSpellPath;
		}
	}

	// Token: 0x170006EE RID: 1774
	// (get) Token: 0x060075B3 RID: 30131 RVA: 0x0025C825 File Offset: 0x0025AA25
	public ref string CustomSummonSpellPath
	{
		get
		{
			return ref this.m_cardDef.CardDef.m_CustomSummonSpellPath;
		}
	}

	// Token: 0x170006EF RID: 1775
	// (get) Token: 0x060075B4 RID: 30132 RVA: 0x0025C837 File Offset: 0x0025AA37
	public List<Board.CustomTraySettings> CustomHeroTraySettings
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDef.CardDef.m_CustomHeroTraySettings;
		}
	}

	// Token: 0x04005C58 RID: 23640
	public static readonly Vector3 ABOVE_DECK_OFFSET = new Vector3(0f, 3.6f, 0f);

	// Token: 0x04005C59 RID: 23641
	public static readonly Vector3 IN_DECK_OFFSET = new Vector3(0f, 0f, 0.1f);

	// Token: 0x04005C5A RID: 23642
	public static readonly Vector3 IN_DECK_SCALE = new Vector3(0.81f, 0.81f, 0.81f);

	// Token: 0x04005C5B RID: 23643
	public static readonly Vector3 IN_DECK_ANGLES = new Vector3(-90f, 270f, 0f);

	// Token: 0x04005C5C RID: 23644
	public static readonly Quaternion IN_DECK_ROTATION = Quaternion.Euler(Card.IN_DECK_ANGLES);

	// Token: 0x04005C5D RID: 23645
	public static readonly Vector3 IN_DECK_HIDDEN_ANGLES = new Vector3(270f, 90f, 0f);

	// Token: 0x04005C5E RID: 23646
	public static readonly Quaternion IN_DECK_HIDDEN_ROTATION = Quaternion.Euler(Card.IN_DECK_HIDDEN_ANGLES);

	// Token: 0x04005C5F RID: 23647
	public const float DEFAULT_KEYWORD_DEATH_DELAY_SEC = 0.6f;

	// Token: 0x04005C60 RID: 23648
	protected global::Entity m_entity;

	// Token: 0x04005C61 RID: 23649
	protected DefLoader.DisposableCardDef m_cardDef;

	// Token: 0x04005C62 RID: 23650
	protected CardEffect m_playEffect;

	// Token: 0x04005C63 RID: 23651
	protected List<CardEffect> m_additionalPlayEffects;

	// Token: 0x04005C64 RID: 23652
	protected CardEffect m_attackEffect;

	// Token: 0x04005C65 RID: 23653
	protected CardEffect m_deathEffect;

	// Token: 0x04005C66 RID: 23654
	protected CardEffect m_lifetimeEffect;

	// Token: 0x04005C67 RID: 23655
	protected List<CardEffect> m_subOptionEffects;

	// Token: 0x04005C68 RID: 23656
	protected List<List<CardEffect>> m_additionalSubOptionEffects;

	// Token: 0x04005C69 RID: 23657
	protected List<CardEffect> m_triggerEffects;

	// Token: 0x04005C6A RID: 23658
	protected List<CardEffect> m_resetGameEffects;

	// Token: 0x04005C6B RID: 23659
	protected Map<Network.HistBlockStart, CardEffect> m_proxyEffects;

	// Token: 0x04005C6C RID: 23660
	protected List<CardEffect> m_allEffects;

	// Token: 0x04005C6D RID: 23661
	protected CardEffect m_customKeywordEffect;

	// Token: 0x04005C6E RID: 23662
	protected CardEffect m_customChoiceRevealEffect;

	// Token: 0x04005C6F RID: 23663
	protected CardEffect m_customChoiceConcealEffect;

	// Token: 0x04005C70 RID: 23664
	protected Map<SpellType, CardEffect> m_spellTableOverrideEffects = new Map<SpellType, CardEffect>();

	// Token: 0x04005C71 RID: 23665
	protected CardSound[] m_announcerLine = new CardSound[3];

	// Token: 0x04005C72 RID: 23666
	protected List<EmoteEntry> m_emotes;

	// Token: 0x04005C73 RID: 23667
	protected Spell m_customSummonSpell;

	// Token: 0x04005C74 RID: 23668
	protected Spell m_customSpawnSpell;

	// Token: 0x04005C75 RID: 23669
	protected Spell m_customSpawnSpellOverride;

	// Token: 0x04005C76 RID: 23670
	protected Spell m_customDeathSpell;

	// Token: 0x04005C77 RID: 23671
	protected Spell m_customDeathSpellOverride;

	// Token: 0x04005C78 RID: 23672
	protected Spell m_customDiscardSpell;

	// Token: 0x04005C79 RID: 23673
	protected Spell m_customDiscardSpellOverride;

	// Token: 0x04005C7A RID: 23674
	private int m_spellLoadCount;

	// Token: 0x04005C7B RID: 23675
	protected string m_actorPath;

	// Token: 0x04005C7C RID: 23676
	protected Actor m_actor;

	// Token: 0x04005C7D RID: 23677
	protected Actor m_actorWaitingToBeReplaced;

	// Token: 0x04005C7E RID: 23678
	private bool m_actorReady = true;

	// Token: 0x04005C7F RID: 23679
	private bool m_actorLoading;

	// Token: 0x04005C80 RID: 23680
	private bool m_transitioningZones;

	// Token: 0x04005C81 RID: 23681
	private bool m_hasBeenGrabbedByEnemyActionHandler;

	// Token: 0x04005C82 RID: 23682
	private Zone m_zone;

	// Token: 0x04005C83 RID: 23683
	private Zone m_prevZone;

	// Token: 0x04005C84 RID: 23684
	private bool m_goingThroughDeathrattleReturnfromGraveyard;

	// Token: 0x04005C85 RID: 23685
	private int m_zonePosition;

	// Token: 0x04005C86 RID: 23686
	private int m_predictedZonePosition;

	// Token: 0x04005C87 RID: 23687
	public ZonePositionChange m_minionWasMovedFromSrcToDst;

	// Token: 0x04005C88 RID: 23688
	private bool m_doNotSort;

	// Token: 0x04005C89 RID: 23689
	private bool m_beingDrawnByOpponent;

	// Token: 0x04005C8A RID: 23690
	private bool m_cardStandInInteractive = true;

	// Token: 0x04005C8B RID: 23691
	private ZoneTransitionStyle m_transitionStyle;

	// Token: 0x04005C8C RID: 23692
	private bool m_doNotWarpToNewZone;

	// Token: 0x04005C8D RID: 23693
	private float m_transitionDelay;

	// Token: 0x04005C8E RID: 23694
	protected bool m_shouldShowTooltip;

	// Token: 0x04005C8F RID: 23695
	protected bool m_showTooltip;

	// Token: 0x04005C90 RID: 23696
	protected bool m_overPlayfield;

	// Token: 0x04005C91 RID: 23697
	protected bool m_mousedOver;

	// Token: 0x04005C92 RID: 23698
	protected bool m_mousedOverByOpponent;

	// Token: 0x04005C93 RID: 23699
	protected bool m_shown = true;

	// Token: 0x04005C94 RID: 23700
	private bool m_inputEnabled = true;

	// Token: 0x04005C95 RID: 23701
	protected bool m_attacking;

	// Token: 0x04005C96 RID: 23702
	protected bool m_moving;

	// Token: 0x04005C97 RID: 23703
	private int m_activeDeathEffectCount;

	// Token: 0x04005C98 RID: 23704
	private bool m_ignoreDeath;

	// Token: 0x04005C99 RID: 23705
	private bool m_suppressDeathEffects;

	// Token: 0x04005C9A RID: 23706
	private bool m_suppressDeathSounds;

	// Token: 0x04005C9B RID: 23707
	private bool m_suppressKeywordDeaths;

	// Token: 0x04005C9C RID: 23708
	private bool m_suppressHandToDeckTransition;

	// Token: 0x04005C9D RID: 23709
	private float m_keywordDeathDelaySec = 0.6f;

	// Token: 0x04005C9E RID: 23710
	private bool m_suppressActorTriggerSpell;

	// Token: 0x04005C9F RID: 23711
	private int m_suppressPlaySoundCount;

	// Token: 0x04005CA0 RID: 23712
	private bool m_isBattleCrySource;

	// Token: 0x04005CA1 RID: 23713
	private bool m_secretTriggered;

	// Token: 0x04005CA2 RID: 23714
	private bool m_secretSheathed;

	// Token: 0x04005CA3 RID: 23715
	private bool m_isBaubleAnimating;

	// Token: 0x04005CA4 RID: 23716
	private Spell m_activeSpawnSpell;

	// Token: 0x04005CA5 RID: 23717
	private global::Player.Side? m_playZoneBlockerSide;

	// Token: 0x04005CA6 RID: 23718
	private float m_delayBeforeHideInNullZoneVisuals;

	// Token: 0x04005CA7 RID: 23719
	private HeroPowerTooltip m_heroPowerTooltip;

	// Token: 0x04005CA8 RID: 23720
	private MagneticPlayData m_magneticPlayData;

	// Token: 0x04005CA9 RID: 23721
	private ZoneChange m_latestZoneChange;

	// Token: 0x04005CAA RID: 23722
	private bool m_skipMilling;

	// Token: 0x04005CAB RID: 23723
	private int m_cardDrawTracker;

	// Token: 0x04005CAC RID: 23724
	private float? m_drawTimeScale;

	// Token: 0x04005CAD RID: 23725
	private const int DRAW_FAST_THRESHOLD_START = 3;

	// Token: 0x04005CAE RID: 23726
	private const int DRAW_FAST_THRESHOLD_MAX = 6;

	// Token: 0x04005CAF RID: 23727
	private const float NORMAL_DRAW_TIME_SCALE = 1f;

	// Token: 0x04005CB0 RID: 23728
	private const float FAST_DRAW_TIME_SCALE = 0.556f;

	// Token: 0x04005CB1 RID: 23729
	private bool m_disableHeroPowerFlipSoundOnce;

	// Token: 0x02002469 RID: 9321
	private class SpellLoadRequest
	{
		// Token: 0x0400EA2B RID: 59947
		public string m_path;

		// Token: 0x0400EA2C RID: 59948
		public PrefabCallback<GameObject> m_loadCallback;
	}

	// Token: 0x0200246A RID: 9322
	public enum AnnouncerLineType
	{
		// Token: 0x0400EA2E RID: 59950
		DEFAULT,
		// Token: 0x0400EA2F RID: 59951
		BEFORE_VERSUS,
		// Token: 0x0400EA30 RID: 59952
		AFTER_VERSUS,
		// Token: 0x0400EA31 RID: 59953
		MAX
	}

	// Token: 0x0200246B RID: 9323
	// (Invoke) Token: 0x06012F3B RID: 77627
	private delegate void ActivateGraveyardActorDeathSpellAfterDelayCallback();
}
