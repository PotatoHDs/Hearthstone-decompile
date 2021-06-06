using System.IO;

namespace PegasusUtil
{
	public class AckAchieveProgress : IProtoBuf
	{
		public enum PacketID
		{
			ID = 243,
			System = 0
		}

		public int Id { get; set; }

		public int AckProgress { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Id.GetHashCode() ^ AckProgress.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			AckAchieveProgress ackAchieveProgress = obj as AckAchieveProgress;
			if (ackAchieveProgress == null)
			{
				return false;
			}
			if (!Id.Equals(ackAchieveProgress.Id))
			{
				return false;
			}
			if (!AckProgress.Equals(ackAchieveProgress.AckProgress))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AckAchieveProgress Deserialize(Stream stream, AckAchieveProgress instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AckAchieveProgress DeserializeLengthDelimited(Stream stream)
		{
			AckAchieveProgress ackAchieveProgress = new AckAchieveProgress();
			DeserializeLengthDelimited(stream, ackAchieveProgress);
			return ackAchieveProgress;
		}

		public static AckAchieveProgress DeserializeLengthDelimited(Stream stream, AckAchieveProgress instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AckAchieveProgress Deserialize(Stream stream, AckAchieveProgress instance, long limit)
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
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.AckProgress = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AckAchieveProgress instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.AckProgress);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Id) + ProtocolParser.SizeOfUInt64((ulong)AckProgress) + 2;
		}
	}
}
