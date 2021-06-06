using System.IO;

namespace bnet.protocol.games.v1
{
	public class GetFindGameRequestsRequest : IProtoBuf
	{
		public bool HasFactoryId;

		private ulong _FactoryId;

		public bool HasNumPlayers;

		private uint _NumPlayers;

		public ulong FactoryId
		{
			get
			{
				return _FactoryId;
			}
			set
			{
				_FactoryId = value;
				HasFactoryId = true;
			}
		}

		public uint NumPlayers
		{
			get
			{
				return _NumPlayers;
			}
			set
			{
				_NumPlayers = value;
				HasNumPlayers = true;
			}
		}

		public bool IsInitialized => true;

		public void SetFactoryId(ulong val)
		{
			FactoryId = val;
		}

		public void SetNumPlayers(uint val)
		{
			NumPlayers = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFactoryId)
			{
				num ^= FactoryId.GetHashCode();
			}
			if (HasNumPlayers)
			{
				num ^= NumPlayers.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetFindGameRequestsRequest getFindGameRequestsRequest = obj as GetFindGameRequestsRequest;
			if (getFindGameRequestsRequest == null)
			{
				return false;
			}
			if (HasFactoryId != getFindGameRequestsRequest.HasFactoryId || (HasFactoryId && !FactoryId.Equals(getFindGameRequestsRequest.FactoryId)))
			{
				return false;
			}
			if (HasNumPlayers != getFindGameRequestsRequest.HasNumPlayers || (HasNumPlayers && !NumPlayers.Equals(getFindGameRequestsRequest.NumPlayers)))
			{
				return false;
			}
			return true;
		}

		public static GetFindGameRequestsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFindGameRequestsRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetFindGameRequestsRequest Deserialize(Stream stream, GetFindGameRequestsRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetFindGameRequestsRequest DeserializeLengthDelimited(Stream stream)
		{
			GetFindGameRequestsRequest getFindGameRequestsRequest = new GetFindGameRequestsRequest();
			DeserializeLengthDelimited(stream, getFindGameRequestsRequest);
			return getFindGameRequestsRequest;
		}

		public static GetFindGameRequestsRequest DeserializeLengthDelimited(Stream stream, GetFindGameRequestsRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetFindGameRequestsRequest Deserialize(Stream stream, GetFindGameRequestsRequest instance, long limit)
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
					instance.FactoryId = binaryReader.ReadUInt64();
					continue;
				case 16:
					instance.NumPlayers = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, GetFindGameRequestsRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasFactoryId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.FactoryId);
			}
			if (instance.HasNumPlayers)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.NumPlayers);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasFactoryId)
			{
				num++;
				num += 8;
			}
			if (HasNumPlayers)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(NumPlayers);
			}
			return num;
		}
	}
}
