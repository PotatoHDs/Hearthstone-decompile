using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	public class RewardTrackUnclaimedRewards : IProtoBuf
	{
		public bool HasRewardTrackId;

		private int _RewardTrackId;

		private List<PlayerRewardTrackLevelState> _UnclaimedLevel = new List<PlayerRewardTrackLevelState>();

		public int RewardTrackId
		{
			get
			{
				return _RewardTrackId;
			}
			set
			{
				_RewardTrackId = value;
				HasRewardTrackId = true;
			}
		}

		public List<PlayerRewardTrackLevelState> UnclaimedLevel
		{
			get
			{
				return _UnclaimedLevel;
			}
			set
			{
				_UnclaimedLevel = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRewardTrackId)
			{
				num ^= RewardTrackId.GetHashCode();
			}
			foreach (PlayerRewardTrackLevelState item in UnclaimedLevel)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RewardTrackUnclaimedRewards rewardTrackUnclaimedRewards = obj as RewardTrackUnclaimedRewards;
			if (rewardTrackUnclaimedRewards == null)
			{
				return false;
			}
			if (HasRewardTrackId != rewardTrackUnclaimedRewards.HasRewardTrackId || (HasRewardTrackId && !RewardTrackId.Equals(rewardTrackUnclaimedRewards.RewardTrackId)))
			{
				return false;
			}
			if (UnclaimedLevel.Count != rewardTrackUnclaimedRewards.UnclaimedLevel.Count)
			{
				return false;
			}
			for (int i = 0; i < UnclaimedLevel.Count; i++)
			{
				if (!UnclaimedLevel[i].Equals(rewardTrackUnclaimedRewards.UnclaimedLevel[i]))
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

		public static RewardTrackUnclaimedRewards Deserialize(Stream stream, RewardTrackUnclaimedRewards instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RewardTrackUnclaimedRewards DeserializeLengthDelimited(Stream stream)
		{
			RewardTrackUnclaimedRewards rewardTrackUnclaimedRewards = new RewardTrackUnclaimedRewards();
			DeserializeLengthDelimited(stream, rewardTrackUnclaimedRewards);
			return rewardTrackUnclaimedRewards;
		}

		public static RewardTrackUnclaimedRewards DeserializeLengthDelimited(Stream stream, RewardTrackUnclaimedRewards instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RewardTrackUnclaimedRewards Deserialize(Stream stream, RewardTrackUnclaimedRewards instance, long limit)
		{
			if (instance.UnclaimedLevel == null)
			{
				instance.UnclaimedLevel = new List<PlayerRewardTrackLevelState>();
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
					instance.RewardTrackId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.UnclaimedLevel.Add(PlayerRewardTrackLevelState.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, RewardTrackUnclaimedRewards instance)
		{
			if (instance.HasRewardTrackId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardTrackId);
			}
			if (instance.UnclaimedLevel.Count <= 0)
			{
				return;
			}
			foreach (PlayerRewardTrackLevelState item in instance.UnclaimedLevel)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				PlayerRewardTrackLevelState.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRewardTrackId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RewardTrackId);
			}
			if (UnclaimedLevel.Count > 0)
			{
				foreach (PlayerRewardTrackLevelState item in UnclaimedLevel)
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
