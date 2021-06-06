using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class AchievementNotifications : IProtoBuf
	{
		private List<AchievementNotification> _AchievementNotifications_ = new List<AchievementNotification>();

		public List<AchievementNotification> AchievementNotifications_
		{
			get
			{
				return _AchievementNotifications_;
			}
			set
			{
				_AchievementNotifications_ = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (AchievementNotification item in AchievementNotifications_)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AchievementNotifications achievementNotifications = obj as AchievementNotifications;
			if (achievementNotifications == null)
			{
				return false;
			}
			if (AchievementNotifications_.Count != achievementNotifications.AchievementNotifications_.Count)
			{
				return false;
			}
			for (int i = 0; i < AchievementNotifications_.Count; i++)
			{
				if (!AchievementNotifications_[i].Equals(achievementNotifications.AchievementNotifications_[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AchievementNotifications Deserialize(Stream stream, AchievementNotifications instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AchievementNotifications DeserializeLengthDelimited(Stream stream)
		{
			AchievementNotifications achievementNotifications = new AchievementNotifications();
			DeserializeLengthDelimited(stream, achievementNotifications);
			return achievementNotifications;
		}

		public static AchievementNotifications DeserializeLengthDelimited(Stream stream, AchievementNotifications instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AchievementNotifications Deserialize(Stream stream, AchievementNotifications instance, long limit)
		{
			if (instance.AchievementNotifications_ == null)
			{
				instance.AchievementNotifications_ = new List<AchievementNotification>();
			}
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
				case 10:
					instance.AchievementNotifications_.Add(AchievementNotification.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, AchievementNotifications instance)
		{
			if (instance.AchievementNotifications_.Count <= 0)
			{
				return;
			}
			foreach (AchievementNotification item in instance.AchievementNotifications_)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				AchievementNotification.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (AchievementNotifications_.Count > 0)
			{
				foreach (AchievementNotification item in AchievementNotifications_)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
