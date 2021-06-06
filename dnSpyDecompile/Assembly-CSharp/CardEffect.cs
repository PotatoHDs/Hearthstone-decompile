using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000873 RID: 2163
public class CardEffect
{
	// Token: 0x060075EB RID: 30187 RVA: 0x0025D868 File Offset: 0x0025BA68
	public CardEffect(CardEffectDef def, Card owner)
	{
		this.m_spellPath = def.m_SpellPath;
		this.m_soundSpellPaths = def.m_SoundSpellPaths;
		this.m_owner = owner;
		if (this.m_soundSpellPaths != null)
		{
			this.m_soundSpells = new List<CardSoundSpell>(this.m_soundSpellPaths.Count);
			for (int i = 0; i < this.m_soundSpellPaths.Count; i++)
			{
				this.m_soundSpells.Add(null);
			}
		}
	}

	// Token: 0x060075EC RID: 30188 RVA: 0x0025D8DA File Offset: 0x0025BADA
	public CardEffect(string spellPath, Card owner)
	{
		this.m_spellPath = spellPath;
		this.m_owner = owner;
	}

	// Token: 0x060075ED RID: 30189 RVA: 0x0025D8F0 File Offset: 0x0025BAF0
	public Spell GetSpell(bool loadIfNeeded = true)
	{
		if (this.m_spell == null && !string.IsNullOrEmpty(this.m_spellPath) && loadIfNeeded)
		{
			this.m_spell = SpellUtils.LoadAndSetupSpell(this.m_spellPath, this.m_owner);
		}
		return this.m_spell;
	}

	// Token: 0x060075EE RID: 30190 RVA: 0x0025D940 File Offset: 0x0025BB40
	public void LoadSoundSpell(int index)
	{
		if (index < 0 || this.m_soundSpellPaths == null || index >= this.m_soundSpellPaths.Count || string.IsNullOrEmpty(this.m_soundSpellPaths[index]))
		{
			return;
		}
		if (this.m_soundSpells[index] == null)
		{
			string text = this.m_soundSpellPaths[index];
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.None);
			if (gameObject == null)
			{
				Error.AddDevFatal("CardEffect.LoadSoundSpell() - FAILED TO LOAD \"{0}\" (PATH: \"{1}\") (index {2})", new object[]
				{
					this.m_spellPath,
					text,
					index
				});
				return;
			}
			CardSoundSpell component = gameObject.GetComponent<CardSoundSpell>();
			this.m_soundSpells[index] = component;
			if (component == null)
			{
				Error.AddDevFatal("CardEffect.LoadSoundSpell() - FAILED TO LOAD \"{0}\" (PATH: \"{1}\") (index {2})", new object[]
				{
					this.m_spellPath,
					text,
					index
				});
				return;
			}
			if (this.m_owner != null)
			{
				SpellUtils.SetupSoundSpell(component, this.m_owner);
			}
		}
	}

	// Token: 0x060075EF RID: 30191 RVA: 0x0025DA40 File Offset: 0x0025BC40
	public List<CardSoundSpell> GetSoundSpells(bool loadIfNeeded = true)
	{
		if (this.m_soundSpells == null)
		{
			return null;
		}
		if (loadIfNeeded)
		{
			for (int i = 0; i < this.m_soundSpells.Count; i++)
			{
				this.LoadSoundSpell(i);
			}
		}
		return this.m_soundSpells;
	}

	// Token: 0x060075F0 RID: 30192 RVA: 0x0025DA80 File Offset: 0x0025BC80
	public void Clear()
	{
		if (this.m_spell != null)
		{
			UnityEngine.Object.Destroy(this.m_spell.gameObject);
		}
		if (this.m_soundSpells != null)
		{
			for (int i = 0; i < this.m_soundSpells.Count; i++)
			{
				Spell spell = this.m_soundSpells[i];
				if (spell != null)
				{
					UnityEngine.Object.Destroy(spell.gameObject);
				}
			}
		}
	}

	// Token: 0x060075F1 RID: 30193 RVA: 0x0025DAEC File Offset: 0x0025BCEC
	public void LoadAll()
	{
		this.GetSpell(true);
		if (this.m_soundSpellPaths != null)
		{
			for (int i = 0; i < this.m_soundSpellPaths.Count; i++)
			{
				this.LoadSoundSpell(i);
			}
		}
	}

	// Token: 0x060075F2 RID: 30194 RVA: 0x0025DB26 File Offset: 0x0025BD26
	public void PurgeSpells()
	{
		SpellUtils.PurgeSpell(this.m_spell);
		SpellUtils.PurgeSpells<CardSoundSpell>(this.m_soundSpells);
	}

	// Token: 0x060075F3 RID: 30195 RVA: 0x0025DB3E File Offset: 0x0025BD3E
	private void DestroySpell(Spell spell)
	{
		if (spell == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(spell.gameObject);
	}

	// Token: 0x04005D24 RID: 23844
	private Spell m_spell;

	// Token: 0x04005D25 RID: 23845
	private List<CardSoundSpell> m_soundSpells;

	// Token: 0x04005D26 RID: 23846
	private string m_spellPath;

	// Token: 0x04005D27 RID: 23847
	private List<string> m_soundSpellPaths;

	// Token: 0x04005D28 RID: 23848
	private Card m_owner;
}
