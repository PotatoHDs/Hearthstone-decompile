using System;
using System.Diagnostics;

// Token: 0x0200099B RID: 2459
public class CPUTimeSoftYield
{
	// Token: 0x06008662 RID: 34402 RVA: 0x002B68F0 File Offset: 0x002B4AF0
	public CPUTimeSoftYield(float maxInterval)
	{
		this.maxInterval = maxInterval;
		this.stopwatch = new Stopwatch();
		this.stopwatch.Start();
	}

	// Token: 0x06008663 RID: 34403 RVA: 0x002B6915 File Offset: 0x002B4B15
	public void NewFrame()
	{
		this.stopwatch.Stop();
		this.stopwatch.Reset();
		this.stopwatch.Start();
	}

	// Token: 0x06008664 RID: 34404 RVA: 0x002B6938 File Offset: 0x002B4B38
	public bool ShouldSoftYield()
	{
		return (float)this.stopwatch.ElapsedMilliseconds / 1000f > this.maxInterval;
	}

	// Token: 0x040071F1 RID: 29169
	private float maxInterval;

	// Token: 0x040071F2 RID: 29170
	private Stopwatch stopwatch;
}
