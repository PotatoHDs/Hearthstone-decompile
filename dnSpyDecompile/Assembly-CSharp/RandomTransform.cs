using System;
using UnityEngine;

// Token: 0x02000A6C RID: 2668
public class RandomTransform : MonoBehaviour
{
	// Token: 0x06008F16 RID: 36630 RVA: 0x002E3D6E File Offset: 0x002E1F6E
	public void Start()
	{
		if (this.m_applyOnStart)
		{
			this.Apply();
		}
	}

	// Token: 0x06008F17 RID: 36631 RVA: 0x002E3D80 File Offset: 0x002E1F80
	public void Apply()
	{
		Vector3 b = new Vector3(UnityEngine.Random.Range(this.positionMin.x, this.positionMax.x), UnityEngine.Random.Range(this.positionMin.y, this.positionMax.y), UnityEngine.Random.Range(this.positionMin.z, this.positionMax.z));
		Vector3 localPosition = base.transform.localPosition + b;
		base.transform.localPosition = localPosition;
		Vector3 b2 = new Vector3(UnityEngine.Random.Range(this.rotationMin.x, this.rotationMax.x), UnityEngine.Random.Range(this.rotationMin.y, this.rotationMax.y), UnityEngine.Random.Range(this.rotationMin.z, this.rotationMax.z));
		Vector3 localEulerAngles = base.transform.localEulerAngles + b2;
		base.transform.localEulerAngles = localEulerAngles;
		Vector3 vector = new Vector3(UnityEngine.Random.Range(this.scaleMin.x, this.scaleMax.x), UnityEngine.Random.Range(this.scaleMin.y, this.scaleMax.y), UnityEngine.Random.Range(this.scaleMin.z, this.scaleMax.z));
		Vector3 localScale = vector;
		vector.Scale(base.transform.localScale);
		base.transform.localScale = localScale;
	}

	// Token: 0x04007797 RID: 30615
	public bool m_applyOnStart;

	// Token: 0x04007798 RID: 30616
	public Vector3 positionMin;

	// Token: 0x04007799 RID: 30617
	public Vector3 positionMax;

	// Token: 0x0400779A RID: 30618
	public Vector3 rotationMin;

	// Token: 0x0400779B RID: 30619
	public Vector3 rotationMax;

	// Token: 0x0400779C RID: 30620
	public Vector3 scaleMin = Vector3.one;

	// Token: 0x0400779D RID: 30621
	public Vector3 scaleMax = Vector3.one;
}
