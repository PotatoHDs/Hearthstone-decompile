using UnityEngine;

public class OptionsMenuPhone : MonoBehaviour
{
	public OptionsMenu m_optionsMenu;

	public UIBButton m_doneButton;

	public GameObject m_mainContentsPanel;

	private void Start()
	{
		m_doneButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			m_optionsMenu.Hide();
		});
	}
}
