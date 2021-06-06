using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class RemovedFriendAssignment : IProtoBuf
	{
		public bool HasId;

		private ulong _Id;

		public ulong Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = true;
			}
		}

		public bool IsInitialized => true;

		public void SetId(ulong val)
		{
			Id = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RemovedFriendAssignment removedFriendAssignment = obj as RemovedFriendAssignment;
			if (removedFriendAssignment == null)
			{
				return false;
			}
			if (HasId != removedFriendAssignment.HasId || (HasId && !Id.Equals(removedFriendAssignment.Id)))
			{
				return false;
			}
			return true;
		}

		public static RemovedFriendAssignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemovedFriendAssignment>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RemovedFriendAssignment Deserialize(Stream stream, RemovedFriendAssignment instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RemovedFriendAssignment DeserializeLengthDelimited(Stream stream)
		{
			RemovedFriendAssignment removedFriendAssignment = new RemovedFriendAssignment();
			DeserializeLengthDelimited(stream, removedFriendAssignment);
			return removedFriendAssignment;
		}

		public static RemovedFriendAssignment DeserializeLengthDelimited(Stream stream, RemovedFriendAssignment instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RemovedFriendAssignment Deserialize(Stream stream, RemovedFriendAssignment instance, long limit)
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
				case 8:
					instance.Id = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, RemovedFriendAssignment instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Id);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Id);
			}
			return num;
		}
	}
}
