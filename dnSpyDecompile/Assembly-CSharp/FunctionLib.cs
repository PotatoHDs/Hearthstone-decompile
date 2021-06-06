using System;
using UnityEngine;

// Token: 0x0200075A RID: 1882
public class FunctionLib : MonoBehaviour
{
	// Token: 0x060069CF RID: 27087 RVA: 0x00228150 File Offset: 0x00226350
	private void onAnimaitonEvent()
	{
		this.lightningScript.Spawn(this.target.transform, this.destination.transform);
	}

	// Token: 0x04005694 RID: 22164
	public LightningCtrl lightningScript;

	// Token: 0x04005695 RID: 22165
	public GameObject target;

	// Token: 0x04005696 RID: 22166
	public GameObject destination;
}
