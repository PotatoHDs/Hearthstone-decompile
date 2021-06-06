using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000979 RID: 2425
[CustomEditClass]
public class SpellTable : MonoBehaviour
{
	// Token: 0x0600858B RID: 34187 RVA: 0x002B1D28 File Offset: 0x002AFF28
	public SpellTableEntry FindEntry(SpellType type)
	{
		foreach (SpellTableEntry spellTableEntry in this.m_Table)
		{
			if (spellTableEntry.m_Type == type)
			{
				return spellTableEntry;
			}
		}
		return null;
	}

	// Token: 0x0600858C RID: 34188 RVA: 0x002B1D84 File Offset: 0x002AFF84
	public Spell GetSpell(SpellType spellType)
	{
		foreach (SpellTableEntry spellTableEntry in this.m_Table)
		{
			if (spellTableEntry.m_Type == spellType)
			{
				if (spellTableEntry.m_Spell == null && !string.IsNullOrEmpty(spellTableEntry.m_SpellPrefabName))
				{
					GameObject gameObject = AssetLoader.Get().InstantiatePrefab(spellTableEntry.m_SpellPrefabName, AssetLoadingOptions.None);
					Spell component = gameObject.GetComponent<Spell>();
					if (component != null)
					{
						spellTableEntry.m_Spell = component;
						TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, base.gameObject.transform);
					}
				}
				if (spellTableEntry.m_Spell == null)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Unable to load spell ",
						spellType,
						" from spell table ",
						base.gameObject.name
					}));
					return null;
				}
				Spell component2 = UnityEngine.Object.Instantiate<GameObject>(spellTableEntry.m_Spell.gameObject).GetComponent<Spell>();
				component2.SetSpellType(spellType);
				return component2;
			}
		}
		return null;
	}

	// Token: 0x0600858D RID: 34189 RVA: 0x002B1EB0 File Offset: 0x002B00B0
	public static Spell InstantiateSpell(SpellType spellType, string spellPrefabName)
	{
		Spell component = AssetLoader.Get().InstantiatePrefab(spellPrefabName, AssetLoadingOptions.None).GetComponent<Spell>();
		if (component != null)
		{
			component.SetSpellType(spellType);
		}
		return component;
	}

	// Token: 0x0600858E RID: 34190 RVA: 0x000A6C55 File Offset: 0x000A4E55
	public void ReleaseSpell(GameObject spellObject)
	{
		UnityEngine.Object.Destroy(spellObject);
	}

	// Token: 0x0600858F RID: 34191 RVA: 0x002B1EE8 File Offset: 0x002B00E8
	public void ReleaseAllSpells()
	{
		foreach (SpellTableEntry spellTableEntry in this.m_Table)
		{
			if (spellTableEntry.m_Spell != null)
			{
				UnityEngine.Object.DestroyImmediate(spellTableEntry.m_Spell.gameObject);
				UnityEngine.Object.DestroyImmediate(spellTableEntry.m_Spell);
				spellTableEntry.m_Spell = null;
			}
		}
	}

	// Token: 0x06008590 RID: 34192 RVA: 0x002B1F64 File Offset: 0x002B0164
	public bool IsLoaded(SpellType spellType)
	{
		Spell x;
		this.FindSpell(spellType, out x);
		return x != null;
	}

	// Token: 0x06008591 RID: 34193 RVA: 0x002B1F84 File Offset: 0x002B0184
	public void SetSpell(SpellType type, Spell spell)
	{
		foreach (SpellTableEntry spellTableEntry in this.m_Table)
		{
			if (spellTableEntry.m_Type == type)
			{
				if (spellTableEntry.m_Spell == null)
				{
					spellTableEntry.m_Spell = spell;
					TransformUtil.AttachAndPreserveLocalTransform(spell.gameObject.transform, base.gameObject.transform);
				}
				return;
			}
		}
		Debug.LogError(string.Concat(new object[]
		{
			"Set invalid spell type ",
			type,
			" in spell table ",
			base.gameObject.name
		}));
	}

	// Token: 0x06008592 RID: 34194 RVA: 0x002B2044 File Offset: 0x002B0244
	public bool FindSpell(SpellType spellType, out Spell spell)
	{
		foreach (SpellTableEntry spellTableEntry in this.m_Table)
		{
			if (spellTableEntry.m_Type == spellType)
			{
				spell = spellTableEntry.m_Spell;
				return true;
			}
		}
		spell = null;
		return false;
	}

	// Token: 0x06008593 RID: 34195 RVA: 0x002B20AC File Offset: 0x002B02AC
	public void Show()
	{
		foreach (SpellTableEntry spellTableEntry in this.m_Table)
		{
			if (!(spellTableEntry.m_Spell == null) && spellTableEntry.m_Type != SpellType.NONE)
			{
				spellTableEntry.m_Spell.Show();
			}
		}
	}

	// Token: 0x06008594 RID: 34196 RVA: 0x002B211C File Offset: 0x002B031C
	public void Hide()
	{
		foreach (SpellTableEntry spellTableEntry in this.m_Table)
		{
			if (!(spellTableEntry.m_Spell == null))
			{
				spellTableEntry.m_Spell.Hide();
			}
		}
	}

	// Token: 0x04006FF2 RID: 28658
	public List<SpellTableEntry> m_Table = new List<SpellTableEntry>();
}
