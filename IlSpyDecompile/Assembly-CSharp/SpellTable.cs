using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class SpellTable : MonoBehaviour
{
	public List<SpellTableEntry> m_Table = new List<SpellTableEntry>();

	public SpellTableEntry FindEntry(SpellType type)
	{
		foreach (SpellTableEntry item in m_Table)
		{
			if (item.m_Type == type)
			{
				return item;
			}
		}
		return null;
	}

	public Spell GetSpell(SpellType spellType)
	{
		foreach (SpellTableEntry item in m_Table)
		{
			if (item.m_Type != spellType)
			{
				continue;
			}
			if (item.m_Spell == null && !string.IsNullOrEmpty(item.m_SpellPrefabName))
			{
				GameObject gameObject = AssetLoader.Get().InstantiatePrefab(item.m_SpellPrefabName);
				Spell component = gameObject.GetComponent<Spell>();
				if (component != null)
				{
					item.m_Spell = component;
					TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, base.gameObject.transform);
				}
			}
			if (item.m_Spell == null)
			{
				Debug.LogError(string.Concat("Unable to load spell ", spellType, " from spell table ", base.gameObject.name));
				return null;
			}
			Spell component2 = Object.Instantiate(item.m_Spell.gameObject).GetComponent<Spell>();
			component2.SetSpellType(spellType);
			return component2;
		}
		return null;
	}

	public static Spell InstantiateSpell(SpellType spellType, string spellPrefabName)
	{
		Spell component = AssetLoader.Get().InstantiatePrefab(spellPrefabName).GetComponent<Spell>();
		if (component != null)
		{
			component.SetSpellType(spellType);
		}
		return component;
	}

	public void ReleaseSpell(GameObject spellObject)
	{
		Object.Destroy(spellObject);
	}

	public void ReleaseAllSpells()
	{
		foreach (SpellTableEntry item in m_Table)
		{
			if (item.m_Spell != null)
			{
				Object.DestroyImmediate(item.m_Spell.gameObject);
				Object.DestroyImmediate(item.m_Spell);
				item.m_Spell = null;
			}
		}
	}

	public bool IsLoaded(SpellType spellType)
	{
		FindSpell(spellType, out var spell);
		return spell != null;
	}

	public void SetSpell(SpellType type, Spell spell)
	{
		foreach (SpellTableEntry item in m_Table)
		{
			if (item.m_Type == type)
			{
				if (item.m_Spell == null)
				{
					item.m_Spell = spell;
					TransformUtil.AttachAndPreserveLocalTransform(spell.gameObject.transform, base.gameObject.transform);
				}
				return;
			}
		}
		Debug.LogError(string.Concat("Set invalid spell type ", type, " in spell table ", base.gameObject.name));
	}

	public bool FindSpell(SpellType spellType, out Spell spell)
	{
		foreach (SpellTableEntry item in m_Table)
		{
			if (item.m_Type == spellType)
			{
				spell = item.m_Spell;
				return true;
			}
		}
		spell = null;
		return false;
	}

	public void Show()
	{
		foreach (SpellTableEntry item in m_Table)
		{
			if (!(item.m_Spell == null) && item.m_Type != 0)
			{
				item.m_Spell.Show();
			}
		}
	}

	public void Hide()
	{
		foreach (SpellTableEntry item in m_Table)
		{
			if (!(item.m_Spell == null))
			{
				item.m_Spell.Hide();
			}
		}
	}
}
