using System;
using System.IO;
using bgs;

// Token: 0x02000601 RID: 1537
public class PegasusPacket : PacketFormat
{
	// Token: 0x06005400 RID: 21504 RVA: 0x001B739A File Offset: 0x001B559A
	public object GetBody()
	{
		return this.Body;
	}

	// Token: 0x06005401 RID: 21505 RVA: 0x001B73A2 File Offset: 0x001B55A2
	public PegasusPacket()
	{
	}

	// Token: 0x06005402 RID: 21506 RVA: 0x001B73AA File Offset: 0x001B55AA
	public PegasusPacket(int type, int context, object body)
	{
		this.Type = type;
		this.Context = context;
		this.Size = -1;
		this.Body = body;
	}

	// Token: 0x06005403 RID: 21507 RVA: 0x001B73CE File Offset: 0x001B55CE
	public PegasusPacket(int type, int context, int size, object body)
	{
		this.Type = type;
		this.Context = context;
		this.Size = size;
		this.Body = body;
	}

	// Token: 0x06005404 RID: 21508 RVA: 0x001B73F3 File Offset: 0x001B55F3
	public override bool IsLoaded()
	{
		return this.Body != null;
	}

	// Token: 0x06005405 RID: 21509 RVA: 0x001B7400 File Offset: 0x001B5600
	public override int Decode(byte[] bytes, int offset, int available)
	{
		string arg = "";
		int num = 0;
		while (num < 8 && num < available)
		{
			arg = arg + bytes[offset + num] + " ";
			num++;
		}
		int num2 = 0;
		if (!this.typeRead)
		{
			if (available < 4)
			{
				return num2;
			}
			this.Type = BitConverter.ToInt32(bytes, offset);
			this.typeRead = true;
			available -= 4;
			num2 += 4;
			offset += 4;
		}
		if (!this.sizeRead)
		{
			if (available < 4)
			{
				return num2;
			}
			this.Size = BitConverter.ToInt32(bytes, offset);
			this.sizeRead = true;
			available -= 4;
			num2 += 4;
			offset += 4;
		}
		if (this.Body == null)
		{
			if (available < this.Size)
			{
				return num2;
			}
			byte[] array = new byte[this.Size];
			Array.Copy(bytes, offset, array, 0, this.Size);
			this.Body = array;
			num2 += this.Size;
		}
		return num2;
	}

	// Token: 0x06005406 RID: 21510 RVA: 0x001B74DC File Offset: 0x001B56DC
	public override byte[] Encode()
	{
		if (this.Body is IProtoBuf)
		{
			IProtoBuf protoBuf = (IProtoBuf)this.Body;
			this.Size = (int)protoBuf.GetSerializedSize();
			byte[] array = new byte[this.Size + 4 + 4];
			Array.Copy(BitConverter.GetBytes(this.Type), 0, array, 0, 4);
			Array.Copy(BitConverter.GetBytes(this.Size), 0, array, 4, 4);
			protoBuf.Serialize(new MemoryStream(array, 8, this.Size));
			return array;
		}
		return null;
	}

	// Token: 0x06005407 RID: 21511 RVA: 0x001B755C File Offset: 0x001B575C
	public override string ToString()
	{
		return "PegasusPacket Type: " + this.Type;
	}

	// Token: 0x06005408 RID: 21512 RVA: 0x001B7573 File Offset: 0x001B5773
	public override bool IsFatalOnError()
	{
		return this.Type == 168;
	}

	// Token: 0x04004A3C RID: 19004
	private const int TYPE_BYTES = 4;

	// Token: 0x04004A3D RID: 19005
	private const int SIZE_BYTES = 4;

	// Token: 0x04004A3E RID: 19006
	public int Size;

	// Token: 0x04004A3F RID: 19007
	public int Type;

	// Token: 0x04004A40 RID: 19008
	public int Context;

	// Token: 0x04004A41 RID: 19009
	public object Body;

	// Token: 0x04004A42 RID: 19010
	private bool sizeRead;

	// Token: 0x04004A43 RID: 19011
	private bool typeRead;
}
