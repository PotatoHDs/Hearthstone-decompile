using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004B0 RID: 1200
	public class SubscribeRequest : IProtoBuf
	{
		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x060053B8 RID: 21432 RVA: 0x00102023 File Offset: 0x00100223
		// (set) Token: 0x060053B9 RID: 21433 RVA: 0x0010202B File Offset: 0x0010022B
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

		// Token: 0x060053BA RID: 21434 RVA: 0x0010203E File Offset: 0x0010023E
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x060053BB RID: 21435 RVA: 0x00102047 File Offset: 0x00100247
		// (set) Token: 0x060053BC RID: 21436 RVA: 0x0010204F File Offset: 0x0010024F
		public ulong ObjectId { get; set; }

		// Token: 0x060053BD RID: 21437 RVA: 0x00102058 File Offset: 0x00100258
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x060053BE RID: 21438 RVA: 0x00102061 File Offset: 0x00100261
		// (set) Token: 0x060053BF RID: 21439 RVA: 0x00102069 File Offset: 0x00100269
		public EntityId AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				this._AccountId = value;
				this.HasAccountId = (value != null);
			}
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x0010207C File Offset: 0x0010027C
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x00102088 File Offset: 0x00100288
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.ObjectId.GetHashCode();
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x001020E0 File Offset: 0x001002E0
		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			return subscribeRequest != null && this.HasAgentId == subscribeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(subscribeRequest.AgentId)) && this.ObjectId.Equals(subscribeRequest.ObjectId) && this.HasAccountId == subscribeRequest.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(subscribeRequest.AccountId));
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x060053C3 RID: 21443 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x00102168 File Offset: 0x00100368
		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x00102172 File Offset: 0x00100372
		public void Deserialize(Stream stream)
		{
			SubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x0010217C File Offset: 0x0010037C
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return SubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x00102188 File Offset: 0x00100388
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			SubscribeRequest.DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x001021A4 File Offset: 0x001003A4
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x001021CC File Offset: 0x001003CC
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance, long limit)
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
						else if (instance.AccountId == null)
						{
							instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
						}
					}
					else
					{
						instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060053CA RID: 21450 RVA: 0x001022B4 File Offset: 0x001004B4
		public void Serialize(Stream stream)
		{
			SubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x001022C0 File Offset: 0x001004C0
		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			if (instance.HasAccountId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x0010233C File Offset: 0x0010053C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize2 = this.AccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1U;
		}

		// Token: 0x04001AA4 RID: 6820
		public bool HasAgentId;

		// Token: 0x04001AA5 RID: 6821
		private EntityId _AgentId;

		// Token: 0x04001AA7 RID: 6823
		public bool HasAccountId;

		// Token: 0x04001AA8 RID: 6824
		private EntityId _AccountId;
	}
}
