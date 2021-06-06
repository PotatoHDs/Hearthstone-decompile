using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000052 RID: 82
	public class GetAccountInfo : IProtoBuf
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00015892 File Offset: 0x00013A92
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x0001589A File Offset: 0x00013A9A
		public GetAccountInfo.Request Request_ { get; set; }

		// Token: 0x0600053A RID: 1338 RVA: 0x000158A4 File Offset: 0x00013AA4
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Request_.GetHashCode();
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000158D4 File Offset: 0x00013AD4
		public override bool Equals(object obj)
		{
			GetAccountInfo getAccountInfo = obj as GetAccountInfo;
			return getAccountInfo != null && this.Request_.Equals(getAccountInfo.Request_);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00015911 File Offset: 0x00013B11
		public void Deserialize(Stream stream)
		{
			GetAccountInfo.Deserialize(stream, this);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001591B File Offset: 0x00013B1B
		public static GetAccountInfo Deserialize(Stream stream, GetAccountInfo instance)
		{
			return GetAccountInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00015928 File Offset: 0x00013B28
		public static GetAccountInfo DeserializeLengthDelimited(Stream stream)
		{
			GetAccountInfo getAccountInfo = new GetAccountInfo();
			GetAccountInfo.DeserializeLengthDelimited(stream, getAccountInfo);
			return getAccountInfo;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00015944 File Offset: 0x00013B44
		public static GetAccountInfo DeserializeLengthDelimited(Stream stream, GetAccountInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAccountInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001596C File Offset: 0x00013B6C
		public static GetAccountInfo Deserialize(Stream stream, GetAccountInfo instance, long limit)
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
					instance.Request_ = (GetAccountInfo.Request)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000541 RID: 1345 RVA: 0x000159EC File Offset: 0x00013BEC
		public void Serialize(Stream stream)
		{
			GetAccountInfo.Serialize(stream, this);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000159F5 File Offset: 0x00013BF5
		public static void Serialize(Stream stream, GetAccountInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Request_));
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00015A0B File Offset: 0x00013C0B
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Request_)) + 1U;
		}

		// Token: 0x02000563 RID: 1379
		public enum PacketID
		{
			// Token: 0x04001E66 RID: 7782
			ID = 201,
			// Token: 0x04001E67 RID: 7783
			System = 0
		}

		// Token: 0x02000564 RID: 1380
		public enum Request
		{
			// Token: 0x04001E69 RID: 7785
			LAST_LOGIN = 1,
			// Token: 0x04001E6A RID: 7786
			DECK_LIST,
			// Token: 0x04001E6B RID: 7787
			COLLECTION,
			// Token: 0x04001E6C RID: 7788
			MEDAL_INFO,
			// Token: 0x04001E6D RID: 7789
			MEDAL_HISTORY,
			// Token: 0x04001E6E RID: 7790
			BOOSTERS,
			// Token: 0x04001E6F RID: 7791
			CARD_BACKS,
			// Token: 0x04001E70 RID: 7792
			PLAYER_RECORD,
			// Token: 0x04001E71 RID: 7793
			GAMES_PLAYED,
			// Token: 0x04001E72 RID: 7794
			DECK_LIMIT,
			// Token: 0x04001E73 RID: 7795
			CAMPAIGN_INFO,
			// Token: 0x04001E74 RID: 7796
			NOTICES,
			// Token: 0x04001E75 RID: 7797
			MOTD,
			// Token: 0x04001E76 RID: 7798
			CLIENT_OPTIONS,
			// Token: 0x04001E77 RID: 7799
			CARD_VALUES,
			// Token: 0x04001E78 RID: 7800
			DISCONNECTED,
			// Token: 0x04001E79 RID: 7801
			ARCANE_DUST_BALANCE,
			// Token: 0x04001E7A RID: 7802
			FEATURES,
			// Token: 0x04001E7B RID: 7803
			REWARD_PROGRESS,
			// Token: 0x04001E7C RID: 7804
			GOLD_BALANCE,
			// Token: 0x04001E7D RID: 7805
			HERO_XP,
			// Token: 0x04001E7E RID: 7806
			PVP_QUEUE,
			// Token: 0x04001E7F RID: 7807
			NOT_SO_MASSIVE_LOGIN,
			// Token: 0x04001E80 RID: 7808
			BOOSTER_TALLY,
			// Token: 0x04001E81 RID: 7809
			TAVERN_BRAWL_INFO,
			// Token: 0x04001E82 RID: 7810
			TAVERN_BRAWL_RECORD,
			// Token: 0x04001E83 RID: 7811
			FAVORITE_HEROES,
			// Token: 0x04001E84 RID: 7812
			ACCOUNT_LICENSES,
			// Token: 0x04001E85 RID: 7813
			COINS
		}
	}
}
