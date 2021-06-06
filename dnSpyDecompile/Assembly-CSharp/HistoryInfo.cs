using System;
using UnityEngine;

// Token: 0x02000319 RID: 793
public class HistoryInfo
{
	// Token: 0x06002C74 RID: 11380 RVA: 0x000DFDF0 File Offset: 0x000DDFF0
	public int GetSplatAmount()
	{
		int num = Mathf.Min((this.m_duplicatedEntity ?? this.m_originalEntity).GetDamage(), Mathf.Max(0, -this.m_maxHealthChangeAmount));
		int num2 = this.m_damageChangeAmount + num;
		if (this.m_armorChangeAmount <= 0)
		{
			return num2;
		}
		return num2 + this.m_armorChangeAmount;
	}

	// Token: 0x06002C75 RID: 11381 RVA: 0x000DFE44 File Offset: 0x000DE044
	public int GetCurrentVitality()
	{
		Entity entity = this.m_duplicatedEntity ?? this.m_originalEntity;
		int currentVitality = entity.GetCurrentVitality();
		int num = this.m_maxHealthChangeAmount;
		if (num < 0)
		{
			int damage = entity.GetDamage();
			num = Mathf.Min(0, damage + this.m_maxHealthChangeAmount);
		}
		return currentVitality + num;
	}

	// Token: 0x06002C76 RID: 11382 RVA: 0x000DFE8C File Offset: 0x000DE08C
	public bool HasValidDisplayEntity()
	{
		HistoryInfoType infoType = this.m_infoType;
		return infoType - HistoryInfoType.FATIGUE <= 1 || (this.GetDuplicatedEntity() != null && (!this.GetDuplicatedEntity().IsHidden() || this.GetDuplicatedEntity().IsSecret()));
	}

	// Token: 0x06002C77 RID: 11383 RVA: 0x000DFECF File Offset: 0x000DE0CF
	public Entity GetDuplicatedEntity()
	{
		return this.m_duplicatedEntity;
	}

	// Token: 0x06002C78 RID: 11384 RVA: 0x000DFED7 File Offset: 0x000DE0D7
	public Entity GetOriginalEntity()
	{
		return this.m_originalEntity;
	}

	// Token: 0x06002C79 RID: 11385 RVA: 0x000DFEDF File Offset: 0x000DE0DF
	public void SetOriginalEntity(Entity entity)
	{
		this.m_originalEntity = entity;
		this.DuplicateEntity(false, false);
	}

	// Token: 0x06002C7A RID: 11386 RVA: 0x000DFEF0 File Offset: 0x000DE0F0
	public bool HasDied()
	{
		Entity entity = this.m_duplicatedEntity ?? this.m_originalEntity;
		return (entity.IsCharacter() || entity.IsWeapon()) && (this.m_died || this.GetSplatAmount() >= this.GetCurrentVitality());
	}

	// Token: 0x06002C7B RID: 11387 RVA: 0x000DFF3B File Offset: 0x000DE13B
	public void SetDied(bool set)
	{
		this.m_died = set;
	}

	// Token: 0x06002C7C RID: 11388 RVA: 0x000DFF44 File Offset: 0x000DE144
	public bool CanDuplicateEntity(bool duplicateHiddenNonSecret, bool isEndOfHistory = false)
	{
		return this.m_originalEntity != null && this.m_originalEntity.GetLoadState() == Entity.LoadState.DONE && (isEndOfHistory || !this.m_dontDuplicateUntilEnd) && (!this.m_originalEntity.IsHidden() || (GameUtils.IsEntityHiddenAfterCurrentTasklist(this.m_originalEntity) && (this.m_originalEntity.IsSecret() || duplicateHiddenNonSecret)));
	}

	// Token: 0x06002C7D RID: 11389 RVA: 0x000DFFAC File Offset: 0x000DE1AC
	public void DuplicateEntity(bool duplicateHiddenNonSecret, bool isEndOfHistory)
	{
		if (this.m_duplicatedEntity != null)
		{
			return;
		}
		if (!this.CanDuplicateEntity(duplicateHiddenNonSecret, isEndOfHistory))
		{
			return;
		}
		this.m_duplicatedEntity = this.m_originalEntity.CloneForHistory(this);
		if (this.m_infoType == HistoryInfoType.CARD_PLAYED || this.m_infoType == HistoryInfoType.WEAPON_PLAYED)
		{
			this.m_duplicatedEntity.SetTag(GAME_TAG.COST, this.m_originalEntity.GetTag(GAME_TAG.TAG_LAST_KNOWN_COST_IN_HAND));
		}
	}

	// Token: 0x0400189B RID: 6299
	public HistoryInfoType m_infoType;

	// Token: 0x0400189C RID: 6300
	public int m_damageChangeAmount;

	// Token: 0x0400189D RID: 6301
	public int m_armorChangeAmount;

	// Token: 0x0400189E RID: 6302
	public int m_maxHealthChangeAmount;

	// Token: 0x0400189F RID: 6303
	public bool m_dontDuplicateUntilEnd;

	// Token: 0x040018A0 RID: 6304
	public bool m_isBurnedCard;

	// Token: 0x040018A1 RID: 6305
	public bool m_isPoisonous;

	// Token: 0x040018A2 RID: 6306
	private Entity m_originalEntity;

	// Token: 0x040018A3 RID: 6307
	private Entity m_duplicatedEntity;

	// Token: 0x040018A4 RID: 6308
	private bool m_died;
}
