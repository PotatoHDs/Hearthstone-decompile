using System;
using UnityEngine;

[Serializable]
public class GamesWonCrown
{
	public GameObject m_crown;

	public void Show()
	{
		m_crown.SetActive(value: true);
	}

	public void Hide()
	{
		m_crown.SetActive(value: false);
	}

	public void Animate()
	{
		Show();
		m_crown.GetComponent<PlayMakerFSM>().SendEvent("Birth");
	}
}
