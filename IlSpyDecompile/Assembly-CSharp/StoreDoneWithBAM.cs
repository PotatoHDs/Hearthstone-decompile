using System.Collections.Generic;

public class StoreDoneWithBAM : UIBPopup
{
	public delegate void ButtonPressedListener();

	public UIBButton m_okayButton;

	public UberText m_headlineText;

	public UberText m_messageText;

	private List<ButtonPressedListener> m_okayListeners = new List<ButtonPressedListener>();

	protected override void Awake()
	{
		base.Awake();
		m_okayButton.AddEventListener(UIEventType.RELEASE, OnOkayPressed);
	}

	public void RegisterOkayListener(ButtonPressedListener listener)
	{
		if (!m_okayListeners.Contains(listener))
		{
			m_okayListeners.Add(listener);
		}
	}

	public void RemoveOkayListener(ButtonPressedListener listener)
	{
		m_okayListeners.Remove(listener);
	}

	private void OnOkayPressed(UIEvent e)
	{
		Hide(animate: true);
		ButtonPressedListener[] array = m_okayListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}
}
