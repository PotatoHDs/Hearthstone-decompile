using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000460 RID: 1120
	public class DeclineInvitationRequest : IProtoBuf
	{
		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06004CA0 RID: 19616 RVA: 0x000EE13E File Offset: 0x000EC33E
		// (set) Token: 0x06004CA1 RID: 19617 RVA: 0x000EE146 File Offset: 0x000EC346
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

		// Token: 0x06004CA2 RID: 19618 RVA: 0x000EE159 File Offset: 0x000EC359
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06004CA3 RID: 19619 RVA: 0x000EE162 File Offset: 0x000EC362
		// (set) Token: 0x06004CA4 RID: 19620 RVA: 0x000EE16A File Offset: 0x000EC36A
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

		// Token: 0x06004CA5 RID: 19621 RVA: 0x000EE17D File Offset: 0x000EC37D
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06004CA6 RID: 19622 RVA: 0x000EE186 File Offset: 0x000EC386
		// (set) Token: 0x06004CA7 RID: 19623 RVA: 0x000EE18E File Offset: 0x000EC38E
		public ulong InvitationId
		{
			get
			{
				return this._InvitationId;
			}
			set
			{
				this._InvitationId = value;
				this.HasInvitationId = true;
			}
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x000EE19E File Offset: 0x000EC39E
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x000EE1A8 File Offset: 0x000EC3A8
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
			if (this.HasInvitationId)
			{
				num ^= this.InvitationId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x000EE208 File Offset: 0x000EC408
		public override bool Equals(object obj)
		{
			DeclineInvitationRequest declineInvitationRequest = obj as DeclineInvitationRequest;
			return declineInvitationRequest != null && this.HasAgentId == declineInvitationRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(declineInvitationRequest.AgentId)) && this.HasChannelId == declineInvitationRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(declineInvitationRequest.ChannelId)) && this.HasInvitationId == declineInvitationRequest.HasInvitationId && (!this.HasInvitationId || this.InvitationId.Equals(declineInvitationRequest.InvitationId));
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06004CAB RID: 19627 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x000EE2A6 File Offset: 0x000EC4A6
		public static DeclineInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DeclineInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x000EE2B0 File Offset: 0x000EC4B0
		public void Deserialize(Stream stream)
		{
			DeclineInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x000EE2BA File Offset: 0x000EC4BA
		public static DeclineInvitationRequest Deserialize(Stream stream, DeclineInvitationRequest instance)
		{
			return DeclineInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x000EE2C8 File Offset: 0x000EC4C8
		public static DeclineInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			DeclineInvitationRequest declineInvitationRequest = new DeclineInvitationRequest();
			DeclineInvitationRequest.DeserializeLengthDelimited(stream, declineInvitationRequest);
			return declineInvitationRequest;
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x000EE2E4 File Offset: 0x000EC4E4
		public static DeclineInvitationRequest DeserializeLengthDelimited(Stream stream, DeclineInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeclineInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x000EE30C File Offset: 0x000EC50C
		public static DeclineInvitationRequest Deserialize(Stream stream, DeclineInvitationRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
						if (num != 25)
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
							instance.InvitationId = binaryReader.ReadUInt64();
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

		// Token: 0x06004CB2 RID: 19634 RVA: 0x000EE3FB File Offset: 0x000EC5FB
		public void Serialize(Stream stream)
		{
			DeclineInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x000EE404 File Offset: 0x000EC604
		public static void Serialize(Stream stream, DeclineInvitationRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
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
			if (instance.HasInvitationId)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.InvitationId);
			}
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x000EE490 File Offset: 0x000EC690
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
			if (this.HasInvitationId)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x040018F8 RID: 6392
		public bool HasAgentId;

		// Token: 0x040018F9 RID: 6393
		private GameAccountHandle _AgentId;

		// Token: 0x040018FA RID: 6394
		public bool HasChannelId;

		// Token: 0x040018FB RID: 6395
		private ChannelId _ChannelId;

		// Token: 0x040018FC RID: 6396
		public bool HasInvitationId;

		// Token: 0x040018FD RID: 6397
		private ulong _InvitationId;
	}
}
