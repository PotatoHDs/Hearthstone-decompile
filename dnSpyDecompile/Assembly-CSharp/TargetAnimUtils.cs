using System;
using UnityEngine;

// Token: 0x02000A99 RID: 2713
public class TargetAnimUtils : MonoBehaviour
{
	// Token: 0x060090CC RID: 37068 RVA: 0x002EF757 File Offset: 0x002ED957
	private void Awake()
	{
		if (this.m_Target == null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060090CD RID: 37069 RVA: 0x002EDC63 File Offset: 0x002EBE63
	public void PrintLog(string message)
	{
		Debug.Log(message);
	}

	// Token: 0x060090CE RID: 37070 RVA: 0x002EDC6B File Offset: 0x002EBE6B
	public void PrintLogWarning(string message)
	{
		Debug.LogWarning(message);
	}

	// Token: 0x060090CF RID: 37071 RVA: 0x002EDC73 File Offset: 0x002EBE73
	public void PrintLogError(string message)
	{
		Debug.LogError(message);
	}

	// Token: 0x060090D0 RID: 37072 RVA: 0x002EF76E File Offset: 0x002ED96E
	public void PlayNewParticles()
	{
		this.m_Target.GetComponent<ParticleSystem>().Play();
	}

	// Token: 0x060090D1 RID: 37073 RVA: 0x002EF780 File Offset: 0x002ED980
	public void StopNewParticles()
	{
		if (this.m_Target == null)
		{
			return;
		}
		this.m_Target.GetComponent<ParticleSystem>().Stop();
	}

	// Token: 0x060090D2 RID: 37074 RVA: 0x002EF7A1 File Offset: 0x002ED9A1
	public void PlayAnimation()
	{
		if (this.m_Target == null)
		{
			return;
		}
		if (this.m_Target.GetComponent<Animation>() != null)
		{
			this.m_Target.GetComponent<Animation>().Play();
		}
	}

	// Token: 0x060090D3 RID: 37075 RVA: 0x002EF7D6 File Offset: 0x002ED9D6
	public void StopAnimation()
	{
		if (this.m_Target == null)
		{
			return;
		}
		if (this.m_Target.GetComponent<Animation>() != null)
		{
			this.m_Target.GetComponent<Animation>().Stop();
		}
	}

	// Token: 0x060090D4 RID: 37076 RVA: 0x002EF80C File Offset: 0x002EDA0C
	public void PlayAnimationsInChildren()
	{
		if (this.m_Target == null)
		{
			return;
		}
		Animation[] componentsInChildren = this.m_Target.GetComponentsInChildren<Animation>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Play();
		}
	}

	// Token: 0x060090D5 RID: 37077 RVA: 0x002EF84C File Offset: 0x002EDA4C
	public void StopAnimationsInChildren()
	{
		if (this.m_Target == null)
		{
			return;
		}
		Animation[] componentsInChildren = this.m_Target.GetComponentsInChildren<Animation>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Stop();
		}
	}

	// Token: 0x060090D6 RID: 37078 RVA: 0x002EF88A File Offset: 0x002EDA8A
	public void ActivateHierarchy()
	{
		this.m_Target.SetActive(true);
	}

	// Token: 0x060090D7 RID: 37079 RVA: 0x002EF898 File Offset: 0x002EDA98
	public void DeactivateHierarchy()
	{
		if (this.m_Target == null)
		{
			return;
		}
		this.m_Target.SetActive(false);
	}

	// Token: 0x060090D8 RID: 37080 RVA: 0x002EF8B5 File Offset: 0x002EDAB5
	public void DestroyHierarchy()
	{
		if (this.m_Target == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_Target);
	}

	// Token: 0x060090D9 RID: 37081 RVA: 0x002EF8D1 File Offset: 0x002EDAD1
	public void FadeIn(float FadeSec)
	{
		if (this.m_Target == null)
		{
			return;
		}
		iTween.FadeTo(this.m_Target, 1f, FadeSec);
	}

	// Token: 0x060090DA RID: 37082 RVA: 0x002EF8F3 File Offset: 0x002EDAF3
	public void FadeOut(float FadeSec)
	{
		if (this.m_Target == null)
		{
			return;
		}
		iTween.FadeTo(this.m_Target, 0f, FadeSec);
	}

	// Token: 0x060090DB RID: 37083 RVA: 0x002EF918 File Offset: 0x002EDB18
	public void SetAlphaHierarchy(float alpha)
	{
		if (this.m_Target == null)
		{
			return;
		}
		Renderer[] componentsInChildren = this.m_Target.GetComponentsInChildren<Renderer>();
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

	// Token: 0x060090DC RID: 37084 RVA: 0x002EF97C File Offset: 0x002EDB7C
	public void PlayDefaultSound()
	{
		if (this.m_Target == null)
		{
			return;
		}
		if (this.m_Target.GetComponent<AudioSource>() == null)
		{
			Debug.LogError(string.Format("TargetAnimUtils.PlayDefaultSound() - Tried to play the AudioSource on {0} but it has no AudioSource. You need an AudioSource to use this function.", this.m_Target));
			return;
		}
		if (SoundManager.Get() == null)
		{
			this.m_Target.GetComponent<AudioSource>().Play();
			return;
		}
		SoundManager.Get().Play(this.m_Target.GetComponent<AudioSource>(), null, null, null);
	}

	// Token: 0x060090DD RID: 37085 RVA: 0x002EF9F4 File Offset: 0x002EDBF4
	public void PlaySound(SoundDef clip)
	{
		if (this.m_Target == null)
		{
			return;
		}
		if (clip == null)
		{
			Debug.LogError(string.Format("TargetAnimUtils.PlayDefaultSound() - No clip was given when trying to play the AudioSource on {0}. You need a clip to use this function.", this.m_Target));
			return;
		}
		if (this.m_Target.GetComponent<AudioSource>() == null)
		{
			Debug.LogError(string.Format("TargetAnimUtils.PlayDefaultSound() - Tried to play clip {0} on {1} but it has no AudioSource. You need an AudioSource to use this function.", clip, this.m_Target));
			return;
		}
		if (SoundManager.Get() == null)
		{
			Debug.LogErrorFormat("TargetAnimutils2: SoundManager is null attempting to play {0}", new object[]
			{
				clip.m_AudioClip
			});
			return;
		}
		SoundManager.Get().PlayOneShot(this.m_Target.GetComponent<AudioSource>(), clip, 1f, null);
	}

	// Token: 0x040079B8 RID: 31160
	public GameObject m_Target;
}
