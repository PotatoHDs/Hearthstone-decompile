using System;
using System.IO;

namespace bnet.protocol.user_manager.v1
{
	// Token: 0x020002EC RID: 748
	public class SubscribeRequest : IProtoBuf
	{
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002C6C RID: 11372 RVA: 0x00098DB3 File Offset: 0x00096FB3
		// (set) Token: 0x06002C6D RID: 11373 RVA: 0x00098DBB File Offset: 0x00096FBB
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

		// Token: 0x06002C6E RID: 11374 RVA: 0x00098DCE File Offset: 0x00096FCE
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x00098DD7 File Offset: 0x00096FD7
		// (set) Token: 0x06002C70 RID: 11376 RVA: 0x00098DDF File Offset: 0x00096FDF
		public ulong ObjectId { get; set; }

		// Token: 0x06002C71 RID: 11377 RVA: 0x00098DE8 File Offset: 0x00096FE8
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x00098DF4 File Offset: 0x00096FF4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num ^ this.ObjectId.GetHashCode();
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x00098E38 File Offset: 0x00097038
		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			return subscribeRequest != null && this.HasAgentId == subscribeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(subscribeRequest.AgentId)) && this.ObjectId.Equals(subscribeRequest.ObjectId);
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002C74 RID: 11380 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x00098E95 File Offset: 0x00097095
		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x00098E9F File Offset: 0x0009709F
		public void Deserialize(Stream stream)
		{
			SubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x00098EA9 File Offset: 0x000970A9
		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return SubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x00098EB4 File Offset: 0x000970B4
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			SubscribeRequest.DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x00098ED0 File Offset: 0x000970D0
		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x00098EF8 File Offset: 0x000970F8
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
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

		// Token: 0x06002C7B RID: 11387 RVA: 0x00098FAA File Offset: 0x000971AA
		public void Serialize(Stream stream)
		{
			SubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x00098FB4 File Offset: 0x000971B4
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
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x00099004 File Offset: 0x00097204
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
			return num + 1U;
		}

		// Token: 0x04001262 RID: 4706
		public bool HasAgentId;

		// Token: 0x04001263 RID: 4707
		private EntityId _AgentId;
	}
}
