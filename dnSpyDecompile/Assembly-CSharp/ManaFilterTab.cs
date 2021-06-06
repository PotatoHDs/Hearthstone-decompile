using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class ManaFilterTab : PegUIElement
{
	// Token: 0x06001405 RID: 5125 RVA: 0x00072FB3 File Offset: 0x000711B3
	protected override void Awake()
	{
		this.m_crystal.MarkAsNotInGame();
		base.Awake();
	}

	// Token: 0x06001406 RID: 5126 RVA: 0x00072FC8 File Offset: 0x000711C8
	public void SetFilterState(ManaFilterTab.FilterState state)
	{
		this.m_filterState = state;
		switch (this.m_filterState)
		{
		case ManaFilterTab.FilterState.ON:
			this.m_crystal.state = ManaCrystal.State.PROPOSED;
			return;
		case ManaFilterTab.FilterState.OFF:
			this.m_crystal.state = ManaCrystal.State.READY;
			return;
		case ManaFilterTab.FilterState.DISABLED:
			this.m_crystal.state = ManaCrystal.State.USED;
			return;
		default:
			return;
		}
	}

	// Token: 0x06001407 RID: 5127 RVA: 0x0007301C File Offset: 0x0007121C
	public void NotifyMousedOver()
	{
		if (this.m_filterState == ManaFilterTab.FilterState.ON)
		{
			return;
		}
		this.m_crystal.state = ManaCrystal.State.PROPOSED;
		SoundManager.Get().LoadAndPlay("mana_crystal_highlight_lp.prefab:279503c4945c5d640b9f7403d764a49b", base.gameObject, 1f, new SoundManager.LoadedCallback(this.ManaCrystalSoundCallback));
	}

	// Token: 0x06001408 RID: 5128 RVA: 0x0007306C File Offset: 0x0007126C
	public void NotifyMousedOut()
	{
		Action<object> action = delegate(object amount)
		{
			SoundManager.Get().SetVolume(this.m_mouseOverSound, (float)amount);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			1f,
			"to",
			0f,
			"time",
			0.5f,
			"easetype",
			iTween.EaseType.linear,
			"onupdate",
			action
		});
		iTween.Stop(base.gameObject);
		iTween.ValueTo(base.gameObject, args);
		if (this.m_filterState == ManaFilterTab.FilterState.ON)
		{
			return;
		}
		this.m_crystal.state = ManaCrystal.State.READY;
	}

	// Token: 0x06001409 RID: 5129 RVA: 0x00073120 File Offset: 0x00071320
	private void ManaCrystalSoundCallback(AudioSource source, object userData)
	{
		if (this.m_mouseOverSound != null)
		{
			SoundManager.Get().Stop(this.m_mouseOverSound);
		}
		this.m_mouseOverSound = source;
		SoundManager.Get().SetVolume(source, 0f);
		if (this.m_crystal.state != ManaCrystal.State.PROPOSED)
		{
			SoundManager.Get().Stop(this.m_mouseOverSound);
		}
		Action<object> action = delegate(object amount)
		{
			SoundManager.Get().SetVolume(source, (float)amount);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			0f,
			"to",
			1f,
			"time",
			0.5f,
			"easetype",
			iTween.EaseType.linear,
			"onupdate",
			action
		});
		iTween.Stop(base.gameObject);
		iTween.ValueTo(base.gameObject, args);
	}

	// Token: 0x0600140A RID: 5130 RVA: 0x00073228 File Offset: 0x00071428
	public void SetManaID(int manaID)
	{
		this.m_manaID = manaID;
		this.UpdateManaText();
	}

	// Token: 0x0600140B RID: 5131 RVA: 0x00073237 File Offset: 0x00071437
	public int GetManaID()
	{
		return this.m_manaID;
	}

	// Token: 0x0600140C RID: 5132 RVA: 0x00073240 File Offset: 0x00071440
	private void UpdateManaText()
	{
		string text = "";
		string text2 = "";
		if (this.m_manaID == -1)
		{
			text2 = GameStrings.Get("GLUE_COLLECTION_ALL");
		}
		else
		{
			text = this.m_manaID.ToString();
			if (this.m_manaID == 7)
			{
				if (UniversalInputManager.UsePhoneUI)
				{
					text += GameStrings.Get("GLUE_COLLECTION_PLUS");
				}
				else
				{
					text2 = GameStrings.Get("GLUE_COLLECTION_PLUS");
				}
			}
		}
		if (this.m_costText != null)
		{
			this.m_costText.Text = text;
		}
		if (this.m_otherText != null)
		{
			this.m_otherText.Text = text2;
		}
	}

	// Token: 0x04000D28 RID: 3368
	public const int ALL_TAB_INDEX = -1;

	// Token: 0x04000D29 RID: 3369
	public const int MIN_MANA_AMOUNT = 0;

	// Token: 0x04000D2A RID: 3370
	public const int MAX_MANA_AMOUNT = 7;

	// Token: 0x04000D2B RID: 3371
	public UberText m_costText;

	// Token: 0x04000D2C RID: 3372
	public UberText m_otherText;

	// Token: 0x04000D2D RID: 3373
	public ManaCrystal m_crystal;

	// Token: 0x04000D2E RID: 3374
	private int m_manaID;

	// Token: 0x04000D2F RID: 3375
	private ManaFilterTab.FilterState m_filterState;

	// Token: 0x04000D30 RID: 3376
	private AudioSource m_mouseOverSound;

	// Token: 0x020014CD RID: 5325
	public enum FilterState
	{
		// Token: 0x0400AADC RID: 43740
		ON,
		// Token: 0x0400AADD RID: 43741
		OFF,
		// Token: 0x0400AADE RID: 43742
		DISABLED
	}
}
