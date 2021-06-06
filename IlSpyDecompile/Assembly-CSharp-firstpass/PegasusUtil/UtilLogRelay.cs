using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class UtilLogRelay : IProtoBuf
	{
		public enum PacketID
		{
			ID = 390
		}

		private List<LogRelayMessage> _Messages = new List<LogRelayMessage>();

		public List<LogRelayMessage> Messages
		{
			get
			{
				return _Messages;
			}
			set
			{
				_Messages = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (LogRelayMessage message in Messages)
			{
				num ^= message.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UtilLogRelay utilLogRelay = obj as UtilLogRelay;
			if (utilLogRelay == null)
			{
				return false;
			}
			if (Messages.Count != utilLogRelay.Messages.Count)
			{
				return false;
			}
			for (int i = 0; i < Messages.Count; i++)
			{
				if (!Messages[i].Equals(utilLogRelay.Messages[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UtilLogRelay Deserialize(Stream stream, UtilLogRelay instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UtilLogRelay DeserializeLengthDelimited(Stream stream)
		{
			UtilLogRelay utilLogRelay = new UtilLogRelay();
			DeserializeLengthDelimited(stream, utilLogRelay);
			return utilLogRelay;
		}

		public static UtilLogRelay DeserializeLengthDelimited(Stream stream, UtilLogRelay instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UtilLogRelay Deserialize(Stream stream, UtilLogRelay instance, long limit)
		{
			if (instance.Messages == null)
			{
				instance.Messages = new List<LogRelayMessage>();
			}
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
					instance.Messages.Add(LogRelayMessage.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, UtilLogRelay instance)
		{
			if (instance.Messages.Count <= 0)
			{
				return;
			}
			foreach (LogRelayMessage message in instance.Messages)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, message.GetSerializedSize());
				LogRelayMessage.Serialize(stream, message);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Messages.Count > 0)
			{
				foreach (LogRelayMessage message in Messages)
				{
					num++;
					uint serializedSize = message.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
