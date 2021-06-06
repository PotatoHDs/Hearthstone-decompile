using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class AcceptInvitationRequest : IProtoBuf
	{
		public bool HasInvitationId;

		private ulong _InvitationId;

		public bool HasOptions;

		private AcceptInvitationOptions _Options;

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

		public AcceptInvitationOptions Options
		{
			get
			{
				return _Options;
			}
			set
			{
				_Options = value;
				HasOptions = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetInvitationId(ulong val)
		{
			InvitationId = val;
		}

		public void SetOptions(AcceptInvitationOptions val)
		{
			Options = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasInvitationId)
			{
				num ^= InvitationId.GetHashCode();
			}
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AcceptInvitationRequest acceptInvitationRequest = obj as AcceptInvitationRequest;
			if (acceptInvitationRequest == null)
			{
				return false;
			}
			if (HasInvitationId != acceptInvitationRequest.HasInvitationId || (HasInvitationId && !InvitationId.Equals(acceptInvitationRequest.InvitationId)))
			{
				return false;
			}
			if (HasOptions != acceptInvitationRequest.HasOptions || (HasOptions && !Options.Equals(acceptInvitationRequest.Options)))
			{
				return false;
			}
			return true;
		}

		public static AcceptInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AcceptInvitationRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AcceptInvitationRequest Deserialize(Stream stream, AcceptInvitationRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AcceptInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			AcceptInvitationRequest acceptInvitationRequest = new AcceptInvitationRequest();
			DeserializeLengthDelimited(stream, acceptInvitationRequest);
			return acceptInvitationRequest;
		}

		public static AcceptInvitationRequest DeserializeLengthDelimited(Stream stream, AcceptInvitationRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AcceptInvitationRequest Deserialize(Stream stream, AcceptInvitationRequest instance, long limit)
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
				case 26:
					if (instance.Options == null)
					{
						instance.Options = AcceptInvitationOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						AcceptInvitationOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		public static void Serialize(Stream stream, AcceptInvitationRequest instance)
		{
			if (instance.HasInvitationId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.InvitationId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				AcceptInvitationOptions.Serialize(stream, instance.Options);
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
			if (HasOptions)
			{
				num++;
				uint serializedSize = Options.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
