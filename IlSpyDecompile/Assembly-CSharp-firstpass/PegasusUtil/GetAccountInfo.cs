using System.IO;

namespace PegasusUtil
{
	public class GetAccountInfo : IProtoBuf
	{
		public enum PacketID
		{
			ID = 201,
			System = 0
		}

		public enum Request
		{
			LAST_LOGIN = 1,
			DECK_LIST,
			COLLECTION,
			MEDAL_INFO,
			MEDAL_HISTORY,
			BOOSTERS,
			CARD_BACKS,
			PLAYER_RECORD,
			GAMES_PLAYED,
			DECK_LIMIT,
			CAMPAIGN_INFO,
			NOTICES,
			MOTD,
			CLIENT_OPTIONS,
			CARD_VALUES,
			DISCONNECTED,
			ARCANE_DUST_BALANCE,
			FEATURES,
			REWARD_PROGRESS,
			GOLD_BALANCE,
			HERO_XP,
			PVP_QUEUE,
			NOT_SO_MASSIVE_LOGIN,
			BOOSTER_TALLY,
			TAVERN_BRAWL_INFO,
			TAVERN_BRAWL_RECORD,
			FAVORITE_HEROES,
			ACCOUNT_LICENSES,
			COINS
		}

		public Request Request_ { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Request_.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GetAccountInfo getAccountInfo = obj as GetAccountInfo;
			if (getAccountInfo == null)
			{
				return false;
			}
			if (!Request_.Equals(getAccountInfo.Request_))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetAccountInfo Deserialize(Stream stream, GetAccountInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetAccountInfo DeserializeLengthDelimited(Stream stream)
		{
			GetAccountInfo getAccountInfo = new GetAccountInfo();
			DeserializeLengthDelimited(stream, getAccountInfo);
			return getAccountInfo;
		}

		public static GetAccountInfo DeserializeLengthDelimited(Stream stream, GetAccountInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetAccountInfo Deserialize(Stream stream, GetAccountInfo instance, long limit)
		{
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 8:
					instance.Request_ = (Request)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetAccountInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Request_);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Request_) + 1;
		}
	}
}
