using UnityEngine;

public class TargetAnimUtils2 : MonoBehaviour
{
	public GameObject m_Target;

	public void PrintLog2(string message)
	{
		Debug.Log(message);
	}

	public void PrintLogWarning2(string message)
	{
		Debug.LogWarning(message);
	}

	public void PrintLogError2(string message)
	{
		Debug.LogError(message);
	}

	public void PlayNewParticles2()
	{
		m_Target.GetComponent<ParticleSystem>().Play();
	}

	public void StopNewParticles2()
	{
		m_Target.GetComponent<ParticleSystem>().Stop();
	}

	public void PlayAnimation2()
	{
		if (m_Target.GetComponent<Animation>() != null)
		{
			m_Target.GetComponent<Animation>().Play();
		}
	}

	public void StopAnimation2()
	{
		if (m_Target.GetComponent<Animation>() != null)
		{
			m_Target.GetComponent<Animation>().Stop();
		}
	}

	public void PlayAnimationsInChildren2()
	{
		Animation[] componentsInChildren = m_Target.GetComponentsInChildren<Animation>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Play();
		}
	}

	public void StopAnimationsInChildren2()
	{
		Animation[] componentsInChildren = m_Target.GetComponentsInChildren<Animation>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Stop();
		}
	}

	public void ActivateHierarchy2()
	{
		m_Target.SetActive(value: true);
	}

	public void DeactivateHierarchy2()
	{
		m_Target.SetActive(value: false);
	}

	public void DestroyHierarchy2()
	{
		Object.Destroy(m_Target);
	}

	public void FadeIn2(float FadeSec)
	{
		iTween.FadeTo(m_Target, 1f, FadeSec);
	}

	public void FadeOut2(float FadeSec)
	{
		iTween.FadeTo(m_Target, 0f, FadeSec);
	}

	public void SetAlphaHierarchy2(float alpha)
	{
		Renderer[] componentsInChildren = m_Target.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Material material = componentsInChildren[i].GetMaterial();
			if (material.HasProperty("_Color"))
			{
				Color color = material.color;
				color.a = alpha;
				material.color = color;
			}
		}
	}

	public void PlayDefaultSound2()
	{
		if (m_Target.GetComponent<AudioSource>() == null)
		{
			Debug.LogError($"TargetAnimUtils2.PlayDefaultSound() - Tried to play the AudioSource on {m_Target} but it has no AudioSource. You need an AudioSource to use this function.");
		}
		else if (SoundManager.Get() == null)
		{
			m_Target.GetComponent<AudioSource>().Play();
		}
		else
		{
			SoundManager.Get().Play(m_Target.GetComponent<AudioSource>());
		}
	}

	public void PlaySound2(SoundDef clip)
	{
		if (clip == null)
		{
			Debug.LogError($"TargetAnimUtils2.PlayDefaultSound() - No clip was given when trying to play the AudioSource on {m_Target}. You need a clip to use this function.");
		}
		else if (m_Target.GetComponent<AudioSource>() == null)
		{
			Debug.LogError($"TargetAnimUtils2.PlayDefaultSound() - Tried to play clip {clip} on {m_Target} but it has no AudioSource. You need an AudioSource to use this function.");
		}
		else if (SoundManager.Get() == null)
		{
			Debug.LogErrorFormat("TargetAnimutils2: SoundManager is null attempting to play {0}", clip.m_AudioClip);
		}
		else
		{
			SoundManager.Get().PlayOneShot(m_Target.GetComponent<AudioSource>(), clip);
		}
	}
}
