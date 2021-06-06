using System;
using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeLeaguePromotionRewards : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 21
		}

		public RewardChest RewardChest { get; set; }

		public int LeagueId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ RewardChest.GetHashCode() ^ LeagueId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeLeaguePromotionRewards profileNoticeLeaguePromotionRewards = obj as ProfileNoticeLeaguePromotionRewards;
			if (profileNoticeLeaguePromotionRewards == null)
			{
				return false;
			}
			if (!RewardChest.Equals(profileNoticeLeaguePromotionRewards.RewardChest))
			{
				return false;
			}
			if (!LeagueId.Equals(profileNoticeLeaguePromotionRewards.LeagueId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeLeaguePromotionRewards Deserialize(Stream stream, ProfileNoticeLeaguePromotionRewards instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeLeaguePromotionRewards DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeLeaguePromotionRewards profileNoticeLeaguePromotionRewards = new ProfileNoticeLeaguePromotionRewards();
			DeserializeLengthDelimited(stream, profileNoticeLeaguePromotionRewards);
			return profileNoticeLeaguePromotionRewards;
		}

		public static ProfileNoticeLeaguePromotionRewards DeserializeLengthDelimited(Stream stream, ProfileNoticeLeaguePromotionRewards instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeLeaguePromotionRewards Deserialize(Stream stream, ProfileNoticeLeaguePromotionRewards instance, long limit)
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
				case 10:
					if (instance.RewardChest == null)
					{
						instance.RewardChest = RewardChest.DeserializeLengthDelimited(stream);
					}
					else
					{
						RewardChest.DeserializeLengthDelimited(stream, instance.RewardChest);
					}
					continue;
				case 16:
					instance.LeagueId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeLeaguePromotionRewards instance)
		{
			if (instance.RewardChest == null)
			{
				throw new ArgumentNullException("RewardChest", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.RewardChest.GetSerializedSize());
			RewardChest.Serialize(stream, instance.RewardChest);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.LeagueId);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = RewardChest.GetSerializedSize();
			return 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + ProtocolParser.SizeOfUInt64((ulong)LeagueId) + 2;
		}
	}
}
