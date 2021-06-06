using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class SuggestInvitationRequest : IProtoBuf
	{
		public bool HasApprovalId;

		private EntityId _ApprovalId;

		public EntityId ChannelId { get; set; }

		public EntityId TargetId { get; set; }

		public EntityId ApprovalId
		{
			get
			{
				return _ApprovalId;
			}
			set
			{
				_ApprovalId = value;
				HasApprovalId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetChannelId(EntityId val)
		{
			ChannelId = val;
		}

		public void SetTargetId(EntityId val)
		{
			TargetId = val;
		}

		public void SetApprovalId(EntityId val)
		{
			ApprovalId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ChannelId.GetHashCode();
			hashCode ^= TargetId.GetHashCode();
			if (HasApprovalId)
			{
				hashCode ^= ApprovalId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			SuggestInvitationRequest suggestInvitationRequest = obj as SuggestInvitationRequest;
			if (suggestInvitationRequest == null)
			{
				return false;
			}
			if (!ChannelId.Equals(suggestInvitationRequest.ChannelId))
			{
				return false;
			}
			if (!TargetId.Equals(suggestInvitationRequest.TargetId))
			{
				return false;
			}
			if (HasApprovalId != suggestInvitationRequest.HasApprovalId || (HasApprovalId && !ApprovalId.Equals(suggestInvitationRequest.ApprovalId)))
			{
				return false;
			}
			return true;
		}

		public static SuggestInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SuggestInvitationRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SuggestInvitationRequest Deserialize(Stream stream, SuggestInvitationRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SuggestInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			SuggestInvitationRequest suggestInvitationRequest = new SuggestInvitationRequest();
			DeserializeLengthDelimited(stream, suggestInvitationRequest);
			return suggestInvitationRequest;
		}

		public static SuggestInvitationRequest DeserializeLengthDelimited(Stream stream, SuggestInvitationRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SuggestInvitationRequest Deserialize(Stream stream, SuggestInvitationRequest instance, long limit)
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
				case 18:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
					continue;
				case 26:
					if (instance.TargetId == null)
					{
						instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
					}
					continue;
				case 34:
					if (instance.ApprovalId == null)
					{
						instance.ApprovalId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ApprovalId);
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

		public static void Serialize(Stream stream, SuggestInvitationRequest instance)
		{
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
			if (instance.TargetId == null)
			{
				throw new ArgumentNullException("TargetId", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
			EntityId.Serialize(stream, instance.TargetId);
			if (instance.HasApprovalId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.ApprovalId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ApprovalId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = ChannelId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint serializedSize2 = TargetId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (HasApprovalId)
			{
				num++;
				uint serializedSize3 = ApprovalId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 2;
		}
	}
}
