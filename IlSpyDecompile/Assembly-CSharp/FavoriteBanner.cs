using UnityEngine;

public class FavoriteBanner : MonoBehaviour
{
	public GameObject m_favoriteBanner;

	private Vector3 m_worldOffset;

	private void Start()
	{
		Vector3 position = m_favoriteBanner.transform.parent.position;
		m_worldOffset = m_favoriteBanner.transform.position - position;
	}

	public void PinToActor(Actor actor)
	{
		if (!(m_favoriteBanner == null))
		{
			m_favoriteBanner.transform.position = m_worldOffset + actor.transform.position;
			m_favoriteBanner.SetActive(value: true);
		}
	}

	public void SetActive(bool enable)
	{
		m_favoriteBanner.SetActive(enable);
	}
}
