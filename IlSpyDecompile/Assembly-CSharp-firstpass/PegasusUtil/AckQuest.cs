using System.IO;

namespace PegasusUtil
{
	public class AckQuest : IProtoBuf
	{
		public enum PacketID
		{
			ID = 604,
			System = 0
		}

		public bool HasQuestId;

		private int _QuestId;

		public int QuestId
		{
			get
			{
				return _QuestId;
			}
			set
			{
				_QuestId = value;
				HasQuestId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasQuestId)
			{
				num ^= QuestId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AckQuest ackQuest = obj as AckQuest;
			if (ackQuest == null)
			{
				return false;
			}
			if (HasQuestId != ackQuest.HasQuestId || (HasQuestId && !QuestId.Equals(ackQuest.QuestId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AckQuest Deserialize(Stream stream, AckQuest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AckQuest DeserializeLengthDelimited(Stream stream)
		{
			AckQuest ackQuest = new AckQuest();
			DeserializeLengthDelimited(stream, ackQuest);
			return ackQuest;
		}

		public static AckQuest DeserializeLengthDelimited(Stream stream, AckQuest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AckQuest Deserialize(Stream stream, AckQuest instance, long limit)
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

		public static void Serialize(Stream stream, AckQuest instance)
		{
			if (instance.HasQuestId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.QuestId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasQuestId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)QuestId);
			}
			return num;
		}
	}
}
