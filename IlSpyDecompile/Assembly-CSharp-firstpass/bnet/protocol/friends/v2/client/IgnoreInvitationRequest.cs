using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class IgnoreInvitationRequest : IProtoBuf
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
			IgnoreInvitationRequest ignoreInvitationRequest = obj as IgnoreInvitationRequest;
			if (ignoreInvitationRequest == null)
			{
				return false;
			}
			if (HasInvitationId != ignoreInvitationRequest.HasInvitationId || (HasInvitationId && !InvitationId.Equals(ignoreInvitationRequest.InvitationId)))
			{
				return false;
			}
			return true;
		}

		public static IgnoreInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IgnoreInvitationRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static IgnoreInvitationRequest Deserialize(Stream stream, IgnoreInvitationRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static IgnoreInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			IgnoreInvitationRequest ignoreInvitationRequest = new IgnoreInvitationRequest();
			DeserializeLengthDelimited(stream, ignoreInvitationRequest);
			return ignoreInvitationRequest;
		}

		public static IgnoreInvitationRequest DeserializeLengthDelimited(Stream stream, IgnoreInvitationRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static IgnoreInvitationRequest Deserialize(Stream stream, IgnoreInvitationRequest instance, long limit)
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
				case 24:
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

		public static void Serialize(Stream stream, IgnoreInvitationRequest instance)
		{
			if (instance.HasInvitationId)
			{
				stream.WriteByte(24);
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
