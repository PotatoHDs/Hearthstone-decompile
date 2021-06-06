using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004D4 RID: 1236
	public class ChannelDescription : IProtoBuf
	{
		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x060056FB RID: 22267 RVA: 0x0010AE09 File Offset: 0x00109009
		// (set) Token: 0x060056FC RID: 22268 RVA: 0x0010AE11 File Offset: 0x00109011
		public EntityId ChannelId { get; set; }

		// Token: 0x060056FD RID: 22269 RVA: 0x0010AE1A File Offset: 0x0010901A
		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x060056FE RID: 22270 RVA: 0x0010AE23 File Offset: 0x00109023
		// (set) Token: 0x060056FF RID: 22271 RVA: 0x0010AE2B File Offset: 0x0010902B
		public uint CurrentMembers
		{
			get
			{
				return this._CurrentMembers;
			}
			set
			{
				this._CurrentMembers = value;
				this.HasCurrentMembers = true;
			}
		}

		// Token: 0x06005700 RID: 22272 RVA: 0x0010AE3B File Offset: 0x0010903B
		public void SetCurrentMembers(uint val)
		{
			this.CurrentMembers = val;
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06005701 RID: 22273 RVA: 0x0010AE44 File Offset: 0x00109044
		// (set) Token: 0x06005702 RID: 22274 RVA: 0x0010AE4C File Offset: 0x0010904C
		public ChannelState State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
				this.HasState = (value != null);
			}
		}

		// Token: 0x06005703 RID: 22275 RVA: 0x0010AE5F File Offset: 0x0010905F
		public void SetState(ChannelState val)
		{
			this.State = val;
		}

		// Token: 0x06005704 RID: 22276 RVA: 0x0010AE68 File Offset: 0x00109068
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ChannelId.GetHashCode();
			if (this.HasCurrentMembers)
			{
				num ^= this.CurrentMembers.GetHashCode();
			}
			if (this.HasState)
			{
				num ^= this.State.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x0010AEC0 File Offset: 0x001090C0
		public override bool Equals(object obj)
		{
			ChannelDescription channelDescription = obj as ChannelDescription;
			return channelDescription != null && this.ChannelId.Equals(channelDescription.ChannelId) && this.HasCurrentMembers == channelDescription.HasCurrentMembers && (!this.HasCurrentMembers || this.CurrentMembers.Equals(channelDescription.CurrentMembers)) && this.HasState == channelDescription.HasState && (!this.HasState || this.State.Equals(channelDescription.State));
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06005706 RID: 22278 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x0010AF48 File Offset: 0x00109148
		public static ChannelDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelDescription>(bs, 0, -1);
		}

		// Token: 0x06005708 RID: 22280 RVA: 0x0010AF52 File Offset: 0x00109152
		public void Deserialize(Stream stream)
		{
			ChannelDescription.Deserialize(stream, this);
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x0010AF5C File Offset: 0x0010915C
		public static ChannelDescription Deserialize(Stream stream, ChannelDescription instance)
		{
			return ChannelDescription.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x0010AF68 File Offset: 0x00109168
		public static ChannelDescription DeserializeLengthDelimited(Stream stream)
		{
			ChannelDescription channelDescription = new ChannelDescription();
			ChannelDescription.DeserializeLengthDelimited(stream, channelDescription);
			return channelDescription;
		}

		// Token: 0x0600570B RID: 22283 RVA: 0x0010AF84 File Offset: 0x00109184
		public static ChannelDescription DeserializeLengthDelimited(Stream stream, ChannelDescription instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelDescription.Deserialize(stream, instance, num);
		}

		// Token: 0x0600570C RID: 22284 RVA: 0x0010AFAC File Offset: 0x001091AC
		public static ChannelDescription Deserialize(Stream stream, ChannelDescription instance, long limit)
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
					if (num != 16)
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
						else if (instance.State == null)
						{
							instance.State = ChannelState.DeserializeLengthDelimited(stream);
						}
						else
						{
							ChannelState.DeserializeLengthDelimited(stream, instance.State);
						}
					}
					else
					{
						instance.CurrentMembers = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.ChannelId == null)
				{
					instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600570D RID: 22285 RVA: 0x0010B094 File Offset: 0x00109294
		public void Serialize(Stream stream)
		{
			ChannelDescription.Serialize(stream, this);
		}

		// Token: 0x0600570E RID: 22286 RVA: 0x0010B0A0 File Offset: 0x001092A0
		public static void Serialize(Stream stream, ChannelDescription instance)
		{
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
			if (instance.HasCurrentMembers)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.CurrentMembers);
			}
			if (instance.HasState)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				ChannelState.Serialize(stream, instance.State);
			}
		}

		// Token: 0x0600570F RID: 22287 RVA: 0x0010B134 File Offset: 0x00109334
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.ChannelId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasCurrentMembers)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.CurrentMembers);
			}
			if (this.HasState)
			{
				num += 1U;
				uint serializedSize2 = this.State.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1U;
		}

		// Token: 0x04001B5F RID: 7007
		public bool HasCurrentMembers;

		// Token: 0x04001B60 RID: 7008
		private uint _CurrentMembers;

		// Token: 0x04001B61 RID: 7009
		public bool HasState;

		// Token: 0x04001B62 RID: 7010
		private ChannelState _State;
	}
}
