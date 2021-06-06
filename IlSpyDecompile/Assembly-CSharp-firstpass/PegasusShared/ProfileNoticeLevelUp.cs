using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeLevelUp : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 0xF
		}

		public bool HasTotalLevel;

		private int _TotalLevel;

		public int HeroClass { get; set; }

		public int NewLevel { get; set; }

		public int TotalLevel
		{
			get
			{
				return _TotalLevel;
			}
			set
			{
				_TotalLevel = value;
				HasTotalLevel = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= HeroClass.GetHashCode();
			hashCode ^= NewLevel.GetHashCode();
			if (HasTotalLevel)
			{
				hashCode ^= TotalLevel.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeLevelUp profileNoticeLevelUp = obj as ProfileNoticeLevelUp;
			if (profileNoticeLevelUp == null)
			{
				return false;
			}
			if (!HeroClass.Equals(profileNoticeLevelUp.HeroClass))
			{
				return false;
			}
			if (!NewLevel.Equals(profileNoticeLevelUp.NewLevel))
			{
				return false;
			}
			if (HasTotalLevel != profileNoticeLevelUp.HasTotalLevel || (HasTotalLevel && !TotalLevel.Equals(profileNoticeLevelUp.TotalLevel)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeLevelUp Deserialize(Stream stream, ProfileNoticeLevelUp instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeLevelUp DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeLevelUp profileNoticeLevelUp = new ProfileNoticeLevelUp();
			DeserializeLengthDelimited(stream, profileNoticeLevelUp);
			return profileNoticeLevelUp;
		}

		public static ProfileNoticeLevelUp DeserializeLengthDelimited(Stream stream, ProfileNoticeLevelUp instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeLevelUp Deserialize(Stream stream, ProfileNoticeLevelUp instance, long limit)
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
					instance.HeroClass = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.NewLevel = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.TotalLevel = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeLevelUp instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.HeroClass);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.NewLevel);
			if (instance.HasTotalLevel)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TotalLevel);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)HeroClass);
			num += ProtocolParser.SizeOfUInt64((ulong)NewLevel);
			if (HasTotalLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TotalLevel);
			}
			return num + 2;
		}
	}
}
