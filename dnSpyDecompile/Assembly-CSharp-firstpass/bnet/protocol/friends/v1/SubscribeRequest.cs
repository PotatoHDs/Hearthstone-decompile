using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x0200041A RID: 1050
	public class SubscribeRequest : IProtoBuf
	{
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06004621 RID: 17953 RVA: 0x000DC116 File Offset: 0x000DA316
		// (set) Token: 0x06004622 RID: 17954 RVA: 0x000DC11E File Offset: 0x000DA31E
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

		// Token: 0x06004623 RID: 17955 RVA: 0x000DC131 File Offset: 0x000DA331
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06004624 RID: 17956 RVA: 0x000DC13A File Offset: 0x000DA33A
		// (set) Token: 0x06004625 RID: 17957 RVA: 0x000DC142 File Offset: 0x000DA342
		public ulong ObjectId { get; set; }

		// Token: 0x06004626 RID: 17958 RVA: 0x000DC14B File Offset: 0x000DA34B
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06004627 RID: 17959 RVA: 0x000DC154 File Offset: 0x000DA354
		// (set) Token: 0x06004628 RID: 17960 RVA: 0x000DC15C File Offset: 0x000DA35C
		public ObjectAddress Forward
		{
			get
			{
				return this._Forward;
			}
			set
			{
				this._Forward = value;
				this.HasForward = (value != null);
			}
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x000DC16F File Offset: 0x000DA36F
		public void SetForward(ObjectAddress val)
		{
			this.Forward = val;
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x000DC178 File Offset: 0x000DA378
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.ObjectId.GetHashCode();
			if (this.HasForward)
			{
				num ^= this.Forward.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x000DC1D0 File Offset: 0x000DA3D0
		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			return subscribeRequest != null && this.HasAgentId == subscribeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(subscribeRequest.AgentId)) && this.ObjectId.Equals(subscribeRequest.ObjectId) && this.HasForward == subscribeRequest.HasForward && (!this.HasForward || this.Forward.Equals(subscribeRequest.Forward));
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x0600462C RID: 17964 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x000DC258 File Offset: 0x000DA458
		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x000DC262 File Offset: 0x000DA462
		public void Deserialize(Stream stream)
		{
			SubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x000DC26C File Offset: 0x000DA46C
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return SubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x000DC278 File Offset: 0x000DA478
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			SubscribeRequest.DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x000DC294 File Offset: 0x000DA494
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x000DC2BC File Offset: 0x000DA4BC
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
						else if (instance.Forward == null)
						{
							instance.Forward = ObjectAddress.DeserializeLengthDelimited(stream);
						}
						else
						{
							ObjectAddress.DeserializeLengthDelimited(stream, instance.Forward);
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

		// Token: 0x06004633 RID: 17971 RVA: 0x000DC3A4 File Offset: 0x000DA5A4
		public void Serialize(Stream stream)
		{
			SubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x000DC3B0 File Offset: 0x000DA5B0
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
			if (instance.HasForward)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Forward.GetSerializedSize());
				ObjectAddress.Serialize(stream, instance.Forward);
			}
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x000DC42C File Offset: 0x000DA62C
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
			if (this.HasForward)
			{
				num += 1U;
				uint serializedSize2 = this.Forward.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1U;
		}

		// Token: 0x0400178B RID: 6027
		public bool HasAgentId;

		// Token: 0x0400178C RID: 6028
		private EntityId _AgentId;

		// Token: 0x0400178E RID: 6030
		public bool HasForward;

		// Token: 0x0400178F RID: 6031
		private ObjectAddress _Forward;
	}
}
