namespace HutongGames.PlayMaker.Actions
{
	public abstract class BaseLogAction : FsmStateAction
	{
		public bool sendToUnityLog;

		public override void Reset()
		{
			sendToUnityLog = false;
		}
	}
}
