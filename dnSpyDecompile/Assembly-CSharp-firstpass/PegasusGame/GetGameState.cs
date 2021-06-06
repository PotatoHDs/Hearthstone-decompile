using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001A9 RID: 425
	public class GetGameState : IProtoBuf
	{
		// Token: 0x06001ADE RID: 6878 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0005F37E File Offset: 0x0005D57E
		public override bool Equals(object obj)
		{
			return obj is GetGameState;
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0005F38B File Offset: 0x0005D58B
		public void Deserialize(Stream stream)
		{
			GetGameState.Deserialize(stream, this);
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0005F395 File Offset: 0x0005D595
		public static GetGameState Deserialize(Stream stream, GetGameState instance)
		{
			return GetGameState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0005F3A0 File Offset: 0x0005D5A0
		public static GetGameState DeserializeLengthDelimited(Stream stream)
		{
			GetGameState getGameState = new GetGameState();
			GetGameState.DeserializeLengthDelimited(stream, getGameState);
			return getGameState;
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0005F3BC File Offset: 0x0005D5BC
		public static GetGameState DeserializeLengthDelimited(Stream stream, GetGameState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameState.Deserialize(stream, instance, num);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0005F3E4 File Offset: 0x0005D5E4
		public static GetGameState Deserialize(Stream stream, GetGameState instance, long limit)
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

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0005F451 File Offset: 0x0005D651
		public void Serialize(Stream stream)
		{
			GetGameState.Serialize(stream, this);
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, GetGameState instance)
		{
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000641 RID: 1601
		public enum PacketID
		{
			// Token: 0x040020F6 RID: 8438
			ID = 1
		}
	}
}
