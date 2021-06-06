using System;
using UnityEngine;

// Token: 0x02000A20 RID: 2592
public class Disable_LowQuality : MonoBehaviour
{
	// Token: 0x06008BC0 RID: 35776 RVA: 0x002CBDF1 File Offset: 0x002C9FF1
	private void Awake()
	{
		GraphicsManager.Get().RegisterLowQualityDisableObject(base.gameObject);
		if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06008BC1 RID: 35777 RVA: 0x002CBE1C File Offset: 0x002CA01C
	private void OnDestroy()
	{
		GraphicsManager graphicsManager = GraphicsManager.Get();
		if (graphicsManager != null)
		{
			graphicsManager.DeregisterLowQualityDisableObject(base.gameObject);
		}
	}
}
