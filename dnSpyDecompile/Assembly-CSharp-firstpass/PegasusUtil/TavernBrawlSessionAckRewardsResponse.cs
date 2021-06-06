using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000E8 RID: 232
	public class TavernBrawlSessionAckRewardsResponse : IProtoBuf
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00038279 File Offset: 0x00036479
		// (set) Token: 0x06000FA1 RID: 4001 RVA: 0x00038281 File Offset: 0x00036481
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0003828C File Offset: 0x0003648C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ErrorCode.GetHashCode();
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x000382BC File Offset: 0x000364BC
		public override bool Equals(object obj)
		{
			TavernBrawlSessionAckRewardsResponse tavernBrawlSessionAckRewardsResponse = obj as TavernBrawlSessionAckRewardsResponse;
			return tavernBrawlSessionAckRewardsResponse != null && this.ErrorCode.Equals(tavernBrawlSessionAckRewardsResponse.ErrorCode);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x000382F9 File Offset: 0x000364F9
		public void Deserialize(Stream stream)
		{
			TavernBrawlSessionAckRewardsResponse.Deserialize(stream, this);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00038303 File Offset: 0x00036503
		public static TavernBrawlSessionAckRewardsResponse Deserialize(Stream stream, TavernBrawlSessionAckRewardsResponse instance)
		{
			return TavernBrawlSessionAckRewardsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00038310 File Offset: 0x00036510
		public static TavernBrawlSessionAckRewardsResponse DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlSessionAckRewardsResponse tavernBrawlSessionAckRewardsResponse = new TavernBrawlSessionAckRewardsResponse();
			TavernBrawlSessionAckRewardsResponse.DeserializeLengthDelimited(stream, tavernBrawlSessionAckRewardsResponse);
			return tavernBrawlSessionAckRewardsResponse;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003832C File Offset: 0x0003652C
		public static TavernBrawlSessionAckRewardsResponse DeserializeLengthDelimited(Stream stream, TavernBrawlSessionAckRewardsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TavernBrawlSessionAckRewardsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00038354 File Offset: 0x00036554
		public static TavernBrawlSessionAckRewardsResponse Deserialize(Stream stream, TavernBrawlSessionAckRewardsResponse instance, long limit)
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

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000383D4 File Offset: 0x000365D4
		public void Serialize(Stream stream)
		{
			TavernBrawlSessionAckRewardsResponse.Serialize(stream, this);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x000383DD File Offset: 0x000365DD
		public static void Serialize(Stream stream, TavernBrawlSessionAckRewardsResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x000383F3 File Offset: 0x000365F3
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode)) + 1U;
		}

		// Token: 0x020005EC RID: 1516
		public enum PacketID
		{
			// Token: 0x04002008 RID: 8200
			ID = 349
		}
	}
}
