using System;

// Token: 0x020002D8 RID: 728
public class ErrorParams
{
	// Token: 0x04001595 RID: 5525
	public ErrorType m_type;

	// Token: 0x04001596 RID: 5526
	public string m_header;

	// Token: 0x04001597 RID: 5527
	public string m_message;

	// Token: 0x04001598 RID: 5528
	public Error.AcknowledgeCallback m_ackCallback;

	// Token: 0x04001599 RID: 5529
	public object m_ackUserData;

	// Token: 0x0400159A RID: 5530
	public bool m_allowClick = true;

	// Token: 0x0400159B RID: 5531
	public bool m_redirectToStore;

	// Token: 0x0400159C RID: 5532
	public float m_delayBeforeNextReset;

	// Token: 0x0400159D RID: 5533
	public FatalErrorReason m_reason;
}
