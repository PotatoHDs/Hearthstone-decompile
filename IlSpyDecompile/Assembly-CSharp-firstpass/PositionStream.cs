using System;
using System.IO;

public class PositionStream : Stream
{
	private Stream stream;

	public int BytesRead { get; private set; }

	public override bool CanRead => true;

	public override bool CanSeek => false;

	public override bool CanWrite => false;

	public override long Length => stream.Length;

	public override long Position
	{
		get
		{
			return BytesRead;
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public PositionStream(Stream baseStream)
	{
		stream = baseStream;
	}

	public override void Flush()
	{
		throw new NotImplementedException();
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		int num = stream.Read(buffer, offset, count);
		BytesRead += num;
		return num;
	}

	public override int ReadByte()
	{
		int result = stream.ReadByte();
		BytesRead++;
		return result;
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		throw new NotImplementedException();
	}

	public override void SetLength(long value)
	{
		throw new NotImplementedException();
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		throw new NotImplementedException();
	}

	public override void Close()
	{
		base.Close();
	}

	protected override void Dispose(bool disposing)
	{
		stream.Dispose();
		base.Dispose(disposing);
	}
}
