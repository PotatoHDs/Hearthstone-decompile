using System;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.Types;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002E1 RID: 737
	public class SetTypingIndicatorRequest : IProtoBuf
	{
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x0009639F File Offset: 0x0009459F
		// (set) Token: 0x06002B78 RID: 11128 RVA: 0x000963A7 File Offset: 0x000945A7
		public AccountId AgentId
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

		// Token: 0x06002B79 RID: 11129 RVA: 0x000963BA File Offset: 0x000945BA
		public void SetAgentId(AccountId val)
		{
			this.AgentId = val;
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x000963C3 File Offset: 0x000945C3
		// (set) Token: 0x06002B7B RID: 11131 RVA: 0x000963CB File Offset: 0x000945CB
		public AccountId TargetId
		{
			get
			{
				return this._TargetId;
			}
			set
			{
				this._TargetId = value;
				this.HasTargetId = (value != null);
			}
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000963DE File Offset: 0x000945DE
		public void SetTargetId(AccountId val)
		{
			this.TargetId = val;
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x000963E7 File Offset: 0x000945E7
		// (set) Token: 0x06002B7E RID: 11134 RVA: 0x000963EF File Offset: 0x000945EF
		public TypingIndicator Action
		{
			get
			{
				return this._Action;
			}
			set
			{
				this._Action = value;
				this.HasAction = true;
			}
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000963FF File Offset: 0x000945FF
		public void SetAction(TypingIndicator val)
		{
			this.Action = val;
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x00096408 File Offset: 0x00094608
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasTargetId)
			{
				num ^= this.TargetId.GetHashCode();
			}
			if (this.HasAction)
			{
				num ^= this.Action.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x00096470 File Offset: 0x00094670
		public override bool Equals(object obj)
		{
			SetTypingIndicatorRequest setTypingIndicatorRequest = obj as SetTypingIndicatorRequest;
			return setTypingIndicatorRequest != null && this.HasAgentId == setTypingIndicatorRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(setTypingIndicatorRequest.AgentId)) && this.HasTargetId == setTypingIndicatorRequest.HasTargetId && (!this.HasTargetId || this.TargetId.Equals(setTypingIndicatorRequest.TargetId)) && this.HasAction == setTypingIndicatorRequest.HasAction && (!this.HasAction || this.Action.Equals(setTypingIndicatorRequest.Action));
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002B82 RID: 11138 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x00096519 File Offset: 0x00094719
		public static SetTypingIndicatorRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SetTypingIndicatorRequest>(bs, 0, -1);
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x00096523 File Offset: 0x00094723
		public void Deserialize(Stream stream)
		{
			SetTypingIndicatorRequest.Deserialize(stream, this);
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x0009652D File Offset: 0x0009472D
		public static SetTypingIndicatorRequest Deserialize(Stream stream, SetTypingIndicatorRequest instance)
		{
			return SetTypingIndicatorRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x00096538 File Offset: 0x00094738
		public static SetTypingIndicatorRequest DeserializeLengthDelimited(Stream stream)
		{
			SetTypingIndicatorRequest setTypingIndicatorRequest = new SetTypingIndicatorRequest();
			SetTypingIndicatorRequest.DeserializeLengthDelimited(stream, setTypingIndicatorRequest);
			return setTypingIndicatorRequest;
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x00096554 File Offset: 0x00094754
		public static SetTypingIndicatorRequest DeserializeLengthDelimited(Stream stream, SetTypingIndicatorRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetTypingIndicatorRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x0009657C File Offset: 0x0009477C
		public static SetTypingIndicatorRequest Deserialize(Stream stream, SetTypingIndicatorRequest instance, long limit)
		{
			instance.Action = TypingIndicator.TYPING_START;
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
							instance.Action = (TypingIndicator)ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (instance.TargetId == null)
					{
						instance.TargetId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.TargetId);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = AccountId.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountId.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x0009666C File Offset: 0x0009486C
		public void Serialize(Stream stream)
		{
			SetTypingIndicatorRequest.Serialize(stream, this);
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x00096678 File Offset: 0x00094878
		public static void Serialize(Stream stream, SetTypingIndicatorRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				AccountId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasTargetId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				AccountId.Serialize(stream, instance.TargetId);
			}
			if (instance.HasAction)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Action));
			}
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000966FC File Offset: 0x000948FC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasTargetId)
			{
				num += 1U;
				uint serializedSize2 = this.TargetId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasAction)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Action));
			}
			return num;
		}

		// Token: 0x04001229 RID: 4649
		public bool HasAgentId;

		// Token: 0x0400122A RID: 4650
		private AccountId _AgentId;

		// Token: 0x0400122B RID: 4651
		public bool HasTargetId;

		// Token: 0x0400122C RID: 4652
		private AccountId _TargetId;

		// Token: 0x0400122D RID: 4653
		public bool HasAction;

		// Token: 0x0400122E RID: 4654
		private TypingIndicator _Action;
	}
}
