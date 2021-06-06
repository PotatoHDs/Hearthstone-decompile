using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000F3 RID: 243
	public class LeaguePromoteSelfRequest : IProtoBuf
	{
		// Token: 0x0600103F RID: 4159 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00039C89 File Offset: 0x00037E89
		public override bool Equals(object obj)
		{
			return obj is LeaguePromoteSelfRequest;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00039C96 File Offset: 0x00037E96
		public void Deserialize(Stream stream)
		{
			LeaguePromoteSelfRequest.Deserialize(stream, this);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00039CA0 File Offset: 0x00037EA0
		public static LeaguePromoteSelfRequest Deserialize(Stream stream, LeaguePromoteSelfRequest instance)
		{
			return LeaguePromoteSelfRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00039CAC File Offset: 0x00037EAC
		public static LeaguePromoteSelfRequest DeserializeLengthDelimited(Stream stream)
		{
			LeaguePromoteSelfRequest leaguePromoteSelfRequest = new LeaguePromoteSelfRequest();
			LeaguePromoteSelfRequest.DeserializeLengthDelimited(stream, leaguePromoteSelfRequest);
			return leaguePromoteSelfRequest;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00039CC8 File Offset: 0x00037EC8
		public static LeaguePromoteSelfRequest DeserializeLengthDelimited(Stream stream, LeaguePromoteSelfRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LeaguePromoteSelfRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00039CF0 File Offset: 0x00037EF0
		public static LeaguePromoteSelfRequest Deserialize(Stream stream, LeaguePromoteSelfRequest instance, long limit)
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

		// Token: 0x06001046 RID: 4166 RVA: 0x00039D5D File Offset: 0x00037F5D
		public void Serialize(Stream stream)
		{
			LeaguePromoteSelfRequest.Serialize(stream, this);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, LeaguePromoteSelfRequest instance)
		{
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x020005F7 RID: 1527
		public enum PacketID
		{
			// Token: 0x04002024 RID: 8228
			ID = 367,
			// Token: 0x04002025 RID: 8229
			System = 0
		}
	}
}
