using System.IO;

namespace bnet.protocol.account.v1
{
	public class GetCAISInfoResponse : IProtoBuf
	{
		public bool HasCaisInfo;

		private CAIS _CaisInfo;

		public CAIS CaisInfo
		{
			get
			{
				return _CaisInfo;
			}
			set
			{
				_CaisInfo = value;
				HasCaisInfo = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetCaisInfo(CAIS val)
		{
			CaisInfo = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasCaisInfo)
			{
				num ^= CaisInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetCAISInfoResponse getCAISInfoResponse = obj as GetCAISInfoResponse;
			if (getCAISInfoResponse == null)
			{
				return false;
			}
			if (HasCaisInfo != getCAISInfoResponse.HasCaisInfo || (HasCaisInfo && !CaisInfo.Equals(getCAISInfoResponse.CaisInfo)))
			{
				return false;
			}
			return true;
		}

		public static GetCAISInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetCAISInfoResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetCAISInfoResponse Deserialize(Stream stream, GetCAISInfoResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetCAISInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetCAISInfoResponse getCAISInfoResponse = new GetCAISInfoResponse();
			DeserializeLengthDelimited(stream, getCAISInfoResponse);
			return getCAISInfoResponse;
		}

		public static GetCAISInfoResponse DeserializeLengthDelimited(Stream stream, GetCAISInfoResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetCAISInfoResponse Deserialize(Stream stream, GetCAISInfoResponse instance, long limit)
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
					if (instance.CaisInfo == null)
					{
						instance.CaisInfo = CAIS.DeserializeLengthDelimited(stream);
					}
					else
					{
						CAIS.DeserializeLengthDelimited(stream, instance.CaisInfo);
					}
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

		public static void Serialize(Stream stream, GetCAISInfoResponse instance)
		{
			if (instance.HasCaisInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.CaisInfo.GetSerializedSize());
				CAIS.Serialize(stream, instance.CaisInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasCaisInfo)
			{
				num++;
				uint serializedSize = CaisInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
