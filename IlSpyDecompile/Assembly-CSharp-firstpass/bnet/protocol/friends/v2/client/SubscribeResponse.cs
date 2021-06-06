using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class SubscribeResponse : IProtoBuf
	{
		public bool HasState;

		private FriendsState _State;

		public FriendsState State
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

		public bool IsInitialized => true;

		public void SetState(FriendsState val)
		{
			State = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasState)
			{
				num ^= State.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			if (subscribeResponse == null)
			{
				return false;
			}
			if (HasState != subscribeResponse.HasState || (HasState && !State.Equals(subscribeResponse.State)))
			{
				return false;
			}
			return true;
		}

		public static SubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance, long limit)
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
						instance.State = FriendsState.DeserializeLengthDelimited(stream);
					}
					else
					{
						FriendsState.DeserializeLengthDelimited(stream, instance.State);
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

		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.HasState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				FriendsState.Serialize(stream, instance.State);
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
			return num;
		}
	}
}
