using System.IO;
using System.Text;

namespace bnet.protocol.session.v1
{
	public class CreateSessionResponse : IProtoBuf
	{
		public bool HasSessionId;

		private string _SessionId;

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

		public void SetSessionId(string val)
		{
			SessionId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSessionId)
			{
				num ^= SessionId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateSessionResponse createSessionResponse = obj as CreateSessionResponse;
			if (createSessionResponse == null)
			{
				return false;
			}
			if (HasSessionId != createSessionResponse.HasSessionId || (HasSessionId && !SessionId.Equals(createSessionResponse.SessionId)))
			{
				return false;
			}
			return true;
		}

		public static CreateSessionResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateSessionResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateSessionResponse Deserialize(Stream stream, CreateSessionResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateSessionResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateSessionResponse createSessionResponse = new CreateSessionResponse();
			DeserializeLengthDelimited(stream, createSessionResponse);
			return createSessionResponse;
		}

		public static CreateSessionResponse DeserializeLengthDelimited(Stream stream, CreateSessionResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateSessionResponse Deserialize(Stream stream, CreateSessionResponse instance, long limit)
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

		public static void Serialize(Stream stream, CreateSessionResponse instance)
		{
			if (instance.HasSessionId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SessionId));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
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
