using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class PlayAnimations : MonoBehaviour
{
	// Token: 0x06000092 RID: 146 RVA: 0x00003AD2 File Offset: 0x00001CD2
	public void Update()
	{
		base.GetComponent<Animation>().PlayQueued("CoinDropA");
		base.GetComponent<Animation>().PlayQueued("CoinDropB");
	}
}
