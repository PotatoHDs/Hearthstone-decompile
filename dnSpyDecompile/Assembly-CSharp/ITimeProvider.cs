using System;

// Token: 0x0200099D RID: 2461
public interface ITimeProvider
{
	// Token: 0x17000785 RID: 1925
	// (get) Token: 0x0600866F RID: 34415
	float TimeSinceStartup { get; }

	// Token: 0x17000786 RID: 1926
	// (get) Token: 0x06008670 RID: 34416
	float UnscaledDeltaTime { get; }
}
