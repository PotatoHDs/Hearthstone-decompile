using System;
using UnityEngine;

// Token: 0x02000A06 RID: 2566
public class ActivateToggle2 : MonoBehaviour
{
	// Token: 0x06008B0D RID: 35597 RVA: 0x002C78FC File Offset: 0x002C5AFC
	private void Start()
	{
		if (this.obj != null)
		{
			this.onoff = this.obj.activeSelf;
		}
	}

	// Token: 0x06008B0E RID: 35598 RVA: 0x002C791D File Offset: 0x002C5B1D
	public void ToggleActive2()
	{
		this.onoff = !this.onoff;
		if (this.obj != null)
		{
			this.obj.SetActive(this.onoff);
		}
	}

	// Token: 0x06008B0F RID: 35599 RVA: 0x002C794D File Offset: 0x002C5B4D
	public void ToggleOn2()
	{
		this.onoff = true;
		if (this.obj != null)
		{
			this.obj.SetActive(this.onoff);
		}
	}

	// Token: 0x06008B10 RID: 35600 RVA: 0x002C7975 File Offset: 0x002C5B75
	public void ToggleOff2()
	{
		this.onoff = false;
		if (this.obj != null)
		{
			this.obj.SetActive(this.onoff);
		}
	}

	// Token: 0x04007396 RID: 29590
	public GameObject obj;

	// Token: 0x04007397 RID: 29591
	private bool onoff;
}
