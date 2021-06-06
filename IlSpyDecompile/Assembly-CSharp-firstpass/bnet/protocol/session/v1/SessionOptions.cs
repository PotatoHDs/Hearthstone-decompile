using System.IO;

namespace bnet.protocol.session.v1
{
	public class SessionOptions : IProtoBuf
	{
		public bool HasBilling;

		private bool _Billing;

		public bool HasPresence;

		private bool _Presence;

		public bool Billing
		{
			get
			{
				return _Billing;
			}
			set
			{
				_Billing = value;
				HasBilling = true;
			}
		}

		public bool Presence
		{
			get
			{
				return _Presence;
			}
			set
			{
				_Presence = value;
				HasPresence = true;
			}
		}

		public bool IsInitialized => true;

		public void SetBilling(bool val)
		{
			Billing = val;
		}

		public void SetPresence(bool val)
		{
			Presence = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasBilling)
			{
				num ^= Billing.GetHashCode();
			}
			if (HasPresence)
			{
				num ^= Presence.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SessionOptions sessionOptions = obj as SessionOptions;
			if (sessionOptions == null)
			{
				return false;
			}
			if (HasBilling != sessionOptions.HasBilling || (HasBilling && !Billing.Equals(sessionOptions.Billing)))
			{
				return false;
			}
			if (HasPresence != sessionOptions.HasPresence || (HasPresence && !Presence.Equals(sessionOptions.Presence)))
			{
				return false;
			}
			return true;
		}

		public static SessionOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SessionOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SessionOptions Deserialize(Stream stream, SessionOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SessionOptions DeserializeLengthDelimited(Stream stream)
		{
			SessionOptions sessionOptions = new SessionOptions();
			DeserializeLengthDelimited(stream, sessionOptions);
			return sessionOptions;
		}

		public static SessionOptions DeserializeLengthDelimited(Stream stream, SessionOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SessionOptions Deserialize(Stream stream, SessionOptions instance, long limit)
		{
			instance.Billing = true;
			instance.Presence = true;
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
					instance.Billing = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.Presence = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, SessionOptions instance)
		{
			if (instance.HasBilling)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.Billing);
			}
			if (instance.HasPresence)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Presence);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasBilling)
			{
				num++;
				num++;
			}
			if (HasPresence)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
