using System.IO;
using System.Text;

namespace bnet.protocol.sns.v1
{
	public class GetGoogleSettingsResponse : IProtoBuf
	{
		public bool HasClientId;

		private string _ClientId;

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

		public void SetClientId(string val)
		{
			ClientId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasClientId)
			{
				num ^= ClientId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGoogleSettingsResponse getGoogleSettingsResponse = obj as GetGoogleSettingsResponse;
			if (getGoogleSettingsResponse == null)
			{
				return false;
			}
			if (HasClientId != getGoogleSettingsResponse.HasClientId || (HasClientId && !ClientId.Equals(getGoogleSettingsResponse.ClientId)))
			{
				return false;
			}
			return true;
		}

		public static GetGoogleSettingsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGoogleSettingsResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGoogleSettingsResponse Deserialize(Stream stream, GetGoogleSettingsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGoogleSettingsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGoogleSettingsResponse getGoogleSettingsResponse = new GetGoogleSettingsResponse();
			DeserializeLengthDelimited(stream, getGoogleSettingsResponse);
			return getGoogleSettingsResponse;
		}

		public static GetGoogleSettingsResponse DeserializeLengthDelimited(Stream stream, GetGoogleSettingsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGoogleSettingsResponse Deserialize(Stream stream, GetGoogleSettingsResponse instance, long limit)
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

		public static void Serialize(Stream stream, GetGoogleSettingsResponse instance)
		{
			if (instance.HasClientId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasClientId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
