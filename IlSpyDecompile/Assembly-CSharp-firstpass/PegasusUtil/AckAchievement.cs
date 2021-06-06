using System.IO;

namespace PegasusUtil
{
	public class AckAchievement : IProtoBuf
	{
		public enum PacketID
		{
			ID = 612,
			System = 0
		}

		public bool HasAchievementId;

		private int _AchievementId;

		public int AchievementId
		{
			get
			{
				return _AchievementId;
			}
			set
			{
				_AchievementId = value;
				HasAchievementId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAchievementId)
			{
				num ^= AchievementId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AckAchievement ackAchievement = obj as AckAchievement;
			if (ackAchievement == null)
			{
				return false;
			}
			if (HasAchievementId != ackAchievement.HasAchievementId || (HasAchievementId && !AchievementId.Equals(ackAchievement.AchievementId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AckAchievement Deserialize(Stream stream, AckAchievement instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AckAchievement DeserializeLengthDelimited(Stream stream)
		{
			AckAchievement ackAchievement = new AckAchievement();
			DeserializeLengthDelimited(stream, ackAchievement);
			return ackAchievement;
		}

		public static AckAchievement DeserializeLengthDelimited(Stream stream, AckAchievement instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AckAchievement Deserialize(Stream stream, AckAchievement instance, long limit)
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
					instance.AchievementId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AckAchievement instance)
		{
			if (instance.HasAchievementId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AchievementId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAchievementId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)AchievementId);
			}
			return num;
		}
	}
}
