using System;
using UnityEngine;

// Token: 0x0200070E RID: 1806
public class ModularBundleDustJar : MonoBehaviour
{
	// Token: 0x060064E4 RID: 25828 RVA: 0x0020EBA4 File Offset: 0x0020CDA4
	public void KeepHeaderTextStraight()
	{
		Quaternion localRotation = base.transform.parent.localRotation;
		this.HeaderText.transform.localRotation = Quaternion.Euler(90f, 360f - localRotation.eulerAngles.y, 0f);
	}

	// Token: 0x040053D0 RID: 21456
	public ModularBundleText HeaderText;

	// Token: 0x040053D1 RID: 21457
	public UberText AmountText;
}
