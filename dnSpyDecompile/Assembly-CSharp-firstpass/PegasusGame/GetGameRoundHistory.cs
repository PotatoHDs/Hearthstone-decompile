using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001A3 RID: 419
	public class GetGameRoundHistory : IProtoBuf
	{
		// Token: 0x06001A54 RID: 6740 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x0005D1A4 File Offset: 0x0005B3A4
		public override bool Equals(object obj)
		{
			return obj is GetGameRoundHistory;
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x0005D1B1 File Offset: 0x0005B3B1
		public void Deserialize(Stream stream)
		{
			GetGameRoundHistory.Deserialize(stream, this);
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x0005D1BB File Offset: 0x0005B3BB
		public static GetGameRoundHistory Deserialize(Stream stream, GetGameRoundHistory instance)
		{
			return GetGameRoundHistory.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x0005D1C8 File Offset: 0x0005B3C8
		public static GetGameRoundHistory DeserializeLengthDelimited(Stream stream)
		{
			GetGameRoundHistory getGameRoundHistory = new GetGameRoundHistory();
			GetGameRoundHistory.DeserializeLengthDelimited(stream, getGameRoundHistory);
			return getGameRoundHistory;
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0005D1E4 File Offset: 0x0005B3E4
		public static GetGameRoundHistory DeserializeLengthDelimited(Stream stream, GetGameRoundHistory instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameRoundHistory.Deserialize(stream, instance, num);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0005D20C File Offset: 0x0005B40C
		public static GetGameRoundHistory Deserialize(Stream stream, GetGameRoundHistory instance, long limit)
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

		// Token: 0x06001A5B RID: 6747 RVA: 0x0005D279 File Offset: 0x0005B479
		public void Serialize(Stream stream)
		{
			GetGameRoundHistory.Serialize(stream, this);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, GetGameRoundHistory instance)
		{
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x0200063D RID: 1597
		public enum PacketID
		{
			// Token: 0x040020EE RID: 8430
			ID = 32
		}
	}
}
