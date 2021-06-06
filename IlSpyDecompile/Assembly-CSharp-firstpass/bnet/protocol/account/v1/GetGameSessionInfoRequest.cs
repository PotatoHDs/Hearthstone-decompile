using System.IO;

namespace bnet.protocol.account.v1
{
	public class GetGameSessionInfoRequest : IProtoBuf
	{
		public bool HasEntityId;

		private EntityId _EntityId;

		public EntityId EntityId
		{
			get
			{
				return _EntityId;
			}
			set
			{
				_EntityId = value;
				HasEntityId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetEntityId(EntityId val)
		{
			EntityId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasEntityId)
			{
				num ^= EntityId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGameSessionInfoRequest getGameSessionInfoRequest = obj as GetGameSessionInfoRequest;
			if (getGameSessionInfoRequest == null)
			{
				return false;
			}
			if (HasEntityId != getGameSessionInfoRequest.HasEntityId || (HasEntityId && !EntityId.Equals(getGameSessionInfoRequest.EntityId)))
			{
				return false;
			}
			return true;
		}

		public static GetGameSessionInfoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameSessionInfoRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGameSessionInfoRequest Deserialize(Stream stream, GetGameSessionInfoRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGameSessionInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGameSessionInfoRequest getGameSessionInfoRequest = new GetGameSessionInfoRequest();
			DeserializeLengthDelimited(stream, getGameSessionInfoRequest);
			return getGameSessionInfoRequest;
		}

		public static GetGameSessionInfoRequest DeserializeLengthDelimited(Stream stream, GetGameSessionInfoRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGameSessionInfoRequest Deserialize(Stream stream, GetGameSessionInfoRequest instance, long limit)
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
					if (instance.EntityId == null)
					{
						instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
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

		public static void Serialize(Stream stream, GetGameSessionInfoRequest instance)
		{
			if (instance.HasEntityId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasEntityId)
			{
				num++;
				uint serializedSize = EntityId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
