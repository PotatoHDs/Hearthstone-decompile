using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001A8 RID: 424
	public class Ping : IProtoBuf
	{
		// Token: 0x06001AD3 RID: 6867 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0005F29F File Offset: 0x0005D49F
		public override bool Equals(object obj)
		{
			return obj is Ping;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0005F2AC File Offset: 0x0005D4AC
		public void Deserialize(Stream stream)
		{
			Ping.Deserialize(stream, this);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0005F2B6 File Offset: 0x0005D4B6
		public static Ping Deserialize(Stream stream, Ping instance)
		{
			return Ping.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0005F2C4 File Offset: 0x0005D4C4
		public static Ping DeserializeLengthDelimited(Stream stream)
		{
			Ping ping = new Ping();
			Ping.DeserializeLengthDelimited(stream, ping);
			return ping;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0005F2E0 File Offset: 0x0005D4E0
		public static Ping DeserializeLengthDelimited(Stream stream, Ping instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Ping.Deserialize(stream, instance, num);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0005F308 File Offset: 0x0005D508
		public static Ping Deserialize(Stream stream, Ping instance, long limit)
		{
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0005F375 File Offset: 0x0005D575
		public void Serialize(Stream stream)
		{
			Ping.Serialize(stream, this);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, Ping instance)
		{
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000640 RID: 1600
		public enum PacketID
		{
			// Token: 0x040020F4 RID: 8436
			ID = 115
		}
	}
}
