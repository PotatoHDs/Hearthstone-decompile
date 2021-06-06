public interface IGameStateTimeTracker
{
	void Update();

	void AdjustAccruedLostTime(float deltaSeconds);

	void ResetAccruedLostTime();

	float GetAccruedLostTimeInSeconds();
}
