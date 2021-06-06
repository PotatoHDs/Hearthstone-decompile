using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000986 RID: 2438
public class SpellUtils
{
	// Token: 0x06008599 RID: 34201 RVA: 0x002B21C0 File Offset: 0x002B03C0
	public static SpellClassTag ConvertClassTagToSpellEnum(TAG_CLASS classTag)
	{
		switch (classTag)
		{
		case TAG_CLASS.DEATHKNIGHT:
			return SpellClassTag.DEATHKNIGHT;
		case TAG_CLASS.DRUID:
			return SpellClassTag.DRUID;
		case TAG_CLASS.HUNTER:
			return SpellClassTag.HUNTER;
		case TAG_CLASS.MAGE:
			return SpellClassTag.MAGE;
		case TAG_CLASS.PALADIN:
			return SpellClassTag.PALADIN;
		case TAG_CLASS.PRIEST:
			return SpellClassTag.PRIEST;
		case TAG_CLASS.ROGUE:
			return SpellClassTag.ROGUE;
		case TAG_CLASS.SHAMAN:
			return SpellClassTag.SHAMAN;
		case TAG_CLASS.WARLOCK:
			return SpellClassTag.WARLOCK;
		case TAG_CLASS.WARRIOR:
			return SpellClassTag.WARRIOR;
		default:
			return SpellClassTag.NONE;
		}
	}

	// Token: 0x0600859A RID: 34202 RVA: 0x002B2218 File Offset: 0x002B0418
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

	// Token: 0x0600859B RID: 34203 RVA: 0x002B2268 File Offset: 0x002B0468
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
			Debug.LogWarning(string.Format("SpellUtils.FindZonesFromTag() - unhandled zoneTag {0}", zoneTag));
			return null;
		}
	}

	// Token: 0x0600859C RID: 34204 RVA: 0x002B2300 File Offset: 0x002B0500
	public static List<Zone> FindZonesFromTag(Spell spell, SpellZoneTag zoneTag, SpellPlayerSide spellSide)
	{
		if (ZoneMgr.Get() == null)
		{
			return null;
		}
		if (spellSide == SpellPlayerSide.NEUTRAL)
		{
			return null;
		}
		if (spellSide == SpellPlayerSide.BOTH)
		{
			return SpellUtils.FindZonesFromTag(zoneTag);
		}
		Player.Side side = SpellUtils.ConvertSpellSideToPlayerSide(spell, spellSide);
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
			Debug.LogWarning(string.Format("SpellUtils.FindZonesFromTag() - Unhandled zoneTag {0}. spellSide={1} playerSide={2}", zoneTag, spellSide, side));
			return null;
		}
	}

	// Token: 0x0600859D RID: 34205 RVA: 0x002B23E0 File Offset: 0x002B05E0
	public static Transform GetLocationTransform(Spell spell)
	{
		GameObject locationObject = SpellUtils.GetLocationObject(spell);
		if (!(locationObject == null))
		{
			return locationObject.transform;
		}
		return null;
	}

	// Token: 0x0600859E RID: 34206 RVA: 0x002B2408 File Offset: 0x002B0608
	public static GameObject GetLocationObject(Spell spell)
	{
		SpellLocation location = spell.GetLocation();
		return SpellUtils.GetSpellLocationObject(spell, location, null);
	}

	// Token: 0x0600859F RID: 34207 RVA: 0x002B2424 File Offset: 0x002B0624
	public static GameObject GetSpellLocationObject(Spell spell, SpellLocation location, string overrideTransformName = null)
	{
		if (location == SpellLocation.NONE)
		{
			return null;
		}
		GameObject gameObject = null;
		if (location == SpellLocation.SOURCE)
		{
			gameObject = spell.GetSource();
		}
		else if (location == SpellLocation.SOURCE_AUTO)
		{
			gameObject = SpellUtils.FindSourceAutoObjectForSpell(spell);
		}
		else if (location == SpellLocation.SOURCE_HERO)
		{
			Card card = SpellUtils.FindHeroCard(spell.GetSourceCard());
			if (card == null)
			{
				return null;
			}
			gameObject = card.gameObject;
		}
		else if (location == SpellLocation.SOURCE_HERO_POWER)
		{
			Card card2 = SpellUtils.FindHeroPowerCard(spell.GetSourceCard());
			if (card2 == null)
			{
				return null;
			}
			gameObject = card2.gameObject;
		}
		else if (location == SpellLocation.SOURCE_PLAY_ZONE)
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
		}
		else if (location == SpellLocation.SOURCE_HAND_ZONE)
		{
			Card sourceCard2 = spell.GetSourceCard();
			if (sourceCard2 == null)
			{
				return null;
			}
			Player controller2 = sourceCard2.GetEntity().GetController();
			ZoneHand zoneHand = ZoneMgr.Get().FindZoneOfType<ZoneHand>(controller2.GetSide());
			if (zoneHand == null)
			{
				return null;
			}
			gameObject = zoneHand.gameObject;
		}
		else if (location == SpellLocation.SOURCE_DECK_ZONE)
		{
			Card sourceCard3 = spell.GetSourceCard();
			if (sourceCard3 == null)
			{
				return null;
			}
			Player controller3 = sourceCard3.GetEntity().GetController();
			ZoneDeck zoneDeck = ZoneMgr.Get().FindZoneOfType<ZoneDeck>(controller3.GetSide());
			if (zoneDeck == null)
			{
				return null;
			}
			gameObject = zoneDeck.gameObject;
		}
		else if (location == SpellLocation.TARGET)
		{
			gameObject = spell.GetVisualTarget();
		}
		else if (location == SpellLocation.TARGET_AUTO)
		{
			gameObject = SpellUtils.FindTargetAutoObjectForSpell(spell);
		}
		else if (location == SpellLocation.TARGET_HERO)
		{
			Card card3 = SpellUtils.FindHeroCard(spell.GetVisualTargetCard());
			if (card3 == null)
			{
				return null;
			}
			gameObject = card3.gameObject;
		}
		else if (location == SpellLocation.TARGET_HERO_POWER)
		{
			Card card4 = SpellUtils.FindHeroPowerCard(spell.GetVisualTargetCard());
			if (card4 == null)
			{
				return null;
			}
			gameObject = card4.gameObject;
		}
		else if (location == SpellLocation.TARGET_PLAY_ZONE)
		{
			Card visualTargetCard = spell.GetVisualTargetCard();
			if (visualTargetCard == null)
			{
				return null;
			}
			Player controller4 = visualTargetCard.GetEntity().GetController();
			ZonePlay zonePlay2 = ZoneMgr.Get().FindZoneOfType<ZonePlay>(controller4.GetSide());
			if (zonePlay2 == null)
			{
				return null;
			}
			gameObject = zonePlay2.gameObject;
		}
		else if (location == SpellLocation.TARGET_HAND_ZONE)
		{
			Card visualTargetCard2 = spell.GetVisualTargetCard();
			if (visualTargetCard2 == null)
			{
				return null;
			}
			Player controller5 = visualTargetCard2.GetEntity().GetController();
			ZoneHand zoneHand2 = ZoneMgr.Get().FindZoneOfType<ZoneHand>(controller5.GetSide());
			if (zoneHand2 == null)
			{
				return null;
			}
			gameObject = zoneHand2.gameObject;
		}
		else if (location == SpellLocation.TARGET_DECK_ZONE)
		{
			Card visualTargetCard3 = spell.GetVisualTargetCard();
			if (visualTargetCard3 == null)
			{
				return null;
			}
			Player controller6 = visualTargetCard3.GetEntity().GetController();
			ZoneDeck zoneDeck2 = ZoneMgr.Get().FindZoneOfType<ZoneDeck>(controller6.GetSide());
			if (zoneDeck2 == null)
			{
				return null;
			}
			gameObject = zoneDeck2.gameObject;
		}
		else if (location == SpellLocation.BOARD)
		{
			if (Board.Get() == null)
			{
				return null;
			}
			gameObject = Board.Get().gameObject;
		}
		else if (location == SpellLocation.FRIENDLY_HERO)
		{
			Player player = SpellUtils.FindFriendlyPlayer(spell);
			if (player == null)
			{
				return null;
			}
			Card heroCard = player.GetHeroCard();
			if (!heroCard)
			{
				return null;
			}
			gameObject = heroCard.gameObject;
		}
		else if (location == SpellLocation.FRIENDLY_HERO_POWER)
		{
			Player player2 = SpellUtils.FindFriendlyPlayer(spell);
			if (player2 == null)
			{
				return null;
			}
			Card heroPowerCard = player2.GetHeroPowerCard();
			if (!heroPowerCard)
			{
				return null;
			}
			gameObject = heroPowerCard.gameObject;
		}
		else if (location == SpellLocation.FRIENDLY_PLAY_ZONE)
		{
			ZonePlay zonePlay3 = SpellUtils.FindFriendlyPlayZone(spell);
			if (!zonePlay3)
			{
				return null;
			}
			gameObject = zonePlay3.gameObject;
		}
		else if (location == SpellLocation.OPPONENT_HERO)
		{
			Player player3 = SpellUtils.FindOpponentPlayer(spell);
			if (player3 == null)
			{
				return null;
			}
			Card heroCard2 = player3.GetHeroCard();
			if (!heroCard2)
			{
				return null;
			}
			gameObject = heroCard2.gameObject;
		}
		else if (location == SpellLocation.OPPONENT_HERO_POWER)
		{
			Player player4 = SpellUtils.FindOpponentPlayer(spell);
			if (player4 == null)
			{
				return null;
			}
			Card heroPowerCard2 = player4.GetHeroPowerCard();
			if (!heroPowerCard2)
			{
				return null;
			}
			gameObject = heroPowerCard2.gameObject;
		}
		else if (location == SpellLocation.OPPONENT_PLAY_ZONE)
		{
			ZonePlay zonePlay4 = SpellUtils.FindOpponentPlayZone(spell);
			if (!zonePlay4)
			{
				return null;
			}
			gameObject = zonePlay4.gameObject;
		}
		else if (location == SpellLocation.CHOSEN_TARGET)
		{
			Card powerTargetCard = spell.GetPowerTargetCard();
			if (powerTargetCard == null)
			{
				return null;
			}
			gameObject = powerTargetCard.gameObject;
		}
		else if (location == SpellLocation.FRIENDLY_HAND_ZONE)
		{
			Player player5 = SpellUtils.FindFriendlyPlayer(spell);
			if (player5 == null)
			{
				return null;
			}
			ZoneHand zoneHand3 = ZoneMgr.Get().FindZoneOfType<ZoneHand>(player5.GetSide());
			if (!zoneHand3)
			{
				return null;
			}
			gameObject = zoneHand3.gameObject;
		}
		else if (location == SpellLocation.OPPONENT_HAND_ZONE)
		{
			Player player6 = SpellUtils.FindOpponentPlayer(spell);
			if (player6 == null)
			{
				return null;
			}
			ZoneHand zoneHand4 = ZoneMgr.Get().FindZoneOfType<ZoneHand>(player6.GetSide());
			if (!zoneHand4)
			{
				return null;
			}
			gameObject = zoneHand4.gameObject;
		}
		else if (location == SpellLocation.FRIENDLY_DECK_ZONE)
		{
			Player player7 = SpellUtils.FindFriendlyPlayer(spell);
			if (player7 == null)
			{
				return null;
			}
			ZoneDeck zoneDeck3 = ZoneMgr.Get().FindZoneOfType<ZoneDeck>(player7.GetSide());
			if (!zoneDeck3)
			{
				return null;
			}
			gameObject = zoneDeck3.gameObject;
		}
		else if (location == SpellLocation.OPPONENT_DECK_ZONE)
		{
			Player player8 = SpellUtils.FindOpponentPlayer(spell);
			if (player8 == null)
			{
				return null;
			}
			ZoneDeck zoneDeck4 = ZoneMgr.Get().FindZoneOfType<ZoneDeck>(player8.GetSide());
			if (!zoneDeck4)
			{
				return null;
			}
			gameObject = zoneDeck4.gameObject;
		}
		else if (location == SpellLocation.FRIENDLY_WEAPON)
		{
			Player player9 = SpellUtils.FindFriendlyPlayer(spell);
			if (player9 == null)
			{
				return null;
			}
			Card weaponCard = player9.GetWeaponCard();
			if (!weaponCard)
			{
				ZoneWeapon zoneWeapon = ZoneMgr.Get().FindZoneOfType<ZoneWeapon>(player9.GetSide());
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
		}
		else if (location == SpellLocation.OPPONENT_WEAPON)
		{
			Player player10 = SpellUtils.FindOpponentPlayer(spell);
			if (player10 == null)
			{
				return null;
			}
			Card weaponCard2 = player10.GetWeaponCard();
			if (!weaponCard2)
			{
				ZoneWeapon zoneWeapon2 = ZoneMgr.Get().FindZoneOfType<ZoneWeapon>(player10.GetSide());
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

	// Token: 0x060085A0 RID: 34208 RVA: 0x002B2A4B File Offset: 0x002B0C4B
	public static bool SetPositionFromLocation(Spell spell)
	{
		return SpellUtils.SetPositionFromLocation(spell, false);
	}

	// Token: 0x060085A1 RID: 34209 RVA: 0x002B2A54 File Offset: 0x002B0C54
	public static bool SetPositionFromLocation(Spell spell, bool setParent)
	{
		Transform locationTransform = SpellUtils.GetLocationTransform(spell);
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

	// Token: 0x060085A2 RID: 34210 RVA: 0x002B2A94 File Offset: 0x002B0C94
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
		if (facing == SpellFacing.SAME_AS_SOURCE)
		{
			GameObject source = spell.GetSource();
			if (source == null)
			{
				return false;
			}
			SpellUtils.FaceSameAs(spell, source, spellFacingOptions);
		}
		else if (facing == SpellFacing.SAME_AS_SOURCE_AUTO)
		{
			GameObject gameObject = SpellUtils.FindSourceAutoObjectForSpell(spell);
			if (gameObject == null)
			{
				return false;
			}
			SpellUtils.FaceSameAs(spell, gameObject, spellFacingOptions);
		}
		else if (facing == SpellFacing.SAME_AS_SOURCE_HERO)
		{
			Card card = SpellUtils.FindHeroCard(spell.GetSourceCard());
			if (card == null)
			{
				return false;
			}
			SpellUtils.FaceSameAs(spell, card, spellFacingOptions);
		}
		else if (facing == SpellFacing.TOWARDS_SOURCE)
		{
			GameObject source2 = spell.GetSource();
			if (source2 == null)
			{
				return false;
			}
			SpellUtils.FaceTowards(spell, source2, spellFacingOptions);
		}
		else if (facing == SpellFacing.TOWARDS_SOURCE_AUTO)
		{
			GameObject gameObject2 = SpellUtils.FindSourceAutoObjectForSpell(spell);
			if (gameObject2 == null)
			{
				return false;
			}
			SpellUtils.FaceTowards(spell, gameObject2, spellFacingOptions);
		}
		else if (facing == SpellFacing.TOWARDS_SOURCE_HERO)
		{
			Card card2 = SpellUtils.FindHeroCard(spell.GetSourceCard());
			if (card2 == null)
			{
				return false;
			}
			SpellUtils.FaceTowards(spell, card2, spellFacingOptions);
		}
		else if (facing == SpellFacing.TOWARDS_TARGET)
		{
			GameObject visualTarget = spell.GetVisualTarget();
			if (visualTarget == null)
			{
				return false;
			}
			SpellUtils.FaceTowards(spell, visualTarget, spellFacingOptions);
		}
		else if (facing == SpellFacing.TOWARDS_TARGET_HERO)
		{
			Card card3 = SpellUtils.FindHeroCard(SpellUtils.FindBestTargetCard(spell));
			if (card3 == null)
			{
				return false;
			}
			SpellUtils.FaceTowards(spell, card3, spellFacingOptions);
		}
		else if (facing == SpellFacing.TOWARDS_CHOSEN_TARGET)
		{
			Card powerTargetCard = spell.GetPowerTargetCard();
			if (powerTargetCard == null)
			{
				return false;
			}
			SpellUtils.FaceTowards(spell, powerTargetCard, spellFacingOptions);
		}
		else if (facing == SpellFacing.OPPOSITE_OF_SOURCE)
		{
			GameObject source3 = spell.GetSource();
			if (source3 == null)
			{
				return false;
			}
			SpellUtils.FaceOppositeOf(spell, source3, spellFacingOptions);
		}
		else if (facing == SpellFacing.OPPOSITE_OF_SOURCE_AUTO)
		{
			GameObject gameObject3 = SpellUtils.FindSourceAutoObjectForSpell(spell);
			if (gameObject3 == null)
			{
				return false;
			}
			SpellUtils.FaceOppositeOf(spell, gameObject3, spellFacingOptions);
		}
		else if (facing == SpellFacing.OPPOSITE_OF_SOURCE_HERO)
		{
			Card card4 = SpellUtils.FindHeroCard(spell.GetSourceCard());
			if (card4 == null)
			{
				return false;
			}
			SpellUtils.FaceOppositeOf(spell, card4, spellFacingOptions);
		}
		else
		{
			if (facing != SpellFacing.TOWARDS_OPPONENT_HERO)
			{
				return false;
			}
			Card card5 = SpellUtils.FindOpponentHeroCard(spell);
			if (card5 == null)
			{
				return false;
			}
			SpellUtils.FaceTowards(spell, card5, spellFacingOptions);
		}
		return true;
	}

	// Token: 0x060085A3 RID: 34211 RVA: 0x002B2CB8 File Offset: 0x002B0EB8
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

	// Token: 0x060085A4 RID: 34212 RVA: 0x002B2CF0 File Offset: 0x002B0EF0
	public static Player FindOpponentPlayer(Spell spell)
	{
		Player player = SpellUtils.FindFriendlyPlayer(spell);
		if (player == null)
		{
			return null;
		}
		return GameState.Get().GetFirstOpponentPlayer(player);
	}

	// Token: 0x060085A5 RID: 34213 RVA: 0x002B2D14 File Offset: 0x002B0F14
	public static ZonePlay FindFriendlyPlayZone(Spell spell)
	{
		Player player = SpellUtils.FindFriendlyPlayer(spell);
		if (player == null)
		{
			return null;
		}
		return ZoneMgr.Get().FindZoneOfType<ZonePlay>(player.GetSide());
	}

	// Token: 0x060085A6 RID: 34214 RVA: 0x002B2D40 File Offset: 0x002B0F40
	public static ZonePlay FindOpponentPlayZone(Spell spell)
	{
		Player player = SpellUtils.FindOpponentPlayer(spell);
		if (player == null)
		{
			return null;
		}
		return ZoneMgr.Get().FindZoneOfType<ZonePlay>(player.GetSide());
	}

	// Token: 0x060085A7 RID: 34215 RVA: 0x002B2D6C File Offset: 0x002B0F6C
	public static Card FindOpponentHeroCard(Spell spell)
	{
		Player player = SpellUtils.FindOpponentPlayer(spell);
		if (player == null)
		{
			return null;
		}
		return player.GetHeroCard();
	}

	// Token: 0x060085A8 RID: 34216 RVA: 0x002B2D8C File Offset: 0x002B0F8C
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

	// Token: 0x060085A9 RID: 34217 RVA: 0x002B2DBD File Offset: 0x002B0FBD
	public static Actor GetParentActor(Spell spell)
	{
		return SceneUtils.FindComponentInThisOrParents<Actor>(spell.gameObject);
	}

	// Token: 0x060085AA RID: 34218 RVA: 0x002B2DCC File Offset: 0x002B0FCC
	public static GameObject GetParentRootObject(Spell spell)
	{
		Actor parentActor = SpellUtils.GetParentActor(spell);
		if (parentActor == null)
		{
			return null;
		}
		return parentActor.GetRootObject();
	}

	// Token: 0x060085AB RID: 34219 RVA: 0x002B2DF4 File Offset: 0x002B0FF4
	public static MeshRenderer GetParentRootObjectMesh(Spell spell)
	{
		Actor parentActor = SpellUtils.GetParentActor(spell);
		if (parentActor == null)
		{
			return null;
		}
		return parentActor.GetMeshRenderer(false);
	}

	// Token: 0x060085AC RID: 34220 RVA: 0x002B2E1A File Offset: 0x002B101A
	public static bool IsNonMetaTaskListInMetaBlock(PowerTaskList taskList)
	{
		return taskList.DoesBlockHaveEffectTimingMetaData() && !taskList.HasEffectTimingMetaData();
	}

	// Token: 0x060085AD RID: 34221 RVA: 0x002B2E31 File Offset: 0x002B1031
	public static bool CanAddPowerTargets(PowerTaskList taskList)
	{
		return !SpellUtils.IsNonMetaTaskListInMetaBlock(taskList) && (taskList.HasTasks() || taskList.IsEndOfBlock());
	}

	// Token: 0x060085AE RID: 34222 RVA: 0x002B2E50 File Offset: 0x002B1050
	public static void SetCustomSpellParent(Spell spell, Component c)
	{
		if (spell == null)
		{
			return;
		}
		if (c == null)
		{
			return;
		}
		spell.transform.parent = c.transform;
		spell.transform.localPosition = Vector3.zero;
	}

	// Token: 0x060085AF RID: 34223 RVA: 0x002B2E88 File Offset: 0x002B1088
	public static Spell LoadAndSetupSpell(string spellPath, Component owner)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(spellPath, AssetLoadingOptions.None);
		if (gameObject == null)
		{
			Error.AddDevFatalUnlessWorkarounds("LoadAndSetupSpell() - Failed to load \"{0}\"", new object[]
			{
				spellPath
			});
			return null;
		}
		Spell component = gameObject.GetComponent<Spell>();
		if (component == null)
		{
			UnityEngine.Object.Destroy(gameObject);
			Error.AddDevFatalUnlessWorkarounds("LoadAndSetupSpell() - \"{0}\" does not have a Spell component.", new object[]
			{
				spellPath
			});
			return null;
		}
		if (owner != null)
		{
			SpellUtils.SetupSpell(component, owner);
		}
		return component;
	}

	// Token: 0x060085B0 RID: 34224 RVA: 0x002B2F03 File Offset: 0x002B1103
	public static void SetupSpell(Spell spell, Component c)
	{
		if (spell == null)
		{
			return;
		}
		if (c == null)
		{
			return;
		}
		spell.SetSource(c.gameObject);
	}

	// Token: 0x060085B1 RID: 34225 RVA: 0x002B2F25 File Offset: 0x002B1125
	public static void SetupSoundSpell(CardSoundSpell spell, Component c)
	{
		if (spell == null)
		{
			return;
		}
		if (c == null)
		{
			return;
		}
		spell.SetSource(c.gameObject);
		spell.transform.parent = c.transform;
		TransformUtil.Identity(spell.transform);
	}

	// Token: 0x060085B2 RID: 34226 RVA: 0x002B2F63 File Offset: 0x002B1163
	public static bool ActivateStateIfNecessary(Spell spell, SpellStateType state)
	{
		if (state == SpellStateType.BIRTH)
		{
			return SpellUtils.ActivateBirthIfNecessary(spell);
		}
		if (state == SpellStateType.DEATH)
		{
			return SpellUtils.ActivateDeathIfNecessary(spell);
		}
		if (state == SpellStateType.CANCEL)
		{
			return SpellUtils.ActivateCancelIfNecessary(spell);
		}
		if (spell != null && spell.GetActiveState() != state)
		{
			spell.ActivateState(state);
			return true;
		}
		return false;
	}

	// Token: 0x060085B3 RID: 34227 RVA: 0x002B2FA4 File Offset: 0x002B11A4
	public static bool ActivateBirthIfNecessary(Spell spell)
	{
		if (spell == null)
		{
			return false;
		}
		SpellStateType activeState = spell.GetActiveState();
		if (activeState == SpellStateType.BIRTH)
		{
			return false;
		}
		if (activeState == SpellStateType.IDLE)
		{
			return false;
		}
		spell.ActivateState(SpellStateType.BIRTH);
		return true;
	}

	// Token: 0x060085B4 RID: 34228 RVA: 0x002B2FD8 File Offset: 0x002B11D8
	public static bool ActivateDeathIfNecessary(Spell spell)
	{
		if (spell == null)
		{
			return false;
		}
		SpellStateType activeState = spell.GetActiveState();
		if (activeState == SpellStateType.DEATH)
		{
			return false;
		}
		if (activeState == SpellStateType.NONE)
		{
			return false;
		}
		spell.ActivateState(SpellStateType.DEATH);
		return true;
	}

	// Token: 0x060085B5 RID: 34229 RVA: 0x002B300C File Offset: 0x002B120C
	public static bool ActivateCancelIfNecessary(Spell spell)
	{
		if (spell == null)
		{
			return false;
		}
		SpellStateType activeState = spell.GetActiveState();
		if (activeState == SpellStateType.CANCEL)
		{
			return false;
		}
		if (activeState == SpellStateType.NONE)
		{
			return false;
		}
		spell.ActivateState(SpellStateType.CANCEL);
		return true;
	}

	// Token: 0x060085B6 RID: 34230 RVA: 0x002B303E File Offset: 0x002B123E
	public static void PurgeSpell(Spell spell)
	{
		if (spell == null)
		{
			return;
		}
		if (!spell.CanPurge())
		{
			return;
		}
		UnityEngine.Object.Destroy(spell.gameObject);
	}

	// Token: 0x060085B7 RID: 34231 RVA: 0x002B3060 File Offset: 0x002B1260
	public static void PurgeSpells<T>(List<T> spells) where T : Spell
	{
		if (spells == null)
		{
			return;
		}
		if (spells.Count == 0)
		{
			return;
		}
		for (int i = 0; i < spells.Count; i++)
		{
			SpellUtils.PurgeSpell(spells[i]);
		}
	}

	// Token: 0x060085B8 RID: 34232 RVA: 0x002B309C File Offset: 0x002B129C
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
		return SpellUtils.FindAutoObjectForSpell(entity, sourceCard, cardType);
	}

	// Token: 0x060085B9 RID: 34233 RVA: 0x002B30F4 File Offset: 0x002B12F4
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
		return SpellUtils.FindAutoObjectForSpell(entity, component, cardType);
	}

	// Token: 0x060085BA RID: 34234 RVA: 0x002B313C File Offset: 0x002B133C
	private static GameObject FindAutoObjectForSpell(Entity entity, Card card, TAG_CARDTYPE cardType)
	{
		if (cardType == TAG_CARDTYPE.SPELL)
		{
			Card heroCard = entity.GetController().GetHeroCard();
			if (heroCard == null)
			{
				return card.gameObject;
			}
			return heroCard.gameObject;
		}
		else
		{
			if (cardType != TAG_CARDTYPE.HERO_POWER)
			{
				if (cardType == TAG_CARDTYPE.ENCHANTMENT)
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
				}
				return card.gameObject;
			}
			Card heroPowerCard = entity.GetController().GetHeroPowerCard();
			if (heroPowerCard == null)
			{
				return card.gameObject;
			}
			return heroPowerCard.gameObject;
		}
	}

	// Token: 0x060085BB RID: 34235 RVA: 0x002B31D0 File Offset: 0x002B13D0
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

	// Token: 0x060085BC RID: 34236 RVA: 0x002B3264 File Offset: 0x002B1464
	private static Card FindHeroCard(Card card)
	{
		if (card == null)
		{
			return null;
		}
		Player controller = card.GetEntity().GetController();
		if (controller == null)
		{
			return null;
		}
		return controller.GetHeroCard();
	}

	// Token: 0x060085BD RID: 34237 RVA: 0x002B3294 File Offset: 0x002B1494
	private static Card FindHeroPowerCard(Card card)
	{
		if (card == null)
		{
			return null;
		}
		Player controller = card.GetEntity().GetController();
		if (controller == null)
		{
			return null;
		}
		return controller.GetHeroPowerCard();
	}

	// Token: 0x060085BE RID: 34238 RVA: 0x002B32C3 File Offset: 0x002B14C3
	private static void FaceSameAs(GameObject source, GameObject target, SpellFacingOptions options)
	{
		SpellUtils.FaceSameAs(source.transform, target.transform, options);
	}

	// Token: 0x060085BF RID: 34239 RVA: 0x002B32D7 File Offset: 0x002B14D7
	private static void FaceSameAs(GameObject source, Component target, SpellFacingOptions options)
	{
		SpellUtils.FaceSameAs(source.transform, target.transform, options);
	}

	// Token: 0x060085C0 RID: 34240 RVA: 0x002B32EB File Offset: 0x002B14EB
	private static void FaceSameAs(Component source, GameObject target, SpellFacingOptions options)
	{
		SpellUtils.FaceSameAs(source.transform, target.transform, options);
	}

	// Token: 0x060085C1 RID: 34241 RVA: 0x002B32FF File Offset: 0x002B14FF
	private static void FaceSameAs(Component source, Component target, SpellFacingOptions options)
	{
		SpellUtils.FaceSameAs(source.transform, target.transform, options);
	}

	// Token: 0x060085C2 RID: 34242 RVA: 0x002B3313 File Offset: 0x002B1513
	private static void FaceSameAs(Transform source, Transform target, SpellFacingOptions options)
	{
		SpellUtils.SetOrientation(source, target.position, target.position + target.forward, options);
	}

	// Token: 0x060085C3 RID: 34243 RVA: 0x002B3333 File Offset: 0x002B1533
	private static void FaceOppositeOf(GameObject source, GameObject target, SpellFacingOptions options)
	{
		SpellUtils.FaceOppositeOf(source.transform, target.transform, options);
	}

	// Token: 0x060085C4 RID: 34244 RVA: 0x002B3347 File Offset: 0x002B1547
	private static void FaceOppositeOf(GameObject source, Component target, SpellFacingOptions options)
	{
		SpellUtils.FaceOppositeOf(source.transform, target.transform, options);
	}

	// Token: 0x060085C5 RID: 34245 RVA: 0x002B335B File Offset: 0x002B155B
	private static void FaceOppositeOf(Component source, GameObject target, SpellFacingOptions options)
	{
		SpellUtils.FaceOppositeOf(source.transform, target.transform, options);
	}

	// Token: 0x060085C6 RID: 34246 RVA: 0x002B336F File Offset: 0x002B156F
	private static void FaceOppositeOf(Component source, Component target, SpellFacingOptions options)
	{
		SpellUtils.FaceOppositeOf(source.transform, target.transform, options);
	}

	// Token: 0x060085C7 RID: 34247 RVA: 0x002B3383 File Offset: 0x002B1583
	private static void FaceOppositeOf(Transform source, Transform target, SpellFacingOptions options)
	{
		SpellUtils.SetOrientation(source, target.position, target.position - target.forward, options);
	}

	// Token: 0x060085C8 RID: 34248 RVA: 0x002B33A3 File Offset: 0x002B15A3
	private static void FaceTowards(GameObject source, GameObject target, SpellFacingOptions options)
	{
		SpellUtils.FaceTowards(source.transform, target.transform, options);
	}

	// Token: 0x060085C9 RID: 34249 RVA: 0x002B33B7 File Offset: 0x002B15B7
	private static void FaceTowards(GameObject source, Component target, SpellFacingOptions options)
	{
		SpellUtils.FaceTowards(source.transform, target.transform, options);
	}

	// Token: 0x060085CA RID: 34250 RVA: 0x002B33CB File Offset: 0x002B15CB
	private static void FaceTowards(Component source, GameObject target, SpellFacingOptions options)
	{
		SpellUtils.FaceTowards(source.transform, target.transform, options);
	}

	// Token: 0x060085CB RID: 34251 RVA: 0x002B33DF File Offset: 0x002B15DF
	private static void FaceTowards(Component source, Component target, SpellFacingOptions options)
	{
		SpellUtils.FaceTowards(source.transform, target.transform, options);
	}

	// Token: 0x060085CC RID: 34252 RVA: 0x002B33F3 File Offset: 0x002B15F3
	private static void FaceTowards(Transform source, Transform target, SpellFacingOptions options)
	{
		SpellUtils.SetOrientation(source, source.position, target.position, options);
	}

	// Token: 0x060085CD RID: 34253 RVA: 0x002B3408 File Offset: 0x002B1608
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

	// Token: 0x060085CE RID: 34254 RVA: 0x002B3478 File Offset: 0x002B1678
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

	// Token: 0x060085CF RID: 34255 RVA: 0x002B355B File Offset: 0x002B175B
	public static IEnumerator FlipActorAndReplaceWithCard(Actor actor, Card card, float time)
	{
		float halfTime = time * 0.5f;
		card.HideCard();
		Hashtable args = iTween.Hash(new object[]
		{
			"z",
			90,
			"time",
			halfTime,
			"easetype",
			iTween.EaseType.linear,
			"name",
			"SpellUtils.FlipActorAndReplaceWithCard"
		});
		iTween.RotateAdd(actor.gameObject, args);
		while (iTween.HasName(actor.gameObject, "SpellUtils.FlipActorAndReplaceWithCard"))
		{
			yield return null;
		}
		TransformUtil.CopyWorld(card, actor);
		card.transform.rotation *= Quaternion.Euler(0f, 0f, 180f);
		actor.Hide();
		card.ShowCard();
		args = iTween.Hash(new object[]
		{
			"z",
			90,
			"time",
			halfTime,
			"easetype",
			iTween.EaseType.linear,
			"name",
			"SpellUtils.FlipActorAndReplaceWithCard"
		});
		iTween.RotateAdd(card.gameObject, args);
		while (iTween.HasName(card.gameObject, "SpellUtils.FlipActorAndReplaceWithCard"))
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x060085D0 RID: 34256 RVA: 0x002B3578 File Offset: 0x002B1778
	public static IEnumerator FlipActorAndReplaceWithOtherActor(Actor actor, Actor otherActor, float time)
	{
		float halfTime = time * 0.5f;
		otherActor.Hide();
		Hashtable args = iTween.Hash(new object[]
		{
			"z",
			90,
			"time",
			halfTime,
			"easetype",
			iTween.EaseType.linear,
			"name",
			"SpellUtils.FlipActorAndReplaceWithOtherActor"
		});
		iTween.RotateAdd(actor.gameObject, args);
		while (iTween.HasName(actor.gameObject, "SpellUtils.FlipActorAndReplaceWithOtherActor"))
		{
			yield return null;
		}
		TransformUtil.CopyWorld(otherActor, actor);
		otherActor.transform.rotation *= Quaternion.Euler(0f, 0f, 180f);
		actor.Hide();
		otherActor.Show();
		args = iTween.Hash(new object[]
		{
			"z",
			90,
			"time",
			halfTime,
			"easetype",
			iTween.EaseType.linear,
			"name",
			"SpellUtils.FlipActorAndReplaceWithOtherActor"
		});
		iTween.RotateAdd(otherActor.gameObject, args);
		while (iTween.HasName(otherActor.gameObject, "SpellUtils.FlipActorAndReplaceWithOtherActor"))
		{
			yield return null;
		}
		yield break;
	}
}
