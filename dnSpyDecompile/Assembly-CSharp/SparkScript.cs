using System;
using UnityEngine;

// Token: 0x0200075C RID: 1884
public class SparkScript : MonoBehaviour
{
	// Token: 0x060069D6 RID: 27094 RVA: 0x002282A4 File Offset: 0x002264A4
	private void Awake()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (UnityEngine.Random.value >= 0.5f)
		{
			component.clip = this.clip1;
			return;
		}
		component.clip = this.clip2;
	}

	// Token: 0x040056A1 RID: 22177
	public AudioClip clip1;

	// Token: 0x040056A2 RID: 22178
	public AudioClip clip2;
}
