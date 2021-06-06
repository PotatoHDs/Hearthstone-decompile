using System;
using System.IO;
using System.Text;

// Token: 0x0200000B RID: 11
public static class ProtocolParser
{
	// Token: 0x0600005B RID: 91 RVA: 0x000036A8 File Offset: 0x000018A8
	public static string ReadString(Stream stream)
	{
		return Encoding.UTF8.GetString(ProtocolParser.ReadBytes(stream));
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000036BC File Offset: 0x000018BC
	public static byte[] ReadBytes(Stream stream)
	{
		int num = (int)ProtocolParser.ReadUInt32(stream);
		byte[] array = new byte[num];
		int num2;
		for (int i = 0; i < num; i += num2)
		{
			num2 = stream.Read(array, i, num - i);
			if (num2 == 0)
			{
				throw new ProtocolBufferException(string.Concat(new object[]
				{
					"Expected ",
					num - i,
					" got ",
					i
				}));
			}
		}
		return array;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00003728 File Offset: 0x00001928
	public static void SkipBytes(Stream stream)
	{
		int num = (int)ProtocolParser.ReadUInt32(stream);
		if (stream.CanSeek)
		{
			stream.Seek((long)num, SeekOrigin.Current);
			return;
		}
		ProtocolParser.ReadBytes(stream);
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00003756 File Offset: 0x00001956
	public static void WriteString(Stream stream, string val)
	{
		ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(val));
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00003769 File Offset: 0x00001969
	public static void WriteBytes(Stream stream, byte[] val)
	{
		ProtocolParser.WriteUInt32(stream, (uint)val.Length);
		stream.Write(val, 0, val.Length);
	}

	// Token: 0x06000060 RID: 96 RVA: 0x0000377F File Offset: 0x0000197F
	[Obsolete("Only for reference")]
	public static ulong ReadFixed64(BinaryReader reader)
	{
		return reader.ReadUInt64();
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00003787 File Offset: 0x00001987
	[Obsolete("Only for reference")]
	public static long ReadSFixed64(BinaryReader reader)
	{
		return reader.ReadInt64();
	}

	// Token: 0x06000062 RID: 98 RVA: 0x0000378F File Offset: 0x0000198F
	[Obsolete("Only for reference")]
	public static uint ReadFixed32(BinaryReader reader)
	{
		return reader.ReadUInt32();
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00003797 File Offset: 0x00001997
	[Obsolete("Only for reference")]
	public static int ReadSFixed32(BinaryReader reader)
	{
		return reader.ReadInt32();
	}

	// Token: 0x06000064 RID: 100 RVA: 0x0000379F File Offset: 0x0000199F
	[Obsolete("Only for reference")]
	public static void WriteFixed64(BinaryWriter writer, ulong val)
	{
		writer.Write(val);
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000037A8 File Offset: 0x000019A8
	[Obsolete("Only for reference")]
	public static void WriteSFixed64(BinaryWriter writer, long val)
	{
		writer.Write(val);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x000037B1 File Offset: 0x000019B1
	[Obsolete("Only for reference")]
	public static void WriteFixed32(BinaryWriter writer, uint val)
	{
		writer.Write(val);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000037BA File Offset: 0x000019BA
	[Obsolete("Only for reference")]
	public static void WriteSFixed32(BinaryWriter writer, int val)
	{
		writer.Write(val);
	}

	// Token: 0x06000068 RID: 104 RVA: 0x000037C3 File Offset: 0x000019C3
	[Obsolete("Only for reference")]
	public static float ReadFloat(BinaryReader reader)
	{
		return reader.ReadSingle();
	}

	// Token: 0x06000069 RID: 105 RVA: 0x000037CB File Offset: 0x000019CB
	[Obsolete("Only for reference")]
	public static double ReadDouble(BinaryReader reader)
	{
		return reader.ReadDouble();
	}

	// Token: 0x0600006A RID: 106 RVA: 0x000037D3 File Offset: 0x000019D3
	[Obsolete("Only for reference")]
	public static void WriteFloat(BinaryWriter writer, float val)
	{
		writer.Write(val);
	}

	// Token: 0x0600006B RID: 107 RVA: 0x000037DC File Offset: 0x000019DC
	[Obsolete("Only for reference")]
	public static void WriteDouble(BinaryWriter writer, double val)
	{
		writer.Write(val);
	}

	// Token: 0x0600006C RID: 108 RVA: 0x000037E8 File Offset: 0x000019E8
	public static Key ReadKey(Stream stream)
	{
		uint num = ProtocolParser.ReadUInt32(stream);
		return new Key(num >> 3, (Wire)(num & 7U));
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00003807 File Offset: 0x00001A07
	public static Key ReadKey(byte firstByte, Stream stream)
	{
		if (firstByte < 128)
		{
			return new Key((uint)(firstByte >> 3), (Wire)(firstByte & 7));
		}
		return new Key(ProtocolParser.ReadUInt32(stream) << 4 | (uint)(firstByte >> 3 & 15), (Wire)(firstByte & 7));
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00003834 File Offset: 0x00001A34
	public static void WriteKey(Stream stream, Key key)
	{
		uint val = key.Field << 3 | (uint)key.WireType;
		ProtocolParser.WriteUInt32(stream, val);
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00003858 File Offset: 0x00001A58
	public static void SkipKey(Stream stream, Key key)
	{
		switch (key.WireType)
		{
		case Wire.Varint:
			ProtocolParser.ReadSkipVarInt(stream);
			return;
		case Wire.Fixed64:
			stream.Seek(8L, SeekOrigin.Current);
			return;
		case Wire.LengthDelimited:
			stream.Seek((long)((ulong)ProtocolParser.ReadUInt32(stream)), SeekOrigin.Current);
			return;
		case Wire.Fixed32:
			stream.Seek(4L, SeekOrigin.Current);
			return;
		}
		throw new NotImplementedException("Unknown wire type: " + key.WireType);
	}

	// Token: 0x06000070 RID: 112 RVA: 0x000038D4 File Offset: 0x00001AD4
	public static byte[] ReadValueBytes(Stream stream, Key key)
	{
		int i = 0;
		switch (key.WireType)
		{
		case Wire.Varint:
			return ProtocolParser.ReadVarIntBytes(stream);
		case Wire.Fixed64:
		{
			byte[] array = new byte[8];
			while (i < 8)
			{
				i += stream.Read(array, i, 8 - i);
			}
			return array;
		}
		case Wire.LengthDelimited:
		{
			uint num = ProtocolParser.ReadUInt32(stream);
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				ProtocolParser.WriteUInt32(memoryStream, num);
				array = new byte[(ulong)num + (ulong)memoryStream.Length];
				memoryStream.ToArray().CopyTo(array, 0);
				i = (int)memoryStream.Length;
				goto IL_C2;
			}
			IL_B2:
			i += stream.Read(array, i, array.Length - i);
			IL_C2:
			if (i >= array.Length)
			{
				return array;
			}
			goto IL_B2;
		}
		case Wire.Fixed32:
		{
			byte[] array = new byte[4];
			while (i < 4)
			{
				i += stream.Read(array, i, 4 - i);
			}
			return array;
		}
		}
		throw new NotImplementedException("Unknown wire type: " + key.WireType);
	}

	// Token: 0x06000071 RID: 113 RVA: 0x000039DC File Offset: 0x00001BDC
	public static void ReadSkipVarInt(Stream stream)
	{
		for (;;)
		{
			int num = stream.ReadByte();
			if (num < 0)
			{
				break;
			}
			if ((num & 128) == 0)
			{
				return;
			}
		}
		throw new IOException("Stream ended too early");
	}

	// Token: 0x06000072 RID: 114 RVA: 0x000039FC File Offset: 0x00001BFC
	public static byte[] ReadVarIntBytes(Stream stream)
	{
		byte[] array = new byte[10];
		int num = 0;
		for (;;)
		{
			int num2 = stream.ReadByte();
			if (num2 < 0)
			{
				break;
			}
			array[num] = (byte)num2;
			num++;
			if ((num2 & 128) == 0)
			{
				goto IL_43;
			}
			if (num >= array.Length)
			{
				goto Block_3;
			}
		}
		throw new IOException("Stream ended too early");
		Block_3:
		throw new ProtocolBufferException("VarInt too long, more than 10 bytes");
		IL_43:
		byte[] array2 = new byte[num];
		Array.Copy(array, array2, array2.Length);
		return array2;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00003A5E File Offset: 0x00001C5E
	[Obsolete("Use (int)ReadUInt64(stream); //yes 64")]
	public static int ReadInt32(Stream stream)
	{
		return (int)ProtocolParser.ReadUInt64(stream);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00003A67 File Offset: 0x00001C67
	[Obsolete("Use WriteUInt64(stream, (ulong)val); //yes 64, negative numbers are encoded that way")]
	public static void WriteInt32(Stream stream, int val)
	{
		ProtocolParser.WriteUInt64(stream, (ulong)((long)val));
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00003A74 File Offset: 0x00001C74
	public static int ReadZInt32(Stream stream)
	{
		uint num = ProtocolParser.ReadUInt32(stream);
		return (int)(num >> 1 ^ (uint)((int)((int)num << 31) >> 31));
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003A93 File Offset: 0x00001C93
	public static void WriteZInt32(Stream stream, int val)
	{
		ProtocolParser.WriteUInt32(stream, (uint)(val << 1 ^ val >> 31));
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00003AA3 File Offset: 0x00001CA3
	public static uint SizeOfZInt32(int val)
	{
		return ProtocolParser.SizeOfUInt32((uint)(val << 1 ^ val >> 31));
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00003AB4 File Offset: 0x00001CB4
	public static uint ReadUInt32(Stream stream)
	{
		uint num = 0U;
		for (int i = 0; i < 5; i++)
		{
			int num2 = stream.ReadByte();
			if (num2 < 0)
			{
				throw new IOException("Stream ended too early");
			}
			if (i == 4 && (num2 & 240) != 0)
			{
				throw new ProtocolBufferException("Got larger VarInt than 32bit unsigned");
			}
			if ((num2 & 128) == 0)
			{
				return num | (uint)((uint)num2 << 7 * i);
			}
			num |= (uint)((uint)(num2 & 127) << 7 * i);
		}
		throw new ProtocolBufferException("Got larger VarInt than 32bit unsigned");
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00003B2C File Offset: 0x00001D2C
	public static void WriteUInt32(Stream stream, uint val)
	{
		byte b;
		for (;;)
		{
			b = (byte)(val & 127U);
			val >>= 7;
			if (val == 0U)
			{
				break;
			}
			b |= 128;
			stream.WriteByte(b);
		}
		stream.WriteByte(b);
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00003B60 File Offset: 0x00001D60
	public static uint SizeOfUInt32(int val)
	{
		return ProtocolParser.SizeOfUInt32((uint)val);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00003B68 File Offset: 0x00001D68
	public static uint SizeOfUInt32(uint val)
	{
		uint num = 1U;
		for (;;)
		{
			val >>= 7;
			if (val == 0U)
			{
				break;
			}
			num += 1U;
		}
		return num;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00003A5E File Offset: 0x00001C5E
	[Obsolete("Use (long)ReadUInt64(stream); instead")]
	public static int ReadInt64(Stream stream)
	{
		return (int)ProtocolParser.ReadUInt64(stream);
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00003A67 File Offset: 0x00001C67
	[Obsolete("Use WriteUInt64 (stream, (ulong)val); instead")]
	public static void WriteInt64(Stream stream, int val)
	{
		ProtocolParser.WriteUInt64(stream, (ulong)((long)val));
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00003B88 File Offset: 0x00001D88
	public static long ReadZInt64(Stream stream)
	{
		ulong num = ProtocolParser.ReadUInt64(stream);
		return (long)(num >> 1 ^ num << 63 >> 63);
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00003BA7 File Offset: 0x00001DA7
	public static void WriteZInt64(Stream stream, long val)
	{
		ProtocolParser.WriteUInt64(stream, (ulong)(val << 1 ^ val >> 63));
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00003BB7 File Offset: 0x00001DB7
	public static uint SizeOfZInt64(long val)
	{
		return ProtocolParser.SizeOfUInt64((ulong)(val << 1 ^ val >> 63));
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00003BC8 File Offset: 0x00001DC8
	public static ulong ReadUInt64(Stream stream)
	{
		ulong num = 0UL;
		for (int i = 0; i < 10; i++)
		{
			int num2 = stream.ReadByte();
			if (num2 < 0)
			{
				throw new IOException("Stream ended too early");
			}
			if (i == 9 && (num2 & 254) != 0)
			{
				throw new ProtocolBufferException("Got larger VarInt than 64 bit unsigned");
			}
			if ((num2 & 128) == 0)
			{
				return num | (ulong)((ulong)((long)num2) << 7 * i);
			}
			num |= (ulong)((ulong)((long)(num2 & 127)) << 7 * i);
		}
		throw new ProtocolBufferException("Got larger VarInt than 64 bit unsigned");
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00003C44 File Offset: 0x00001E44
	public static void WriteUInt64(Stream stream, ulong val)
	{
		byte b;
		for (;;)
		{
			b = (byte)(val & 127UL);
			val >>= 7;
			if (val == 0UL)
			{
				break;
			}
			b |= 128;
			stream.WriteByte(b);
		}
		stream.WriteByte(b);
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00003C79 File Offset: 0x00001E79
	public static uint SizeOfUInt64(int val)
	{
		return ProtocolParser.SizeOfUInt64((ulong)((long)val));
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00003C84 File Offset: 0x00001E84
	public static uint SizeOfUInt64(ulong val)
	{
		uint num = 1U;
		for (;;)
		{
			val >>= 7;
			if (val == 0UL)
			{
				break;
			}
			num += 1U;
		}
		return num;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00003CA4 File Offset: 0x00001EA4
	public static bool ReadBool(Stream stream)
	{
		int num = stream.ReadByte();
		if (num < 0)
		{
			throw new IOException("Stream ended too early");
		}
		if (num == 1)
		{
			return true;
		}
		if (num == 0)
		{
			return false;
		}
		throw new ProtocolBufferException("Invalid boolean value");
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00003CDC File Offset: 0x00001EDC
	public static void WriteBool(Stream stream, bool val)
	{
		stream.WriteByte(val ? 1 : 0);
	}

	// Token: 0x04000020 RID: 32
	public static MemoryStreamStack Stack = new AllocationStack();
}
