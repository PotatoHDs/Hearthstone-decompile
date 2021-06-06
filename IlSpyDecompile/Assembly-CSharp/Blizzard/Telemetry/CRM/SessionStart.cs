using System.IO;
using System.Text;

namespace Blizzard.Telemetry.CRM
{
	public class SessionStart : IProtoBuf
	{
		public bool HasEventPayload;

		private string _EventPayload;

		public bool HasApplicationId;

		private string _ApplicationId;

		public string EventPayload
		{
			get
			{
				return _EventPayload;
			}
			set
			{
				_EventPayload = value;
				HasEventPayload = value != null;
			}
		}

		public string ApplicationId
		{
			get
			{
				return _ApplicationId;
			}
			set
			{
				_ApplicationId = value;
				HasApplicationId = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasEventPayload)
			{
				num ^= EventPayload.GetHashCode();
			}
			if (HasApplicationId)
			{
				num ^= ApplicationId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SessionStart sessionStart = obj as SessionStart;
			if (sessionStart == null)
			{
				return false;
			}
			if (HasEventPayload != sessionStart.HasEventPayload || (HasEventPayload && !EventPayload.Equals(sessionStart.EventPayload)))
			{
				return false;
			}
			if (HasApplicationId != sessionStart.HasApplicationId || (HasApplicationId && !ApplicationId.Equals(sessionStart.ApplicationId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SessionStart Deserialize(Stream stream, SessionStart instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SessionStart DeserializeLengthDelimited(Stream stream)
		{
			SessionStart sessionStart = new SessionStart();
			DeserializeLengthDelimited(stream, sessionStart);
			return sessionStart;
		}

		public static SessionStart DeserializeLengthDelimited(Stream stream, SessionStart instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SessionStart Deserialize(Stream stream, SessionStart instance, long limit)
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
				if (num == -1)
				{
					if (limit < 0)
					{
						break;
					}
					throw new EndOfStreamException();
				}
				Key key = ProtocolParser.ReadKey((byte)num, stream);
				switch (key.Field)
				{
				case 0u:
					throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
				case 20u:
					if (key.WireType == Wire.LengthDelimited)
					{
						instance.EventPayload = ProtocolParser.ReadString(stream);
					}
					break;
				case 30u:
					if (key.WireType == Wire.LengthDelimited)
					{
						instance.ApplicationId = ProtocolParser.ReadString(stream);
					}
					break;
				default:
					ProtocolParser.SkipKey(stream, key);
					break;
				}
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SessionStart instance)
		{
			if (instance.HasEventPayload)
			{
				stream.WriteByte(162);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EventPayload));
			}
			if (instance.HasApplicationId)
			{
				stream.WriteByte(242);
				stream.WriteByte(1);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ApplicationId));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasEventPayload)
			{
				num += 2;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(EventPayload);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasApplicationId)
			{
				num += 2;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
