using System.IO;

namespace bnet.protocol.channel.v1
{
	public class JoinChannelResponse : IProtoBuf
	{
		public bool HasObjectId;

		private ulong _ObjectId;

		public bool HasAlreadyMember;

		private bool _AlreadyMember;

		public bool HasMemberId;

		private EntityId _MemberId;

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

		public bool AlreadyMember
		{
			get
			{
				return _AlreadyMember;
			}
			set
			{
				_AlreadyMember = value;
				HasAlreadyMember = true;
			}
		}

		public EntityId MemberId
		{
			get
			{
				return _MemberId;
			}
			set
			{
				_MemberId = value;
				HasMemberId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
		}

		public void SetAlreadyMember(bool val)
		{
			AlreadyMember = val;
		}

		public void SetMemberId(EntityId val)
		{
			MemberId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasObjectId)
			{
				num ^= ObjectId.GetHashCode();
			}
			if (HasAlreadyMember)
			{
				num ^= AlreadyMember.GetHashCode();
			}
			if (HasMemberId)
			{
				num ^= MemberId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			JoinChannelResponse joinChannelResponse = obj as JoinChannelResponse;
			if (joinChannelResponse == null)
			{
				return false;
			}
			if (HasObjectId != joinChannelResponse.HasObjectId || (HasObjectId && !ObjectId.Equals(joinChannelResponse.ObjectId)))
			{
				return false;
			}
			if (HasAlreadyMember != joinChannelResponse.HasAlreadyMember || (HasAlreadyMember && !AlreadyMember.Equals(joinChannelResponse.AlreadyMember)))
			{
				return false;
			}
			if (HasMemberId != joinChannelResponse.HasMemberId || (HasMemberId && !MemberId.Equals(joinChannelResponse.MemberId)))
			{
				return false;
			}
			return true;
		}

		public static JoinChannelResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinChannelResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static JoinChannelResponse Deserialize(Stream stream, JoinChannelResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static JoinChannelResponse DeserializeLengthDelimited(Stream stream)
		{
			JoinChannelResponse joinChannelResponse = new JoinChannelResponse();
			DeserializeLengthDelimited(stream, joinChannelResponse);
			return joinChannelResponse;
		}

		public static JoinChannelResponse DeserializeLengthDelimited(Stream stream, JoinChannelResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static JoinChannelResponse Deserialize(Stream stream, JoinChannelResponse instance, long limit)
		{
			instance.AlreadyMember = false;
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
				case 32:
					instance.AlreadyMember = ProtocolParser.ReadBool(stream);
					continue;
				case 42:
					if (instance.MemberId == null)
					{
						instance.MemberId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.MemberId);
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

		public static void Serialize(Stream stream, JoinChannelResponse instance)
		{
			if (instance.HasObjectId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
			if (instance.HasAlreadyMember)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.AlreadyMember);
			}
			if (instance.HasMemberId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				EntityId.Serialize(stream, instance.MemberId);
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
			if (HasAlreadyMember)
			{
				num++;
				num++;
			}
			if (HasMemberId)
			{
				num++;
				uint serializedSize = MemberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
