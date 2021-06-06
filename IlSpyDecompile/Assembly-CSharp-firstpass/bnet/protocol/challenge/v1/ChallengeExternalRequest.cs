using System.IO;
using System.Text;

namespace bnet.protocol.challenge.v1
{
	public class ChallengeExternalRequest : IProtoBuf
	{
		public bool HasRequestToken;

		private string _RequestToken;

		public bool HasPayloadType;

		private string _PayloadType;

		public bool HasPayload;

		private byte[] _Payload;

		public string RequestToken
		{
			get
			{
				return _RequestToken;
			}
			set
			{
				_RequestToken = value;
				HasRequestToken = value != null;
			}
		}

		public string PayloadType
		{
			get
			{
				return _PayloadType;
			}
			set
			{
				_PayloadType = value;
				HasPayloadType = value != null;
			}
		}

		public byte[] Payload
		{
			get
			{
				return _Payload;
			}
			set
			{
				_Payload = value;
				HasPayload = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetRequestToken(string val)
		{
			RequestToken = val;
		}

		public void SetPayloadType(string val)
		{
			PayloadType = val;
		}

		public void SetPayload(byte[] val)
		{
			Payload = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRequestToken)
			{
				num ^= RequestToken.GetHashCode();
			}
			if (HasPayloadType)
			{
				num ^= PayloadType.GetHashCode();
			}
			if (HasPayload)
			{
				num ^= Payload.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChallengeExternalRequest challengeExternalRequest = obj as ChallengeExternalRequest;
			if (challengeExternalRequest == null)
			{
				return false;
			}
			if (HasRequestToken != challengeExternalRequest.HasRequestToken || (HasRequestToken && !RequestToken.Equals(challengeExternalRequest.RequestToken)))
			{
				return false;
			}
			if (HasPayloadType != challengeExternalRequest.HasPayloadType || (HasPayloadType && !PayloadType.Equals(challengeExternalRequest.PayloadType)))
			{
				return false;
			}
			if (HasPayload != challengeExternalRequest.HasPayload || (HasPayload && !Payload.Equals(challengeExternalRequest.Payload)))
			{
				return false;
			}
			return true;
		}

		public static ChallengeExternalRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChallengeExternalRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChallengeExternalRequest Deserialize(Stream stream, ChallengeExternalRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChallengeExternalRequest DeserializeLengthDelimited(Stream stream)
		{
			ChallengeExternalRequest challengeExternalRequest = new ChallengeExternalRequest();
			DeserializeLengthDelimited(stream, challengeExternalRequest);
			return challengeExternalRequest;
		}

		public static ChallengeExternalRequest DeserializeLengthDelimited(Stream stream, ChallengeExternalRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChallengeExternalRequest Deserialize(Stream stream, ChallengeExternalRequest instance, long limit)
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
					instance.RequestToken = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.PayloadType = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.Payload = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, ChallengeExternalRequest instance)
		{
			if (instance.HasRequestToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RequestToken));
			}
			if (instance.HasPayloadType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PayloadType));
			}
			if (instance.HasPayload)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, instance.Payload);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRequestToken)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(RequestToken);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasPayloadType)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(PayloadType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasPayload)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(Payload.Length) + Payload.Length);
			}
			return num;
		}
	}
}
