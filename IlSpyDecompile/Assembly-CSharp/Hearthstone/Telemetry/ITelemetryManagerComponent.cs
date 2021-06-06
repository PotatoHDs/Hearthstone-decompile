namespace Hearthstone.Telemetry
{
	public interface ITelemetryManagerComponent
	{
		void Initialize();

		void Update();

		void Shutdown();
	}
}
