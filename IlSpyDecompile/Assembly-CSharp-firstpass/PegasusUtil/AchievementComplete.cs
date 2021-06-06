using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class AchievementComplete : IProtoBuf
	{
		public enum PacketID
		{
			ID = 618,
			System = 0
		}

		public bool HasAchievementId;

		private int _AchievementId;

		private List<int> _AchievementIds = new List<int>();

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

		public List<int> AchievementIds
		{
			get
			{
				return _AchievementIds;
			}
			set
			{
				_AchievementIds = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAchievementId)
			{
				num ^= AchievementId.GetHashCode();
			}
			foreach (int achievementId in AchievementIds)
			{
				num ^= achievementId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AchievementComplete achievementComplete = obj as AchievementComplete;
			if (achievementComplete == null)
			{
				return false;
			}
			if (HasAchievementId != achievementComplete.HasAchievementId || (HasAchievementId && !AchievementId.Equals(achievementComplete.AchievementId)))
			{
				return false;
			}
			if (AchievementIds.Count != achievementComplete.AchievementIds.Count)
			{
				return false;
			}
			for (int i = 0; i < AchievementIds.Count; i++)
			{
				if (!AchievementIds[i].Equals(achievementComplete.AchievementIds[i]))
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

		public static AchievementComplete Deserialize(Stream stream, AchievementComplete instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AchievementComplete DeserializeLengthDelimited(Stream stream)
		{
			AchievementComplete achievementComplete = new AchievementComplete();
			DeserializeLengthDelimited(stream, achievementComplete);
			return achievementComplete;
		}

		public static AchievementComplete DeserializeLengthDelimited(Stream stream, AchievementComplete instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AchievementComplete Deserialize(Stream stream, AchievementComplete instance, long limit)
		{
			if (instance.AchievementIds == null)
			{
				instance.AchievementIds = new List<int>();
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
				case 8:
					instance.AchievementId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.AchievementIds.Add((int)ProtocolParser.ReadUInt64(stream));
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

		public static void Serialize(Stream stream, AchievementComplete instance)
		{
			if (instance.HasAchievementId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AchievementId);
			}
			if (instance.AchievementIds.Count <= 0)
			{
				return;
			}
			foreach (int achievementId in instance.AchievementIds)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)achievementId);
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
			if (AchievementIds.Count > 0)
			{
				foreach (int achievementId in AchievementIds)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)achievementId);
				}
				return num;
			}
			return num;
		}
	}
}
