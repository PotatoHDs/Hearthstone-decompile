using System;
using System.Collections;
using UnityEngine;

public class ManaFilterTab : PegUIElement
{
	public enum FilterState
	{
		ON,
		OFF,
		DISABLED
	}

	public const int ALL_TAB_INDEX = -1;

	public const int MIN_MANA_AMOUNT = 0;

	public const int MAX_MANA_AMOUNT = 7;

	public UberText m_costText;

	public UberText m_otherText;

	public ManaCrystal m_crystal;

	private int m_manaID;

	private FilterState m_filterState;

	private AudioSource m_mouseOverSound;

	protected override void Awake()
	{
		m_crystal.MarkAsNotInGame();
		base.Awake();
	}

	public void SetFilterState(FilterState state)
	{
		m_filterState = state;
		switch (m_filterState)
		{
		case FilterState.DISABLED:
			m_crystal.state = ManaCrystal.State.USED;
			break;
		case FilterState.OFF:
			m_crystal.state = ManaCrystal.State.READY;
			break;
		case FilterState.ON:
			m_crystal.state = ManaCrystal.State.PROPOSED;
			break;
		}
	}

	public void NotifyMousedOver()
	{
		if (m_filterState != 0)
		{
			m_crystal.state = ManaCrystal.State.PROPOSED;
			SoundManager.Get().LoadAndPlay("mana_crystal_highlight_lp.prefab:279503c4945c5d640b9f7403d764a49b", base.gameObject, 1f, ManaCrystalSoundCallback);
		}
	}

	public void NotifyMousedOut()
	{
		Action<object> action = delegate(object amount)
		{
			SoundManager.Get().SetVolume(m_mouseOverSound, (float)amount);
		};
		Hashtable args = iTween.Hash("from", 1f, "to", 0f, "time", 0.5f, "easetype", iTween.EaseType.linear, "onupdate", action);
		iTween.Stop(base.gameObject);
		iTween.ValueTo(base.gameObject, args);
		if (m_filterState != 0)
		{
			m_crystal.state = ManaCrystal.State.READY;
		}
	}

	private void ManaCrystalSoundCallback(AudioSource source, object userData)
	{
		if (m_mouseOverSound != null)
		{
			SoundManager.Get().Stop(m_mouseOverSound);
		}
		m_mouseOverSound = source;
		SoundManager.Get().SetVolume(source, 0f);
		if (m_crystal.state != ManaCrystal.State.PROPOSED)
		{
			SoundManager.Get().Stop(m_mouseOverSound);
		}
		Action<object> action = delegate(object amount)
		{
			SoundManager.Get().SetVolume(source, (float)amount);
		};
		Hashtable args = iTween.Hash("from", 0f, "to", 1f, "time", 0.5f, "easetype", iTween.EaseType.linear, "onupdate", action);
		iTween.Stop(base.gameObject);
		iTween.ValueTo(base.gameObject, args);
	}

	public void SetManaID(int manaID)
	{
		m_manaID = manaID;
		UpdateManaText();
	}

	public int GetManaID()
	{
		return m_manaID;
	}

	private void UpdateManaText()
	{
		string text = "";
		string text2 = "";
		if (m_manaID == -1)
		{
			text2 = GameStrings.Get("GLUE_COLLECTION_ALL");
		}
		else
		{
			text = m_manaID.ToString();
			if (m_manaID == 7)
			{
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					text += GameStrings.Get("GLUE_COLLECTION_PLUS");
				}
				else
				{
					text2 = GameStrings.Get("GLUE_COLLECTION_PLUS");
				}
			}
		}
		if (m_costText != null)
		{
			m_costText.Text = text;
		}
		if (m_otherText != null)
		{
			m_otherText.Text = text2;
		}
	}
}
