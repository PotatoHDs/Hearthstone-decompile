using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005FA RID: 1530
[ExecuteAlways]
public class TransformOverride : MonoBehaviour
{
	// Token: 0x06005345 RID: 21317 RVA: 0x001B33BC File Offset: 0x001B15BC
	public void Awake()
	{
		if (Application.IsPlaying(this))
		{
			this.UpdateObject();
		}
	}

	// Token: 0x06005346 RID: 21318 RVA: 0x001B33CC File Offset: 0x001B15CC
	public void AddCategory(ScreenCategory screen)
	{
		if (!Application.IsPlaying(this))
		{
			this.m_screenCategory.Add(screen);
			this.m_localPosition.Add(base.transform.localPosition);
			this.m_localScale.Add(base.transform.localScale);
			this.m_localRotation.Add(base.transform.localRotation);
		}
	}

	// Token: 0x06005347 RID: 21319 RVA: 0x001B342F File Offset: 0x001B162F
	public void AddCategory()
	{
		this.AddCategory(PlatformSettings.Screen);
	}

	// Token: 0x06005348 RID: 21320 RVA: 0x001B343C File Offset: 0x001B163C
	public void UpdateObject()
	{
		int bestScreenMatch = PlatformSettings.GetBestScreenMatch(this.m_screenCategory);
		base.transform.localPosition = this.m_localPosition[bestScreenMatch];
		base.transform.localScale = this.m_localScale[bestScreenMatch];
		base.transform.localRotation = this.m_localRotation[bestScreenMatch];
	}

	// Token: 0x040049EB RID: 18923
	public List<ScreenCategory> m_screenCategory = new List<ScreenCategory>();

	// Token: 0x040049EC RID: 18924
	public List<Vector3> m_localPosition = new List<Vector3>();

	// Token: 0x040049ED RID: 18925
	public List<Vector3> m_localScale = new List<Vector3>();

	// Token: 0x040049EE RID: 18926
	public List<Quaternion> m_localRotation = new List<Quaternion>();

	// Token: 0x040049EF RID: 18927
	public float testVal;
}
