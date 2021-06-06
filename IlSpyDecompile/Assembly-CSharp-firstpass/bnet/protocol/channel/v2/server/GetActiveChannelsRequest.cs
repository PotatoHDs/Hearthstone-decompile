using System.IO;
using System.Text;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	public class GetActiveChannelsRequest : IProtoBuf
	{
		public bool HasCollectionId;

		private string _CollectionId;

		public bool HasType;

		private bnet.protocol.channel.v1.UniqueChannelType _Type;

		public bool HasContinuation;

		private ulong _Continuation;

		public string CollectionId
		{
			get
			{
				return _CollectionId;
			}
			set
			{
				_CollectionId = value;
				HasCollectionId = value != null;
			}
		}

		public bnet.protocol.channel.v1.UniqueChannelType Type
		{
			get
			{
				return _Type;
			}
			set
			{
				_Type = value;
				HasType = value != null;
			}
		}

		public ulong Continuation
		{
			get
			{
				return _Continuation;
			}
			set
			{
				_Continuation = value;
				HasContinuation = true;
			}
		}

		public bool IsInitialized => true;

		public void SetCollectionId(string val)
		{
			CollectionId = val;
		}

		public void SetType(bnet.protocol.channel.v1.UniqueChannelType val)
		{
			Type = val;
		}

		public void SetContinuation(ulong val)
		{
			Continuation = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasCollectionId)
			{
				num ^= CollectionId.GetHashCode();
			}
			if (HasType)
			{
				num ^= Type.GetHashCode();
			}
			if (HasContinuation)
			{
				num ^= Continuation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetActiveChannelsRequest getActiveChannelsRequest = obj as GetActiveChannelsRequest;
			if (getActiveChannelsRequest == null)
			{
				return false;
			}
			if (HasCollectionId != getActiveChannelsRequest.HasCollectionId || (HasCollectionId && !CollectionId.Equals(getActiveChannelsRequest.CollectionId)))
			{
				return false;
			}
			if (HasType != getActiveChannelsRequest.HasType || (HasType && !Type.Equals(getActiveChannelsRequest.Type)))
			{
				return false;
			}
			if (HasContinuation != getActiveChannelsRequest.HasContinuation || (HasContinuation && !Continuation.Equals(getActiveChannelsRequest.Continuation)))
			{
				return false;
			}
			return true;
		}

		public static GetActiveChannelsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetActiveChannelsRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetActiveChannelsRequest Deserialize(Stream stream, GetActiveChannelsRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetActiveChannelsRequest DeserializeLengthDelimited(Stream stream)
		{
			GetActiveChannelsRequest getActiveChannelsRequest = new GetActiveChannelsRequest();
			DeserializeLengthDelimited(stream, getActiveChannelsRequest);
			return getActiveChannelsRequest;
		}

		public static GetActiveChannelsRequest DeserializeLengthDelimited(Stream stream, GetActiveChannelsRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetActiveChannelsRequest Deserialize(Stream stream, GetActiveChannelsRequest instance, long limit)
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
					instance.CollectionId = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					if (instance.Type == null)
					{
						instance.Type = bnet.protocol.channel.v1.UniqueChannelType.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.channel.v1.UniqueChannelType.DeserializeLengthDelimited(stream, instance.Type);
					}
					continue;
				case 24:
					instance.Continuation = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GetActiveChannelsRequest instance)
		{
			if (instance.HasCollectionId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CollectionId));
			}
			if (instance.HasType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Type.GetSerializedSize());
				bnet.protocol.channel.v1.UniqueChannelType.Serialize(stream, instance.Type);
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasCollectionId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(CollectionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasType)
			{
				num++;
				uint serializedSize = Type.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasContinuation)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Continuation);
			}
			return num;
		}
	}
}
