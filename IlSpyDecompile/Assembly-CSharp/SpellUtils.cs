using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellUtils
{
	public static SpellClassTag ConvertClassTagToSpellEnum(TAG_CLASS classTag)
	{
		return classTag switch
		{
			TAG_CLASS.DEATHKNIGHT => SpellClassTag.DEATHKNIGHT, 
			TAG_CLASS.DRUID => SpellClassTag.DRUID, 
			TAG_CLASS.HUNTER => SpellClassTag.HUNTER, 
			TAG_CLASS.MAGE => SpellClassTag.MAGE, 
			TAG_CLASS.PALADIN => SpellClassTag.PALADIN, 
			TAG_CLASS.PRIEST => SpellClassTag.PRIEST, 
			TAG_CLASS.ROGUE => SpellClassTag.ROGUE, 
			TAG_CLASS.SHAMAN => SpellClassTag.SHAMAN, 
			TAG_CLASS.WARLOCK => SpellClassTag.WARLOCK, 
			TAG_CLASS.WARRIOR => SpellClassTag.WARRIOR, 
			_ => SpellClassTag.NONE, 
		};
	}

	public static Player.Side ConvertSpellSideToPlayerSide(Spell spell, SpellPlayerSide spellSide)
	{
		Entity entity = spell.GetSourceCard().GetEntity();
		switch (spellSide)
		{
		case SpellPlayerSide.FRIENDLY:
			return Player.Side.FRIENDLY;
		case SpellPlayerSide.OPPONENT:
			return Player.Side.OPPOSING;
		case SpellPlayerSide.SOURCE:
			if (entity.IsControlledByFriendlySidePlayer())
			{
				return Player.Side.FRIENDLY;
			}
			return Player.Side.OPPOSING;
		case SpellPlayerSide.TARGET:
			if (entity.IsControlledByFriendlySidePlayer())
			{
				return Player.Side.OPPOSING;
			}
			return Player.Side.FRIENDLY;
		default:
			return Player.Side.NEUTRAL;
		}
	}

	public static List<Zone> FindZonesFromTag(SpellZoneTag zoneTag)
	{
		ZoneMgr zoneMgr = ZoneMgr.Get();
		if (zoneMgr == null)
		{
			return null;
		}
		switch (zoneTag)
		{
		case SpellZoneTag.PLAY:
			return zoneMgr.FindZonesOfType<Zone, ZonePlay>();
		case SpellZoneTag.HERO:
			return zoneMgr.FindZonesOfType<Zone, ZoneHero>();
		case SpellZoneTag.HERO_POWER:
			return zoneMgr.FindZonesOfType<Zone, ZoneHeroPower>();
		case SpellZoneTag.WEAPON:
			return zoneMgr.FindZonesOfType<Zone, ZoneWeapon>();
		case SpellZoneTag.DECK:
			return zoneMgr.FindZonesOfType<Zone, ZoneDeck>();
		case SpellZoneTag.HAND:
			return zoneMgr.FindZonesOfType<Zone, ZoneHand>();
		case SpellZoneTag.GRAVEYARD:
			return zoneMgr.FindZonesOfType<Zone, ZoneGraveyard>();
		case SpellZoneTag.SECRET:
			return zoneMgr.FindZonesOfType<Zone, ZoneSecret>();
		default:
			Debug.LogWarning($"SpellUtils.FindZonesFromTag() - unhandled zoneTag {zoneTag}");
			return null;
		}
	}

	public static List<Zone> FindZonesFromTag(Spell spell, SpellZoneTag zoneTag, SpellPlayerSide spellSide)
	{
		if (ZoneMgr.Get() == null)
		{
			return null;
		}
		switch (spellSide)
		{
		case SpellPlayerSide.NEUTRAL:
			return null;
		case SpellPlayerSide.BOTH:
			return FindZonesFromTag(zoneTag);
		default:
		{
			Player.Side side = ConvertSpellSideToPlayerSide(spell, spellSide);
			switch (zoneTag)
			{
			case SpellZoneTag.PLAY:
				return ZoneMgr.Get().FindZonesOfType<Zone, ZonePlay>(side);
			case SpellZoneTag.HERO:
				return ZoneMgr.Get().FindZonesOfType<Zone, ZoneHero>(side);
			case SpellZoneTag.HERO_POWER:
				return ZoneMgr.Get().FindZonesOfType<Zone, ZoneHeroPower>(side);
			case SpellZoneTag.WEAPON:
				return ZoneMgr.Get().FindZonesOfType<Zone, ZoneWeapon>(side);
			case SpellZoneTag.DECK:
				return ZoneMgr.Get().FindZonesOfType<Zone, ZoneDeck>(side);
			case SpellZoneTag.HAND:
				return ZoneMgr.Get().FindZonesOfType<Zone, ZoneHand>(side);
			case SpellZoneTag.GRAVEYARD:
				return ZoneMgr.Get().FindZonesOfType<Zone, ZoneGraveyard>(side);
			case SpellZoneTag.SECRET:
				return ZoneMgr.Get().FindZonesOfType<Zone, ZoneSecret>(side);
			default:
				Debug.LogWarning($"SpellUtils.FindZonesFromTag() - Unhandled zoneTag {zoneTag}. spellSide={spellSide} playerSide={side}");
				return null;
			}
		}
		}
	}

	public static Transform GetLocationTransform(Spell spell)
	{
		GameObject locationObject = GetLocationObject(spell);
		if (!(locationObject == null))
		{
			return locationObject.transform;
		}
		return null;
	}

	public static GameObject GetLocationObject(Spell spell)
	{
		SpellLocation location = spell.GetLocation();
		return GetSpellLocationObject(spell, location);
	}

	public static GameObject GetSpellLocationObject(Spell spell, SpellLocation location, string overrideTransformName = null)
	{
		if (location == SpellLocation.NONE)
		{
			return null;
		}
		GameObject gameObject = null;
		switch (location)
		{
		case SpellLocation.SOURCE:
			gameObject = spell.GetSource();
			break;
		case SpellLocation.SOURCE_AUTO:
			gameObject = FindSourceAutoObjectForSpell(spell);
			break;
		case SpellLocation.SOURCE_HERO:
		{
			Card card2 = FindHeroCard(spell.GetSourceCard());
			if (card2 == null)
			{
				return null;
			}
			gameObject = card2.gameObject;
			break;
		}
		case SpellLocation.SOURCE_HERO_POWER:
		{
			Card card4 = FindHeroPowerCard(spell.GetSourceCard());
			if (card4 == null)
			{
				return null;
			}
			gameObject = card4.gameObject;
			break;
		}
		case SpellLocation.SOURCE_PLAY_ZONE:
		{
			Card sourceCard = spell.GetSourceCard();
			if (sourceCard == null)
			{
				return null;
			}
			Player controller = sourceCard.GetEntity().GetController();
			ZonePlay zonePlay = ZoneMgr.Get().FindZoneOfType<ZonePlay>(controller.GetSide());
			if (zonePlay == null)
			{
				return null;
			}
			gameObject = zonePlay.gameObject;
			break;
		}
		case SpellLocation.SOURCE_HAND_ZONE:
		{
			Card sourceCard2 = spell.GetSourceCard();
			if (sourceCard2 == null)
			{
				return null;
			}
			Player controller3 = sourceCard2.GetEntity().GetController();
			ZoneHand zoneHand3 = ZoneMgr.Get().FindZoneOfType<ZoneHand>(controller3.GetSide());
			if (zoneHand3 == null)
			{
				return null;
			}
			gameObject = zoneHand3.gameObject;
			break;
		}
		case SpellLocation.SOURCE_DECK_ZONE:
		{
			Card sourceCard3 = spell.GetSourceCard();
			if (sourceCard3 == null)
			{
				return null;
			}
			Player controller5 = sourceCard3.GetEntity().GetController();
			ZoneDeck zoneDeck3 = ZoneMgr.Get().FindZoneOfType<ZoneDeck>(controller5.GetSide());
			if (zoneDeck3 == null)
			{
				return null;
			}
			gameObject = zoneDeck3.gameObject;
			break;
		}
		case SpellLocation.TARGET:
			gameObject = spell.GetVisualTarget();
			break;
		case SpellLocation.TARGET_AUTO:
			gameObject = FindTargetAutoObjectForSpell(spell);
			break;
		case SpellLocation.TARGET_HERO:
		{
			Card card3 = FindHeroCard(spell.GetVisualTargetCard());
			if (card3 == null)
			{
				return null;
			}
			gameObject = card3.gameObject;
			break;
		}
		case SpellLocation.TARGET_HERO_POWER:
		{
			Card card = FindHeroPowerCard(spell.GetVisualTargetCard());
			if (card == null)
			{
				return null;
			}
			gameObject = card.gameObject;
			break;
		}
		case SpellLocation.TARGET_PLAY_ZONE:
		{
			Card visualTargetCard2 = spell.GetVisualTargetCard();
			if (visualTargetCard2 == null)
			{
				return null;
			}
			Player controller4 = visualTargetCard2.GetEntity().GetController();
			ZonePlay zonePlay3 = ZoneMgr.Get().FindZoneOfType<ZonePlay>(controller4.GetSide());
			if (zonePlay3 == null)
			{
				return null;
			}
			gameObject = zonePlay3.gameObject;
			break;
		}
		case SpellLocation.TARGET_HAND_ZONE:
		{
			Card visualTargetCard = spell.GetVisualTargetCard();
			if (visualTargetCard == null)
			{
				return null;
			}
			Player controller2 = visualTargetCard.GetEntity().GetController();
			ZoneHand zoneHand2 = ZoneMgr.Get().FindZoneOfType<ZoneHand>(controller2.GetSide());
			if (zoneHand2 == null)
			{
				return null;
			}
			gameObject = zoneHand2.gameObject;
			break;
		}
		case SpellLocation.TARGET_DECK_ZONE:
		{
			Card visualTargetCard3 = spell.GetVisualTargetCard();
			if (visualTargetCard3 == null)
			{
				return null;
			}
			Player controller6 = visualTargetCard3.GetEntity().GetController();
			ZoneDeck zoneDeck4 = ZoneMgr.Get().FindZoneOfType<ZoneDeck>(controller6.GetSide());
			if (zoneDeck4 == null)
			{
				return null;
			}
			gameObject = zoneDeck4.gameObject;
			break;
		}
		case SpellLocation.BOARD:
			if (Board.Get() == null)
			{
				return null;
			}
			gameObject = Board.Get().gameObject;
			break;
		case SpellLocation.FRIENDLY_HERO:
		{
			Player player5 = FindFriendlyPlayer(spell);
			if (player5 == null)
			{
				return null;
			}
			Card heroCard = player5.GetHeroCard();
			if (!heroCard)
			{
				return null;
			}
			gameObject = heroCard.gameObject;
			break;
		}
		case SpellLocation.FRIENDLY_HERO_POWER:
		{
			Player player3 = FindFriendlyPlayer(spell);
			if (player3 == null)
			{
				return null;
			}
			Card heroPowerCard = player3.GetHeroPowerCard();
			if (!heroPowerCard)
			{
				return null;
			}
			gameObject = heroPowerCard.gameObject;
			break;
		}
		case SpellLocation.FRIENDLY_PLAY_ZONE:
		{
			ZonePlay zonePlay4 = FindFriendlyPlayZone(spell);
			if (!zonePlay4)
			{
				return null;
			}
			gameObject = zonePlay4.gameObject;
			break;
		}
		case SpellLocation.OPPONENT_HERO:
		{
			Player player9 = FindOpponentPlayer(spell);
			if (player9 == null)
			{
				return null;
			}
			Card heroCard2 = player9.GetHeroCard();
			if (!heroCard2)
			{
				return null;
			}
			gameObject = heroCard2.gameObject;
			break;
		}
		case SpellLocation.OPPONENT_HERO_POWER:
		{
			Player player7 = FindOpponentPlayer(spell);
			if (player7 == null)
			{
				return null;
			}
			Card heroPowerCard2 = player7.GetHeroPowerCard();
			if (!heroPowerCard2)
			{
				return null;
			}
			gameObject = heroPowerCard2.gameObject;
			break;
		}
		case SpellLocation.OPPONENT_PLAY_ZONE:
		{
			ZonePlay zonePlay2 = FindOpponentPlayZone(spell);
			if (!zonePlay2)
			{
				return null;
			}
			gameObject = zonePlay2.gameObject;
			break;
		}
		case SpellLocation.CHOSEN_TARGET:
		{
			Card powerTargetCard = spell.GetPowerTargetCard();
			if (powerTargetCard == null)
			{
				return null;
			}
			gameObject = powerTargetCard.gameObject;
			break;
		}
		case SpellLocation.FRIENDLY_HAND_ZONE:
		{
			Player player2 = FindFriendlyPlayer(spell);
			if (player2 == null)
			{
				return null;
			}
			ZoneHand zoneHand = ZoneMgr.Get().FindZoneOfType<ZoneHand>(player2.GetSide());
			if (!zoneHand)
			{
				return null;
			}
			gameObject = zoneHand.gameObject;
			break;
		}
		case SpellLocation.OPPONENT_HAND_ZONE:
		{
			Player player10 = FindOpponentPlayer(spell);
			if (player10 == null)
			{
				return null;
			}
			ZoneHand zoneHand4 = ZoneMgr.Get().FindZoneOfType<ZoneHand>(player10.GetSide());
			if (!zoneHand4)
			{
				return null;
			}
			gameObject = zoneHand4.gameObject;
			break;
		}
		case SpellLocation.FRIENDLY_DECK_ZONE:
		{
			Player player8 = FindFriendlyPlayer(spell);
			if (player8 == null)
			{
				return null;
			}
			ZoneDeck zoneDeck2 = ZoneMgr.Get().FindZoneOfType<ZoneDeck>(player8.GetSide());
			if (!zoneDeck2)
			{
				return null;
			}
			gameObject = zoneDeck2.gameObject;
			break;
		}
		case SpellLocation.OPPONENT_DECK_ZONE:
		{
			Player player6 = FindOpponentPlayer(spell);
			if (player6 == null)
			{
				return null;
			}
			ZoneDeck zoneDeck = ZoneMgr.Get().FindZoneOfType<ZoneDeck>(player6.GetSide());
			if (!zoneDeck)
			{
				return null;
			}
			gameObject = zoneDeck.gameObject;
			break;
		}
		case SpellLocation.FRIENDLY_WEAPON:
		{
			Player player4 = FindFriendlyPlayer(spell);
			if (player4 == null)
			{
				return null;
			}
			Card weaponCard2 = player4.GetWeaponCard();
			if (!weaponCard2)
			{
				ZoneWeapon zoneWeapon2 = ZoneMgr.Get().FindZoneOfType<ZoneWeapon>(player4.GetSide());
				if (!zoneWeapon2)
				{
					return null;
				}
				gameObject = zoneWeapon2.gameObject;
			}
			else
			{
				gameObject = weaponCard2.gameObject;
			}
			break;
		}
		case SpellLocation.OPPONENT_WEAPON:
		{
			Player player = FindOpponentPlayer(spell);
			if (player == null)
			{
				return null;
			}
			Card weaponCard = player.GetWeaponCard();
			if (!weaponCard)
			{
				ZoneWeapon zoneWeapon = ZoneMgr.Get().FindZoneOfType<ZoneWeapon>(player.GetSide());
				if (!zoneWeapon)
				{
					return null;
				}
				gameObject = zoneWeapon.gameObject;
			}
			else
			{
				gameObject = weaponCard.gameObject;
			}
			break;
		}
		}
		if (gameObject == null)
		{
			return null;
		}
		if (string.IsNullOrEmpty(overrideTransformName))
		{
			overrideTransformName = spell.GetLocationTransformName();
		}
		if (!string.IsNullOrEmpty(overrideTransformName))
		{
			GameObject gameObject2 = SceneUtils.FindChildBySubstring(gameObject, overrideTransformName);
			if (gameObject2 != null)
			{
				return gameObject2;
			}
		}
		return gameObject;
	}

	public static bool SetPositionFromLocation(Spell spell)
	{
		return SetPositionFromLocation(spell, setParent: false);
	}

	public static bool SetPositionFromLocation(Spell spell, bool setParent)
	{
		Transform locationTransform = GetLocationTransform(spell);
		if (locationTransform == null)
		{
			return false;
		}
		if (setParent)
		{
			spell.transform.parent = locationTransform;
		}
		spell.transform.position = locationTransform.position;
		return true;
	}

	public static bool SetOrientationFromFacing(Spell spell)
	{
		SpellFacing facing = spell.GetFacing();
		if (facing == SpellFacing.NONE)
		{
			return false;
		}
		SpellFacingOptions spellFacingOptions = spell.GetFacingOptions();
		if (spellFacingOptions == null)
		{
			spellFacingOptions = new SpellFacingOptions();
		}
		switch (facing)
		{
		case SpellFacing.SAME_AS_SOURCE:
		{
			GameObject source3 = spell.GetSource();
			if (source3 == null)
			{
				return false;
			}
			FaceSameAs(spell, source3, spellFacingOptions);
			break;
		}
		case SpellFacing.SAME_AS_SOURCE_AUTO:
		{
			GameObject gameObject = FindSourceAutoObjectForSpell(spell);
			if (gameObject == null)
			{
				return false;
			}
			FaceSameAs(spell, gameObject, spellFacingOptions);
			break;
		}
		case SpellFacing.SAME_AS_SOURCE_HERO:
		{
			Card card5 = FindHeroCard(spell.GetSourceCard());
			if (card5 == null)
			{
				return false;
			}
			FaceSameAs(spell, card5, spellFacingOptions);
			break;
		}
		case SpellFacing.TOWARDS_SOURCE:
		{
			GameObject source = spell.GetSource();
			if (source == null)
			{
				return false;
			}
			FaceTowards(spell, source, spellFacingOptions);
			break;
		}
		case SpellFacing.TOWARDS_SOURCE_AUTO:
		{
			GameObject gameObject3 = FindSourceAutoObjectForSpell(spell);
			if (gameObject3 == null)
			{
				return false;
			}
			FaceTowards(spell, gameObject3, spellFacingOptions);
			break;
		}
		case SpellFacing.TOWARDS_SOURCE_HERO:
		{
			Card card2 = FindHeroCard(spell.GetSourceCard());
			if (card2 == null)
			{
				return false;
			}
			FaceTowards(spell, card2, spellFacingOptions);
			break;
		}
		case SpellFacing.TOWARDS_TARGET:
		{
			GameObject visualTarget = spell.GetVisualTarget();
			if (visualTarget == null)
			{
				return false;
			}
			FaceTowards(spell, visualTarget, spellFacingOptions);
			break;
		}
		case SpellFacing.TOWARDS_TARGET_HERO:
		{
			Card card4 = FindHeroCard(FindBestTargetCard(spell));
			if (card4 == null)
			{
				return false;
			}
			FaceTowards(spell, card4, spellFacingOptions);
			break;
		}
		case SpellFacing.TOWARDS_CHOSEN_TARGET:
		{
			Card powerTargetCard = spell.GetPowerTargetCard();
			if (powerTargetCard == null)
			{
				return false;
			}
			FaceTowards(spell, powerTargetCard, spellFacingOptions);
			break;
		}
		case SpellFacing.OPPOSITE_OF_SOURCE:
		{
			GameObject source2 = spell.GetSource();
			if (source2 == null)
			{
				return false;
			}
			FaceOppositeOf(spell, source2, spellFacingOptions);
			break;
		}
		case SpellFacing.OPPOSITE_OF_SOURCE_AUTO:
		{
			GameObject gameObject2 = FindSourceAutoObjectForSpell(spell);
			if (gameObject2 == null)
			{
				return false;
			}
			FaceOppositeOf(spell, gameObject2, spellFacingOptions);
			break;
		}
		case SpellFacing.OPPOSITE_OF_SOURCE_HERO:
		{
			Card card3 = FindHeroCard(spell.GetSourceCard());
			if (card3 == null)
			{
				return false;
			}
			FaceOppositeOf(spell, card3, spellFacingOptions);
			break;
		}
		case SpellFacing.TOWARDS_OPPONENT_HERO:
		{
			Card card = FindOpponentHeroCard(spell);
			if (card == null)
			{
				return false;
			}
			FaceTowards(spell, card, spellFacingOptions);
			break;
		}
		default:
			return false;
		}
		return true;
	}

	public static Player FindFriendlyPlayer(Spell spell)
	{
		if (spell == null)
		{
			return null;
		}
		Card sourceCard = spell.GetSourceCard();
		if (sourceCard == null)
		{
			return null;
		}
		return sourceCard.GetEntity().GetController();
	}

	public static Player FindOpponentPlayer(Spell spell)
	{
		Player player = FindFriendlyPlayer(spell);
		if (player == null)
		{
			return null;
		}
		return GameState.Get().GetFirstOpponentPlayer(player);
	}

	public static ZonePlay FindFriendlyPlayZone(Spell spell)
	{
		Player player = FindFriendlyPlayer(spell);
		if (player == null)
		{
			return null;
		}
		return ZoneMgr.Get().FindZoneOfType<ZonePlay>(player.GetSide());
	}

	public static ZonePlay FindOpponentPlayZone(Spell spell)
	{
		Player player = FindOpponentPlayer(spell);
		if (player == null)
		{
			return null;
		}
		return ZoneMgr.Get().FindZoneOfType<ZonePlay>(player.GetSide());
	}

	public static Card FindOpponentHeroCard(Spell spell)
	{
		return FindOpponentPlayer(spell)?.GetHeroCard();
	}

	public static Zone FindTargetZone(Spell spell)
	{
		Card targetCard = spell.GetTargetCard();
		if (targetCard == null)
		{
			return null;
		}
		Entity entity = targetCard.GetEntity();
		return ZoneMgr.Get().FindZoneForEntity(entity);
	}

	public static Actor GetParentActor(Spell spell)
	{
		return SceneUtils.FindComponentInThisOrParents<Actor>(spell.gameObject);
	}

	public static GameObject GetParentRootObject(Spell spell)
	{
		Actor parentActor = GetParentActor(spell);
		if (parentActor == null)
		{
			return null;
		}
		return parentActor.GetRootObject();
	}

	public static MeshRenderer GetParentRootObjectMesh(Spell spell)
	{
		Actor parentActor = GetParentActor(spell);
		if (parentActor == null)
		{
			return null;
		}
		return parentActor.GetMeshRenderer();
	}

	public static bool IsNonMetaTaskListInMetaBlock(PowerTaskList taskList)
	{
		if (!taskList.DoesBlockHaveEffectTimingMetaData())
		{
			return false;
		}
		if (taskList.HasEffectTimingMetaData())
		{
			return false;
		}
		return true;
	}

	public static bool CanAddPowerTargets(PowerTaskList taskList)
	{
		if (IsNonMetaTaskListInMetaBlock(taskList))
		{
			return false;
		}
		if (!taskList.HasTasks() && !taskList.IsEndOfBlock())
		{
			return false;
		}
		return true;
	}

	public static void SetCustomSpellParent(Spell spell, Component c)
	{
		if (!(spell == null) && !(c == null))
		{
			spell.transform.parent = c.transform;
			spell.transform.localPosition = Vector3.zero;
		}
	}

	public static Spell LoadAndSetupSpell(string spellPath, Component owner)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(spellPath);
		if (gameObject == null)
		{
			Error.AddDevFatalUnlessWorkarounds("LoadAndSetupSpell() - Failed to load \"{0}\"", spellPath);
			return null;
		}
		Spell component = gameObject.GetComponent<Spell>();
		if (component == null)
		{
			UnityEngine.Object.Destroy(gameObject);
			Error.AddDevFatalUnlessWorkarounds("LoadAndSetupSpell() - \"{0}\" does not have a Spell component.", spellPath);
			return null;
		}
		if (owner != null)
		{
			SetupSpell(component, owner);
		}
		return component;
	}

	public static void SetupSpell(Spell spell, Component c)
	{
		if (!(spell == null) && !(c == null))
		{
			spell.SetSource(c.gameObject);
		}
	}

	public static void SetupSoundSpell(CardSoundSpell spell, Component c)
	{
		if (!(spell == null) && !(c == null))
		{
			spell.SetSource(c.gameObject);
			spell.transform.parent = c.transform;
			TransformUtil.Identity(spell.transform);
		}
	}

	public static bool ActivateStateIfNecessary(Spell spell, SpellStateType state)
	{
		switch (state)
		{
		case SpellStateType.BIRTH:
			return ActivateBirthIfNecessary(spell);
		case SpellStateType.DEATH:
			return ActivateDeathIfNecessary(spell);
		case SpellStateType.CANCEL:
			return ActivateCancelIfNecessary(spell);
		default:
			if (spell != null && spell.GetActiveState() != state)
			{
				spell.ActivateState(state);
				return true;
			}
			return false;
		}
	}

	public static bool ActivateBirthIfNecessary(Spell spell)
	{
		if (spell == null)
		{
			return false;
		}
		switch (spell.GetActiveState())
		{
		case SpellStateType.BIRTH:
			return false;
		case SpellStateType.IDLE:
			return false;
		default:
			spell.ActivateState(SpellStateType.BIRTH);
			return true;
		}
	}

	public static bool ActivateDeathIfNecessary(Spell spell)
	{
		if (spell == null)
		{
			return false;
		}
		switch (spell.GetActiveState())
		{
		case SpellStateType.DEATH:
			return false;
		case SpellStateType.NONE:
			return false;
		default:
			spell.ActivateState(SpellStateType.DEATH);
			return true;
		}
	}

	public static bool ActivateCancelIfNecessary(Spell spell)
	{
		if (spell == null)
		{
			return false;
		}
		switch (spell.GetActiveState())
		{
		case SpellStateType.CANCEL:
			return false;
		case SpellStateType.NONE:
			return false;
		default:
			spell.ActivateState(SpellStateType.CANCEL);
			return true;
		}
	}

	public static void PurgeSpell(Spell spell)
	{
		if (!(spell == null) && spell.CanPurge())
		{
			UnityEngine.Object.Destroy(spell.gameObject);
		}
	}

	public static void PurgeSpells<T>(List<T> spells) where T : Spell
	{
		if (spells != null && spells.Count != 0)
		{
			for (int i = 0; i < spells.Count; i++)
			{
				PurgeSpell(spells[i]);
			}
		}
	}

	private static GameObject FindSourceAutoObjectForSpell(Spell spell)
	{
		GameObject source = spell.GetSource();
		Card sourceCard = spell.GetSourceCard();
		if (sourceCard == null)
		{
			return source;
		}
		Entity entity = sourceCard.GetEntity();
		TAG_CARDTYPE cardType = entity.GetCardType();
		PowerTaskList powerTaskList = spell.GetPowerTaskList();
		if (powerTaskList != null)
		{
			EntityDef effectEntityDef = powerTaskList.GetEffectEntityDef();
			if (effectEntityDef != null)
			{
				cardType = effectEntityDef.GetCardType();
			}
		}
		return FindAutoObjectForSpell(entity, sourceCard, cardType);
	}

	private static GameObject FindTargetAutoObjectForSpell(Spell spell)
	{
		GameObject visualTarget = spell.GetVisualTarget();
		if (visualTarget == null)
		{
			return null;
		}
		Card component = visualTarget.GetComponent<Card>();
		if (component == null)
		{
			return visualTarget;
		}
		Entity entity = component.GetEntity();
		TAG_CARDTYPE cardType = entity.GetCardType();
		return FindAutoObjectForSpell(entity, component, cardType);
	}

	private static GameObject FindAutoObjectForSpell(Entity entity, Card card, TAG_CARDTYPE cardType)
	{
		switch (cardType)
		{
		case TAG_CARDTYPE.SPELL:
		{
			Card heroCard = entity.GetController().GetHeroCard();
			if (heroCard == null)
			{
				return card.gameObject;
			}
			return heroCard.gameObject;
		}
		case TAG_CARDTYPE.HERO_POWER:
		{
			Card heroPowerCard = entity.GetController().GetHeroPowerCard();
			if (heroPowerCard == null)
			{
				return card.gameObject;
			}
			return heroPowerCard.gameObject;
		}
		case TAG_CARDTYPE.ENCHANTMENT:
		{
			Entity entity2 = GameState.Get().GetEntity(entity.GetAttached());
			if (entity2 != null)
			{
				Card card2 = entity2.GetCard();
				if (card2 != null)
				{
					return card2.gameObject;
				}
			}
			break;
		}
		}
		return card.gameObject;
	}

	private static Card FindBestTargetCard(Spell spell)
	{
		Card sourceCard = spell.GetSourceCard();
		if (sourceCard == null)
		{
			return spell.GetVisualTargetCard();
		}
		Player controller = sourceCard.GetEntity().GetController();
		if (controller == null)
		{
			return spell.GetVisualTargetCard();
		}
		Player.Side side = controller.GetSide();
		List<GameObject> visualTargets = spell.GetVisualTargets();
		for (int i = 0; i < visualTargets.Count; i++)
		{
			Card component = visualTargets[i].GetComponent<Card>();
			if (!(component == null) && component.GetEntity().GetController().GetSide() != side)
			{
				return component;
			}
		}
		return spell.GetVisualTargetCard();
	}

	private static Card FindHeroCard(Card card)
	{
		if (card == null)
		{
			return null;
		}
		return card.GetEntity().GetController()?.GetHeroCard();
	}

	private static Card FindHeroPowerCard(Card card)
	{
		if (card == null)
		{
			return null;
		}
		return card.GetEntity().GetController()?.GetHeroPowerCard();
	}

	private static void FaceSameAs(GameObject source, GameObject target, SpellFacingOptions options)
	{
		FaceSameAs(source.transform, target.transform, options);
	}

	private static void FaceSameAs(GameObject source, Component target, SpellFacingOptions options)
	{
		FaceSameAs(source.transform, target.transform, options);
	}

	private static void FaceSameAs(Component source, GameObject target, SpellFacingOptions options)
	{
		FaceSameAs(source.transform, target.transform, options);
	}

	private static void FaceSameAs(Component source, Component target, SpellFacingOptions options)
	{
		FaceSameAs(source.transform, target.transform, options);
	}

	private static void FaceSameAs(Transform source, Transform target, SpellFacingOptions options)
	{
		SetOrientation(source, target.position, target.position + target.forward, options);
	}

	private static void FaceOppositeOf(GameObject source, GameObject target, SpellFacingOptions options)
	{
		FaceOppositeOf(source.transform, target.transform, options);
	}

	private static void FaceOppositeOf(GameObject source, Component target, SpellFacingOptions options)
	{
		FaceOppositeOf(source.transform, target.transform, options);
	}

	private static void FaceOppositeOf(Component source, GameObject target, SpellFacingOptions options)
	{
		FaceOppositeOf(source.transform, target.transform, options);
	}

	private static void FaceOppositeOf(Component source, Component target, SpellFacingOptions options)
	{
		FaceOppositeOf(source.transform, target.transform, options);
	}

	private static void FaceOppositeOf(Transform source, Transform target, SpellFacingOptions options)
	{
		SetOrientation(source, target.position, target.position - target.forward, options);
	}

	private static void FaceTowards(GameObject source, GameObject target, SpellFacingOptions options)
	{
		FaceTowards(source.transform, target.transform, options);
	}

	private static void FaceTowards(GameObject source, Component target, SpellFacingOptions options)
	{
		FaceTowards(source.transform, target.transform, options);
	}

	private static void FaceTowards(Component source, GameObject target, SpellFacingOptions options)
	{
		FaceTowards(source.transform, target.transform, options);
	}

	private static void FaceTowards(Component source, Component target, SpellFacingOptions options)
	{
		FaceTowards(source.transform, target.transform, options);
	}

	private static void FaceTowards(Transform source, Transform target, SpellFacingOptions options)
	{
		SetOrientation(source, source.position, target.position, options);
	}

	private static void SetOrientation(Transform source, Vector3 sourcePosition, Vector3 targetPosition, SpellFacingOptions options)
	{
		if (!options.m_RotateX || !options.m_RotateY)
		{
			if (options.m_RotateX)
			{
				targetPosition.x = sourcePosition.x;
			}
			else
			{
				if (!options.m_RotateY)
				{
					return;
				}
				targetPosition.y = sourcePosition.y;
			}
		}
		Vector3 forward = targetPosition - sourcePosition;
		if (forward.sqrMagnitude > Mathf.Epsilon)
		{
			source.rotation = Quaternion.LookRotation(forward);
		}
	}

	public static T GetAppropriateElementAccordingToRanges<T>(T[] elements, Func<T, ValueRange> rangeAccessor, int desiredValue)
	{
		if (elements.Length == 0)
		{
			return default(T);
		}
		int maxHandledValue = elements.Max((T x) => rangeAccessor(x).m_maxValue);
		if (desiredValue > maxHandledValue)
		{
			return elements.First((T x) => rangeAccessor(x).m_maxValue == maxHandledValue);
		}
		int minHandledValue = elements.Min((T x) => rangeAccessor(x).m_minValue);
		if (desiredValue < minHandledValue)
		{
			return elements.First((T x) => rangeAccessor(x).m_minValue == minHandledValue);
		}
		for (int i = 0; i < elements.Length; i++)
		{
			if (desiredValue >= rangeAccessor(elements[i]).m_minValue && desiredValue <= rangeAccessor(elements[i]).m_maxValue)
			{
				return elements[i];
			}
		}
		return default(T);
	}

	public static IEnumerator FlipActorAndReplaceWithCard(Actor actor, Card card, float time)
	{
		float halfTime = time * 0.5f;
		card.HideCard();
		Hashtable args = iTween.Hash("z", 90, "time", halfTime, "easetype", iTween.EaseType.linear, "name", "SpellUtils.FlipActorAndReplaceWithCard");
		iTween.RotateAdd(actor.gameObject, args);
		while (iTween.HasName(actor.gameObject, "SpellUtils.FlipActorAndReplaceWithCard"))
		{
			yield return null;
		}
		TransformUtil.CopyWorld(card, actor);
		card.transform.rotation *= Quaternion.Euler(0f, 0f, 180f);
		actor.Hide();
		card.ShowCard();
		args = iTween.Hash("z", 90, "time", halfTime, "easetype", iTween.EaseType.linear, "name", "SpellUtils.FlipActorAndReplaceWithCard");
		iTween.RotateAdd(card.gameObject, args);
		while (iTween.HasName(card.gameObject, "SpellUtils.FlipActorAndReplaceWithCard"))
		{
			yield return null;
		}
	}

	public static IEnumerator FlipActorAndReplaceWithOtherActor(Actor actor, Actor otherActor, float time)
	{
		float halfTime = time * 0.5f;
		otherActor.Hide();
		Hashtable args = iTween.Hash("z", 90, "time", halfTime, "easetype", iTween.EaseType.linear, "name", "SpellUtils.FlipActorAndReplaceWithOtherActor");
		iTween.RotateAdd(actor.gameObject, args);
		while (iTween.HasName(actor.gameObject, "SpellUtils.FlipActorAndReplaceWithOtherActor"))
		{
			yield return null;
		}
		TransformUtil.CopyWorld(otherActor, actor);
		otherActor.transform.rotation *= Quaternion.Euler(0f, 0f, 180f);
		actor.Hide();
		otherActor.Show();
		args = iTween.Hash("z", 90, "time", halfTime, "easetype", iTween.EaseType.linear, "name", "SpellUtils.FlipActorAndReplaceWithOtherActor");
		iTween.RotateAdd(otherActor.gameObject, args);
		while (iTween.HasName(otherActor.gameObject, "SpellUtils.FlipActorAndReplaceWithOtherActor"))
		{
			yield return null;
		}
	}
}
