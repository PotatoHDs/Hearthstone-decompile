using System;
using UnityEngine;

// Token: 0x02000935 RID: 2357
public class Tooltip : MonoBehaviour
{
	// Token: 0x06008229 RID: 33321 RVA: 0x002A50C8 File Offset: 0x002A32C8
	public void UpdateText(string headline, string description)
	{
		this.headlineText.text = headline;
		this.descriptionText.text = description;
	}

	// Token: 0x04006D17 RID: 27927
	public TextMesh headlineText;

	// Token: 0x04006D18 RID: 27928
	public TextMesh descriptionText;
}
