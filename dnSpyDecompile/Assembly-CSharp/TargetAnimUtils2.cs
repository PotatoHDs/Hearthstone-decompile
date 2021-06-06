using System;
using UnityEngine;

// Token: 0x02000A9A RID: 2714
public class TargetAnimUtils2 : MonoBehaviour
{
	// Token: 0x060090DF RID: 37087 RVA: 0x002EDC63 File Offset: 0x002EBE63
	public void PrintLog2(string message)
	{
		Debug.Log(message);
	}

	// Token: 0x060090E0 RID: 37088 RVA: 0x002EDC6B File Offset: 0x002EBE6B
	public void PrintLogWarning2(string message)
	{
		Debug.LogWarning(message);
	}

	// Token: 0x060090E1 RID: 37089 RVA: 0x002EDC73 File Offset: 0x002EBE73
	public void PrintLogError2(string message)
	{
		Debug.LogError(message);
	}

	// Token: 0x060090E2 RID: 37090 RVA: 0x002EFA97 File Offset: 0x002EDC97
	public void PlayNewParticles2()
	{
		this.m_Target.GetComponent<ParticleSystem>().Play();
	}

	// Token: 0x060090E3 RID: 37091 RVA: 0x002EFAA9 File Offset: 0x002EDCA9
	public void StopNewParticles2()
	{
		this.m_Target.GetComponent<ParticleSystem>().Stop();
	}

	// Token: 0x060090E4 RID: 37092 RVA: 0x002EFABB File Offset: 0x002EDCBB
	public void PlayAnimation2()
	{
		if (this.m_Target.GetComponent<Animation>() != null)
		{
			this.m_Target.GetComponent<Animation>().Play();
		}
	}

	// Token: 0x060090E5 RID: 37093 RVA: 0x002EFAE1 File Offset: 0x002EDCE1
	public void StopAnimation2()
	{
		if (this.m_Target.GetComponent<Animation>() != null)
		{
			this.m_Target.GetComponent<Animation>().Stop();
		}
	}

	// Token: 0x060090E6 RID: 37094 RVA: 0x002EFB08 File Offset: 0x002EDD08
	public void PlayAnimationsInChildren2()
	{
		Animation[] componentsInChildren = this.m_Target.GetComponentsInChildren<Animation>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Play();
		}
	}

	// Token: 0x060090E7 RID: 37095 RVA: 0x002EFB38 File Offset: 0x002EDD38
	public void StopAnimationsInChildren2()
	{
		Animation[] componentsInChildren = this.m_Target.GetComponentsInChildren<Animation>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Stop();
		}
	}

	// Token: 0x060090E8 RID: 37096 RVA: 0x002EFB67 File Offset: 0x002EDD67
	public void ActivateHierarchy2()
	{
		this.m_Target.SetActive(true);
	}

	// Token: 0x060090E9 RID: 37097 RVA: 0x002EFB75 File Offset: 0x002EDD75
	public void DeactivateHierarchy2()
	{
		this.m_Target.SetActive(false);
	}

	// Token: 0x060090EA RID: 37098 RVA: 0x002EFB83 File Offset: 0x002EDD83
	public void DestroyHierarchy2()
	{
		UnityEngine.Object.Destroy(this.m_Target);
	}

	// Token: 0x060090EB RID: 37099 RVA: 0x002EFB90 File Offset: 0x002EDD90
	public void FadeIn2(float FadeSec)
	{
		iTween.FadeTo(this.m_Target, 1f, FadeSec);
	}

	// Token: 0x060090EC RID: 37100 RVA: 0x002EFBA3 File Offset: 0x002EDDA3
	public void FadeOut2(float FadeSec)
	{
		iTween.FadeTo(this.m_Target, 0f, FadeSec);
	}

	// Token: 0x060090ED RID: 37101 RVA: 0x002EFBB8 File Offset: 0x002EDDB8
	public void SetAlphaHierarchy2(float alpha)
	{
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

	// Token: 0x060090EE RID: 37102 RVA: 0x002EFC0C File Offset: 0x002EDE0C
	public void PlayDefaultSound2()
	{
		if (this.m_Target.GetComponent<AudioSource>() == null)
		{
			Debug.LogError(string.Format("TargetAnimUtils2.PlayDefaultSound() - Tried to play the AudioSource on {0} but it has no AudioSource. You need an AudioSource to use this function.", this.m_Target));
			return;
		}
		if (SoundManager.Get() == null)
		{
			this.m_Target.GetComponent<AudioSource>().Play();
			return;
		}
		SoundManager.Get().Play(this.m_Target.GetComponent<AudioSource>(), null, null, null);
	}

	// Token: 0x060090EF RID: 37103 RVA: 0x002EFC74 File Offset: 0x002EDE74
	public void PlaySound2(SoundDef clip)
	{
		if (clip == null)
		{
			Debug.LogError(string.Format("TargetAnimUtils2.PlayDefaultSound() - No clip was given when trying to play the AudioSource on {0}. You need a clip to use this function.", this.m_Target));
			return;
		}
		if (this.m_Target.GetComponent<AudioSource>() == null)
		{
			Debug.LogError(string.Format("TargetAnimUtils2.PlayDefaultSound() - Tried to play clip {0} on {1} but it has no AudioSource. You need an AudioSource to use this function.", clip, this.m_Target));
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

	// Token: 0x040079B9 RID: 31161
	public GameObject m_Target;
}
