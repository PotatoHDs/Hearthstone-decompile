using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002E2 RID: 738
	public class AdvanceViewTimeRequest : IProtoBuf
	{
		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x0009676D File Offset: 0x0009496D
		// (set) Token: 0x06002B8E RID: 11150 RVA: 0x00096775 File Offset: 0x00094975
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

		// Token: 0x06002B8F RID: 11151 RVA: 0x00096788 File Offset: 0x00094988
		public void SetAgentId(AccountId val)
		{
			this.AgentId = val;
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002B90 RID: 11152 RVA: 0x00096791 File Offset: 0x00094991
		// (set) Token: 0x06002B91 RID: 11153 RVA: 0x00096799 File Offset: 0x00094999
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

		// Token: 0x06002B92 RID: 11154 RVA: 0x000967AC File Offset: 0x000949AC
		public void SetTargetId(AccountId val)
		{
			this.TargetId = val;
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000967B8 File Offset: 0x000949B8
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
			return num;
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x00096800 File Offset: 0x00094A00
		public override bool Equals(object obj)
		{
			AdvanceViewTimeRequest advanceViewTimeRequest = obj as AdvanceViewTimeRequest;
			return advanceViewTimeRequest != null && this.HasAgentId == advanceViewTimeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(advanceViewTimeRequest.AgentId)) && this.HasTargetId == advanceViewTimeRequest.HasTargetId && (!this.HasTargetId || this.TargetId.Equals(advanceViewTimeRequest.TargetId));
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x00096870 File Offset: 0x00094A70
		public static AdvanceViewTimeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AdvanceViewTimeRequest>(bs, 0, -1);
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x0009687A File Offset: 0x00094A7A
		public void Deserialize(Stream stream)
		{
			AdvanceViewTimeRequest.Deserialize(stream, this);
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x00096884 File Offset: 0x00094A84
		public static AdvanceViewTimeRequest Deserialize(Stream stream, AdvanceViewTimeRequest instance)
		{
			return AdvanceViewTimeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x00096890 File Offset: 0x00094A90
		public static AdvanceViewTimeRequest DeserializeLengthDelimited(Stream stream)
		{
			AdvanceViewTimeRequest advanceViewTimeRequest = new AdvanceViewTimeRequest();
			AdvanceViewTimeRequest.DeserializeLengthDelimited(stream, advanceViewTimeRequest);
			return advanceViewTimeRequest;
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000968AC File Offset: 0x00094AAC
		public static AdvanceViewTimeRequest DeserializeLengthDelimited(Stream stream, AdvanceViewTimeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AdvanceViewTimeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000968D4 File Offset: 0x00094AD4
		public static AdvanceViewTimeRequest Deserialize(Stream stream, AdvanceViewTimeRequest instance, long limit)
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

		// Token: 0x06002B9C RID: 11164 RVA: 0x000969A6 File Offset: 0x00094BA6
		public void Serialize(Stream stream)
		{
			AdvanceViewTimeRequest.Serialize(stream, this);
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000969B0 File Offset: 0x00094BB0
		public static void Serialize(Stream stream, AdvanceViewTimeRequest instance)
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
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x00096A18 File Offset: 0x00094C18
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
			return num;
		}

		// Token: 0x0400122F RID: 4655
		public bool HasAgentId;

		// Token: 0x04001230 RID: 4656
		private AccountId _AgentId;

		// Token: 0x04001231 RID: 4657
		public bool HasTargetId;

		// Token: 0x04001232 RID: 4658
		private AccountId _TargetId;
	}
}
