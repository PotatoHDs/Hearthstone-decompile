using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	public class DestroySessionRequest : IProtoBuf
	{
		public bool HasIdentity;

		private bnet.protocol.account.v1.Identity _Identity;

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
			if (HasSessionId)
			{
				num ^= SessionId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DestroySessionRequest destroySessionRequest = obj as DestroySessionRequest;
			if (destroySessionRequest == null)
			{
				return false;
			}
			if (HasIdentity != destroySessionRequest.HasIdentity || (HasIdentity && !Identity.Equals(destroySessionRequest.Identity)))
			{
				return false;
			}
			if (HasSessionId != destroySessionRequest.HasSessionId || (HasSessionId && !SessionId.Equals(destroySessionRequest.SessionId)))
			{
				return false;
			}
			return true;
		}

		public static DestroySessionRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DestroySessionRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DestroySessionRequest Deserialize(Stream stream, DestroySessionRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DestroySessionRequest DeserializeLengthDelimited(Stream stream)
		{
			DestroySessionRequest destroySessionRequest = new DestroySessionRequest();
			DeserializeLengthDelimited(stream, destroySessionRequest);
			return destroySessionRequest;
		}

		public static DestroySessionRequest DeserializeLengthDelimited(Stream stream, DestroySessionRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DestroySessionRequest Deserialize(Stream stream, DestroySessionRequest instance, long limit)
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

		public static void Serialize(Stream stream, DestroySessionRequest instance)
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
			return num;
		}
	}
}
