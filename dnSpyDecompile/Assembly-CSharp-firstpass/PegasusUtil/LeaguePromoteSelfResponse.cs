using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000F4 RID: 244
	public class LeaguePromoteSelfResponse : IProtoBuf
	{
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x00039D66 File Offset: 0x00037F66
		// (set) Token: 0x0600104B RID: 4171 RVA: 0x00039D6E File Offset: 0x00037F6E
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x0600104C RID: 4172 RVA: 0x00039D78 File Offset: 0x00037F78
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ErrorCode.GetHashCode();
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00039DA8 File Offset: 0x00037FA8
		public override bool Equals(object obj)
		{
			LeaguePromoteSelfResponse leaguePromoteSelfResponse = obj as LeaguePromoteSelfResponse;
			return leaguePromoteSelfResponse != null && this.ErrorCode.Equals(leaguePromoteSelfResponse.ErrorCode);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00039DE5 File Offset: 0x00037FE5
		public void Deserialize(Stream stream)
		{
			LeaguePromoteSelfResponse.Deserialize(stream, this);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00039DEF File Offset: 0x00037FEF
		public static LeaguePromoteSelfResponse Deserialize(Stream stream, LeaguePromoteSelfResponse instance)
		{
			return LeaguePromoteSelfResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00039DFC File Offset: 0x00037FFC
		public static LeaguePromoteSelfResponse DeserializeLengthDelimited(Stream stream)
		{
			LeaguePromoteSelfResponse leaguePromoteSelfResponse = new LeaguePromoteSelfResponse();
			LeaguePromoteSelfResponse.DeserializeLengthDelimited(stream, leaguePromoteSelfResponse);
			return leaguePromoteSelfResponse;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00039E18 File Offset: 0x00038018
		public static LeaguePromoteSelfResponse DeserializeLengthDelimited(Stream stream, LeaguePromoteSelfResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LeaguePromoteSelfResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00039E40 File Offset: 0x00038040
		public static LeaguePromoteSelfResponse Deserialize(Stream stream, LeaguePromoteSelfResponse instance, long limit)
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
				else if (num == 8)
				{
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001053 RID: 4179 RVA: 0x00039EC0 File Offset: 0x000380C0
		public void Serialize(Stream stream)
		{
			LeaguePromoteSelfResponse.Serialize(stream, this);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00039EC9 File Offset: 0x000380C9
		public static void Serialize(Stream stream, LeaguePromoteSelfResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00039EDF File Offset: 0x000380DF
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode)) + 1U;
		}

		// Token: 0x020005F8 RID: 1528
		public enum PacketID
		{
			// Token: 0x04002027 RID: 8231
			ID = 368
		}
	}
}
