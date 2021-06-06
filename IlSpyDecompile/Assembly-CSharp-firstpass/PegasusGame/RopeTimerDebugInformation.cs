using System.IO;

namespace PegasusGame
{
	public class RopeTimerDebugInformation : IProtoBuf
	{
		public enum PacketID
		{
			ID = 8
		}

		public int MicrosecondsRemainingInTurn { get; set; }

		public int BaseMicrosecondsInTurn { get; set; }

		public int SlushTimeInMicroseconds { get; set; }

		public int TotalMicrosecondsInTurn { get; set; }

		public int OpponentSlushTimeInMicroseconds { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ MicrosecondsRemainingInTurn.GetHashCode() ^ BaseMicrosecondsInTurn.GetHashCode() ^ SlushTimeInMicroseconds.GetHashCode() ^ TotalMicrosecondsInTurn.GetHashCode() ^ OpponentSlushTimeInMicroseconds.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			RopeTimerDebugInformation ropeTimerDebugInformation = obj as RopeTimerDebugInformation;
			if (ropeTimerDebugInformation == null)
			{
				return false;
			}
			if (!MicrosecondsRemainingInTurn.Equals(ropeTimerDebugInformation.MicrosecondsRemainingInTurn))
			{
				return false;
			}
			if (!BaseMicrosecondsInTurn.Equals(ropeTimerDebugInformation.BaseMicrosecondsInTurn))
			{
				return false;
			}
			if (!SlushTimeInMicroseconds.Equals(ropeTimerDebugInformation.SlushTimeInMicroseconds))
			{
				return false;
			}
			if (!TotalMicrosecondsInTurn.Equals(ropeTimerDebugInformation.TotalMicrosecondsInTurn))
			{
				return false;
			}
			if (!OpponentSlushTimeInMicroseconds.Equals(ropeTimerDebugInformation.OpponentSlushTimeInMicroseconds))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RopeTimerDebugInformation Deserialize(Stream stream, RopeTimerDebugInformation instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RopeTimerDebugInformation DeserializeLengthDelimited(Stream stream)
		{
			RopeTimerDebugInformation ropeTimerDebugInformation = new RopeTimerDebugInformation();
			DeserializeLengthDelimited(stream, ropeTimerDebugInformation);
			return ropeTimerDebugInformation;
		}

		public static RopeTimerDebugInformation DeserializeLengthDelimited(Stream stream, RopeTimerDebugInformation instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RopeTimerDebugInformation Deserialize(Stream stream, RopeTimerDebugInformation instance, long limit)
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
					instance.MicrosecondsRemainingInTurn = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.BaseMicrosecondsInTurn = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.SlushTimeInMicroseconds = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.TotalMicrosecondsInTurn = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.OpponentSlushTimeInMicroseconds = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, RopeTimerDebugInformation instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.MicrosecondsRemainingInTurn);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.BaseMicrosecondsInTurn);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SlushTimeInMicroseconds);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.TotalMicrosecondsInTurn);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.OpponentSlushTimeInMicroseconds);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)MicrosecondsRemainingInTurn) + ProtocolParser.SizeOfUInt64((ulong)BaseMicrosecondsInTurn) + ProtocolParser.SizeOfUInt64((ulong)SlushTimeInMicroseconds) + ProtocolParser.SizeOfUInt64((ulong)TotalMicrosecondsInTurn) + ProtocolParser.SizeOfUInt64((ulong)OpponentSlushTimeInMicroseconds) + 5;
		}
	}
}
