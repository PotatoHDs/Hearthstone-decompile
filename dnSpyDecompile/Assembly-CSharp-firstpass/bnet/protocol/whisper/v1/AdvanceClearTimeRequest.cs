using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002E3 RID: 739
	public class AdvanceClearTimeRequest : IProtoBuf
	{
		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x00096A6E File Offset: 0x00094C6E
		// (set) Token: 0x06002BA1 RID: 11169 RVA: 0x00096A76 File Offset: 0x00094C76
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

		// Token: 0x06002BA2 RID: 11170 RVA: 0x00096A89 File Offset: 0x00094C89
		public void SetAgentId(AccountId val)
		{
			this.AgentId = val;
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x00096A92 File Offset: 0x00094C92
		// (set) Token: 0x06002BA4 RID: 11172 RVA: 0x00096A9A File Offset: 0x00094C9A
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

		// Token: 0x06002BA5 RID: 11173 RVA: 0x00096AAD File Offset: 0x00094CAD
		public void SetTargetId(AccountId val)
		{
			this.TargetId = val;
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x00096AB8 File Offset: 0x00094CB8
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

		// Token: 0x06002BA7 RID: 11175 RVA: 0x00096B00 File Offset: 0x00094D00
		public override bool Equals(object obj)
		{
			AdvanceClearTimeRequest advanceClearTimeRequest = obj as AdvanceClearTimeRequest;
			return advanceClearTimeRequest != null && this.HasAgentId == advanceClearTimeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(advanceClearTimeRequest.AgentId)) && this.HasTargetId == advanceClearTimeRequest.HasTargetId && (!this.HasTargetId || this.TargetId.Equals(advanceClearTimeRequest.TargetId));
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x00096B70 File Offset: 0x00094D70
		public static AdvanceClearTimeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AdvanceClearTimeRequest>(bs, 0, -1);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x00096B7A File Offset: 0x00094D7A
		public void Deserialize(Stream stream)
		{
			AdvanceClearTimeRequest.Deserialize(stream, this);
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x00096B84 File Offset: 0x00094D84
		public static AdvanceClearTimeRequest Deserialize(Stream stream, AdvanceClearTimeRequest instance)
		{
			return AdvanceClearTimeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x00096B90 File Offset: 0x00094D90
		public static AdvanceClearTimeRequest DeserializeLengthDelimited(Stream stream)
		{
			AdvanceClearTimeRequest advanceClearTimeRequest = new AdvanceClearTimeRequest();
			AdvanceClearTimeRequest.DeserializeLengthDelimited(stream, advanceClearTimeRequest);
			return advanceClearTimeRequest;
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x00096BAC File Offset: 0x00094DAC
		public static AdvanceClearTimeRequest DeserializeLengthDelimited(Stream stream, AdvanceClearTimeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AdvanceClearTimeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x00096BD4 File Offset: 0x00094DD4
		public static AdvanceClearTimeRequest Deserialize(Stream stream, AdvanceClearTimeRequest instance, long limit)
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

		// Token: 0x06002BAF RID: 11183 RVA: 0x00096CA6 File Offset: 0x00094EA6
		public void Serialize(Stream stream)
		{
			AdvanceClearTimeRequest.Serialize(stream, this);
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x00096CB0 File Offset: 0x00094EB0
		public static void Serialize(Stream stream, AdvanceClearTimeRequest instance)
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

		// Token: 0x06002BB1 RID: 11185 RVA: 0x00096D18 File Offset: 0x00094F18
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

		// Token: 0x04001233 RID: 4659
		public bool HasAgentId;

		// Token: 0x04001234 RID: 4660
		private AccountId _AgentId;

		// Token: 0x04001235 RID: 4661
		public bool HasTargetId;

		// Token: 0x04001236 RID: 4662
		private AccountId _TargetId;
	}
}
