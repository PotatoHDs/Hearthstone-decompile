using UnityEngine;

public class PlayerLeaderboardInformationPanel : MonoBehaviour
{
	public UberText m_panelLabel;

	public void SetTitle(string text)
	{
		m_panelLabel.Text = text;
	}
}
