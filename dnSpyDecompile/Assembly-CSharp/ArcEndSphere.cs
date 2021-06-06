using System;
using UnityEngine;

// Token: 0x02000756 RID: 1878
public class ArcEndSphere : MonoBehaviour
{
	// Token: 0x060069C0 RID: 27072 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x060069C1 RID: 27073 RVA: 0x0022711C File Offset: 0x0022531C
	private void Update()
	{
		Material material = base.GetComponent<Renderer>().GetMaterial();
		Vector2 mainTextureOffset = material.mainTextureOffset;
		mainTextureOffset.x += Time.deltaTime * 1f;
		material.mainTextureOffset = mainTextureOffset;
	}
}
