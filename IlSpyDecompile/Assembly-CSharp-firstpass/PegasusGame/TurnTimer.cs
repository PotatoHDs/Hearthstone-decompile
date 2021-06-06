using System.IO;

namespace PegasusGame
{
	public class TurnTimer : IProtoBuf
	{
		public enum PacketID
		{
			ID = 9
		}

		public int Seconds { get; set; }

		public int Turn { get; set; }

		public bool Show { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Seconds.GetHashCode() ^ Turn.GetHashCode() ^ Show.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			TurnTimer turnTimer = obj as TurnTimer;
			if (turnTimer == null)
			{
				return false;
			}
			if (!Seconds.Equals(turnTimer.Seconds))
			{
				return false;
			}
			if (!Turn.Equals(turnTimer.Turn))
			{
				return false;
			}
			if (!Show.Equals(turnTimer.Show))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TurnTimer Deserialize(Stream stream, TurnTimer instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TurnTimer DeserializeLengthDelimited(Stream stream)
		{
			TurnTimer turnTimer = new TurnTimer();
			DeserializeLengthDelimited(stream, turnTimer);
			return turnTimer;
		}

		public static TurnTimer DeserializeLengthDelimited(Stream stream, TurnTimer instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TurnTimer Deserialize(Stream stream, TurnTimer instance, long limit)
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
					instance.Seconds = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Turn = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Show = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, TurnTimer instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Seconds);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Turn);
			stream.WriteByte(24);
			ProtocolParser.WriteBool(stream, instance.Show);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)Seconds) + ProtocolParser.SizeOfUInt64((ulong)Turn) + 1 + 3;
		}
	}
}
