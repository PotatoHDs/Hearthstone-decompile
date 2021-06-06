using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005E3 RID: 1507
public class BoundsOverride : MonoBehaviour
{
	// Token: 0x17000513 RID: 1299
	// (get) Token: 0x0600528A RID: 21130 RVA: 0x001B15DC File Offset: 0x001AF7DC
	public Bounds bounds
	{
		get
		{
			int bestScreenMatch = PlatformSettings.GetBestScreenMatch(this.m_screenCategory);
			return this.m_bounds[bestScreenMatch];
		}
	}

	// Token: 0x0600528B RID: 21131 RVA: 0x001B1601 File Offset: 0x001AF801
	public void AddCategory()
	{
		this.AddCategory(PlatformSettings.Screen);
	}

	// Token: 0x0600528C RID: 21132 RVA: 0x001B1610 File Offset: 0x001AF810
	public void AddCategory(ScreenCategory screen)
	{
		if (!Application.IsPlaying(this))
		{
			this.m_screenCategory.Add(screen);
			this.m_bounds.Add(default(Bounds));
		}
	}

	// Token: 0x040049A7 RID: 18855
	public List<ScreenCategory> m_screenCategory = new List<ScreenCategory>();

	// Token: 0x040049A8 RID: 18856
	public List<Bounds> m_bounds = new List<Bounds>();
}
