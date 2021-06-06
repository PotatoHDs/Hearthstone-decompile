using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001A4 RID: 420
	public class GetGameRealTimeBattlefieldRaces : IProtoBuf
	{
		// Token: 0x06001A5F RID: 6751 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0005D282 File Offset: 0x0005B482
		public override bool Equals(object obj)
		{
			return obj is GetGameRealTimeBattlefieldRaces;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0005D28F File Offset: 0x0005B48F
		public void Deserialize(Stream stream)
		{
			GetGameRealTimeBattlefieldRaces.Deserialize(stream, this);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0005D299 File Offset: 0x0005B499
		public static GetGameRealTimeBattlefieldRaces Deserialize(Stream stream, GetGameRealTimeBattlefieldRaces instance)
		{
			return GetGameRealTimeBattlefieldRaces.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0005D2A4 File Offset: 0x0005B4A4
		public static GetGameRealTimeBattlefieldRaces DeserializeLengthDelimited(Stream stream)
		{
			GetGameRealTimeBattlefieldRaces getGameRealTimeBattlefieldRaces = new GetGameRealTimeBattlefieldRaces();
			GetGameRealTimeBattlefieldRaces.DeserializeLengthDelimited(stream, getGameRealTimeBattlefieldRaces);
			return getGameRealTimeBattlefieldRaces;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0005D2C0 File Offset: 0x0005B4C0
		public static GetGameRealTimeBattlefieldRaces DeserializeLengthDelimited(Stream stream, GetGameRealTimeBattlefieldRaces instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameRealTimeBattlefieldRaces.Deserialize(stream, instance, num);
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0005D2E8 File Offset: 0x0005B4E8
		public static GetGameRealTimeBattlefieldRaces Deserialize(Stream stream, GetGameRealTimeBattlefieldRaces instance, long limit)
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

		// Token: 0x06001A66 RID: 6758 RVA: 0x0005D355 File Offset: 0x0005B555
		public void Serialize(Stream stream)
		{
			GetGameRealTimeBattlefieldRaces.Serialize(stream, this);
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, GetGameRealTimeBattlefieldRaces instance)
		{
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x0200063E RID: 1598
		public enum PacketID
		{
			// Token: 0x040020F0 RID: 8432
			ID = 33
		}
	}
}
