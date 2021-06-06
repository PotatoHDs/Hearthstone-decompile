using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class SpecialEventTiming : IProtoBuf
	{
		public bool HasDeprecatedSecondsTilStart;

		private ulong _DeprecatedSecondsTilStart;

		public bool HasDeprecatedSecondsTilEnd;

		private ulong _DeprecatedSecondsTilEnd;

		public bool HasSecondsToStart;

		private long _SecondsToStart;

		public bool HasSecondsToEnd;

		private long _SecondsToEnd;

		public bool HasEventId;

		private long _EventId;

		public string EventName { get; set; }

		public ulong DeprecatedSecondsTilStart
		{
			get
			{
				return _DeprecatedSecondsTilStart;
			}
			set
			{
				_DeprecatedSecondsTilStart = value;
				HasDeprecatedSecondsTilStart = true;
			}
		}

		public ulong DeprecatedSecondsTilEnd
		{
			get
			{
				return _DeprecatedSecondsTilEnd;
			}
			set
			{
				_DeprecatedSecondsTilEnd = value;
				HasDeprecatedSecondsTilEnd = true;
			}
		}

		public long SecondsToStart
		{
			get
			{
				return _SecondsToStart;
			}
			set
			{
				_SecondsToStart = value;
				HasSecondsToStart = true;
			}
		}

		public long SecondsToEnd
		{
			get
			{
				return _SecondsToEnd;
			}
			set
			{
				_SecondsToEnd = value;
				HasSecondsToEnd = true;
			}
		}

		public long EventId
		{
			get
			{
				return _EventId;
			}
			set
			{
				_EventId = value;
				HasEventId = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= EventName.GetHashCode();
			if (HasDeprecatedSecondsTilStart)
			{
				hashCode ^= DeprecatedSecondsTilStart.GetHashCode();
			}
			if (HasDeprecatedSecondsTilEnd)
			{
				hashCode ^= DeprecatedSecondsTilEnd.GetHashCode();
			}
			if (HasSecondsToStart)
			{
				hashCode ^= SecondsToStart.GetHashCode();
			}
			if (HasSecondsToEnd)
			{
				hashCode ^= SecondsToEnd.GetHashCode();
			}
			if (HasEventId)
			{
				hashCode ^= EventId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			SpecialEventTiming specialEventTiming = obj as SpecialEventTiming;
			if (specialEventTiming == null)
			{
				return false;
			}
			if (!EventName.Equals(specialEventTiming.EventName))
			{
				return false;
			}
			if (HasDeprecatedSecondsTilStart != specialEventTiming.HasDeprecatedSecondsTilStart || (HasDeprecatedSecondsTilStart && !DeprecatedSecondsTilStart.Equals(specialEventTiming.DeprecatedSecondsTilStart)))
			{
				return false;
			}
			if (HasDeprecatedSecondsTilEnd != specialEventTiming.HasDeprecatedSecondsTilEnd || (HasDeprecatedSecondsTilEnd && !DeprecatedSecondsTilEnd.Equals(specialEventTiming.DeprecatedSecondsTilEnd)))
			{
				return false;
			}
			if (HasSecondsToStart != specialEventTiming.HasSecondsToStart || (HasSecondsToStart && !SecondsToStart.Equals(specialEventTiming.SecondsToStart)))
			{
				return false;
			}
			if (HasSecondsToEnd != specialEventTiming.HasSecondsToEnd || (HasSecondsToEnd && !SecondsToEnd.Equals(specialEventTiming.SecondsToEnd)))
			{
				return false;
			}
			if (HasEventId != specialEventTiming.HasEventId || (HasEventId && !EventId.Equals(specialEventTiming.EventId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SpecialEventTiming Deserialize(Stream stream, SpecialEventTiming instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SpecialEventTiming DeserializeLengthDelimited(Stream stream)
		{
			SpecialEventTiming specialEventTiming = new SpecialEventTiming();
			DeserializeLengthDelimited(stream, specialEventTiming);
			return specialEventTiming;
		}

		public static SpecialEventTiming DeserializeLengthDelimited(Stream stream, SpecialEventTiming instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SpecialEventTiming Deserialize(Stream stream, SpecialEventTiming instance, long limit)
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
					instance.EventName = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.DeprecatedSecondsTilStart = ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.DeprecatedSecondsTilEnd = ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.SecondsToStart = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.SecondsToEnd = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.EventId = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SpecialEventTiming instance)
		{
			if (instance.EventName == null)
			{
				throw new ArgumentNullException("EventName", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EventName));
			if (instance.HasDeprecatedSecondsTilStart)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.DeprecatedSecondsTilStart);
			}
			if (instance.HasDeprecatedSecondsTilEnd)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.DeprecatedSecondsTilEnd);
			}
			if (instance.HasSecondsToStart)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SecondsToStart);
			}
			if (instance.HasSecondsToEnd)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SecondsToEnd);
			}
			if (instance.HasEventId)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.EventId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(EventName);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (HasDeprecatedSecondsTilStart)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(DeprecatedSecondsTilStart);
			}
			if (HasDeprecatedSecondsTilEnd)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(DeprecatedSecondsTilEnd);
			}
			if (HasSecondsToStart)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SecondsToStart);
			}
			if (HasSecondsToEnd)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SecondsToEnd);
			}
			if (HasEventId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)EventId);
			}
			return num + 1;
		}
	}
}
