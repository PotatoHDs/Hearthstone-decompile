using UnityEngine;

public class UnityTimeProvider : ITimeProvider
{
	public float TimeSinceStartup => Time.realtimeSinceStartup;

	public float UnscaledDeltaTime => Time.unscaledDeltaTime;
}
