using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004C0 RID: 1216
	public class SubscribeChannelRequest : IProtoBuf
	{
		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06005516 RID: 21782 RVA: 0x0010574D File Offset: 0x0010394D
		// (set) Token: 0x06005517 RID: 21783 RVA: 0x00105755 File Offset: 0x00103955
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

		// Token: 0x06005518 RID: 21784 RVA: 0x00105768 File Offset: 0x00103968
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06005519 RID: 21785 RVA: 0x00105771 File Offset: 0x00103971
		// (set) Token: 0x0600551A RID: 21786 RVA: 0x00105779 File Offset: 0x00103979
		public EntityId ChannelId { get; set; }

		// Token: 0x0600551B RID: 21787 RVA: 0x00105782 File Offset: 0x00103982
		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x0600551C RID: 21788 RVA: 0x0010578B File Offset: 0x0010398B
		// (set) Token: 0x0600551D RID: 21789 RVA: 0x00105793 File Offset: 0x00103993
		public ulong ObjectId { get; set; }

		// Token: 0x0600551E RID: 21790 RVA: 0x0010579C File Offset: 0x0010399C
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x0600551F RID: 21791 RVA: 0x001057A5 File Offset: 0x001039A5
		// (set) Token: 0x06005520 RID: 21792 RVA: 0x001057AD File Offset: 0x001039AD
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

		// Token: 0x06005521 RID: 21793 RVA: 0x001057C0 File Offset: 0x001039C0
		public void SetAgentIdentity(Identity val)
		{
			this.AgentIdentity = val;
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x001057CC File Offset: 0x001039CC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.ChannelId.GetHashCode();
			num ^= this.ObjectId.GetHashCode();
			if (this.HasAgentIdentity)
			{
				num ^= this.AgentIdentity.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x00105834 File Offset: 0x00103A34
		public override bool Equals(object obj)
		{
			SubscribeChannelRequest subscribeChannelRequest = obj as SubscribeChannelRequest;
			return subscribeChannelRequest != null && this.HasAgentId == subscribeChannelRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(subscribeChannelRequest.AgentId)) && this.ChannelId.Equals(subscribeChannelRequest.ChannelId) && this.ObjectId.Equals(subscribeChannelRequest.ObjectId) && this.HasAgentIdentity == subscribeChannelRequest.HasAgentIdentity && (!this.HasAgentIdentity || this.AgentIdentity.Equals(subscribeChannelRequest.AgentIdentity));
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06005524 RID: 21796 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x001058D1 File Offset: 0x00103AD1
		public static SubscribeChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeChannelRequest>(bs, 0, -1);
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x001058DB File Offset: 0x00103ADB
		public void Deserialize(Stream stream)
		{
			SubscribeChannelRequest.Deserialize(stream, this);
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x001058E5 File Offset: 0x00103AE5
		public static SubscribeChannelRequest Deserialize(Stream stream, SubscribeChannelRequest instance)
		{
			return SubscribeChannelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005528 RID: 21800 RVA: 0x001058F0 File Offset: 0x00103AF0
		public static SubscribeChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeChannelRequest subscribeChannelRequest = new SubscribeChannelRequest();
			SubscribeChannelRequest.DeserializeLengthDelimited(stream, subscribeChannelRequest);
			return subscribeChannelRequest;
		}

		// Token: 0x06005529 RID: 21801 RVA: 0x0010590C File Offset: 0x00103B0C
		public static SubscribeChannelRequest DeserializeLengthDelimited(Stream stream, SubscribeChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeChannelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600552A RID: 21802 RVA: 0x00105934 File Offset: 0x00103B34
		public static SubscribeChannelRequest Deserialize(Stream stream, SubscribeChannelRequest instance, long limit)
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
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								if (instance.ChannelId == null)
								{
									instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
									continue;
								}
								EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
								continue;
							}
						}
						else
						{
							if (instance.AgentId == null)
							{
								instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.ObjectId = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
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

		// Token: 0x0600552B RID: 21803 RVA: 0x00105A5C File Offset: 0x00103C5C
		public void Serialize(Stream stream)
		{
			SubscribeChannelRequest.Serialize(stream, this);
		}

		// Token: 0x0600552C RID: 21804 RVA: 0x00105A68 File Offset: 0x00103C68
		public static void Serialize(Stream stream, SubscribeChannelRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				Identity.Serialize(stream, instance.AgentIdentity);
			}
		}

		// Token: 0x0600552D RID: 21805 RVA: 0x00105B20 File Offset: 0x00103D20
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.ChannelId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			if (this.HasAgentIdentity)
			{
				num += 1U;
				uint serializedSize3 = this.AgentIdentity.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 2U;
		}

		// Token: 0x04001AEC RID: 6892
		public bool HasAgentId;

		// Token: 0x04001AED RID: 6893
		private EntityId _AgentId;

		// Token: 0x04001AF0 RID: 6896
		public bool HasAgentIdentity;

		// Token: 0x04001AF1 RID: 6897
		private Identity _AgentIdentity;
	}
}
