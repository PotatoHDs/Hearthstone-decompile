public class ErrorParams
{
	public ErrorType m_type;

	public string m_header;

	public string m_message;

	public Error.AcknowledgeCallback m_ackCallback;

	public object m_ackUserData;

	public bool m_allowClick = true;

	public bool m_redirectToStore;

	public float m_delayBeforeNextReset;

	public FatalErrorReason m_reason;
}
