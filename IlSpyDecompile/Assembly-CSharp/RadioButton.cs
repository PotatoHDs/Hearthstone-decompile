using UnityEngine;

public class RadioButton : PegUIElement
{
	public GameObject m_hoverGlow;

	public GameObject m_selectedGlow;

	private int m_id;

	private object m_userData;

	protected override void Awake()
	{
		base.Awake();
		m_hoverGlow.SetActive(value: false);
		m_selectedGlow.SetActive(value: false);
		SoundManager.Get().Load("tiny_button_press_2.prefab:dab8dd96f82865041bbf96a32e47642e");
		SoundManager.Get().Load("tiny_button_mouseover_2.prefab:ba1a1effe29265246b1cb3d833c8ac78");
	}

	public void SetButtonID(int id)
	{
		m_id = id;
	}

	public int GetButtonID()
	{
		return m_id;
	}

	public void SetUserData(object userData)
	{
		m_userData = userData;
	}

	public object GetUserData()
	{
		return m_userData;
	}

	public void SetSelected(bool selected)
	{
		m_selectedGlow.SetActive(selected);
	}

	public bool IsSelected()
	{
		return m_selectedGlow.activeSelf;
	}

	protected override void OnOver(InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("tiny_button_mouseover_2.prefab:ba1a1effe29265246b1cb3d833c8ac78");
		m_hoverGlow.SetActive(value: true);
	}

	protected override void OnOut(InteractionState oldState)
	{
		m_hoverGlow.SetActive(value: false);
	}

	protected override void OnRelease()
	{
		base.OnRelease();
		SoundManager.Get().LoadAndPlay("tiny_button_press_2.prefab:dab8dd96f82865041bbf96a32e47642e");
	}

	protected override void OnDoubleClick()
	{
	}
}
