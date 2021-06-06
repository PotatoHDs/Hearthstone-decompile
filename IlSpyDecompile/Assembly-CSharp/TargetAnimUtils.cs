using UnityEngine;

public class TargetAnimUtils : MonoBehaviour
{
	public GameObject m_Target;

	private void Awake()
	{
		if (m_Target == null)
		{
			base.enabled = false;
		}
	}

	public void PrintLog(string message)
	{
		Debug.Log(message);
	}

	public void PrintLogWarning(string message)
	{
		Debug.LogWarning(message);
	}

	public void PrintLogError(string message)
	{
		Debug.LogError(message);
	}

	public void PlayNewParticles()
	{
		m_Target.GetComponent<ParticleSystem>().Play();
	}

	public void StopNewParticles()
	{
		if (!(m_Target == null))
		{
			m_Target.GetComponent<ParticleSystem>().Stop();
		}
	}

	public void PlayAnimation()
	{
		if (!(m_Target == null) && m_Target.GetComponent<Animation>() != null)
		{
			m_Target.GetComponent<Animation>().Play();
		}
	}

	public void StopAnimation()
	{
		if (!(m_Target == null) && m_Target.GetComponent<Animation>() != null)
		{
			m_Target.GetComponent<Animation>().Stop();
		}
	}

	public void PlayAnimationsInChildren()
	{
		if (!(m_Target == null))
		{
			Animation[] componentsInChildren = m_Target.GetComponentsInChildren<Animation>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Play();
			}
		}
	}

	public void StopAnimationsInChildren()
	{
		if (!(m_Target == null))
		{
			Animation[] componentsInChildren = m_Target.GetComponentsInChildren<Animation>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	public void ActivateHierarchy()
	{
		m_Target.SetActive(value: true);
	}

	public void DeactivateHierarchy()
	{
		if (!(m_Target == null))
		{
			m_Target.SetActive(value: false);
		}
	}

	public void DestroyHierarchy()
	{
		if (!(m_Target == null))
		{
			Object.Destroy(m_Target);
		}
	}

	public void FadeIn(float FadeSec)
	{
		if (!(m_Target == null))
		{
			iTween.FadeTo(m_Target, 1f, FadeSec);
		}
	}

	public void FadeOut(float FadeSec)
	{
		if (!(m_Target == null))
		{
			iTween.FadeTo(m_Target, 0f, FadeSec);
		}
	}

	public void SetAlphaHierarchy(float alpha)
	{
		if (m_Target == null)
		{
			return;
		}
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

	public void PlayDefaultSound()
	{
		if (!(m_Target == null))
		{
			if (m_Target.GetComponent<AudioSource>() == null)
			{
				Debug.LogError($"TargetAnimUtils.PlayDefaultSound() - Tried to play the AudioSource on {m_Target} but it has no AudioSource. You need an AudioSource to use this function.");
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
	}

	public void PlaySound(SoundDef clip)
	{
		if (!(m_Target == null))
		{
			if (clip == null)
			{
				Debug.LogError($"TargetAnimUtils.PlayDefaultSound() - No clip was given when trying to play the AudioSource on {m_Target}. You need a clip to use this function.");
			}
			else if (m_Target.GetComponent<AudioSource>() == null)
			{
				Debug.LogError($"TargetAnimUtils.PlayDefaultSound() - Tried to play clip {clip} on {m_Target} but it has no AudioSource. You need an AudioSource to use this function.");
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
}
