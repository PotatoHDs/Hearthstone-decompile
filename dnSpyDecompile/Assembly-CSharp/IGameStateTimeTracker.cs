using System;

// Token: 0x0200031D RID: 797
public interface IGameStateTimeTracker
{
	// Token: 0x06002CE5 RID: 11493
	void Update();

	// Token: 0x06002CE6 RID: 11494
	void AdjustAccruedLostTime(float deltaSeconds);

	// Token: 0x06002CE7 RID: 11495
	void ResetAccruedLostTime();

	// Token: 0x06002CE8 RID: 11496
	float GetAccruedLostTimeInSeconds();
}
