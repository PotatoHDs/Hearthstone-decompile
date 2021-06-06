using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200007D RID: 125
	public class BattlegroundsPremiumStatusRequest : IProtoBuf
	{
		// Token: 0x060007CF RID: 1999 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001C766 File Offset: 0x0001A966
		public override bool Equals(object obj)
		{
			return obj is BattlegroundsPremiumStatusRequest;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001C773 File Offset: 0x0001A973
		public void Deserialize(Stream stream)
		{
			BattlegroundsPremiumStatusRequest.Deserialize(stream, this);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001C77D File Offset: 0x0001A97D
		public static BattlegroundsPremiumStatusRequest Deserialize(Stream stream, BattlegroundsPremiumStatusRequest instance)
		{
			return BattlegroundsPremiumStatusRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001C788 File Offset: 0x0001A988
		public static BattlegroundsPremiumStatusRequest DeserializeLengthDelimited(Stream stream)
		{
			BattlegroundsPremiumStatusRequest battlegroundsPremiumStatusRequest = new BattlegroundsPremiumStatusRequest();
			BattlegroundsPremiumStatusRequest.DeserializeLengthDelimited(stream, battlegroundsPremiumStatusRequest);
			return battlegroundsPremiumStatusRequest;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		public static BattlegroundsPremiumStatusRequest DeserializeLengthDelimited(Stream stream, BattlegroundsPremiumStatusRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BattlegroundsPremiumStatusRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001C7CC File Offset: 0x0001A9CC
		public static BattlegroundsPremiumStatusRequest Deserialize(Stream stream, BattlegroundsPremiumStatusRequest instance, long limit)
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

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001C839 File Offset: 0x0001AA39
		public void Serialize(Stream stream)
		{
			BattlegroundsPremiumStatusRequest.Serialize(stream, this);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, BattlegroundsPremiumStatusRequest instance)
		{
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000590 RID: 1424
		public enum PacketID
		{
			// Token: 0x04001F0C RID: 7948
			ID = 374,
			// Token: 0x04001F0D RID: 7949
			System = 0
		}
	}
}
