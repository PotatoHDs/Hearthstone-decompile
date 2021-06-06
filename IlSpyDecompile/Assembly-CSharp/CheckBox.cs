using UnityEngine;

[CustomEditClass]
public class CheckBox : PegUIElement
{
	public GameObject m_check;

	public TextMesh m_text;

	public UberText m_uberText;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_checkOnSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_checkOffSound;

	private bool m_checked;

	private int m_buttonID;

	protected override void OnOver(InteractionState oldState)
	{
		if (base.gameObject.activeInHierarchy)
		{
			SetState(InteractionState.Over);
		}
	}

	protected override void OnOut(InteractionState oldState)
	{
		if (base.gameObject.activeInHierarchy)
		{
			SetState(InteractionState.Up);
		}
	}

	protected override void OnPress()
	{
		if (base.gameObject.activeInHierarchy)
		{
			SetState(InteractionState.Down);
		}
	}

	protected override void OnRelease()
	{
		if (base.gameObject.activeInHierarchy)
		{
			ToggleChecked();
			if (m_checked && !string.IsNullOrEmpty(m_checkOnSound))
			{
				SoundManager.Get().LoadAndPlay(m_checkOnSound);
			}
			else if (!m_checked && !string.IsNullOrEmpty(m_checkOffSound))
			{
				SoundManager.Get().LoadAndPlay(m_checkOffSound);
			}
			SetState(InteractionState.Over);
		}
	}

	public void SetButtonText(string s)
	{
		if (m_text != null)
		{
			m_text.text = s;
		}
		if (m_uberText != null)
		{
			m_uberText.Text = s;
		}
	}

	public void SetButtonID(int id)
	{
		m_buttonID = id;
	}

	public int GetButtonID()
	{
		return m_buttonID;
	}

	public void SetState(InteractionState state)
	{
		SetEnabled(enabled: true);
		switch (state)
		{
		}
	}

	public virtual void SetChecked(bool isChecked)
	{
		m_checked = isChecked;
		if (m_check != null)
		{
			m_check.SetActive(m_checked);
		}
	}

	public bool IsChecked()
	{
		return m_checked;
	}

	private bool ToggleChecked()
	{
		SetChecked(!m_checked);
		return m_checked;
	}
}
