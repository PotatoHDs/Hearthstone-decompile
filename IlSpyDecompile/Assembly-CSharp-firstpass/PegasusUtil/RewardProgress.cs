using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class RewardProgress : IProtoBuf
	{
		public enum PacketID
		{
			ID = 271
		}

		public Date SeasonEnd { get; set; }

		public int SeasonNumber { get; set; }

		public Date NextQuestCancel { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ SeasonEnd.GetHashCode() ^ SeasonNumber.GetHashCode() ^ NextQuestCancel.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			RewardProgress rewardProgress = obj as RewardProgress;
			if (rewardProgress == null)
			{
				return false;
			}
			if (!SeasonEnd.Equals(rewardProgress.SeasonEnd))
			{
				return false;
			}
			if (!SeasonNumber.Equals(rewardProgress.SeasonNumber))
			{
				return false;
			}
			if (!NextQuestCancel.Equals(rewardProgress.NextQuestCancel))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RewardProgress Deserialize(Stream stream, RewardProgress instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RewardProgress DeserializeLengthDelimited(Stream stream)
		{
			RewardProgress rewardProgress = new RewardProgress();
			DeserializeLengthDelimited(stream, rewardProgress);
			return rewardProgress;
		}

		public static RewardProgress DeserializeLengthDelimited(Stream stream, RewardProgress instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RewardProgress Deserialize(Stream stream, RewardProgress instance, long limit)
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
					if (instance.SeasonEnd == null)
					{
						instance.SeasonEnd = Date.DeserializeLengthDelimited(stream);
					}
					else
					{
						Date.DeserializeLengthDelimited(stream, instance.SeasonEnd);
					}
					continue;
				case 40:
					instance.SeasonNumber = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 90:
					if (instance.NextQuestCancel == null)
					{
						instance.NextQuestCancel = Date.DeserializeLengthDelimited(stream);
					}
					else
					{
						Date.DeserializeLengthDelimited(stream, instance.NextQuestCancel);
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

		public static void Serialize(Stream stream, RewardProgress instance)
		{
			if (instance.SeasonEnd == null)
			{
				throw new ArgumentNullException("SeasonEnd", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.SeasonEnd.GetSerializedSize());
			Date.Serialize(stream, instance.SeasonEnd);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SeasonNumber);
			if (instance.NextQuestCancel == null)
			{
				throw new ArgumentNullException("NextQuestCancel", "Required by proto specification.");
			}
			stream.WriteByte(90);
			ProtocolParser.WriteUInt32(stream, instance.NextQuestCancel.GetSerializedSize());
			Date.Serialize(stream, instance.NextQuestCancel);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = SeasonEnd.GetSerializedSize();
			uint num = 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + ProtocolParser.SizeOfUInt64((ulong)SeasonNumber);
			uint serializedSize2 = NextQuestCancel.GetSerializedSize();
			return num + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 3;
		}
	}
}
