using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeRewardCurrency : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 9
		}

		public bool HasCurrencyType;

		private CurrencyType _CurrencyType;

		public int Amount { get; set; }

		public CurrencyType CurrencyType
		{
			get
			{
				return _CurrencyType;
			}
			set
			{
				_CurrencyType = value;
				HasCurrencyType = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Amount.GetHashCode();
			if (HasCurrencyType)
			{
				hashCode ^= CurrencyType.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeRewardCurrency profileNoticeRewardCurrency = obj as ProfileNoticeRewardCurrency;
			if (profileNoticeRewardCurrency == null)
			{
				return false;
			}
			if (!Amount.Equals(profileNoticeRewardCurrency.Amount))
			{
				return false;
			}
			if (HasCurrencyType != profileNoticeRewardCurrency.HasCurrencyType || (HasCurrencyType && !CurrencyType.Equals(profileNoticeRewardCurrency.CurrencyType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeRewardCurrency Deserialize(Stream stream, ProfileNoticeRewardCurrency instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeRewardCurrency DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeRewardCurrency profileNoticeRewardCurrency = new ProfileNoticeRewardCurrency();
			DeserializeLengthDelimited(stream, profileNoticeRewardCurrency);
			return profileNoticeRewardCurrency;
		}

		public static ProfileNoticeRewardCurrency DeserializeLengthDelimited(Stream stream, ProfileNoticeRewardCurrency instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeRewardCurrency Deserialize(Stream stream, ProfileNoticeRewardCurrency instance, long limit)
		{
			instance.CurrencyType = CurrencyType.CURRENCY_TYPE_UNKNOWN;
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
					instance.Amount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.CurrencyType = (CurrencyType)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeRewardCurrency instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Amount);
			if (instance.HasCurrencyType)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrencyType);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Amount);
			if (HasCurrencyType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CurrencyType);
			}
			return num + 1;
		}
	}
}
