using System.IO;
using System.Text;

namespace bnet.protocol.session.v1
{
	public class GetSignedSessionStateResponse : IProtoBuf
	{
		public bool HasToken;

		private string _Token;

		public string Token
		{
			get
			{
				return _Token;
			}
			set
			{
				_Token = value;
				HasToken = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetToken(string val)
		{
			Token = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasToken)
			{
				num ^= Token.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetSignedSessionStateResponse getSignedSessionStateResponse = obj as GetSignedSessionStateResponse;
			if (getSignedSessionStateResponse == null)
			{
				return false;
			}
			if (HasToken != getSignedSessionStateResponse.HasToken || (HasToken && !Token.Equals(getSignedSessionStateResponse.Token)))
			{
				return false;
			}
			return true;
		}

		public static GetSignedSessionStateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetSignedSessionStateResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetSignedSessionStateResponse Deserialize(Stream stream, GetSignedSessionStateResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetSignedSessionStateResponse DeserializeLengthDelimited(Stream stream)
		{
			GetSignedSessionStateResponse getSignedSessionStateResponse = new GetSignedSessionStateResponse();
			DeserializeLengthDelimited(stream, getSignedSessionStateResponse);
			return getSignedSessionStateResponse;
		}

		public static GetSignedSessionStateResponse DeserializeLengthDelimited(Stream stream, GetSignedSessionStateResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetSignedSessionStateResponse Deserialize(Stream stream, GetSignedSessionStateResponse instance, long limit)
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
					instance.Token = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, GetSignedSessionStateResponse instance)
		{
			if (instance.HasToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Token));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasToken)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Token);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
