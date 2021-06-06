using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x0200033F RID: 831
	public class BatchUnsubscribeRequest : IProtoBuf
	{
		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06003382 RID: 13186 RVA: 0x000ABC77 File Offset: 0x000A9E77
		// (set) Token: 0x06003383 RID: 13187 RVA: 0x000ABC7F File Offset: 0x000A9E7F
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

		// Token: 0x06003384 RID: 13188 RVA: 0x000ABC92 File Offset: 0x000A9E92
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x000ABC9B File Offset: 0x000A9E9B
		// (set) Token: 0x06003386 RID: 13190 RVA: 0x000ABCA3 File Offset: 0x000A9EA3
		public List<EntityId> EntityId
		{
			get
			{
				return this._EntityId;
			}
			set
			{
				this._EntityId = value;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06003387 RID: 13191 RVA: 0x000ABC9B File Offset: 0x000A9E9B
		public List<EntityId> EntityIdList
		{
			get
			{
				return this._EntityId;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06003388 RID: 13192 RVA: 0x000ABCAC File Offset: 0x000A9EAC
		public int EntityIdCount
		{
			get
			{
				return this._EntityId.Count;
			}
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x000ABCB9 File Offset: 0x000A9EB9
		public void AddEntityId(EntityId val)
		{
			this._EntityId.Add(val);
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x000ABCC7 File Offset: 0x000A9EC7
		public void ClearEntityId()
		{
			this._EntityId.Clear();
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x000ABCD4 File Offset: 0x000A9ED4
		public void SetEntityId(List<EntityId> val)
		{
			this.EntityId = val;
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x000ABCDD File Offset: 0x000A9EDD
		// (set) Token: 0x0600338D RID: 13197 RVA: 0x000ABCE5 File Offset: 0x000A9EE5
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

		// Token: 0x0600338E RID: 13198 RVA: 0x000ABCF5 File Offset: 0x000A9EF5
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000ABD00 File Offset: 0x000A9F00
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			foreach (EntityId entityId in this.EntityId)
			{
				num ^= entityId.GetHashCode();
			}
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000ABD94 File Offset: 0x000A9F94
		public override bool Equals(object obj)
		{
			BatchUnsubscribeRequest batchUnsubscribeRequest = obj as BatchUnsubscribeRequest;
			if (batchUnsubscribeRequest == null)
			{
				return false;
			}
			if (this.HasAgentId != batchUnsubscribeRequest.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(batchUnsubscribeRequest.AgentId)))
			{
				return false;
			}
			if (this.EntityId.Count != batchUnsubscribeRequest.EntityId.Count)
			{
				return false;
			}
			for (int i = 0; i < this.EntityId.Count; i++)
			{
				if (!this.EntityId[i].Equals(batchUnsubscribeRequest.EntityId[i]))
				{
					return false;
				}
			}
			return this.HasObjectId == batchUnsubscribeRequest.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(batchUnsubscribeRequest.ObjectId));
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06003391 RID: 13201 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x000ABE58 File Offset: 0x000AA058
		public static BatchUnsubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BatchUnsubscribeRequest>(bs, 0, -1);
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x000ABE62 File Offset: 0x000AA062
		public void Deserialize(Stream stream)
		{
			BatchUnsubscribeRequest.Deserialize(stream, this);
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x000ABE6C File Offset: 0x000AA06C
		public static BatchUnsubscribeRequest Deserialize(Stream stream, BatchUnsubscribeRequest instance)
		{
			return BatchUnsubscribeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x000ABE78 File Offset: 0x000AA078
		public static BatchUnsubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			BatchUnsubscribeRequest batchUnsubscribeRequest = new BatchUnsubscribeRequest();
			BatchUnsubscribeRequest.DeserializeLengthDelimited(stream, batchUnsubscribeRequest);
			return batchUnsubscribeRequest;
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x000ABE94 File Offset: 0x000AA094
		public static BatchUnsubscribeRequest DeserializeLengthDelimited(Stream stream, BatchUnsubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BatchUnsubscribeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000ABEBC File Offset: 0x000AA0BC
		public static BatchUnsubscribeRequest Deserialize(Stream stream, BatchUnsubscribeRequest instance, long limit)
		{
			if (instance.EntityId == null)
			{
				instance.EntityId = new List<EntityId>();
			}
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
							instance.ObjectId = ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.EntityId.Add(bnet.protocol.EntityId.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = bnet.protocol.EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					bnet.protocol.EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x000ABFA2 File Offset: 0x000AA1A2
		public void Serialize(Stream stream)
		{
			BatchUnsubscribeRequest.Serialize(stream, this);
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000ABFAC File Offset: 0x000AA1AC
		public static void Serialize(Stream stream, BatchUnsubscribeRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				bnet.protocol.EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.EntityId.Count > 0)
			{
				foreach (EntityId entityId in instance.EntityId)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, entityId.GetSerializedSize());
					bnet.protocol.EntityId.Serialize(stream, entityId);
				}
			}
			if (instance.HasObjectId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000AC06C File Offset: 0x000AA26C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.EntityId.Count > 0)
			{
				foreach (EntityId entityId in this.EntityId)
				{
					num += 1U;
					uint serializedSize2 = entityId.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasObjectId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			return num;
		}

		// Token: 0x04001402 RID: 5122
		public bool HasAgentId;

		// Token: 0x04001403 RID: 5123
		private EntityId _AgentId;

		// Token: 0x04001404 RID: 5124
		private List<EntityId> _EntityId = new List<EntityId>();

		// Token: 0x04001405 RID: 5125
		public bool HasObjectId;

		// Token: 0x04001406 RID: 5126
		private ulong _ObjectId;
	}
}
