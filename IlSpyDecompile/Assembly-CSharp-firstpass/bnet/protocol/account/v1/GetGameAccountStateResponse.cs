using System.IO;

namespace bnet.protocol.account.v1
{
	public class GetGameAccountStateResponse : IProtoBuf
	{
		public bool HasState;

		private GameAccountState _State;

		public bool HasTags;

		private GameAccountFieldTags _Tags;

		public GameAccountState State
		{
			get
			{
				return _State;
			}
			set
			{
				_State = value;
				HasState = value != null;
			}
		}

		public GameAccountFieldTags Tags
		{
			get
			{
				return _Tags;
			}
			set
			{
				_Tags = value;
				HasTags = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetState(GameAccountState val)
		{
			State = val;
		}

		public void SetTags(GameAccountFieldTags val)
		{
			Tags = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasState)
			{
				num ^= State.GetHashCode();
			}
			if (HasTags)
			{
				num ^= Tags.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGameAccountStateResponse getGameAccountStateResponse = obj as GetGameAccountStateResponse;
			if (getGameAccountStateResponse == null)
			{
				return false;
			}
			if (HasState != getGameAccountStateResponse.HasState || (HasState && !State.Equals(getGameAccountStateResponse.State)))
			{
				return false;
			}
			if (HasTags != getGameAccountStateResponse.HasTags || (HasTags && !Tags.Equals(getGameAccountStateResponse.Tags)))
			{
				return false;
			}
			return true;
		}

		public static GetGameAccountStateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameAccountStateResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGameAccountStateResponse Deserialize(Stream stream, GetGameAccountStateResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGameAccountStateResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGameAccountStateResponse getGameAccountStateResponse = new GetGameAccountStateResponse();
			DeserializeLengthDelimited(stream, getGameAccountStateResponse);
			return getGameAccountStateResponse;
		}

		public static GetGameAccountStateResponse DeserializeLengthDelimited(Stream stream, GetGameAccountStateResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGameAccountStateResponse Deserialize(Stream stream, GetGameAccountStateResponse instance, long limit)
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
					if (instance.State == null)
					{
						instance.State = GameAccountState.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountState.DeserializeLengthDelimited(stream, instance.State);
					}
					continue;
				case 18:
					if (instance.Tags == null)
					{
						instance.Tags = GameAccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountFieldTags.DeserializeLengthDelimited(stream, instance.Tags);
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

		public static void Serialize(Stream stream, GetGameAccountStateResponse instance)
		{
			if (instance.HasState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				GameAccountState.Serialize(stream, instance.State);
			}
			if (instance.HasTags)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Tags.GetSerializedSize());
				GameAccountFieldTags.Serialize(stream, instance.Tags);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasState)
			{
				num++;
				uint serializedSize = State.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasTags)
			{
				num++;
				uint serializedSize2 = Tags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
