using System.IO;

namespace PegasusShared
{
	public class AdventureProgress : IProtoBuf
	{
		public enum Flags
		{
			OWNED = 1,
			DEFEAT_HEROIC_MISSION_1 = 2,
			DEFEAT_HEROIC_MISSION_2 = 4,
			DEFEAT_HEROIC_MISSION_3 = 8,
			DEFEAT_HEROIC_MISSION_4 = 0x10,
			DEFEAT_CLASS_CHALLENGE_MISSION_1 = 0x100,
			DEFEAT_CLASS_CHALLENGE_MISSION_2 = 0x200,
			DEFEAT_CLASS_CHALLENGE_MISSION_3 = 0x400
		}

		public bool HasAck;

		private int _Ack;

		public int WingId { get; set; }

		public int Progress { get; set; }

		public int Ack
		{
			get
			{
				return _Ack;
			}
			set
			{
				_Ack = value;
				HasAck = true;
			}
		}

		public ulong Flags_ { get; set; }

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= WingId.GetHashCode();
			hashCode ^= Progress.GetHashCode();
			if (HasAck)
			{
				hashCode ^= Ack.GetHashCode();
			}
			return hashCode ^ Flags_.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			AdventureProgress adventureProgress = obj as AdventureProgress;
			if (adventureProgress == null)
			{
				return false;
			}
			if (!WingId.Equals(adventureProgress.WingId))
			{
				return false;
			}
			if (!Progress.Equals(adventureProgress.Progress))
			{
				return false;
			}
			if (HasAck != adventureProgress.HasAck || (HasAck && !Ack.Equals(adventureProgress.Ack)))
			{
				return false;
			}
			if (!Flags_.Equals(adventureProgress.Flags_))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AdventureProgress Deserialize(Stream stream, AdventureProgress instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AdventureProgress DeserializeLengthDelimited(Stream stream)
		{
			AdventureProgress adventureProgress = new AdventureProgress();
			DeserializeLengthDelimited(stream, adventureProgress);
			return adventureProgress;
		}

		public static AdventureProgress DeserializeLengthDelimited(Stream stream, AdventureProgress instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AdventureProgress Deserialize(Stream stream, AdventureProgress instance, long limit)
		{
			instance.Ack = 0;
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
					instance.WingId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Progress = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Ack = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Flags_ = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AdventureProgress instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.WingId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Progress);
			if (instance.HasAck)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Ack);
			}
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, instance.Flags_);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)WingId);
			num += ProtocolParser.SizeOfUInt64((ulong)Progress);
			if (HasAck)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Ack);
			}
			num += ProtocolParser.SizeOfUInt64(Flags_);
			return num + 3;
		}
	}
}
