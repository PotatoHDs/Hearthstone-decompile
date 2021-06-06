using System;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v2.Types;

namespace bnet.protocol.channel.v2.membership
{
	// Token: 0x020004A9 RID: 1193
	public class ChannelRemovedNotification : IProtoBuf
	{
		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x0600532A RID: 21290 RVA: 0x00100B31 File Offset: 0x000FED31
		// (set) Token: 0x0600532B RID: 21291 RVA: 0x00100B39 File Offset: 0x000FED39
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

		// Token: 0x0600532C RID: 21292 RVA: 0x00100B4C File Offset: 0x000FED4C
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x0600532D RID: 21293 RVA: 0x00100B55 File Offset: 0x000FED55
		// (set) Token: 0x0600532E RID: 21294 RVA: 0x00100B5D File Offset: 0x000FED5D
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

		// Token: 0x0600532F RID: 21295 RVA: 0x00100B70 File Offset: 0x000FED70
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06005330 RID: 21296 RVA: 0x00100B79 File Offset: 0x000FED79
		// (set) Token: 0x06005331 RID: 21297 RVA: 0x00100B81 File Offset: 0x000FED81
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

		// Token: 0x06005332 RID: 21298 RVA: 0x00100B94 File Offset: 0x000FED94
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06005333 RID: 21299 RVA: 0x00100B9D File Offset: 0x000FED9D
		// (set) Token: 0x06005334 RID: 21300 RVA: 0x00100BA5 File Offset: 0x000FEDA5
		public ChannelRemovedReason Reason
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

		// Token: 0x06005335 RID: 21301 RVA: 0x00100BB5 File Offset: 0x000FEDB5
		public void SetReason(ChannelRemovedReason val)
		{
			this.Reason = val;
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x00100BC0 File Offset: 0x000FEDC0
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
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005337 RID: 21303 RVA: 0x00100C3C File Offset: 0x000FEE3C
		public override bool Equals(object obj)
		{
			ChannelRemovedNotification channelRemovedNotification = obj as ChannelRemovedNotification;
			return channelRemovedNotification != null && this.HasAgentId == channelRemovedNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(channelRemovedNotification.AgentId)) && this.HasSubscriberId == channelRemovedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(channelRemovedNotification.SubscriberId)) && this.HasChannelId == channelRemovedNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(channelRemovedNotification.ChannelId)) && this.HasReason == channelRemovedNotification.HasReason && (!this.HasReason || this.Reason.Equals(channelRemovedNotification.Reason));
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06005338 RID: 21304 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x00100D10 File Offset: 0x000FEF10
		public static ChannelRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelRemovedNotification>(bs, 0, -1);
		}

		// Token: 0x0600533A RID: 21306 RVA: 0x00100D1A File Offset: 0x000FEF1A
		public void Deserialize(Stream stream)
		{
			ChannelRemovedNotification.Deserialize(stream, this);
		}

		// Token: 0x0600533B RID: 21307 RVA: 0x00100D24 File Offset: 0x000FEF24
		public static ChannelRemovedNotification Deserialize(Stream stream, ChannelRemovedNotification instance)
		{
			return ChannelRemovedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x00100D30 File Offset: 0x000FEF30
		public static ChannelRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			ChannelRemovedNotification channelRemovedNotification = new ChannelRemovedNotification();
			ChannelRemovedNotification.DeserializeLengthDelimited(stream, channelRemovedNotification);
			return channelRemovedNotification;
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x00100D4C File Offset: 0x000FEF4C
		public static ChannelRemovedNotification DeserializeLengthDelimited(Stream stream, ChannelRemovedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelRemovedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x00100D74 File Offset: 0x000FEF74
		public static ChannelRemovedNotification Deserialize(Stream stream, ChannelRemovedNotification instance, long limit)
		{
			instance.Reason = ChannelRemovedReason.CHANNEL_REMOVED_REASON_MEMBER_LEFT;
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
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								if (instance.SubscriberId == null)
								{
									instance.SubscriberId = GameAccountHandle.DeserializeLengthDelimited(stream);
									continue;
								}
								GameAccountHandle.DeserializeLengthDelimited(stream, instance.SubscriberId);
								continue;
							}
						}
						else
						{
							if (instance.AgentId == null)
							{
								instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
							continue;
						}
					}
					else if (num != 34)
					{
						if (num == 40)
						{
							instance.Reason = (ChannelRemovedReason)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (instance.ChannelId == null)
						{
							instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
							continue;
						}
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
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

		// Token: 0x0600533F RID: 21311 RVA: 0x00100EA7 File Offset: 0x000FF0A7
		public void Serialize(Stream stream)
		{
			ChannelRemovedNotification.Serialize(stream, this);
		}

		// Token: 0x06005340 RID: 21312 RVA: 0x00100EB0 File Offset: 0x000FF0B0
		public static void Serialize(Stream stream, ChannelRemovedNotification instance)
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
			if (instance.HasChannelId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Reason));
			}
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x00100F64 File Offset: 0x000FF164
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
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize3 = this.ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Reason));
			}
			return num;
		}

		// Token: 0x04001A88 RID: 6792
		public bool HasAgentId;

		// Token: 0x04001A89 RID: 6793
		private GameAccountHandle _AgentId;

		// Token: 0x04001A8A RID: 6794
		public bool HasSubscriberId;

		// Token: 0x04001A8B RID: 6795
		private GameAccountHandle _SubscriberId;

		// Token: 0x04001A8C RID: 6796
		public bool HasChannelId;

		// Token: 0x04001A8D RID: 6797
		private ChannelId _ChannelId;

		// Token: 0x04001A8E RID: 6798
		public bool HasReason;

		// Token: 0x04001A8F RID: 6799
		private ChannelRemovedReason _Reason;
	}
}
