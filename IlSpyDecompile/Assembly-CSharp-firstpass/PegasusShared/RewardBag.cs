using System.IO;

namespace PegasusShared
{
	public class RewardBag : IProtoBuf
	{
		public bool HasRewardBooster;

		private ProfileNoticeRewardBooster _RewardBooster;

		public bool HasRewardCard;

		private ProfileNoticeRewardCard _RewardCard;

		public bool HasRewardDust;

		private ProfileNoticeRewardDust _RewardDust;

		public bool HasRewardGold;

		private ProfileNoticeRewardCurrency _RewardGold;

		public bool HasRewardCardBack;

		private ProfileNoticeCardBack _RewardCardBack;

		public bool HasRewardArenaTicket;

		private ProfileNoticeRewardForge _RewardArenaTicket;

		public ProfileNoticeRewardBooster RewardBooster
		{
			get
			{
				return _RewardBooster;
			}
			set
			{
				_RewardBooster = value;
				HasRewardBooster = value != null;
			}
		}

		public ProfileNoticeRewardCard RewardCard
		{
			get
			{
				return _RewardCard;
			}
			set
			{
				_RewardCard = value;
				HasRewardCard = value != null;
			}
		}

		public ProfileNoticeRewardDust RewardDust
		{
			get
			{
				return _RewardDust;
			}
			set
			{
				_RewardDust = value;
				HasRewardDust = value != null;
			}
		}

		public ProfileNoticeRewardCurrency RewardGold
		{
			get
			{
				return _RewardGold;
			}
			set
			{
				_RewardGold = value;
				HasRewardGold = value != null;
			}
		}

		public ProfileNoticeCardBack RewardCardBack
		{
			get
			{
				return _RewardCardBack;
			}
			set
			{
				_RewardCardBack = value;
				HasRewardCardBack = value != null;
			}
		}

		public ProfileNoticeRewardForge RewardArenaTicket
		{
			get
			{
				return _RewardArenaTicket;
			}
			set
			{
				_RewardArenaTicket = value;
				HasRewardArenaTicket = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRewardBooster)
			{
				num ^= RewardBooster.GetHashCode();
			}
			if (HasRewardCard)
			{
				num ^= RewardCard.GetHashCode();
			}
			if (HasRewardDust)
			{
				num ^= RewardDust.GetHashCode();
			}
			if (HasRewardGold)
			{
				num ^= RewardGold.GetHashCode();
			}
			if (HasRewardCardBack)
			{
				num ^= RewardCardBack.GetHashCode();
			}
			if (HasRewardArenaTicket)
			{
				num ^= RewardArenaTicket.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RewardBag rewardBag = obj as RewardBag;
			if (rewardBag == null)
			{
				return false;
			}
			if (HasRewardBooster != rewardBag.HasRewardBooster || (HasRewardBooster && !RewardBooster.Equals(rewardBag.RewardBooster)))
			{
				return false;
			}
			if (HasRewardCard != rewardBag.HasRewardCard || (HasRewardCard && !RewardCard.Equals(rewardBag.RewardCard)))
			{
				return false;
			}
			if (HasRewardDust != rewardBag.HasRewardDust || (HasRewardDust && !RewardDust.Equals(rewardBag.RewardDust)))
			{
				return false;
			}
			if (HasRewardGold != rewardBag.HasRewardGold || (HasRewardGold && !RewardGold.Equals(rewardBag.RewardGold)))
			{
				return false;
			}
			if (HasRewardCardBack != rewardBag.HasRewardCardBack || (HasRewardCardBack && !RewardCardBack.Equals(rewardBag.RewardCardBack)))
			{
				return false;
			}
			if (HasRewardArenaTicket != rewardBag.HasRewardArenaTicket || (HasRewardArenaTicket && !RewardArenaTicket.Equals(rewardBag.RewardArenaTicket)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RewardBag Deserialize(Stream stream, RewardBag instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RewardBag DeserializeLengthDelimited(Stream stream)
		{
			RewardBag rewardBag = new RewardBag();
			DeserializeLengthDelimited(stream, rewardBag);
			return rewardBag;
		}

		public static RewardBag DeserializeLengthDelimited(Stream stream, RewardBag instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RewardBag Deserialize(Stream stream, RewardBag instance, long limit)
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
					if (instance.RewardBooster == null)
					{
						instance.RewardBooster = ProfileNoticeRewardBooster.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticeRewardBooster.DeserializeLengthDelimited(stream, instance.RewardBooster);
					}
					continue;
				case 18:
					if (instance.RewardCard == null)
					{
						instance.RewardCard = ProfileNoticeRewardCard.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticeRewardCard.DeserializeLengthDelimited(stream, instance.RewardCard);
					}
					continue;
				case 26:
					if (instance.RewardDust == null)
					{
						instance.RewardDust = ProfileNoticeRewardDust.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticeRewardDust.DeserializeLengthDelimited(stream, instance.RewardDust);
					}
					continue;
				case 34:
					if (instance.RewardGold == null)
					{
						instance.RewardGold = ProfileNoticeRewardCurrency.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticeRewardCurrency.DeserializeLengthDelimited(stream, instance.RewardGold);
					}
					continue;
				case 42:
					if (instance.RewardCardBack == null)
					{
						instance.RewardCardBack = ProfileNoticeCardBack.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticeCardBack.DeserializeLengthDelimited(stream, instance.RewardCardBack);
					}
					continue;
				case 50:
					if (instance.RewardArenaTicket == null)
					{
						instance.RewardArenaTicket = ProfileNoticeRewardForge.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNoticeRewardForge.DeserializeLengthDelimited(stream, instance.RewardArenaTicket);
					}
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

		public static void Serialize(Stream stream, RewardBag instance)
		{
			if (instance.HasRewardBooster)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RewardBooster.GetSerializedSize());
				ProfileNoticeRewardBooster.Serialize(stream, instance.RewardBooster);
			}
			if (instance.HasRewardCard)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.RewardCard.GetSerializedSize());
				ProfileNoticeRewardCard.Serialize(stream, instance.RewardCard);
			}
			if (instance.HasRewardDust)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.RewardDust.GetSerializedSize());
				ProfileNoticeRewardDust.Serialize(stream, instance.RewardDust);
			}
			if (instance.HasRewardGold)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.RewardGold.GetSerializedSize());
				ProfileNoticeRewardCurrency.Serialize(stream, instance.RewardGold);
			}
			if (instance.HasRewardCardBack)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.RewardCardBack.GetSerializedSize());
				ProfileNoticeCardBack.Serialize(stream, instance.RewardCardBack);
			}
			if (instance.HasRewardArenaTicket)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.RewardArenaTicket.GetSerializedSize());
				ProfileNoticeRewardForge.Serialize(stream, instance.RewardArenaTicket);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRewardBooster)
			{
				num++;
				uint serializedSize = RewardBooster.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasRewardCard)
			{
				num++;
				uint serializedSize2 = RewardCard.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasRewardDust)
			{
				num++;
				uint serializedSize3 = RewardDust.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasRewardGold)
			{
				num++;
				uint serializedSize4 = RewardGold.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasRewardCardBack)
			{
				num++;
				uint serializedSize5 = RewardCardBack.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (HasRewardArenaTicket)
			{
				num++;
				uint serializedSize6 = RewardArenaTicket.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			return num;
		}
	}
}
