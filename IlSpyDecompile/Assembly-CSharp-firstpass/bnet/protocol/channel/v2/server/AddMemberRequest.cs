using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	public class AddMemberRequest : IProtoBuf
	{
		public bool HasChannelId;

		private bnet.protocol.channel.v1.ChannelId _ChannelId;

		public bool HasMember;

		private CreateMemberOptions _Member;

		public bnet.protocol.channel.v1.ChannelId ChannelId
		{
			get
			{
				return _ChannelId;
			}
			set
			{
				_ChannelId = value;
				HasChannelId = value != null;
			}
		}

		public CreateMemberOptions Member
		{
			get
			{
				return _Member;
			}
			set
			{
				_Member = value;
				HasMember = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetChannelId(bnet.protocol.channel.v1.ChannelId val)
		{
			ChannelId = val;
		}

		public void SetMember(CreateMemberOptions val)
		{
			Member = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasMember)
			{
				num ^= Member.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AddMemberRequest addMemberRequest = obj as AddMemberRequest;
			if (addMemberRequest == null)
			{
				return false;
			}
			if (HasChannelId != addMemberRequest.HasChannelId || (HasChannelId && !ChannelId.Equals(addMemberRequest.ChannelId)))
			{
				return false;
			}
			if (HasMember != addMemberRequest.HasMember || (HasMember && !Member.Equals(addMemberRequest.Member)))
			{
				return false;
			}
			return true;
		}

		public static AddMemberRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddMemberRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AddMemberRequest Deserialize(Stream stream, AddMemberRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AddMemberRequest DeserializeLengthDelimited(Stream stream)
		{
			AddMemberRequest addMemberRequest = new AddMemberRequest();
			DeserializeLengthDelimited(stream, addMemberRequest);
			return addMemberRequest;
		}

		public static AddMemberRequest DeserializeLengthDelimited(Stream stream, AddMemberRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AddMemberRequest Deserialize(Stream stream, AddMemberRequest instance, long limit)
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
					if (instance.ChannelId == null)
					{
						instance.ChannelId = bnet.protocol.channel.v1.ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.channel.v1.ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 18:
					if (instance.Member == null)
					{
						instance.Member = CreateMemberOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						CreateMemberOptions.DeserializeLengthDelimited(stream, instance.Member);
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

		public static void Serialize(Stream stream, AddMemberRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				bnet.protocol.channel.v1.ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasMember)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Member.GetSerializedSize());
				CreateMemberOptions.Serialize(stream, instance.Member);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasChannelId)
			{
				num++;
				uint serializedSize = ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasMember)
			{
				num++;
				uint serializedSize2 = Member.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
