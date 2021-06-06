using System.Diagnostics;

public class CPUTimeSoftYield
{
	private float maxInterval;

	private Stopwatch stopwatch;

	public CPUTimeSoftYield(float maxInterval)
	{
		this.maxInterval = maxInterval;
		stopwatch = new Stopwatch();
		stopwatch.Start();
	}

	public void NewFrame()
	{
		stopwatch.Stop();
		stopwatch.Reset();
		stopwatch.Start();
	}

	public bool ShouldSoftYield()
	{
		return (float)stopwatch.ElapsedMilliseconds / 1000f > maxInterval;
	}
}
