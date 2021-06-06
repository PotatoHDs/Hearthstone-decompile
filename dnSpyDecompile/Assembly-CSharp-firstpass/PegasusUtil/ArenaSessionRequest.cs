using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000079 RID: 121
	public class ArenaSessionRequest : IProtoBuf
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001BEA2 File Offset: 0x0001A0A2
		public override bool Equals(object obj)
		{
			return obj is ArenaSessionRequest;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001BEAF File Offset: 0x0001A0AF
		public void Deserialize(Stream stream)
		{
			ArenaSessionRequest.Deserialize(stream, this);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001BEB9 File Offset: 0x0001A0B9
		public static ArenaSessionRequest Deserialize(Stream stream, ArenaSessionRequest instance)
		{
			return ArenaSessionRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001BEC4 File Offset: 0x0001A0C4
		public static ArenaSessionRequest DeserializeLengthDelimited(Stream stream)
		{
			ArenaSessionRequest arenaSessionRequest = new ArenaSessionRequest();
			ArenaSessionRequest.DeserializeLengthDelimited(stream, arenaSessionRequest);
			return arenaSessionRequest;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001BEE0 File Offset: 0x0001A0E0
		public static ArenaSessionRequest DeserializeLengthDelimited(Stream stream, ArenaSessionRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ArenaSessionRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001BF08 File Offset: 0x0001A108
		public static ArenaSessionRequest Deserialize(Stream stream, ArenaSessionRequest instance, long limit)
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

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001BF75 File Offset: 0x0001A175
		public void Serialize(Stream stream)
		{
			ArenaSessionRequest.Serialize(stream, this);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, ArenaSessionRequest instance)
		{
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x0200058C RID: 1420
		public enum PacketID
		{
			// Token: 0x04001F00 RID: 7936
			ID = 346,
			// Token: 0x04001F01 RID: 7937
			System = 0
		}
	}
}
