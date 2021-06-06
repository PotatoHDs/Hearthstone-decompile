using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	public class SessionGameTimeWarningNotification : IProtoBuf
	{
		public bool HasIdentity;

		private bnet.protocol.account.v1.Identity _Identity;

		public bool HasSessionId;

		private string _SessionId;

		public bool HasRemainingTimeDurationMin;

		private uint _RemainingTimeDurationMin;

		public bool HasRestrictionType;

		private uint _RestrictionType;

		public bnet.protocol.account.v1.Identity Identity
		{
			get
			{
				return _Identity;
			}
			set
			{
				_Identity = value;
				HasIdentity = value != null;
			}
		}

		public string SessionId
		{
			get
			{
				return _SessionId;
			}
			set
			{
				_SessionId = value;
				HasSessionId = value != null;
			}
		}

		public uint RemainingTimeDurationMin
		{
			get
			{
				return _RemainingTimeDurationMin;
			}
			set
			{
				_RemainingTimeDurationMin = value;
				HasRemainingTimeDurationMin = true;
			}
		}

		public uint RestrictionType
		{
			get
			{
				return _RestrictionType;
			}
			set
			{
				_RestrictionType = value;
				HasRestrictionType = true;
			}
		}

		public bool IsInitialized => true;

		public void SetIdentity(bnet.protocol.account.v1.Identity val)
		{
			Identity = val;
		}

		public void SetSessionId(string val)
		{
			SessionId = val;
		}

		public void SetRemainingTimeDurationMin(uint val)
		{
			RemainingTimeDurationMin = val;
		}

		public void SetRestrictionType(uint val)
		{
			RestrictionType = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasIdentity)
			{
				num ^= Identity.GetHashCode();
			}
			if (HasSessionId)
			{
				num ^= SessionId.GetHashCode();
			}
			if (HasRemainingTimeDurationMin)
			{
				num ^= RemainingTimeDurationMin.GetHashCode();
			}
			if (HasRestrictionType)
			{
				num ^= RestrictionType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SessionGameTimeWarningNotification sessionGameTimeWarningNotification = obj as SessionGameTimeWarningNotification;
			if (sessionGameTimeWarningNotification == null)
			{
				return false;
			}
			if (HasIdentity != sessionGameTimeWarningNotification.HasIdentity || (HasIdentity && !Identity.Equals(sessionGameTimeWarningNotification.Identity)))
			{
				return false;
			}
			if (HasSessionId != sessionGameTimeWarningNotification.HasSessionId || (HasSessionId && !SessionId.Equals(sessionGameTimeWarningNotification.SessionId)))
			{
				return false;
			}
			if (HasRemainingTimeDurationMin != sessionGameTimeWarningNotification.HasRemainingTimeDurationMin || (HasRemainingTimeDurationMin && !RemainingTimeDurationMin.Equals(sessionGameTimeWarningNotification.RemainingTimeDurationMin)))
			{
				return false;
			}
			if (HasRestrictionType != sessionGameTimeWarningNotification.HasRestrictionType || (HasRestrictionType && !RestrictionType.Equals(sessionGameTimeWarningNotification.RestrictionType)))
			{
				return false;
			}
			return true;
		}

		public static SessionGameTimeWarningNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SessionGameTimeWarningNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SessionGameTimeWarningNotification Deserialize(Stream stream, SessionGameTimeWarningNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SessionGameTimeWarningNotification DeserializeLengthDelimited(Stream stream)
		{
			SessionGameTimeWarningNotification sessionGameTimeWarningNotification = new SessionGameTimeWarningNotification();
			DeserializeLengthDelimited(stream, sessionGameTimeWarningNotification);
			return sessionGameTimeWarningNotification;
		}

		public static SessionGameTimeWarningNotification DeserializeLengthDelimited(Stream stream, SessionGameTimeWarningNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SessionGameTimeWarningNotification Deserialize(Stream stream, SessionGameTimeWarningNotification instance, long limit)
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
					if (instance.Identity == null)
					{
						instance.Identity = bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream, instance.Identity);
					}
					continue;
				case 18:
					instance.SessionId = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.RemainingTimeDurationMin = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.RestrictionType = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, SessionGameTimeWarningNotification instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				bnet.protocol.account.v1.Identity.Serialize(stream, instance.Identity);
			}
			if (instance.HasSessionId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SessionId));
			}
			if (instance.HasRemainingTimeDurationMin)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.RemainingTimeDurationMin);
			}
			if (instance.HasRestrictionType)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.RestrictionType);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasIdentity)
			{
				num++;
				uint serializedSize = Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasSessionId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(SessionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasRemainingTimeDurationMin)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(RemainingTimeDurationMin);
			}
			if (HasRestrictionType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(RestrictionType);
			}
			return num;
		}
	}
}
