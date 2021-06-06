using System.IO;

namespace bnet.protocol.presence.v1
{
	public class SubscribeResult : IProtoBuf
	{
		public bool HasEntityId;

		private EntityId _EntityId;

		public bool HasResult;

		private uint _Result;

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

		public uint Result
		{
			get
			{
				return _Result;
			}
			set
			{
				_Result = value;
				HasResult = true;
			}
		}

		public bool IsInitialized => true;

		public void SetEntityId(EntityId val)
		{
			EntityId = val;
		}

		public void SetResult(uint val)
		{
			Result = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasEntityId)
			{
				num ^= EntityId.GetHashCode();
			}
			if (HasResult)
			{
				num ^= Result.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeResult subscribeResult = obj as SubscribeResult;
			if (subscribeResult == null)
			{
				return false;
			}
			if (HasEntityId != subscribeResult.HasEntityId || (HasEntityId && !EntityId.Equals(subscribeResult.EntityId)))
			{
				return false;
			}
			if (HasResult != subscribeResult.HasResult || (HasResult && !Result.Equals(subscribeResult.Result)))
			{
				return false;
			}
			return true;
		}

		public static SubscribeResult ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResult>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscribeResult Deserialize(Stream stream, SubscribeResult instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscribeResult DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResult subscribeResult = new SubscribeResult();
			DeserializeLengthDelimited(stream, subscribeResult);
			return subscribeResult;
		}

		public static SubscribeResult DeserializeLengthDelimited(Stream stream, SubscribeResult instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscribeResult Deserialize(Stream stream, SubscribeResult instance, long limit)
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
				case 16:
					instance.Result = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, SubscribeResult instance)
		{
			if (instance.HasEntityId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
			if (instance.HasResult)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Result);
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
			if (HasResult)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Result);
			}
			return num;
		}
	}
}
