using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class RevokeAllInvitationsRequest : IProtoBuf
	{
		public bool IsInitialized => true;

		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is RevokeAllInvitationsRequest))
			{
				return false;
			}
			return true;
		}

		public static RevokeAllInvitationsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RevokeAllInvitationsRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RevokeAllInvitationsRequest Deserialize(Stream stream, RevokeAllInvitationsRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RevokeAllInvitationsRequest DeserializeLengthDelimited(Stream stream)
		{
			RevokeAllInvitationsRequest revokeAllInvitationsRequest = new RevokeAllInvitationsRequest();
			DeserializeLengthDelimited(stream, revokeAllInvitationsRequest);
			return revokeAllInvitationsRequest;
		}

		public static RevokeAllInvitationsRequest DeserializeLengthDelimited(Stream stream, RevokeAllInvitationsRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RevokeAllInvitationsRequest Deserialize(Stream stream, RevokeAllInvitationsRequest instance, long limit)
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
				if (num == -1)
				{
					if (limit < 0)
					{
						break;
					}
					throw new EndOfStreamException();
				}
				Key key = ProtocolParser.ReadKey((byte)num, stream);
				if (key.Field == 0)
				{
					throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
				}
				ProtocolParser.SkipKey(stream, key);
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, RevokeAllInvitationsRequest instance)
		{
		}

		public uint GetSerializedSize()
		{
			return 0u;
		}
	}
}
