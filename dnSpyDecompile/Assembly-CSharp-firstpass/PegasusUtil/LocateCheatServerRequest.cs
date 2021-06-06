using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000ED RID: 237
	public class LocateCheatServerRequest : IProtoBuf
	{
		// Token: 0x06000FE9 RID: 4073 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00038FEA File Offset: 0x000371EA
		public override bool Equals(object obj)
		{
			return obj is LocateCheatServerRequest;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00038FF7 File Offset: 0x000371F7
		public void Deserialize(Stream stream)
		{
			LocateCheatServerRequest.Deserialize(stream, this);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00039001 File Offset: 0x00037201
		public static LocateCheatServerRequest Deserialize(Stream stream, LocateCheatServerRequest instance)
		{
			return LocateCheatServerRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0003900C File Offset: 0x0003720C
		public static LocateCheatServerRequest DeserializeLengthDelimited(Stream stream)
		{
			LocateCheatServerRequest locateCheatServerRequest = new LocateCheatServerRequest();
			LocateCheatServerRequest.DeserializeLengthDelimited(stream, locateCheatServerRequest);
			return locateCheatServerRequest;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00039028 File Offset: 0x00037228
		public static LocateCheatServerRequest DeserializeLengthDelimited(Stream stream, LocateCheatServerRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LocateCheatServerRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00039050 File Offset: 0x00037250
		public static LocateCheatServerRequest Deserialize(Stream stream, LocateCheatServerRequest instance, long limit)
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

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000390BD File Offset: 0x000372BD
		public void Serialize(Stream stream)
		{
			LocateCheatServerRequest.Serialize(stream, this);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, LocateCheatServerRequest instance)
		{
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x020005F1 RID: 1521
		public enum PacketID
		{
			// Token: 0x04002015 RID: 8213
			ID = 361,
			// Token: 0x04002016 RID: 8214
			System = 4
		}
	}
}
