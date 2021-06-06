using System;
using System.IO;

// Token: 0x0200000C RID: 12
[Obsolete("Renamed to PositionStream")]
public class StreamRead : PositionStream
{
	// Token: 0x06000088 RID: 136 RVA: 0x00003CF7 File Offset: 0x00001EF7
	public StreamRead(Stream baseStream) : base(baseStream)
	{
	}
}
