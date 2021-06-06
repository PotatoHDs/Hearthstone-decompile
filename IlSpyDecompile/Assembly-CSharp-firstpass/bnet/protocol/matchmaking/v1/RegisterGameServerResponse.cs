using System.IO;
using System.Text;

namespace bnet.protocol.matchmaking.v1
{
	public class RegisterGameServerResponse : IProtoBuf
	{
		public bool HasGameServerGuid;

		private ulong _GameServerGuid;

		public bool HasClientId;

		private string _ClientId;

		public ulong GameServerGuid
		{
			get
			{
				return _GameServerGuid;
			}
			set
			{
				_GameServerGuid = value;
				HasGameServerGuid = true;
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

		public void SetGameServerGuid(ulong val)
		{
			GameServerGuid = val;
		}

		public void SetClientId(string val)
		{
			ClientId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameServerGuid)
			{
				num ^= GameServerGuid.GetHashCode();
			}
			if (HasClientId)
			{
				num ^= ClientId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RegisterGameServerResponse registerGameServerResponse = obj as RegisterGameServerResponse;
			if (registerGameServerResponse == null)
			{
				return false;
			}
			if (HasGameServerGuid != registerGameServerResponse.HasGameServerGuid || (HasGameServerGuid && !GameServerGuid.Equals(registerGameServerResponse.GameServerGuid)))
			{
				return false;
			}
			if (HasClientId != registerGameServerResponse.HasClientId || (HasClientId && !ClientId.Equals(registerGameServerResponse.ClientId)))
			{
				return false;
			}
			return true;
		}

		public static RegisterGameServerResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterGameServerResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RegisterGameServerResponse Deserialize(Stream stream, RegisterGameServerResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RegisterGameServerResponse DeserializeLengthDelimited(Stream stream)
		{
			RegisterGameServerResponse registerGameServerResponse = new RegisterGameServerResponse();
			DeserializeLengthDelimited(stream, registerGameServerResponse);
			return registerGameServerResponse;
		}

		public static RegisterGameServerResponse DeserializeLengthDelimited(Stream stream, RegisterGameServerResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RegisterGameServerResponse Deserialize(Stream stream, RegisterGameServerResponse instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 9:
					instance.GameServerGuid = binaryReader.ReadUInt64();
					continue;
				case 18:
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

		public static void Serialize(Stream stream, RegisterGameServerResponse instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasGameServerGuid)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.GameServerGuid);
			}
			if (instance.HasClientId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameServerGuid)
			{
				num++;
				num += 8;
			}
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
