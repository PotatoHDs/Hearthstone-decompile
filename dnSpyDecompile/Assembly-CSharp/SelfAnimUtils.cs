using System;
using UnityEngine;

// Token: 0x02000A87 RID: 2695
public class SelfAnimUtils : MonoBehaviour
{
	// Token: 0x0600905F RID: 36959 RVA: 0x002EDC63 File Offset: 0x002EBE63
	public void PrintLog(string message)
	{
		Debug.Log(message);
	}

	// Token: 0x06009060 RID: 36960 RVA: 0x002EDC6B File Offset: 0x002EBE6B
	public void PrintLogWarning(string message)
	{
		Debug.LogWarning(message);
	}

	// Token: 0x06009061 RID: 36961 RVA: 0x002EDC73 File Offset: 0x002EBE73
	public void PrintLogError(string message)
	{
		Debug.LogError(message);
	}

	// Token: 0x06009062 RID: 36962 RVA: 0x002EDC7B File Offset: 0x002EBE7B
	public void PlayAnimation()
	{
		if (base.GetComponent<Animation>() != null)
		{
			base.GetComponent<Animation>().Play();
		}
	}

	// Token: 0x06009063 RID: 36963 RVA: 0x002EDC97 File Offset: 0x002EBE97
	public void StopAnimation()
	{
		if (base.GetComponent<Animation>() != null)
		{
			base.GetComponent<Animation>().Stop();
		}
	}

	// Token: 0x06009064 RID: 36964 RVA: 0x00028159 File Offset: 0x00026359
	public void ActivateHierarchy()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06009065 RID: 36965 RVA: 0x00028167 File Offset: 0x00026367
	public void DeactivateHierarchy()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06009066 RID: 36966 RVA: 0x0003DCF6 File Offset: 0x0003BEF6
	public void DestroyHierarchy()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06009067 RID: 36967 RVA: 0x002EDCB2 File Offset: 0x002EBEB2
	public void FadeIn(float FadeSec)
	{
		iTween.FadeTo(base.gameObject, 1f, FadeSec);
	}

	// Token: 0x06009068 RID: 36968 RVA: 0x002EDCC5 File Offset: 0x002EBEC5
	public void FadeOut(float FadeSec)
	{
		iTween.FadeTo(base.gameObject, 0f, FadeSec);
	}

	// Token: 0x06009069 RID: 36969 RVA: 0x002EDCD8 File Offset: 0x002EBED8
	public void SetAlphaHierarchy(float alpha)
	{
		Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>();
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

	// Token: 0x0600906A RID: 36970 RVA: 0x002EDD28 File Offset: 0x002EBF28
	public void PlayDefaultSound()
	{
		if (base.GetComponent<AudioSource>() == null)
		{
			Debug.LogError(string.Format("SelfAnimUtils.PlayDefaultSound() - Tried to play the AudioSource on {0} but it has no AudioSource. You need an AudioSource to use this function.", base.gameObject));
			return;
		}
		if (SoundManager.Get() == null)
		{
			base.GetComponent<AudioSource>().Play();
			return;
		}
		SoundManager.Get().Play(base.GetComponent<AudioSource>(), null, null, null);
	}

	// Token: 0x0600906B RID: 36971 RVA: 0x002EDD80 File Offset: 0x002EBF80
	public void PlaySound(SoundDef clip)
	{
		if (clip == null)
		{
			Debug.LogError(string.Format("SelfAnimUtils.PlayDefaultSound() - No clip was given when trying to play the AudioSource on {0}. You need a clip to use this function.", base.gameObject));
			return;
		}
		if (base.GetComponent<AudioSource>() == null)
		{
			Debug.LogError(string.Format("SelfAnimUtils.PlayDefaultSound() - Tried to play clip {0} on {1} but it has no AudioSource. You need an AudioSource to use this function.", clip, base.gameObject));
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
		SoundManager.Get().PlayOneShot(base.GetComponent<AudioSource>(), clip, 1f, null);
	}

	// Token: 0x0600906C RID: 36972 RVA: 0x002EDE0A File Offset: 0x002EC00A
	public void RandomRotationX()
	{
		TransformUtil.SetEulerAngleX(base.gameObject, UnityEngine.Random.Range(0f, 360f));
	}

	// Token: 0x0600906D RID: 36973 RVA: 0x002EDE26 File Offset: 0x002EC026
	public void RandomRotationY()
	{
		TransformUtil.SetEulerAngleY(base.gameObject, UnityEngine.Random.Range(0f, 360f));
	}

	// Token: 0x0600906E RID: 36974 RVA: 0x002EDE42 File Offset: 0x002EC042
	public void RandomRotationZ()
	{
		TransformUtil.SetEulerAngleZ(base.gameObject, UnityEngine.Random.Range(0f, 360f));
	}
}
