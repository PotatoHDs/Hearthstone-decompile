using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000F9 RID: 249
	public class BattlegroundsRatingInfoResponse : IProtoBuf
	{
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x0003A5FD File Offset: 0x000387FD
		// (set) Token: 0x0600108C RID: 4236 RVA: 0x0003A605 File Offset: 0x00038805
		public BattlegroundsPlayerInfo PlayerInfo { get; set; }

		// Token: 0x0600108D RID: 4237 RVA: 0x0003A60E File Offset: 0x0003880E
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.PlayerInfo.GetHashCode();
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0003A628 File Offset: 0x00038828
		public override bool Equals(object obj)
		{
			BattlegroundsRatingInfoResponse battlegroundsRatingInfoResponse = obj as BattlegroundsRatingInfoResponse;
			return battlegroundsRatingInfoResponse != null && this.PlayerInfo.Equals(battlegroundsRatingInfoResponse.PlayerInfo);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0003A657 File Offset: 0x00038857
		public void Deserialize(Stream stream)
		{
			BattlegroundsRatingInfoResponse.Deserialize(stream, this);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0003A661 File Offset: 0x00038861
		public static BattlegroundsRatingInfoResponse Deserialize(Stream stream, BattlegroundsRatingInfoResponse instance)
		{
			return BattlegroundsRatingInfoResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0003A66C File Offset: 0x0003886C
		public static BattlegroundsRatingInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			BattlegroundsRatingInfoResponse battlegroundsRatingInfoResponse = new BattlegroundsRatingInfoResponse();
			BattlegroundsRatingInfoResponse.DeserializeLengthDelimited(stream, battlegroundsRatingInfoResponse);
			return battlegroundsRatingInfoResponse;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0003A688 File Offset: 0x00038888
		public static BattlegroundsRatingInfoResponse DeserializeLengthDelimited(Stream stream, BattlegroundsRatingInfoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BattlegroundsRatingInfoResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0003A6B0 File Offset: 0x000388B0
		public static BattlegroundsRatingInfoResponse Deserialize(Stream stream, BattlegroundsRatingInfoResponse instance, long limit)
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
				else if (num == 10)
				{
					if (instance.PlayerInfo == null)
					{
						instance.PlayerInfo = BattlegroundsPlayerInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						BattlegroundsPlayerInfo.DeserializeLengthDelimited(stream, instance.PlayerInfo);
					}
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

		// Token: 0x06001094 RID: 4244 RVA: 0x0003A74A File Offset: 0x0003894A
		public void Serialize(Stream stream)
		{
			BattlegroundsRatingInfoResponse.Serialize(stream, this);
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0003A753 File Offset: 0x00038953
		public static void Serialize(Stream stream, BattlegroundsRatingInfoResponse instance)
		{
			if (instance.PlayerInfo == null)
			{
				throw new ArgumentNullException("PlayerInfo", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.PlayerInfo.GetSerializedSize());
			BattlegroundsPlayerInfo.Serialize(stream, instance.PlayerInfo);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0003A794 File Offset: 0x00038994
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.PlayerInfo.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1U;
		}

		// Token: 0x020005FC RID: 1532
		public enum PacketID
		{
			// Token: 0x04002031 RID: 8241
			ID = 373,
			// Token: 0x04002032 RID: 8242
			System = 0
		}
	}
}
