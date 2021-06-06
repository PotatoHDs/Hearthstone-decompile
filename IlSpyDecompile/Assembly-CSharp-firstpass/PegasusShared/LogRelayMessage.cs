using System.IO;
using System.Text;

namespace PegasusShared
{
	public class LogRelayMessage : IProtoBuf
	{
		public bool HasLog;

		private string _Log;

		public bool HasEvent;

		private string _Event;

		public bool HasMessage;

		private string _Message;

		public bool HasTimestamp;

		private long _Timestamp;

		public bool HasSeverity;

		private int _Severity;

		public string Log
		{
			get
			{
				return _Log;
			}
			set
			{
				_Log = value;
				HasLog = value != null;
			}
		}

		public string Event
		{
			get
			{
				return _Event;
			}
			set
			{
				_Event = value;
				HasEvent = value != null;
			}
		}

		public string Message
		{
			get
			{
				return _Message;
			}
			set
			{
				_Message = value;
				HasMessage = value != null;
			}
		}

		public long Timestamp
		{
			get
			{
				return _Timestamp;
			}
			set
			{
				_Timestamp = value;
				HasTimestamp = true;
			}
		}

		public int Severity
		{
			get
			{
				return _Severity;
			}
			set
			{
				_Severity = value;
				HasSeverity = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasLog)
			{
				num ^= Log.GetHashCode();
			}
			if (HasEvent)
			{
				num ^= Event.GetHashCode();
			}
			if (HasMessage)
			{
				num ^= Message.GetHashCode();
			}
			if (HasTimestamp)
			{
				num ^= Timestamp.GetHashCode();
			}
			if (HasSeverity)
			{
				num ^= Severity.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			LogRelayMessage logRelayMessage = obj as LogRelayMessage;
			if (logRelayMessage == null)
			{
				return false;
			}
			if (HasLog != logRelayMessage.HasLog || (HasLog && !Log.Equals(logRelayMessage.Log)))
			{
				return false;
			}
			if (HasEvent != logRelayMessage.HasEvent || (HasEvent && !Event.Equals(logRelayMessage.Event)))
			{
				return false;
			}
			if (HasMessage != logRelayMessage.HasMessage || (HasMessage && !Message.Equals(logRelayMessage.Message)))
			{
				return false;
			}
			if (HasTimestamp != logRelayMessage.HasTimestamp || (HasTimestamp && !Timestamp.Equals(logRelayMessage.Timestamp)))
			{
				return false;
			}
			if (HasSeverity != logRelayMessage.HasSeverity || (HasSeverity && !Severity.Equals(logRelayMessage.Severity)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static LogRelayMessage Deserialize(Stream stream, LogRelayMessage instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static LogRelayMessage DeserializeLengthDelimited(Stream stream)
		{
			LogRelayMessage logRelayMessage = new LogRelayMessage();
			DeserializeLengthDelimited(stream, logRelayMessage);
			return logRelayMessage;
		}

		public static LogRelayMessage DeserializeLengthDelimited(Stream stream, LogRelayMessage instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static LogRelayMessage Deserialize(Stream stream, LogRelayMessage instance, long limit)
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
					instance.Log = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.Event = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.Message = ProtocolParser.ReadString(stream);
					continue;
				case 32:
					instance.Timestamp = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.Severity = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, LogRelayMessage instance)
		{
			if (instance.HasLog)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Log));
			}
			if (instance.HasEvent)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Event));
			}
			if (instance.HasMessage)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Message));
			}
			if (instance.HasTimestamp)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Timestamp);
			}
			if (instance.HasSeverity)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Severity);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasLog)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Log);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasEvent)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Event);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasMessage)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(Message);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasTimestamp)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Timestamp);
			}
			if (HasSeverity)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Severity);
			}
			return num;
		}
	}
}
