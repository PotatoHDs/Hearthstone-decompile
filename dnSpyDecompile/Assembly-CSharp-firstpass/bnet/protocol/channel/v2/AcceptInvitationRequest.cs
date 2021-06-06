using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200045F RID: 1119
	public class AcceptInvitationRequest : IProtoBuf
	{
		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06004C8A RID: 19594 RVA: 0x000EDD86 File Offset: 0x000EBF86
		// (set) Token: 0x06004C8B RID: 19595 RVA: 0x000EDD8E File Offset: 0x000EBF8E
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

		// Token: 0x06004C8C RID: 19596 RVA: 0x000EDDA1 File Offset: 0x000EBFA1
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06004C8D RID: 19597 RVA: 0x000EDDAA File Offset: 0x000EBFAA
		// (set) Token: 0x06004C8E RID: 19598 RVA: 0x000EDDB2 File Offset: 0x000EBFB2
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

		// Token: 0x06004C8F RID: 19599 RVA: 0x000EDDC5 File Offset: 0x000EBFC5
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06004C90 RID: 19600 RVA: 0x000EDDCE File Offset: 0x000EBFCE
		// (set) Token: 0x06004C91 RID: 19601 RVA: 0x000EDDD6 File Offset: 0x000EBFD6
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

		// Token: 0x06004C92 RID: 19602 RVA: 0x000EDDE6 File Offset: 0x000EBFE6
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x06004C93 RID: 19603 RVA: 0x000EDDF0 File Offset: 0x000EBFF0
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

		// Token: 0x06004C94 RID: 19604 RVA: 0x000EDE50 File Offset: 0x000EC050
		public override bool Equals(object obj)
		{
			AcceptInvitationRequest acceptInvitationRequest = obj as AcceptInvitationRequest;
			return acceptInvitationRequest != null && this.HasAgentId == acceptInvitationRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(acceptInvitationRequest.AgentId)) && this.HasChannelId == acceptInvitationRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(acceptInvitationRequest.ChannelId)) && this.HasInvitationId == acceptInvitationRequest.HasInvitationId && (!this.HasInvitationId || this.InvitationId.Equals(acceptInvitationRequest.InvitationId));
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06004C95 RID: 19605 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x000EDEEE File Offset: 0x000EC0EE
		public static AcceptInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AcceptInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x000EDEF8 File Offset: 0x000EC0F8
		public void Deserialize(Stream stream)
		{
			AcceptInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x000EDF02 File Offset: 0x000EC102
		public static AcceptInvitationRequest Deserialize(Stream stream, AcceptInvitationRequest instance)
		{
			return AcceptInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x000EDF10 File Offset: 0x000EC110
		public static AcceptInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			AcceptInvitationRequest acceptInvitationRequest = new AcceptInvitationRequest();
			AcceptInvitationRequest.DeserializeLengthDelimited(stream, acceptInvitationRequest);
			return acceptInvitationRequest;
		}

		// Token: 0x06004C9A RID: 19610 RVA: 0x000EDF2C File Offset: 0x000EC12C
		public static AcceptInvitationRequest DeserializeLengthDelimited(Stream stream, AcceptInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AcceptInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004C9B RID: 19611 RVA: 0x000EDF54 File Offset: 0x000EC154
		public static AcceptInvitationRequest Deserialize(Stream stream, AcceptInvitationRequest instance, long limit)
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

		// Token: 0x06004C9C RID: 19612 RVA: 0x000EE043 File Offset: 0x000EC243
		public void Serialize(Stream stream)
		{
			AcceptInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x06004C9D RID: 19613 RVA: 0x000EE04C File Offset: 0x000EC24C
		public static void Serialize(Stream stream, AcceptInvitationRequest instance)
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

		// Token: 0x06004C9E RID: 19614 RVA: 0x000EE0D8 File Offset: 0x000EC2D8
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

		// Token: 0x040018F2 RID: 6386
		public bool HasAgentId;

		// Token: 0x040018F3 RID: 6387
		private GameAccountHandle _AgentId;

		// Token: 0x040018F4 RID: 6388
		public bool HasChannelId;

		// Token: 0x040018F5 RID: 6389
		private ChannelId _ChannelId;

		// Token: 0x040018F6 RID: 6390
		public bool HasInvitationId;

		// Token: 0x040018F7 RID: 6391
		private ulong _InvitationId;
	}
}
