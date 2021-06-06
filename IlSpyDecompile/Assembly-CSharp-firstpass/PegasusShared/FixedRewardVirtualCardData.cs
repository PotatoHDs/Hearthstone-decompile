using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	public class FixedRewardVirtualCardData : IProtoBuf
	{
		public bool HasAchieveId;

		private int _AchieveId;

		public int FixedRewardMapId { get; set; }

		public DeckCardData CardData { get; set; }

		public string ActionType { get; set; }

		public int AchieveId
		{
			get
			{
				return _AchieveId;
			}
			set
			{
				_AchieveId = value;
				HasAchieveId = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= FixedRewardMapId.GetHashCode();
			hashCode ^= CardData.GetHashCode();
			hashCode ^= ActionType.GetHashCode();
			if (HasAchieveId)
			{
				hashCode ^= AchieveId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			FixedRewardVirtualCardData fixedRewardVirtualCardData = obj as FixedRewardVirtualCardData;
			if (fixedRewardVirtualCardData == null)
			{
				return false;
			}
			if (!FixedRewardMapId.Equals(fixedRewardVirtualCardData.FixedRewardMapId))
			{
				return false;
			}
			if (!CardData.Equals(fixedRewardVirtualCardData.CardData))
			{
				return false;
			}
			if (!ActionType.Equals(fixedRewardVirtualCardData.ActionType))
			{
				return false;
			}
			if (HasAchieveId != fixedRewardVirtualCardData.HasAchieveId || (HasAchieveId && !AchieveId.Equals(fixedRewardVirtualCardData.AchieveId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FixedRewardVirtualCardData Deserialize(Stream stream, FixedRewardVirtualCardData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FixedRewardVirtualCardData DeserializeLengthDelimited(Stream stream)
		{
			FixedRewardVirtualCardData fixedRewardVirtualCardData = new FixedRewardVirtualCardData();
			DeserializeLengthDelimited(stream, fixedRewardVirtualCardData);
			return fixedRewardVirtualCardData;
		}

		public static FixedRewardVirtualCardData DeserializeLengthDelimited(Stream stream, FixedRewardVirtualCardData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FixedRewardVirtualCardData Deserialize(Stream stream, FixedRewardVirtualCardData instance, long limit)
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
					instance.FixedRewardMapId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.CardData == null)
					{
						instance.CardData = DeckCardData.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeckCardData.DeserializeLengthDelimited(stream, instance.CardData);
					}
					continue;
				case 26:
					instance.ActionType = ProtocolParser.ReadString(stream);
					continue;
				case 32:
					instance.AchieveId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, FixedRewardVirtualCardData instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FixedRewardMapId);
			if (instance.CardData == null)
			{
				throw new ArgumentNullException("CardData", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.CardData.GetSerializedSize());
			DeckCardData.Serialize(stream, instance.CardData);
			if (instance.ActionType == null)
			{
				throw new ArgumentNullException("ActionType", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ActionType));
			if (instance.HasAchieveId)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AchieveId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)FixedRewardMapId);
			uint serializedSize = CardData.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(ActionType);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (HasAchieveId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)AchieveId);
			}
			return num + 3;
		}
	}
}
