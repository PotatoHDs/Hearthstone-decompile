using System.IO;

namespace PegasusUtil
{
	public class AckWingProgress : IProtoBuf
	{
		public enum PacketID
		{
			ID = 308,
			System = 0
		}

		public int Wing { get; set; }

		public int Ack { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Wing.GetHashCode() ^ Ack.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			AckWingProgress ackWingProgress = obj as AckWingProgress;
			if (ackWingProgress == null)
			{
				return false;
			}
			if (!Wing.Equals(ackWingProgress.Wing))
			{
				return false;
			}
			if (!Ack.Equals(ackWingProgress.Ack))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AckWingProgress Deserialize(Stream stream, AckWingProgress instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AckWingProgress DeserializeLengthDelimited(Stream stream)
		{
			AckWingProgress ackWingProgress = new AckWingProgress();
			DeserializeLengthDelimited(stream, ackWingProgress);
			return ackWingProgress;
		}

		public static AckWingProgress DeserializeLengthDelimited(Stream stream, AckWingProgress instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AckWingProgress Deserialize(Stream stream, AckWingProgress instance, long limit)
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
					instance.Wing = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Ack = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AckWingProgress instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Wing);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Ack);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Wing) + ProtocolParser.SizeOfUInt64((ulong)Ack) + 2;
		}
	}
}
