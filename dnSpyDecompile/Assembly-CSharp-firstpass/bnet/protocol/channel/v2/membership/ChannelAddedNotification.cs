using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2.membership
{
	// Token: 0x020004A8 RID: 1192
	public class ChannelAddedNotification : IProtoBuf
	{
		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06005314 RID: 21268 RVA: 0x0010074F File Offset: 0x000FE94F
		// (set) Token: 0x06005315 RID: 21269 RVA: 0x00100757 File Offset: 0x000FE957
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

		// Token: 0x06005316 RID: 21270 RVA: 0x0010076A File Offset: 0x000FE96A
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06005317 RID: 21271 RVA: 0x00100773 File Offset: 0x000FE973
		// (set) Token: 0x06005318 RID: 21272 RVA: 0x0010077B File Offset: 0x000FE97B
		public GameAccountHandle SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = (value != null);
			}
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x0010078E File Offset: 0x000FE98E
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x0600531A RID: 21274 RVA: 0x00100797 File Offset: 0x000FE997
		// (set) Token: 0x0600531B RID: 21275 RVA: 0x0010079F File Offset: 0x000FE99F
		public ChannelDescription Membership
		{
			get
			{
				return this._Membership;
			}
			set
			{
				this._Membership = value;
				this.HasMembership = (value != null);
			}
		}

		// Token: 0x0600531C RID: 21276 RVA: 0x001007B2 File Offset: 0x000FE9B2
		public void SetMembership(ChannelDescription val)
		{
			this.Membership = val;
		}

		// Token: 0x0600531D RID: 21277 RVA: 0x001007BC File Offset: 0x000FE9BC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			if (this.HasMembership)
			{
				num ^= this.Membership.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600531E RID: 21278 RVA: 0x00100818 File Offset: 0x000FEA18
		public override bool Equals(object obj)
		{
			ChannelAddedNotification channelAddedNotification = obj as ChannelAddedNotification;
			return channelAddedNotification != null && this.HasAgentId == channelAddedNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(channelAddedNotification.AgentId)) && this.HasSubscriberId == channelAddedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(channelAddedNotification.SubscriberId)) && this.HasMembership == channelAddedNotification.HasMembership && (!this.HasMembership || this.Membership.Equals(channelAddedNotification.Membership));
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x0600531F RID: 21279 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x001008B3 File Offset: 0x000FEAB3
		public static ChannelAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelAddedNotification>(bs, 0, -1);
		}

		// Token: 0x06005321 RID: 21281 RVA: 0x001008BD File Offset: 0x000FEABD
		public void Deserialize(Stream stream)
		{
			ChannelAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x06005322 RID: 21282 RVA: 0x001008C7 File Offset: 0x000FEAC7
		public static ChannelAddedNotification Deserialize(Stream stream, ChannelAddedNotification instance)
		{
			return ChannelAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005323 RID: 21283 RVA: 0x001008D4 File Offset: 0x000FEAD4
		public static ChannelAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			ChannelAddedNotification channelAddedNotification = new ChannelAddedNotification();
			ChannelAddedNotification.DeserializeLengthDelimited(stream, channelAddedNotification);
			return channelAddedNotification;
		}

		// Token: 0x06005324 RID: 21284 RVA: 0x001008F0 File Offset: 0x000FEAF0
		public static ChannelAddedNotification DeserializeLengthDelimited(Stream stream, ChannelAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005325 RID: 21285 RVA: 0x00100918 File Offset: 0x000FEB18
		public static ChannelAddedNotification Deserialize(Stream stream, ChannelAddedNotification instance, long limit)
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
						else if (instance.Membership == null)
						{
							instance.Membership = ChannelDescription.DeserializeLengthDelimited(stream);
						}
						else
						{
							ChannelDescription.DeserializeLengthDelimited(stream, instance.Membership);
						}
					}
					else if (instance.SubscriberId == null)
					{
						instance.SubscriberId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.SubscriberId);
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

		// Token: 0x06005326 RID: 21286 RVA: 0x00100A1A File Offset: 0x000FEC1A
		public void Serialize(Stream stream)
		{
			ChannelAddedNotification.Serialize(stream, this);
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x00100A24 File Offset: 0x000FEC24
		public static void Serialize(Stream stream, ChannelAddedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasMembership)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Membership.GetSerializedSize());
				ChannelDescription.Serialize(stream, instance.Membership);
			}
		}

		// Token: 0x06005328 RID: 21288 RVA: 0x00100AB8 File Offset: 0x000FECB8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize2 = this.SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasMembership)
			{
				num += 1U;
				uint serializedSize3 = this.Membership.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x04001A82 RID: 6786
		public bool HasAgentId;

		// Token: 0x04001A83 RID: 6787
		private GameAccountHandle _AgentId;

		// Token: 0x04001A84 RID: 6788
		public bool HasSubscriberId;

		// Token: 0x04001A85 RID: 6789
		private GameAccountHandle _SubscriberId;

		// Token: 0x04001A86 RID: 6790
		public bool HasMembership;

		// Token: 0x04001A87 RID: 6791
		private ChannelDescription _Membership;
	}
}
