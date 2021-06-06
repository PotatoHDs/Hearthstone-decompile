using System;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x02000968 RID: 2408
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RitualSpellConfig", order = 1)]
public class RitualSpellConfig : ScriptableObject
{
	// Token: 0x06008468 RID: 33896 RVA: 0x002AD8B8 File Offset: 0x002ABAB8
	public bool IsRitualEntity(global::Entity entity)
	{
		if (entity == null)
		{
			return false;
		}
		string cardId = entity.GetCardId();
		return this.IsRitualEntity(cardId);
	}

	// Token: 0x06008469 RID: 33897 RVA: 0x002AD8D8 File Offset: 0x002ABAD8
	public bool IsRitualEntity(string cardId)
	{
		return this.m_ritualCardIds.Contains(cardId);
	}

	// Token: 0x0600846A RID: 33898 RVA: 0x002AD8E8 File Offset: 0x002ABAE8
	public bool IsRitualEntityInPlay(global::Player controller)
	{
		if (controller == null)
		{
			return false;
		}
		switch (this.m_ritualEntityType)
		{
		case TAG_CARDTYPE.HERO:
		{
			global::Entity hero = controller.GetHero();
			return hero != null && this.IsRitualEntity(hero);
		}
		case TAG_CARDTYPE.MINION:
			using (List<Card>.Enumerator enumerator = controller.GetBattlefieldZone().GetCards().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Card card = enumerator.Current;
					if (this.IsRitualEntity(card.GetEntity()))
					{
						return true;
					}
				}
				return false;
			}
			break;
		case TAG_CARDTYPE.SPELL:
			foreach (Card card2 in controller.GetSecretZone().GetCards())
			{
				if (this.IsRitualEntity(card2.GetEntity()))
				{
					return true;
				}
			}
			return false;
		case TAG_CARDTYPE.ENCHANTMENT:
			return false;
		case TAG_CARDTYPE.WEAPON:
			break;
		default:
			return false;
		}
		Card weaponCard = controller.GetWeaponCard();
		return this.IsRitualEntity(weaponCard.GetEntity());
	}

	// Token: 0x0600846B RID: 33899 RVA: 0x002AD9FC File Offset: 0x002ABBFC
	public Spell GetRitualActivateSpell(global::Entity ritualEntity)
	{
		if (!this.IsRitualEntity(ritualEntity))
		{
			return null;
		}
		return this.GetRitualSpellForClass(ritualEntity.GetClass(), true);
	}

	// Token: 0x0600846C RID: 33900 RVA: 0x002ADA16 File Offset: 0x002ABC16
	public Spell GetRitualTriggerSpell(global::Entity ritualEntity)
	{
		if (!this.IsRitualEntity(ritualEntity))
		{
			return null;
		}
		return this.GetRitualSpellForClass(ritualEntity.GetClass(), false);
	}

	// Token: 0x0600846D RID: 33901 RVA: 0x002ADA30 File Offset: 0x002ABC30
	private Spell GetRitualSpellForClass(TAG_CLASS entityClass, bool isActivate)
	{
		foreach (RitualSpellConfig.ClassSpecificRitualConfig classSpecificRitualConfig in this.m_classSpecificRitualConfig)
		{
			if (classSpecificRitualConfig.m_class == entityClass)
			{
				if (isActivate)
				{
					return classSpecificRitualConfig.m_ritualPortalSpell;
				}
				return classSpecificRitualConfig.m_ritualEffectSpell;
			}
		}
		return null;
	}

	// Token: 0x0600846E RID: 33902 RVA: 0x002ADAA0 File Offset: 0x002ABCA0
	public bool DoesTaskListContainRitualEntity(PowerTaskList powerTaskList, int entityID)
	{
		if (powerTaskList.GetBlockType() != HistoryBlock.Type.TRIGGER)
		{
			return false;
		}
		foreach (PowerTask powerTask in powerTaskList.GetTaskList())
		{
			Network.HistChangeEntity histChangeEntity = powerTask.GetPower() as Network.HistChangeEntity;
			if (histChangeEntity != null && histChangeEntity.Entity.ID == entityID && this.IsRitualEntity(histChangeEntity.Entity.CardID))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600846F RID: 33903 RVA: 0x002ADB2C File Offset: 0x002ABD2C
	public bool DoesFutureTaskListContainsRitualEntity(List<PowerTaskList> futureTaskLists, PowerTaskList currentTaskList, int entityID)
	{
		foreach (PowerTaskList powerTaskList in futureTaskLists)
		{
			if (powerTaskList != null && powerTaskList.IsDescendantOfBlock(currentTaskList) && this.DoesTaskListContainRitualEntity(powerTaskList, entityID))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06008470 RID: 33904 RVA: 0x002ADB90 File Offset: 0x002ABD90
	public Actor LoadRitualActor(global::Entity entity)
	{
		if (entity == null)
		{
			return null;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetZoneActor(entity, TAG_ZONE.PLAY), AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Log.Spells.PrintError("RitualSpellConfig unable to load Invoke Actor GameObject.", Array.Empty<object>());
			return null;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Log.Spells.PrintError("RitualSpellConfig Invoke Actor GameObject contains no Actor component.", Array.Empty<object>());
			UnityEngine.Object.Destroy(gameObject);
			return null;
		}
		component.SetEntity(entity);
		component.SetCardDefFromEntity(entity);
		return component;
	}

	// Token: 0x06008471 RID: 33905 RVA: 0x002ADC14 File Offset: 0x002ABE14
	public void UpdateAndPositionActor(Actor actor)
	{
		if (actor == null)
		{
			return;
		}
		if (this.m_hideRitualActor)
		{
			actor.Hide();
		}
		string name = (actor.GetEntity().GetControllerSide() == global::Player.Side.FRIENDLY) ? this.m_friendlyBoneName : this.m_opponentBoneName;
		Transform parent = Board.Get().FindBone(name);
		actor.transform.parent = parent;
		actor.transform.localPosition = Vector3.zero;
	}

	// Token: 0x06008472 RID: 33906 RVA: 0x002ADC80 File Offset: 0x002ABE80
	public void UpdateRitualActorComponents(Actor actor)
	{
		if (actor != null)
		{
			GameObject attackObject = actor.m_attackObject;
			if (attackObject != null)
			{
				attackObject.SetActive(this.m_showAttack);
			}
		}
		if (actor != null)
		{
			GameObject healthObject = actor.m_healthObject;
			if (healthObject != null)
			{
				healthObject.SetActive(this.m_showHealth);
			}
		}
		if (actor != null)
		{
			GameObject armorSpellBone = actor.m_armorSpellBone;
			if (armorSpellBone == null)
			{
				return;
			}
			armorSpellBone.SetActive(this.m_showArmor);
		}
	}

	// Token: 0x04006F72 RID: 28530
	public List<RitualSpellConfig.ClassSpecificRitualConfig> m_classSpecificRitualConfig = new List<RitualSpellConfig.ClassSpecificRitualConfig>();

	// Token: 0x04006F73 RID: 28531
	public List<string> m_ritualCardIds = new List<string>();

	// Token: 0x04006F74 RID: 28532
	public TAG_CARDTYPE m_ritualEntityType = TAG_CARDTYPE.HERO;

	// Token: 0x04006F75 RID: 28533
	public string m_friendlyBoneName = "FriendlyRitual";

	// Token: 0x04006F76 RID: 28534
	public string m_opponentBoneName = "OpponentRitual";

	// Token: 0x04006F77 RID: 28535
	public bool m_hideRitualActor = true;

	// Token: 0x04006F78 RID: 28536
	public bool m_showAttack;

	// Token: 0x04006F79 RID: 28537
	public bool m_showHealth;

	// Token: 0x04006F7A RID: 28538
	public bool m_showArmor;

	// Token: 0x04006F7B RID: 28539
	public GAME_TAG m_proxyRitualEntityTag;

	// Token: 0x04006F7C RID: 28540
	public bool m_showRitualVisualsInPlay;

	// Token: 0x04006F7D RID: 28541
	public string m_portalSpellEventName = "showRitualActor";

	// Token: 0x0200262C RID: 9772
	[Serializable]
	public class ClassSpecificRitualConfig
	{
		// Token: 0x0400EFE3 RID: 61411
		public TAG_CLASS m_class;

		// Token: 0x0400EFE4 RID: 61412
		public Spell m_ritualPortalSpell;

		// Token: 0x0400EFE5 RID: 61413
		public Spell m_ritualEffectSpell;
	}
}
