using System;
using System.IO;
using bgs;

public class PegasusPacket : PacketFormat
{
	private const int TYPE_BYTES = 4;

	private const int SIZE_BYTES = 4;

	public int Size;

	public int Type;

	public int Context;

	public object Body;

	private bool sizeRead;

	private bool typeRead;

	public object GetBody()
	{
		return Body;
	}

	public PegasusPacket()
	{
	}

	public PegasusPacket(int type, int context, object body)
	{
		Type = type;
		Context = context;
		Size = -1;
		Body = body;
	}

	public PegasusPacket(int type, int context, int size, object body)
	{
		Type = type;
		Context = context;
		Size = size;
		Body = body;
	}

	public override bool IsLoaded()
	{
		return Body != null;
	}

	public override int Decode(byte[] bytes, int offset, int available)
	{
		string text = "";
		for (int i = 0; i < 8 && i < available; i++)
		{
			text = text + bytes[offset + i] + " ";
		}
		int num = 0;
		if (!typeRead)
		{
			if (available < 4)
			{
				return num;
			}
			Type = BitConverter.ToInt32(bytes, offset);
			typeRead = true;
			available -= 4;
			num += 4;
			offset += 4;
		}
		if (!sizeRead)
		{
			if (available < 4)
			{
				return num;
			}
			Size = BitConverter.ToInt32(bytes, offset);
			sizeRead = true;
			available -= 4;
			num += 4;
			offset += 4;
		}
		if (Body == null)
		{
			if (available < Size)
			{
				return num;
			}
			byte[] array = new byte[Size];
			Array.Copy(bytes, offset, array, 0, Size);
			Body = array;
			num += Size;
		}
		return num;
	}

	public override byte[] Encode()
	{
		if (Body is IProtoBuf)
		{
			IProtoBuf protoBuf = (IProtoBuf)Body;
			Size = (int)protoBuf.GetSerializedSize();
			byte[] array = new byte[Size + 4 + 4];
			Array.Copy(BitConverter.GetBytes(Type), 0, array, 0, 4);
			Array.Copy(BitConverter.GetBytes(Size), 0, array, 4, 4);
			protoBuf.Serialize(new MemoryStream(array, 8, Size));
			return array;
		}
		return null;
	}

	public override string ToString()
	{
		return "PegasusPacket Type: " + Type;
	}

	public override bool IsFatalOnError()
	{
		return Type == 168;
	}
}
