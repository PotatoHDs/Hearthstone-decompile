using UnityEngine;

public class PlayParticlesList : MonoBehaviour
{
	public GameObject[] m_objects;

	public void PlayParticle(int theIndex)
	{
		if (theIndex < 0 || theIndex > m_objects.Length)
		{
			Debug.LogWarning("The index is out of range");
		}
		else if (m_objects[theIndex] == null)
		{
			Debug.LogWarningFormat("{0} PlayParticlesList object is null", base.gameObject.name);
		}
		else
		{
			m_objects[theIndex].GetComponent<ParticleSystem>().Play();
		}
	}
}
