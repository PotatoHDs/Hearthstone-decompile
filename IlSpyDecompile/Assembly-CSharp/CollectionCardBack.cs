using UnityEngine;

public class CollectionCardBack : MonoBehaviour
{
	public UberText m_name;

	public GameObject m_favoriteBanner;

	public GameObject m_nameFrame;

	private int m_cardBackId = -1;

	private int m_seasonId = -1;

	public void Awake()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_nameFrame.SetActive(value: false);
		}
	}

	public void SetCardBackId(int id)
	{
		m_cardBackId = id;
	}

	public int GetCardBackId()
	{
		return m_cardBackId;
	}

	public void SetSeasonId(int seasonId)
	{
		m_seasonId = seasonId;
	}

	public int GetSeasonId()
	{
		return m_seasonId;
	}

	public void SetCardBackName(string name)
	{
		if (!(m_name == null))
		{
			m_name.Text = name;
		}
	}

	public void ShowFavoriteBanner(bool show)
	{
		if (!(m_favoriteBanner == null))
		{
			m_favoriteBanner.SetActive(show);
		}
	}
}
