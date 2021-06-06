using System;
using UnityEngine;

// Token: 0x020008A0 RID: 2208
public class EmoteEntry
{
	// Token: 0x0600797F RID: 31103 RVA: 0x00279ECE File Offset: 0x002780CE
	public EmoteEntry(EmoteType type, string spellPath, string soundSpellPath, string stringKey, Card owner)
	{
		this.m_emoteType = type;
		this.m_emoteSpellPath = spellPath;
		this.m_emoteSoundSpellPath = soundSpellPath;
		this.m_emoteGameStringKey = stringKey;
		this.m_owner = owner;
	}

	// Token: 0x06007980 RID: 31104 RVA: 0x00279EFB File Offset: 0x002780FB
	public EmoteType GetEmoteType()
	{
		return this.m_emoteType;
	}

	// Token: 0x06007981 RID: 31105 RVA: 0x00279F04 File Offset: 0x00278104
	public string GetGameStringKey()
	{
		if (this.m_emoteSoundSpell != null)
		{
			string text = this.m_emoteSoundSpell.DetermineGameStringKey();
			if (!string.IsNullOrEmpty(text))
			{
				this.m_emoteGameStringKey = text;
			}
		}
		return this.m_emoteGameStringKey;
	}

	// Token: 0x06007982 RID: 31106 RVA: 0x00279F40 File Offset: 0x00278140
	private void LoadSoundSpell()
	{
		if (string.IsNullOrEmpty(this.m_emoteSoundSpellPath))
		{
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(this.m_emoteSoundSpellPath, AssetLoadingOptions.None);
		if (gameObject == null)
		{
			Error.AddDevFatalUnlessWorkarounds("EmoteEntry.LoadSoundSpell() - Failed to load \"{0}\"", new object[]
			{
				this.m_emoteSoundSpellPath
			});
			return;
		}
		this.m_emoteSoundSpell = gameObject.GetComponent<CardSoundSpell>();
		if (this.m_emoteSoundSpell == null)
		{
			UnityEngine.Object.Destroy(gameObject);
			Error.AddDevFatalUnlessWorkarounds("EmoteEntry.LoadSoundSpell() - \"{0}\" does not have a Spell component.", new object[]
			{
				this.m_emoteSoundSpellPath
			});
			return;
		}
		if (this.m_owner != null)
		{
			SpellUtils.SetupSoundSpell(this.m_emoteSoundSpell, this.m_owner);
		}
	}

	// Token: 0x06007983 RID: 31107 RVA: 0x00279FEE File Offset: 0x002781EE
	public CardSoundSpell GetSoundSpell(bool loadIfNeeded = true)
	{
		if (this.m_emoteSoundSpell == null && loadIfNeeded)
		{
			this.LoadSoundSpell();
		}
		return this.m_emoteSoundSpell;
	}

	// Token: 0x06007984 RID: 31108 RVA: 0x0027A00C File Offset: 0x0027820C
	public Spell GetSpell(bool loadIfNeeded = true)
	{
		if (this.m_emoteSpell == null && loadIfNeeded && !string.IsNullOrEmpty(this.m_emoteSpellPath))
		{
			this.m_emoteSpell = SpellUtils.LoadAndSetupSpell(this.m_emoteSpellPath, this.m_owner);
		}
		return this.m_emoteSpell;
	}

	// Token: 0x06007985 RID: 31109 RVA: 0x0027A048 File Offset: 0x00278248
	public void Clear()
	{
		if (this.m_emoteSoundSpell != null)
		{
			UnityEngine.Object.Destroy(this.m_emoteSoundSpell.gameObject);
			this.m_emoteSoundSpell = null;
		}
		if (this.m_emoteSpell != null)
		{
			UnityEngine.Object.Destroy(this.m_emoteSpell.gameObject);
			this.m_emoteSpell = null;
		}
	}

	// Token: 0x04005EA0 RID: 24224
	private EmoteType m_emoteType;

	// Token: 0x04005EA1 RID: 24225
	private Spell m_emoteSpell;

	// Token: 0x04005EA2 RID: 24226
	private CardSoundSpell m_emoteSoundSpell;

	// Token: 0x04005EA3 RID: 24227
	private string m_emoteGameStringKey;

	// Token: 0x04005EA4 RID: 24228
	private string m_emoteSpellPath;

	// Token: 0x04005EA5 RID: 24229
	private string m_emoteSoundSpellPath;

	// Token: 0x04005EA6 RID: 24230
	private Card m_owner;
}
