using System.Collections.Generic;
using UnityEngine;

public class CardEffect
{
	private Spell m_spell;

	private List<CardSoundSpell> m_soundSpells;

	private string m_spellPath;

	private List<string> m_soundSpellPaths;

	private Card m_owner;

	public CardEffect(CardEffectDef def, Card owner)
	{
		m_spellPath = def.m_SpellPath;
		m_soundSpellPaths = def.m_SoundSpellPaths;
		m_owner = owner;
		if (m_soundSpellPaths != null)
		{
			m_soundSpells = new List<CardSoundSpell>(m_soundSpellPaths.Count);
			for (int i = 0; i < m_soundSpellPaths.Count; i++)
			{
				m_soundSpells.Add(null);
			}
		}
	}

	public CardEffect(string spellPath, Card owner)
	{
		m_spellPath = spellPath;
		m_owner = owner;
	}

	public Spell GetSpell(bool loadIfNeeded = true)
	{
		if (m_spell == null && !string.IsNullOrEmpty(m_spellPath) && loadIfNeeded)
		{
			m_spell = SpellUtils.LoadAndSetupSpell(m_spellPath, m_owner);
		}
		return m_spell;
	}

	public void LoadSoundSpell(int index)
	{
		if (index < 0 || m_soundSpellPaths == null || index >= m_soundSpellPaths.Count || string.IsNullOrEmpty(m_soundSpellPaths[index]) || !(m_soundSpells[index] == null))
		{
			return;
		}
		string text = m_soundSpellPaths[index];
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text);
		if (gameObject == null)
		{
			Error.AddDevFatal("CardEffect.LoadSoundSpell() - FAILED TO LOAD \"{0}\" (PATH: \"{1}\") (index {2})", m_spellPath, text, index);
			return;
		}
		CardSoundSpell component = gameObject.GetComponent<CardSoundSpell>();
		m_soundSpells[index] = component;
		if (component == null)
		{
			Error.AddDevFatal("CardEffect.LoadSoundSpell() - FAILED TO LOAD \"{0}\" (PATH: \"{1}\") (index {2})", m_spellPath, text, index);
		}
		else if (m_owner != null)
		{
			SpellUtils.SetupSoundSpell(component, m_owner);
		}
	}

	public List<CardSoundSpell> GetSoundSpells(bool loadIfNeeded = true)
	{
		if (m_soundSpells == null)
		{
			return null;
		}
		if (loadIfNeeded)
		{
			for (int i = 0; i < m_soundSpells.Count; i++)
			{
				LoadSoundSpell(i);
			}
		}
		return m_soundSpells;
	}

	public void Clear()
	{
		if (m_spell != null)
		{
			Object.Destroy(m_spell.gameObject);
		}
		if (m_soundSpells == null)
		{
			return;
		}
		for (int i = 0; i < m_soundSpells.Count; i++)
		{
			Spell spell = m_soundSpells[i];
			if (spell != null)
			{
				Object.Destroy(spell.gameObject);
			}
		}
	}

	public void LoadAll()
	{
		GetSpell();
		if (m_soundSpellPaths != null)
		{
			for (int i = 0; i < m_soundSpellPaths.Count; i++)
			{
				LoadSoundSpell(i);
			}
		}
	}

	public void PurgeSpells()
	{
		SpellUtils.PurgeSpell(m_spell);
		SpellUtils.PurgeSpells(m_soundSpells);
	}

	private void DestroySpell(Spell spell)
	{
		if (!(spell == null))
		{
			Object.Destroy(spell.gameObject);
		}
	}
}
