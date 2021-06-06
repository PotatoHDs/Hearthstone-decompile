using System.IO;
using PegasusShared;

namespace PegasusGame
{
	public class AchievementProgress : IProtoBuf
	{
		public enum PacketID
		{
			ID = 52
		}

		public bool HasAchievementId;

		private int _AchievementId;

		public bool HasOpType;

		private ProgOpType _OpType;

		public bool HasAmount;

		private int _Amount;

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

		public ProgOpType OpType
		{
			get
			{
				return _OpType;
			}
			set
			{
				_OpType = value;
				HasOpType = true;
			}
		}

		public int Amount
		{
			get
			{
				return _Amount;
			}
			set
			{
				_Amount = value;
				HasAmount = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAchievementId)
			{
				num ^= AchievementId.GetHashCode();
			}
			if (HasOpType)
			{
				num ^= OpType.GetHashCode();
			}
			if (HasAmount)
			{
				num ^= Amount.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AchievementProgress achievementProgress = obj as AchievementProgress;
			if (achievementProgress == null)
			{
				return false;
			}
			if (HasAchievementId != achievementProgress.HasAchievementId || (HasAchievementId && !AchievementId.Equals(achievementProgress.AchievementId)))
			{
				return false;
			}
			if (HasOpType != achievementProgress.HasOpType || (HasOpType && !OpType.Equals(achievementProgress.OpType)))
			{
				return false;
			}
			if (HasAmount != achievementProgress.HasAmount || (HasAmount && !Amount.Equals(achievementProgress.Amount)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AchievementProgress Deserialize(Stream stream, AchievementProgress instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AchievementProgress DeserializeLengthDelimited(Stream stream)
		{
			AchievementProgress achievementProgress = new AchievementProgress();
			DeserializeLengthDelimited(stream, achievementProgress);
			return achievementProgress;
		}

		public static AchievementProgress DeserializeLengthDelimited(Stream stream, AchievementProgress instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AchievementProgress Deserialize(Stream stream, AchievementProgress instance, long limit)
		{
			instance.OpType = ProgOpType.PROG_OP_NONE;
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
					instance.OpType = (ProgOpType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Amount = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AchievementProgress instance)
		{
			if (instance.HasAchievementId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AchievementId);
			}
			if (instance.HasOpType)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.OpType);
			}
			if (instance.HasAmount)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Amount);
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
			if (HasOpType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)OpType);
			}
			if (HasAmount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Amount);
			}
			return num;
		}
	}
}
