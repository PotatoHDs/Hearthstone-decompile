using UnityEngine;

public class HistoryInfo
{
	public HistoryInfoType m_infoType;

	public int m_damageChangeAmount;

	public int m_armorChangeAmount;

	public int m_maxHealthChangeAmount;

	public bool m_dontDuplicateUntilEnd;

	public bool m_isBurnedCard;

	public bool m_isPoisonous;

	private Entity m_originalEntity;

	private Entity m_duplicatedEntity;

	private bool m_died;

	public int GetSplatAmount()
	{
		int num = Mathf.Min((m_duplicatedEntity ?? m_originalEntity).GetDamage(), Mathf.Max(0, -m_maxHealthChangeAmount));
		int num2 = m_damageChangeAmount + num;
		if (m_armorChangeAmount <= 0)
		{
			return num2;
		}
		return num2 + m_armorChangeAmount;
	}

	public int GetCurrentVitality()
	{
		Entity entity = m_duplicatedEntity ?? m_originalEntity;
		int currentVitality = entity.GetCurrentVitality();
		int num = m_maxHealthChangeAmount;
		if (num < 0)
		{
			int damage = entity.GetDamage();
			num = Mathf.Min(0, damage + m_maxHealthChangeAmount);
		}
		return currentVitality + num;
	}

	public bool HasValidDisplayEntity()
	{
		HistoryInfoType infoType = m_infoType;
		if ((uint)(infoType - 6) <= 1u)
		{
			return true;
		}
		if (GetDuplicatedEntity() == null)
		{
			return false;
		}
		if (GetDuplicatedEntity().IsHidden() && !GetDuplicatedEntity().IsSecret())
		{
			return false;
		}
		return true;
	}

	public Entity GetDuplicatedEntity()
	{
		return m_duplicatedEntity;
	}

	public Entity GetOriginalEntity()
	{
		return m_originalEntity;
	}

	public void SetOriginalEntity(Entity entity)
	{
		m_originalEntity = entity;
		DuplicateEntity(duplicateHiddenNonSecret: false, isEndOfHistory: false);
	}

	public bool HasDied()
	{
		Entity entity = m_duplicatedEntity ?? m_originalEntity;
		if (!entity.IsCharacter() && !entity.IsWeapon())
		{
			return false;
		}
		if (m_died)
		{
			return true;
		}
		if (GetSplatAmount() >= GetCurrentVitality())
		{
			return true;
		}
		return false;
	}

	public void SetDied(bool set)
	{
		m_died = set;
	}

	public bool CanDuplicateEntity(bool duplicateHiddenNonSecret, bool isEndOfHistory = false)
	{
		if (m_originalEntity == null)
		{
			return false;
		}
		if (m_originalEntity.GetLoadState() != Entity.LoadState.DONE)
		{
			return false;
		}
		if (!isEndOfHistory && m_dontDuplicateUntilEnd)
		{
			return false;
		}
		if (!m_originalEntity.IsHidden())
		{
			return true;
		}
		if (!GameUtils.IsEntityHiddenAfterCurrentTasklist(m_originalEntity))
		{
			return false;
		}
		if (m_originalEntity.IsSecret())
		{
			return true;
		}
		if (duplicateHiddenNonSecret)
		{
			return true;
		}
		return false;
	}

	public void DuplicateEntity(bool duplicateHiddenNonSecret, bool isEndOfHistory)
	{
		if (m_duplicatedEntity == null && CanDuplicateEntity(duplicateHiddenNonSecret, isEndOfHistory))
		{
			m_duplicatedEntity = m_originalEntity.CloneForHistory(this);
			if (m_infoType == HistoryInfoType.CARD_PLAYED || m_infoType == HistoryInfoType.WEAPON_PLAYED)
			{
				m_duplicatedEntity.SetTag(GAME_TAG.COST, m_originalEntity.GetTag(GAME_TAG.TAG_LAST_KNOWN_COST_IN_HAND));
			}
		}
	}
}
