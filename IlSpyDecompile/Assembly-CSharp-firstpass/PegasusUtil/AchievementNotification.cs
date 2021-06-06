using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class AchievementNotification : IProtoBuf
	{
		public bool HasStartDateLocal;

		private Date _StartDateLocal;

		public bool HasEndDateLocal;

		private Date _EndDateLocal;

		public bool HasIntervalRewardCount;

		private int _IntervalRewardCount;

		public long PlayerId { get; set; }

		public long AchievementId { get; set; }

		public int Amount { get; set; }

		public int Quota { get; set; }

		public Date StartDateLocal
		{
			get
			{
				return _StartDateLocal;
			}
			set
			{
				_StartDateLocal = value;
				HasStartDateLocal = value != null;
			}
		}

		public Date EndDateLocal
		{
			get
			{
				return _EndDateLocal;
			}
			set
			{
				_EndDateLocal = value;
				HasEndDateLocal = value != null;
			}
		}

		public bool Complete { get; set; }

		public bool NewAchievement { get; set; }

		public bool RemoveAchievement { get; set; }

		public int IntervalRewardCount
		{
			get
			{
				return _IntervalRewardCount;
			}
			set
			{
				_IntervalRewardCount = value;
				HasIntervalRewardCount = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= PlayerId.GetHashCode();
			hashCode ^= AchievementId.GetHashCode();
			hashCode ^= Amount.GetHashCode();
			hashCode ^= Quota.GetHashCode();
			if (HasStartDateLocal)
			{
				hashCode ^= StartDateLocal.GetHashCode();
			}
			if (HasEndDateLocal)
			{
				hashCode ^= EndDateLocal.GetHashCode();
			}
			hashCode ^= Complete.GetHashCode();
			hashCode ^= NewAchievement.GetHashCode();
			hashCode ^= RemoveAchievement.GetHashCode();
			if (HasIntervalRewardCount)
			{
				hashCode ^= IntervalRewardCount.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			AchievementNotification achievementNotification = obj as AchievementNotification;
			if (achievementNotification == null)
			{
				return false;
			}
			if (!PlayerId.Equals(achievementNotification.PlayerId))
			{
				return false;
			}
			if (!AchievementId.Equals(achievementNotification.AchievementId))
			{
				return false;
			}
			if (!Amount.Equals(achievementNotification.Amount))
			{
				return false;
			}
			if (!Quota.Equals(achievementNotification.Quota))
			{
				return false;
			}
			if (HasStartDateLocal != achievementNotification.HasStartDateLocal || (HasStartDateLocal && !StartDateLocal.Equals(achievementNotification.StartDateLocal)))
			{
				return false;
			}
			if (HasEndDateLocal != achievementNotification.HasEndDateLocal || (HasEndDateLocal && !EndDateLocal.Equals(achievementNotification.EndDateLocal)))
			{
				return false;
			}
			if (!Complete.Equals(achievementNotification.Complete))
			{
				return false;
			}
			if (!NewAchievement.Equals(achievementNotification.NewAchievement))
			{
				return false;
			}
			if (!RemoveAchievement.Equals(achievementNotification.RemoveAchievement))
			{
				return false;
			}
			if (HasIntervalRewardCount != achievementNotification.HasIntervalRewardCount || (HasIntervalRewardCount && !IntervalRewardCount.Equals(achievementNotification.IntervalRewardCount)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AchievementNotification Deserialize(Stream stream, AchievementNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AchievementNotification DeserializeLengthDelimited(Stream stream)
		{
			AchievementNotification achievementNotification = new AchievementNotification();
			DeserializeLengthDelimited(stream, achievementNotification);
			return achievementNotification;
		}

		public static AchievementNotification DeserializeLengthDelimited(Stream stream, AchievementNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AchievementNotification Deserialize(Stream stream, AchievementNotification instance, long limit)
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
					instance.PlayerId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.AchievementId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Amount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Quota = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					if (instance.StartDateLocal == null)
					{
						instance.StartDateLocal = Date.DeserializeLengthDelimited(stream);
					}
					else
					{
						Date.DeserializeLengthDelimited(stream, instance.StartDateLocal);
					}
					continue;
				case 50:
					if (instance.EndDateLocal == null)
					{
						instance.EndDateLocal = Date.DeserializeLengthDelimited(stream);
					}
					else
					{
						Date.DeserializeLengthDelimited(stream, instance.EndDateLocal);
					}
					continue;
				case 56:
					instance.Complete = ProtocolParser.ReadBool(stream);
					continue;
				case 64:
					instance.NewAchievement = ProtocolParser.ReadBool(stream);
					continue;
				case 72:
					instance.RemoveAchievement = ProtocolParser.ReadBool(stream);
					continue;
				case 80:
					instance.IntervalRewardCount = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AchievementNotification instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.AchievementId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Amount);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Quota);
			if (instance.HasStartDateLocal)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.StartDateLocal.GetSerializedSize());
				Date.Serialize(stream, instance.StartDateLocal);
			}
			if (instance.HasEndDateLocal)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.EndDateLocal.GetSerializedSize());
				Date.Serialize(stream, instance.EndDateLocal);
			}
			stream.WriteByte(56);
			ProtocolParser.WriteBool(stream, instance.Complete);
			stream.WriteByte(64);
			ProtocolParser.WriteBool(stream, instance.NewAchievement);
			stream.WriteByte(72);
			ProtocolParser.WriteBool(stream, instance.RemoveAchievement);
			if (instance.HasIntervalRewardCount)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.IntervalRewardCount);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)PlayerId);
			num += ProtocolParser.SizeOfUInt64((ulong)AchievementId);
			num += ProtocolParser.SizeOfUInt64((ulong)Amount);
			num += ProtocolParser.SizeOfUInt64((ulong)Quota);
			if (HasStartDateLocal)
			{
				num++;
				uint serializedSize = StartDateLocal.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasEndDateLocal)
			{
				num++;
				uint serializedSize2 = EndDateLocal.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			num++;
			num++;
			num++;
			if (HasIntervalRewardCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)IntervalRewardCount);
			}
			return num + 7;
		}
	}
}
