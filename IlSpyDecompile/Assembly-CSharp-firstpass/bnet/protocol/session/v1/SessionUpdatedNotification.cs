using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	public class SessionUpdatedNotification : IProtoBuf
	{
		public bool HasIdentity;

		private bnet.protocol.account.v1.Identity _Identity;

		public bool HasReason;

		private uint _Reason;

		public bool HasSessionId;

		private string _SessionId;

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

		public uint Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = true;
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

		public bool IsInitialized => true;

		public void SetIdentity(bnet.protocol.account.v1.Identity val)
		{
			Identity = val;
		}

		public void SetReason(uint val)
		{
			Reason = val;
		}

		public void SetSessionId(string val)
		{
			SessionId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasIdentity)
			{
				num ^= Identity.GetHashCode();
			}
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			if (HasSessionId)
			{
				num ^= SessionId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SessionUpdatedNotification sessionUpdatedNotification = obj as SessionUpdatedNotification;
			if (sessionUpdatedNotification == null)
			{
				return false;
			}
			if (HasIdentity != sessionUpdatedNotification.HasIdentity || (HasIdentity && !Identity.Equals(sessionUpdatedNotification.Identity)))
			{
				return false;
			}
			if (HasReason != sessionUpdatedNotification.HasReason || (HasReason && !Reason.Equals(sessionUpdatedNotification.Reason)))
			{
				return false;
			}
			if (HasSessionId != sessionUpdatedNotification.HasSessionId || (HasSessionId && !SessionId.Equals(sessionUpdatedNotification.SessionId)))
			{
				return false;
			}
			return true;
		}

		public static SessionUpdatedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SessionUpdatedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SessionUpdatedNotification Deserialize(Stream stream, SessionUpdatedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SessionUpdatedNotification DeserializeLengthDelimited(Stream stream)
		{
			SessionUpdatedNotification sessionUpdatedNotification = new SessionUpdatedNotification();
			DeserializeLengthDelimited(stream, sessionUpdatedNotification);
			return sessionUpdatedNotification;
		}

		public static SessionUpdatedNotification DeserializeLengthDelimited(Stream stream, SessionUpdatedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SessionUpdatedNotification Deserialize(Stream stream, SessionUpdatedNotification instance, long limit)
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
				case 16:
					instance.Reason = ProtocolParser.ReadUInt32(stream);
					continue;
				case 26:
					instance.SessionId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, SessionUpdatedNotification instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				bnet.protocol.account.v1.Identity.Serialize(stream, instance.Identity);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.HasSessionId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SessionId));
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
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Reason);
			}
			if (HasSessionId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(SessionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
