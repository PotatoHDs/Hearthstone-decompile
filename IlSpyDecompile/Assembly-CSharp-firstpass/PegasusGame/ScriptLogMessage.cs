using System.IO;
using System.Text;

namespace PegasusGame
{
	public class ScriptLogMessage : IProtoBuf
	{
		public enum PacketID
		{
			ID = 50
		}

		public bool HasSeverity;

		private int _Severity;

		public bool HasEvent;

		private string _Event;

		public bool HasMessage;

		private string _Message;

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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSeverity)
			{
				num ^= Severity.GetHashCode();
			}
			if (HasEvent)
			{
				num ^= Event.GetHashCode();
			}
			if (HasMessage)
			{
				num ^= Message.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ScriptLogMessage scriptLogMessage = obj as ScriptLogMessage;
			if (scriptLogMessage == null)
			{
				return false;
			}
			if (HasSeverity != scriptLogMessage.HasSeverity || (HasSeverity && !Severity.Equals(scriptLogMessage.Severity)))
			{
				return false;
			}
			if (HasEvent != scriptLogMessage.HasEvent || (HasEvent && !Event.Equals(scriptLogMessage.Event)))
			{
				return false;
			}
			if (HasMessage != scriptLogMessage.HasMessage || (HasMessage && !Message.Equals(scriptLogMessage.Message)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ScriptLogMessage Deserialize(Stream stream, ScriptLogMessage instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ScriptLogMessage DeserializeLengthDelimited(Stream stream)
		{
			ScriptLogMessage scriptLogMessage = new ScriptLogMessage();
			DeserializeLengthDelimited(stream, scriptLogMessage);
			return scriptLogMessage;
		}

		public static ScriptLogMessage DeserializeLengthDelimited(Stream stream, ScriptLogMessage instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ScriptLogMessage Deserialize(Stream stream, ScriptLogMessage instance, long limit)
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
					instance.Severity = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Event = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.Message = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ScriptLogMessage instance)
		{
			if (instance.HasSeverity)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Severity);
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
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSeverity)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Severity);
			}
			if (HasEvent)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Event);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasMessage)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Message);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
