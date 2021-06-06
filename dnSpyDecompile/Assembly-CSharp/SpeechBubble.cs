using System;
using UnityEngine;

// Token: 0x02000B36 RID: 2870
public class SpeechBubble : MonoBehaviour
{
	// Token: 0x0600986F RID: 39023 RVA: 0x00315BD6 File Offset: 0x00313DD6
	private void Awake()
	{
		this.rotation = base.transform.rotation;
	}

	// Token: 0x06009870 RID: 39024 RVA: 0x00315BE9 File Offset: 0x00313DE9
	private void LateUpdate()
	{
		base.transform.rotation = this.rotation;
	}

	// Token: 0x04007F73 RID: 32627
	private Quaternion rotation;
}
