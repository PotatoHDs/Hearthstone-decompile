using System;
using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeTavernBrawlRewards : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 17
		}

		public bool HasBrawlMode;

		private TavernBrawlMode _BrawlMode;

		public RewardChest RewardChest { get; set; }

		public int NumWins { get; set; }

		public TavernBrawlMode BrawlMode
		{
			get
			{
				return _BrawlMode;
			}
			set
			{
				_BrawlMode = value;
				HasBrawlMode = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= RewardChest.GetHashCode();
			hashCode ^= NumWins.GetHashCode();
			if (HasBrawlMode)
			{
				hashCode ^= BrawlMode.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeTavernBrawlRewards profileNoticeTavernBrawlRewards = obj as ProfileNoticeTavernBrawlRewards;
			if (profileNoticeTavernBrawlRewards == null)
			{
				return false;
			}
			if (!RewardChest.Equals(profileNoticeTavernBrawlRewards.RewardChest))
			{
				return false;
			}
			if (!NumWins.Equals(profileNoticeTavernBrawlRewards.NumWins))
			{
				return false;
			}
			if (HasBrawlMode != profileNoticeTavernBrawlRewards.HasBrawlMode || (HasBrawlMode && !BrawlMode.Equals(profileNoticeTavernBrawlRewards.BrawlMode)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeTavernBrawlRewards Deserialize(Stream stream, ProfileNoticeTavernBrawlRewards instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeTavernBrawlRewards DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeTavernBrawlRewards profileNoticeTavernBrawlRewards = new ProfileNoticeTavernBrawlRewards();
			DeserializeLengthDelimited(stream, profileNoticeTavernBrawlRewards);
			return profileNoticeTavernBrawlRewards;
		}

		public static ProfileNoticeTavernBrawlRewards DeserializeLengthDelimited(Stream stream, ProfileNoticeTavernBrawlRewards instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeTavernBrawlRewards Deserialize(Stream stream, ProfileNoticeTavernBrawlRewards instance, long limit)
		{
			instance.BrawlMode = TavernBrawlMode.TB_MODE_NORMAL;
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
					instance.NumWins = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.BrawlMode = (TavernBrawlMode)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeTavernBrawlRewards instance)
		{
			if (instance.RewardChest == null)
			{
				throw new ArgumentNullException("RewardChest", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.RewardChest.GetSerializedSize());
			RewardChest.Serialize(stream, instance.RewardChest);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.NumWins);
			if (instance.HasBrawlMode)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BrawlMode);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = RewardChest.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt64((ulong)NumWins);
			if (HasBrawlMode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BrawlMode);
			}
			return num + 2;
		}
	}
}
