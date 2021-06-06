public class FatalErrorMessage
{
	public string m_id;

	public string m_text;

	public Error.AcknowledgeCallback m_ackCallback;

	public object m_ackUserData;

	public bool m_allowClick = true;

	public bool m_redirectToStore;

	public float m_delayBeforeNextReset;

	public FatalErrorReason m_reason;
}
