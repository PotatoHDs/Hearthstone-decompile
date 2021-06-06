using System.IO;
using System.Text;

namespace Blizzard.Telemetry.CRM
{
	public class CrmEvent : IProtoBuf
	{
		public bool HasEventName;

		private string _EventName;

		public bool HasEventPayload;

		private string _EventPayload;

		public bool HasApplicationId;

		private string _ApplicationId;

		public string EventName
		{
			get
			{
				return _EventName;
			}
			set
			{
				_EventName = value;
				HasEventName = value != null;
			}
		}

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
			if (HasEventName)
			{
				num ^= EventName.GetHashCode();
			}
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
			CrmEvent crmEvent = obj as CrmEvent;
			if (crmEvent == null)
			{
				return false;
			}
			if (HasEventName != crmEvent.HasEventName || (HasEventName && !EventName.Equals(crmEvent.EventName)))
			{
				return false;
			}
			if (HasEventPayload != crmEvent.HasEventPayload || (HasEventPayload && !EventPayload.Equals(crmEvent.EventPayload)))
			{
				return false;
			}
			if (HasApplicationId != crmEvent.HasApplicationId || (HasApplicationId && !ApplicationId.Equals(crmEvent.ApplicationId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CrmEvent Deserialize(Stream stream, CrmEvent instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CrmEvent DeserializeLengthDelimited(Stream stream)
		{
			CrmEvent crmEvent = new CrmEvent();
			DeserializeLengthDelimited(stream, crmEvent);
			return crmEvent;
		}

		public static CrmEvent DeserializeLengthDelimited(Stream stream, CrmEvent instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CrmEvent Deserialize(Stream stream, CrmEvent instance, long limit)
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
				case 82:
					instance.EventName = ProtocolParser.ReadString(stream);
					continue;
				default:
				{
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

		public static void Serialize(Stream stream, CrmEvent instance)
		{
			if (instance.HasEventName)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EventName));
			}
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
			if (HasEventName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(EventName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasEventPayload)
			{
				num += 2;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(EventPayload);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasApplicationId)
			{
				num += 2;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(ApplicationId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}
	}
}
