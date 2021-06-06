using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameTimeRemainingInfo : IProtoBuf
	{
		public bool HasMinutesRemaining;

		private uint _MinutesRemaining;

		public bool HasParentalDailyMinutesRemaining;

		private uint _ParentalDailyMinutesRemaining;

		public bool HasParentalWeeklyMinutesRemaining;

		private uint _ParentalWeeklyMinutesRemaining;

		public bool HasSecondsRemainingUntilKick;

		private uint _SecondsRemainingUntilKick;

		public uint MinutesRemaining
		{
			get
			{
				return _MinutesRemaining;
			}
			set
			{
				_MinutesRemaining = value;
				HasMinutesRemaining = true;
			}
		}

		public uint ParentalDailyMinutesRemaining
		{
			get
			{
				return _ParentalDailyMinutesRemaining;
			}
			set
			{
				_ParentalDailyMinutesRemaining = value;
				HasParentalDailyMinutesRemaining = true;
			}
		}

		public uint ParentalWeeklyMinutesRemaining
		{
			get
			{
				return _ParentalWeeklyMinutesRemaining;
			}
			set
			{
				_ParentalWeeklyMinutesRemaining = value;
				HasParentalWeeklyMinutesRemaining = true;
			}
		}

		public uint SecondsRemainingUntilKick
		{
			get
			{
				return _SecondsRemainingUntilKick;
			}
			set
			{
				_SecondsRemainingUntilKick = value;
				HasSecondsRemainingUntilKick = true;
			}
		}

		public bool IsInitialized => true;

		public void SetMinutesRemaining(uint val)
		{
			MinutesRemaining = val;
		}

		public void SetParentalDailyMinutesRemaining(uint val)
		{
			ParentalDailyMinutesRemaining = val;
		}

		public void SetParentalWeeklyMinutesRemaining(uint val)
		{
			ParentalWeeklyMinutesRemaining = val;
		}

		public void SetSecondsRemainingUntilKick(uint val)
		{
			SecondsRemainingUntilKick = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMinutesRemaining)
			{
				num ^= MinutesRemaining.GetHashCode();
			}
			if (HasParentalDailyMinutesRemaining)
			{
				num ^= ParentalDailyMinutesRemaining.GetHashCode();
			}
			if (HasParentalWeeklyMinutesRemaining)
			{
				num ^= ParentalWeeklyMinutesRemaining.GetHashCode();
			}
			if (HasSecondsRemainingUntilKick)
			{
				num ^= SecondsRemainingUntilKick.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameTimeRemainingInfo gameTimeRemainingInfo = obj as GameTimeRemainingInfo;
			if (gameTimeRemainingInfo == null)
			{
				return false;
			}
			if (HasMinutesRemaining != gameTimeRemainingInfo.HasMinutesRemaining || (HasMinutesRemaining && !MinutesRemaining.Equals(gameTimeRemainingInfo.MinutesRemaining)))
			{
				return false;
			}
			if (HasParentalDailyMinutesRemaining != gameTimeRemainingInfo.HasParentalDailyMinutesRemaining || (HasParentalDailyMinutesRemaining && !ParentalDailyMinutesRemaining.Equals(gameTimeRemainingInfo.ParentalDailyMinutesRemaining)))
			{
				return false;
			}
			if (HasParentalWeeklyMinutesRemaining != gameTimeRemainingInfo.HasParentalWeeklyMinutesRemaining || (HasParentalWeeklyMinutesRemaining && !ParentalWeeklyMinutesRemaining.Equals(gameTimeRemainingInfo.ParentalWeeklyMinutesRemaining)))
			{
				return false;
			}
			if (HasSecondsRemainingUntilKick != gameTimeRemainingInfo.HasSecondsRemainingUntilKick || (HasSecondsRemainingUntilKick && !SecondsRemainingUntilKick.Equals(gameTimeRemainingInfo.SecondsRemainingUntilKick)))
			{
				return false;
			}
			return true;
		}

		public static GameTimeRemainingInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameTimeRemainingInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameTimeRemainingInfo Deserialize(Stream stream, GameTimeRemainingInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameTimeRemainingInfo DeserializeLengthDelimited(Stream stream)
		{
			GameTimeRemainingInfo gameTimeRemainingInfo = new GameTimeRemainingInfo();
			DeserializeLengthDelimited(stream, gameTimeRemainingInfo);
			return gameTimeRemainingInfo;
		}

		public static GameTimeRemainingInfo DeserializeLengthDelimited(Stream stream, GameTimeRemainingInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameTimeRemainingInfo Deserialize(Stream stream, GameTimeRemainingInfo instance, long limit)
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
					instance.MinutesRemaining = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.ParentalDailyMinutesRemaining = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.ParentalWeeklyMinutesRemaining = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.SecondsRemainingUntilKick = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, GameTimeRemainingInfo instance)
		{
			if (instance.HasMinutesRemaining)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.MinutesRemaining);
			}
			if (instance.HasParentalDailyMinutesRemaining)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.ParentalDailyMinutesRemaining);
			}
			if (instance.HasParentalWeeklyMinutesRemaining)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.ParentalWeeklyMinutesRemaining);
			}
			if (instance.HasSecondsRemainingUntilKick)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.SecondsRemainingUntilKick);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMinutesRemaining)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MinutesRemaining);
			}
			if (HasParentalDailyMinutesRemaining)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ParentalDailyMinutesRemaining);
			}
			if (HasParentalWeeklyMinutesRemaining)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ParentalWeeklyMinutesRemaining);
			}
			if (HasSecondsRemainingUntilKick)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(SecondsRemainingUntilKick);
			}
			return num;
		}
	}
}
