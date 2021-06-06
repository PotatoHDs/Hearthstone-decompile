using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class RevokeInvitationRequest : IProtoBuf
	{
		public ulong InvitationId { get; set; }

		public EntityId ChannelId { get; set; }

		public bool IsInitialized => true;

		public void SetInvitationId(ulong val)
		{
			InvitationId = val;
		}

		public void SetChannelId(EntityId val)
		{
			ChannelId = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ InvitationId.GetHashCode() ^ ChannelId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			RevokeInvitationRequest revokeInvitationRequest = obj as RevokeInvitationRequest;
			if (revokeInvitationRequest == null)
			{
				return false;
			}
			if (!InvitationId.Equals(revokeInvitationRequest.InvitationId))
			{
				return false;
			}
			if (!ChannelId.Equals(revokeInvitationRequest.ChannelId))
			{
				return false;
			}
			return true;
		}

		public static RevokeInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RevokeInvitationRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RevokeInvitationRequest Deserialize(Stream stream, RevokeInvitationRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RevokeInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			RevokeInvitationRequest revokeInvitationRequest = new RevokeInvitationRequest();
			DeserializeLengthDelimited(stream, revokeInvitationRequest);
			return revokeInvitationRequest;
		}

		public static RevokeInvitationRequest DeserializeLengthDelimited(Stream stream, RevokeInvitationRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RevokeInvitationRequest Deserialize(Stream stream, RevokeInvitationRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 25:
					instance.InvitationId = binaryReader.ReadUInt64();
					continue;
				case 34:
					if (instance.ChannelId == null)
					{
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
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

		public static void Serialize(Stream stream, RevokeInvitationRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(25);
			binaryWriter.Write(instance.InvitationId);
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(34);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
		}

		public uint GetSerializedSize()
		{
			int num = 0 + 8;
			uint serializedSize = ChannelId.GetSerializedSize();
			return (uint)(num + (int)(serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 2);
		}
	}
}
