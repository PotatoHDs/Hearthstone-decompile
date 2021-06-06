using System;
using UnityEngine;

// Token: 0x02000A88 RID: 2696
public class SendPMEvent : MonoBehaviour
{
	// Token: 0x06009070 RID: 36976 RVA: 0x002EDE5E File Offset: 0x002EC05E
	public void SendEvent()
	{
		this.fsm.SendEvent(this.eventName);
	}

	// Token: 0x04007943 RID: 31043
	public string eventName;

	// Token: 0x04007944 RID: 31044
	public PlayMakerFSM fsm;
}
