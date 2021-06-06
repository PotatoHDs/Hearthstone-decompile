using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	public class SessionCreatedNotification : IProtoBuf
	{
		public bool HasIdentity;

		private bnet.protocol.account.v1.Identity _Identity;

		public bool HasReason;

		private uint _Reason;

		public bool HasSessionId;

		private string _SessionId;

		public bool HasSessionKey;

		private byte[] _SessionKey;

		public bool HasClientId;

		private string _ClientId;

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

		public byte[] SessionKey
		{
			get
			{
				return _SessionKey;
			}
			set
			{
				_SessionKey = value;
				HasSessionKey = value != null;
			}
		}

		public string ClientId
		{
			get
			{
				return _ClientId;
			}
			set
			{
				_ClientId = value;
				HasClientId = value != null;
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

		public void SetSessionKey(byte[] val)
		{
			SessionKey = val;
		}

		public void SetClientId(string val)
		{
			ClientId = val;
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
			if (HasSessionKey)
			{
				num ^= SessionKey.GetHashCode();
			}
			if (HasClientId)
			{
				num ^= ClientId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SessionCreatedNotification sessionCreatedNotification = obj as SessionCreatedNotification;
			if (sessionCreatedNotification == null)
			{
				return false;
			}
			if (HasIdentity != sessionCreatedNotification.HasIdentity || (HasIdentity && !Identity.Equals(sessionCreatedNotification.Identity)))
			{
				return false;
			}
			if (HasReason != sessionCreatedNotification.HasReason || (HasReason && !Reason.Equals(sessionCreatedNotification.Reason)))
			{
				return false;
			}
			if (HasSessionId != sessionCreatedNotification.HasSessionId || (HasSessionId && !SessionId.Equals(sessionCreatedNotification.SessionId)))
			{
				return false;
			}
			if (HasSessionKey != sessionCreatedNotification.HasSessionKey || (HasSessionKey && !SessionKey.Equals(sessionCreatedNotification.SessionKey)))
			{
				return false;
			}
			if (HasClientId != sessionCreatedNotification.HasClientId || (HasClientId && !ClientId.Equals(sessionCreatedNotification.ClientId)))
			{
				return false;
			}
			return true;
		}

		public static SessionCreatedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SessionCreatedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SessionCreatedNotification Deserialize(Stream stream, SessionCreatedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SessionCreatedNotification DeserializeLengthDelimited(Stream stream)
		{
			SessionCreatedNotification sessionCreatedNotification = new SessionCreatedNotification();
			DeserializeLengthDelimited(stream, sessionCreatedNotification);
			return sessionCreatedNotification;
		}

		public static SessionCreatedNotification DeserializeLengthDelimited(Stream stream, SessionCreatedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SessionCreatedNotification Deserialize(Stream stream, SessionCreatedNotification instance, long limit)
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
				case 34:
					instance.SessionKey = ProtocolParser.ReadBytes(stream);
					continue;
				case 42:
					instance.ClientId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, SessionCreatedNotification instance)
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
			if (instance.HasSessionKey)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, instance.SessionKey);
			}
			if (instance.HasClientId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
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
			if (HasSessionKey)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(SessionKey.Length) + SessionKey.Length);
			}
			if (HasClientId)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
