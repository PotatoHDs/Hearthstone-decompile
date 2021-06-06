namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Starts location service updates. Last location coordinates can be retrieved with GetLocationInfo.")]
	public class StartLocationServiceUpdates : FsmStateAction
	{
		[Tooltip("Maximum time to wait in seconds before failing.")]
		public FsmFloat maxWait;

		public FsmFloat desiredAccuracy;

		public FsmFloat updateDistance;

		[Tooltip("Event to send when the location services have started.")]
		public FsmEvent successEvent;

		[Tooltip("Event to send if the location services fail to start.")]
		public FsmEvent failedEvent;

		public override void Reset()
		{
			maxWait = 20f;
			desiredAccuracy = 10f;
			updateDistance = 10f;
			successEvent = null;
			failedEvent = null;
		}

		public override void OnEnter()
		{
			Finish();
		}

		public override void OnUpdate()
		{
		}
	}
}
