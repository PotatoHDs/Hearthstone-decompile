using System.Collections.Generic;
using System.Linq;

public class CollectionDeckSlot
{
	public delegate void DelOnSlotEmptied(CollectionDeckSlot slot);

	public DelOnSlotEmptied OnSlotEmptied;

	private int m_index;

	private List<int> m_count = new List<int>(new int[EnumUtils.Length<TAG_PREMIUM>()]);

	private string m_cardId;

	private bool m_owned = true;

	public EntityDef m_entityDefOverride;

	public int Index
	{
		get
		{
			return m_index;
		}
		set
		{
			m_index = value;
		}
	}

	public TAG_PREMIUM PreferredPremium
	{
		get
		{
			TAG_PREMIUM preferredPremium = CollectionManager.Get().GetPreferredPremium();
			if (m_count[(int)preferredPremium] > 0)
			{
				return preferredPremium;
			}
			preferredPremium = TAG_PREMIUM.NORMAL;
			for (int num = m_count.Count - 1; num > 0; num--)
			{
				if (m_count[num] > 0)
				{
					return (TAG_PREMIUM)num;
				}
			}
			return preferredPremium;
		}
	}

	public TAG_PREMIUM UnPreferredPremium
	{
		get
		{
			TAG_PREMIUM preferredPremium = CollectionManager.Get().GetPreferredPremium();
			for (int i = 0; i < m_count.Count; i++)
			{
				if (m_count[i] > 0 && i != (int)preferredPremium)
				{
					return (TAG_PREMIUM)i;
				}
			}
			return preferredPremium;
		}
	}

	public int Count => m_count.Sum();

	public string CardID
	{
		get
		{
			return m_cardId;
		}
		set
		{
			m_cardId = value;
		}
	}

	public bool Owned
	{
		get
		{
			return m_owned;
		}
		set
		{
			m_owned = value;
		}
	}

	public override string ToString()
	{
		return $"[CollectionDeckSlot: Index={Index}, PreferredPremium={PreferredPremium}, Count={Count}, CardID={CardID}]";
	}

	public int GetCount(TAG_PREMIUM premium)
	{
		return m_count[(int)premium];
	}

	public void SetCount(int count, TAG_PREMIUM premium)
	{
		m_count[(int)premium] = count;
		if (Count <= 0 && OnSlotEmptied != null)
		{
			OnSlotEmptied(this);
		}
	}

	public void RemoveCard(int count, TAG_PREMIUM premium)
	{
		m_count[(int)premium] -= count;
		if (Count <= 0 && OnSlotEmptied != null)
		{
			OnSlotEmptied(this);
		}
	}

	public void AddCard(int count, TAG_PREMIUM premium)
	{
		m_count[(int)premium] += count;
	}

	public void CreateDynamicEntity()
	{
		if (m_entityDefOverride == null)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(m_cardId);
			m_entityDefOverride = entityDef.Clone();
		}
	}

	public EntityDef GetEntityDef()
	{
		if (m_entityDefOverride != null)
		{
			return m_entityDefOverride;
		}
		return DefLoader.Get().GetEntityDef(m_cardId);
	}

	public void CopyFrom(CollectionDeckSlot otherSlot)
	{
		Index = otherSlot.Index;
		m_count = new List<int>(otherSlot.m_count);
		CardID = otherSlot.CardID;
		Owned = otherSlot.Owned;
	}
}
