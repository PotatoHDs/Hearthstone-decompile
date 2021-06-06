using System.IO;

namespace PegasusUtil
{
	public class GameCurrencyStates : IProtoBuf
	{
		public bool HasArcaneDustBalance;

		private long _ArcaneDustBalance;

		public bool HasCappedGoldBalance;

		private long _CappedGoldBalance;

		public bool HasBonusGoldBalance;

		private long _BonusGoldBalance;

		public bool HasGoldCap;

		private long _GoldCap;

		public bool HasGoldCapWarning;

		private long _GoldCapWarning;

		public bool HasCurrencyVersion;

		private long _CurrencyVersion;

		public long ArcaneDustBalance
		{
			get
			{
				return _ArcaneDustBalance;
			}
			set
			{
				_ArcaneDustBalance = value;
				HasArcaneDustBalance = true;
			}
		}

		public long CappedGoldBalance
		{
			get
			{
				return _CappedGoldBalance;
			}
			set
			{
				_CappedGoldBalance = value;
				HasCappedGoldBalance = true;
			}
		}

		public long BonusGoldBalance
		{
			get
			{
				return _BonusGoldBalance;
			}
			set
			{
				_BonusGoldBalance = value;
				HasBonusGoldBalance = true;
			}
		}

		public long GoldCap
		{
			get
			{
				return _GoldCap;
			}
			set
			{
				_GoldCap = value;
				HasGoldCap = true;
			}
		}

		public long GoldCapWarning
		{
			get
			{
				return _GoldCapWarning;
			}
			set
			{
				_GoldCapWarning = value;
				HasGoldCapWarning = true;
			}
		}

		public long CurrencyVersion
		{
			get
			{
				return _CurrencyVersion;
			}
			set
			{
				_CurrencyVersion = value;
				HasCurrencyVersion = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasArcaneDustBalance)
			{
				num ^= ArcaneDustBalance.GetHashCode();
			}
			if (HasCappedGoldBalance)
			{
				num ^= CappedGoldBalance.GetHashCode();
			}
			if (HasBonusGoldBalance)
			{
				num ^= BonusGoldBalance.GetHashCode();
			}
			if (HasGoldCap)
			{
				num ^= GoldCap.GetHashCode();
			}
			if (HasGoldCapWarning)
			{
				num ^= GoldCapWarning.GetHashCode();
			}
			if (HasCurrencyVersion)
			{
				num ^= CurrencyVersion.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameCurrencyStates gameCurrencyStates = obj as GameCurrencyStates;
			if (gameCurrencyStates == null)
			{
				return false;
			}
			if (HasArcaneDustBalance != gameCurrencyStates.HasArcaneDustBalance || (HasArcaneDustBalance && !ArcaneDustBalance.Equals(gameCurrencyStates.ArcaneDustBalance)))
			{
				return false;
			}
			if (HasCappedGoldBalance != gameCurrencyStates.HasCappedGoldBalance || (HasCappedGoldBalance && !CappedGoldBalance.Equals(gameCurrencyStates.CappedGoldBalance)))
			{
				return false;
			}
			if (HasBonusGoldBalance != gameCurrencyStates.HasBonusGoldBalance || (HasBonusGoldBalance && !BonusGoldBalance.Equals(gameCurrencyStates.BonusGoldBalance)))
			{
				return false;
			}
			if (HasGoldCap != gameCurrencyStates.HasGoldCap || (HasGoldCap && !GoldCap.Equals(gameCurrencyStates.GoldCap)))
			{
				return false;
			}
			if (HasGoldCapWarning != gameCurrencyStates.HasGoldCapWarning || (HasGoldCapWarning && !GoldCapWarning.Equals(gameCurrencyStates.GoldCapWarning)))
			{
				return false;
			}
			if (HasCurrencyVersion != gameCurrencyStates.HasCurrencyVersion || (HasCurrencyVersion && !CurrencyVersion.Equals(gameCurrencyStates.CurrencyVersion)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameCurrencyStates Deserialize(Stream stream, GameCurrencyStates instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameCurrencyStates DeserializeLengthDelimited(Stream stream)
		{
			GameCurrencyStates gameCurrencyStates = new GameCurrencyStates();
			DeserializeLengthDelimited(stream, gameCurrencyStates);
			return gameCurrencyStates;
		}

		public static GameCurrencyStates DeserializeLengthDelimited(Stream stream, GameCurrencyStates instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameCurrencyStates Deserialize(Stream stream, GameCurrencyStates instance, long limit)
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
					instance.ArcaneDustBalance = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.CappedGoldBalance = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.BonusGoldBalance = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.GoldCap = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.GoldCapWarning = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.CurrencyVersion = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GameCurrencyStates instance)
		{
			if (instance.HasArcaneDustBalance)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ArcaneDustBalance);
			}
			if (instance.HasCappedGoldBalance)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CappedGoldBalance);
			}
			if (instance.HasBonusGoldBalance)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BonusGoldBalance);
			}
			if (instance.HasGoldCap)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GoldCap);
			}
			if (instance.HasGoldCapWarning)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GoldCapWarning);
			}
			if (instance.HasCurrencyVersion)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrencyVersion);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasArcaneDustBalance)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ArcaneDustBalance);
			}
			if (HasCappedGoldBalance)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CappedGoldBalance);
			}
			if (HasBonusGoldBalance)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BonusGoldBalance);
			}
			if (HasGoldCap)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GoldCap);
			}
			if (HasGoldCapWarning)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GoldCapWarning);
			}
			if (HasCurrencyVersion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CurrencyVersion);
			}
			return num;
		}
	}
}
