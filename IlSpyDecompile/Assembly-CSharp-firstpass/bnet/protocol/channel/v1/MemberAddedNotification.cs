using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class MemberAddedNotification : IProtoBuf
	{
		public bool HasChannelId;

		private ChannelId _ChannelId;

		public bool HasSubscriberId;

		private SubscriberId _SubscriberId;

		public Member Member { get; set; }

		public ChannelId ChannelId
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

		public SubscriberId SubscriberId
		{
			get
			{
				return _SubscriberId;
			}
			set
			{
				_SubscriberId = value;
				HasSubscriberId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetMember(Member val)
		{
			Member = val;
		}

		public void SetChannelId(ChannelId val)
		{
			ChannelId = val;
		}

		public void SetSubscriberId(SubscriberId val)
		{
			SubscriberId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Member.GetHashCode();
			if (HasChannelId)
			{
				hashCode ^= ChannelId.GetHashCode();
			}
			if (HasSubscriberId)
			{
				hashCode ^= SubscriberId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			MemberAddedNotification memberAddedNotification = obj as MemberAddedNotification;
			if (memberAddedNotification == null)
			{
				return false;
			}
			if (!Member.Equals(memberAddedNotification.Member))
			{
				return false;
			}
			if (HasChannelId != memberAddedNotification.HasChannelId || (HasChannelId && !ChannelId.Equals(memberAddedNotification.ChannelId)))
			{
				return false;
			}
			if (HasSubscriberId != memberAddedNotification.HasSubscriberId || (HasSubscriberId && !SubscriberId.Equals(memberAddedNotification.SubscriberId)))
			{
				return false;
			}
			return true;
		}

		public static MemberAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberAddedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MemberAddedNotification Deserialize(Stream stream, MemberAddedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MemberAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			MemberAddedNotification memberAddedNotification = new MemberAddedNotification();
			DeserializeLengthDelimited(stream, memberAddedNotification);
			return memberAddedNotification;
		}

		public static MemberAddedNotification DeserializeLengthDelimited(Stream stream, MemberAddedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MemberAddedNotification Deserialize(Stream stream, MemberAddedNotification instance, long limit)
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
					if (instance.Member == null)
					{
						instance.Member = Member.DeserializeLengthDelimited(stream);
					}
					else
					{
						Member.DeserializeLengthDelimited(stream, instance.Member);
					}
					continue;
				case 18:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 26:
					if (instance.SubscriberId == null)
					{
						instance.SubscriberId = SubscriberId.DeserializeLengthDelimited(stream);
					}
					else
					{
						SubscriberId.DeserializeLengthDelimited(stream, instance.SubscriberId);
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

		public static void Serialize(Stream stream, MemberAddedNotification instance)
		{
			if (instance.Member == null)
			{
				throw new ArgumentNullException("Member", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Member.GetSerializedSize());
			Member.Serialize(stream, instance.Member);
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				SubscriberId.Serialize(stream, instance.SubscriberId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Member.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasChannelId)
			{
				num++;
				uint serializedSize2 = ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasSubscriberId)
			{
				num++;
				uint serializedSize3 = SubscriberId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 1;
		}
	}
}
