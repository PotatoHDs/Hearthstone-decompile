using System.IO;

namespace PegasusGame
{
	public class PendingEvent : IProtoBuf
	{
		public bool HasData;

		private byte[] _Data;

		public int Type { get; set; }

		public int Command { get; set; }

		public uint ObserverId { get; set; }

		public int PlayerId { get; set; }

		public int DataType { get; set; }

		public byte[] Data
		{
			get
			{
				return _Data;
			}
			set
			{
				_Data = value;
				HasData = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Type.GetHashCode();
			hashCode ^= Command.GetHashCode();
			hashCode ^= ObserverId.GetHashCode();
			hashCode ^= PlayerId.GetHashCode();
			hashCode ^= DataType.GetHashCode();
			if (HasData)
			{
				hashCode ^= Data.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PendingEvent pendingEvent = obj as PendingEvent;
			if (pendingEvent == null)
			{
				return false;
			}
			if (!Type.Equals(pendingEvent.Type))
			{
				return false;
			}
			if (!Command.Equals(pendingEvent.Command))
			{
				return false;
			}
			if (!ObserverId.Equals(pendingEvent.ObserverId))
			{
				return false;
			}
			if (!PlayerId.Equals(pendingEvent.PlayerId))
			{
				return false;
			}
			if (!DataType.Equals(pendingEvent.DataType))
			{
				return false;
			}
			if (HasData != pendingEvent.HasData || (HasData && !Data.Equals(pendingEvent.Data)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PendingEvent Deserialize(Stream stream, PendingEvent instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PendingEvent DeserializeLengthDelimited(Stream stream)
		{
			PendingEvent pendingEvent = new PendingEvent();
			DeserializeLengthDelimited(stream, pendingEvent);
			return pendingEvent;
		}

		public static PendingEvent DeserializeLengthDelimited(Stream stream, PendingEvent instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PendingEvent Deserialize(Stream stream, PendingEvent instance, long limit)
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
					instance.Type = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Command = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.ObserverId = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.DataType = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 50:
					instance.Data = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, PendingEvent instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Type);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Command);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.ObserverId);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DataType);
			if (instance.HasData)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, instance.Data);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Type);
			num += ProtocolParser.SizeOfUInt64((ulong)Command);
			num += ProtocolParser.SizeOfUInt32(ObserverId);
			num += ProtocolParser.SizeOfUInt64((ulong)PlayerId);
			num += ProtocolParser.SizeOfUInt64((ulong)DataType);
			if (HasData)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(Data.Length) + Data.Length);
			}
			return num + 5;
		}
	}
}
