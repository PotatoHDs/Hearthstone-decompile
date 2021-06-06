using System;
using UnityEngine;

// Token: 0x02000B3C RID: 2876
public class ShowoffRotate : MonoBehaviour
{
	// Token: 0x06009882 RID: 39042 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x06009883 RID: 39043 RVA: 0x00315F25 File Offset: 0x00314125
	private void Update()
	{
		base.transform.Rotate(0f, this.Speed, 0f);
	}

	// Token: 0x04007F80 RID: 32640
	public float Speed = 1f;
}
