using System;
using UnityEngine;

// Token: 0x020006DE RID: 1758
public class Glow : MonoBehaviour
{
	// Token: 0x06006224 RID: 25124 RVA: 0x002006CF File Offset: 0x001FE8CF
	public void UpdateAlpha(float alpha)
	{
		base.GetComponent<Renderer>().GetMaterial().SetColor("_TintColor", new Color(1f, 1f, 1f, alpha));
	}
}
