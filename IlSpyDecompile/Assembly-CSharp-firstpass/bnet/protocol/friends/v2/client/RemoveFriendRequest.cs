using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class RemoveFriendRequest : IProtoBuf
	{
		public bool HasTargetAccountId;

		private ulong _TargetAccountId;

		public ulong TargetAccountId
		{
			get
			{
				return _TargetAccountId;
			}
			set
			{
				_TargetAccountId = value;
				HasTargetAccountId = true;
			}
		}

		public bool IsInitialized => true;

		public void SetTargetAccountId(ulong val)
		{
			TargetAccountId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTargetAccountId)
			{
				num ^= TargetAccountId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RemoveFriendRequest removeFriendRequest = obj as RemoveFriendRequest;
			if (removeFriendRequest == null)
			{
				return false;
			}
			if (HasTargetAccountId != removeFriendRequest.HasTargetAccountId || (HasTargetAccountId && !TargetAccountId.Equals(removeFriendRequest.TargetAccountId)))
			{
				return false;
			}
			return true;
		}

		public static RemoveFriendRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveFriendRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RemoveFriendRequest Deserialize(Stream stream, RemoveFriendRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RemoveFriendRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveFriendRequest removeFriendRequest = new RemoveFriendRequest();
			DeserializeLengthDelimited(stream, removeFriendRequest);
			return removeFriendRequest;
		}

		public static RemoveFriendRequest DeserializeLengthDelimited(Stream stream, RemoveFriendRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RemoveFriendRequest Deserialize(Stream stream, RemoveFriendRequest instance, long limit)
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
				case 16:
					instance.TargetAccountId = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, RemoveFriendRequest instance)
		{
			if (instance.HasTargetAccountId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.TargetAccountId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTargetAccountId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(TargetAccountId);
			}
			return num;
		}
	}
}
