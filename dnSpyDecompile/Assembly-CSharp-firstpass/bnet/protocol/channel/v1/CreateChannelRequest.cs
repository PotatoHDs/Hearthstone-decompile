using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004BC RID: 1212
	public class CreateChannelRequest : IProtoBuf
	{
		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x060054BE RID: 21694 RVA: 0x00104933 File Offset: 0x00102B33
		// (set) Token: 0x060054BF RID: 21695 RVA: 0x0010493B File Offset: 0x00102B3B
		public Identity AgentIdentity
		{
			get
			{
				return this._AgentIdentity;
			}
			set
			{
				this._AgentIdentity = value;
				this.HasAgentIdentity = (value != null);
			}
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x0010494E File Offset: 0x00102B4E
		public void SetAgentIdentity(Identity val)
		{
			this.AgentIdentity = val;
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x060054C1 RID: 21697 RVA: 0x00104957 File Offset: 0x00102B57
		// (set) Token: 0x060054C2 RID: 21698 RVA: 0x0010495F File Offset: 0x00102B5F
		public ChannelState ChannelState
		{
			get
			{
				return this._ChannelState;
			}
			set
			{
				this._ChannelState = value;
				this.HasChannelState = (value != null);
			}
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x00104972 File Offset: 0x00102B72
		public void SetChannelState(ChannelState val)
		{
			this.ChannelState = val;
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x060054C4 RID: 21700 RVA: 0x0010497B File Offset: 0x00102B7B
		// (set) Token: 0x060054C5 RID: 21701 RVA: 0x00104983 File Offset: 0x00102B83
		public EntityId ChannelId
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

		// Token: 0x060054C6 RID: 21702 RVA: 0x00104996 File Offset: 0x00102B96
		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x060054C7 RID: 21703 RVA: 0x0010499F File Offset: 0x00102B9F
		// (set) Token: 0x060054C8 RID: 21704 RVA: 0x001049A7 File Offset: 0x00102BA7
		public ulong ObjectId
		{
			get
			{
				return this._ObjectId;
			}
			set
			{
				this._ObjectId = value;
				this.HasObjectId = true;
			}
		}

		// Token: 0x060054C9 RID: 21705 RVA: 0x001049B7 File Offset: 0x00102BB7
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x001049C0 File Offset: 0x00102BC0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentIdentity)
			{
				num ^= this.AgentIdentity.GetHashCode();
			}
			if (this.HasChannelState)
			{
				num ^= this.ChannelState.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x00104A38 File Offset: 0x00102C38
		public override bool Equals(object obj)
		{
			CreateChannelRequest createChannelRequest = obj as CreateChannelRequest;
			return createChannelRequest != null && this.HasAgentIdentity == createChannelRequest.HasAgentIdentity && (!this.HasAgentIdentity || this.AgentIdentity.Equals(createChannelRequest.AgentIdentity)) && this.HasChannelState == createChannelRequest.HasChannelState && (!this.HasChannelState || this.ChannelState.Equals(createChannelRequest.ChannelState)) && this.HasChannelId == createChannelRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(createChannelRequest.ChannelId)) && this.HasObjectId == createChannelRequest.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(createChannelRequest.ObjectId));
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x060054CC RID: 21708 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x00104B01 File Offset: 0x00102D01
		public static CreateChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelRequest>(bs, 0, -1);
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x00104B0B File Offset: 0x00102D0B
		public void Deserialize(Stream stream)
		{
			CreateChannelRequest.Deserialize(stream, this);
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x00104B15 File Offset: 0x00102D15
		public static CreateChannelRequest Deserialize(Stream stream, CreateChannelRequest instance)
		{
			return CreateChannelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060054D0 RID: 21712 RVA: 0x00104B20 File Offset: 0x00102D20
		public static CreateChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelRequest createChannelRequest = new CreateChannelRequest();
			CreateChannelRequest.DeserializeLengthDelimited(stream, createChannelRequest);
			return createChannelRequest;
		}

		// Token: 0x060054D1 RID: 21713 RVA: 0x00104B3C File Offset: 0x00102D3C
		public static CreateChannelRequest DeserializeLengthDelimited(Stream stream, CreateChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateChannelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060054D2 RID: 21714 RVA: 0x00104B64 File Offset: 0x00102D64
		public static CreateChannelRequest Deserialize(Stream stream, CreateChannelRequest instance, long limit)
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
				else
				{
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num == 26)
							{
								if (instance.ChannelState == null)
								{
									instance.ChannelState = ChannelState.DeserializeLengthDelimited(stream);
									continue;
								}
								ChannelState.DeserializeLengthDelimited(stream, instance.ChannelState);
								continue;
							}
						}
						else
						{
							if (instance.AgentIdentity == null)
							{
								instance.AgentIdentity = Identity.DeserializeLengthDelimited(stream);
								continue;
							}
							Identity.DeserializeLengthDelimited(stream, instance.AgentIdentity);
							continue;
						}
					}
					else if (num != 34)
					{
						if (num == 40)
						{
							instance.ObjectId = ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (instance.ChannelId == null)
						{
							instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
							continue;
						}
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
						continue;
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x00104C8F File Offset: 0x00102E8F
		public void Serialize(Stream stream)
		{
			CreateChannelRequest.Serialize(stream, this);
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x00104C98 File Offset: 0x00102E98
		public static void Serialize(Stream stream, CreateChannelRequest instance)
		{
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				Identity.Serialize(stream, instance.AgentIdentity);
			}
			if (instance.HasChannelState)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelState.GetSerializedSize());
				ChannelState.Serialize(stream, instance.ChannelState);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasObjectId)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
		}

		// Token: 0x060054D5 RID: 21717 RVA: 0x00104D48 File Offset: 0x00102F48
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentIdentity)
			{
				num += 1U;
				uint serializedSize = this.AgentIdentity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasChannelState)
			{
				num += 1U;
				uint serializedSize2 = this.ChannelState.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize3 = this.ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasObjectId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			return num;
		}

		// Token: 0x04001AD7 RID: 6871
		public bool HasAgentIdentity;

		// Token: 0x04001AD8 RID: 6872
		private Identity _AgentIdentity;

		// Token: 0x04001AD9 RID: 6873
		public bool HasChannelState;

		// Token: 0x04001ADA RID: 6874
		private ChannelState _ChannelState;

		// Token: 0x04001ADB RID: 6875
		public bool HasChannelId;

		// Token: 0x04001ADC RID: 6876
		private EntityId _ChannelId;

		// Token: 0x04001ADD RID: 6877
		public bool HasObjectId;

		// Token: 0x04001ADE RID: 6878
		private ulong _ObjectId;
	}
}
