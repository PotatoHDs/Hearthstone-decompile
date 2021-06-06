using UnityEngine;

public class EmoteEntry
{
	private EmoteType m_emoteType;

	private Spell m_emoteSpell;

	private CardSoundSpell m_emoteSoundSpell;

	private string m_emoteGameStringKey;

	private string m_emoteSpellPath;

	private string m_emoteSoundSpellPath;

	private Card m_owner;

	public EmoteEntry(EmoteType type, string spellPath, string soundSpellPath, string stringKey, Card owner)
	{
		m_emoteType = type;
		m_emoteSpellPath = spellPath;
		m_emoteSoundSpellPath = soundSpellPath;
		m_emoteGameStringKey = stringKey;
		m_owner = owner;
	}

	public EmoteType GetEmoteType()
	{
		return m_emoteType;
	}

	public string GetGameStringKey()
	{
		if (m_emoteSoundSpell != null)
		{
			string text = m_emoteSoundSpell.DetermineGameStringKey();
			if (!string.IsNullOrEmpty(text))
			{
				m_emoteGameStringKey = text;
			}
		}
		return m_emoteGameStringKey;
	}

	private void LoadSoundSpell()
	{
		if (string.IsNullOrEmpty(m_emoteSoundSpellPath))
		{
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(m_emoteSoundSpellPath);
		if (gameObject == null)
		{
			Error.AddDevFatalUnlessWorkarounds("EmoteEntry.LoadSoundSpell() - Failed to load \"{0}\"", m_emoteSoundSpellPath);
			return;
		}
		m_emoteSoundSpell = gameObject.GetComponent<CardSoundSpell>();
		if (m_emoteSoundSpell == null)
		{
			Object.Destroy(gameObject);
			Error.AddDevFatalUnlessWorkarounds("EmoteEntry.LoadSoundSpell() - \"{0}\" does not have a Spell component.", m_emoteSoundSpellPath);
		}
		else if (m_owner != null)
		{
			SpellUtils.SetupSoundSpell(m_emoteSoundSpell, m_owner);
		}
	}

	public CardSoundSpell GetSoundSpell(bool loadIfNeeded = true)
	{
		if (m_emoteSoundSpell == null && loadIfNeeded)
		{
			LoadSoundSpell();
		}
		return m_emoteSoundSpell;
	}

	public Spell GetSpell(bool loadIfNeeded = true)
	{
		if (m_emoteSpell == null && loadIfNeeded && !string.IsNullOrEmpty(m_emoteSpellPath))
		{
			m_emoteSpell = SpellUtils.LoadAndSetupSpell(m_emoteSpellPath, m_owner);
		}
		return m_emoteSpell;
	}

	public void Clear()
	{
		if (m_emoteSoundSpell != null)
		{
			Object.Destroy(m_emoteSoundSpell.gameObject);
			m_emoteSoundSpell = null;
		}
		if (m_emoteSpell != null)
		{
			Object.Destroy(m_emoteSpell.gameObject);
			m_emoteSpell = null;
		}
	}
}
