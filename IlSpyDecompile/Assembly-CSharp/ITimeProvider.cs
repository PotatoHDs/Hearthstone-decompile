public interface ITimeProvider
{
	float TimeSinceStartup { get; }

	float UnscaledDeltaTime { get; }
}
