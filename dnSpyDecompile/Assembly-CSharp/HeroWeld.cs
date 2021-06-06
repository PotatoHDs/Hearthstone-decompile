using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000A3A RID: 2618
public class HeroWeld : MonoBehaviour
{
	// Token: 0x06008CD9 RID: 36057 RVA: 0x002D1AE8 File Offset: 0x002CFCE8
	public void DoAnim()
	{
		if (SoundManager.Get() == null)
		{
			this.m_weldInSound.GetComponent<AudioSource>().Play();
		}
		else
		{
			SoundManager.Get().Play(this.m_weldInSound.GetComponent<AudioSource>(), null, null, null);
		}
		base.gameObject.SetActive(true);
		this.m_lights = base.gameObject.GetComponentsInChildren<Light>();
		Light[] lights = this.m_lights;
		for (int i = 0; i < lights.Length; i++)
		{
			lights[i].enabled = true;
		}
		string text = "HeroWeldIn";
		base.gameObject.GetComponent<Animation>().Stop(text);
		base.gameObject.GetComponent<Animation>().Play(text);
		base.StartCoroutine(this.DestroyWhenFinished());
	}

	// Token: 0x06008CDA RID: 36058 RVA: 0x002D1B98 File Offset: 0x002CFD98
	private IEnumerator DestroyWhenFinished()
	{
		yield return new WaitForSeconds(5f);
		Light[] lights = this.m_lights;
		for (int i = 0; i < lights.Length; i++)
		{
			lights[i].enabled = false;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x040075A3 RID: 30115
	private Light[] m_lights;

	// Token: 0x040075A4 RID: 30116
	public AudioSource m_weldInSound;
}
