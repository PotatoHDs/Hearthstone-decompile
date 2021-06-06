using System;

// Token: 0x02000602 RID: 1538
public class ResponseWithRequest
{
	// Token: 0x06005409 RID: 21513 RVA: 0x001B7582 File Offset: 0x001B5782
	public ResponseWithRequest(PegasusPacket response)
	{
		this.Response = response;
		this.Request = null;
	}

	// Token: 0x0600540A RID: 21514 RVA: 0x001B7598 File Offset: 0x001B5798
	public ResponseWithRequest(PegasusPacket response, PegasusPacket request)
	{
		this.Response = response;
		this.Request = request;
	}

	// Token: 0x04004A44 RID: 19012
	public PegasusPacket Response;

	// Token: 0x04004A45 RID: 19013
	public PegasusPacket Request;
}
