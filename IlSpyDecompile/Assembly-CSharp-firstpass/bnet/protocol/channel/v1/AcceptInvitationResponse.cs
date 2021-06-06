using System.IO;

namespace bnet.protocol.channel.v1
{
	public class AcceptInvitationResponse : IProtoBuf
	{
		public ulong ObjectId { get; set; }

		public bool IsInitialized => true;

		public void SetObjectId(ulong val)
		{
			ObjectId = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ObjectId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			AcceptInvitationResponse acceptInvitationResponse = obj as AcceptInvitationResponse;
			if (acceptInvitationResponse == null)
			{
				return false;
			}
			if (!ObjectId.Equals(acceptInvitationResponse.ObjectId))
			{
				return false;
			}
			return true;
		}

		public static AcceptInvitationResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AcceptInvitationResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AcceptInvitationResponse Deserialize(Stream stream, AcceptInvitationResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AcceptInvitationResponse DeserializeLengthDelimited(Stream stream)
		{
			AcceptInvitationResponse acceptInvitationResponse = new AcceptInvitationResponse();
			DeserializeLengthDelimited(stream, acceptInvitationResponse);
			return acceptInvitationResponse;
		}

		public static AcceptInvitationResponse DeserializeLengthDelimited(Stream stream, AcceptInvitationResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AcceptInvitationResponse Deserialize(Stream stream, AcceptInvitationResponse instance, long limit)
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

		public static void Serialize(Stream stream, AcceptInvitationResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64(ObjectId) + 1;
		}
	}
}
