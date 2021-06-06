using System;
using UnityEngine;

// Token: 0x02000A05 RID: 2565
public class ActivateToggle : MonoBehaviour
{
	// Token: 0x06008B08 RID: 35592 RVA: 0x002C785B File Offset: 0x002C5A5B
	private void Start()
	{
		if (this.obj != null)
		{
			this.onoff = this.obj.activeSelf;
		}
	}

	// Token: 0x06008B09 RID: 35593 RVA: 0x002C787C File Offset: 0x002C5A7C
	public void ToggleActive()
	{
		this.onoff = !this.onoff;
		if (this.obj != null)
		{
			this.obj.SetActive(this.onoff);
		}
	}

	// Token: 0x06008B0A RID: 35594 RVA: 0x002C78AC File Offset: 0x002C5AAC
	public void ToggleOn()
	{
		this.onoff = true;
		if (this.obj != null)
		{
			this.obj.SetActive(this.onoff);
		}
	}

	// Token: 0x06008B0B RID: 35595 RVA: 0x002C78D4 File Offset: 0x002C5AD4
	public void ToggleOff()
	{
		this.onoff = false;
		if (this.obj != null)
		{
			this.obj.SetActive(this.onoff);
		}
	}

	// Token: 0x04007394 RID: 29588
	public GameObject obj;

	// Token: 0x04007395 RID: 29589
	private bool onoff;
}
