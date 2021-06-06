using System.IO;

namespace PegasusGame
{
	public class BattlegroundsRatingChange : IProtoBuf
	{
		public enum PacketID
		{
			ID = 34
		}

		public bool HasNewRating;

		private int _NewRating;

		public bool HasRatingChange;

		private int _RatingChange;

		public int NewRating
		{
			get
			{
				return _NewRating;
			}
			set
			{
				_NewRating = value;
				HasNewRating = true;
			}
		}

		public int RatingChange
		{
			get
			{
				return _RatingChange;
			}
			set
			{
				_RatingChange = value;
				HasRatingChange = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasNewRating)
			{
				num ^= NewRating.GetHashCode();
			}
			if (HasRatingChange)
			{
				num ^= RatingChange.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BattlegroundsRatingChange battlegroundsRatingChange = obj as BattlegroundsRatingChange;
			if (battlegroundsRatingChange == null)
			{
				return false;
			}
			if (HasNewRating != battlegroundsRatingChange.HasNewRating || (HasNewRating && !NewRating.Equals(battlegroundsRatingChange.NewRating)))
			{
				return false;
			}
			if (HasRatingChange != battlegroundsRatingChange.HasRatingChange || (HasRatingChange && !RatingChange.Equals(battlegroundsRatingChange.RatingChange)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BattlegroundsRatingChange Deserialize(Stream stream, BattlegroundsRatingChange instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BattlegroundsRatingChange DeserializeLengthDelimited(Stream stream)
		{
			BattlegroundsRatingChange battlegroundsRatingChange = new BattlegroundsRatingChange();
			DeserializeLengthDelimited(stream, battlegroundsRatingChange);
			return battlegroundsRatingChange;
		}

		public static BattlegroundsRatingChange DeserializeLengthDelimited(Stream stream, BattlegroundsRatingChange instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BattlegroundsRatingChange Deserialize(Stream stream, BattlegroundsRatingChange instance, long limit)
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
					instance.NewRating = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.RatingChange = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, BattlegroundsRatingChange instance)
		{
			if (instance.HasNewRating)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.NewRating);
			}
			if (instance.HasRatingChange)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RatingChange);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasNewRating)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)NewRating);
			}
			if (HasRatingChange)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RatingChange);
			}
			return num;
		}
	}
}
