using System.IO;

namespace PegasusUtil
{
	public class ClientRequestResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 328
		}

		public enum ClientRequestResponseFlags
		{
			CRRF_SERVICE_NONE,
			CRRF_SERVICE_UNAVAILABLE,
			CRRF_SERVICE_UNKNOWN_ERROR
		}

		public bool HasResponseFlags;

		private ClientRequestResponseFlags _ResponseFlags;

		public ClientRequestResponseFlags ResponseFlags
		{
			get
			{
				return _ResponseFlags;
			}
			set
			{
				_ResponseFlags = value;
				HasResponseFlags = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasResponseFlags)
			{
				num ^= ResponseFlags.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ClientRequestResponse clientRequestResponse = obj as ClientRequestResponse;
			if (clientRequestResponse == null)
			{
				return false;
			}
			if (HasResponseFlags != clientRequestResponse.HasResponseFlags || (HasResponseFlags && !ResponseFlags.Equals(clientRequestResponse.ResponseFlags)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ClientRequestResponse Deserialize(Stream stream, ClientRequestResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ClientRequestResponse DeserializeLengthDelimited(Stream stream)
		{
			ClientRequestResponse clientRequestResponse = new ClientRequestResponse();
			DeserializeLengthDelimited(stream, clientRequestResponse);
			return clientRequestResponse;
		}

		public static ClientRequestResponse DeserializeLengthDelimited(Stream stream, ClientRequestResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ClientRequestResponse Deserialize(Stream stream, ClientRequestResponse instance, long limit)
		{
			instance.ResponseFlags = ClientRequestResponseFlags.CRRF_SERVICE_NONE;
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
				case 8:
					instance.ResponseFlags = (ClientRequestResponseFlags)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ClientRequestResponse instance)
		{
			if (instance.HasResponseFlags)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ResponseFlags);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasResponseFlags)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ResponseFlags);
			}
			return num;
		}
	}
}
