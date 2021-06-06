using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.friends.v1
{
	// Token: 0x0200041C RID: 1052
	public class SendInvitationRequest : IProtoBuf
	{
		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x0600464D RID: 17997 RVA: 0x000DC84C File Offset: 0x000DAA4C
		// (set) Token: 0x0600464E RID: 17998 RVA: 0x000DC854 File Offset: 0x000DAA54
		public Identity AgentIdentity
		{
			get
			{
				return this._AgentIdentity;
			}
			set
			{
				this._AgentIdentity = value;
				this.HasAgentIdentity = (value != null);
			}
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x000DC867 File Offset: 0x000DAA67
		public void SetAgentIdentity(Identity val)
		{
			this.AgentIdentity = val;
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06004650 RID: 18000 RVA: 0x000DC870 File Offset: 0x000DAA70
		// (set) Token: 0x06004651 RID: 18001 RVA: 0x000DC878 File Offset: 0x000DAA78
		public EntityId TargetId { get; set; }

		// Token: 0x06004652 RID: 18002 RVA: 0x000DC881 File Offset: 0x000DAA81
		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06004653 RID: 18003 RVA: 0x000DC88A File Offset: 0x000DAA8A
		// (set) Token: 0x06004654 RID: 18004 RVA: 0x000DC892 File Offset: 0x000DAA92
		public InvitationParams Params { get; set; }

		// Token: 0x06004655 RID: 18005 RVA: 0x000DC89B File Offset: 0x000DAA9B
		public void SetParams(InvitationParams val)
		{
			this.Params = val;
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x000DC8A4 File Offset: 0x000DAAA4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentIdentity)
			{
				num ^= this.AgentIdentity.GetHashCode();
			}
			num ^= this.TargetId.GetHashCode();
			return num ^ this.Params.GetHashCode();
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x000DC8F0 File Offset: 0x000DAAF0
		public override bool Equals(object obj)
		{
			SendInvitationRequest sendInvitationRequest = obj as SendInvitationRequest;
			return sendInvitationRequest != null && this.HasAgentIdentity == sendInvitationRequest.HasAgentIdentity && (!this.HasAgentIdentity || this.AgentIdentity.Equals(sendInvitationRequest.AgentIdentity)) && this.TargetId.Equals(sendInvitationRequest.TargetId) && this.Params.Equals(sendInvitationRequest.Params);
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06004658 RID: 18008 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x000DC95F File Offset: 0x000DAB5F
		public static SendInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x000DC969 File Offset: 0x000DAB69
		public void Deserialize(Stream stream)
		{
			SendInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x000DC973 File Offset: 0x000DAB73
		public static SendInvitationRequest Deserialize(Stream stream, SendInvitationRequest instance)
		{
			return SendInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x000DC980 File Offset: 0x000DAB80
		public static SendInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			SendInvitationRequest sendInvitationRequest = new SendInvitationRequest();
			SendInvitationRequest.DeserializeLengthDelimited(stream, sendInvitationRequest);
			return sendInvitationRequest;
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x000DC99C File Offset: 0x000DAB9C
		public static SendInvitationRequest DeserializeLengthDelimited(Stream stream, SendInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x000DC9C4 File Offset: 0x000DABC4
		public static SendInvitationRequest Deserialize(Stream stream, SendInvitationRequest instance, long limit)
		{
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.Params == null)
						{
							instance.Params = InvitationParams.DeserializeLengthDelimited(stream);
						}
						else
						{
							InvitationParams.DeserializeLengthDelimited(stream, instance.Params);
						}
					}
					else if (instance.TargetId == null)
					{
						instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
					}
				}
				else if (instance.AgentIdentity == null)
				{
					instance.AgentIdentity = Identity.DeserializeLengthDelimited(stream);
				}
				else
				{
					Identity.DeserializeLengthDelimited(stream, instance.AgentIdentity);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x000DCAC6 File Offset: 0x000DACC6
		public void Serialize(Stream stream)
		{
			SendInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x000DCAD0 File Offset: 0x000DACD0
		public static void Serialize(Stream stream, SendInvitationRequest instance)
		{
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				Identity.Serialize(stream, instance.AgentIdentity);
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

		// Token: 0x06004661 RID: 18017 RVA: 0x000DCB84 File Offset: 0x000DAD84
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentIdentity)
			{
				num += 1U;
				uint serializedSize = this.AgentIdentity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.TargetId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			uint serializedSize3 = this.Params.GetSerializedSize();
			num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			return num + 2U;
		}

		// Token: 0x04001796 RID: 6038
		public bool HasAgentIdentity;

		// Token: 0x04001797 RID: 6039
		private Identity _AgentIdentity;
	}
}
