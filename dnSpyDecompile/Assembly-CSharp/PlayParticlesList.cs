using System;
using UnityEngine;

// Token: 0x02000A62 RID: 2658
public class PlayParticlesList : MonoBehaviour
{
	// Token: 0x06008ECE RID: 36558 RVA: 0x002E1964 File Offset: 0x002DFB64
	public void PlayParticle(int theIndex)
	{
		if (theIndex < 0 || theIndex > this.m_objects.Length)
		{
			Debug.LogWarning("The index is out of range");
			return;
		}
		if (this.m_objects[theIndex] == null)
		{
			Debug.LogWarningFormat("{0} PlayParticlesList object is null", new object[]
			{
				base.gameObject.name
			});
			return;
		}
		this.m_objects[theIndex].GetComponent<ParticleSystem>().Play();
	}

	// Token: 0x04007727 RID: 30503
	public GameObject[] m_objects;
}
