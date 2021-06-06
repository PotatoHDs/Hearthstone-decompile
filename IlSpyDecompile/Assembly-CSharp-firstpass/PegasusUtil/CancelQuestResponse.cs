using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class CancelQuestResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 282
		}

		public bool HasNextQuestCancel;

		private Date _NextQuestCancel;

		public int QuestId { get; set; }

		public bool Success { get; set; }

		public Date NextQuestCancel
		{
			get
			{
				return _NextQuestCancel;
			}
			set
			{
				_NextQuestCancel = value;
				HasNextQuestCancel = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= QuestId.GetHashCode();
			hashCode ^= Success.GetHashCode();
			if (HasNextQuestCancel)
			{
				hashCode ^= NextQuestCancel.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			CancelQuestResponse cancelQuestResponse = obj as CancelQuestResponse;
			if (cancelQuestResponse == null)
			{
				return false;
			}
			if (!QuestId.Equals(cancelQuestResponse.QuestId))
			{
				return false;
			}
			if (!Success.Equals(cancelQuestResponse.Success))
			{
				return false;
			}
			if (HasNextQuestCancel != cancelQuestResponse.HasNextQuestCancel || (HasNextQuestCancel && !NextQuestCancel.Equals(cancelQuestResponse.NextQuestCancel)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CancelQuestResponse Deserialize(Stream stream, CancelQuestResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CancelQuestResponse DeserializeLengthDelimited(Stream stream)
		{
			CancelQuestResponse cancelQuestResponse = new CancelQuestResponse();
			DeserializeLengthDelimited(stream, cancelQuestResponse);
			return cancelQuestResponse;
		}

		public static CancelQuestResponse DeserializeLengthDelimited(Stream stream, CancelQuestResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CancelQuestResponse Deserialize(Stream stream, CancelQuestResponse instance, long limit)
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
					instance.QuestId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Success = ProtocolParser.ReadBool(stream);
					continue;
				case 26:
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

		public static void Serialize(Stream stream, CancelQuestResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.QuestId);
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.Success);
			if (instance.HasNextQuestCancel)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.NextQuestCancel.GetSerializedSize());
				Date.Serialize(stream, instance.NextQuestCancel);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)QuestId);
			num++;
			if (HasNextQuestCancel)
			{
				num++;
				uint serializedSize = NextQuestCancel.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 2;
		}
	}
}
