using System.IO;

namespace bnet.protocol.account.v1
{
	public class GetAccountStateResponse : IProtoBuf
	{
		public bool HasState;

		private AccountState _State;

		public bool HasTags;

		private AccountFieldTags _Tags;

		public AccountState State
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

		public AccountFieldTags Tags
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

		public void SetState(AccountState val)
		{
			State = val;
		}

		public void SetTags(AccountFieldTags val)
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
			GetAccountStateResponse getAccountStateResponse = obj as GetAccountStateResponse;
			if (getAccountStateResponse == null)
			{
				return false;
			}
			if (HasState != getAccountStateResponse.HasState || (HasState && !State.Equals(getAccountStateResponse.State)))
			{
				return false;
			}
			if (HasTags != getAccountStateResponse.HasTags || (HasTags && !Tags.Equals(getAccountStateResponse.Tags)))
			{
				return false;
			}
			return true;
		}

		public static GetAccountStateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAccountStateResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetAccountStateResponse Deserialize(Stream stream, GetAccountStateResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetAccountStateResponse DeserializeLengthDelimited(Stream stream)
		{
			GetAccountStateResponse getAccountStateResponse = new GetAccountStateResponse();
			DeserializeLengthDelimited(stream, getAccountStateResponse);
			return getAccountStateResponse;
		}

		public static GetAccountStateResponse DeserializeLengthDelimited(Stream stream, GetAccountStateResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetAccountStateResponse Deserialize(Stream stream, GetAccountStateResponse instance, long limit)
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
						instance.State = AccountState.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountState.DeserializeLengthDelimited(stream, instance.State);
					}
					continue;
				case 18:
					if (instance.Tags == null)
					{
						instance.Tags = AccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountFieldTags.DeserializeLengthDelimited(stream, instance.Tags);
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

		public static void Serialize(Stream stream, GetAccountStateResponse instance)
		{
			if (instance.HasState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				AccountState.Serialize(stream, instance.State);
			}
			if (instance.HasTags)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Tags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.Tags);
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
