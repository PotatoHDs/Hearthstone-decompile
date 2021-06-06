using System.Collections;
using UnityEngine;

public class HeroWeld : MonoBehaviour
{
	private Light[] m_lights;

	public AudioSource m_weldInSound;

	public void DoAnim()
	{
		if (SoundManager.Get() == null)
		{
			m_weldInSound.GetComponent<AudioSource>().Play();
		}
		else
		{
			SoundManager.Get().Play(m_weldInSound.GetComponent<AudioSource>());
		}
		base.gameObject.SetActive(value: true);
		m_lights = base.gameObject.GetComponentsInChildren<Light>();
		Light[] lights = m_lights;
		for (int i = 0; i < lights.Length; i++)
		{
			lights[i].enabled = true;
		}
		string animation = "HeroWeldIn";
		base.gameObject.GetComponent<Animation>().Stop(animation);
		base.gameObject.GetComponent<Animation>().Play(animation);
		StartCoroutine(DestroyWhenFinished());
	}

	private IEnumerator DestroyWhenFinished()
	{
		yield return new WaitForSeconds(5f);
		Light[] lights = m_lights;
		for (int i = 0; i < lights.Length; i++)
		{
			lights[i].enabled = false;
		}
		Object.Destroy(base.gameObject);
	}
}
