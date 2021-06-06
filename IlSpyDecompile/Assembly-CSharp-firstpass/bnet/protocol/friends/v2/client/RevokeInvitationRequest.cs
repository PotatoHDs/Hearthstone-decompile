using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class RevokeInvitationRequest : IProtoBuf
	{
		public bool HasInvitationId;

		private ulong _InvitationId;

		public ulong InvitationId
		{
			get
			{
				return _InvitationId;
			}
			set
			{
				_InvitationId = value;
				HasInvitationId = true;
			}
		}

		public bool IsInitialized => true;

		public void SetInvitationId(ulong val)
		{
			InvitationId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasInvitationId)
			{
				num ^= InvitationId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RevokeInvitationRequest revokeInvitationRequest = obj as RevokeInvitationRequest;
			if (revokeInvitationRequest == null)
			{
				return false;
			}
			if (HasInvitationId != revokeInvitationRequest.HasInvitationId || (HasInvitationId && !InvitationId.Equals(revokeInvitationRequest.InvitationId)))
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
				case 16:
					instance.InvitationId = ProtocolParser.ReadUInt64(stream);
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
			if (instance.HasInvitationId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.InvitationId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasInvitationId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(InvitationId);
			}
			return num;
		}
	}
}
