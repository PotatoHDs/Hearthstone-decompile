using UnityEngine;

public class SendPMEvent : MonoBehaviour
{
	public string eventName;

	public PlayMakerFSM fsm;

	public void SendEvent()
	{
		fsm.SendEvent(eventName);
	}
}
