using System;
using UnityEngine;

// Token: 0x02000A8C RID: 2700
public class SetVisible : MonoBehaviour
{
	// Token: 0x0600907E RID: 36990 RVA: 0x002EE127 File Offset: 0x002EC327
	private void Start()
	{
		if (this.obj == null)
		{
			return;
		}
		this.renderers = this.obj.GetComponentsInChildren<MeshRenderer>();
		this.skinRenderers = this.obj.GetComponentsInChildren<SkinnedMeshRenderer>();
	}

	// Token: 0x0600907F RID: 36991 RVA: 0x002EE15A File Offset: 0x002EC35A
	public void SetOn()
	{
		this.onoff = true;
		this.SetRenderers(this.onoff);
	}

	// Token: 0x06009080 RID: 36992 RVA: 0x002EE16F File Offset: 0x002EC36F
	public void SetOff()
	{
		this.onoff = false;
		this.SetRenderers(this.onoff);
	}

	// Token: 0x06009081 RID: 36993 RVA: 0x002EE184 File Offset: 0x002EC384
	private void SetRenderers(bool value)
	{
		if (this.obj == null || this.renderers == null || this.renderers.Length == 0)
		{
			return;
		}
		MeshRenderer[] array = this.renderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = value;
		}
		SkinnedMeshRenderer[] array2 = this.skinRenderers;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].enabled = value;
		}
	}

	// Token: 0x0400794D RID: 31053
	public GameObject obj;

	// Token: 0x0400794E RID: 31054
	private MeshRenderer[] renderers;

	// Token: 0x0400794F RID: 31055
	private SkinnedMeshRenderer[] skinRenderers;

	// Token: 0x04007950 RID: 31056
	private bool onoff;
}
