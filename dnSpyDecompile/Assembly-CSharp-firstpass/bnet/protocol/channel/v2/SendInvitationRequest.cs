using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200045E RID: 1118
	public class SendInvitationRequest : IProtoBuf
	{
		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06004C77 RID: 19575 RVA: 0x000EDA85 File Offset: 0x000EBC85
		// (set) Token: 0x06004C78 RID: 19576 RVA: 0x000EDA8D File Offset: 0x000EBC8D
		public GameAccountHandle AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		// Token: 0x06004C79 RID: 19577 RVA: 0x000EDAA0 File Offset: 0x000EBCA0
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06004C7A RID: 19578 RVA: 0x000EDAA9 File Offset: 0x000EBCA9
		// (set) Token: 0x06004C7B RID: 19579 RVA: 0x000EDAB1 File Offset: 0x000EBCB1
		public SendInvitationOptions Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
				this.HasOptions = (value != null);
			}
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x000EDAC4 File Offset: 0x000EBCC4
		public void SetOptions(SendInvitationOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x000EDAD0 File Offset: 0x000EBCD0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x000EDB18 File Offset: 0x000EBD18
		public override bool Equals(object obj)
		{
			SendInvitationRequest sendInvitationRequest = obj as SendInvitationRequest;
			return sendInvitationRequest != null && this.HasAgentId == sendInvitationRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(sendInvitationRequest.AgentId)) && this.HasOptions == sendInvitationRequest.HasOptions && (!this.HasOptions || this.Options.Equals(sendInvitationRequest.Options));
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06004C7F RID: 19583 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x000EDB88 File Offset: 0x000EBD88
		public static SendInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x06004C81 RID: 19585 RVA: 0x000EDB92 File Offset: 0x000EBD92
		public void Deserialize(Stream stream)
		{
			SendInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x000EDB9C File Offset: 0x000EBD9C
		public static SendInvitationRequest Deserialize(Stream stream, SendInvitationRequest instance)
		{
			return SendInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004C83 RID: 19587 RVA: 0x000EDBA8 File Offset: 0x000EBDA8
		public static SendInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			SendInvitationRequest sendInvitationRequest = new SendInvitationRequest();
			SendInvitationRequest.DeserializeLengthDelimited(stream, sendInvitationRequest);
			return sendInvitationRequest;
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x000EDBC4 File Offset: 0x000EBDC4
		public static SendInvitationRequest DeserializeLengthDelimited(Stream stream, SendInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x000EDBEC File Offset: 0x000EBDEC
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Options == null)
					{
						instance.Options = SendInvitationOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						SendInvitationOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004C86 RID: 19590 RVA: 0x000EDCBE File Offset: 0x000EBEBE
		public void Serialize(Stream stream)
		{
			SendInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x06004C87 RID: 19591 RVA: 0x000EDCC8 File Offset: 0x000EBEC8
		public static void Serialize(Stream stream, SendInvitationRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				SendInvitationOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06004C88 RID: 19592 RVA: 0x000EDD30 File Offset: 0x000EBF30
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize2 = this.Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x040018EE RID: 6382
		public bool HasAgentId;

		// Token: 0x040018EF RID: 6383
		private GameAccountHandle _AgentId;

		// Token: 0x040018F0 RID: 6384
		public bool HasOptions;

		// Token: 0x040018F1 RID: 6385
		private SendInvitationOptions _Options;
	}
}
