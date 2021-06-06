using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004C6 RID: 1222
	public class RemoveMemberRequest : IProtoBuf
	{
		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06005589 RID: 21897 RVA: 0x00106853 File Offset: 0x00104A53
		// (set) Token: 0x0600558A RID: 21898 RVA: 0x0010685B File Offset: 0x00104A5B
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

		// Token: 0x0600558B RID: 21899 RVA: 0x0010686E File Offset: 0x00104A6E
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x0600558C RID: 21900 RVA: 0x00106877 File Offset: 0x00104A77
		// (set) Token: 0x0600558D RID: 21901 RVA: 0x0010687F File Offset: 0x00104A7F
		public EntityId MemberId { get; set; }

		// Token: 0x0600558E RID: 21902 RVA: 0x00106888 File Offset: 0x00104A88
		public void SetMemberId(EntityId val)
		{
			this.MemberId = val;
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x0600558F RID: 21903 RVA: 0x00106891 File Offset: 0x00104A91
		// (set) Token: 0x06005590 RID: 21904 RVA: 0x00106899 File Offset: 0x00104A99
		public uint Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x001068A9 File Offset: 0x00104AA9
		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x001068B4 File Offset: 0x00104AB4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.MemberId.GetHashCode();
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x0010690C File Offset: 0x00104B0C
		public override bool Equals(object obj)
		{
			RemoveMemberRequest removeMemberRequest = obj as RemoveMemberRequest;
			return removeMemberRequest != null && this.HasAgentId == removeMemberRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(removeMemberRequest.AgentId)) && this.MemberId.Equals(removeMemberRequest.MemberId) && this.HasReason == removeMemberRequest.HasReason && (!this.HasReason || this.Reason.Equals(removeMemberRequest.Reason));
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06005594 RID: 21908 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005595 RID: 21909 RVA: 0x00106994 File Offset: 0x00104B94
		public static RemoveMemberRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveMemberRequest>(bs, 0, -1);
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x0010699E File Offset: 0x00104B9E
		public void Deserialize(Stream stream)
		{
			RemoveMemberRequest.Deserialize(stream, this);
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x001069A8 File Offset: 0x00104BA8
		public static RemoveMemberRequest Deserialize(Stream stream, RemoveMemberRequest instance)
		{
			return RemoveMemberRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x001069B4 File Offset: 0x00104BB4
		public static RemoveMemberRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveMemberRequest removeMemberRequest = new RemoveMemberRequest();
			RemoveMemberRequest.DeserializeLengthDelimited(stream, removeMemberRequest);
			return removeMemberRequest;
		}

		// Token: 0x06005599 RID: 21913 RVA: 0x001069D0 File Offset: 0x00104BD0
		public static RemoveMemberRequest DeserializeLengthDelimited(Stream stream, RemoveMemberRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemoveMemberRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600559A RID: 21914 RVA: 0x001069F8 File Offset: 0x00104BF8
		public static RemoveMemberRequest Deserialize(Stream stream, RemoveMemberRequest instance, long limit)
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
							instance.Reason = ProtocolParser.ReadUInt32(stream);
						}
					}
					else if (instance.MemberId == null)
					{
						instance.MemberId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.MemberId);
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

		// Token: 0x0600559B RID: 21915 RVA: 0x00106AE0 File Offset: 0x00104CE0
		public void Serialize(Stream stream)
		{
			RemoveMemberRequest.Serialize(stream, this);
		}

		// Token: 0x0600559C RID: 21916 RVA: 0x00106AEC File Offset: 0x00104CEC
		public static void Serialize(Stream stream, RemoveMemberRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.MemberId == null)
			{
				throw new ArgumentNullException("MemberId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
			EntityId.Serialize(stream, instance.MemberId);
			if (instance.HasReason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
		}

		// Token: 0x0600559D RID: 21917 RVA: 0x00106B80 File Offset: 0x00104D80
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.MemberId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			return num + 1U;
		}

		// Token: 0x04001AFD RID: 6909
		public bool HasAgentId;

		// Token: 0x04001AFE RID: 6910
		private EntityId _AgentId;

		// Token: 0x04001B00 RID: 6912
		public bool HasReason;

		// Token: 0x04001B01 RID: 6913
		private uint _Reason;
	}
}
