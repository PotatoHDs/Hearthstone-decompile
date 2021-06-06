using System.IO;

namespace PegasusUtil
{
	public class ClaimAchievementReward : IProtoBuf
	{
		public enum PacketID
		{
			ID = 613,
			System = 0
		}

		public bool HasAchievementId;

		private int _AchievementId;

		public bool HasChooseOneRewardItemId;

		private int _ChooseOneRewardItemId;

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

		public int ChooseOneRewardItemId
		{
			get
			{
				return _ChooseOneRewardItemId;
			}
			set
			{
				_ChooseOneRewardItemId = value;
				HasChooseOneRewardItemId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAchievementId)
			{
				num ^= AchievementId.GetHashCode();
			}
			if (HasChooseOneRewardItemId)
			{
				num ^= ChooseOneRewardItemId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ClaimAchievementReward claimAchievementReward = obj as ClaimAchievementReward;
			if (claimAchievementReward == null)
			{
				return false;
			}
			if (HasAchievementId != claimAchievementReward.HasAchievementId || (HasAchievementId && !AchievementId.Equals(claimAchievementReward.AchievementId)))
			{
				return false;
			}
			if (HasChooseOneRewardItemId != claimAchievementReward.HasChooseOneRewardItemId || (HasChooseOneRewardItemId && !ChooseOneRewardItemId.Equals(claimAchievementReward.ChooseOneRewardItemId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ClaimAchievementReward Deserialize(Stream stream, ClaimAchievementReward instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ClaimAchievementReward DeserializeLengthDelimited(Stream stream)
		{
			ClaimAchievementReward claimAchievementReward = new ClaimAchievementReward();
			DeserializeLengthDelimited(stream, claimAchievementReward);
			return claimAchievementReward;
		}

		public static ClaimAchievementReward DeserializeLengthDelimited(Stream stream, ClaimAchievementReward instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ClaimAchievementReward Deserialize(Stream stream, ClaimAchievementReward instance, long limit)
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
				case 16:
					instance.ChooseOneRewardItemId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ClaimAchievementReward instance)
		{
			if (instance.HasAchievementId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AchievementId);
			}
			if (instance.HasChooseOneRewardItemId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ChooseOneRewardItemId);
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
			if (HasChooseOneRewardItemId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ChooseOneRewardItemId);
			}
			return num;
		}
	}
}
