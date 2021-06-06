using System;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public class TGTArrow : MonoBehaviour
{
	// Token: 0x06000B05 RID: 2821 RVA: 0x00041850 File Offset: 0x0003FA50
	private void onEnable()
	{
		this.m_ArrowRoot.transform.localEulerAngles = new Vector3(0f, 170f, 0f);
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00041878 File Offset: 0x0003FA78
	public void FireArrow(bool randomRotation)
	{
		if (randomRotation)
		{
			Vector3 localEulerAngles = this.m_ArrowMesh.transform.localEulerAngles;
			this.m_ArrowMesh.transform.localEulerAngles = new Vector3(localEulerAngles.x + UnityEngine.Random.Range(0f, 360f), localEulerAngles.y, localEulerAngles.z);
			this.m_ArrowRoot.transform.localEulerAngles = new Vector3(UnityEngine.Random.Range(0f, 20f), UnityEngine.Random.Range(160f, 180f), 0f);
		}
		this.ArrowAnimation();
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x00041914 File Offset: 0x0003FB14
	public void ArrowAnimation()
	{
		this.m_Trail.SetActive(true);
		this.m_Trail.GetComponent<Renderer>().GetMaterial().SetColor("_Color", new Color(0.15f, 0.15f, 0.15f, 0.15f));
		iTween.ColorTo(this.m_Trail, iTween.Hash(new object[]
		{
			"color",
			Color.clear,
			"time",
			0.1f,
			"oncomplete",
			"OnAnimationComplete"
		}));
		Vector3 localPosition = this.m_ArrowRoot.transform.localPosition;
		iTween.MoveFrom(this.m_ArrowRoot, iTween.Hash(new object[]
		{
			"position",
			new Vector3(localPosition.x, localPosition.y, localPosition.z + 0.4f),
			"islocal",
			true,
			"time",
			0.05f,
			"easetype",
			iTween.EaseType.easeOutQuart
		}));
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x00041A3B File Offset: 0x0003FC3B
	public void OnAnimationComplete()
	{
		this.m_Trail.SetActive(false);
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x00041A49 File Offset: 0x0003FC49
	public void Bullseye()
	{
		this.m_BullseyeParticles.Play();
	}

	// Token: 0x04000736 RID: 1846
	public GameObject m_ArrowRoot;

	// Token: 0x04000737 RID: 1847
	public GameObject m_ArrowMesh;

	// Token: 0x04000738 RID: 1848
	public GameObject m_Trail;

	// Token: 0x04000739 RID: 1849
	public ParticleSystem m_BullseyeParticles;
}
