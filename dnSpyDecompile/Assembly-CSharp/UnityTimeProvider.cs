using System;
using UnityEngine;

// Token: 0x0200099E RID: 2462
public class UnityTimeProvider : ITimeProvider
{
	// Token: 0x17000787 RID: 1927
	// (get) Token: 0x06008671 RID: 34417 RVA: 0x002B6A8D File Offset: 0x002B4C8D
	public float TimeSinceStartup
	{
		get
		{
			return Time.realtimeSinceStartup;
		}
	}

	// Token: 0x17000788 RID: 1928
	// (get) Token: 0x06008672 RID: 34418 RVA: 0x002B6A94 File Offset: 0x002B4C94
	public float UnscaledDeltaTime
	{
		get
		{
			return Time.unscaledDeltaTime;
		}
	}
}
