using System;
using UnityEngine;

// Token: 0x02000B0F RID: 2831
[CustomEditClass]
public class DustJarPanel : MonoBehaviour
{
	// Token: 0x0600969D RID: 38557 RVA: 0x0030BD64 File Offset: 0x00309F64
	public void Show(int dustAmount)
	{
		this.m_dustCount.Text = dustAmount.ToString();
		Vector3 localScale = this.m_dustJar.transform.localScale;
		this.m_dustJar.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.ScaleTo(this.m_dustJar.gameObject, iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			0.5f,
			"easetype",
			iTween.EaseType.easeOutElastic
		}));
		if (this.m_dustJarEntranceSound != null)
		{
			SoundManager.Get().Play(UnityEngine.Object.Instantiate<AudioSource>(this.m_dustJarEntranceSound, base.transform), null, null, null);
		}
	}

	// Token: 0x04007E29 RID: 32297
	[CustomEditField(Sections = "Dust Panel")]
	public GameObject m_dustJar;

	// Token: 0x04007E2A RID: 32298
	[CustomEditField(Sections = "Dust Panel")]
	public UberText m_dustCount;

	// Token: 0x04007E2B RID: 32299
	[CustomEditField(Sections = "Dust Panel")]
	public AudioSource m_dustJarEntranceSound;
}
