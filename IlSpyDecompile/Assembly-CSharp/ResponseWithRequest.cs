public class ResponseWithRequest
{
	public PegasusPacket Response;

	public PegasusPacket Request;

	public ResponseWithRequest(PegasusPacket response)
	{
		Response = response;
		Request = null;
	}

	public ResponseWithRequest(PegasusPacket response, PegasusPacket request)
	{
		Response = response;
		Request = request;
	}
}
