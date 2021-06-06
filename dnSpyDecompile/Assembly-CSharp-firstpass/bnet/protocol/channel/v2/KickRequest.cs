using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200045A RID: 1114
	public class KickRequest : IProtoBuf
	{
		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06004C1F RID: 19487 RVA: 0x000ECAF6 File Offset: 0x000EACF6
		// (set) Token: 0x06004C20 RID: 19488 RVA: 0x000ECAFE File Offset: 0x000EACFE
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

		// Token: 0x06004C21 RID: 19489 RVA: 0x000ECB11 File Offset: 0x000EAD11
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06004C22 RID: 19490 RVA: 0x000ECB1A File Offset: 0x000EAD1A
		// (set) Token: 0x06004C23 RID: 19491 RVA: 0x000ECB22 File Offset: 0x000EAD22
		public ChannelId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x000ECB35 File Offset: 0x000EAD35
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06004C25 RID: 19493 RVA: 0x000ECB3E File Offset: 0x000EAD3E
		// (set) Token: 0x06004C26 RID: 19494 RVA: 0x000ECB46 File Offset: 0x000EAD46
		public GameAccountHandle TargetId
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

		// Token: 0x06004C27 RID: 19495 RVA: 0x000ECB59 File Offset: 0x000EAD59
		public void SetTargetId(GameAccountHandle val)
		{
			this.TargetId = val;
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x000ECB64 File Offset: 0x000EAD64
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasTargetId)
			{
				num ^= this.TargetId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x000ECBC0 File Offset: 0x000EADC0
		public override bool Equals(object obj)
		{
			KickRequest kickRequest = obj as KickRequest;
			return kickRequest != null && this.HasAgentId == kickRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(kickRequest.AgentId)) && this.HasChannelId == kickRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(kickRequest.ChannelId)) && this.HasTargetId == kickRequest.HasTargetId && (!this.HasTargetId || this.TargetId.Equals(kickRequest.TargetId));
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06004C2A RID: 19498 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x000ECC5B File Offset: 0x000EAE5B
		public static KickRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<KickRequest>(bs, 0, -1);
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x000ECC65 File Offset: 0x000EAE65
		public void Deserialize(Stream stream)
		{
			KickRequest.Deserialize(stream, this);
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x000ECC6F File Offset: 0x000EAE6F
		public static KickRequest Deserialize(Stream stream, KickRequest instance)
		{
			return KickRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x000ECC7C File Offset: 0x000EAE7C
		public static KickRequest DeserializeLengthDelimited(Stream stream)
		{
			KickRequest kickRequest = new KickRequest();
			KickRequest.DeserializeLengthDelimited(stream, kickRequest);
			return kickRequest;
		}

		// Token: 0x06004C2F RID: 19503 RVA: 0x000ECC98 File Offset: 0x000EAE98
		public static KickRequest DeserializeLengthDelimited(Stream stream, KickRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return KickRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004C30 RID: 19504 RVA: 0x000ECCC0 File Offset: 0x000EAEC0
		public static KickRequest Deserialize(Stream stream, KickRequest instance, long limit)
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
						else if (instance.TargetId == null)
						{
							instance.TargetId = GameAccountHandle.DeserializeLengthDelimited(stream);
						}
						else
						{
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.TargetId);
						}
					}
					else if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
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

		// Token: 0x06004C31 RID: 19505 RVA: 0x000ECDC2 File Offset: 0x000EAFC2
		public void Serialize(Stream stream)
		{
			KickRequest.Serialize(stream, this);
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x000ECDCC File Offset: 0x000EAFCC
		public static void Serialize(Stream stream, KickRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasTargetId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.TargetId);
			}
		}

		// Token: 0x06004C33 RID: 19507 RVA: 0x000ECE60 File Offset: 0x000EB060
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize2 = this.ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasTargetId)
			{
				num += 1U;
				uint serializedSize3 = this.TargetId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x040018D6 RID: 6358
		public bool HasAgentId;

		// Token: 0x040018D7 RID: 6359
		private GameAccountHandle _AgentId;

		// Token: 0x040018D8 RID: 6360
		public bool HasChannelId;

		// Token: 0x040018D9 RID: 6361
		private ChannelId _ChannelId;

		// Token: 0x040018DA RID: 6362
		public bool HasTargetId;

		// Token: 0x040018DB RID: 6363
		private GameAccountHandle _TargetId;
	}
}
