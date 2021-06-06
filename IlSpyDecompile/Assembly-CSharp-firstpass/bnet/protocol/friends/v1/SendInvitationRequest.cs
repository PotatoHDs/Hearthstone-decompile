using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.friends.v1
{
	public class SendInvitationRequest : IProtoBuf
	{
		public bool HasAgentIdentity;

		private bnet.protocol.account.v1.Identity _AgentIdentity;

		public bnet.protocol.account.v1.Identity AgentIdentity
		{
			get
			{
				return _AgentIdentity;
			}
			set
			{
				_AgentIdentity = value;
				HasAgentIdentity = value != null;
			}
		}

		public EntityId TargetId { get; set; }

		public InvitationParams Params { get; set; }

		public bool IsInitialized => true;

		public void SetAgentIdentity(bnet.protocol.account.v1.Identity val)
		{
			AgentIdentity = val;
		}

		public void SetTargetId(EntityId val)
		{
			TargetId = val;
		}

		public void SetParams(InvitationParams val)
		{
			Params = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentIdentity)
			{
				num ^= AgentIdentity.GetHashCode();
			}
			num ^= TargetId.GetHashCode();
			return num ^ Params.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SendInvitationRequest sendInvitationRequest = obj as SendInvitationRequest;
			if (sendInvitationRequest == null)
			{
				return false;
			}
			if (HasAgentIdentity != sendInvitationRequest.HasAgentIdentity || (HasAgentIdentity && !AgentIdentity.Equals(sendInvitationRequest.AgentIdentity)))
			{
				return false;
			}
			if (!TargetId.Equals(sendInvitationRequest.TargetId))
			{
				return false;
			}
			if (!Params.Equals(sendInvitationRequest.Params))
			{
				return false;
			}
			return true;
		}

		public static SendInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendInvitationRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SendInvitationRequest Deserialize(Stream stream, SendInvitationRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SendInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			SendInvitationRequest sendInvitationRequest = new SendInvitationRequest();
			DeserializeLengthDelimited(stream, sendInvitationRequest);
			return sendInvitationRequest;
		}

		public static SendInvitationRequest DeserializeLengthDelimited(Stream stream, SendInvitationRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SendInvitationRequest Deserialize(Stream stream, SendInvitationRequest instance, long limit)
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
					if (instance.AgentIdentity == null)
					{
						instance.AgentIdentity = bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream, instance.AgentIdentity);
					}
					continue;
				case 18:
					if (instance.TargetId == null)
					{
						instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
					}
					continue;
				case 26:
					if (instance.Params == null)
					{
						instance.Params = InvitationParams.DeserializeLengthDelimited(stream);
					}
					else
					{
						InvitationParams.DeserializeLengthDelimited(stream, instance.Params);
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

		public static void Serialize(Stream stream, SendInvitationRequest instance)
		{
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				bnet.protocol.account.v1.Identity.Serialize(stream, instance.AgentIdentity);
			}
			if (instance.TargetId == null)
			{
				throw new ArgumentNullException("TargetId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
			EntityId.Serialize(stream, instance.TargetId);
			if (instance.Params == null)
			{
				throw new ArgumentNullException("Params", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.Params.GetSerializedSize());
			InvitationParams.Serialize(stream, instance.Params);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAgentIdentity)
			{
				num++;
				uint serializedSize = AgentIdentity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = TargetId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			uint serializedSize3 = Params.GetSerializedSize();
			num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			return num + 2;
		}
	}
}
