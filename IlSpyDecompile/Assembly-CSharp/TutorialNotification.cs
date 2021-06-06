public class TutorialNotification : Notification
{
	public UIBButton m_ButtonStart;

	public UberText m_WantedText;

	public void SetWantedText(string txt)
	{
		if (m_WantedText != null)
		{
			m_WantedText.Text = txt;
			m_WantedText.gameObject.SetActive(value: true);
		}
	}
}
