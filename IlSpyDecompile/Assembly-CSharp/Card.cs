using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using PegasusGame;
using UnityEngine;

public class Card : MonoBehaviour
{
	private class SpellLoadRequest
	{
		public string m_path;

		public PrefabCallback<GameObject> m_loadCallback;
	}

	public enum AnnouncerLineType
	{
		DEFAULT,
		BEFORE_VERSUS,
		AFTER_VERSUS,
		MAX
	}

	private delegate void ActivateGraveyardActorDeathSpellAfterDelayCallback();

	public static readonly Vector3 ABOVE_DECK_OFFSET = new Vector3(0f, 3.6f, 0f);

	public static readonly Vector3 IN_DECK_OFFSET = new Vector3(0f, 0f, 0.1f);

	public static readonly Vector3 IN_DECK_SCALE = new Vector3(0.81f, 0.81f, 0.81f);

	public static readonly Vector3 IN_DECK_ANGLES = new Vector3(-90f, 270f, 0f);

	public static readonly Quaternion IN_DECK_ROTATION = Quaternion.Euler(IN_DECK_ANGLES);

	public static readonly Vector3 IN_DECK_HIDDEN_ANGLES = new Vector3(270f, 90f, 0f);

	public static readonly Quaternion IN_DECK_HIDDEN_ROTATION = Quaternion.Euler(IN_DECK_HIDDEN_ANGLES);

	public const float DEFAULT_KEYWORD_DEATH_DELAY_SEC = 0.6f;

	protected Entity m_entity;

	protected DefLoader.DisposableCardDef m_cardDef;

	protected CardEffect m_playEffect;

	protected List<CardEffect> m_additionalPlayEffects;

	protected CardEffect m_attackEffect;

	protected CardEffect m_deathEffect;

	protected CardEffect m_lifetimeEffect;

	protected List<CardEffect> m_subOptionEffects;

	protected List<List<CardEffect>> m_additionalSubOptionEffects;

	protected List<CardEffect> m_triggerEffects;

	protected List<CardEffect> m_resetGameEffects;

	protected Map<Network.HistBlockStart, CardEffect> m_proxyEffects;

	protected List<CardEffect> m_allEffects;

	protected CardEffect m_customKeywordEffect;

	protected CardEffect m_customChoiceRevealEffect;

	protected CardEffect m_customChoiceConcealEffect;

	protected Map<SpellType, CardEffect> m_spellTableOverrideEffects = new Map<SpellType, CardEffect>();

	protected CardSound[] m_announcerLine = new CardSound[3];

	protected List<EmoteEntry> m_emotes;

	protected Spell m_customSummonSpell;

	protected Spell m_customSpawnSpell;

	protected Spell m_customSpawnSpellOverride;

	protected Spell m_customDeathSpell;

	protected Spell m_customDeathSpellOverride;

	protected Spell m_customDiscardSpell;

	protected Spell m_customDiscardSpellOverride;

	private int m_spellLoadCount;

	protected string m_actorPath;

	protected Actor m_actor;

	protected Actor m_actorWaitingToBeReplaced;

	private bool m_actorReady = true;

	private bool m_actorLoading;

	private bool m_transitioningZones;

	private bool m_hasBeenGrabbedByEnemyActionHandler;

	private Zone m_zone;

	private Zone m_prevZone;

	private bool m_goingThroughDeathrattleReturnfromGraveyard;

	private int m_zonePosition;

	private int m_predictedZonePosition;

	public ZonePositionChange m_minionWasMovedFromSrcToDst;

	private bool m_doNotSort;

	private bool m_beingDrawnByOpponent;

	private bool m_cardStandInInteractive = true;

	private ZoneTransitionStyle m_transitionStyle;

	private bool m_doNotWarpToNewZone;

	private float m_transitionDelay;

	protected bool m_shouldShowTooltip;

	protected bool m_showTooltip;

	protected bool m_overPlayfield;

	protected bool m_mousedOver;

	protected bool m_mousedOverByOpponent;

	protected bool m_shown = true;

	private bool m_inputEnabled = true;

	protected bool m_attacking;

	protected bool m_moving;

	private int m_activeDeathEffectCount;

	private bool m_ignoreDeath;

	private bool m_suppressDeathEffects;

	private bool m_suppressDeathSounds;

	private bool m_suppressKeywordDeaths;

	private bool m_suppressHandToDeckTransition;

	private float m_keywordDeathDelaySec = 0.6f;

	private bool m_suppressActorTriggerSpell;

	private int m_suppressPlaySoundCount;

	private bool m_isBattleCrySource;

	private bool m_secretTriggered;

	private bool m_secretSheathed;

	private bool m_isBaubleAnimating;

	private Spell m_activeSpawnSpell;

	private Player.Side? m_playZoneBlockerSide;

	private float m_delayBeforeHideInNullZoneVisuals;

	private HeroPowerTooltip m_heroPowerTooltip;

	private MagneticPlayData m_magneticPlayData;

	private ZoneChange m_latestZoneChange;

	private bool m_skipMilling;

	private int m_cardDrawTracker;

	private float? m_drawTimeScale;

	private const int DRAW_FAST_THRESHOLD_START = 3;

	private const int DRAW_FAST_THRESHOLD_MAX = 6;

	private const float NORMAL_DRAW_TIME_SCALE = 1f;

	private const float FAST_DRAW_TIME_SCALE = 0.556f;

	private bool m_disableHeroPowerFlipSoundOnce;

	public bool IsBeingDragged { get; set; }

	public bool HasCardDef => m_cardDef?.CardDef != null;

	public bool HasHiddenCardDef => m_cardDef?.CardDef is HiddenCard;

	public string CustomHeroPhoneManaGem
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDef.CardDef.m_CustomHeroPhoneManaGem;
		}
	}

	public string CustomHeroTray
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDef.CardDef.m_CustomHeroTray;
		}
	}

	public string CustomHeroTrayGolden
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDef.CardDef.m_CustomHeroTrayGolden;
		}
	}

	public string CustomHeroPhoneTray
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDef.CardDef.m_CustomHeroPhoneTray;
		}
	}

	public ref string DiamondCustomSpawnSpellPath => ref m_cardDef.CardDef.m_DiamondCustomSpawnSpellPath;

	public ref string GoldenCustomSpawnSpellPath => ref m_cardDef.CardDef.m_GoldenCustomSpawnSpellPath;

	public ref string CustomSpawnSpellPath => ref m_cardDef.CardDef.m_CustomSpawnSpellPath;

	public ref string DiamondCustomSummonSpellPath => ref m_cardDef.CardDef.m_DiamondCustomSummonSpellPath;

	public ref string GoldenCustomSummonSpellPath => ref m_cardDef.CardDef.m_GoldenCustomSummonSpellPath;

	public ref string CustomSummonSpellPath => ref m_cardDef.CardDef.m_CustomSummonSpellPath;

	public List<Board.CustomTraySettings> CustomHeroTraySettings
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDef.CardDef.m_CustomHeroTraySettings;
		}
	}

	public override string ToString()
	{
		if (m_entity != null)
		{
			return m_entity.ToString();
		}
		return "UNKNOWN CARD";
	}

	public Entity GetEntity()
	{
		return m_entity;
	}

	public void SetEntity(Entity entity)
	{
		m_entity = entity;
	}

	public void Destroy()
	{
		if (m_actor != null)
		{
			m_actor.Destroy();
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public Player GetController()
	{
		if (m_entity == null)
		{
			return null;
		}
		return m_entity.GetController();
	}

	public Player.Side GetControllerSide()
	{
		if (m_entity == null)
		{
			return Player.Side.NEUTRAL;
		}
		return m_entity.GetControllerSide();
	}

	public Entity GetHero()
	{
		return GetController()?.GetHero();
	}

	public Card GetHeroCard()
	{
		return GetHero()?.GetCard();
	}

	public Entity GetHeroPower()
	{
		return GetController()?.GetHeroPower();
	}

	public Card GetHeroPowerCard()
	{
		return GetHeroPower()?.GetCard();
	}

	public TAG_PREMIUM GetPremium()
	{
		if (m_entity == null)
		{
			return TAG_PREMIUM.NORMAL;
		}
		return m_entity.GetPremiumType();
	}

	public bool IsOverPlayfield()
	{
		return m_overPlayfield;
	}

	public void NotifyOverPlayfield()
	{
		m_overPlayfield = true;
		UpdateActorState();
	}

	public void NotifyLeftPlayfield()
	{
		m_overPlayfield = false;
		UpdateActorState();
	}

	public void OnDestroy()
	{
		ReleaseAssets();
		if (m_mousedOver && GameState.Get() != null && !(InputManager.Get() == null))
		{
			InputManager.Get().NotifyCardDestroyed(this);
		}
	}

	public void NotifyMousedOver()
	{
		m_mousedOver = true;
		UpdateActorState();
		UpdateProposedManaUsage();
		if ((bool)RemoteActionHandler.Get() && (bool)TargetReticleManager.Get())
		{
			RemoteActionHandler.Get().NotifyOpponentOfMouseOverEntity(GetEntity().GetCard());
		}
		if (GameState.Get() != null)
		{
			GameState.Get().GetGameEntity().NotifyOfCardMousedOver(GetEntity());
		}
		if (m_zone is ZoneHand)
		{
			Spell actorSpell = GetActorSpell(SpellType.SPELL_POWER_HINT_BURST);
			if (actorSpell != null)
			{
				actorSpell.Deactivate();
			}
			Spell actorSpell2 = GetActorSpell(SpellType.SPELL_POWER_HINT_IDLE);
			if (actorSpell2 != null)
			{
				actorSpell2.Deactivate();
			}
			Spell actorSpell3 = GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST);
			if (actorSpell3 != null)
			{
				actorSpell3.Deactivate();
			}
			Spell actorSpell4 = GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_IDLE);
			if (actorSpell4 != null)
			{
				actorSpell4.Deactivate();
			}
			GetActorSpell(SpellType.LIFESTEAL_DOES_DAMAGE_HINT_IDLE);
			if (actorSpell4 != null)
			{
				actorSpell4.Deactivate();
			}
			if (GameState.Get() != null && GameState.Get().IsMulliganManagerActive())
			{
				SoundManager.Get().LoadAndPlay("collection_manager_card_mouse_over.prefab:0d4e20bc78956bc48b5e2963ec39211c", base.gameObject);
			}
			if (ShouldShowHeroPowerTooltip())
			{
				m_heroPowerTooltip.NotifyMousedOver();
			}
		}
		if (m_entity.IsControlledByFriendlySidePlayer() && (m_entity.IsHero() || m_zone is ZonePlay) && !m_transitioningZones)
		{
			bool flag = m_entity.HasSpellPower() || m_entity.HasSpellPowerDouble();
			bool flag2 = m_entity.HasHeroPowerDamage();
			if (flag || flag2)
			{
				Spell actorSpell5 = GetActorSpell(SpellType.SPELL_POWER_HINT_BURST);
				if (actorSpell5 != null)
				{
					actorSpell5.Reactivate();
				}
				if (flag)
				{
					ZoneMgr.Get().OnSpellPowerEntityMousedOver(m_entity.GetSpellPowerSchool());
				}
			}
			if (m_entity.HasHealingDoesDamageHint())
			{
				Spell actorSpell6 = GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST);
				if (actorSpell6 != null)
				{
					actorSpell6.Reactivate();
				}
				ZoneMgr.Get().OnHealingDoesDamageEntityMousedOver();
			}
			if (m_entity.HasLifestealDoesDamageHint())
			{
				Spell actorSpell7 = GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST);
				if (actorSpell7 != null)
				{
					actorSpell7.Reactivate();
				}
				ZoneMgr.Get().OnLifestealDoesDamageEntityMousedOver();
			}
		}
		if (m_entity.IsSidekickHero() && m_zone is ZoneHero && m_heroPowerTooltip != null && !TargetReticleManager.Get().IsActive())
		{
			m_heroPowerTooltip.NotifyMousedOver();
		}
		if (m_entity.IsWeapon() && m_entity.IsExhausted() && m_actor != null && m_actor.GetAttackObject() != null)
		{
			m_actor.GetAttackObject().Enlarge(1f);
		}
		if (m_entity.IsQuest() && m_zone is ZoneSecret)
		{
			QuestController component = m_actor.GetComponent<QuestController>();
			if (component != null)
			{
				component.NotifyMousedOver();
			}
		}
		if (m_entity.IsPuzzle() && m_zone is ZoneSecret)
		{
			PuzzleController component2 = m_actor.GetComponent<PuzzleController>();
			if (component2 != null)
			{
				component2.NotifyMousedOver();
			}
		}
		if (m_entity.IsRulebook() && m_zone is ZoneSecret)
		{
			RulebookController component3 = m_actor.GetComponent<RulebookController>();
			if (component3 != null)
			{
				component3.NotifyMousedOver();
			}
		}
	}

	public void NotifyMousedOut()
	{
		m_mousedOver = false;
		UpdateActorState();
		UpdateProposedManaUsage();
		if ((bool)RemoteActionHandler.Get())
		{
			RemoteActionHandler.Get().NotifyOpponentOfMouseOut();
		}
		if ((bool)TooltipPanelManager.Get())
		{
			TooltipPanelManager.Get().HideKeywordHelp();
		}
		if ((bool)CardTypeBanner.Get())
		{
			CardTypeBanner.Get().Hide(this);
		}
		if (GameState.Get() != null)
		{
			GameState.Get().GetGameEntity().NotifyOfCardMousedOff(GetEntity());
		}
		if (m_entity.IsControlledByFriendlySidePlayer() && (m_entity.IsHero() || m_zone is ZonePlay))
		{
			if (m_entity.HasSpellPower())
			{
				ZoneMgr.Get().OnSpellPowerEntityMousedOut(m_entity.GetSpellPowerSchool());
			}
			if (m_entity.HasHealingDoesDamageHint())
			{
				ZoneMgr.Get().OnHealingDoesDamageEntityMousedOut();
			}
			if (m_entity.HasLifestealDoesDamageHint())
			{
				ZoneMgr.Get().OnLifestealDoesDamageEntityMousedOut();
			}
		}
		if (m_entity.IsWeapon() && m_entity.IsExhausted() && m_actor != null && m_actor.GetAttackObject() != null)
		{
			m_actor.GetAttackObject().ScaleToZero();
		}
		if (m_entity.IsQuest() && m_zone is ZoneSecret)
		{
			QuestController component = m_actor.GetComponent<QuestController>();
			if (component != null)
			{
				component.NotifyMousedOut();
			}
		}
		if (m_entity.IsPuzzle() && m_zone is ZoneSecret && m_actor != null)
		{
			PuzzleController component2 = m_actor.GetComponent<PuzzleController>();
			if (component2 != null)
			{
				component2.NotifyMousedOut();
			}
		}
		if (m_entity.IsRulebook() && m_zone is ZoneSecret && m_actor != null)
		{
			RulebookController component3 = m_actor.GetComponent<RulebookController>();
			if (component3 != null)
			{
				component3.NotifyMousedOut();
			}
		}
		if (m_heroPowerTooltip != null)
		{
			m_heroPowerTooltip.NotifyMousedOut();
		}
	}

	public bool IsMousedOver()
	{
		return m_mousedOver;
	}

	public void NotifyOpponentMousedOverThisCard()
	{
		m_mousedOverByOpponent = true;
		UpdateActorState();
	}

	public void NotifyOpponentMousedOffThisCard()
	{
		m_mousedOverByOpponent = false;
		UpdateActorState();
	}

	public void NotifyPickedUp()
	{
		m_transitioningZones = false;
		if (GetZone() is ZoneHand)
		{
			CutoffFriendlyCardDraw();
		}
		if (ShouldShowHeroPowerTooltip())
		{
			m_heroPowerTooltip.NotifyPickedUp();
		}
	}

	public void NotifyTargetingCanceled()
	{
		if (m_entity.IsCharacter() && !IsAttacking())
		{
			Spell actorAttackSpellForInput = GetActorAttackSpellForInput();
			if (actorAttackSpellForInput != null)
			{
				if (!ShouldShowImmuneVisuals())
				{
					GetActor().ActivateSpellDeathState(SpellType.IMMUNE);
				}
				SpellStateType activeState = actorAttackSpellForInput.GetActiveState();
				if (activeState != 0 && activeState != SpellStateType.CANCEL)
				{
					actorAttackSpellForInput.ActivateState(SpellStateType.CANCEL);
				}
			}
		}
		ActivateHandStateSpells();
	}

	public bool IsInputEnabled()
	{
		if (m_entity != null)
		{
			if (m_entity.HasQueuedChangeEntity())
			{
				return false;
			}
			if (m_entity.IsHeroPower() && m_entity.HasQueuedControllerTagChange())
			{
				return false;
			}
		}
		return m_inputEnabled;
	}

	public void SetInputEnabled(bool enabled)
	{
		m_inputEnabled = enabled;
		UpdateActorState();
	}

	public bool IsAllowedToShowTooltip()
	{
		if (m_zone == null)
		{
			return false;
		}
		if (m_zone.m_ServerTag != TAG_ZONE.PLAY && m_zone.m_ServerTag != TAG_ZONE.SECRET && m_zone.m_ServerTag == TAG_ZONE.HAND && m_zone.m_Side != Player.Side.OPPOSING)
		{
			return false;
		}
		if (GameState.Get() != null)
		{
			if (m_entity.IsSidekickHero())
			{
				if (m_entity.GetZone() != TAG_ZONE.PLAY)
				{
					return false;
				}
			}
			else if (m_entity.IsHero() && m_entity.GetZone() == TAG_ZONE.PLAY && !GameState.Get().GetBooleanGameOption(GameEntityOption.SHOW_HERO_TOOLTIPS))
			{
				return false;
			}
		}
		if (m_entity.IsQuest() || m_entity.IsPuzzle() || m_entity.IsRulebook())
		{
			return false;
		}
		return true;
	}

	public bool IsAbleToShowTooltip()
	{
		if (m_entity == null)
		{
			return false;
		}
		if (m_actor == null)
		{
			return false;
		}
		if (BigCard.Get() == null)
		{
			return false;
		}
		return true;
	}

	public bool GetShouldShowTooltip()
	{
		return m_shouldShowTooltip;
	}

	public void SetShouldShowTooltip()
	{
		if (IsAllowedToShowTooltip() && !m_shouldShowTooltip)
		{
			m_shouldShowTooltip = true;
		}
	}

	public void ShowTooltip()
	{
		if (!m_showTooltip)
		{
			m_showTooltip = true;
			UpdateTooltip();
		}
	}

	public void HideTooltip()
	{
		m_shouldShowTooltip = false;
		if (m_showTooltip)
		{
			m_showTooltip = false;
			UpdateTooltip();
		}
	}

	public bool IsShowingTooltip()
	{
		return m_showTooltip;
	}

	private void ShowMouseOverSpell()
	{
		if (m_entity == null || m_actor == null)
		{
			return;
		}
		if (m_entity.HasTag(GAME_TAG.VOODOO_LINK) || m_entity.DoEnchantmentsHaveVoodooLink())
		{
			Spell spell = m_actor.GetSpell(SpellType.VOODOO_LINK);
			if ((bool)spell)
			{
				spell.SetSource(base.gameObject);
				spell.Activate();
			}
		}
		string cardId = m_entity.GetCardId();
		if (cardId == MagtheridonLinkToHellfireWardersSpell.MagtheridonId || cardId == MagtheridonLinkToHellfireWardersSpell.HellfireWarderId)
		{
			Spell spell2 = m_actor.GetSpell(SpellType.MAGTHERIDON_LINK);
			if ((bool)spell2)
			{
				spell2.SetSource(base.gameObject);
				spell2.Activate();
			}
		}
	}

	private void HideMouseOverSpell()
	{
		if (!(m_actor == null))
		{
			Spell spellIfLoaded = m_actor.GetSpellIfLoaded(SpellType.VOODOO_LINK);
			if ((bool)spellIfLoaded)
			{
				spellIfLoaded.Deactivate();
			}
			spellIfLoaded = m_actor.GetSpellIfLoaded(SpellType.MAGTHERIDON_LINK);
			if ((bool)spellIfLoaded)
			{
				spellIfLoaded.Deactivate();
			}
		}
	}

	public void UpdateTooltip()
	{
		if (GetShouldShowTooltip() && IsAllowedToShowTooltip() && IsAbleToShowTooltip() && m_showTooltip)
		{
			ShowMouseOverSpell();
			if (BigCard.Get() != null)
			{
				BigCard.Get().Show(this);
			}
			return;
		}
		m_showTooltip = false;
		m_shouldShowTooltip = false;
		HideMouseOverSpell();
		if (BigCard.Get() != null)
		{
			BigCard.Get().Hide(this);
		}
	}

	public bool IsAttacking()
	{
		return m_attacking;
	}

	public void EnableAttacking(bool enable)
	{
		m_attacking = enable;
	}

	public bool IsMoving()
	{
		return m_moving;
	}

	public void EnableMoving(bool enable)
	{
		m_moving = enable;
	}

	public bool WillIgnoreDeath()
	{
		return m_ignoreDeath;
	}

	public void IgnoreDeath(bool ignore)
	{
		m_ignoreDeath = ignore;
	}

	public bool WillSuppressDeathEffects()
	{
		return m_suppressDeathEffects;
	}

	public void SuppressDeathEffects(bool suppress)
	{
		m_suppressDeathEffects = suppress;
	}

	public bool WillSuppressDeathSounds()
	{
		return m_suppressDeathSounds;
	}

	public void SuppressDeathSounds(bool suppress)
	{
		m_suppressDeathSounds = suppress;
	}

	public bool WillSuppressKeywordDeaths()
	{
		return m_suppressKeywordDeaths;
	}

	public void SuppressKeywordDeaths(bool suppress)
	{
		m_suppressKeywordDeaths = suppress;
	}

	public float GetKeywordDeathDelaySec()
	{
		return m_keywordDeathDelaySec;
	}

	public void SetKeywordDeathDelaySec(float sec)
	{
		m_keywordDeathDelaySec = sec;
	}

	public bool WillSuppressActorTriggerSpell()
	{
		return m_suppressActorTriggerSpell;
	}

	public void SuppressActorTriggerSpell(bool suppress)
	{
		m_suppressActorTriggerSpell = suppress;
	}

	public bool WillSuppressPlaySounds()
	{
		if ((GetEntity() != null && GetEntity().HasTag(GAME_TAG.SUPPRESS_ALL_SUMMON_VO)) || GetController().HasTag(GAME_TAG.SUPPRESS_SUMMON_VO_FOR_PLAYER))
		{
			return true;
		}
		return m_suppressPlaySoundCount > 0;
	}

	public bool WillSuppressCustomSpells()
	{
		if (!GameState.Get().GetGameEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_SPELLS) && !GetController().HasTag(GAME_TAG.FORCE_NO_CUSTOM_SPELLS))
		{
			return GetEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_SPELLS);
		}
		return true;
	}

	public bool WillSuppressCustomSummonSpells()
	{
		if (!GameState.Get().GetGameEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_SUMMON_SPELLS) && !GetController().HasTag(GAME_TAG.FORCE_NO_CUSTOM_SUMMON_SPELLS))
		{
			return GetEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_SUMMON_SPELLS);
		}
		return true;
	}

	public bool WillSuppressCustomLifetimeSpells()
	{
		if (!GameState.Get().GetGameEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_LIFETIME_SPELLS) && !GetController().HasTag(GAME_TAG.FORCE_NO_CUSTOM_LIFETIME_SPELLS))
		{
			return GetEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_LIFETIME_SPELLS);
		}
		return true;
	}

	public bool WillSuppressCustomKeywordSpells()
	{
		if (!GameState.Get().GetGameEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_KEYWORD_SPELLS) && !GetController().HasTag(GAME_TAG.FORCE_NO_CUSTOM_KEYWORD_SPELLS))
		{
			return GetEntity().HasTag(GAME_TAG.FORCE_NO_CUSTOM_KEYWORD_SPELLS);
		}
		return true;
	}

	public void SuppressPlaySounds(bool suppress)
	{
		if (suppress)
		{
			m_suppressPlaySoundCount++;
		}
		else if (--m_suppressPlaySoundCount < 0)
		{
			m_suppressPlaySoundCount = 0;
		}
	}

	public void SuppressHandToDeckTransition()
	{
		m_suppressHandToDeckTransition = true;
	}

	public bool IsShown()
	{
		return m_shown;
	}

	public void ShowCard()
	{
		if (!m_shown)
		{
			m_shown = true;
			ShowImpl();
		}
	}

	private void ShowImpl()
	{
		if (!(m_actor == null))
		{
			m_actor.Show();
			RefreshActor();
		}
	}

	public void HideCard()
	{
		if (m_shown && !m_actorLoading)
		{
			m_shown = false;
			HideImpl();
		}
	}

	private void HideImpl()
	{
		if (!(m_actor == null))
		{
			m_actor.Hide();
		}
	}

	public void SetBattleCrySource(bool source)
	{
		m_isBattleCrySource = source;
		if (m_actor != null)
		{
			if (source)
			{
				SceneUtils.SetLayer(m_actor.gameObject, GameLayer.IgnoreFullScreenEffects);
				return;
			}
			SceneUtils.SetLayer(m_actor.gameObject, GameLayer.Default);
			SceneUtils.SetLayer(m_actor.GetMeshRenderer().gameObject, GameLayer.CardRaycast);
		}
	}

	public void DoTauntNotification()
	{
		if (!(m_activeSpawnSpell != null) || !m_activeSpawnSpell.IsActive())
		{
			iTween.PunchScale(m_actor.gameObject, new Vector3(0.2f, 0.2f, 0.2f), 0.5f);
		}
	}

	public void UpdateProposedManaUsage()
	{
		if (GameState.Get() == null || GameState.Get().GetSelectedOption() != -1)
		{
			return;
		}
		Player player = GameState.Get().GetPlayer(GetEntity().GetControllerId());
		if (player == null || !player.IsFriendlySide() || !player.HasTag(GAME_TAG.CURRENT_PLAYER))
		{
			return;
		}
		if (m_mousedOver)
		{
			bool num = m_entity.GetZone() == TAG_ZONE.HAND;
			bool flag = m_entity.IsHeroPowerOrGameModeButton();
			if ((num || flag) && GameState.Get().IsValidOption(m_entity) && (!m_entity.IsSpell() || !player.HasTag(GAME_TAG.SPELLS_COST_HEALTH)) && !m_entity.HasTag(GAME_TAG.CARD_COSTS_HEALTH))
			{
				player.ProposeManaCrystalUsage(m_entity);
			}
		}
		else
		{
			player.CancelAllProposedMana(m_entity);
		}
	}

	public void SetMagneticPlayData(MagneticPlayData data)
	{
		if (data != null)
		{
			if (m_magneticPlayData != null)
			{
				Log.Gameplay.PrintError("{0}.SetMagneticPlayData: m_magneticPlayData is already set! {1}", this, m_magneticPlayData);
			}
			m_magneticPlayData = data;
		}
	}

	public MagneticPlayData GetMagneticPlayData()
	{
		return m_magneticPlayData;
	}

	public void DetermineIfOverrideDrawTimeScale()
	{
		if (!m_drawTimeScale.HasValue)
		{
			if (m_cardDrawTracker < 3)
			{
				m_drawTimeScale = 1f;
			}
			else if (m_cardDrawTracker <= 6)
			{
				float num = -0.111f;
				m_drawTimeScale = 1f + num * (float)(m_cardDrawTracker + 1 - 3);
			}
			else
			{
				m_drawTimeScale = 0.556f;
			}
		}
	}

	public void ResetCardDrawTimeScale()
	{
		m_drawTimeScale = null;
	}

	public bool CanPlayHealingDoesDamageHint()
	{
		if (!IsShown())
		{
			return false;
		}
		if (m_entity == null)
		{
			return false;
		}
		if (m_actor == null || !m_actor.IsShown())
		{
			return false;
		}
		if (m_entity.HasTag(GAME_TAG.AFFECTED_BY_HEALING_DOES_DAMAGE))
		{
			return true;
		}
		if (m_entity.HasTag(GAME_TAG.LIFESTEAL))
		{
			return true;
		}
		return m_entity.GetCardTextBuilder().ContainsBonusHealingToken(m_entity);
	}

	public bool CanPlayLifestealDoesDamageHint()
	{
		if (!IsShown())
		{
			return false;
		}
		if (m_entity == null)
		{
			return false;
		}
		if (m_actor == null || !m_actor.IsShown())
		{
			return false;
		}
		return m_entity.HasTag(GAME_TAG.LIFESTEAL);
	}

	public bool CanPlaySpellPowerHint(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		if (!IsShown())
		{
			return false;
		}
		if (m_actor == null || !m_actor.IsShown())
		{
			return false;
		}
		if (m_entity == null)
		{
			return false;
		}
		if (m_entity.IsAffectedBySpellPower() && spellSchool == TAG_SPELL_SCHOOL.NONE)
		{
			return true;
		}
		if (spellSchool != 0 && spellSchool != m_entity.GetSpellSchool())
		{
			return false;
		}
		return m_entity.GetCardTextBuilder().ContainsBonusDamageToken(m_entity);
	}

	public DefLoader.DisposableCardDef ShareDisposableCardDef()
	{
		return m_cardDef?.Share();
	}

	public void SetCardDef(DefLoader.DisposableCardDef cardDef, bool updateActor)
	{
		if (!(m_cardDef?.CardDef == cardDef?.CardDef))
		{
			ReleaseCardDef();
			m_cardDef = cardDef.Share();
			InitCardDefAssets();
			if (m_actor != null && !updateActor)
			{
				m_actor.SetCardDef(m_cardDef);
				m_actor.UpdateAllComponents();
			}
		}
	}

	public void PurgeSpells()
	{
		foreach (CardEffect allEffect in m_allEffects)
		{
			allEffect.PurgeSpells();
		}
	}

	private bool ShouldPreloadCardAssets()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return false;
		}
		return Options.Get().GetBool(Option.PRELOAD_CARD_ASSETS, defaultVal: false);
	}

	public void OverrideCustomSpawnSpell(Spell spell)
	{
		if (spell == null)
		{
			Debug.LogErrorFormat("Tried to set OverrideCustomSpawnSpell to null!");
		}
		else
		{
			m_customSpawnSpellOverride = SetupOverrideSpell(m_customSpawnSpellOverride, spell);
		}
	}

	public void OverrideCustomDeathSpell(Spell spell)
	{
		if (spell == null)
		{
			Debug.LogErrorFormat("Tried to set OverrideCustomDeathSpell to null!");
		}
		else
		{
			m_customDeathSpellOverride = SetupOverrideSpell(m_customDeathSpellOverride, spell);
		}
	}

	public void OverrideCustomDiscardSpell(Spell spell)
	{
		if (spell == null)
		{
			Debug.LogErrorFormat("Tried to set OverrideCustomDiscardSpell to null!");
		}
		else
		{
			m_customDiscardSpellOverride = SetupOverrideSpell(m_customDiscardSpellOverride, spell);
		}
	}

	public Texture GetPortraitTexture()
	{
		if (!(m_cardDef?.CardDef == null))
		{
			return m_cardDef.CardDef.GetPortraitTexture();
		}
		return null;
	}

	public Material GetGoldenMaterial()
	{
		if (!(m_cardDef?.CardDef == null))
		{
			return m_cardDef.CardDef.GetPremiumPortraitMaterial();
		}
		return null;
	}

	public CardEffect GetPlayEffect(int index)
	{
		if (index > 0)
		{
			if (--index >= m_additionalPlayEffects.Count)
			{
				return null;
			}
			return m_additionalPlayEffects[index];
		}
		return m_playEffect;
	}

	public CardEffect GetOrCreateProxyEffect(Network.HistBlockStart blockStart, CardEffectDef proxyEffectDef)
	{
		if (m_proxyEffects == null)
		{
			m_proxyEffects = new Map<Network.HistBlockStart, CardEffect>();
		}
		if (m_proxyEffects.ContainsKey(blockStart))
		{
			return m_proxyEffects[blockStart];
		}
		CardEffect effect = new CardEffect(proxyEffectDef, this);
		InitEffect(proxyEffectDef, ref effect);
		m_proxyEffects.Add(blockStart, effect);
		return effect;
	}

	public void DeactivatePlaySpell()
	{
		Entity entity = GetEntity();
		Entity parentEntity = entity.GetParentEntity();
		Spell spell;
		if (parentEntity == null)
		{
			spell = GetPlaySpell(0, loadIfNeeded: false);
		}
		else
		{
			Card card = parentEntity.GetCard();
			int subCardIndex = parentEntity.GetSubCardIndex(entity);
			spell = card.GetSubOptionSpell(subCardIndex, 0, loadIfNeeded: false);
		}
		if (spell != null && spell.GetActiveState() != 0)
		{
			spell.SafeActivateState(SpellStateType.CANCEL);
		}
	}

	public Spell GetPlaySpell(int index, bool loadIfNeeded = true)
	{
		return GetPlayEffect(index)?.GetSpell(loadIfNeeded);
	}

	public List<CardSoundSpell> GetPlaySoundSpells(int index, bool loadIfNeeded = true)
	{
		return GetPlayEffect(index)?.GetSoundSpells(loadIfNeeded);
	}

	public Spell GetAttackSpell(bool loadIfNeeded = true)
	{
		if (m_attackEffect == null)
		{
			return null;
		}
		return m_attackEffect.GetSpell(loadIfNeeded);
	}

	public List<CardSoundSpell> GetAttackSoundSpells(bool loadIfNeeded = true)
	{
		if (m_attackEffect == null)
		{
			return null;
		}
		return m_attackEffect.GetSoundSpells(loadIfNeeded);
	}

	public List<CardSoundSpell> GetDeathSoundSpells(bool loadIfNeeded = true)
	{
		if (m_deathEffect == null)
		{
			return null;
		}
		return m_deathEffect.GetSoundSpells(loadIfNeeded);
	}

	public Spell GetLifetimeSpell(bool loadIfNeeded = true)
	{
		if (m_lifetimeEffect == null)
		{
			return null;
		}
		return m_lifetimeEffect.GetSpell(loadIfNeeded);
	}

	public List<CardSoundSpell> GetLifetimeSoundSpells(bool loadIfNeeded = true)
	{
		if (m_lifetimeEffect == null)
		{
			return null;
		}
		return m_lifetimeEffect.GetSoundSpells(loadIfNeeded);
	}

	public CardEffect GetSubOptionEffect(int suboption, int index)
	{
		if (suboption < 0)
		{
			return null;
		}
		if (index > 0)
		{
			if (m_additionalSubOptionEffects == null)
			{
				return null;
			}
			if (suboption >= m_additionalSubOptionEffects.Count)
			{
				return null;
			}
			List<CardEffect> list = m_additionalSubOptionEffects[suboption];
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
		if (m_subOptionEffects == null)
		{
			return null;
		}
		if (suboption >= m_subOptionEffects.Count)
		{
			return null;
		}
		return m_subOptionEffects[suboption];
	}

	public Spell GetSubOptionSpell(int suboption, int index, bool loadIfNeeded = true)
	{
		return GetSubOptionEffect(suboption, index)?.GetSpell(loadIfNeeded);
	}

	public List<CardSoundSpell> GetSubOptionSoundSpells(int suboption, int index, bool loadIfNeeded = true)
	{
		return GetSubOptionEffect(suboption, index)?.GetSoundSpells(loadIfNeeded);
	}

	public CardEffect GetTriggerEffect(int index)
	{
		if (m_triggerEffects == null)
		{
			return null;
		}
		if (index < 0)
		{
			return null;
		}
		if (index >= m_triggerEffects.Count)
		{
			return null;
		}
		return m_triggerEffects[index];
	}

	public CardEffect GetResetGameEffect(int index)
	{
		if (m_resetGameEffects == null)
		{
			return null;
		}
		if (index < 0)
		{
			return null;
		}
		if (index >= m_resetGameEffects.Count)
		{
			return null;
		}
		return m_resetGameEffects[index];
	}

	public Spell GetTriggerSpell(int index, bool loadIfNeeded = true)
	{
		return GetTriggerEffect(index)?.GetSpell(loadIfNeeded);
	}

	public List<CardSoundSpell> GetTriggerSoundSpells(int index, bool loadIfNeeded = true)
	{
		return GetTriggerEffect(index)?.GetSoundSpells(loadIfNeeded);
	}

	public Spell GetCustomKeywordSpell()
	{
		if (m_customKeywordEffect == null)
		{
			return null;
		}
		return m_customKeywordEffect.GetSpell();
	}

	public Spell GetCustomSummonSpell()
	{
		return m_customSummonSpell;
	}

	public Spell GetCustomSpawnSpell()
	{
		return m_customSpawnSpell;
	}

	public Spell GetCustomDeathSpell()
	{
		return m_customDeathSpell;
	}

	public Spell GetCustomDeathSpellOverride()
	{
		return m_customDeathSpellOverride;
	}

	public Spell GetCustomChoiceRevealSpell()
	{
		if (m_customChoiceRevealEffect == null)
		{
			return null;
		}
		return m_customChoiceRevealEffect.GetSpell();
	}

	public Spell GetCustomChoiceConcealSpell()
	{
		if (m_customChoiceConcealEffect == null)
		{
			return null;
		}
		return m_customChoiceConcealEffect.GetSpell();
	}

	public Spell GetSpellTableOverride(SpellType spellType)
	{
		CardEffect value = null;
		if (m_spellTableOverrideEffects.TryGetValue(spellType, out value))
		{
			return value.GetSpell();
		}
		foreach (SpellTableOverride spellTableOverride in m_cardDef.CardDef.m_SpellTableOverrides)
		{
			if (spellTableOverride.m_Type == spellType)
			{
				if (string.IsNullOrEmpty(spellTableOverride.m_SpellPrefabName))
				{
					break;
				}
				CardEffect effect = null;
				InitEffect(spellTableOverride.m_SpellPrefabName, ref effect);
				if (effect != null)
				{
					m_spellTableOverrideEffects[spellType] = effect;
					return effect.GetSpell();
				}
			}
		}
		return null;
	}

	public AudioSource GetAnnouncerLine(AnnouncerLineType type)
	{
		CardSound cardSound = m_announcerLine[(int)type];
		if (cardSound == null || cardSound.GetSound() == null)
		{
			if (m_announcerLine[0] == null)
			{
				string message = $"Card.GetAnnouncerLine(AnnouncerLineType type) - Failed to load announcer audio source.";
				if (HearthstoneApplication.UseDevWorkarounds())
				{
					Debug.LogError(message);
				}
				return SoundManager.Get().GetPlaceholderSource();
			}
			cardSound = m_announcerLine[0];
		}
		return cardSound.GetSound();
	}

	public EmoteEntry GetEmoteEntry(EmoteType emoteType)
	{
		if (m_emotes == null)
		{
			return null;
		}
		bool flag = emoteType == EmoteType.GREETINGS || emoteType == EmoteType.MIRROR_GREETINGS;
		if (SpecialEventManager.Get().IsEventActive(SpecialEventType.LUNAR_NEW_YEAR, activeIfDoesNotExist: false))
		{
			if (flag)
			{
				foreach (EmoteEntry emote in m_emotes)
				{
					if (emote.GetEmoteType() == EmoteType.HAPPY_NEW_YEAR_LUNAR)
					{
						return emote;
					}
				}
			}
		}
		else if (SpecialEventManager.Get().IsEventActive(SpecialEventType.FEAST_OF_WINTER_VEIL, activeIfDoesNotExist: false))
		{
			if (flag)
			{
				foreach (EmoteEntry emote2 in m_emotes)
				{
					if (emote2.GetEmoteType() == EmoteType.HAPPY_HOLIDAYS)
					{
						return emote2;
					}
				}
			}
		}
		else if (SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_FIRE_FESTIVAL_EMOTES_EVERGREEN, activeIfDoesNotExist: false))
		{
			if (flag)
			{
				foreach (EmoteEntry emote3 in m_emotes)
				{
					if (emote3.GetEmoteType() == EmoteType.FIRE_FESTIVAL)
					{
						return emote3;
					}
				}
			}
			else if (emoteType == EmoteType.WOW)
			{
				foreach (EmoteEntry emote4 in m_emotes)
				{
					if (emote4.GetEmoteType() == EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_THREE)
					{
						return emote4;
					}
				}
			}
		}
		else if (SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_HAPPY_NEW_YEAR, activeIfDoesNotExist: false))
		{
			if (flag)
			{
				foreach (EmoteEntry emote5 in m_emotes)
				{
					if (emote5.GetEmoteType() == EmoteType.HAPPY_NEW_YEAR)
					{
						return emote5;
					}
				}
			}
		}
		else if (SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_PIRATE_DAY, activeIfDoesNotExist: false))
		{
			if (flag)
			{
				foreach (EmoteEntry emote6 in m_emotes)
				{
					if (emote6.GetEmoteType() == EmoteType.PIRATE_DAY)
					{
						return emote6;
					}
				}
			}
		}
		else if (SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_NOBLEGARDEN, activeIfDoesNotExist: false) && flag)
		{
			foreach (EmoteEntry emote7 in m_emotes)
			{
				if (emote7.GetEmoteType() == EmoteType.HAPPY_NOBLEGARDEN)
				{
					return emote7;
				}
			}
		}
		foreach (EmoteEntry emote8 in m_emotes)
		{
			if (emote8.GetEmoteType() == emoteType)
			{
				return emote8;
			}
		}
		return null;
	}

	public Spell GetBestSummonSpell()
	{
		bool standard;
		return GetBestSummonSpell(out standard);
	}

	public Spell GetBestSummonSpell(out bool standard)
	{
		if (m_customSummonSpell != null && GetMagneticPlayData() == null && GetEntity() != null && !GetEntity().HasTag(GAME_TAG.CARD_DOES_NOTHING) && !WillSuppressCustomSpells() && !WillSuppressCustomSummonSpells())
		{
			standard = false;
			return m_customSummonSpell;
		}
		standard = true;
		if (m_cardDef?.CardDef == null)
		{
			Log.Gameplay.PrintError("Cannot determine best summon spell. Missing CardDef");
			return null;
		}
		bool useFastAnimations = GameState.Get() != null && GameState.Get().GetGameEntity().HasTag(GAME_TAG.BACON_USE_FAST_ANIMATIONS);
		SpellType spellType = m_cardDef.CardDef.DetermineSummonInSpell_HandToPlay(this, useFastAnimations);
		return GetActorSpell(spellType);
	}

	public Spell GetBestSpawnSpell()
	{
		bool standard;
		return GetBestSpawnSpell(out standard);
	}

	public Spell GetBestSpawnSpell(out bool standard)
	{
		standard = false;
		if (m_entity.HasTag(GAME_TAG.HAS_BEEN_REBORN))
		{
			Spell actorSpell = GetActorSpell(SpellType.REBORN_SPAWN);
			if (actorSpell != null)
			{
				return actorSpell;
			}
		}
		if ((bool)m_customSpawnSpellOverride)
		{
			return m_customSpawnSpellOverride;
		}
		if ((bool)m_customSpawnSpell)
		{
			return m_customSpawnSpell;
		}
		standard = true;
		if (m_entity.IsControlledByFriendlySidePlayer())
		{
			return GetActorSpell(SpellType.FRIENDLY_SPAWN_MINION);
		}
		return GetActorSpell(SpellType.OPPONENT_SPAWN_MINION);
	}

	public Spell GetBestDeathSpell()
	{
		bool standard;
		return GetBestDeathSpell(out standard);
	}

	public Spell GetBestDeathSpell(out bool standard)
	{
		return GetBestDeathSpell(m_actor, out standard);
	}

	private Spell GetBestDeathSpell(Actor actor)
	{
		bool standard;
		return GetBestDeathSpell(actor, out standard);
	}

	private Spell GetBestDeathSpell(Actor actor, out bool standard)
	{
		standard = false;
		if (m_prevZone is ZoneHand && m_zone is ZoneGraveyard)
		{
			if ((bool)m_customDiscardSpellOverride)
			{
				return m_customDiscardSpellOverride;
			}
			if ((bool)m_customDiscardSpell && !m_entity.IsSilenced())
			{
				return m_customDiscardSpell;
			}
		}
		else
		{
			if ((bool)m_customDeathSpellOverride)
			{
				return m_customDeathSpellOverride;
			}
			if ((bool)m_customDeathSpell && !m_entity.IsSilenced())
			{
				return m_customDeathSpell;
			}
		}
		standard = true;
		return actor.GetSpell(SpellType.DEATH);
	}

	public void ActivateCharacterPlayEffects()
	{
		if (!WillSuppressPlaySounds())
		{
			ActivateSoundSpellList(m_playEffect.GetSoundSpells());
		}
		SuppressPlaySounds(suppress: false);
		ActivateLifetimeEffects();
	}

	public void ActivateCharacterAttackEffects()
	{
		ActivateSoundSpellList(m_attackEffect.GetSoundSpells());
	}

	public void ActivateCharacterDeathEffects()
	{
		if (m_suppressDeathEffects)
		{
			return;
		}
		if (!m_suppressDeathSounds)
		{
			if (((m_emotes == null) ? (-1) : m_emotes.FindIndex((EmoteEntry e) => e != null && e.GetEmoteType() == EmoteType.DEATH_LINE)) >= 0)
			{
				PlayEmote(EmoteType.DEATH_LINE);
			}
			else
			{
				ActivateSoundSpellList(m_deathEffect.GetSoundSpells());
			}
		}
		m_suppressDeathSounds = false;
		DeactivateLifetimeEffects();
	}

	public void ActivateLifetimeEffects()
	{
		if (m_lifetimeEffect == null || m_entity.IsSilenced() || m_entity.HasTag(GAME_TAG.CARD_DOES_NOTHING) || WillSuppressCustomSpells() || WillSuppressCustomLifetimeSpells())
		{
			return;
		}
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		if (gameEntity == null || !gameEntity.HasTag(GAME_TAG.SQUELCH_LIFETIME_EFFECTS))
		{
			Spell spell = m_lifetimeEffect.GetSpell();
			if (spell != null)
			{
				spell.Deactivate();
				spell.ActivateState(SpellStateType.BIRTH);
			}
			if (m_lifetimeEffect.GetSoundSpells() != null)
			{
				ActivateSoundSpellList(m_lifetimeEffect.GetSoundSpells());
			}
		}
	}

	public void DeactivateLifetimeEffects()
	{
		if (m_lifetimeEffect == null)
		{
			return;
		}
		Spell spell = m_lifetimeEffect.GetSpell();
		if (spell != null)
		{
			SpellStateType activeState = spell.GetActiveState();
			if (activeState != 0 && activeState != SpellStateType.DEATH)
			{
				spell.ActivateState(SpellStateType.DEATH);
			}
		}
	}

	public void ActivateCustomKeywordEffect()
	{
		if (m_customKeywordEffect == null || (GetEntity() != null && (GetEntity().HasTag(GAME_TAG.CARD_DOES_NOTHING) || WillSuppressCustomSpells() || WillSuppressCustomKeywordSpells())))
		{
			return;
		}
		Spell spell = m_customKeywordEffect.GetSpell();
		if (spell == null)
		{
			Debug.LogWarning($"Card.ActivateCustomKeywordEffect() -- failed to load custom keyword spell for card {ToString()}");
			return;
		}
		if (spell.DoesBlockServerEvents())
		{
			GameState.Get().AddServerBlockingSpell(spell);
		}
		TransformUtil.AttachAndPreserveLocalTransform(spell.transform, m_actor.transform);
		spell.ActivateState(SpellStateType.BIRTH);
	}

	public void DeactivateCustomKeywordEffect()
	{
		if (m_customKeywordEffect != null)
		{
			Spell spell = m_customKeywordEffect.GetSpell(loadIfNeeded: false);
			if (!(spell == null) && spell.IsActive())
			{
				spell.ActivateState(SpellStateType.DEATH);
			}
		}
	}

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
			ActivateSoundSpell(soundSpell);
			result = true;
		}
		return result;
	}

	public bool ActivateSoundSpell(CardSoundSpell soundSpell)
	{
		if (soundSpell == null || GetEntity().HasTag(GAME_TAG.CARD_DOES_NOTHING))
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
			StartCoroutine(WaitThenActivateSoundSpell(soundSpell));
		}
		else
		{
			soundSpell.Reactivate();
		}
		return true;
	}

	public bool HasActiveEmoteSound()
	{
		if (m_emotes == null)
		{
			return false;
		}
		foreach (EmoteEntry emote in m_emotes)
		{
			CardSoundSpell soundSpell = emote.GetSoundSpell(loadIfNeeded: false);
			if (soundSpell != null && soundSpell.IsActive())
			{
				return true;
			}
		}
		return false;
	}

	public EmoteEntry GetActiveEmoteSound()
	{
		if (m_emotes == null)
		{
			return null;
		}
		foreach (EmoteEntry emote in m_emotes)
		{
			CardSoundSpell soundSpell = emote.GetSoundSpell(loadIfNeeded: false);
			if (soundSpell != null && soundSpell.IsActive())
			{
				return emote;
			}
		}
		return null;
	}

	public bool HasUnfinishedEmoteSpell()
	{
		if (m_emotes == null)
		{
			return false;
		}
		foreach (EmoteEntry emote in m_emotes)
		{
			Spell spell = emote.GetSpell(loadIfNeeded: false);
			if (spell != null && !spell.IsFinished())
			{
				return true;
			}
		}
		return false;
	}

	public CardSoundSpell PlayEmote(EmoteType emoteType)
	{
		return PlayEmote(emoteType, Notification.SpeechBubbleDirection.None);
	}

	public CardSoundSpell PlayEmote(EmoteType emoteType, Notification.SpeechBubbleDirection overrideDirection)
	{
		EmoteEntry emoteEntry = GetEmoteEntry(emoteType);
		CardSoundSpell cardSoundSpell = emoteEntry?.GetSoundSpell();
		Spell spell = emoteEntry?.GetSpell();
		if (!m_entity.IsHero())
		{
			return null;
		}
		if (m_actor == null)
		{
			return null;
		}
		if (cardSoundSpell != null)
		{
			cardSoundSpell.Reactivate();
			if (cardSoundSpell.IsActive())
			{
				for (int i = 0; i < m_emotes.Count; i++)
				{
					EmoteEntry emoteEntry2 = m_emotes[i];
					if (emoteEntry2 != emoteEntry)
					{
						Spell soundSpell = emoteEntry2.GetSoundSpell(loadIfNeeded: false);
						if ((bool)soundSpell)
						{
							soundSpell.Deactivate();
						}
					}
				}
			}
			GameState.Get().GetGameEntity().OnEmotePlayed(this, emoteType, cardSoundSpell);
		}
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.BottomLeft;
		if (GetEntity().IsControlledByOpposingSidePlayer())
		{
			direction = Notification.SpeechBubbleDirection.TopRight;
		}
		if (overrideDirection != 0)
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
			notification = NotificationManager.Get().CreateSpeechBubble(text, direction, m_actor, bDestroyWhenNewCreated: true);
			float num = 1.5f;
			if ((bool)cardSoundSpell)
			{
				AudioSource activeAudioSource = cardSoundSpell.GetActiveAudioSource();
				if ((bool)activeAudioSource && (bool)activeAudioSource.clip && num < activeAudioSource.clip.length)
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

	private void InitCardDefAssets()
	{
		InitEffect(m_cardDef.CardDef.m_PlayEffectDef, ref m_playEffect);
		InitEffectList(m_cardDef.CardDef.m_AdditionalPlayEffectDefs, ref m_additionalPlayEffects);
		InitEffect(m_cardDef.CardDef.m_AttackEffectDef, ref m_attackEffect);
		InitEffect(m_cardDef.CardDef.m_DeathEffectDef, ref m_deathEffect);
		InitEffect(m_cardDef.CardDef.m_LifetimeEffectDef, ref m_lifetimeEffect);
		InitEffect(m_cardDef.CardDef.m_CustomKeywordSpellPath, ref m_customKeywordEffect);
		InitEffect(m_cardDef.CardDef.m_CustomChoiceRevealSpellPath, ref m_customChoiceRevealEffect);
		InitEffect(m_cardDef.CardDef.m_CustomChoiceConcealSpellPath, ref m_customChoiceConcealEffect);
		InitEffectList(m_cardDef.CardDef.m_SubOptionEffectDefs, ref m_subOptionEffects);
		InitEffectListList(m_cardDef.CardDef.m_AdditionalSubOptionEffectDefs, ref m_additionalSubOptionEffects);
		InitEffectList(m_cardDef.CardDef.m_TriggerEffectDefs, ref m_triggerEffects);
		InitEffectList(m_cardDef.CardDef.m_ResetGameEffectDefs, ref m_resetGameEffects);
		InitSound(m_cardDef.CardDef.m_AnnouncerLinePath, ref m_announcerLine[0], alwaysValid: true);
		InitSound(m_cardDef.CardDef.m_AnnouncerLineBeforeVersusPath, ref m_announcerLine[1], alwaysValid: false);
		InitSound(m_cardDef.CardDef.m_AnnouncerLineAfterVersusPath, ref m_announcerLine[2], alwaysValid: false);
		InitEmoteList();
	}

	private void InitEffect(CardEffectDef effectDef, ref CardEffect effect)
	{
		DestroyCardEffect(ref effect);
		if (effectDef != null)
		{
			effect = new CardEffect(effectDef, this);
			if (m_allEffects == null)
			{
				m_allEffects = new List<CardEffect>();
			}
			m_allEffects.Add(effect);
			if (ShouldPreloadCardAssets())
			{
				effect.LoadAll();
			}
		}
	}

	private void InitEffect(string spellPath, ref CardEffect effect)
	{
		DestroyCardEffect(ref effect);
		if (!string.IsNullOrEmpty(spellPath))
		{
			effect = new CardEffect(spellPath, this);
			if (m_allEffects == null)
			{
				m_allEffects = new List<CardEffect>();
			}
			m_allEffects.Add(effect);
			if (ShouldPreloadCardAssets())
			{
				effect.LoadAll();
			}
		}
	}

	private void InitEffectList(List<CardEffectDef> effectDefs, ref List<CardEffect> effects)
	{
		DestroyCardEffectList(ref effects);
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
				if (m_allEffects == null)
				{
					m_allEffects = new List<CardEffect>();
				}
				m_allEffects.Add(cardEffect);
				if (ShouldPreloadCardAssets())
				{
					cardEffect.LoadAll();
				}
			}
			effects.Add(cardEffect);
		}
	}

	private void InitEffectListList(List<List<CardEffectDef>> effectDefs, ref List<List<CardEffect>> effects)
	{
		if (effects != null)
		{
			for (int i = 0; i < effects.Count; i++)
			{
				List<CardEffect> effects2 = effects[i];
				DestroyCardEffectList(ref effects2);
			}
			effects = null;
		}
		if (effectDefs != null)
		{
			effects = new List<List<CardEffect>>();
			for (int j = 0; j < effectDefs.Count; j++)
			{
				List<CardEffect> effects3 = effects[j];
				InitEffectList(effectDefs[j], ref effects3);
			}
		}
	}

	private void InitSound(string path, ref CardSound cardSound, bool alwaysValid)
	{
		DestroyCardSound(ref cardSound);
		if (!string.IsNullOrEmpty(path))
		{
			cardSound = new CardSound(path, this, alwaysValid);
			if (ShouldPreloadCardAssets())
			{
				cardSound.GetSound();
			}
		}
	}

	private void InitEmoteList()
	{
		DestroyEmoteList();
		if (m_cardDef.CardDef.m_EmoteDefs == null)
		{
			return;
		}
		m_emotes = new List<EmoteEntry>();
		for (int i = 0; i < m_cardDef.CardDef.m_EmoteDefs.Count; i++)
		{
			EmoteEntryDef emoteEntryDef = m_cardDef.CardDef.m_EmoteDefs[i];
			EmoteEntry emoteEntry = new EmoteEntry(emoteEntryDef.m_emoteType, emoteEntryDef.m_emoteSpellPath, emoteEntryDef.m_emoteSoundSpellPath, emoteEntryDef.m_emoteGameStringKey, this);
			if (ShouldPreloadCardAssets())
			{
				emoteEntry.GetSoundSpell();
				emoteEntry.GetSpell();
			}
			m_emotes.Add(emoteEntry);
		}
	}

	private Spell SetupOverrideSpell(Spell existingSpell, Spell spell)
	{
		if (existingSpell != null)
		{
			if (existingSpell.IsActive())
			{
				Log.Gameplay.PrintError("destroying active spell {0} currently in state {1} with source card {2}.", existingSpell, existingSpell.GetActiveState(), existingSpell.GetSourceCard());
			}
			UnityEngine.Object.Destroy(existingSpell.gameObject);
		}
		SpellUtils.SetupSpell(spell, this);
		return spell;
	}

	private void ReleaseAssets()
	{
		ReleaseCardDef();
		DestroyCardDefAssets();
	}

	private void ReleaseCardDef()
	{
		m_cardDef?.Dispose();
		m_cardDef = null;
	}

	private void DestroyCardDefAssets()
	{
		DestroyCardEffect(ref m_playEffect);
		DestroyCardEffect(ref m_attackEffect);
		DestroyCardEffect(ref m_deathEffect);
		DestroyCardEffect(ref m_lifetimeEffect);
		DestroyCardEffectList(ref m_subOptionEffects);
		DestroyCardEffectList(ref m_triggerEffects);
		DestroyCardEffectList(ref m_resetGameEffects);
		foreach (CardEffect value in m_spellTableOverrideEffects.Values)
		{
			value.Clear();
		}
		m_spellTableOverrideEffects.Clear();
		if (m_proxyEffects != null)
		{
			List<CardEffect> effects = new List<CardEffect>(m_proxyEffects.Values);
			DestroyCardEffectList(ref effects);
			m_proxyEffects.Clear();
		}
		DestroyCardEffect(ref m_customKeywordEffect);
		DestroyCardEffect(ref m_customChoiceRevealEffect);
		DestroyCardEffect(ref m_customChoiceConcealEffect);
		for (int i = 0; i < m_announcerLine.Count(); i++)
		{
			DestroyCardSound(ref m_announcerLine[i]);
		}
		DestroyEmoteList();
		DestroyCardAsset(ref m_customSummonSpell);
		DestroyCardAsset(ref m_customSpawnSpell);
		DestroyCardAsset(ref m_customSpawnSpellOverride);
		DestroyCardAsset(ref m_customDeathSpell);
		DestroyCardAsset(ref m_customDeathSpellOverride);
		DestroyCardAsset(ref m_customDiscardSpell);
		DestroyCardAsset(ref m_customDiscardSpellOverride);
	}

	public void DestroyCardDefAssetsOnEntityChanged()
	{
		DeactivateLifetimeEffects();
		DestroyCardAsset(ref m_customDeathSpell);
		DestroyCardEffect(ref m_lifetimeEffect);
	}

	private void DestroyCardEffect(ref CardEffect effect)
	{
		if (effect != null)
		{
			effect.PurgeSpells();
			effect = null;
		}
	}

	private void DestroyCardSound(ref CardSound cardSound)
	{
		if (cardSound != null)
		{
			cardSound.Clear();
			cardSound = null;
		}
	}

	private void DestroyCardEffectList(ref List<CardEffect> effects)
	{
		if (effects == null)
		{
			return;
		}
		foreach (CardEffect effect in effects)
		{
			effect.PurgeSpells();
		}
		effects = null;
	}

	private void DestroyCardAsset<T>(ref T asset) where T : Component
	{
		if (!((UnityEngine.Object)asset == (UnityEngine.Object)null))
		{
			UnityEngine.Object.Destroy(asset.gameObject);
			asset = null;
		}
	}

	private void DestroyCardAsset<T>(T asset) where T : Component
	{
		if (!((UnityEngine.Object)asset == (UnityEngine.Object)null))
		{
			UnityEngine.Object.Destroy(asset.gameObject);
		}
	}

	private void DestroySpellList<T>(List<T> spells) where T : Spell
	{
		if (spells != null)
		{
			for (int i = 0; i < spells.Count; i++)
			{
				DestroyCardAsset(spells[i]);
			}
			spells = null;
		}
	}

	private void DestroyEmoteList()
	{
		if (m_emotes != null)
		{
			for (int i = 0; i < m_emotes.Count; i++)
			{
				m_emotes[i].Clear();
			}
			m_emotes = null;
		}
	}

	public void CancelActiveSpells()
	{
		SpellUtils.ActivateCancelIfNecessary(GetPlaySpell(0, loadIfNeeded: false));
		if (m_subOptionEffects != null)
		{
			foreach (CardEffect subOptionEffect in m_subOptionEffects)
			{
				SpellUtils.ActivateCancelIfNecessary(subOptionEffect.GetSpell(loadIfNeeded: false));
			}
		}
		if (m_triggerEffects == null)
		{
			return;
		}
		foreach (CardEffect triggerEffect in m_triggerEffects)
		{
			SpellUtils.ActivateCancelIfNecessary(triggerEffect.GetSpell(loadIfNeeded: false));
		}
	}

	public void CancelCustomSpells()
	{
		SpellUtils.ActivateCancelIfNecessary(m_customSummonSpell);
		SpellUtils.ActivateCancelIfNecessary(m_customSpawnSpell);
		SpellUtils.ActivateCancelIfNecessary(m_customSpawnSpellOverride);
		SpellUtils.ActivateCancelIfNecessary(m_customDeathSpell);
		SpellUtils.ActivateCancelIfNecessary(m_customDeathSpellOverride);
		SpellUtils.ActivateCancelIfNecessary(m_customDiscardSpell);
		SpellUtils.ActivateCancelIfNecessary(m_customDiscardSpellOverride);
	}

	private IEnumerator WaitThenActivateSoundSpell(CardSoundSpell soundSpell)
	{
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		while (gameEntity.GetGameOptions().GetBooleanOption(GameEntityOption.DELAY_CARD_SOUND_SPELLS))
		{
			yield return null;
		}
		soundSpell.Reactivate();
	}

	public void OnTagsChanged(TagDeltaList changeList, bool fromShowEntity)
	{
		bool flag = false;
		for (int i = 0; i < changeList.Count; i++)
		{
			TagDelta tagDelta = changeList[i];
			switch (tagDelta.tag)
			{
			case 45:
			case 47:
			case 48:
			case 187:
			case 292:
			case 917:
			case 920:
			case 1046:
				flag = true;
				break;
			default:
				OnTagChanged(tagDelta, fromShowEntity);
				break;
			}
		}
		if (flag && !m_entity.IsLoadingAssets() && IsActorReady())
		{
			UpdateActorComponents();
		}
	}

	public void OnMetaData(Network.HistMetaData metaData)
	{
		if ((metaData.MetaType != HistoryMeta.Type.DAMAGE && metaData.MetaType != HistoryMeta.Type.HEALING && metaData.MetaType != HistoryMeta.Type.POISONOUS) || !CanShowActorVisuals() || m_entity.GetZone() != TAG_ZONE.PLAY)
		{
			return;
		}
		Spell actorSpell = GetActorSpell(SpellType.DAMAGE);
		if (actorSpell == null)
		{
			UpdateActorComponents();
			return;
		}
		actorSpell.AddFinishedCallback(OnSpellFinished_UpdateActorComponents);
		if (m_entity.IsCharacter())
		{
			int damage = ((metaData.MetaType == HistoryMeta.Type.HEALING) ? (-metaData.Data) : metaData.Data);
			DamageSplatSpell damageSplatSpell = (DamageSplatSpell)actorSpell;
			damageSplatSpell.SetDamage(damage);
			if (metaData.MetaType == HistoryMeta.Type.POISONOUS)
			{
				if (damageSplatSpell.IsPoisonous())
				{
					return;
				}
				damageSplatSpell.SetPoisonous(isPoisonous: true);
			}
			else
			{
				damageSplatSpell.SetPoisonous(isPoisonous: false);
			}
			actorSpell.ActivateState(SpellStateType.ACTION);
			BoardEvents boardEvents = BoardEvents.Get();
			if (boardEvents != null)
			{
				if (metaData.MetaType == HistoryMeta.Type.HEALING)
				{
					boardEvents.HealEvent(this, -metaData.Data);
				}
				else
				{
					boardEvents.DamageEvent(this, metaData.Data);
				}
			}
		}
		else
		{
			actorSpell.Activate();
		}
	}

	public void HandleCardExhaustedTagChanged(TagDelta change)
	{
		if (m_entity.IsSecret())
		{
			if (!CanShowSecretActorVisuals())
			{
				return;
			}
		}
		else if (!CanShowActorVisuals())
		{
			return;
		}
		if (m_entity.IsHeroPower() && m_entity.GetController() != null && m_entity.GetController().GetTag(GAME_TAG.HERO_POWER_DISABLED) != 0)
		{
			change.newValue = 1;
		}
		if (change.newValue != change.oldValue)
		{
			if (GameState.Get().IsTurnStartManagerActive() && m_entity.IsControlledByFriendlySidePlayer())
			{
				TurnStartManager.Get().NotifyOfExhaustedChange(this, change);
			}
			else
			{
				ShowExhaustedChange(change.newValue);
			}
		}
	}

	public void OnTagChanged(TagDelta change, bool fromShowEntity)
	{
		if (TagVisualConfiguration.Get() != null)
		{
			TagVisualConfiguration.Get().ProcessTagChange((GAME_TAG)change.tag, this, fromShowEntity, change);
		}
		_ = change.tag;
		m_entity.GetCardTextBuilder().OnTagChange(this, change);
		if (m_actor != null)
		{
			m_actor.UpdateDiamondCardArt();
		}
	}

	public void ActivateDormantStateVisual()
	{
		m_actor.ActivateSpellBirthState(SpellType.DORMANT);
		if (m_entity.IsFrozen())
		{
			m_actor.ActivateSpellDeathState(SpellType.FROZEN);
		}
		if (m_entity.IsSilenced())
		{
			m_actor.ActivateSpellDeathState(SpellType.SILENCE);
		}
		DeactivateLifetimeEffects();
	}

	public void DeactivateDormantStateVisual()
	{
		m_actor.ActivateSpellDeathState(SpellType.DORMANT);
		if (m_entity.IsFrozen())
		{
			m_actor.ActivateSpellBirthState(SpellType.FROZEN);
		}
		if (m_entity.IsSilenced())
		{
			m_actor.ActivateSpellBirthState(SpellType.SILENCE);
		}
		ActivateLifetimeEffects();
		ActivateActorSpell(SpellType.AWAKEN_FROM_DORMANT);
		if (m_entity.IsControlledByFriendlySidePlayer())
		{
			if (m_entity.GetRealTimeSpellpower() > 0 || m_entity.GetRealTimeSpellpowerDouble())
			{
				ZoneMgr.Get().OnSpellPowerEntityEnteredPlay(m_entity.GetSpellPowerSchool());
			}
			if (m_entity.GetRealTimeHealingDoeDamageHint())
			{
				ZoneMgr.Get().OnHealingDoesDamageEntityEnteredPlay();
			}
			if (m_entity.GetRealTimeLifestealDoesDamageHint())
			{
				ZoneMgr.Get().OnLifestealDoesDamageEntityEnteredPlay();
			}
		}
		if (m_entity.IsAsleep())
		{
			m_actor.ActivateSpellBirthState(SpellType.Zzz);
		}
	}

	public void UpdateQuestUI()
	{
		if (m_entity != null && m_entity.IsQuest() && !(m_actor == null))
		{
			QuestController component = m_actor.GetComponent<QuestController>();
			if (component == null)
			{
				Log.Gameplay.PrintError("Quest card {0} does not have a QuestController component.", this);
			}
			else
			{
				component.UpdateQuestUI();
			}
		}
	}

	public void UpdateSideQuestUI(bool allowQuestComplete)
	{
		if (m_entity != null && m_entity.IsSideQuest() && !(m_actor == null))
		{
			SideQuestController component = m_actor.GetComponent<SideQuestController>();
			if (component == null)
			{
				Log.Gameplay.PrintError("SideQuest card {0} does not have a SideQuestController component.", this);
			}
			else
			{
				component.UpdateQuestUI(allowQuestComplete);
			}
		}
	}

	public void UpdatePuzzleUI()
	{
		if (m_entity != null && m_entity.IsPuzzle() && !(m_actor == null))
		{
			PuzzleController component = m_actor.GetComponent<PuzzleController>();
			if (component == null)
			{
				Log.Gameplay.PrintError("Puzzle card {0} does not have a PuzzleController component.", this);
			}
			else
			{
				component.UpdatePuzzleUI();
			}
		}
	}

	public void UpdateCardCostHealth(TagDelta change)
	{
		if (m_entity.IsControlledByFriendlySidePlayer())
		{
			Card mousedOverCard = InputManager.Get().GetMousedOverCard();
			if (mousedOverCard != null)
			{
				Entity entity = mousedOverCard.GetEntity();
				if (entity == m_entity)
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
		if (CanShowActorVisuals() && change.newValue > 0)
		{
			m_actor.ActivateSpellBirthState(SpellType.SPELLS_COST_HEALTH);
		}
		else
		{
			m_actor.ActivateSpellDeathState(SpellType.SPELLS_COST_HEALTH);
		}
	}

	public bool CanShowActorVisuals()
	{
		if (m_entity.IsLoadingAssets())
		{
			return false;
		}
		if (m_actor == null)
		{
			return false;
		}
		if (!m_actor.IsShown())
		{
			return false;
		}
		return true;
	}

	private bool CanShowSecretActorVisuals()
	{
		if (m_entity.IsLoadingAssets())
		{
			return false;
		}
		if (m_actor == null)
		{
			return false;
		}
		if (m_actorReady && !m_actor.IsShown())
		{
			return false;
		}
		return true;
	}

	public bool ShouldShowImmuneVisuals()
	{
		if (m_entity != null && m_entity.HasTag(GAME_TAG.IMMUNE))
		{
			return !m_entity.HasTag(GAME_TAG.DONT_SHOW_IMMUNE);
		}
		return false;
	}

	public void ActivateStateSpells(bool forceActivate = false)
	{
		if (m_actor == null || (m_entity.GetController() != null && !m_entity.GetController().IsFriendlySide() && m_entity.IsObfuscated()))
		{
			return;
		}
		if (m_entity != null && m_entity.IsHeroPower())
		{
			UpdateHeroPowerRelatedVisual();
		}
		TagVisualConfiguration.Get().ActivateStateSpells(this);
		TAG_ZONE tAG_ZONE = ((GetZone() != null) ? GetZone().m_ServerTag : TAG_ZONE.SETASIDE);
		if (tAG_ZONE == TAG_ZONE.HAND)
		{
			ActivateHandStateSpells(forceActivate);
		}
		else if (m_entity != null && (tAG_ZONE == TAG_ZONE.PLAY || tAG_ZONE == TAG_ZONE.SECRET))
		{
			bool exhausted = m_entity.IsExhausted();
			if (m_entity.IsHeroPower() && m_entity.GetController() != null && m_entity.GetController().HasTag(GAME_TAG.HERO_POWER_DISABLED))
			{
				exhausted = true;
			}
			ShowExhaustedChange(exhausted);
		}
	}

	public void UpdateHeroPowerRelatedVisual()
	{
		if (!m_entity.IsHeroPower())
		{
			return;
		}
		Player controller = m_entity.GetController();
		if (controller != null)
		{
			if (controller.HasTag(GAME_TAG.STEADY_SHOT_CAN_TARGET) && m_entity.GetClasses().Contains(TAG_CLASS.HUNTER))
			{
				m_actor.ActivateSpellBirthState(SpellType.STEADY_SHOT_CAN_TARGET);
			}
			else
			{
				m_actor.ActivateSpellDeathState(SpellType.STEADY_SHOT_CAN_TARGET);
			}
			if (controller.HasTag(GAME_TAG.CURRENT_HEROPOWER_DAMAGE_BONUS) && controller.IsHeroPowerAffectedByBonusDamage())
			{
				m_actor.ActivateSpellBirthState(SpellType.CURRENT_HEROPOWER_DAMAGE_BONUS);
			}
			else
			{
				m_actor.ActivateSpellDeathState(SpellType.CURRENT_HEROPOWER_DAMAGE_BONUS);
			}
		}
	}

	public void ActivateHandStateSpells(bool forceActivate = false)
	{
		m_entity.GetController();
		if ((m_entity.IsHeroPowerOrGameModeButton() || m_entity.IsSpell()) && m_playEffect != null)
		{
			SpellUtils.ActivateCancelIfNecessary(m_playEffect.GetSpell(loadIfNeeded: false));
		}
		if (m_entity.IsSpell())
		{
			SpellUtils.ActivateCancelIfNecessary(GetActorSpell(SpellType.POWER_UP, loadIfNeeded: false));
		}
		if (TagVisualConfiguration.Get() != null)
		{
			TagVisualConfiguration.Get().ActivateHandStateSpells(this, forceActivate);
		}
	}

	public void DeactivateHandStateSpells(Actor actor = null)
	{
		if (actor == null)
		{
			if (m_actor == null)
			{
				return;
			}
			actor = m_actor;
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

	public void ActivateActorArmsDealingSpell()
	{
		if (CardStandInIsInteractive())
		{
			PowerTaskList currentTaskList = GameState.Get().GetPowerProcessor().GetCurrentTaskList();
			if (currentTaskList != null && currentTaskList.IsBlock())
			{
				StartCoroutine(WaitPowerTaskListAndActivateArmsDealing(currentTaskList));
			}
			else
			{
				m_actor.ActivateSpellBirthState(SpellType.ARMS_DEALING);
			}
		}
		else
		{
			Spell spell = m_actor.GetSpell(SpellType.ARMS_DEALING);
			if (spell != null)
			{
				spell.ActivateState(SpellStateType.IDLE);
			}
		}
	}

	private IEnumerator WaitPowerTaskListAndActivateArmsDealing(PowerTaskList curPowerTaskList)
	{
		while (!curPowerTaskList.IsComplete())
		{
			yield return null;
		}
		if (GetZone() is ZoneHand)
		{
			m_actor.ActivateSpellBirthState(SpellType.ARMS_DEALING);
		}
	}

	public void ToggleDeathrattle(bool on)
	{
		if (on)
		{
			m_actor.ActivateSpellBirthState(SpellType.DEATHRATTLE_IDLE);
		}
		else
		{
			m_actor.ActivateSpellDeathState(SpellType.DEATHRATTLE_IDLE);
		}
	}

	public void UpdateBauble()
	{
		if (!IsBaubleAnimating())
		{
			DeactivateBaubles();
			SpellType prioritizedBaubleSpellType = m_entity.GetPrioritizedBaubleSpellType();
			if (prioritizedBaubleSpellType != 0 && m_actor != null)
			{
				m_actor.ActivateSpellBirthState(prioritizedBaubleSpellType);
			}
		}
	}

	public void DeactivateBaubles()
	{
		SpellType prioritizedBaubleSpellType = m_entity.GetPrioritizedBaubleSpellType();
		SpellType[] array = new SpellType[8]
		{
			SpellType.TRIGGER,
			SpellType.POISONOUS,
			SpellType.POISONOUS_INSTANT,
			SpellType.INSPIRE,
			SpellType.LIFESTEAL,
			SpellType.OVERKILL,
			SpellType.SPELLBURST,
			SpellType.FRENZY
		};
		foreach (SpellType spellType in array)
		{
			if (prioritizedBaubleSpellType != spellType)
			{
				SpellUtils.ActivateDeathIfNecessary(GetActorSpell(spellType, loadIfNeeded: false));
			}
		}
	}

	public bool IsBaubleAnimating()
	{
		return m_isBaubleAnimating;
	}

	public void SetIsBaubleAnimating(bool isAnimating)
	{
		m_isBaubleAnimating = isAnimating;
	}

	public void ShowExhaustedChange(int val)
	{
		bool exhausted = val == 1;
		ShowExhaustedChange(exhausted);
	}

	public void ShowExhaustedChange(bool exhausted)
	{
		if (m_entity.IsHeroPower())
		{
			StopCoroutine("PlayHeroPowerAnimation");
			StartCoroutine("PlayHeroPowerAnimation", exhausted);
		}
		else if (m_entity.IsWeapon())
		{
			if (exhausted)
			{
				SheatheWeapon();
			}
			else
			{
				UnSheatheWeapon();
			}
		}
		else if (m_entity.IsSecret())
		{
			StartCoroutine(ShowSecretExhaustedChange(exhausted));
		}
	}

	public void DisableHeroPowerFlipSoundOnce()
	{
		m_disableHeroPowerFlipSoundOnce = true;
	}

	private IEnumerator PlayHeroPowerAnimation(bool exhausted)
	{
		string animationName;
		if (exhausted)
		{
			animationName = (UniversalInputManager.UsePhoneUI ? "HeroPower_Used_phone" : "HeroPower_Used");
			if (m_actor != null && m_actor.UseCoinManaGem())
			{
				Spell spellIfLoaded = m_actor.GetSpellIfLoaded(SpellType.COIN_MANA_GEM);
				if (spellIfLoaded != null)
				{
					spellIfLoaded.Deactivate();
				}
			}
		}
		else
		{
			animationName = (UniversalInputManager.UsePhoneUI ? "HeroPower_Restore_phone" : "HeroPower_Restore");
			if (m_actor != null && m_actor.UseCoinManaGem())
			{
				Spell spellIfLoaded2 = m_actor.GetSpellIfLoaded(SpellType.COIN_MANA_GEM);
				if (spellIfLoaded2 != null)
				{
					spellIfLoaded2.Reactivate();
				}
			}
		}
		SetInputEnabled(enabled: false);
		MinionShake shake = m_actor.gameObject.GetComponentInChildren<MinionShake>();
		if (shake == null)
		{
			yield break;
		}
		while (shake.isShaking())
		{
			yield return null;
		}
		while (m_actor.gameObject.transform.parent != base.transform)
		{
			yield return null;
		}
		if (m_disableHeroPowerFlipSoundOnce)
		{
			m_disableHeroPowerFlipSoundOnce = false;
		}
		else
		{
			string text = (exhausted ? "hero_power_icon_flip_off.prefab:621ead6ff672f5b4bbfd6578ee217a42" : "hero_power_icon_flip_on.prefab:e1491b367801f6b4395dc63ce0b08f0a");
			SoundManager.Get().LoadAndPlay(text);
		}
		m_actor.GetComponent<Animation>().Play(animationName);
		Spell spell = GetPlaySpell(0);
		if (spell != null)
		{
			while (spell.GetActiveState() != 0)
			{
				yield return null;
			}
		}
		SetInputEnabled(enabled: true);
		if (animationName.Contains("Used") && GameState.Get().IsValidOption(m_entity) && !m_entity.HasSubCards() && spell != null)
		{
			SetInputEnabled(enabled: false);
		}
	}

	private void SheatheWeapon()
	{
		if (GetZone() is ZoneWeapon)
		{
			m_actor.GetAttackObject().ScaleToZero();
			ActivateActorSpell(SpellType.SHEATHE);
		}
		else if (!(GetZone() == null) && !(GetZone() is ZoneGraveyard))
		{
			Log.Gameplay.PrintError("Failed to process Card.SheatheWeapon() card:{0} zone:{1}", this, GetZone());
		}
	}

	private void UnSheatheWeapon()
	{
		if (GetZone() is ZoneWeapon)
		{
			m_actor.GetAttackObject().Enlarge(1f);
			ActivateActorSpell(SpellType.UNSHEATHE);
		}
		else if (!(GetZone() == null) && !(GetZone() is ZoneGraveyard))
		{
			Log.Gameplay.PrintError("Failed to process Card.UnSheatheWeapon() card:{0} zone:{1}", this, GetZone());
		}
	}

	private IEnumerator ShowSecretExhaustedChange(bool exhausted)
	{
		while (!m_actorReady)
		{
			yield return null;
		}
		if (m_entity.IsDarkWandererSecret())
		{
			yield break;
		}
		Spell spell = m_actor.GetComponent<Spell>();
		while (spell.GetActiveState() != 0)
		{
			yield return null;
		}
		if (CanShowSecretZoneCard())
		{
			if (exhausted)
			{
				SheatheSecret(spell);
			}
			else
			{
				UnSheatheSecret(spell);
			}
		}
	}

	private void SheatheSecret(Spell spell)
	{
		if (!m_secretSheathed && m_entity.IsExhausted())
		{
			m_secretSheathed = true;
			spell.ActivateState(SpellStateType.IDLE);
		}
	}

	private void UnSheatheSecret(Spell spell)
	{
		if (m_secretSheathed && !m_entity.IsExhausted())
		{
			m_secretSheathed = false;
			spell.ActivateState(SpellStateType.DEATH);
		}
	}

	public void OnEnchantmentAdded(int oldEnchantmentCount, Entity enchantment)
	{
		if (CanShowActorVisuals() && IsActorReady())
		{
			UpdateBauble();
		}
		Spell spell = null;
		if (GameState.Get() != null && GameState.Get().GetGameEntity() != null && GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_ENCHANTMENT_SPARKLES))
		{
			switch (enchantment.GetEnchantmentBirthVisual())
			{
			case TAG_ENCHANTMENT_VISUAL.POSITIVE:
				spell = GetActorSpell(SpellType.ENCHANT_POSITIVE);
				break;
			case TAG_ENCHANTMENT_VISUAL.NEGATIVE:
				spell = GetActorSpell(SpellType.ENCHANT_NEGATIVE);
				break;
			case TAG_ENCHANTMENT_VISUAL.NEUTRAL:
				spell = GetActorSpell(SpellType.ENCHANT_NEUTRAL);
				break;
			}
		}
		if (spell == null)
		{
			UpdateEnchantments();
			UpdateTooltip();
		}
		else
		{
			spell.AddStateFinishedCallback(OnEnchantmentSpellStateFinished);
			spell.ActivateState(SpellStateType.BIRTH);
		}
	}

	public void OnEnchantmentRemoved(int oldEnchantmentCount, Entity enchantment)
	{
		if (CanShowActorVisuals())
		{
			UpdateBauble();
		}
		Spell spell = null;
		if (GameState.Get() != null && GameState.Get().GetGameEntity() != null && GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_ENCHANTMENT_SPARKLES))
		{
			switch (enchantment.GetEnchantmentBirthVisual())
			{
			case TAG_ENCHANTMENT_VISUAL.POSITIVE:
				spell = GetActorSpell(SpellType.ENCHANT_POSITIVE);
				break;
			case TAG_ENCHANTMENT_VISUAL.NEGATIVE:
				spell = GetActorSpell(SpellType.ENCHANT_NEGATIVE);
				break;
			case TAG_ENCHANTMENT_VISUAL.NEUTRAL:
				spell = GetActorSpell(SpellType.ENCHANT_NEUTRAL);
				break;
			}
		}
		if (spell == null)
		{
			UpdateEnchantments();
			UpdateTooltip();
		}
		else
		{
			spell.AddStateFinishedCallback(OnEnchantmentSpellStateFinished);
			spell.ActivateState(SpellStateType.DEATH);
		}
	}

	private void OnEnchantmentSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (prevStateType == SpellStateType.BIRTH || prevStateType == SpellStateType.DEATH)
		{
			spell.RemoveStateFinishedCallback(OnEnchantmentSpellStateFinished);
			UpdateEnchantments();
			UpdateTooltip();
		}
	}

	public void UpdateEnchantments()
	{
		if (GameState.Get() != null && GameState.Get().GetGameEntity() != null && !GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_ENCHANTMENT_SPARKLES))
		{
			return;
		}
		List<Entity> enchantments = m_entity.GetEnchantments();
		Spell actorSpell = GetActorSpell(SpellType.ENCHANT_POSITIVE);
		Spell actorSpell2 = GetActorSpell(SpellType.ENCHANT_NEGATIVE);
		Spell actorSpell3 = GetActorSpell(SpellType.ENCHANT_NEUTRAL);
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
		foreach (Entity item in enchantments)
		{
			TAG_ENCHANTMENT_VISUAL enchantmentIdleVisual = item.GetEnchantmentIdleVisual();
			switch (enchantmentIdleVisual)
			{
			case TAG_ENCHANTMENT_VISUAL.POSITIVE:
				num++;
				break;
			case TAG_ENCHANTMENT_VISUAL.NEGATIVE:
				num--;
				break;
			}
			if (enchantmentIdleVisual != 0)
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

	public Spell GetActorSpell(SpellType spellType, bool loadIfNeeded = true)
	{
		if (m_actor == null)
		{
			return null;
		}
		if (loadIfNeeded)
		{
			return m_actor.GetSpell(spellType);
		}
		return m_actor.GetSpellIfLoaded(spellType);
	}

	public Spell ActivateActorSpell(SpellType spellType)
	{
		return ActivateActorSpell(m_actor, spellType, null, null);
	}

	public Spell ActivateActorSpell(SpellType spellType, Spell.FinishedCallback finishedCallback)
	{
		return ActivateActorSpell(m_actor, spellType, finishedCallback, null);
	}

	public Spell ActivateActorSpell(SpellType spellType, Spell.FinishedCallback finishedCallback, Spell.StateFinishedCallback stateFinishedCallback)
	{
		return ActivateActorSpell(m_actor, spellType, finishedCallback, stateFinishedCallback);
	}

	private Spell ActivateActorSpell(Actor actor, SpellType spellType)
	{
		return ActivateActorSpell(actor, spellType, null, null);
	}

	private Spell ActivateActorSpell(Actor actor, SpellType spellType, Spell.FinishedCallback finishedCallback)
	{
		return ActivateActorSpell(actor, spellType, finishedCallback, null);
	}

	private Spell ActivateActorSpell(Actor actor, SpellType spellType, Spell.FinishedCallback finishedCallback, Spell.StateFinishedCallback stateFinishedCallback)
	{
		if (actor == null)
		{
			Log.Gameplay.Print($"{this}.ActivateActorSpell() - actor IS NULL spellType={spellType}");
			return null;
		}
		Spell spell = actor.GetSpell(spellType);
		if (spell == null)
		{
			Log.Gameplay.Print($"{this}.ActivateActorSpell() - spell IS NULL actor={actor} spellType={spellType}");
			return null;
		}
		ActivateSpell(spell, finishedCallback, stateFinishedCallback);
		return spell;
	}

	private void ActivateSpell(Spell spell, Spell.FinishedCallback finishedCallback)
	{
		ActivateSpell(spell, finishedCallback, null, null, null);
	}

	private void ActivateSpell(Spell spell, Spell.FinishedCallback finishedCallback, Spell.StateFinishedCallback stateFinishedCallback)
	{
		ActivateSpell(spell, finishedCallback, null, stateFinishedCallback, null);
	}

	private void ActivateSpell(Spell spell, Spell.FinishedCallback finishedCallback, object finishedUserData, Spell.StateFinishedCallback stateFinishedCallback)
	{
		ActivateSpell(spell, finishedCallback, finishedUserData, stateFinishedCallback, null);
	}

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

	public Spell GetActorAttackSpellForInput()
	{
		if (m_actor == null)
		{
			Log.Gameplay.Print("{0}.GetActorAttackSpellForInput() - m_actor IS NULL", this);
			return null;
		}
		if (m_zone == null)
		{
			Log.Gameplay.Print("{0}.GetActorAttackSpellForInput() - m_zone IS NULL", this);
			return null;
		}
		Spell spell = m_actor.GetSpell(SpellType.FRIENDLY_ATTACK);
		if (spell == null)
		{
			Log.Gameplay.Print("{0}.GetActorAttackSpellForInput() - {1} spell is null", this, SpellType.FRIENDLY_ATTACK);
			return null;
		}
		return spell;
	}

	public void FakeDeath()
	{
		if (!m_suppressKeywordDeaths)
		{
			StartCoroutine(WaitAndPrepareForDeathAnimation(m_actor));
		}
		ActivateDeathSpell(m_actor);
	}

	private Spell ActivateDeathSpell(Actor actor)
	{
		bool standard;
		Spell bestDeathSpell = GetBestDeathSpell(actor, out standard);
		if (bestDeathSpell == null)
		{
			Debug.LogError($"{this}.ActivateDeathSpell() - {SpellType.DEATH} is null");
			return null;
		}
		CleanUpCustomSpell(bestDeathSpell, ref m_customDeathSpell);
		CleanUpCustomSpell(bestDeathSpell, ref m_customDeathSpellOverride);
		m_activeDeathEffectCount++;
		if (standard)
		{
			if (m_actor != actor)
			{
				bestDeathSpell.AddStateFinishedCallback(OnSpellStateFinished_DestroyActor);
			}
		}
		else
		{
			bestDeathSpell.SetSource(base.gameObject);
			if (m_actor != actor)
			{
				bestDeathSpell.AddStateFinishedCallback(OnSpellStateFinished_CustomDeath);
			}
			SpellUtils.SetCustomSpellParent(bestDeathSpell, actor);
		}
		bestDeathSpell.AddFinishedCallback(OnSpellFinished_Death);
		bestDeathSpell.Activate();
		BoardEvents boardEvents = BoardEvents.Get();
		if (boardEvents != null)
		{
			boardEvents.DeathEvent(this);
		}
		return bestDeathSpell;
	}

	private Spell ActivateHandSpawnSpell()
	{
		if (m_customSpawnSpellOverride == null)
		{
			return ActivateDefaultSpawnSpell(OnSpellFinished_DefaultHandSpawn);
		}
		Entity creator = m_entity.GetCreator();
		Card card = null;
		if (creator != null && creator.IsMinion())
		{
			card = creator.GetCard();
		}
		if (card != null)
		{
			TransformUtil.CopyWorld(base.transform, card.transform);
		}
		ActivateCustomHandSpawnSpell(m_customSpawnSpellOverride, card);
		return m_customSpawnSpellOverride;
	}

	private void ActivatePlaySpawnEffects_HeroPowerOrWeapon()
	{
		Spell spell = m_customSpawnSpellOverride;
		if (spell == null)
		{
			spell = m_customSpawnSpell;
			if (spell == null)
			{
				ActivateDefaultSpawnSpell(OnSpellFinished_DefaultPlaySpawn);
				return;
			}
		}
		if (m_zone is ZoneHeroPower)
		{
			m_actor.Hide();
		}
		ActivateCustomSpawnSpell(spell);
	}

	private Spell ActivateDefaultSpawnSpell(Spell.FinishedCallback finishedCallback)
	{
		m_inputEnabled = false;
		m_actor.ToggleForceIdle(bOn: true);
		int num = m_entity.GetTag(GAME_TAG.PREMIUM);
		SpellType spellType = SpellType.SUMMON_IN;
		if (num == 2)
		{
			spellType = SpellType.SUMMON_IN_DIAMOND;
		}
		if (m_zone is ZoneHand && m_entity.HasTag(GAME_TAG.GHOSTLY))
		{
			spellType = SpellType.GHOSTLY_SUMMON_IN;
		}
		else if (m_zone is ZoneHand && m_entity.HasTag(GAME_TAG.CREATOR))
		{
			Entity entity = GameState.Get().GetEntity(m_entity.GetTag(GAME_TAG.CREATOR));
			if (entity != null && entity.HasTag(GAME_TAG.TWINSPELL) && entity.GetTag(GAME_TAG.TWINSPELL_COPY) == GameUtils.TranslateCardIdToDbId(m_entity.GetCardId()))
			{
				spellType = ((GameState.Get().GetGameEntity().GetTag(GAME_TAG.BACON_USE_FAST_ANIMATIONS) > 0) ? SpellType.TWINSPELL_SUMMON_IN_FAST : SpellType.TWINSPELL_SUMMON_IN);
			}
		}
		else if (m_entity.IsWeapon() && (m_zone is ZoneWeapon || m_zone is ZoneHeroPower))
		{
			spellType = (m_entity.IsControlledByFriendlySidePlayer() ? SpellType.SUMMON_IN_FRIENDLY : SpellType.SUMMON_IN_OPPONENT);
		}
		Spell spell = ActivateActorSpell(spellType, finishedCallback);
		if (spell == null)
		{
			Debug.LogError($"{this}.ActivateDefaultSpawnSpell() - {spellType} is null");
			return null;
		}
		return spell;
	}

	private void ActivateCustomSpawnSpell(Spell spell)
	{
		spell.SetSource(base.gameObject);
		spell.RemoveAllTargets();
		spell.AddTarget(base.gameObject);
		spell.AddStateFinishedCallback(OnSpellStateFinished_DestroySpell);
		SpellUtils.SetCustomSpellParent(spell, m_actor);
		spell.AddFinishedCallback(OnSpellFinished_CustomPlaySpawn);
		spell.Activate();
	}

	private void ActivateCustomHandSpawnSpell(Spell spell, Card creatorCard)
	{
		GameObject source = ((creatorCard == null) ? base.gameObject : creatorCard.gameObject);
		spell.SetSource(source);
		spell.RemoveAllTargets();
		spell.AddTarget(base.gameObject);
		spell.AddStateFinishedCallback(OnSpellStateFinished_DestroySpell);
		SpellUtils.SetCustomSpellParent(spell, m_actor);
		spell.AddFinishedCallback(OnSpellFinished_CustomHandSpawn);
		spell.Activate();
	}

	private void ActivateMinionSpawnEffects()
	{
		Entity creator = m_entity.GetCreator();
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
		bool standard;
		Spell bestSpawnSpell = GetBestSpawnSpell(out standard);
		if (standard)
		{
			if (card == null)
			{
				ActivateStandardSpawnMinionSpell();
			}
			else
			{
				StartCoroutine(ActivateCreatorSpawnMinionSpell(creator, card));
			}
		}
		else
		{
			ActivateCustomSpawnMinionSpell(bestSpawnSpell, card);
		}
	}

	private IEnumerator ActivateCreatorSpawnMinionSpell(Entity creator, Card creatorCard)
	{
		while (creator.IsLoadingAssets() || !creatorCard.IsActorReady())
		{
			yield return 0;
		}
		if (creatorCard.ActivateCreatorSpawnMinionSpell() != null)
		{
			yield return new WaitForSeconds(0.9f);
		}
		ActivateStandardSpawnMinionSpell();
	}

	private Spell ActivateCreatorSpawnMinionSpell()
	{
		if (m_entity.IsControlledByFriendlySidePlayer())
		{
			return ActivateActorSpell(SpellType.FRIENDLY_SPAWN_MINION);
		}
		return ActivateActorSpell(SpellType.OPPONENT_SPAWN_MINION);
	}

	private void ActivateStandardSpawnMinionSpell()
	{
		if (m_entity.IsControlledByFriendlySidePlayer())
		{
			m_activeSpawnSpell = ActivateActorSpell(SpellType.FRIENDLY_SPAWN_MINION, OnSpellFinished_StandardSpawnCharacter);
		}
		else
		{
			m_activeSpawnSpell = ActivateActorSpell(SpellType.OPPONENT_SPAWN_MINION, OnSpellFinished_StandardSpawnCharacter);
		}
		ActivateCharacterPlayEffects();
	}

	private void ActivateStandardSpawnHeroSpell()
	{
		if (m_entity.IsControlledByFriendlySidePlayer())
		{
			m_activeSpawnSpell = ActivateActorSpell(SpellType.FRIENDLY_SPAWN_HERO, OnSpellFinished_StandardSpawnCharacter);
		}
		else
		{
			m_activeSpawnSpell = ActivateActorSpell(SpellType.OPPONENT_SPAWN_HERO, OnSpellFinished_StandardSpawnCharacter);
		}
	}

	private void ActivateCustomSpawnMinionSpell(Spell spell, Card creatorCard)
	{
		m_activeSpawnSpell = spell;
		GameObject source = ((creatorCard == null) ? base.gameObject : creatorCard.gameObject);
		spell.SetSource(source);
		spell.RemoveAllTargets();
		spell.AddTarget(base.gameObject);
		spell.AddStateFinishedCallback(OnSpellStateFinished_DestroySpell);
		SpellUtils.SetCustomSpellParent(spell, m_actor);
		spell.AddFinishedCallback(OnSpellFinished_CustomSpawnMinion);
		spell.Activate();
	}

	private IEnumerator ActivateReviveSpell()
	{
		while (m_activeDeathEffectCount > 0)
		{
			yield return 0;
		}
		ActivateStandardSpawnMinionSpell();
	}

	private IEnumerator ActivateActorBattlecrySpell()
	{
		Spell battlecrySpell = GetActorSpell(SpellType.BATTLECRY);
		if (battlecrySpell == null || !(m_zone is ZonePlay) || InputManager.Get() == null || InputManager.Get().GetBattlecrySourceCard() != this)
		{
			yield break;
		}
		yield return new WaitForSeconds(0.01f);
		if (!(InputManager.Get() == null) && !(InputManager.Get().GetBattlecrySourceCard() != this))
		{
			if (battlecrySpell.GetActiveState() == SpellStateType.NONE)
			{
				battlecrySpell.ActivateState(SpellStateType.BIRTH);
			}
			Spell playSpell = GetPlaySpell(0);
			if ((bool)playSpell)
			{
				playSpell.ActivateState(SpellStateType.BIRTH);
			}
		}
	}

	private void CleanUpCustomSpell(Spell chosenSpell, ref Spell customSpell)
	{
		if ((bool)customSpell)
		{
			if (chosenSpell == customSpell)
			{
				customSpell = null;
			}
			else
			{
				UnityEngine.Object.Destroy(customSpell.gameObject);
			}
		}
	}

	private void OnSpellFinished_StandardSpawnCharacter(Spell spell, object userData)
	{
		m_actorReady = true;
		m_inputEnabled = true;
		m_actor.Show();
		ActivateStateSpells();
		RefreshActor();
		UpdateActorComponents();
		BoardEvents boardEvents = BoardEvents.Get();
		if (boardEvents != null)
		{
			boardEvents.SummonedEvent(this);
		}
	}

	private void OnSpellFinished_CustomSpawnMinion(Spell spell, object userData)
	{
		OnSpellFinished_StandardSpawnCharacter(spell, userData);
		CleanUpCustomSpell(spell, ref m_customSpawnSpell);
		CleanUpCustomSpell(spell, ref m_customSpawnSpellOverride);
		ActivateCharacterPlayEffects();
	}

	private void OnSpellFinished_DefaultHandSpawn(Spell spell, object userData)
	{
		m_actor.ToggleForceIdle(bOn: false);
		m_inputEnabled = true;
		ActivateStateSpells();
		RefreshActor();
		UpdateActorComponents();
	}

	private void OnSpellFinished_CustomHandSpawn(Spell spell, object userData)
	{
		OnSpellFinished_DefaultHandSpawn(spell, userData);
		CleanUpCustomSpell(spell, ref m_customSpawnSpellOverride);
	}

	private void OnSpellFinished_DefaultPlaySpawn(Spell spell, object userData)
	{
		m_actor.ToggleForceIdle(bOn: false);
		m_inputEnabled = true;
		if (m_zone != null)
		{
			ActivateStateSpells();
		}
		RefreshActor();
		UpdateActorComponents();
	}

	private void OnSpellFinished_CustomPlaySpawn(Spell spell, object userData)
	{
		OnSpellFinished_DefaultPlaySpawn(spell, userData);
		CleanUpCustomSpell(spell, ref m_customSpawnSpell);
		CleanUpCustomSpell(spell, ref m_customSpawnSpellOverride);
	}

	private void OnSpellFinished_StandardCardSummon(Spell spell, object userData)
	{
		m_actorReady = true;
		m_inputEnabled = true;
		ActivateStateSpells();
		RefreshActor();
		UpdateActorComponents();
	}

	private void OnSpellFinished_UpdateActorComponents(Spell spell, object userData)
	{
		UpdateActorComponents();
	}

	private void OnSpellFinished_Death(Spell spell, object userData)
	{
		m_suppressKeywordDeaths = false;
		m_keywordDeathDelaySec = 0.6f;
		m_activeDeathEffectCount--;
		GameState.Get().ClearCardBeingDrawn(this);
	}

	private void OnSpellStateFinished_DestroyActor(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			if (m_zone is ZoneGraveyard)
			{
				PurgeSpells();
			}
			Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(spell.gameObject);
			if (actor == null)
			{
				Debug.LogWarning($"Card.OnSpellStateFinished_DestroyActor() - spell {spell} on Card {this} has no Actor ancestor");
			}
			else
			{
				actor.Destroy();
			}
		}
	}

	private void OnSpellStateFinished_DestroySpell(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			UnityEngine.Object.Destroy(spell.gameObject);
		}
	}

	private void OnSpellStateFinished_CustomDeath(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(spell.gameObject);
			if (actor == null)
			{
				Debug.LogWarning($"Card.OnSpellStateFinished_CustomDeath() - spell {spell} on Card {this} has no Actor ancestor");
			}
			else
			{
				actor.Destroy();
			}
		}
	}

	public void UpdateActorState(bool forceHighlightRefresh = false)
	{
		if (m_actor == null || !m_shown || m_entity.IsBusy() || m_zone is ZoneGraveyard)
		{
			return;
		}
		if (!m_inputEnabled || (m_zone != null && !m_zone.IsInputEnabled()))
		{
			m_actor.SetActorState(ActorStateType.CARD_IDLE);
			return;
		}
		if (m_overPlayfield)
		{
			m_actor.SetActorState(ActorStateType.CARD_OVER_PLAYFIELD);
			return;
		}
		GameState gameState = GameState.Get();
		if (gameState != null && gameState.IsEntityInputEnabled(m_entity))
		{
			if (forceHighlightRefresh)
			{
				m_actor.SetActorState(ActorStateType.CARD_IDLE);
			}
			switch (gameState.GetResponseMode())
			{
			case GameState.ResponseMode.CHOICE:
				if (DoChoiceHighlight(gameState))
				{
					return;
				}
				break;
			case GameState.ResponseMode.OPTION:
				if (DoOptionHighlight(gameState))
				{
					return;
				}
				break;
			case GameState.ResponseMode.SUB_OPTION:
				if (DoSubOptionHighlight(gameState))
				{
					return;
				}
				break;
			case GameState.ResponseMode.OPTION_TARGET:
				if (DoOptionTargetHighlight(gameState))
				{
					return;
				}
				break;
			}
		}
		if (m_mousedOver && !(m_zone is ZoneHand))
		{
			m_actor.SetActorState(ActorStateType.CARD_MOUSE_OVER);
		}
		else if (m_mousedOverByOpponent)
		{
			m_actor.SetActorState(ActorStateType.CARD_OPPONENT_MOUSE_OVER);
		}
		else
		{
			m_actor.SetActorState(ActorStateType.CARD_IDLE);
		}
	}

	private bool DoChoiceHighlight(GameState state)
	{
		if (state.GetChosenEntities().Contains(m_entity))
		{
			if (m_mousedOver)
			{
				m_actor.SetActorState(ActorStateType.CARD_PLAYABLE_MOUSE_OVER);
			}
			else
			{
				m_actor.SetActorState(ActorStateType.CARD_SELECTED);
			}
			return true;
		}
		int entityId = m_entity.GetEntityId();
		if (state.GetFriendlyEntityChoices().Entities.Contains(entityId))
		{
			if (GameState.Get().IsMulliganManagerActive())
			{
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_HAS_HERO_LOBBY))
				{
					if (m_mousedOver)
					{
						m_actor.SetActorState(GameState.Get().GetGameEntity().GetMulliganChoiceHighlightState());
					}
					else
					{
						m_actor.SetActorState(ActorStateType.CARD_IDLE);
					}
				}
				else
				{
					m_actor.SetActorState(GameState.Get().GetGameEntity().GetMulliganChoiceHighlightState());
				}
			}
			else
			{
				m_actor.SetActorState(ActorStateType.CARD_SELECTABLE);
			}
			return true;
		}
		return false;
	}

	private bool DoOptionHighlight(GameState state)
	{
		if (!GameState.Get().IsValidOption(m_entity))
		{
			return false;
		}
		bool flag = m_entity.GetZone() == TAG_ZONE.HAND;
		bool flag2 = m_entity.GetController().IsRealTimeComboActive();
		if ((flag || m_entity.IsHeroPowerOrGameModeButton()) && flag2 && m_entity.HasTag(GAME_TAG.COMBO))
		{
			m_actor.SetActorState(ActorStateType.CARD_COMBO);
			return true;
		}
		bool realTimePoweredUp = m_entity.GetRealTimePoweredUp();
		if ((flag || m_entity.IsHeroPowerOrGameModeButton()) && realTimePoweredUp)
		{
			m_actor.SetActorState(ActorStateType.CARD_POWERED_UP);
			return true;
		}
		if ((m_entity.GetZone() == TAG_ZONE.PLAY || (m_latestZoneChange != null && m_latestZoneChange.GetDestinationZone() != null && m_latestZoneChange.GetDestinationZone().m_ServerTag == TAG_ZONE.PLAY)) && state.GetGameEntity().GetTag(GAME_TAG.ALLOW_MOVE_MINION) > 0 && m_entity.IsMinion())
		{
			if (!GameState.Get().HasEnoughManaForMoveMinionHoverTarget(m_entity))
			{
				if (m_mousedOver)
				{
					m_actor.SetActorState(ActorStateType.CARD_MOUSE_OVER);
				}
				else
				{
					m_actor.SetActorState(ActorStateType.CARD_IDLE);
				}
				return true;
			}
			if (m_mousedOver)
			{
				m_actor.SetActorState(ActorStateType.CARD_MOVEABLE_MOUSE_OVER);
			}
			else
			{
				m_actor.SetActorState(ActorStateType.CARD_MOVEABLE);
			}
			return true;
		}
		if (!flag && m_mousedOver)
		{
			if (m_entity.GetRealTimeAttackableByRush())
			{
				m_actor.SetActorState(ActorStateType.CARD_ATTACKABLE_BY_RUSH_MOUSE_OVER);
			}
			else
			{
				m_actor.SetActorState(ActorStateType.CARD_PLAYABLE_MOUSE_OVER);
			}
			return true;
		}
		if (m_entity.GetRealTimeAttackableByRush())
		{
			m_actor.SetActorState(ActorStateType.CARD_ATTACKABLE_BY_RUSH);
		}
		else
		{
			m_actor.SetActorState(ActorStateType.CARD_PLAYABLE);
		}
		return true;
	}

	private bool DoSubOptionHighlight(GameState state)
	{
		Network.Options.Option selectedNetworkOption = state.GetSelectedNetworkOption();
		int entityId = m_entity.GetEntityId();
		foreach (Network.Options.Option.SubOption sub in selectedNetworkOption.Subs)
		{
			if (entityId == sub.ID)
			{
				if (!sub.PlayErrorInfo.IsValid())
				{
					return false;
				}
				if (m_mousedOver)
				{
					m_actor.SetActorState(ActorStateType.CARD_PLAYABLE_MOUSE_OVER);
				}
				else
				{
					m_actor.SetActorState(ActorStateType.CARD_PLAYABLE);
				}
				return true;
			}
		}
		return false;
	}

	private bool DoOptionTargetHighlight(GameState state)
	{
		Network.Options.Option.SubOption selectedNetworkSubOption = state.GetSelectedNetworkSubOption();
		int entityId = m_entity.GetEntityId();
		if (selectedNetworkSubOption.IsValidTarget(entityId))
		{
			if (m_mousedOver)
			{
				m_actor.SetActorState(ActorStateType.CARD_VALID_TARGET_MOUSE_OVER);
			}
			else
			{
				m_actor.SetActorState(ActorStateType.CARD_VALID_TARGET);
			}
			return true;
		}
		return false;
	}

	public Actor GetActor()
	{
		return m_actor;
	}

	public void SetActor(Actor actor)
	{
		m_actor = actor;
	}

	public string GetActorAssetPath()
	{
		return m_actorPath;
	}

	public void SetActorAssetPath(string actorName)
	{
		m_actorPath = actorName;
	}

	public bool IsActorReady()
	{
		return m_actorReady;
	}

	public bool IsActorLoading()
	{
		return m_actorLoading;
	}

	public void UpdateActorComponents()
	{
		if (!(m_actor == null))
		{
			m_actor.UpdateAllComponents();
		}
	}

	public void RefreshActor()
	{
		UpdateActorState();
		if (m_entity.IsEnchanted())
		{
			UpdateEnchantments();
		}
		UpdateTooltip();
	}

	public Zone GetZone()
	{
		return m_zone;
	}

	public Zone GetPrevZone()
	{
		return m_prevZone;
	}

	public void SetZone(Zone zone)
	{
		m_zone = zone;
	}

	public int GetZonePosition()
	{
		return m_zonePosition;
	}

	public void SetZonePosition(int pos)
	{
		m_zonePosition = pos;
	}

	public int GetPredictedZonePosition()
	{
		return m_predictedZonePosition;
	}

	public void SetPredictedZonePosition(int pos)
	{
		m_predictedZonePosition = pos;
	}

	public ZoneTransitionStyle GetTransitionStyle()
	{
		return m_transitionStyle;
	}

	public void SetTransitionStyle(ZoneTransitionStyle style)
	{
		m_transitionStyle = style;
	}

	public bool IsTransitioningZones()
	{
		return m_transitioningZones;
	}

	public void EnableTransitioningZones(bool enable)
	{
		m_transitioningZones = enable;
	}

	public bool HasBeenGrabbedByEnemyActionHandler()
	{
		return m_hasBeenGrabbedByEnemyActionHandler;
	}

	public void MarkAsGrabbedByEnemyActionHandler(bool enable)
	{
		Log.FaceDownCard.Print("Card.MarkAsGrabbedByEnemyActionHandler() - card={0} enable={1}", this, enable);
		m_hasBeenGrabbedByEnemyActionHandler = enable;
	}

	public bool IsDoNotSort()
	{
		return m_doNotSort;
	}

	public void SetDoNotSort(bool on)
	{
		if (m_entity.IsControlledByOpposingSidePlayer())
		{
			Log.FaceDownCard.Print("Card.SetDoNotSort() - card={0} on={1}", this, on);
		}
		m_doNotSort = on;
	}

	public bool IsDoNotWarpToNewZone()
	{
		return m_doNotWarpToNewZone;
	}

	public void SetDoNotWarpToNewZone(bool on)
	{
		m_doNotWarpToNewZone = on;
	}

	public float GetTransitionDelay()
	{
		return m_transitionDelay;
	}

	public void SetTransitionDelay(float delay)
	{
		m_transitionDelay = delay;
	}

	public void UpdateZoneFromTags()
	{
		m_zonePosition = m_entity.GetZonePosition();
		Zone zone = ZoneMgr.Get().FindZoneForEntity(m_entity);
		TransitionToZone(zone);
		if (zone != null)
		{
			zone.UpdateLayout();
		}
	}

	public void TransitionToZone(Zone zone, ZoneChange zoneChange = null)
	{
		m_latestZoneChange = zoneChange;
		if (m_zone == zone)
		{
			Log.Gameplay.Print("Card.TransitionToZone() - card={0} already in target zone", this);
			return;
		}
		if (zone == null)
		{
			m_zone.RemoveCard(this);
			m_prevZone = m_zone;
			m_zone = null;
			DeactivateLifetimeEffects();
			DeactivateCustomKeywordEffect();
			if (m_prevZone is ZoneHand)
			{
				DeactivateHandStateSpells();
			}
			if (m_prevZone is ZoneHeroPower)
			{
				foreach (Card card in m_prevZone.GetCards())
				{
					if (!(card == this) && card.GetEntity().GetTag(GAME_TAG.LINKED_ENTITY) == m_entity.GetEntityId() && card.m_customSpawnSpellOverride != null)
					{
						if (m_actor != null)
						{
							m_actor.DeactivateAllSpells();
						}
						return;
					}
				}
			}
			if (m_prevZone is ZoneHero)
			{
				Player controller = m_prevZone.GetController();
				if (controller.GetHero() != null && controller.GetHero().GetCard() != null)
				{
					controller.GetHero().GetCard().ShowCard();
				}
			}
			DoNullZoneVisuals();
			return;
		}
		if (m_zone is ZoneSecret && m_entity != null && m_entity.IsQuest())
		{
			NotifyMousedOut();
		}
		m_prevZone = m_zone;
		m_zone = zone;
		if (m_prevZone is ZoneDeck && m_zone is ZoneHand)
		{
			if (m_zone.m_Side == Player.Side.FRIENDLY)
			{
				m_cardDrawTracker = GameState.Get().GetFriendlyCardDrawCounter();
				GameState.Get().IncrementFriendlyCardDrawCounter();
			}
			else
			{
				m_cardDrawTracker = GameState.Get().GetOpponentCardDrawCounter();
				GameState.Get().IncrementOpponentCardDrawCounter();
			}
		}
		if (m_prevZone != null)
		{
			m_prevZone.RemoveCard(this);
		}
		m_zone.AddCard(this);
		if ((m_zone is ZonePlay || m_zone is ZoneHero) && m_prevZone is ZoneHand && m_entity.IsHero() && GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS) && MulliganManager.Get() != null && MulliganManager.Get().IsMulliganActive())
		{
			m_actorReady = true;
			return;
		}
		if (m_zone is ZoneGraveyard && m_actor != null && m_actor.UseCoinManaGem())
		{
			m_actor.DestroySpell(SpellType.COIN_MANA_GEM);
		}
		if (m_zone is ZoneGraveyard && GameState.Get().IsBeingDrawn(this))
		{
			m_actorReady = true;
			DiscardCardBeingDrawn();
		}
		else if (m_zone is ZoneGraveyard && m_ignoreDeath)
		{
			m_actorReady = true;
		}
		else if (m_zone is ZoneGraveyard && m_actor != null && m_actorReady && m_entity.IsSpell())
		{
			m_actorReady = false;
			StartCoroutine(LoadActorAndSpellsAfterPowerUpFinishes());
		}
		else
		{
			m_actorReady = false;
			LoadActorAndSpells();
		}
	}

	public void UpdateActor(bool forceIfNullZone = false, string actorPath = null)
	{
		if (!forceIfNullZone && m_zone == null)
		{
			return;
		}
		TAG_ZONE zone = m_entity.GetZone();
		if (actorPath == null)
		{
			actorPath = m_cardDef.CardDef.DetermineActorPathForZone(m_entity, zone);
		}
		if (m_actor != null && m_actorPath == actorPath)
		{
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(actorPath, AssetLoadingOptions.IgnorePrefabPosition);
		if (!gameObject)
		{
			Debug.LogWarningFormat("Card.UpdateActor() - FAILED to load actor \"{0}\"", actorPath);
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarningFormat("Card.UpdateActor() - ERROR actor \"{0}\" has no Actor component", actorPath);
			return;
		}
		if (m_actor != null)
		{
			m_actor.Destroy();
		}
		m_actor = component;
		m_actorPath = actorPath;
		m_actor.SetEntity(m_entity);
		m_actor.SetCard(this);
		m_actor.SetCardDef(m_cardDef);
		m_actor.UpdateAllComponents();
		if (m_shown)
		{
			ShowImpl();
		}
		else
		{
			HideImpl();
		}
		RefreshActor();
	}

	private IEnumerator LoadActorAndSpellsAfterPowerUpFinishes()
	{
		m_actorLoading = true;
		Spell spell = m_actor.GetSpell(SpellType.POWER_UP);
		if (spell != null)
		{
			while (spell.GetActiveState() != 0 && spell.GetActiveState() != SpellStateType.IDLE)
			{
				yield return null;
			}
		}
		LoadActorAndSpells();
	}

	private void LoadActorAndSpells()
	{
		m_actorLoading = true;
		List<SpellLoadRequest> list = new List<SpellLoadRequest>();
		if (m_prevZone is ZoneHand && (m_zone is ZonePlay || m_zone is ZoneHero || m_zone is ZoneWeapon))
		{
			SpellLoadRequest spellLoadRequest = MakeCustomSpellLoadRequest(m_cardDef.CardDef.m_CustomSummonSpellPath, m_cardDef.CardDef.m_GoldenCustomSummonSpellPath, OnCustomSummonSpellLoaded);
			if (spellLoadRequest != null)
			{
				list.Add(spellLoadRequest);
			}
		}
		if (!m_customDeathSpell && (m_zone is ZoneHand || m_zone is ZonePlay))
		{
			SpellLoadRequest spellLoadRequest2 = MakeCustomSpellLoadRequest(m_cardDef.CardDef.m_CustomDeathSpellPath, m_cardDef.CardDef.m_GoldenCustomDeathSpellPath, OnCustomDeathSpellLoaded);
			if (spellLoadRequest2 != null)
			{
				list.Add(spellLoadRequest2);
			}
		}
		if (!m_customDiscardSpell && (m_zone is ZoneHand || m_zone is ZoneGraveyard))
		{
			SpellLoadRequest spellLoadRequest3 = MakeCustomSpellLoadRequest(m_cardDef.CardDef.m_CustomDiscardSpellPath, m_cardDef.CardDef.m_GoldenCustomDiscardSpellPath, OnCustomDiscardSpellLoaded);
			if (spellLoadRequest3 != null)
			{
				list.Add(spellLoadRequest3);
			}
		}
		if (!m_customSpawnSpell && (m_zone is ZonePlay || m_zone is ZoneWeapon))
		{
			SpellLoadRequest spellLoadRequest4 = MakeCustomSpellLoadRequest(m_cardDef.CardDef.m_CustomSpawnSpellPath, m_cardDef.CardDef.m_GoldenCustomSpawnSpellPath, OnCustomSpawnSpellLoaded);
			if (spellLoadRequest4 != null)
			{
				list.Add(spellLoadRequest4);
			}
		}
		m_spellLoadCount = list.Count;
		if (list.Count == 0)
		{
			LoadActor();
			return;
		}
		foreach (SpellLoadRequest item in list)
		{
			AssetLoader.Get().InstantiatePrefab(item.m_path, item.m_loadCallback);
		}
	}

	private SpellLoadRequest MakeCustomSpellLoadRequest(string customPath, string goldenCustomPath, PrefabCallback<GameObject> loadCallback)
	{
		string text = customPath;
		if (m_entity.GetPremiumType() == TAG_PREMIUM.GOLDEN && !string.IsNullOrEmpty(goldenCustomPath))
		{
			text = goldenCustomPath;
		}
		else if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		return new SpellLoadRequest
		{
			m_path = text,
			m_loadCallback = loadCallback
		};
	}

	private void OnCustomSummonSpellLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Error.AddDevFatal("Card.OnCustomSummonSpellLoaded() - FAILED to load \"{0}\" for card {1}", assetRef, this);
			FinishSpellLoad();
			return;
		}
		m_customSummonSpell = go.GetComponent<Spell>();
		if (m_customSummonSpell == null)
		{
			FinishSpellLoad();
			return;
		}
		SpellUtils.SetupSpell(m_customSummonSpell, this);
		FinishSpellLoad();
	}

	private void OnCustomDeathSpellLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Error.AddDevFatal("Card.OnCustomDeathSpellLoaded() - FAILED to load \"{0}\" for card {1}", assetRef, this);
			FinishSpellLoad();
			return;
		}
		m_customDeathSpell = go.GetComponent<Spell>();
		if (m_customDeathSpell == null)
		{
			FinishSpellLoad();
			return;
		}
		SpellUtils.SetupSpell(m_customDeathSpell, this);
		FinishSpellLoad();
	}

	private void OnCustomDiscardSpellLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Error.AddDevFatal("Card.OnCustomDiscardSpellLoaded() - FAILED to load \"{0}\" for card {1}", assetRef, this);
			FinishSpellLoad();
			return;
		}
		m_customDiscardSpell = go.GetComponent<Spell>();
		if (m_customDiscardSpell == null)
		{
			FinishSpellLoad();
			return;
		}
		SpellUtils.SetupSpell(m_customDiscardSpell, this);
		FinishSpellLoad();
	}

	private void OnCustomSpawnSpellLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Error.AddDevFatal("Card.OnCustomSpawnSpellLoaded() - FAILED to load \"{0}\" for card {1}", assetRef, this);
			FinishSpellLoad();
			return;
		}
		m_customSpawnSpell = go.GetComponent<Spell>();
		if (m_customSpawnSpell == null)
		{
			FinishSpellLoad();
			return;
		}
		SpellUtils.SetupSpell(m_customSpawnSpell, this);
		FinishSpellLoad();
	}

	private void FinishSpellLoad()
	{
		m_spellLoadCount--;
		if (m_spellLoadCount <= 0)
		{
			LoadActor();
		}
	}

	private void LoadActor()
	{
		RefreshHeroPowerTooltip();
		string text = m_cardDef.CardDef.DetermineActorPathForZone(m_entity, m_zone.m_ServerTag);
		if (m_actorPath == text || text == null)
		{
			m_actorPath = text;
			FinishActorLoad(m_actor);
		}
		else
		{
			AssetLoader.Get().InstantiatePrefab(text, OnActorLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		}
	}

	private bool ShouldShowHeroPowerTooltip()
	{
		if (m_heroPowerTooltip != null && m_zone is ZoneHand)
		{
			return m_entity.IsControlledByFriendlySidePlayer();
		}
		return false;
	}

	private void CreateHeroPowerTooltip()
	{
		if (m_heroPowerTooltip == null)
		{
			m_heroPowerTooltip = base.gameObject.AddComponent<HeroPowerTooltip>();
			m_heroPowerTooltip.Setup(this);
		}
	}

	private void DestroyHeroPowerTooltip()
	{
		if (m_heroPowerTooltip != null)
		{
			UnityEngine.Object.Destroy(m_heroPowerTooltip);
			m_heroPowerTooltip = null;
		}
	}

	public void RefreshHeroPowerTooltip()
	{
		DestroyHeroPowerTooltip();
		if (m_entity.IsHero() && m_zone is ZoneHand)
		{
			CreateHeroPowerTooltip();
		}
		else if (m_entity.IsSidekickHero() && m_zone is ZoneHero)
		{
			CreateHeroPowerTooltip();
		}
		else if (m_entity.HasTag(GAME_TAG.DISPLAY_CARD_ON_MOUSEOVER) && m_zone is ZoneHand)
		{
			CreateHeroPowerTooltip();
		}
	}

	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"Card.OnActorLoaded() - FAILED to load actor \"{assetRef}\"");
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning($"Card.OnActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
			return;
		}
		Actor actor = m_actor;
		m_actor = component;
		m_actorPath = assetRef.ToString();
		m_actor.SetEntity(m_entity);
		m_actor.SetCard(this);
		m_actor.SetCardDef(m_cardDef);
		m_actor.UpdateAllComponents();
		FinishActorLoad(actor);
	}

	private void FinishActorLoad(Actor oldActor)
	{
		m_actorLoading = false;
		OnZoneChanged();
		OnActorChanged(oldActor);
		if (m_isBattleCrySource)
		{
			SceneUtils.SetLayer(m_actor.gameObject, GameLayer.IgnoreFullScreenEffects);
		}
		RefreshActor();
	}

	public void ForceLoadHandActor()
	{
		string text = m_cardDef.CardDef.DetermineActorPathForZone(m_entity, TAG_ZONE.HAND);
		if (m_actor != null && m_actorPath == text)
		{
			ShowCard();
			m_actor.Show();
			RefreshActor();
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("Card.ForceLoadHandActor() - FAILED to load actor \"{0}\"", text);
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarningFormat("Card.ForceLoadHandActor() - ERROR actor \"{0}\" has no Actor component", text);
			return;
		}
		if (m_actor != null)
		{
			m_actor.Destroy();
		}
		m_actor = component;
		m_actorPath = text;
		m_actor.SetEntity(m_entity);
		m_actor.SetCard(this);
		m_actor.SetCardDef(m_cardDef);
		m_actor.UpdateAllComponents();
		if (m_shown)
		{
			ShowImpl();
		}
		else
		{
			HideImpl();
		}
		RefreshActor();
	}

	private void HideHeroPowerTooltip()
	{
		if (m_heroPowerTooltip != null)
		{
			m_heroPowerTooltip.NotifyMousedOut();
		}
	}

	private void OnZoneChanged()
	{
		if (m_prevZone is ZoneHand && m_zone is ZoneGraveyard)
		{
			if (m_mousedOver)
			{
				NotifyMousedOut();
			}
			DoDiscardAnimation();
			HideHeroPowerTooltip();
		}
		else if (m_prevZone is ZoneHand)
		{
			if (m_mousedOver)
			{
				NotifyMousedOut();
			}
		}
		else if (m_zone is ZoneGraveyard)
		{
			if (m_entity.IsHero())
			{
				if (m_entity.HasTag(GAME_TAG.SIDEKICK))
				{
					DoNullZoneVisuals();
				}
				else
				{
					m_doNotSort = true;
				}
			}
		}
		else if (m_zone is ZoneHand)
		{
			if (!m_doNotSort)
			{
				ShowCard();
			}
			if (m_prevZone is ZoneGraveyard && m_entity.IsSpell())
			{
				m_actor.Hide();
				ActivateActorSpell(SpellType.SUMMON_IN, OnSpellFinished_DefaultHandSpawn);
			}
		}
		else if ((m_prevZone is ZoneGraveyard || m_prevZone is ZoneDeck) && m_zone.m_ServerTag == TAG_ZONE.PLAY)
		{
			ShowCard();
		}
		if (!(m_zone is ZonePlay) && m_magneticPlayData != null)
		{
			SpellUtils.ActivateDeathIfNecessary(GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_RIGHT));
			SpellUtils.ActivateDeathIfNecessary(m_magneticPlayData.m_targetMech.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT));
			SpellUtils.ActivateDeathIfNecessary(m_magneticPlayData.m_beamSpell);
			m_magneticPlayData = null;
		}
	}

	private void OnActorChanged(Actor oldActor)
	{
		HideTooltip();
		bool flag = false;
		bool flag2 = GameState.Get().IsGameCreating();
		if (m_prevZone == null && m_zone is ZoneGraveyard)
		{
			if (oldActor != null && oldActor != m_actor)
			{
				oldActor.Destroy();
			}
			if (IsShown())
			{
				HideCard();
			}
			else
			{
				HideImpl();
			}
			DeactivateHandStateSpells();
			flag = true;
			m_actorReady = true;
		}
		else if (oldActor == null)
		{
			bool flag3 = GameState.Get().IsMulliganPhaseNowOrPending();
			if (m_zone is ZoneHand && GameState.Get().IsBeginPhase())
			{
				bool flag4 = m_entity.GetCardId() == CoinManager.Get().GetFavoriteCoinCardId();
				if (flag3 && !GameState.Get().HasTheCoinBeenSpawned())
				{
					if (flag4)
					{
						GameState.Get().NotifyOfCoinSpawn();
						m_actor.TurnOffCollider();
						m_actor.Hide();
						m_actorReady = true;
						flag = true;
						base.transform.position = Vector3.zero;
						m_doNotWarpToNewZone = true;
						m_doNotSort = true;
					}
					else
					{
						Player controller = m_entity.GetController();
						if (controller.IsOpposingSide() && this == m_zone.GetLastCard() && !controller.HasTag(GAME_TAG.FIRST_PLAYER))
						{
							GameState.Get().NotifyOfCoinSpawn();
							m_actor.TurnOffCollider();
							m_actorReady = true;
							flag = true;
						}
					}
				}
				if (!flag4)
				{
					ZoneMgr.Get().FindZoneOfType<ZoneDeck>(m_zone.m_Side).SetCardToInDeckState(this);
				}
			}
			else if (flag2)
			{
				TransformUtil.CopyWorld(base.transform, m_zone.transform);
				if (m_zone is ZonePlay || m_zone is ZoneHero || m_zone is ZoneHeroPower || m_zone is ZoneWeapon)
				{
					ActivateLifetimeEffects();
				}
			}
			else
			{
				if (!m_doNotWarpToNewZone)
				{
					TransformUtil.CopyWorld(base.transform, m_zone.transform);
				}
				if (m_zone is ZoneHand)
				{
					if (!m_doNotWarpToNewZone)
					{
						ZoneHand zoneHand = (ZoneHand)m_zone;
						base.transform.localScale = zoneHand.GetCardScale();
						base.transform.localEulerAngles = zoneHand.GetCardRotation(this);
						base.transform.position = zoneHand.GetCardPosition(this);
					}
					if (m_entity.HasTag(GAME_TAG.LINKED_ENTITY))
					{
						int id = m_entity.GetTag(GAME_TAG.LINKED_ENTITY);
						Entity entity = GameState.Get().GetEntity(id);
						if (entity != null && entity.GetCard() != null)
						{
							m_actor.Hide();
							m_doNotSort = true;
							flag = true;
						}
					}
					else if (m_entity.HasTag(GAME_TAG.CREATOR) && GameState.Get().GetEntity(m_entity.GetTag(GAME_TAG.CREATOR)) != null && GameState.Get().GetEntity(m_entity.GetTag(GAME_TAG.CREATOR)).HasTag(GAME_TAG.TWINSPELL))
					{
						m_transitionStyle = ZoneTransitionStyle.INSTANT;
						ActivateHandSpawnSpell();
						InputManager.Get().GetFriendlyHand().ActivateTwinspellSpellDeath();
						InputManager.Get().GetFriendlyHand().ClearReservedCard();
					}
					else
					{
						m_actorReady = true;
						m_shown = true;
						if (!m_doNotWarpToNewZone)
						{
							m_actor.Hide();
							ActivateHandSpawnSpell();
							flag = true;
						}
					}
				}
				if (m_prevZone == null && m_zone is ZonePlay)
				{
					if (!m_doNotWarpToNewZone)
					{
						ZonePlay zonePlay = (ZonePlay)m_zone;
						base.transform.position = zonePlay.GetCardPosition(this);
					}
					if (m_cardDef.CardDef.m_SuppressPlaySoundsDuringMulligan && GameState.Get().IsMulliganPhaseNowOrPending())
					{
						SuppressPlaySounds(suppress: true);
					}
					if (m_entity.HasTag(GAME_TAG.LINKED_ENTITY))
					{
						if ((bool)m_customSpawnSpellOverride)
						{
							ActivateMinionSpawnEffects();
						}
						else
						{
							m_transitionStyle = ZoneTransitionStyle.INSTANT;
							Transform transform = Board.Get().FindBone("SpawnOffscreen");
							base.transform.position = transform.position;
							ActivateCharacterPlayEffects();
							OnSpellFinished_StandardSpawnCharacter(null, null);
						}
					}
					else
					{
						m_actor.Hide();
						ActivateMinionSpawnEffects();
					}
					flag = true;
				}
				else if (!flag3 && (m_zone is ZoneHeroPower || m_zone is ZoneWeapon))
				{
					if (IsShown())
					{
						ActivatePlaySpawnEffects_HeroPowerOrWeapon();
						flag = true;
						m_actorReady = true;
					}
				}
				else if (m_prevZone == null && m_zone is ZoneHero)
				{
					Entity entity2 = m_entity;
					if (entity2.HasTag(GAME_TAG.SIDEKICK))
					{
						ActivateStandardSpawnHeroSpell();
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
						ActivateStandardSpawnHeroSpell();
						flag = true;
					}
				}
			}
		}
		else if (m_prevZone == null && (m_zone is ZoneHeroPower || m_zone is ZoneWeapon))
		{
			oldActor.Destroy();
			TransformUtil.CopyWorld(base.transform, m_zone.transform);
			m_transitionStyle = ZoneTransitionStyle.INSTANT;
			ActivatePlaySpawnEffects_HeroPowerOrWeapon();
			flag = true;
			m_actorReady = true;
		}
		else if (m_prevZone == null && m_zone is ZoneHand && oldActor == m_actor && !m_goingThroughDeathrattleReturnfromGraveyard)
		{
			ActivateHandStateSpells();
			flag = true;
			m_actorReady = true;
		}
		else if (m_prevZone is ZoneHand && (m_zone is ZonePlay || m_zone is ZoneHero))
		{
			if (m_entity.IsObfuscated())
			{
				flag = true;
				m_actorReady = true;
			}
			else
			{
				ActivateActorSpells_HandToPlay(oldActor);
				if (m_cardDef.CardDef.m_SuppressPlaySoundsOnSummon || m_entity.HasTag(GAME_TAG.CARD_DOES_NOTHING))
				{
					SuppressPlaySounds(suppress: true);
				}
				ActivateCharacterPlayEffects();
				m_actor.Hide();
				flag = true;
				if (CardTypeBanner.Get() != null && CardTypeBanner.Get().HasCardDef && CardTypeBanner.Get().HasSameCardDef(m_cardDef.CardDef))
				{
					CardTypeBanner.Get().Hide();
				}
			}
		}
		else if (m_prevZone is ZoneHand && m_zone is ZoneWeapon)
		{
			if (ActivateActorSpells_HandToWeapon(oldActor))
			{
				m_actor.Hide();
				flag = true;
				if (CardTypeBanner.Get() != null && CardTypeBanner.Get().HasCardDef && CardTypeBanner.Get().HasSameCardDef(m_cardDef.CardDef))
				{
					CardTypeBanner.Get().Hide();
				}
			}
		}
		else if ((m_prevZone is ZonePlay || m_prevZone is ZoneHero) && m_zone is ZoneHand)
		{
			DeactivateLifetimeEffects();
			if (m_mousedOver && m_entity.IsControlledByFriendlySidePlayer())
			{
				if (m_entity.HasSpellPower())
				{
					ZoneMgr.Get().OnSpellPowerEntityMousedOut(m_entity.GetSpellPowerSchool());
				}
				if (m_entity.HasHealingDoesDamageHint())
				{
					ZoneMgr.Get().OnHealingDoesDamageEntityMousedOut();
				}
			}
			bool useFastAnimations = GameState.Get().GetGameEntity().GetTag(GAME_TAG.BACON_USE_FAST_ANIMATIONS) > 0;
			if (DoPlayToHandTransition(oldActor, wasInGraveyard: false, useFastAnimations))
			{
				flag = true;
			}
		}
		else if (m_prevZone is ZoneHero && m_zone is ZoneGraveyard)
		{
			oldActor.DoCardDeathVisuals();
			DeactivateCustomKeywordEffect();
			flag = true;
			m_actorReady = true;
		}
		else if (m_prevZone != null && (m_prevZone is ZonePlay || m_prevZone is ZoneWeapon || m_prevZone is ZoneHeroPower) && m_zone is ZoneGraveyard)
		{
			if (m_mousedOver && m_entity.IsControlledByFriendlySidePlayer() && m_prevZone is ZonePlay)
			{
				if (m_entity.HasSpellPower())
				{
					ZoneMgr.Get().OnSpellPowerEntityMousedOut(m_entity.GetSpellPowerSchool());
				}
				if (m_entity.HasHealingDoesDamageHint())
				{
					ZoneMgr.Get().OnHealingDoesDamageEntityMousedOut();
				}
			}
			if (m_entity.HasTag(GAME_TAG.DEATHRATTLE_RETURN_ZONE) && DoesCardReturnFromGraveyard())
			{
				m_playZoneBlockerSide = m_prevZone.m_Side;
				m_prevZone.AddLayoutBlocker();
				m_goingThroughDeathrattleReturnfromGraveyard = true;
				TAG_ZONE zoneTag = m_entity.GetTag<TAG_ZONE>(GAME_TAG.DEATHRATTLE_RETURN_ZONE);
				int cardFutureController = GetCardFutureController();
				Zone zone = ZoneMgr.Get().FindZoneForTags(cardFutureController, zoneTag, m_entity.GetCardType(), m_entity);
				if (zone is ZoneDeck)
				{
					zone.AddLayoutBlocker();
				}
				m_actorWaitingToBeReplaced = oldActor;
				m_actor.Hide();
				flag = true;
				m_actorReady = true;
			}
			else if (HandlePlayActorDeath(oldActor))
			{
				flag = true;
			}
		}
		else if (m_prevZone is ZoneDeck && m_zone is ZoneHand)
		{
			if (m_zone.m_Side == Player.Side.FRIENDLY)
			{
				if (GameState.Get().IsPastBeginPhase())
				{
					m_actorWaitingToBeReplaced = oldActor;
					m_cardStandInInteractive = false;
					if (!TurnStartManager.Get().IsCardDrawHandled(this))
					{
						DrawFriendlyCard();
					}
					flag = true;
				}
				else
				{
					m_actor.TurnOffCollider();
					m_actor.SetActorState(ActorStateType.CARD_IDLE);
				}
			}
			else if (GameState.Get().IsPastBeginPhase())
			{
				if (oldActor != null)
				{
					oldActor.Destroy();
				}
				DrawOpponentCard();
				flag = true;
			}
		}
		else if (m_prevZone is ZoneSecret && m_zone is ZoneGraveyard && m_entity.IsSecret())
		{
			flag = true;
			m_actorReady = true;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				m_shown = false;
				m_actor.Hide();
			}
			else
			{
				ShowSecretDeath(oldActor);
			}
		}
		else if (m_prevZone is ZoneGraveyard && m_zone is ZonePlay)
		{
			m_actor.Hide();
			StartCoroutine(ActivateReviveSpell());
			flag = true;
		}
		else if (m_prevZone is ZoneDeck && m_zone is ZoneGraveyard)
		{
			MillCard();
			flag = true;
		}
		else if (m_prevZone is ZoneDeck && m_zone is ZonePlay)
		{
			if (oldActor != null)
			{
				oldActor.Destroy();
			}
			AnimateDeckToPlay();
			flag = true;
		}
		else if (m_prevZone is ZonePlay && m_zone is ZoneDeck)
		{
			DeactivateLifetimeEffects();
			m_playZoneBlockerSide = m_prevZone.m_Side;
			m_prevZone.AddLayoutBlocker();
			ZoneMgr.Get().FindZoneOfType<ZoneDeck>(m_zone.m_Side).AddLayoutBlocker();
			DoPlayToDeckTransition(oldActor);
			flag = true;
		}
		else if (m_prevZone is ZoneHand && m_zone is ZoneDeck && GameState.Get().IsPastBeginPhase())
		{
			if (!m_suppressHandToDeckTransition)
			{
				StartCoroutine(DoHandToDeckTransition(oldActor));
			}
			else
			{
				oldActor.Destroy();
				m_actorReady = true;
			}
			m_suppressHandToDeckTransition = false;
			flag = true;
		}
		else if (m_goingThroughDeathrattleReturnfromGraveyard && m_zone is ZoneDeck)
		{
			m_goingThroughDeathrattleReturnfromGraveyard = false;
			if (HandleGraveyardToDeck(oldActor))
			{
				flag = true;
			}
		}
		else if (m_goingThroughDeathrattleReturnfromGraveyard && m_zone is ZoneHand)
		{
			m_goingThroughDeathrattleReturnfromGraveyard = false;
			if (HandleGraveyardToHand(oldActor))
			{
				flag = true;
			}
		}
		if (!flag && oldActor == m_actor)
		{
			if (m_prevZone != null && m_prevZone.m_Side != m_zone.m_Side && m_prevZone is ZoneSecret && m_zone is ZoneSecret)
			{
				StartCoroutine(SwitchSecretSides());
				flag = true;
			}
			if (!flag)
			{
				m_actorReady = true;
			}
			return;
		}
		if (!flag && m_zone is ZoneSecret)
		{
			m_shown = true;
			if ((bool)oldActor)
			{
				oldActor.Destroy();
			}
			m_transitionStyle = ZoneTransitionStyle.INSTANT;
			m_zone.UpdateLayout();
			ShowSecretQuestBirth();
			flag = true;
			m_actorReady = true;
			if (flag2)
			{
				ActivateStateSpells();
			}
		}
		if (!flag)
		{
			if ((bool)oldActor)
			{
				oldActor.Destroy();
			}
			bool flag5 = m_zone.m_ServerTag == TAG_ZONE.PLAY || m_zone.m_ServerTag == TAG_ZONE.SECRET || m_zone.m_ServerTag == TAG_ZONE.HAND;
			if (IsShown() && flag5)
			{
				ActivateStateSpells();
			}
			m_actorReady = true;
			if (IsShown())
			{
				ShowImpl();
			}
			else
			{
				HideImpl();
			}
		}
	}

	private bool HandleGraveyardToDeck(Actor oldActor)
	{
		if ((bool)m_actorWaitingToBeReplaced)
		{
			if ((bool)oldActor)
			{
				oldActor.Destroy();
			}
			oldActor = m_actorWaitingToBeReplaced;
			m_actorWaitingToBeReplaced = null;
			DoPlayToDeckTransition(oldActor);
			return true;
		}
		return false;
	}

	private bool HandleGraveyardToHand(Actor oldActor)
	{
		if ((bool)m_actorWaitingToBeReplaced)
		{
			if ((bool)oldActor && oldActor != m_actor)
			{
				oldActor.Destroy();
			}
			oldActor = m_actorWaitingToBeReplaced;
			m_actorWaitingToBeReplaced = null;
			bool useFastAnimations = GameState.Get().GetGameEntity().GetTag(GAME_TAG.BACON_USE_FAST_ANIMATIONS) > 0;
			if (DoPlayToHandTransition(oldActor, wasInGraveyard: true, useFastAnimations))
			{
				return true;
			}
		}
		return false;
	}

	public bool CardStandInIsInteractive()
	{
		return m_cardStandInInteractive;
	}

	private void ReadyCardForDraw()
	{
		GetController().GetDeckZone().SetCardToInDeckState(this);
	}

	public void DrawFriendlyCard()
	{
		StartCoroutine(DrawFriendlyCardWithTiming());
	}

	private IEnumerator DrawFriendlyCardWithTiming()
	{
		m_doNotSort = true;
		m_transitionStyle = ZoneTransitionStyle.SLOW;
		m_actor.Hide();
		while ((bool)GameState.Get().GetFriendlyCardBeingDrawn())
		{
			yield return null;
		}
		GameState.Get().SetFriendlyCardBeingDrawn(this);
		ReadyCardForDraw();
		Actor cardDrawStandIn = Gameplay.Get().GetCardDrawStandIn();
		cardDrawStandIn.transform.parent = m_actor.transform.parent;
		cardDrawStandIn.transform.localPosition = Vector3.zero;
		cardDrawStandIn.transform.localScale = Vector3.one;
		cardDrawStandIn.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		cardDrawStandIn.Show();
		cardDrawStandIn.GetRootObject().GetComponentInChildren<CardBackDisplay>().SetCardBack(CardBackManager.CardBackSlot.FRIENDLY);
		if (m_actorWaitingToBeReplaced != null)
		{
			m_actorWaitingToBeReplaced.Destroy();
			m_actorWaitingToBeReplaced = null;
		}
		DetermineIfOverrideDrawTimeScale();
		Transform transform = Board.Get().FindBone("FriendlyDrawCard");
		Vector3[] array = new Vector3[3]
		{
			base.gameObject.transform.position,
			base.gameObject.transform.position + ABOVE_DECK_OFFSET,
			transform.position
		};
		float num = 1.5f * m_drawTimeScale.Value;
		iTween.MoveTo(base.gameObject, iTween.Hash("path", array, "time", num, "easetype", iTween.EaseType.easeInSineOutExpo));
		base.gameObject.transform.localEulerAngles = new Vector3(270f, 270f, 0f);
		Vector3 vector = new Vector3(0f, 0f, 357f);
		float num2 = 1.35f * m_drawTimeScale.Value;
		float num3 = 0.15f * m_drawTimeScale.Value;
		iTween.RotateTo(base.gameObject, iTween.Hash("rotation", vector, "time", num2, "delay", num3));
		float num4 = 0.75f * m_drawTimeScale.Value;
		float num5 = 0.15f * m_drawTimeScale.Value;
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", transform.localScale, "time", num4, "delay", num5));
		SoundManager.Get().LoadAndPlay("draw_card_1.prefab:19dd221ebfed9754e85ef1f104e0fddb", base.gameObject);
		cardDrawStandIn.transform.parent = null;
		cardDrawStandIn.Hide();
		m_actor.Show();
		m_actor.TurnOffCollider();
		GameState.Get().GetFriendlySidePlayer().GetDeckZone()
			.UpdateLayout();
		while (iTween.Count(base.gameObject) > 0)
		{
			yield return null;
		}
		m_actorReady = true;
		if (ShouldCardDrawWaitForTurnStartSpells())
		{
			yield return StartCoroutine(WaitForCardDrawBlockingTurnStartSpells());
		}
		else
		{
			PowerTask cardDrawBlockingTask = GetPowerTaskToBlockCardDraw();
			if (cardDrawBlockingTask != null)
			{
				while (!cardDrawBlockingTask.IsCompleted())
				{
					yield return null;
				}
			}
		}
		m_doNotSort = false;
		GameState.Get().ClearCardBeingDrawn(this);
		ResetCardDrawTimeScale();
		if (m_zone != null && m_zone is ZoneHand)
		{
			ZoneHand handZone = (ZoneHand)m_zone;
			SoundManager.Get().LoadAndPlay("add_card_to_hand_1.prefab:bf6b149b859734c4faf9a96356c53646", base.gameObject);
			ActivateStateSpells();
			RefreshActor();
			m_zone.UpdateLayout();
			yield return new WaitForSeconds(0.3f);
			m_cardStandInInteractive = true;
			handZone.MakeStandInInteractive(this);
		}
	}

	public bool IsBeingDrawnByOpponent()
	{
		return m_beingDrawnByOpponent;
	}

	private void DrawOpponentCard()
	{
		StartCoroutine(DrawOpponentCardWithTiming());
	}

	private IEnumerator DrawOpponentCardWithTiming()
	{
		m_doNotSort = true;
		m_beingDrawnByOpponent = true;
		m_actor.Hide();
		while ((bool)GameState.Get().GetOpponentCardBeingDrawn())
		{
			yield return null;
		}
		if (GetZonePosition() == 0)
		{
			yield return null;
		}
		m_actor.Show();
		GameState.Get().SetOpponentCardBeingDrawn(this);
		ReadyCardForDraw();
		ZoneHand zoneHand = (ZoneHand)m_zone;
		zoneHand.UpdateLayout();
		if (m_entity.HasTag(GAME_TAG.REVEALED))
		{
			StartCoroutine(DrawKnownOpponentCard(zoneHand));
		}
		else
		{
			StartCoroutine(DrawUnknownOpponentCard(zoneHand));
		}
	}

	private IEnumerator DrawUnknownOpponentCard(ZoneHand handZone)
	{
		SoundManager.Get().LoadAndPlay("draw_card_and_add_to_hand_opp_1.prefab:5a05fbb2c5833a94182e1b454647d5c8", base.gameObject);
		base.gameObject.transform.rotation = IN_DECK_HIDDEN_ROTATION;
		DetermineIfOverrideDrawTimeScale();
		Transform transform = Board.Get().FindBone("OpponentDrawCard");
		Vector3[] array = new Vector3[4]
		{
			base.gameObject.transform.position,
			base.gameObject.transform.position + ABOVE_DECK_OFFSET,
			transform.position,
			handZone.GetCardPosition(this)
		};
		float num = 1.75f * m_drawTimeScale.Value;
		iTween.MoveTo(base.gameObject, iTween.Hash("path", array, "time", num, "easetype", iTween.EaseType.easeInOutQuart));
		float num2 = 0.7f * m_drawTimeScale.Value;
		float num3 = 0.8f * m_drawTimeScale.Value;
		iTween.RotateTo(base.gameObject, iTween.Hash("rotation", handZone.GetCardRotation(this), "time", num2, "delay", num3, "easetype", iTween.EaseType.easeInOutCubic));
		float num4 = 0.7f * m_drawTimeScale.Value;
		float num5 = 0.8f * m_drawTimeScale.Value;
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", handZone.GetCardScale(), "time", num4, "delay", num5, "easetype", iTween.EaseType.easeInOutQuint));
		GameState.Get().GetOpposingSidePlayer().GetDeckZone()
			.UpdateLayout();
		yield return new WaitForSeconds(0.2f);
		m_actorReady = true;
		yield return new WaitForSeconds(0.6f);
		GameState.Get().UpdateOptionHighlights();
		while (iTween.Count(base.gameObject) > 0)
		{
			yield return null;
		}
		m_doNotSort = false;
		m_beingDrawnByOpponent = false;
		GameState.Get().SetOpponentCardBeingDrawn(null);
		ResetCardDrawTimeScale();
		handZone.UpdateLayout();
	}

	private IEnumerator DrawKnownOpponentCard(ZoneHand handZone)
	{
		Actor handActor = null;
		bool loadingActor = true;
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			loadingActor = false;
			if (go == null)
			{
				Error.AddDevFatal("Card.DrawKnownOpponentCard() - failed to load {0}", assetRef);
			}
			else
			{
				handActor = go.GetComponent<Actor>();
				if (handActor == null)
				{
					Error.AddDevFatal("Card.DrawKnownOpponentCard() - instance of {0} has no Actor component", base.name);
				}
			}
		};
		string actorPath = ActorNames.GetHandActor(m_entity);
		AssetLoader.Get().InstantiatePrefab(actorPath, callback, null, AssetLoadingOptions.IgnorePrefabPosition);
		while (loadingActor)
		{
			yield return null;
		}
		if ((bool)handActor)
		{
			handActor.SetEntity(m_entity);
			handActor.SetCardDef(m_cardDef);
			handActor.UpdateAllComponents();
			StartCoroutine(RevealDrawnOpponentCard(actorPath, handActor, handZone));
		}
		else
		{
			StartCoroutine(DrawUnknownOpponentCard(handZone));
		}
	}

	private IEnumerator RevealDrawnOpponentCard(string handActorPath, Actor handActor, ZoneHand handZone)
	{
		SoundManager.Get().LoadAndPlay("draw_card_1.prefab:19dd221ebfed9754e85ef1f104e0fddb", base.gameObject);
		handActor.transform.parent = m_actor.transform.parent;
		TransformUtil.CopyLocal(handActor, m_actor);
		m_actor.Hide();
		DetermineIfOverrideDrawTimeScale();
		base.gameObject.transform.localEulerAngles = new Vector3(270f, 90f, 0f);
		string text = "OpponentDrawCardAndReveal";
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			text += "_phone";
		}
		Transform transform = Board.Get().FindBone(text);
		Vector3[] array = new Vector3[3]
		{
			base.gameObject.transform.position,
			base.gameObject.transform.position + ABOVE_DECK_OFFSET,
			transform.position
		};
		float num = 1.75f * m_drawTimeScale.Value;
		iTween.MoveTo(base.gameObject, iTween.Hash("path", array, "time", num, "easetype", iTween.EaseType.easeInOutQuart));
		float num2 = 0.7f * m_drawTimeScale.Value;
		float num3 = 0.8f * m_drawTimeScale.Value;
		iTween.RotateTo(base.gameObject, iTween.Hash("rotation", transform.eulerAngles, "time", num2, "delay", num3, "easetype", iTween.EaseType.easeInOutCubic));
		float num4 = 0.7f * m_drawTimeScale.Value;
		float num5 = 0.8f * m_drawTimeScale.Value;
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", transform.localScale, "time", num4, "delay", num5, "easetype", iTween.EaseType.easeInOutQuint));
		GameState.Get().GetOpposingSidePlayer().GetDeckZone()
			.UpdateLayout();
		yield return new WaitForSeconds(1.75f);
		m_actorReady = true;
		m_beingDrawnByOpponent = false;
		string actorName = m_actorPath;
		m_actorWaitingToBeReplaced = m_actor;
		m_actorPath = handActorPath;
		m_actor = handActor;
		PowerTask cardDrawBlockingTask = GetPowerTaskToBlockCardDraw();
		if (cardDrawBlockingTask != null)
		{
			while (!cardDrawBlockingTask.IsCompleted())
			{
				yield return null;
			}
			if (handActor == null)
			{
				handActor = m_actor;
			}
		}
		if (m_entity.GetZone() != TAG_ZONE.HAND)
		{
			m_doNotSort = false;
			GameState.Get().ClearCardBeingDrawn(this);
			ResetCardDrawTimeScale();
		}
		else
		{
			m_actor = m_actorWaitingToBeReplaced;
			m_actorPath = actorName;
			m_actorWaitingToBeReplaced = null;
			m_beingDrawnByOpponent = true;
			yield return StartCoroutine(HideRevealedOpponentCard(handActor));
		}
	}

	private IEnumerator HideRevealedOpponentCard(Actor handActor)
	{
		float num = 0.5f;
		float num2 = 0.525f * num;
		if (!GetController().IsRevealed())
		{
			float num3 = 180f;
			TransformUtil.SetEulerAngleZ(m_actor.gameObject, 0f - num3);
			if (handActor != null)
			{
				iTween.RotateAdd(handActor.gameObject, iTween.Hash("z", num3, "time", num, "easetype", iTween.EaseType.easeInOutCubic));
			}
			iTween.RotateAdd(m_actor.gameObject, iTween.Hash("z", num3, "time", num, "easetype", iTween.EaseType.easeInOutCubic));
		}
		Action<object> action = delegate
		{
			if (handActor != null)
			{
				UnityEngine.Object.Destroy(handActor.gameObject);
			}
			m_actor.Show();
		};
		iTween.Timer(m_actor.gameObject, iTween.Hash("time", num2, "oncomplete", action));
		yield return new WaitForSeconds(num);
		m_doNotSort = false;
		m_beingDrawnByOpponent = false;
		GameState.Get().SetOpponentCardBeingDrawn(null);
		ResetCardDrawTimeScale();
		SoundManager.Get().LoadAndPlay("add_card_to_hand_1.prefab:bf6b149b859734c4faf9a96356c53646", base.gameObject);
		ActivateStateSpells();
		RefreshActor();
		m_zone.UpdateLayout();
	}

	private void AnimateDeckToPlay()
	{
		if (m_customSpawnSpellOverride == null)
		{
			((ZonePlay)m_zone).AddLayoutBlocker();
			ZoneDeck zoneDeck = ZoneMgr.Get().FindZoneOfType<ZoneDeck>(m_zone.m_Side);
			if (m_latestZoneChange != null && m_latestZoneChange.GetSourceControllerId() != 0 && m_latestZoneChange.GetSourceControllerId() != m_latestZoneChange.GetDestinationControllerId() && m_latestZoneChange.GetSourceZone() is ZoneDeck)
			{
				zoneDeck = (ZoneDeck)m_latestZoneChange.GetSourceZone();
			}
			zoneDeck.SetCardToInDeckState(this);
			m_doNotSort = true;
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(m_entity), AssetLoadingOptions.IgnorePrefabPosition);
			Actor component = gameObject.GetComponent<Actor>();
			SetupDeckToPlayActor(component, gameObject);
			SpellType spellType = m_cardDef.CardDef.DetermineSummonOutSpell_HandToPlay(this);
			Spell spell = component.GetSpell(spellType);
			GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", AssetLoadingOptions.IgnorePrefabPosition);
			Actor component2 = gameObject2.GetComponent<Actor>();
			SetupDeckToPlayActor(component2, gameObject2);
			StartCoroutine(AnimateDeckToPlay(component, spell, component2));
		}
		else
		{
			m_actor.Hide();
			ZonePlay obj = (ZonePlay)m_zone;
			SetTransitionStyle(ZoneTransitionStyle.INSTANT);
			obj.UpdateLayout();
			ActivateMinionSpawnEffects();
		}
	}

	private void SetupDeckToPlayActor(Actor actor, GameObject actorObject)
	{
		actor.SetEntity(m_entity);
		actor.SetCardDef(m_cardDef);
		actor.UpdateAllComponents();
		actorObject.transform.parent = base.transform;
		actorObject.transform.localPosition = Vector3.zero;
		actorObject.transform.localScale = Vector3.one;
		actorObject.transform.localRotation = Quaternion.identity;
	}

	private IEnumerator AnimateDeckToPlay(Actor cardFaceActor, Spell outSpell, Actor hiddenActor)
	{
		cardFaceActor.Hide();
		m_actor.Hide();
		hiddenActor.Hide();
		m_inputEnabled = false;
		SoundManager.Get().LoadAndPlay("draw_card_into_play.prefab:52139cc25c53e184fab47b23c72df0d1", base.gameObject);
		base.gameObject.transform.localEulerAngles = new Vector3(270f, 90f, 0f);
		iTween.MoveTo(base.gameObject, base.gameObject.transform.position + ABOVE_DECK_OFFSET, 0.6f);
		iTween.RotateTo(base.gameObject, iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "time", 0.7f, "delay", 0.6f, "easetype", iTween.EaseType.easeInOutCubic, "islocal", true));
		hiddenActor.Show();
		yield return new WaitForSeconds(0.4f);
		iTween.MoveTo(hiddenActor.gameObject, iTween.Hash("position", new Vector3(0f, 3f, 0f), "time", 1f, "delay", 0f, "islocal", true));
		m_doNotSort = false;
		ZonePlay obj = (ZonePlay)m_zone;
		obj.RemoveLayoutBlocker();
		obj.SetTransitionTime(1.6f);
		obj.UpdateLayout();
		yield return new WaitForSeconds(0.2f);
		float cardFlipTime = 0.35f;
		iTween.RotateTo(hiddenActor.gameObject, iTween.Hash("rotation", new Vector3(0f, 0f, -90f), "time", cardFlipTime, "delay", 0f, "easetype", iTween.EaseType.easeInCubic, "islocal", true));
		yield return new WaitForSeconds(cardFlipTime);
		hiddenActor.Destroy();
		cardFaceActor.Show();
		cardFaceActor.gameObject.transform.localPosition = new Vector3(0f, 3f, 0f);
		cardFaceActor.gameObject.transform.Rotate(new Vector3(0f, 0f, 90f));
		iTween.RotateTo(cardFaceActor.gameObject, iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "time", cardFlipTime, "delay", 0f, "easetype", iTween.EaseType.easeOutCubic, "islocal", true));
		m_actor.gameObject.transform.localPosition = new Vector3(0f, 2.86f, 0f);
		cardFaceActor.gameObject.transform.localPosition = new Vector3(0f, 2.86f, 0f);
		iTween.MoveTo(hiddenActor.gameObject, iTween.Hash("position", Vector3.zero, "time", 1f, "delay", 0f, "islocal", true));
		ActivateSpell(outSpell, OnSpellFinished_HandToPlay_SummonOut, null, OnSpellStateFinished_DestroyActor);
		ActivateCharacterPlayEffects();
		m_actor.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	public void SetSkipMilling(bool skipMilling)
	{
		m_skipMilling = skipMilling;
	}

	private void MillCard()
	{
		if (m_skipMilling)
		{
			m_actor.Hide();
		}
		else
		{
			StartCoroutine(MillCardWithTiming());
		}
	}

	private IEnumerator MillCardWithTiming()
	{
		SetDoNotSort(on: true);
		ReadyCardForDraw();
		Player cardOwner = m_entity.GetController();
		string text;
		if (cardOwner.IsFriendlySide())
		{
			while ((bool)GameState.Get().GetFriendlyCardBeingDrawn())
			{
				yield return null;
			}
			GameState.Get().SetFriendlyCardBeingDrawn(this);
			text = "FriendlyMillCard";
		}
		else
		{
			while ((bool)GameState.Get().GetOpponentCardBeingDrawn())
			{
				yield return null;
			}
			GameState.Get().SetOpponentCardBeingDrawn(this);
			text = "OpponentMillCard";
		}
		int turn = GameState.Get().GetTurn();
		if (turn != GameState.Get().GetLastTurnRemindedOfFullHand() && cardOwner.GetHandZone().GetCardCount() >= 10)
		{
			GameState.Get().SetLastTurnRemindedOfFullHand(turn);
			cardOwner.GetHeroCard().PlayEmote(EmoteType.ERROR_HAND_FULL);
		}
		m_actor.Show();
		m_actor.TurnOffCollider();
		Transform transform = Board.Get().FindBone(text);
		Vector3[] array = new Vector3[3]
		{
			base.gameObject.transform.position,
			base.gameObject.transform.position + ABOVE_DECK_OFFSET,
			transform.position
		};
		iTween.MoveTo(base.gameObject, iTween.Hash("path", array, "time", 1.5f, "easetype", iTween.EaseType.easeInSineOutExpo));
		base.gameObject.transform.localEulerAngles = new Vector3(270f, 270f, 0f);
		iTween.RotateTo(base.gameObject, iTween.Hash("rotation", new Vector3(0f, 0f, 357f), "time", 1.35f, "delay", 0.15f));
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", transform.localScale, "time", 0.75f, "delay", 0.15f));
		while (iTween.Count(base.gameObject) > 0)
		{
			yield return null;
		}
		m_actorReady = true;
		RefreshActor();
		Spell spell = m_actor.GetSpell(SpellType.HANDFULL);
		spell.AddStateFinishedCallback(OnSpellStateFinished_DestroyActor);
		spell.Activate();
		GameState.Get().ClearCardBeingDrawn(this);
		SetDoNotSort(on: false);
	}

	private void ActivateActorSpells_HandToPlay(Actor oldActor)
	{
		if (oldActor == null)
		{
			Debug.LogError($"{this}.ActivateActorSpells_HandToPlay() - oldActor=null");
			return;
		}
		if (m_cardDef == null)
		{
			Debug.LogError($"{this}.ActivateActorSpells_HandToPlay() - m_cardDef=null");
			return;
		}
		if (m_actor == null)
		{
			Debug.LogError($"{this}.ActivateActorSpells_HandToPlay() - m_actor=null");
			return;
		}
		DeactivateHandStateSpells(oldActor);
		SpellType spellType = m_cardDef.CardDef.DetermineSummonOutSpell_HandToPlay(this);
		Spell spell = oldActor.GetSpell(spellType);
		bool standard;
		if (spell == null)
		{
			Debug.LogError($"{this}.ActivateActorSpells_HandToPlay() - outSpell=null outSpellType={spellType}");
			m_actorReady = true;
		}
		else if (GetBestSummonSpell(out standard) == null)
		{
			Debug.LogError($"{this}.ActivateActorSpells_HandToPlay() - inSpell=null standard={standard}");
		}
		else
		{
			m_inputEnabled = false;
			spell.SetSource(base.gameObject);
			ActivateSpell(spell, OnSpellFinished_HandToPlay_SummonOut, oldActor, OnSpellStateFinished_DestroyActor);
		}
	}

	private void OnSpellFinished_HandToPlay_SummonOut(Spell spell, object userData)
	{
		Actor actor = userData as Actor;
		m_actor.Show();
		if (m_magneticPlayData != null)
		{
			SpellUtils.ActivateDeathIfNecessary(actor.GetSpellIfLoaded(SpellType.MAGNETIC_HAND_LINKED_RIGHT));
			ActivateActorSpell(SpellType.MAGNETIC_PLAY_LINKED_RIGHT);
		}
		bool standard;
		Spell bestSummonSpell = GetBestSummonSpell(out standard);
		if (bestSummonSpell == null)
		{
			Debug.LogErrorFormat("{0}.OnSpellFinished_HandToPlay_SummonOut() - inSpell=null standard={1}", this, standard);
			return;
		}
		if (!standard)
		{
			bestSummonSpell.AddStateFinishedCallback(OnSpellStateFinished_DestroySpell);
			SpellUtils.SetCustomSpellParent(bestSummonSpell, m_actor);
		}
		bestSummonSpell.AddFinishedCallback(OnSpellFinished_HandToPlay_SummonIn);
		bestSummonSpell.Activate();
	}

	private void OnSpellFinished_HandToPlay_SummonIn(Spell spell, object userData)
	{
		m_actorReady = true;
		m_inputEnabled = true;
		ActivateStateSpells();
		RefreshActor();
		if (m_entity.IsControlledByFriendlySidePlayer() && !m_entity.GetRealTimeIsDormant())
		{
			if (m_entity.HasSpellPower() || m_entity.HasSpellPowerDouble())
			{
				ZoneMgr.Get().OnSpellPowerEntityEnteredPlay(m_entity.GetSpellPowerSchool());
			}
			if (m_entity.HasHealingDoesDamageHint())
			{
				ZoneMgr.Get().OnHealingDoesDamageEntityEnteredPlay();
			}
			if (m_entity.HasLifestealDoesDamageHint())
			{
				ZoneMgr.Get().OnLifestealDoesDamageEntityEnteredPlay();
			}
		}
		if (m_entity.HasWindfury())
		{
			ActivateActorSpell(SpellType.WINDFURY_BURST);
		}
		StartCoroutine(ActivateActorBattlecrySpell());
		BoardEvents boardEvents = BoardEvents.Get();
		if (boardEvents != null)
		{
			boardEvents.SummonedEvent(this);
		}
	}

	private bool ActivateActorSpells_HandToWeapon(Actor oldActor)
	{
		if (oldActor == null)
		{
			Debug.LogError($"{this}.ActivateActorSpells_HandToWeapon() - oldActor=null");
			return false;
		}
		if (m_actor == null)
		{
			Debug.LogError($"{this}.ActivateActorSpells_HandToWeapon() - m_actor=null");
			return false;
		}
		DeactivateHandStateSpells(oldActor);
		oldActor.SetActorState(ActorStateType.CARD_IDLE);
		SpellType spellType = SpellType.SUMMON_OUT_WEAPON;
		Spell spell = oldActor.GetSpell(spellType);
		if (spell == null)
		{
			Debug.LogError($"{this}.ActivateActorSpells_HandToWeapon() - outSpell=null outSpellType={spellType}");
			return false;
		}
		Spell spell2 = m_customSummonSpell;
		if (spell2 == null)
		{
			SpellType spellType2 = (m_entity.IsControlledByFriendlySidePlayer() ? SpellType.SUMMON_IN_FRIENDLY : SpellType.SUMMON_IN_OPPONENT);
			spell2 = GetActorSpell(spellType2);
			if (spell2 == null)
			{
				Debug.LogError($"{this}.ActivateActorSpells_HandToWeapon() - inSpell=null inSpellType={spellType2}");
				return false;
			}
		}
		m_inputEnabled = false;
		ActivateSpell(spell, OnSpellFinished_HandToWeapon_SummonOut, spell2, OnSpellStateFinished_DestroyActor);
		return true;
	}

	private void OnSpellFinished_HandToWeapon_SummonOut(Spell spell, object userData)
	{
		m_actor.Show();
		Spell spell2 = m_customSummonSpell;
		if (spell2 == null)
		{
			spell2 = (Spell)userData;
		}
		else
		{
			spell2.AddStateFinishedCallback(OnSpellStateFinished_DestroySpell);
			SpellUtils.SetCustomSpellParent(spell2, m_actor);
		}
		ActivateSpell(spell2, OnSpellFinished_StandardCardSummon);
	}

	private void DiscardCardBeingDrawn()
	{
		if (this == GameState.Get().GetOpponentCardBeingDrawn())
		{
			m_actorWaitingToBeReplaced.Destroy();
			m_actorWaitingToBeReplaced = null;
		}
		if (m_actor.IsShown())
		{
			ActivateDeathSpell(m_actor);
		}
		else
		{
			GameState.Get().ClearCardBeingDrawn(this);
		}
	}

	private void DoDiscardAnimation()
	{
		ZoneHand zoneHand = m_prevZone as ZoneHand;
		m_actor.SetBlockTextComponentUpdate(block: true);
		m_doNotSort = true;
		iTween.Stop(base.gameObject);
		float num = 3f;
		if (GetEntity().IsControlledByOpposingSidePlayer())
		{
			num = 0f - num;
		}
		iTween.MoveTo(position: new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + num), target: base.gameObject, time: 3f);
		Vector3 vector = base.transform.localScale;
		if (zoneHand != null)
		{
			vector = zoneHand.GetCardScale();
		}
		iTween.ScaleTo(base.gameObject, vector * 1.5f, 3f);
		StartCoroutine(ActivateGraveyardActorDeathSpellAfterDelay(1f, 4f));
	}

	private bool DoPlayToHandTransition(Actor oldActor, bool wasInGraveyard = false, bool useFastAnimations = false)
	{
		bool num = ActivateActorSpells_PlayToHand(oldActor, wasInGraveyard, useFastAnimations);
		if (num)
		{
			m_actor.Hide();
		}
		return num;
	}

	private bool ActivateActorSpells_PlayToHand(Actor oldActor, bool wasInGraveyard, bool useFastAnimations)
	{
		if (oldActor == null)
		{
			Debug.LogError($"{this}.ActivateActorSpells_PlayToHand() - oldActor=null");
			return false;
		}
		if (m_actor == null)
		{
			Debug.LogError($"{this}.ActivateActorSpells_PlayToHand() - m_actor=null");
			return false;
		}
		SpellType spellType = (useFastAnimations ? SpellType.BOUNCE_OUT_FAST : SpellType.BOUNCE_OUT);
		Spell outSpell = oldActor.GetSpell(spellType);
		if (outSpell == null)
		{
			Debug.LogError($"{this}.ActivateActorSpells_PlayToHand() - outSpell=null outSpellType={spellType}");
			return false;
		}
		SpellType spellType2 = SpellType.BOUNCE_IN;
		if (m_actor.UseTechLevelManaGem())
		{
			spellType2 = SpellType.BOUNCE_IN_TECH_LEVEL;
		}
		else if (useFastAnimations)
		{
			spellType2 = SpellType.BOUNCE_IN_FAST;
		}
		Spell inSpell = GetActorSpell(spellType2);
		if (inSpell == null)
		{
			Debug.LogError($"{this}.ActivateActorSpells_PlayToHand() - inSpell=null inSpellType={spellType2}");
			return false;
		}
		m_inputEnabled = false;
		outSpell.SetSource(base.gameObject);
		if (m_entity.IsControlledByFriendlySidePlayer())
		{
			Spell.FinishedCallback finishedCallback = (wasInGraveyard ? new Spell.FinishedCallback(OnSpellFinished_PlayToHand_SummonOut_FromGraveyard) : new Spell.FinishedCallback(OnSpellFinished_PlayToHand_SummonOut));
			Spell.StateFinishedCallback callback = delegate(Spell spell, SpellStateType prevStateType, object userData)
			{
				if (prevStateType == SpellStateType.CANCEL)
				{
					ActivateSpell(outSpell, finishedCallback, inSpell, OnSpellStateFinished_DestroyActor);
				}
			};
			if (!CancelCustomSummonSpell(callback))
			{
				ActivateSpell(outSpell, finishedCallback, inSpell, OnSpellStateFinished_DestroyActor);
			}
		}
		else
		{
			if (m_entity.IsControlledByOpposingSidePlayer())
			{
				Log.FaceDownCard.Print("Card.ActivateActorSpells_PlayToHand() - {0} - {1} on {2}", this, spellType, oldActor);
				Log.FaceDownCard.Print("Card.ActivateActorSpells_PlayToHand() - {0} - {1} on {2}", this, spellType2, m_actor);
			}
			Spell.FinishedCallback finishedCallback2 = (wasInGraveyard ? ((Spell.FinishedCallback)delegate
			{
				ResumeLayoutForPlayZone();
			}) : null);
			ActivateSpell(outSpell, finishedCallback2, null, OnSpellStateFinished_PlayToHand_OldActor_SummonOut);
			ActivateSpell(inSpell, OnSpellFinished_PlayToHand_SummonIn);
		}
		return true;
	}

	private bool CancelCustomSummonSpell(Spell.StateFinishedCallback callback)
	{
		if (m_customSummonSpell == null)
		{
			return false;
		}
		if (!m_customSummonSpell.HasUsableState(SpellStateType.CANCEL))
		{
			return false;
		}
		if (m_customSummonSpell.GetActiveState() == SpellStateType.NONE)
		{
			return false;
		}
		if (m_customSummonSpell.GetActiveState() == SpellStateType.CANCEL)
		{
			return false;
		}
		m_customSummonSpell.AddStateFinishedCallback(callback);
		m_customSummonSpell.ActivateState(SpellStateType.CANCEL);
		return true;
	}

	private void OnSpellFinished_PlayToHand_SummonOut(Spell spell, object userData)
	{
		Spell spell2 = (Spell)userData;
		ActivateSpell(spell2, OnSpellFinished_StandardCardSummon);
	}

	private void OnSpellFinished_PlayToHand_SummonOut_FromGraveyard(Spell spell, object userData)
	{
		OnSpellFinished_PlayToHand_SummonOut(spell, userData);
		ResumeLayoutForPlayZone();
	}

	private void ResumeLayoutForPlayZone()
	{
		Player.Side side = (m_playZoneBlockerSide.HasValue ? m_playZoneBlockerSide.Value : m_zone.m_Side);
		m_playZoneBlockerSide = null;
		ZonePlay zonePlay = ZoneMgr.Get().FindZoneOfType<ZonePlay>(side);
		zonePlay.RemoveLayoutBlocker();
		zonePlay.UpdateLayout();
	}

	private void OnSpellStateFinished_PlayToHand_OldActor_SummonOut(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (m_entity.IsControlledByOpposingSidePlayer())
		{
			Log.FaceDownCard.Print("Card.OnSpellStateFinished_PlayToHand_OldActor_SummonOut() - {0} stateType={1}", this, spell.GetActiveState());
		}
		OnSpellStateFinished_DestroyActor(spell, prevStateType, userData);
	}

	private void OnSpellFinished_PlayToHand_SummonIn(Spell spell, object userData)
	{
		if (m_entity.IsControlledByOpposingSidePlayer())
		{
			Log.FaceDownCard.Print("Card.OnSpellFinished_PlayToHand_SummonIn() - {0}", this);
		}
		OnSpellFinished_StandardCardSummon(spell, userData);
	}

	private IEnumerator DoHandToDeckTransition(Actor handActor)
	{
		m_doNotSort = true;
		DeactivateHandStateSpells();
		ZoneDeck deckZone = m_zone as ZoneDeck;
		ZoneHand handZone = m_prevZone as ZoneHand;
		deckZone.AddLayoutBlocker();
		float num = (handZone.GetController().IsFriendlySide() ? 3f : (-3f));
		iTween.MoveTo(position: new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + num), target: base.gameObject, time: 1.75f);
		iTween.ScaleTo(base.gameObject, base.transform.localScale * 1.5f, 1.75f);
		yield return new WaitForSeconds(1.85f);
		yield return AnimatePlayToDeck(base.gameObject, deckZone, !handZone.GetController().IsFriendlySide());
		handActor.Destroy();
		m_actorReady = true;
		m_doNotSort = false;
		deckZone.RemoveLayoutBlocker();
		deckZone.UpdateLayout();
	}

	private void DoPlayToDeckTransition(Actor playActor)
	{
		m_doNotSort = true;
		m_actor.Hide();
		StartCoroutine(AnimatePlayToDeck(playActor));
	}

	private IEnumerator AnimatePlayToDeck(Actor playActor)
	{
		Actor handActor = null;
		bool loadingActor = true;
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			loadingActor = false;
			if (go == null)
			{
				Error.AddDevFatal("Card.AnimatePlayToGraveyardToDeck() - failed to load {0}", assetRef);
			}
			else
			{
				handActor = go.GetComponent<Actor>();
				if (handActor == null)
				{
					Error.AddDevFatal("Card.AnimatePlayToGraveyardToDeck() - instance of {0} has no Actor component", base.name);
				}
			}
		};
		AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(m_entity), callback, null, AssetLoadingOptions.IgnorePrefabPosition);
		while (loadingActor)
		{
			yield return null;
		}
		if (handActor == null)
		{
			playActor.Destroy();
			yield break;
		}
		handActor.SetEntity(m_entity);
		handActor.SetCardDef(m_cardDef);
		handActor.UpdateAllComponents();
		handActor.transform.parent = playActor.GetCard().transform;
		TransformUtil.Identity(handActor);
		handActor.Hide();
		SpellType spellType = SpellType.SUMMON_OUT;
		Spell spell2 = playActor.GetSpell(spellType);
		if (spell2 == null)
		{
			Error.AddDevFatal("{0}.AnimatePlayToGraveyardToDeck() - outSpell=null outSpellType={1}", this, spellType);
			yield break;
		}
		SpellType spellType2 = SpellType.SUMMON_IN;
		Spell inSpell = handActor.GetSpell(spellType2);
		if (inSpell == null)
		{
			Error.AddDevFatal("{0}.AnimatePlayToGraveyardToDeck() - inSpell=null inSpellType={1}", this, spellType2);
			yield break;
		}
		bool waitForSpells = true;
		Spell.FinishedCallback callback2 = delegate
		{
			waitForSpells = false;
		};
		Spell.StateFinishedCallback callback3 = delegate(Spell spell, SpellStateType prevStateType, object userData)
		{
			if (spell.GetActiveState() == SpellStateType.NONE)
			{
				playActor.Destroy();
			}
		};
		Spell.FinishedCallback callback4 = delegate
		{
			inSpell.Activate();
			ResumeLayoutForPlayZone();
		};
		inSpell.AddFinishedCallback(callback2);
		spell2.AddFinishedCallback(callback4);
		spell2.AddStateFinishedCallback(callback3);
		PrepareForDeathAnimation(playActor);
		spell2.Activate();
		while (waitForSpells)
		{
			yield return 0;
		}
		ZoneDeck deckZone = (ZoneDeck)m_zone;
		yield return StartCoroutine(AnimatePlayToDeck(base.gameObject, deckZone));
		handActor.Destroy();
		m_actorReady = true;
		m_doNotSort = false;
		deckZone.RemoveLayoutBlocker();
		deckZone.UpdateLayout();
	}

	public IEnumerator AnimatePlayToDeck(GameObject mover, ZoneDeck deckZone, bool hideBackSide = false, float timeScale = 1f)
	{
		SoundManager.Get().LoadAndPlay("MinionToDeck_transition.prefab:8063f1b133f28e34aaeade8fcabe250c");
		Vector3 vector = deckZone.GetThicknessForLayout().GetMeshRenderer().bounds.center + IN_DECK_OFFSET;
		Vector3 vector2 = vector + ABOVE_DECK_OFFSET;
		Vector3 vector3 = new Vector3(0f, IN_DECK_ANGLES.y, 0f);
		Vector3 iN_DECK_ANGLES = IN_DECK_ANGLES;
		Vector3 iN_DECK_SCALE = IN_DECK_SCALE;
		float num = 0.3f;
		if (hideBackSide)
		{
			vector3.y = (iN_DECK_ANGLES.y = 0f - IN_DECK_ANGLES.y);
			num = 0.5f;
		}
		float num2 = 1f;
		if (timeScale > 0f)
		{
			num2 *= 1f / timeScale;
		}
		Actor component = mover.GetComponent<Actor>();
		iTween.MoveTo(mover, iTween.Hash("position", vector2, "delay", 0f * num2, "time", 0.7f * num2, "easetype", iTween.EaseType.easeInOutCubic));
		iTween.RotateTo(mover, iTween.Hash("rotation", vector3, "delay", 0f * num2, "time", 0.2f * num2, "easetype", iTween.EaseType.easeInOutCubic));
		iTween.MoveTo(mover, iTween.Hash("position", vector, "delay", 0.7f * num2, "time", 0.7f * num2, "easetype", iTween.EaseType.easeOutCubic));
		iTween.ScaleTo(mover, iTween.Hash("scale", iN_DECK_SCALE, "delay", 0.7f * num2, "time", 0.6f * num2, "easetype", iTween.EaseType.easeInCubic));
		if (base.gameObject != null && component != null)
		{
			iTween.RotateTo(mover, iTween.Hash("rotation", iN_DECK_ANGLES, "delay", 0.2f * num2, "time", num * num2, "easetype", iTween.EaseType.easeOutCubic, "oncomplete", "OnCardRotateIntoDeckComplete", "oncompleteparams", component, "oncompletetarget", base.gameObject));
		}
		else
		{
			iTween.RotateTo(mover, iTween.Hash("rotation", iN_DECK_ANGLES, "delay", 0.2f * num2, "time", num * num2, "easetype", iTween.EaseType.easeOutCubic));
		}
		while (iTween.HasTween(mover))
		{
			yield return 0;
		}
	}

	private void OnCardRotateIntoDeckComplete(Actor cardActor)
	{
		if (base.gameObject != null && cardActor != null)
		{
			if (cardActor.m_eliteObject != null)
			{
				cardActor.m_eliteObject.SetActive(value: false);
			}
			if (cardActor.m_portraitMesh != null)
			{
				cardActor.m_portraitMesh.SetActive(value: false);
			}
			if (cardActor.m_manaObject != null)
			{
				cardActor.m_manaObject.SetActive(value: false);
			}
			if (cardActor.m_costTextMesh != null)
			{
				cardActor.m_costTextMesh.Hide();
			}
		}
	}

	public void SetSecretTriggered(bool set)
	{
		m_secretTriggered = set;
	}

	public bool WasSecretTriggered()
	{
		return m_secretTriggered;
	}

	public bool CanShowSecretTrigger()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return true;
		}
		if (m_zone.IsOnlyCard(this))
		{
			return true;
		}
		return false;
	}

	public void ShowSecretTrigger()
	{
		m_actor.GetComponent<Spell>().ActivateState(SpellStateType.ACTION);
	}

	private bool CanShowSecretZoneCard()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return true;
		}
		ZoneSecret zoneSecret = m_zone as ZoneSecret;
		if (zoneSecret == null)
		{
			return false;
		}
		if (m_entity != null && m_entity.IsQuest())
		{
			return true;
		}
		if (m_entity != null && m_entity.IsPuzzle())
		{
			return true;
		}
		if (m_entity != null && m_entity.IsRulebook())
		{
			return true;
		}
		if (m_entity != null && m_entity.IsSigil())
		{
			return true;
		}
		if (zoneSecret.GetSecretCards().IndexOf(this) == 0)
		{
			return true;
		}
		if (zoneSecret.GetSideQuestCards().IndexOf(this) == 0)
		{
			return true;
		}
		return false;
	}

	private void ShowSecretQuestBirth()
	{
		Spell component = m_actor.GetComponent<Spell>();
		if (!CanShowSecretZoneCard())
		{
			Spell.StateFinishedCallback callback = delegate(Spell thisSpell, SpellStateType prevStateType, object userData)
			{
				if (thisSpell.GetActiveState() == SpellStateType.NONE && !CanShowSecretZoneCard())
				{
					HideCard();
				}
			};
			component.AddStateFinishedCallback(callback);
		}
		component.ActivateState(SpellStateType.BIRTH);
	}

	public bool CanShowSecretDeath()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return true;
		}
		if (m_prevZone.GetCardCount() == 0)
		{
			return true;
		}
		return false;
	}

	public void ShowSecretDeath(Actor oldActor)
	{
		Spell component = oldActor.GetComponent<Spell>();
		if (m_secretTriggered)
		{
			m_secretTriggered = false;
			if (component.GetActiveState() == SpellStateType.NONE)
			{
				oldActor.Destroy();
			}
			else
			{
				component.AddStateFinishedCallback(OnSpellStateFinished_DestroyActor);
			}
			return;
		}
		component.AddStateFinishedCallback(OnSpellStateFinished_DestroyActor);
		component.ActivateState(SpellStateType.ACTION);
		oldActor.transform.parent = null;
		m_doNotSort = true;
		if (!UniversalInputManager.UsePhoneUI)
		{
			iTween.Stop(base.gameObject);
			m_actor.Hide();
			StartCoroutine(WaitAndThenShowDestroyedSecret());
		}
	}

	private IEnumerator WaitAndThenShowDestroyedSecret()
	{
		yield return new WaitForSeconds(0.5f);
		float num = 2f;
		if (GetEntity().IsControlledByOpposingSidePlayer())
		{
			num = 0f - num;
		}
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z + num);
		m_actor.Show();
		iTween.MoveTo(base.gameObject, position, 3f);
		base.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
		base.transform.localEulerAngles = new Vector3(0f, 0f, 357f);
		iTween.ScaleTo(base.gameObject, new Vector3(1.25f, 0.2f, 1.25f), 3f);
		StartCoroutine(ActivateGraveyardActorDeathSpellAfterDelay(1f, 4f));
	}

	private IEnumerator SwitchSecretSides()
	{
		m_doNotSort = true;
		Actor newActor = null;
		bool loadingActor = true;
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			loadingActor = false;
			if (go == null)
			{
				Error.AddDevFatal("Card.SwitchSecretSides() - failed to load {0}", assetRef);
			}
			else
			{
				newActor = go.GetComponent<Actor>();
				if (newActor == null)
				{
					Error.AddDevFatal("Card.SwitchSecretSides() - instance of {0} has no Actor component", base.name);
				}
			}
		};
		AssetLoader.Get().InstantiatePrefab(m_actorPath, callback, null, AssetLoadingOptions.IgnorePrefabPosition);
		while (loadingActor)
		{
			yield return null;
		}
		if ((bool)newActor)
		{
			Actor oldActor = m_actor;
			m_actor = newActor;
			m_actor.SetEntity(m_entity);
			m_actor.SetCard(this);
			m_actor.SetCardDef(m_cardDef);
			m_actor.UpdateAllComponents();
			m_actor.transform.parent = oldActor.transform.parent;
			TransformUtil.Identity(m_actor);
			m_actor.Hide();
			if (!CanShowSecretDeath())
			{
				oldActor.Destroy();
			}
			else
			{
				oldActor.transform.parent = base.transform.parent;
				m_transitionStyle = ZoneTransitionStyle.INSTANT;
				bool oldActorFinished = false;
				Spell.FinishedCallback callback2 = delegate
				{
					oldActorFinished = true;
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
				while (!oldActorFinished)
				{
					yield return null;
				}
			}
			m_shown = true;
			m_actor.Show();
			ShowSecretQuestBirth();
		}
		m_actorReady = true;
		m_doNotSort = false;
		m_zone.UpdateLayout();
		ActivateStateSpells();
	}

	private bool ShouldCardDrawWaitForTurnStartSpells()
	{
		SpellController spellController = TurnStartManager.Get().GetSpellController();
		if (spellController == null)
		{
			return false;
		}
		if (spellController.IsSource(this))
		{
			return true;
		}
		if (spellController.IsTarget(this))
		{
			return true;
		}
		return false;
	}

	private IEnumerator WaitForCardDrawBlockingTurnStartSpells()
	{
		while (ShouldCardDrawWaitForTurnStartSpells())
		{
			yield return null;
		}
	}

	private PowerTask GetPowerTaskToBlockCardDraw()
	{
		if (m_latestZoneChange == null)
		{
			return null;
		}
		PowerTaskList taskList = m_latestZoneChange.GetParentList().GetTaskList();
		if (taskList == null)
		{
			return null;
		}
		if (taskList.IsEndOfBlock() && taskList.IsComplete())
		{
			return null;
		}
		PowerTask blockingTask = null;
		PowerTaskList currentTaskList = GameState.Get().GetPowerProcessor().GetCurrentTaskList();
		if (currentTaskList != null && currentTaskList.IsDescendantOfBlock(taskList))
		{
			DoesTaskListBlockCardDraw(currentTaskList, out blockingTask);
		}
		foreach (PowerTaskList item in GameState.Get().GetPowerProcessor().GetPowerQueue())
		{
			if (item.IsDescendantOfBlock(taskList) && DoesTaskListBlockCardDraw(item, out var blockingTask2))
			{
				if (!CanPowerTaskListBlockCardDraw(item))
				{
					return blockingTask;
				}
				blockingTask = blockingTask2;
			}
		}
		return blockingTask;
	}

	private bool CanPowerTaskListBlockCardDraw(PowerTaskList blockingPowerTaskList)
	{
		PowerTaskList currentTaskList = GameState.Get().GetPowerProcessor().GetCurrentTaskList();
		if (currentTaskList != null && (currentTaskList.HasCardDraw() || currentTaskList.HasCardMill() || currentTaskList.HasFatigue()))
		{
			return false;
		}
		foreach (PowerTaskList item in GameState.Get().GetPowerProcessor().GetPowerQueue())
		{
			if (item == blockingPowerTaskList)
			{
				break;
			}
			if (item.HasCardDraw() || item.HasCardMill() || item.HasFatigue())
			{
				return false;
			}
		}
		return true;
	}

	private bool DoesTaskListBlockCardDraw(PowerTaskList taskList, out PowerTask blockingTask)
	{
		blockingTask = GetPowerTaskBlockingCardDraw(taskList);
		if (blockingTask == null)
		{
			return false;
		}
		foreach (PowerTask task in taskList.GetTaskList())
		{
			if (task == blockingTask)
			{
				break;
			}
			if (task.IsCardDraw() || task.IsCardMill() || task.IsFatigue())
			{
				blockingTask = null;
				return false;
			}
		}
		return true;
	}

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
		int entityId = m_entity.GetEntityId();
		List<PowerTask> taskList2 = taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			PowerTask powerTask = taskList2[i];
			if (powerTask.IsCompleted())
			{
				continue;
			}
			Network.PowerHistory power = powerTask.GetPower();
			int num = 0;
			switch (power.Type)
			{
			case Network.PowerType.META_DATA:
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType == HistoryMeta.Type.HOLD_DRAWN_CARD && histMetaData.Info.Count == 1 && histMetaData.Info[0] == entityId)
				{
					return powerTask;
				}
				break;
			}
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
		return null;
	}

	private void CutoffFriendlyCardDraw()
	{
		if (!m_actorReady)
		{
			if (m_actorWaitingToBeReplaced != null)
			{
				m_actorWaitingToBeReplaced.Destroy();
				m_actorWaitingToBeReplaced = null;
			}
			m_actor.Show();
			m_actor.TurnOffCollider();
			m_doNotSort = false;
			m_actorReady = true;
			ActivateStateSpells();
			RefreshActor();
			GameState.Get().ClearCardBeingDrawn(this);
			m_zone.UpdateLayout();
		}
	}

	private IEnumerator WaitAndPrepareForDeathAnimation(Actor dyingActor)
	{
		yield return new WaitForSeconds(m_keywordDeathDelaySec);
		PrepareForDeathAnimation(dyingActor);
	}

	private void PrepareForDeathAnimation(Actor dyingActor)
	{
		dyingActor.ToggleCollider(enabled: false);
		dyingActor.ToggleForceIdle(bOn: true);
		dyingActor.SetActorState(ActorStateType.CARD_IDLE);
		dyingActor.DoCardDeathVisuals();
		DeactivateCustomKeywordEffect();
	}

	private IEnumerator ActivateGraveyardActorDeathSpellAfterDelay(float predelay, float postdelay, ActivateGraveyardActorDeathSpellAfterDelayCallback finishedCallback = null)
	{
		m_actor.DoCardDeathVisuals();
		Spell chosenSpell = GetBestDeathSpell();
		if (chosenSpell.DoesBlockServerEvents())
		{
			GameState.Get().AddServerBlockingSpell(chosenSpell);
		}
		yield return new WaitForSeconds(predelay);
		ActivateSpell(chosenSpell, null);
		CleanUpCustomSpell(chosenSpell, ref m_customDiscardSpell);
		CleanUpCustomSpell(chosenSpell, ref m_customDiscardSpellOverride);
		yield return new WaitForSeconds(postdelay);
		m_doNotSort = false;
		m_actor.SetBlockTextComponentUpdate(block: false);
		finishedCallback?.Invoke();
	}

	private bool HandlePlayActorDeath(Actor oldActor)
	{
		bool result = false;
		if (!m_cardDef.CardDef.m_SuppressDeathrattleDeath && m_entity.HasDeathrattle() && !m_entity.IsDeathrattleDisabled())
		{
			ActivateActorSpell(oldActor, SpellType.DEATHRATTLE_DEATH);
		}
		if (!m_cardDef.CardDef.m_SuppressDeathrattleDeath && m_entity.HasTag(GAME_TAG.REBORN))
		{
			ActivateActorSpell(oldActor, SpellType.REBORN_DEATH);
		}
		if (m_suppressDeathEffects)
		{
			if ((bool)oldActor)
			{
				oldActor.Destroy();
			}
			if (IsShown())
			{
				ShowImpl();
			}
			else
			{
				HideImpl();
			}
			result = true;
			m_actorReady = true;
		}
		else
		{
			if (!m_suppressKeywordDeaths)
			{
				StartCoroutine(WaitAndPrepareForDeathAnimation(oldActor));
			}
			if (ActivateDeathSpell(oldActor) != null)
			{
				m_actor.Hide();
				result = true;
				m_actorReady = true;
			}
		}
		return result;
	}

	private bool DoesCardReturnFromGraveyard()
	{
		foreach (PowerTaskList item in GameState.Get().GetPowerProcessor().GetPowerQueue())
		{
			if (DoesTaskListReturnCardFromGraveyard(item))
			{
				Log.Gameplay.PrintInfo("Found the task for returning entity {0} from graveyard!", m_entity);
				return true;
			}
		}
		return false;
	}

	private bool DoesTaskListReturnCardFromGraveyard(PowerTaskList taskList)
	{
		if (!taskList.IsTriggerBlock())
		{
			return false;
		}
		foreach (PowerTask task in taskList.GetTaskList())
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type != Network.PowerType.TAG_CHANGE)
			{
				continue;
			}
			Network.HistTagChange histTagChange = power as Network.HistTagChange;
			if (histTagChange.Tag == 49 && histTagChange.Entity == m_entity.GetEntityId())
			{
				if (histTagChange.Value == 6)
				{
					return false;
				}
				return true;
			}
		}
		return false;
	}

	private int GetCardFutureController()
	{
		foreach (PowerTaskList item in GameState.Get().GetPowerProcessor().GetPowerQueue())
		{
			int cardFutureControllerFromTaskList = GetCardFutureControllerFromTaskList(item);
			if (cardFutureControllerFromTaskList != m_entity.GetControllerId())
			{
				return cardFutureControllerFromTaskList;
			}
		}
		return m_entity.GetControllerId();
	}

	private int GetCardFutureControllerFromTaskList(PowerTaskList taskList)
	{
		foreach (PowerTask task in taskList.GetTaskList())
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = power as Network.HistTagChange;
				if (histTagChange.Tag == 50 && histTagChange.Entity == m_entity.GetEntityId())
				{
					return histTagChange.Value;
				}
			}
		}
		return m_entity.GetControllerId();
	}

	public void SetDelayBeforeHideInNullZoneVisuals(float delay)
	{
		m_delayBeforeHideInNullZoneVisuals = delay;
	}

	private void DoNullZoneVisuals()
	{
		StartCoroutine(DoNullZoneVisualsWithTiming());
	}

	private IEnumerator DoNullZoneVisualsWithTiming()
	{
		if (m_delayBeforeHideInNullZoneVisuals > 0f)
		{
			yield return new WaitForSeconds(m_delayBeforeHideInNullZoneVisuals);
		}
		Spell nullZoneSpell = GetBestNullZoneSpell();
		if (nullZoneSpell != null)
		{
			nullZoneSpell.Activate();
			while (nullZoneSpell.GetActiveState() != 0)
			{
				yield return null;
			}
		}
		if (m_actor != null)
		{
			m_actor.DeactivateAllSpells();
		}
		HideCard();
	}

	private Spell GetBestNullZoneSpell()
	{
		if (m_entity.HasTag(GAME_TAG.GHOSTLY) && GetControllerSide() == Player.Side.FRIENDLY && m_prevZone is ZoneHand && m_actor != null)
		{
			return m_actor.GetSpell(SpellType.GHOSTLY_DEATH);
		}
		if (m_entity.IsSpell() && m_prevZone is ZoneHand && m_actor != null && m_zone is ZoneGraveyard)
		{
			return m_actor.GetSpell(SpellType.POWER_UP);
		}
		return null;
	}

	public void SetDrawTimeScale(float scale)
	{
		m_drawTimeScale = scale;
	}

	public bool HasSameCardDef(CardDef cardDef)
	{
		return m_cardDef?.CardDef == cardDef;
	}

	public T GetCardDefComponent<T>()
	{
		if (!HasCardDef)
		{
			return default(T);
		}
		return m_cardDef.CardDef.GetComponent<T>();
	}
}
