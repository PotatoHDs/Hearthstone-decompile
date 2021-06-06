using UnityEngine;

public class PlayNewParticles : MonoBehaviour
{
	public GameObject m_Target;

	public GameObject m_Target2;

	public GameObject m_Target3;

	public GameObject m_Target4;

	public void PlayNewParticles3()
	{
		if (!(m_Target == null))
		{
			m_Target.GetComponent<ParticleSystem>().Play();
		}
	}

	public void StopNewParticles3()
	{
		if (!(m_Target == null))
		{
			m_Target.GetComponent<ParticleSystem>().Stop();
		}
	}

	public void PlayNewParticles3andChilds()
	{
		if (!(m_Target2 == null))
		{
			m_Target2.GetComponent<ParticleSystem>().Play(withChildren: true);
		}
	}

	public void StopNewParticles3andChilds()
	{
		if (!(m_Target2 == null))
		{
			m_Target2.GetComponent<ParticleSystem>().Stop(withChildren: true);
		}
	}

	public void PlayNewParticles3andChilds2()
	{
		if (!(m_Target3 == null))
		{
			m_Target3.GetComponent<ParticleSystem>().Play(withChildren: true);
		}
	}

	public void StopNewParticles3andChilds2()
	{
		if (!(m_Target3 == null))
		{
			m_Target3.GetComponent<ParticleSystem>().Stop(withChildren: true);
		}
	}

	public void PlayNewParticles3andChilds3()
	{
		if (!(m_Target4 == null))
		{
			m_Target4.GetComponent<ParticleSystem>().Play(withChildren: true);
		}
	}

	public void StopNewParticles3andChilds3()
	{
		if (!(m_Target4 == null))
		{
			m_Target4.GetComponent<ParticleSystem>().Stop(withChildren: true);
		}
	}
}
