using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v1;
using bnet.protocol.channel.v2.Types;

namespace bnet.protocol.channel.v2.server
{
	public class RemoveMemberRequest : IProtoBuf
	{
		public bool HasChannelId;

		private bnet.protocol.channel.v1.ChannelId _ChannelId;

		public bool HasReason;

		private ChannelRemovedReason _Reason;

		public bool HasMemberId;

		private GameAccountHandle _MemberId;

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

		public ChannelRemovedReason Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = true;
			}
		}

		public GameAccountHandle MemberId
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

		public void SetChannelId(bnet.protocol.channel.v1.ChannelId val)
		{
			ChannelId = val;
		}

		public void SetReason(ChannelRemovedReason val)
		{
			Reason = val;
		}

		public void SetMemberId(GameAccountHandle val)
		{
			MemberId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelId)
			{
				num ^= ChannelId.GetHashCode();
			}
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			if (HasMemberId)
			{
				num ^= MemberId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RemoveMemberRequest removeMemberRequest = obj as RemoveMemberRequest;
			if (removeMemberRequest == null)
			{
				return false;
			}
			if (HasChannelId != removeMemberRequest.HasChannelId || (HasChannelId && !ChannelId.Equals(removeMemberRequest.ChannelId)))
			{
				return false;
			}
			if (HasReason != removeMemberRequest.HasReason || (HasReason && !Reason.Equals(removeMemberRequest.Reason)))
			{
				return false;
			}
			if (HasMemberId != removeMemberRequest.HasMemberId || (HasMemberId && !MemberId.Equals(removeMemberRequest.MemberId)))
			{
				return false;
			}
			return true;
		}

		public static RemoveMemberRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveMemberRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RemoveMemberRequest Deserialize(Stream stream, RemoveMemberRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RemoveMemberRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveMemberRequest removeMemberRequest = new RemoveMemberRequest();
			DeserializeLengthDelimited(stream, removeMemberRequest);
			return removeMemberRequest;
		}

		public static RemoveMemberRequest DeserializeLengthDelimited(Stream stream, RemoveMemberRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RemoveMemberRequest Deserialize(Stream stream, RemoveMemberRequest instance, long limit)
		{
			instance.Reason = ChannelRemovedReason.CHANNEL_REMOVED_REASON_MEMBER_LEFT;
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
				case 16:
					instance.Reason = (ChannelRemovedReason)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					if (instance.MemberId == null)
					{
						instance.MemberId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.MemberId);
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

		public static void Serialize(Stream stream, RemoveMemberRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				bnet.protocol.channel.v1.ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Reason);
			}
			if (instance.HasMemberId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.MemberId);
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
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Reason);
			}
			if (HasMemberId)
			{
				num++;
				uint serializedSize2 = MemberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
