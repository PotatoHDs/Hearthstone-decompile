using System;

// Token: 0x020002DC RID: 732
public class FatalErrorMessage
{
	// Token: 0x040015BF RID: 5567
	public string m_id;

	// Token: 0x040015C0 RID: 5568
	public string m_text;

	// Token: 0x040015C1 RID: 5569
	public Error.AcknowledgeCallback m_ackCallback;

	// Token: 0x040015C2 RID: 5570
	public object m_ackUserData;

	// Token: 0x040015C3 RID: 5571
	public bool m_allowClick = true;

	// Token: 0x040015C4 RID: 5572
	public bool m_redirectToStore;

	// Token: 0x040015C5 RID: 5573
	public float m_delayBeforeNextReset;

	// Token: 0x040015C6 RID: 5574
	public FatalErrorReason m_reason;
}
