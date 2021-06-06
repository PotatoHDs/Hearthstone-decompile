using System.IO;
using System.Text;

namespace bnet.protocol.games.v1
{
	public class RegisterServerResponse : IProtoBuf
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
			RegisterServerResponse registerServerResponse = obj as RegisterServerResponse;
			if (registerServerResponse == null)
			{
				return false;
			}
			if (HasClientId != registerServerResponse.HasClientId || (HasClientId && !ClientId.Equals(registerServerResponse.ClientId)))
			{
				return false;
			}
			return true;
		}

		public static RegisterServerResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterServerResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RegisterServerResponse Deserialize(Stream stream, RegisterServerResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RegisterServerResponse DeserializeLengthDelimited(Stream stream)
		{
			RegisterServerResponse registerServerResponse = new RegisterServerResponse();
			DeserializeLengthDelimited(stream, registerServerResponse);
			return registerServerResponse;
		}

		public static RegisterServerResponse DeserializeLengthDelimited(Stream stream, RegisterServerResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RegisterServerResponse Deserialize(Stream stream, RegisterServerResponse instance, long limit)
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

		public static void Serialize(Stream stream, RegisterServerResponse instance)
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
