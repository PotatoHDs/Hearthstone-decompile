using System.IO;

namespace bnet.protocol.channel.v1
{
	public class SubscribeChannelResponse : IProtoBuf
	{
		public bool HasObjectId;

		private ulong _ObjectId;

		public ulong ObjectId
		{
			get
			{
				return _ObjectId;
			}
			set
			{
				_ObjectId = value;
				HasObjectId = true;
			}
		}

		public bool IsInitialized => true;

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasObjectId)
			{
				num ^= ObjectId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeChannelResponse subscribeChannelResponse = obj as SubscribeChannelResponse;
			if (subscribeChannelResponse == null)
			{
				return false;
			}
			if (HasObjectId != subscribeChannelResponse.HasObjectId || (HasObjectId && !ObjectId.Equals(subscribeChannelResponse.ObjectId)))
			{
				return false;
			}
			return true;
		}

		public static SubscribeChannelResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeChannelResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscribeChannelResponse Deserialize(Stream stream, SubscribeChannelResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscribeChannelResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeChannelResponse subscribeChannelResponse = new SubscribeChannelResponse();
			DeserializeLengthDelimited(stream, subscribeChannelResponse);
			return subscribeChannelResponse;
		}

		public static SubscribeChannelResponse DeserializeLengthDelimited(Stream stream, SubscribeChannelResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscribeChannelResponse Deserialize(Stream stream, SubscribeChannelResponse instance, long limit)
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
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SubscribeChannelResponse instance)
		{
			if (instance.HasObjectId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasObjectId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ObjectId);
			}
			return num;
		}
	}
}
