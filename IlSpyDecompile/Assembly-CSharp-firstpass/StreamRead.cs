using System;
using System.IO;

[Obsolete("Renamed to PositionStream")]
public class StreamRead : PositionStream
{
	public StreamRead(Stream baseStream)
		: base(baseStream)
	{
	}
}
