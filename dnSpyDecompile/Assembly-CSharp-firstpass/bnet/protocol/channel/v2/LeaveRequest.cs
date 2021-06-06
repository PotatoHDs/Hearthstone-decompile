using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000459 RID: 1113
	public class LeaveRequest : IProtoBuf
	{
		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06004C0C RID: 19468 RVA: 0x000EC7F5 File Offset: 0x000EA9F5
		// (set) Token: 0x06004C0D RID: 19469 RVA: 0x000EC7FD File Offset: 0x000EA9FD
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

		// Token: 0x06004C0E RID: 19470 RVA: 0x000EC810 File Offset: 0x000EAA10
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06004C0F RID: 19471 RVA: 0x000EC819 File Offset: 0x000EAA19
		// (set) Token: 0x06004C10 RID: 19472 RVA: 0x000EC821 File Offset: 0x000EAA21
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

		// Token: 0x06004C11 RID: 19473 RVA: 0x000EC834 File Offset: 0x000EAA34
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x000EC840 File Offset: 0x000EAA40
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
			return num;
		}

		// Token: 0x06004C13 RID: 19475 RVA: 0x000EC888 File Offset: 0x000EAA88
		public override bool Equals(object obj)
		{
			LeaveRequest leaveRequest = obj as LeaveRequest;
			return leaveRequest != null && this.HasAgentId == leaveRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(leaveRequest.AgentId)) && this.HasChannelId == leaveRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(leaveRequest.ChannelId));
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06004C14 RID: 19476 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004C15 RID: 19477 RVA: 0x000EC8F8 File Offset: 0x000EAAF8
		public static LeaveRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<LeaveRequest>(bs, 0, -1);
		}

		// Token: 0x06004C16 RID: 19478 RVA: 0x000EC902 File Offset: 0x000EAB02
		public void Deserialize(Stream stream)
		{
			LeaveRequest.Deserialize(stream, this);
		}

		// Token: 0x06004C17 RID: 19479 RVA: 0x000EC90C File Offset: 0x000EAB0C
		public static LeaveRequest Deserialize(Stream stream, LeaveRequest instance)
		{
			return LeaveRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004C18 RID: 19480 RVA: 0x000EC918 File Offset: 0x000EAB18
		public static LeaveRequest DeserializeLengthDelimited(Stream stream)
		{
			LeaveRequest leaveRequest = new LeaveRequest();
			LeaveRequest.DeserializeLengthDelimited(stream, leaveRequest);
			return leaveRequest;
		}

		// Token: 0x06004C19 RID: 19481 RVA: 0x000EC934 File Offset: 0x000EAB34
		public static LeaveRequest DeserializeLengthDelimited(Stream stream, LeaveRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LeaveRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004C1A RID: 19482 RVA: 0x000EC95C File Offset: 0x000EAB5C
		public static LeaveRequest Deserialize(Stream stream, LeaveRequest instance, long limit)
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

		// Token: 0x06004C1B RID: 19483 RVA: 0x000ECA2E File Offset: 0x000EAC2E
		public void Serialize(Stream stream)
		{
			LeaveRequest.Serialize(stream, this);
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x000ECA38 File Offset: 0x000EAC38
		public static void Serialize(Stream stream, LeaveRequest instance)
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
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x000ECAA0 File Offset: 0x000EACA0
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
			return num;
		}

		// Token: 0x040018D2 RID: 6354
		public bool HasAgentId;

		// Token: 0x040018D3 RID: 6355
		private GameAccountHandle _AgentId;

		// Token: 0x040018D4 RID: 6356
		public bool HasChannelId;

		// Token: 0x040018D5 RID: 6357
		private ChannelId _ChannelId;
	}
}
