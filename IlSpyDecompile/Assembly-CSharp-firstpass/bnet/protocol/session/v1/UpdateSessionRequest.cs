using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	public class UpdateSessionRequest : IProtoBuf
	{
		public bool HasIdentity;

		private bnet.protocol.account.v1.Identity _Identity;

		public bool HasOptions;

		private SessionOptions _Options;

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

		public SessionOptions Options
		{
			get
			{
				return _Options;
			}
			set
			{
				_Options = value;
				HasOptions = value != null;
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

		public void SetOptions(SessionOptions val)
		{
			Options = val;
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
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			if (HasSessionId)
			{
				num ^= SessionId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateSessionRequest updateSessionRequest = obj as UpdateSessionRequest;
			if (updateSessionRequest == null)
			{
				return false;
			}
			if (HasIdentity != updateSessionRequest.HasIdentity || (HasIdentity && !Identity.Equals(updateSessionRequest.Identity)))
			{
				return false;
			}
			if (HasOptions != updateSessionRequest.HasOptions || (HasOptions && !Options.Equals(updateSessionRequest.Options)))
			{
				return false;
			}
			if (HasSessionId != updateSessionRequest.HasSessionId || (HasSessionId && !SessionId.Equals(updateSessionRequest.SessionId)))
			{
				return false;
			}
			return true;
		}

		public static UpdateSessionRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateSessionRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateSessionRequest Deserialize(Stream stream, UpdateSessionRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateSessionRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateSessionRequest updateSessionRequest = new UpdateSessionRequest();
			DeserializeLengthDelimited(stream, updateSessionRequest);
			return updateSessionRequest;
		}

		public static UpdateSessionRequest DeserializeLengthDelimited(Stream stream, UpdateSessionRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateSessionRequest Deserialize(Stream stream, UpdateSessionRequest instance, long limit)
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
					if (instance.Options == null)
					{
						instance.Options = SessionOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						SessionOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
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

		public static void Serialize(Stream stream, UpdateSessionRequest instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				bnet.protocol.account.v1.Identity.Serialize(stream, instance.Identity);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				SessionOptions.Serialize(stream, instance.Options);
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
			if (HasOptions)
			{
				num++;
				uint serializedSize2 = Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
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
