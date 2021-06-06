using System;
using System.IO;

// Token: 0x0200000D RID: 13
public class PositionStream : Stream
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000089 RID: 137 RVA: 0x00003D00 File Offset: 0x00001F00
	// (set) Token: 0x0600008A RID: 138 RVA: 0x00003D08 File Offset: 0x00001F08
	public int BytesRead { get; private set; }

	// Token: 0x0600008B RID: 139 RVA: 0x00003D11 File Offset: 0x00001F11
	public PositionStream(Stream baseStream)
	{
		this.stream = baseStream;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00003D20 File Offset: 0x00001F20
	public override void Flush()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00003D28 File Offset: 0x00001F28
	public override int Read(byte[] buffer, int offset, int count)
	{
		int num = this.stream.Read(buffer, offset, count);
		this.BytesRead += num;
		return num;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00003D53 File Offset: 0x00001F53
	public override int ReadByte()
	{
		int result = this.stream.ReadByte();
		this.BytesRead++;
		return result;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00003D20 File Offset: 0x00001F20
	public override long Seek(long offset, SeekOrigin origin)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00003D20 File Offset: 0x00001F20
	public override void SetLength(long value)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00003D20 File Offset: 0x00001F20
	public override void Write(byte[] buffer, int offset, int count)
	{
		throw new NotImplementedException();
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000092 RID: 146 RVA: 0x00003D6E File Offset: 0x00001F6E
	public override bool CanRead
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000093 RID: 147 RVA: 0x00003D71 File Offset: 0x00001F71
	public override bool CanSeek
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000094 RID: 148 RVA: 0x00003D71 File Offset: 0x00001F71
	public override bool CanWrite
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000095 RID: 149 RVA: 0x00003D74 File Offset: 0x00001F74
	public override long Length
	{
		get
		{
			return this.stream.Length;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000096 RID: 150 RVA: 0x00003D81 File Offset: 0x00001F81
	// (set) Token: 0x06000097 RID: 151 RVA: 0x00003D20 File Offset: 0x00001F20
	public override long Position
	{
		get
		{
			return (long)this.BytesRead;
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00003D8A File Offset: 0x00001F8A
	public override void Close()
	{
		base.Close();
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00003D92 File Offset: 0x00001F92
	protected override void Dispose(bool disposing)
	{
		this.stream.Dispose();
		base.Dispose(disposing);
	}

	// Token: 0x04000021 RID: 33
	private Stream stream;
}
