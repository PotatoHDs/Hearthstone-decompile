using System.Collections.Generic;
using UnityEngine;

public class GameModeIcon : PegUIElement
{
	public UberText m_text;

	public List<GameObject> m_Xmarks = new List<GameObject>();

	public GameObject m_friendlyChallengeBanner;

	public GameObject m_wildVines;

	public void Show(bool show)
	{
		base.gameObject.SetActive(show);
	}

	public void SetText(string text)
	{
		if (!(m_text == null))
		{
			m_text.Text = text;
		}
	}

	public void ShowXMarks(uint numberOfMarks)
	{
		if (m_Xmarks.Count != 0)
		{
			for (int i = 0; i < numberOfMarks; i++)
			{
				m_Xmarks[i].SetActive(value: true);
			}
		}
	}

	public void ShowFriendlyChallengeBanner(bool showBanner)
	{
		if (!(m_friendlyChallengeBanner == null))
		{
			m_friendlyChallengeBanner.SetActive(showBanner);
		}
	}

	public void ShowWildVines(bool showVines)
	{
		if (!(m_wildVines == null))
		{
			m_wildVines.SetActive(showVines);
		}
	}
}
