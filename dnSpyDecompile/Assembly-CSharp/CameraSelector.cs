using System;
using UnityEngine;

// Token: 0x02000758 RID: 1880
public class CameraSelector : MonoBehaviour
{
	// Token: 0x060069C6 RID: 27078 RVA: 0x00227164 File Offset: 0x00225364
	private void Start()
	{
		if (this.activateOnStart)
		{
			Camera.main.transform.rotation = Quaternion.Euler(this.cameraRotation);
			Camera.main.transform.position = this.cameraPosition;
		}
	}

	// Token: 0x060069C7 RID: 27079 RVA: 0x0022719D File Offset: 0x0022539D
	private void OnMouseDown()
	{
		Camera.main.transform.rotation = Quaternion.Euler(this.cameraRotation);
		Camera.main.transform.position = this.cameraPosition;
	}

	// Token: 0x04005685 RID: 22149
	public Vector3 cameraPosition;

	// Token: 0x04005686 RID: 22150
	public Vector3 cameraRotation;

	// Token: 0x04005687 RID: 22151
	public bool activateOnStart;
}
