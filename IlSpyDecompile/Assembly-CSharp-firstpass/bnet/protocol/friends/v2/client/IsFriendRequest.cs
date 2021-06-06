using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class IsFriendRequest : IProtoBuf
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
			IsFriendRequest isFriendRequest = obj as IsFriendRequest;
			if (isFriendRequest == null)
			{
				return false;
			}
			if (HasTargetAccountId != isFriendRequest.HasTargetAccountId || (HasTargetAccountId && !TargetAccountId.Equals(isFriendRequest.TargetAccountId)))
			{
				return false;
			}
			return true;
		}

		public static IsFriendRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IsFriendRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static IsFriendRequest Deserialize(Stream stream, IsFriendRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static IsFriendRequest DeserializeLengthDelimited(Stream stream)
		{
			IsFriendRequest isFriendRequest = new IsFriendRequest();
			DeserializeLengthDelimited(stream, isFriendRequest);
			return isFriendRequest;
		}

		public static IsFriendRequest DeserializeLengthDelimited(Stream stream, IsFriendRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static IsFriendRequest Deserialize(Stream stream, IsFriendRequest instance, long limit)
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

		public static void Serialize(Stream stream, IsFriendRequest instance)
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
