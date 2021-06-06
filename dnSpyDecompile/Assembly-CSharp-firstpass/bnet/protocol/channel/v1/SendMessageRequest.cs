using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004C7 RID: 1223
	public class SendMessageRequest : IProtoBuf
	{
		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x0600559F RID: 21919 RVA: 0x00106BE8 File Offset: 0x00104DE8
		// (set) Token: 0x060055A0 RID: 21920 RVA: 0x00106BF0 File Offset: 0x00104DF0
		public EntityId AgentId
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

		// Token: 0x060055A1 RID: 21921 RVA: 0x00106C03 File Offset: 0x00104E03
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x060055A2 RID: 21922 RVA: 0x00106C0C File Offset: 0x00104E0C
		// (set) Token: 0x060055A3 RID: 21923 RVA: 0x00106C14 File Offset: 0x00104E14
		public Message Message { get; set; }

		// Token: 0x060055A4 RID: 21924 RVA: 0x00106C1D File Offset: 0x00104E1D
		public void SetMessage(Message val)
		{
			this.Message = val;
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x060055A5 RID: 21925 RVA: 0x00106C26 File Offset: 0x00104E26
		// (set) Token: 0x060055A6 RID: 21926 RVA: 0x00106C2E File Offset: 0x00104E2E
		public ulong RequiredPrivileges
		{
			get
			{
				return this._RequiredPrivileges;
			}
			set
			{
				this._RequiredPrivileges = value;
				this.HasRequiredPrivileges = true;
			}
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x00106C3E File Offset: 0x00104E3E
		public void SetRequiredPrivileges(ulong val)
		{
			this.RequiredPrivileges = val;
		}

		// Token: 0x060055A8 RID: 21928 RVA: 0x00106C48 File Offset: 0x00104E48
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.Message.GetHashCode();
			if (this.HasRequiredPrivileges)
			{
				num ^= this.RequiredPrivileges.GetHashCode();
			}
			return num;
		}

		// Token: 0x060055A9 RID: 21929 RVA: 0x00106CA0 File Offset: 0x00104EA0
		public override bool Equals(object obj)
		{
			SendMessageRequest sendMessageRequest = obj as SendMessageRequest;
			return sendMessageRequest != null && this.HasAgentId == sendMessageRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(sendMessageRequest.AgentId)) && this.Message.Equals(sendMessageRequest.Message) && this.HasRequiredPrivileges == sendMessageRequest.HasRequiredPrivileges && (!this.HasRequiredPrivileges || this.RequiredPrivileges.Equals(sendMessageRequest.RequiredPrivileges));
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x060055AA RID: 21930 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060055AB RID: 21931 RVA: 0x00106D28 File Offset: 0x00104F28
		public static SendMessageRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendMessageRequest>(bs, 0, -1);
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x00106D32 File Offset: 0x00104F32
		public void Deserialize(Stream stream)
		{
			SendMessageRequest.Deserialize(stream, this);
		}

		// Token: 0x060055AD RID: 21933 RVA: 0x00106D3C File Offset: 0x00104F3C
		public static SendMessageRequest Deserialize(Stream stream, SendMessageRequest instance)
		{
			return SendMessageRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060055AE RID: 21934 RVA: 0x00106D48 File Offset: 0x00104F48
		public static SendMessageRequest DeserializeLengthDelimited(Stream stream)
		{
			SendMessageRequest sendMessageRequest = new SendMessageRequest();
			SendMessageRequest.DeserializeLengthDelimited(stream, sendMessageRequest);
			return sendMessageRequest;
		}

		// Token: 0x060055AF RID: 21935 RVA: 0x00106D64 File Offset: 0x00104F64
		public static SendMessageRequest DeserializeLengthDelimited(Stream stream, SendMessageRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendMessageRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x00106D8C File Offset: 0x00104F8C
		public static SendMessageRequest Deserialize(Stream stream, SendMessageRequest instance, long limit)
		{
			instance.RequiredPrivileges = 0UL;
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
						if (num != 24)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.RequiredPrivileges = ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (instance.Message == null)
					{
						instance.Message = Message.DeserializeLengthDelimited(stream);
					}
					else
					{
						Message.DeserializeLengthDelimited(stream, instance.Message);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x00106E7C File Offset: 0x0010507C
		public void Serialize(Stream stream)
		{
			SendMessageRequest.Serialize(stream, this);
		}

		// Token: 0x060055B2 RID: 21938 RVA: 0x00106E88 File Offset: 0x00105088
		public static void Serialize(Stream stream, SendMessageRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.Message == null)
			{
				throw new ArgumentNullException("Message", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Message.GetSerializedSize());
			Message.Serialize(stream, instance.Message);
			if (instance.HasRequiredPrivileges)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.RequiredPrivileges);
			}
		}

		// Token: 0x060055B3 RID: 21939 RVA: 0x00106F1C File Offset: 0x0010511C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.Message.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.HasRequiredPrivileges)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.RequiredPrivileges);
			}
			return num + 1U;
		}

		// Token: 0x04001B02 RID: 6914
		public bool HasAgentId;

		// Token: 0x04001B03 RID: 6915
		private EntityId _AgentId;

		// Token: 0x04001B05 RID: 6917
		public bool HasRequiredPrivileges;

		// Token: 0x04001B06 RID: 6918
		private ulong _RequiredPrivileges;
	}
}
